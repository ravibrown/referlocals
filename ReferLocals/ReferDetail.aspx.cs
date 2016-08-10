using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class ReferDetail : System.Web.UI.Page
    {
        public static string UniqueId = "";
        public static Int64 UserId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDropDownList();
            BindJobsByUser();
            if (!IsPostBack)
            {
                if (Page.RouteData.Values.Count > 0)
                {
                    string subcategoryName = Page.RouteData.Values["subcategory"] as string;
                    string profileUrl = Page.RouteData.Values["profileurl"] as string;
                    SubCategory subcat = new SubCategory();

                    var subcatID = subcat.GetSubCategoryIDByName(subcategoryName);
                    if (subcatID > 0)
                    {
                        User user = new User();
                        var userData = user.GetUserDetails(subcatID, profileUrl);
                        if (userData != null)
                        {
                            BindData(userData.UniqueId);

                        }
                        else
                        {
                            divUserData.Visible = false;
                            divDataNotFound.Visible = true;

                        }

                    }
                    else
                    {
                        divUserData.Visible = false;
                        divDataNotFound.Visible = true;
                    }
                }
                else
                if (Request.QueryString["Id"] != null)
                {
                    BindData(Request.QueryString["Id"]);
                }
            }
        }

        public void BindData(string id)
        {
            User obj = new User();
            obj.UniqueId = id;
            List<User> lst = obj.GetAll();
            if (lst != null && lst.Count > 0)
            {
                if (lst[0].IsApprovedByAdmin == false && lst[0].IsVerified == false)
                {
                    Response.Redirect("/index");
                }
                else
                {
                    divUserData.Visible = true;
                    divDataNotFound.Visible = false;

                    UserId = lst[0].Id;
                    lblFirstName.Text = lst[0].FirstName;
                    lblLastName.Text = lst[0].LastName;
                    SetFavorite(lst[0]);
                    SetFlag(lst[0]);
                    ltJobsDone.Text = Convert.ToString(lst[0].JobsDone);
                    UserControl.BreadCrumbs wc = (UserControl.BreadCrumbs)this.Master.FindControl("BreadCrumbs");
                    if (wc != null)
                    {
                        wc.BreadCrumbName = lblFirstName.Text + " " + lblLastName.Text + "'s Profile";
                    }
                    rptDetail.DataSource = lst;
                    rptDetail.DataBind();

                    UserCityMapping city = new UserCityMapping();
                    city.UserId = lst[0].Id;
                    UniqueId = lst[0].UniqueId;
                    List<UserCityMapping> lst_city = city.GetAll();
                    if (lst_city != null && lst_city.Count > 0)
                    {
                        rptCities.DataSource = lst_city;
                        rptCities.DataBind();
                    }
                }
            }
            else
            {
                divUserData.Visible = false;
                divDataNotFound.Visible = true;

            }
        }
        public void BindDropDownList()
        {
            CountryCode code = new CountryCode();
            List<CountryCode> lst = code.GetAll();
            if (lst != null && lst.Count > 0)
            {
                drpCountryCode.DataSource = lst;
                drpCountryCode.DataTextField = "TeleCode";
                drpCountryCode.DataValueField = "TeleCode";
                drpCountryCode.DataBind();

                drpCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();

            }
        }
        protected string BindAddress(Int64 Id)
        {
            string Result = "";
            States state = new States();
            state.Id = Id;
            if (state.GetRecord())
            {
                Result = state.State + " " + state.City ;
            }
            return Result;
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


        #region "WebMethod"
        [WebMethod]
        public static IList GetReferList(int index, int take, int IsReferd)
        {
            List<PropReferal> list_category = new List<PropReferal>();


            Referal obj = new Referal();

            obj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            obj.UserId = UserId;
            if (IsReferd == 0)
            {
                obj.CheckIsRefered = (int)HelperEnums.BooleanValues.Disapproved;
            }
            else
            {
                obj.CheckIsRefered = (int)HelperEnums.BooleanValues.Approved;
            }

            list_category = obj.GetAllReferals(UserId, (HelperEnums.BooleanValues)IsReferd, take, index); //obj.GetAllForAjax();
            if (list_category != null && list_category.Count > 0)
            {
                for (int i = 0; i < list_category.Count; i++)
                {
                    States city = new States();
                    city.Id = Convert.ToInt64(list_category[i].UserCityId);
                    if (city.GetRecord())
                    {
                        User usr = new User();
                        usr.Id = Convert.ToInt64(list_category[i].UserId);
                        if (usr.GetRecord())
                        {
                            list_category[i].UserCityName = city.City;
                            list_category[i].UserZipName = city.State;
                            list_category[i].UserName = usr.FirstName + " " + usr.LastName;
                        }
                    }
                }
            }


            return list_category;
        }
        [WebMethod]
        public static void SendEmailProfileLink(string email, string url)
        {
            EmailMessage message = new EmailMessage();
            message.Message = Common.BodyProfileLink.Replace("##Username##", Convert.ToString(HttpContext.Current.Session[SessionKeys.UserName])).Replace("##JOBURL##", "<a href=" + url + ">" + url + "</a>");
            message.Subject = Common.SubjectProfileLink.Replace("##Username##", Convert.ToString(HttpContext.Current.Session[SessionKeys.UserName]));
            message.To = email;
            bool ret = Common.SendEmailWithGenericTemplate(email,message.Subject,message.Message);

        }
        [WebMethod]
        public static void SendTextProfileLink(string countryCode, string phoneNumber, string url)
        {
            Common.SendCodeThroughSms(countryCode + phoneNumber, "Hey, " + Convert.ToString(HttpContext.Current.Session[SessionKeys.UserName]) + " shared this professional profile with you posted at referlocals " + Environment.NewLine + url + Environment.NewLine + "Thanks" + Environment.NewLine + "ReferLocals Team");
        }

        #endregion

        private void SetFavorite(User data)
        {

            if (data != null)
            {

                if (aFavorite != null)
                {
                    if (SessionService.Pull(SessionKeys.UserId) > 0)
                    {
                        if (SessionService.Pull(SessionKeys.UserId) == data.Id)
                        {
                            aFavorite.Visible = false;
                        }
                        else {
                            aFavorite.Visible = true;
                            aFavorite.Attributes.Add("onclick", "SetFavoriteProfessional(" + data.Id + ")");
                            aFavorite.Attributes.Add("class", "aFavorite" + data.Id);


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
                        }
                    }
                    else
                    {
                        aFavorite.Attributes.Add("onclick", "ShowLoginAlert('Please login to favorite this user');");
                    }
                }
            }
        }
        private void SetFlag(User data)
        {

            if (data != null)
            {


                if (SessionService.Pull(SessionKeys.UserId) > 0)
                {
                    if (SessionService.Pull(SessionKeys.UserId) == data.Id)
                    {
                        aFlag.Visible = false;
                    }
                    else
                    {
                        aFlag.Visible = true;
                        aFlag.Attributes.Add("onclick", "OpenFlagProfessionalModal(" + data.Id + ")");

                        aFlag.Attributes.Add("class", "aFlag" + data.Id);
                        
                        iFlag.Attributes.Remove("class");
                        if (data.IsFlag)
                        {

                            iFlag.Attributes.Add("class", "fa fa-flag");
                            iFlag.Visible = false;
                        }
                        else
                        {
                            iFlag.Attributes.Add("class", "fa fa-flag-o");
                        }
                    }
                }
                else
                {
                    aFlag.Attributes.Add("onclick", "ShowLoginAlert('Please login to flag this user');");
                }

            }
        }
        protected void rptDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor aShareThisJob = (HtmlAnchor)e.Item.FindControl("aShareThisProfile");
                HtmlAnchor aInviteToViewJob = (HtmlAnchor)e.Item.FindControl("aInviteToViewJob");
                HtmlAnchor aSendMsgModal = (HtmlAnchor)e.Item.FindControl("aSendMsgModal");

                var data = (User)e.Item.DataItem;
                if (SessionService.Pull(SessionKeys.UserId) > 0)
                {
                    aSendMsgModal.Attributes.Add("onclick", " ShowSendMsgModal(" + data.Id + ")");
                    aInviteToViewJob.Attributes.Add("onclick", " ShowInviteJobModal(" + data.Id + ")");
                    aShareThisJob.HRef = "#divShareThisProfileModal";
                    aShareThisJob.Attributes.Remove("onclick");
                }
                else
                {
                    aSendMsgModal.Attributes.Add("onclick", "ShowLoginAlert('Please login to send message');");
                    aInviteToViewJob.Attributes.Add("onclick", "ShowLoginAlert('Please login to send invite for job');");
                    aShareThisJob.HRef = "javascript:void(0);";
                    aShareThisJob.Attributes.Add("onclick", "ShowLoginAlert('Please login to share this profile');");
                }
            }
        }
    }
}