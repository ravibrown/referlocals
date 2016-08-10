using API.Models;
using AutoMapper;
using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataAccess.WindowAzurePushNotification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/Professional")]
    public class ProfessionalController : ApiController
    {
        [Route("ProfessionalsByLocationIDForProApp")]
        public TopReferralDataContract ProfessionalsByLocationIDForProApp(LocationAndCitiesServeBindingModel model)
        {
            User user = new User();
            return user.ProfessionalsByLocationIDForProApp(model.loggedInUserID, model.pageIndex, model.pageSize, model.locationID, model.citiesServe);
        }
        [Route("NewUpdates")]
        public NewProfessionalUpdates GetNewUpdates(long userID)
        {
            User user = new User();
            return user.GetNewProfessionalUpdates(userID);
        }

        [HttpGet]
        public void Test()
        {
            User user = new DataAccess.Classes.User();
            List<long> test1 = new List<long>();
            test1.Add(6);
            test1.Add(4);
          var data=  user.SearchProfessionalWithLocationForPushNotification(898,test1);
        }


        [Route("Search")]
        [HttpGet]
        public TopReferralDataContract ProfessionalsByLocationAndSubcategoryID(long subCategoryID, long userID, long locationID, int pageIndex, int pageSize)
        {
            User user = new User();
            return user.ProfessionalsByLocationAndSubcategoryID(subCategoryID, userID, pageIndex, pageSize, locationID);
        }

        [Route("Login")]
        public ProfessionalDataContract Login(LoginBindingModel loginModel)
        {

            User user = new User();
            ProfessionalDataContract userData = new ProfessionalDataContract();
            if ((loginModel.Phone != "" || loginModel.Email != "") && loginModel.Password != "")
            {
                var pwd = Common.Encode(loginModel.Password);
                userData = user.GetProfessionalByEmailPwd(loginModel.Email, loginModel.CountryCode, loginModel.Phone, pwd, HelperEnums.UserType.Professional);

                if (userData.Id > 0)
                {



                    //UserCityMapping cityMapping = new UserCityMapping();
                    //    var citiesIServe = cityMapping.GetCitiesIServe(userData.Id);
                    //    userData.CitiesIServe = citiesIServe;

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
                        user.AddUserDevice(loginModel.Platform, loginModel.DeviceToken, userData.Id);
                        userData.ResultDescription = "Login Successfully";
                        //Task.Run(() =>
                        //{
                        //    user.AddUpdateWindowAzureTokenForPro(userData.Id,loginModel.DeviceToken,loginModel.Platform);
                        //});
                        AsyncAwaitOperations obj = new AsyncAwaitOperations();
                        obj.AddUpdateWindowAzureTokenForPro(userData.Id, loginModel.DeviceToken, loginModel.Platform);


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
                userData.ResultDescription = "Please fill appropriate data";
            }
            return userData;

        }

        [Route("SocialLogin")]
        public ProfessionalDataContract SocialLogin(SocialLoginBindingModel signupData)
        {
            ProfessionalDataContract userData = new ProfessionalDataContract();
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

            userData = Mapper.Map<DataAccess.Classes.User, ProfessionalDataContract>(user);
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
                    userData = Mapper.Map<DataAccess.Classes.User, ProfessionalDataContract>(user);
                    userData.ResultStatus = 1;
                    userData.ResultDescription = "User Registered Successfully";
                    user.AddUserDevice(signupData.Platform, signupData.DeviceToken, userData.Id);
                    return userData;
                }
                else
                {
                    userData.ResultStatus = 0;
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
                }

            }
            else
            {
                userData = user.GetProfessionalBySocialID(signupData.SocialID, (int)signupData.SocialType);
                userData.ResultStatus = 2;
                userData.ResultDescription = "SocialID already exists. Please Login this User";
            }
            return userData;
        }
        [Route("SignUp")]
        public ProfessionalDataContract SignUp(LoginBindingModel signupData)
        {
            ProfessionalDataContract userData = new ProfessionalDataContract();
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
            user.IsDeleted = false;
            user.Type = (int)HelperEnums.UserType.Professional;
            user.RoleId = (int)HelperEnums.Role.User;
            user.IsApprovedByAdmin = true;
            user.IsVerified = false;
            user.IsProfileUpdated = false;
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

                userData = Mapper.Map<DataAccess.Classes.User, ProfessionalDataContract>(user);
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
        public ProfessionalDataContract Verify(long userID, string OTP)
        {
            ProfessionalDataContract userData = new ProfessionalDataContract();
            User log = new User();
            log.Id = userID;
            log.IsDeleted = false;
            bool flag = log.GetRecord();
            userData = Mapper.Map<User, ProfessionalDataContract>(log);
            if (flag)
            {

                if (log.IsVerified)
                {

                    userData.ResultStatus = 1;
                    userData.ResultDescription = "User Verified Successfully";
                    Common.SendCodeThroughSms(log.CountryCode + log.PhoneNumber, Common.SMSBodyForProfessionalSignup);
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
                            Common.SendCodeThroughSms(log.CountryCode + log.PhoneNumber, Common.SMSBodyForProfessionalSignup);

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

        [Route("UpdateSubCategories")]
        public ProfessionalDataContract Update(ProfessionalSubcategoryBindingModel model)
        {
            User user = new User();
            return user.UpdateProfessionalSubcategories(model.SubCategoryIds, model.UserId);
        }


        [Route("Update")]
        public ProfessionalDataContract Update(ProfessionalProfileBindingModel profileData)
        {
            User user = new User();
            var resultData = user.CheckEmailPhoneAlreadyExists(profileData.Email, profileData.PhoneNumber, profileData.CountryCode, profileData.Id);
            if (resultData.ResultStatus == 1)

            {
                var userData = Mapper.Map<ProfessionalProfileBindingModel, User>(profileData);
                var data = user.UpdateProfessionalProfile(userData);
                if (data.Id > 0)
                {

                    //UserCityMapping cityMapping = new UserCityMapping();
                    //var citiesIServe = cityMapping.GetCitiesIServe(userData.Id);
                    //data.CitiesIServe = citiesIServe;

                    data.ResultDescription = "Success";
                    data.ResultStatus = 1;


                }
                else
                {
                    data = new ProfessionalDataContract();
                    data.ResultDescription = "User doesn't exist";
                    data.ResultStatus = 0;
                }

                return data;
            }
            else
            {
                ProfessionalDataContract data = new ProfessionalDataContract();
                data.ResultDescription = resultData.ResultDescription;
                data.ResultStatus = resultData.ResultStatus;
                return data;
            }
        }

        [Route("ProfessionalByID")]
        [HttpGet]
        public ProfessionalDataContract ProfessionalByID(long userID,long loggedInUserID)
        {
            User user = new User();
            return user.GetProfessionalByID(userID, loggedInUserID);
        }

    }
}
