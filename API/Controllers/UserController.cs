using API.Models;
using AutoMapper;
using DataAccess;
using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataAccess.WindowAzurePushNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [Route("GetProfessionalsByLocationIDForUserApp")]
        public TopReferralDataContract GetProfessionalsByLocationIDForUserApp(long loggedInUserID, int pageIndex,int  pageSize,long locationID)
        {
            User user = new User();
            return user.ProfessionalsByLocationIDForUserApp(loggedInUserID, pageIndex, pageSize, locationID);
        }

        [Route("NewUpdates")]
        public NewUserUpdates GetNewUpdates(long userID)
        {
            User user = new User();
           return user.GetNewUserUpdates(userID);
        }

        [Route("AppFeedBack")]
        public ResultData SaveAppFeedBack(AppFeedBackBindingModel model)
        {
            AppFeedback appFeedBack = new AppFeedback();
            return appFeedBack.Save(model.UserID, model.FeedBack, model.IsProAppFeedback);
        }
        [Route("UserAcitivityCounts")]
        [HttpGet]
        public CountUserActivites_Result UserAcitivityCounts(long userID)
        {
            return new User().GetUserActivityCounts(userID);
        }
        [Route("ProfessionalAcitivityCounts")]
        [HttpGet]
        public CountProfessionalActivites_Result ProfessionalAcitivityCounts(long userID)
        {
            return new User().GetProfessionalActivityCounts(userID);
        }

        [Route("SocialLogin")]
        public UserDataContract SocialLogin(SocialLoginBindingModel signupData)
        {
            UserDataContract userData = new UserDataContract();
            User user = new User();
            user.Email = signupData.Email;
            user.FirstName = signupData.FirstName;
            user.LastName = signupData.LastName;

            user.Type = (int)signupData.UserType;
            user.IsDeleted = false;
            user.RoleId = (int)HelperEnums.Role.User;

            user.IsApprovedByAdmin = true;
            user.IsVerified = true;
            user.IsProfileUpdated = false;

            userData = Mapper.Map<User, UserDataContract>(user);
            var socialIDAlreadyExists = user.IsSocialIDAlreadyExists((int)(signupData.SocialType), signupData.SocialID);
            if (!socialIDAlreadyExists)
            {
                // user.Password = Common.Encode(signupData.Password);
                //user.VerificationCode = Common.NewVerificationCode().Substring(0, 6);
                var userSocialData = user.IsEmailAlreadyExists(signupData.Email);
                if (userSocialData == null)
                {
                    user.UniqueId = Common.CreateUniqueId().Substring(0, 6);
                    user.SocialType = signupData.SocialType;
                    user.SocialID = signupData.SocialID;

                    try
                    {

                        if (!string.IsNullOrEmpty(signupData.Image))
                        {
                            var userImageName = DateTime.Now.ToFileTimeUtc() + ".jpg";
                            File.WriteAllBytes(HttpContext.Current.Server.MapPath(Common.UserImagesPath) + userImageName, Convert.FromBase64String(signupData.Image));

                            user.Image = userImageName;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    user.Add(user);
                    user.Image = !string.IsNullOrEmpty(user.Image) ? Common.UserImagesPath + user.Image : Common.NoImageIcon;
                    userData = Mapper.Map<User, UserDataContract>(user);
                    userData.ResultStatus = 1;
                    userData.ResultDescription = "User Registered Successfully";
                    user.AddUserDevice(signupData.Platform, signupData.DeviceToken, userData.Id);
                    return userData;
                }
                else
                {
                    if (userSocialData.SocialType == HelperEnums.SocialType.Facebook)
                    {
                        if (userSocialData.Type == (int)HelperEnums.UserType.User)
                        {
                            userData.ResultDescription = "You are already registered With facebook via Referlocals user app.";
                        }
                        else
                        {
                            userData.ResultDescription = "You are already registered With facebook via Referlocals Pro app.";

                        }
                    }
                    else if (userSocialData.SocialType == HelperEnums.SocialType.Google)
                    {
                        if (userSocialData.Type == (int)HelperEnums.UserType.User)
                        {
                            userData.ResultDescription = "You are already registered With google via Referlocals user app.";
                        }
                        else
                        {
                            userData.ResultDescription = "You are already registered With google via Referlocals Pro app.";

                        }
                    }
                    else
                    {
                        userData.ResultDescription = "You are already registered With this email address.";
                    }
                    userData.ResultStatus = 0;

                }

            }
            else
            {
                userData = user.GetUserBySocialID(signupData.SocialID, (int)signupData.SocialType);
                userData.ResultStatus = 2;
                userData.ResultDescription = "SocialID already exists. Please Login this User";
            }
            return userData;
        }

        [Route("TestEmail")]
        public void TestEmail()
        {
            Task.Run(() =>
            {
                string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                string EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();

                var client = new SmtpClient("smtp.gmail.com", 587)
                {


                    Credentials = new NetworkCredential(FromEmail, EmailPassword),
                    EnableSsl = true
                };
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add("ravi@impact-works.com");
                mailMessage.From = new MailAddress("support@referlocals.com", "Referlocals");
                mailMessage.Subject = "test";
                mailMessage.Body = "test";

                client.Send(mailMessage);
            });
        }
        [Route("Login")]
        public UserDataContract Login(LoginBindingModel loginModel)
        {
            User log = new User();

            if ((loginModel.Phone != "" || loginModel.Email != "") && loginModel.Password != "")
            {
                var pwd = Common.Encode(loginModel.Password);
                var userData = log.GetUserByEmailPwd(loginModel.Email, loginModel.CountryCode, loginModel.Phone, pwd, HelperEnums.UserType.User);
                if (userData != null)
                {
                    if (userData.Id > 0)
                    {


                        if (!userData.IsVerified.GetValueOrDefault())
                        {
                            userData.ResultStatus = 0;
                            userData.ResultDescription = "User is not verified";
                        }
                        else if (!userData.IsApprovedByAdmin.GetValueOrDefault())
                        {
                            userData.ResultStatus = 0;
                            userData.ResultDescription = "User is not approved";
                        }
                        else
                        {
                            userData.ResultStatus = 1;
                            userData.ResultDescription = "Login Successfully";
                            log.AddUserDevice(loginModel.Platform, loginModel.DeviceToken, userData.Id);
                           
                            // Task.Run(() =>
                            //{
                            //    log.AddUpdateWindowAzureTokenForUser(userData.Id, loginModel.DeviceToken, loginModel.Platform);
                            //});
                            AsyncAwaitOperations obj = new AsyncAwaitOperations();
                            obj.AddUpdateWindowAzureTokenForUser(userData.Id, loginModel.DeviceToken, loginModel.Platform);
                            //try
                            //{
                            //    Task.Factory.StartNew(() =>
                            //    {
                            //        Common.SendEmailWithGenericTemplate("ravi@impact-works.com", " notification reg start", "test");
                            //        try
                            //        {
                            //            Common.SendEmailWithGenericTemplate("ravi@impact-works.com", " notification reg start", "test");
                            //            log.AddUpdateWindowAzureTokenForUser(userData.Id, loginModel.DeviceToken, loginModel.Platform);
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            Common.SendEmailWithGenericTemplate("ravi@impact-works.com", " inner controller", "test");
                            //        }

                            //    });
                            //}
                            //catch (Exception ex)
                            //{
                            //    Common.SendEmailWithGenericTemplate("ravi@impact-works.com", "controller", "test");

                            //}


                        }
                    }
                    else
                    {
                        userData.ResultStatus = 0;
                        userData.ResultDescription = "Invalid Credentials";
                    }
                }
                else
                {
                    userData.ResultStatus = 0;
                    userData.ResultDescription = "Invalid Credentials";
                }
                return userData;
            }
            return null;

        }

        [Route("SignUp")]
        public UserDataContract SignUp(LoginBindingModel signupData)
        {
            UserDataContract userData = new UserDataContract();
            User user = new User();
            if (!string.IsNullOrEmpty(signupData.Phone))
            {
                user.CountryCode = signupData.CountryCode;
                user.PhoneNumber = signupData.Phone;
            }
            if (!string.IsNullOrEmpty(signupData.Email))
            {
                user.Email = signupData.Email;
            }
            user.Type = (int)HelperEnums.UserType.User;
            user.IsDeleted = false;
            user.RoleId = (int)HelperEnums.Role.User;
            user.IsApprovedByAdmin = true;
            user.IsVerified = false;
            user.IsProfileUpdated = false;


            userData = Mapper.Map<User, UserDataContract>(user);
            if (!user.GetRecord())
            {
                user.Password = Common.Encode(signupData.Password);
                user.VerificationCode = Common.NewVerificationCode().Substring(0, 6);
                user.UniqueId = Common.CreateUniqueId().Substring(0, 6);
                user.Add(user);

                if (!string.IsNullOrEmpty(signupData.Phone) && !string.IsNullOrEmpty(signupData.Email))
                {
                    Common.SendCodeThroughSms(user.CountryCode + user.PhoneNumber, user.VerificationCode);

                    EmailMessage message = new EmailMessage();
                    message.Message = "Hi ," + user.FirstName + "<br/>" + Common.VerifyBodyMessage.Replace("##OTP##", user.VerificationCode);
                    message.Subject = Common.OTPSubject;
                    message.To = user.Email;
                    bool ret = Common.SendEmail(message);
                    if (ret)
                    {
                        //email sent
                    }
                    else
                    {

                        //email not sent
                    }
                }
                else if (!string.IsNullOrEmpty(signupData.Phone))
                {
                    var isSmsSent = Common.SendCodeThroughSms(signupData.CountryCode + signupData.Phone, user.VerificationCode);

                }
                else if (!string.IsNullOrEmpty(signupData.Email))
                {
                    EmailMessage message = new EmailMessage();
                    message.Message = "Hi ," + user.FirstName + "<br/>" + Common.VerifyBodyMessage.Replace("##OTP##", user.VerificationCode);
                    message.Subject = Common.OTPSubject;
                    message.To = user.Email;
                    bool ret = Common.SendEmail(message);
                    if (ret)
                    {
                        //email sent;
                    }
                    else
                    {
                        //email not sent;
                    }
                }


                userData = Mapper.Map<User, UserDataContract>(user);
                userData.ResultStatus = 1;
                userData.ResultDescription = "User Registered Successfully";
                user.AddUserDevice(signupData.Platform, signupData.DeviceToken, userData.Id);
                return userData;


            }
            else
            {

                userData.ResultStatus = 0;
                userData.ResultDescription = "User Already Exists";
            }
            return userData;
        }

        [Route("Verify")]
        public UserDataContract Verify(long userID, string OTP)
        {
            UserDataContract userData = new UserDataContract();
            User log = new User();
            log.Id = userID;
            log.IsDeleted = false;
            bool flag = log.GetRecord();
            userData = Mapper.Map<User, UserDataContract>(log);
            if (flag)
            {

                if (log.IsVerified)
                {

                    userData.ResultStatus = 1;
                    userData.ResultDescription = "User Verified Successfully";
                    Common.SendCodeThroughSms(log.CountryCode+log.PhoneNumber, Common.SMSBodyForUserSignup);
                    return userData;
                }
                else
                {
                    if (log.VerificationCode == OTP)
                    {

                        log.IsVerified = true;
                        var isUpdated = log.Edit(log);
                        if (isUpdated)
                        {
                            userData.IsVerified = true;
                            userData.ResultStatus = 1;
                            Common.SendCodeThroughSms(log.CountryCode + log.PhoneNumber, Common.SMSBodyForUserSignup);
                            userData.ResultDescription = "User Verified Successfully";
                            return userData;
                        }
                        else
                        {
                            userData.ResultStatus = 0;
                            userData.ResultDescription = "server error";
                            return userData;

                        }

                    }
                    else
                    {
                        userData.ResultStatus = 0;
                        userData.ResultDescription = "Invalid OTP";
                        return userData;


                    }
                }
            }
            else
            {
                userData.ResultStatus = 0;
                userData.ResultDescription = "User doesn't exist";
                return userData;

            }
        }

        [Route("Update")]
        public UserDataContract Update(ProfileBindingModel profileData)
        {

            User user = new User();
            var resultData = user.CheckEmailPhoneAlreadyExists(profileData.Email, profileData.PhoneNumber, profileData.CountryCode, profileData.Id);
            if (resultData.ResultStatus == 1)
            {
                var userData = Mapper.Map<ProfileBindingModel, User>(profileData);
                var data = user.Update(userData);
                if (data.Id > 0)
                {
                    data.ResultDescription = "Success";
                    data.ResultStatus = 1;

                }
                else
                {
                    data = new UserDataContract();
                    data.ResultDescription = "User doesn't exist";
                    data.ResultStatus = 0;
                }

                return data;
            }
            else
            {
                UserDataContract data = new UserDataContract();
                data.ResultDescription = resultData.ResultDescription;
                data.ResultStatus = resultData.ResultStatus;
                return data;
            }
        }

        [Route("IsEmailExists")]
        public ResultData IsEmailExists(string email, string phonenumber, long countryCode, long userID)
        {
            User user = new DataAccess.Classes.User();
            return user.CheckEmailPhoneAlreadyExists(email, phonenumber, countryCode, userID);
        }
        [Route("Logout")]
        public ResultData Logout(LogoutBindingModel logoutModel)
        {
            User user = new User();
            user.UpdateUserDevice(null, null, null, logoutModel.UserID);

            return new ResultData { ResultDescription = "Logout Successfully", ResultStatus = 1 };
        }

        [Route("ForgotPassword")]
        public ForgotPasswordDataContract ForgotPassword(ForgotPasswordBindingModel model)
        {

            User user = new User();
            if (!string.IsNullOrEmpty(model.Phone))
            {
                user.CountryCode = model.CountryCode;
                user.PhoneNumber = model.Phone;
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                user.Email = model.Email;
            }
            user.IsDeleted = false;
            user.IsApprovedByAdmin = true;
            if (user.GetRecord())
            {
                user.VerificationCode = Common.NewVerificationCode().Substring(0, 6);
                user.UpdateVerificationCode(user.VerificationCode, user.Id);
                if (!string.IsNullOrEmpty(model.Phone) && !string.IsNullOrEmpty(model.Email))
                {
                    Common.SendCodeThroughSms(user.CountryCode + user.PhoneNumber, user.VerificationCode);

                    EmailMessage message = new EmailMessage();
                    message.Message = "Hi ," + user.FirstName + "<br/>" + Common.ForgetPasswordBodyMessage.Replace("##OTP##", user.VerificationCode);
                    message.Subject = Common.ForgetPasswordSubject;
                    message.To = user.Email;
                    bool ret = Common.SendEmail(message);

                }
                else if (!string.IsNullOrEmpty(model.Phone))
                {
                    var isSmsSent = Common.SendCodeThroughSms(model.CountryCode + model.Phone, "Your OTP to reset password is " + user.VerificationCode);

                }
                else if (!string.IsNullOrEmpty(model.Email))
                {
                    EmailMessage message = new EmailMessage();
                    message.Message = "Hi ," + user.FirstName + "<br/>" + Common.ForgetPasswordBodyMessage.Replace("##OTP##", user.VerificationCode);
                    message.Subject = Common.ForgetPasswordSubject;
                    message.To = user.Email;
                    bool ret = Common.SendEmail(message);

                }
                return new ForgotPasswordDataContract { UserID = user.Id, ResultDescription = "OTP Sent Successfully", ResultStatus = 1 };
            }
            else
            {
                return new ForgotPasswordDataContract { ResultDescription = "Email/Phone dosen't exist", ResultStatus = 0 };
            }

        }

        [Route("ResetPassword")]
        public ResultData ResetPassword(ResetPasswordBindingModel model)
        {
            User user = new User();
            return user.ResetPassword(Common.Encode(model.NewPassword), model.VerificationCode, model.UserID);
        }

        [Route("ChangePassword")]
        public ResultData ChangePassword(ChangePwdBindingModel model)
        {
            User user = new User();
            return user.ChangePassword(Common.Encode(model.NewPassword), Common.Encode(model.OldPassword), model.UserID);
        }

        [Route("UserByID")]
        [HttpGet]
        public UserDataContract UserByID(long userID)
        {
            User user = new User();
            return user.GetUserByID(userID);
        }

        [Route("GetUserProfile")]
        public UserProfileDataContract GetUserProfile(long userID, long loggedInUserID)
        {
            User user = new User();
            return user.GetUserProfile(userID, loggedInUserID);
        }

    }
}
