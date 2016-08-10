using API.Models;
using AutoMapper;
using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/referral")]
    public class ReferralController : ApiController
    {
        [Route("Search")]
        public ReferralDataContract Search(SearchReferralBindingModel model)
        {
            return BindResult(model);
        }
        [Route("TopReferrals")]
        [HttpGet]
        public TopReferralDataContract TopReferrals(long userID,long locationID, int pageIndex, int pageSize)
        {
            User user = new User();
            return user.TopReferrals(userID,pageIndex, pageSize, locationID);
        }
        private ReferralDataContract BindResult(SearchReferralBindingModel model)
        {
            ReferralDataContract referrals = new ReferralDataContract();
            User obj = new User();
            List<ProfessionalDataContract> lst = new List<ProfessionalDataContract>();

            obj.CountryCode = Convert.ToInt64(model.CountryCode);
            obj.PhoneNumber = model.Phone;
            obj.Email = model.Email;
            obj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            if (obj.SearchProfessionalHasRecords(model.CountryCode,model.Phone, model.Email))
            {
                lst = obj.SearchProfessionalForReferral(model.Phone, model.Email, model.CountryCode);
                //var userData = Mapper.Map<List<User>, List<UserDataContract>>(lst);
                referrals.UserList = lst;
                SubCategory subcategory = new SubCategory();
                subcategory.Id = Convert.ToInt64(model.SubCategoryID);
                subcategory.GetRecord();
                if (subcategory.DataRecieved == true)
                {
                    referrals.SubCategoryName = subcategory.Name;
                }
                referrals.IsHavingSameCategory = false;
                foreach (var item in lst)
                {
                    foreach (var subCatId in item.ProfessionalUrls)
                    {
                        if (subCatId.SubCategoryID == model.SubCategoryID)
                        {
                            referrals.IsHavingSameCategory = true;
                        }
                    }


                }
                referrals.ResultDescription = "Professionals Found";
                referrals.ResultStatus = 1;
                return referrals;

            }
            else
            {
                User Log = new User();
                Log.CountryCode = Convert.ToInt64(model.CountryCode);
                Log.PhoneNumber = model.Phone;
                if (!Log.GetRecord())
                {
                    Log.CountryCode = 0;
                    Log.PhoneNumber = "";
                    Log.Email = model.Email;
                    if (!Log.GetRecord())
                    {
                        Log.CountryCode = Convert.ToInt64(model.CountryCode);
                        Log.PhoneNumber = model.Phone;
                        if (!string.IsNullOrEmpty(model.Name))
                        {

                            string[] namesplit = model.Name.Split(' ');
                            if (!string.IsNullOrEmpty(namesplit[0]))
                            {
                                obj.FirstName = namesplit[0];
                            }
                            if (namesplit.Length > 1)
                            {
                                if (!string.IsNullOrEmpty(namesplit[1]))
                                {
                                    obj.LastName = namesplit[1];
                                }
                            }
                            obj.Type = (int)HelperEnums.UserType.Professional;
                            obj.RoleId = (int)HelperEnums.Role.User;
                            obj.Password = Common.Encode(Common.DefaultUserPassword);
                            obj.VerificationCode = Common.NewVerificationCode().Substring(0, 6);
                            obj.UniqueId = Common.CreateUniqueId().Substring(0, 6);
                            if (obj.Add(obj))
                            {
                                SubCategory subcategory = new SubCategory();
                                subcategory.Id = Convert.ToInt64(model.SubCategoryID);
                                subcategory.GetRecord();
                                if (subcategory.DataRecieved == true)
                                {
                                    //  referrals.SubCategoryName = subcategory.Name;
                                    subcategory.Name = subcategory.Name;
                                    if (subcategory.GetRecord())
                                    {
                                        UserSubCategoryMapping cat = new UserSubCategoryMapping();
                                        cat.UserId = obj.Id;
                                        cat.SubCategoryId = subcategory.Id;
                                        cat.IsApprovedByAdmin = true;
                                        if (cat.Add(cat))
                                        {
                                            referrals.UserID = obj.Id;
                                            referrals.ResultDescription = "New professional added in unregistered";
                                            referrals.ResultStatus = 1;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        referrals.ResultDescription = "Email exists but phone number is not matched";
                        referrals.ResultStatus = 0;
                        //Response.Write("<script>alert('Email exist but phone number is not matched');</script>");
                    }
                }
                else
                {
                    referrals.ResultDescription = "Phone number exists but email is not matched";
                    referrals.ResultStatus = 0;


                }
                return referrals;
            }
        }

        [Route("Add")]
        public ResultData AddReferral(ReferralBindingModel model)
        {
            Referal obj = new Referal();
            obj.UserId = model.UserID;
            obj.SenderId = model.SenderID;
            obj.Comment = model.Comment;
            obj.IsSatisfied = model.IsSatisfied;
            obj.IsRefered = model.IsReferred;
            obj.CityId = model.LocationID;
            obj.IsFlag = false;
            obj.IsDeleted = false;
            obj.IsApprovedByAdmin = true;
            obj.CreatedDate = DateTime.UtcNow;
            obj.UpdatedDate = DateTime.UtcNow;
            if (obj.Add(obj))
            {
                return new ResultData { ResultDescription = "Referral added successfully", ResultStatus = 1 };
            }
            else
            {
                return new ResultData { ResultDescription = "Oops.. There was some error", ResultStatus = 0 };
            }
        }

        [Route("ReferralsGiven")]
        public ReferralWithUserDataWrapper GetReferralGiven(long senderID, int pageIndex, int pageSize, HelperEnums.BooleanValues orderBy)
        {
            Referal referal = new Referal();
            return referal.GetReferralsGiven(senderID, pageIndex, pageSize, orderBy );
        }
        [Route("Referrals")]
        public ReferralWithUserDataWrapper GetReferrals(long userID, int pageIndex, int pageSize)
        {
            Referal referal = new Referal();
            return referal.GetReferrals(userID, pageIndex, pageSize);
        }
        [Route("FlagReferral")]
        [HttpGet]
        public ResultData FlagReferral(long referralID)
        {
            Referal referal = new Referal();
            if (referal.FlagReferral(referralID))
            {
                EmailMessage message = new EmailMessage();
                message.To = Common.FromEmail;
                message.From = Common.FromEmail;
                message.Subject = Common.SubjectFlagReferral;
                message.Message = Common.EmailBodyFlagReferral;
                Common.SendEmail(message);
                return new ResultData { ResultDescription = "Referral Flagged Successfully", ResultStatus = 1 };
            }
            
            return new ResultData { ResultDescription = "Oops.. something went wrong.", ResultStatus = 0 };

        }

        [Route("SendReferralRequest")]
        [HttpGet]
        public ResultData SendReferralRequest(long userID, long loggedInUserID)
        {
            User user = new User();
            var userData= user.GetUserDetails(userID);
            var proData= user.GetProfessionalByID(loggedInUserID);
            if (userData != null)
            {
                Common.SendEmailWithGenericTemplate(userData.Email,"Referral Request","Hey "+userData.FirstName+" "+user.LastName+"<br/>"+ proData.FirstName+" "+proData.LastName+ " has requested refferal from you. If this professional has done any of your job, refer him by <a href='"+Common.WebsiteHostNameForLink + "/AddReferal?Id="+proData.UniqueId+"'>clicking here<a>.");
                return new ResultData { ResultDescription = "Request Sent", ResultStatus = 1 };
            }
            return new ResultData { ResultDescription = "technical problem", ResultStatus = 0 };
        }
    }
}
