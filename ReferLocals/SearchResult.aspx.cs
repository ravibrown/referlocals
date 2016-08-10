using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DataAccess.DataContractClasses;
using System.Web.UI.HtmlControls;
using System.Threading.Tasks;

namespace ReferLocals
{
    public partial class SearchResult : System.Web.UI.Page
    {
        public static string subcategoryName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindResult();
                BindJobsByUser();
            }
        }
        public void BindJobsByUser()
        {
            var loggedInUserID = SessionService.Pull(SessionKeys.UserId);
            if (loggedInUserID > 0)
            {
                Jobs job = new Jobs();
                var jobData = job.GetJobsByUserID((int)HelperEnums.JobStatus.Open, loggedInUserID, 0, int.MaxValue);
                if (jobData.JobCount > 0)
                {
                    aInvite.Attributes.Add("onclick", "SendJobInvite();");
                    rptJobs.Visible = true;
                    rptJobs.DataSource = jobData.Jobs;
                    rptJobs.DataBind();
                    aInvite.Visible = true;
                }
                else
                {
                    aInvite.Visible = false;
                    divJobs.InnerHtml = "You don't have jobs to invite";
                    rptJobs.Visible = false;
                }

            }
            else
            {
                aInvite.Attributes.Add("onclick", "ShowLoginAlert();");
            }
        }
        public void BindResult()
        {
            User obj = new User();
            List<User> lst = new List<User>();

            if (Request.QueryString["email"] != null && Request.QueryString["phone"] != null && !string.IsNullOrEmpty(Request.QueryString["countryCode"]))
            {
                //obj.UniqueId = Request.QueryString["Id"];
                obj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                lst = obj.SearchProfessional(Request.QueryString["phone"], Request.QueryString["email"], Request.QueryString["countryCode"]);
                SubCategory subcategory = new SubCategory();
                subcategory.Id = Convert.ToInt64(Request.QueryString["subCatID"]);
                subcategory.GetRecord();
                if (subcategory.DataRecieved == true)
                {
                    subcategoryName = subcategory.Name;
                }
                bool isHavingSameCategory = false;
                foreach (var item in lst)
                {
                    foreach (var subCatId in item.SubCategoryIds)
                    {
                        if (Convert.ToString(subCatId.GetValueOrDefault()) == Request.QueryString["subCatID"])
                        {
                            isHavingSameCategory = true;
                        }
                    }


                }

                if (isHavingSameCategory)
                {
                    divDifferenCatMsg.Visible = false;
                }
                else
                {
                    divDifferenCatMsg.Visible = true;
                }


            }
            else if (SessionService.HasKey("LocationId") && Request.QueryString["type"] != null)
            {
                obj.CityId = (Int64)SessionService.Pull("LocationId");
                obj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                SubCategory sub = new SubCategory();
                sub.Name = Request.QueryString["type"].ToString().Replace("-", " ");
                subcategoryName = sub.Name;
                if (sub.GetRecord())
                {
                    obj.SubCategoryId = sub.Id;
                    lst = obj.SearchProfessionalWithLocation(obj.CityId.GetValueOrDefault(), sub.Id); //obj.GetAllWithJoin();
                }

            }
            if (lst != null && lst.Count > 0)
            {
                rptResult.DataSource = lst;
                rptResult.DataBind();
                divNoResult.Visible = false;
                divResult.Visible = true;
            }
            else
            {
                divNoResult.Visible = true;
                divResult.Visible = false;
            }
        }

        protected Int64 GetTotalReferal(Int64 UserId)
        {
            Int64 Records = 0;
            try
            {
                Referal obj = new Referal();
                obj.UserId = UserId;
                Records = obj.GetTotalRecords();
            }
            catch (Exception ex)
            {

            }
            return Records;
        }

        protected void rptResult_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var data = (User)(e.Item.DataItem);
                UserCityMapping obj = new UserCityMapping();
                HiddenField userid =  (HiddenField)e.Item.FindControl("hdnId");
                HtmlAnchor aFavorite = (HtmlAnchor)e.Item.FindControl("aFavorite");
                HtmlGenericControl iFavorite = (HtmlGenericControl)e.Item.FindControl("iFavorite");
                HtmlAnchor aFlag = (HtmlAnchor)e.Item.FindControl("aFlag");
                HtmlGenericControl iFlag = (HtmlGenericControl)e.Item.FindControl("iFlag");
                HtmlAnchor aInviteToViewJob = (HtmlAnchor)e.Item.FindControl("aInviteToViewJob");
                HtmlAnchor aSendMsgModal = (HtmlAnchor)e.Item.FindControl("aSendMsgModal");
                
                var loggedInUserID=SessionService.Pull(SessionKeys.UserId);
                if (loggedInUserID > 0)
                {
                    if (aSendMsgModal != null)
                    {
                        aSendMsgModal.Attributes.Add("onclick", " ShowSendMsgModal(" + data.Id + ")");
                    }
                    if (aInviteToViewJob != null)
                    {
                        aInviteToViewJob.Attributes.Add("onclick", " ShowInviteJobModal(" + data.Id + ")");
                    }
                    if (aFavorite != null)
                    {
                        aFavorite.Attributes.Add("onclick", "SetFavoriteProfessional(" + data.Id + ")");
                        aFavorite.Attributes.Add("class", "aFavorite" + data.Id);

                    }
                    if (iFavorite != null)
                    {
                        iFavorite.Attributes.Remove("class");
                        if (data.IsFavorite)
                        {

                            iFavorite.Attributes.Add("class", "fa fa-heart");
                        }
                        else
                        {
                            iFavorite.Attributes.Add("class", "fa fa-heart-o");
                        }
                    }

                    if (aFlag != null)
                    {
                        //aFlag.Attributes.Add("onclick", "SetFlagProfessional(" + data.Id + ")");
                        aFlag.Attributes.Add("onclick", "OpenFlagProfessionalModal(" + data.Id + ")");
                        
                        aFlag.Attributes.Add("class", "aFlag" + data.Id);

                    }
                    if (iFlag != null)
                    {
                        iFlag.Attributes.Remove("class");
                        if (data.IsFlag)
                        {

                            iFlag.Attributes.Add("class", "fa fa-flag");
                            iFlag.Visible = false;
                        }
                        else
                        {
                            iFlag.Visible = true;
                            iFlag.Attributes.Add("class", "fa fa-flag-o");
                        }
                    }
                }
                else
                {
                    aSendMsgModal.Attributes.Add("onclick", "ShowLoginAlert('Please login to send message');");
                    aInviteToViewJob.Attributes.Add("onclick", "ShowLoginAlert('Please login to send invite for job');");
                    aFlag.Attributes.Add("onclick", "ShowLoginAlert('Please login to flag this professional');");
                    aFavorite.Attributes.Add("onclick", "ShowLoginAlert('Please login to favorite this professional');");
                }
                if (userid != null)
                {
                  
                    obj.UserId = Convert.ToInt64(userid.Value);
                    obj.Take = 5;
                    System.Collections.Generic.List<UserCityMapping> lst = obj.GetAll();
                    if (lst != null && lst.Count > 0)
                    {
                        Repeater rptCities = (Repeater)e.Item.FindControl("rptCities");
                        rptCities.DataSource = lst;
                        rptCities.DataBind();
                    }
                }
               
            }
        }

        [WebMethod]
        public static void SendJobInvite(long inviteeID, string jobUrls)
        {
            string newJobUrl = string.Empty;
            
            User user = new DataAccess.Classes.User();
            var data = user.GetProfessionalByID(inviteeID);
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.Email))
                {
                    foreach (var jobUrl in jobUrls.Split(','))
                    {
                        newJobUrl +="<br/>"+ Common.WebsiteHostNameForLink + jobUrl + "<br/>";

                    }
                    EmailMessage msg = new EmailMessage();
                    msg.To = data.Email;
                    msg.Subject = Common.SubjectJobInvitation.Replace("##Username##", SessionService.Pull<string>(SessionKeys.UserName));


                    msg.Message =Environment.NewLine+ Common.BodyJobInvitation.Replace("##Username##", SessionService.Pull<string>(SessionKeys.UserName))
                        .Replace("##JOBURL##", newJobUrl);
                    Common.SendEmail(msg);
                }
                else
                {
                    foreach (var jobUrl in jobUrls.Split(','))
                    {
                        newJobUrl += Common.WebsiteHostNameForLink + jobUrl + Environment.NewLine;

                    }
                    Common.SendCodeThroughSms(data.CountryCode + data.PhoneNumber, Common.SMSBodyJobInvitation.Replace("##Username##", SessionService.Pull<string>(SessionKeys.UserName)).Replace("##JOBURL##", newJobUrl));
                }

                #region PushNoitification
                var userID = SessionService.Pull(SessionKeys.UserId);
                var username = SessionService.Pull<string>(SessionKeys.UserName);
                Task.Run(async () =>
                {
                    
                    var jsonString = "{\"UserID\":\""+ userID + "\"}";
                        DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                        {
                            await pro.SendWindowAzurePushNotification(data.Platform, Convert.ToString(data.Id), username+" has invited you to view a job", HelperEnums.PushNotificationType.JobInvitation, jsonString);
                        }
                    
                });
                #endregion
            }
        }

        [WebMethod]
        public static ResultData SetFavoriteProfessional(long professionalID)
        {
            Favorite favorite = new Favorite();
            return favorite.Save(SessionService.Pull(SessionKeys.UserId), professionalID, HelperEnums.FavoriteType.Professional);

        }

        [WebMethod]
        public static ResultData SetFlagProfessional(long professionalID,string reason)
        {
            Flag flag = new Flag();
            return flag.Save(SessionService.Pull(SessionKeys.UserId), professionalID, HelperEnums.FlagType.Professional,reason);

        }

        [WebMethod]
        public static void SendMessage(long msgReceiverID, string msg)
        {
            Messages message = new Messages();
            message.Save(SessionService.Pull(SessionKeys.UserId), msgReceiverID, msg, null, false);
        }
    }
}