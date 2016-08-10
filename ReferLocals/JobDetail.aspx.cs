using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class JobDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindData();
                BindDropDownList();

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
        public void BindData()
        {
            Jobs obj = new Jobs();
            string Id = Page.RouteData.Values["Id"] as string;
            if (Id != "")
            {
                obj.Id = Convert.ToInt64(Id);
                obj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                var loggedInUserID = SessionService.Pull(SessionKeys.UserId);
                List<Jobs> lst = obj.GetAll(loggedInUserID);
                if (lst != null && lst.Count > 0)
                {
                    UserControl.BreadCrumbs uc = (UserControl.BreadCrumbs)(this.Master.FindControl("BreadCrumbs"));
                    if (uc != null)
                    {
                        uc.BreadCrumbName = lst[0].Title;
                    }


                    BindAppointmentData(Convert.ToInt64(Id));
                    rptResults.DataSource = lst;
                    rptResults.DataBind();
                }
                if ((int)SessionService.Pull(SessionKeys.UserType) == (int)HelperEnums.UserType.Professional)
                {
                    BindEstimate();
                }
            }
        }
        public void BindAppointmentData(long jobID)
        {
            Appointment appointment = new Appointment();
            var userType = (HelperEnums.UserType)SessionService.Pull(SessionKeys.UserType);
            var appointmentData = appointment.GetAppointmentsByJobID(jobID, SessionService.Pull(SessionKeys.UserId), userType);
            if (appointmentData != null)
            {
                divAppointment.Visible = true;
                lbAppointmentOn.Text = appointmentData.AppointmentDate + " " + appointmentData.AppointmentTime;
                if (userType == HelperEnums.UserType.User)
                {
                    User obj = new DataAccess.Classes.User();
                    var data = obj.GetProfessionalByID(appointmentData.ProfessionalID.GetValueOrDefault());
                    if (data != null)
                    {
                        if (!string.IsNullOrEmpty(data.ProfileUrl))
                        {
                            aUserUrl.HRef = "/" + data.ProfessionalUrls[0].SubCategoryName + "/" + data.ProfileUrl;
                        }
                        else
                        { aUserUrl.HRef = "/ReferDetail?Id=" + data.UniqueId; }
                        // aUserUrl.HRef = "/user/profile/" + data.UniqueId;
                        lbName.Text = data.FirstName + " " + data.LastName;
                        lbEmail.Text = data.Email;
                        lbPhone.Text = data.CountryCode + " " + data.PhoneNumber;
                    }
                }
                if (userType == HelperEnums.UserType.Professional)
                {
                    User obj = new DataAccess.Classes.User();
                    var data = obj.GetUserByID(appointmentData.UserID.GetValueOrDefault());
                    if (data != null)
                    {
                        aUserUrl.HRef = "/user/profile/" + data.UniqueId;
                        lbName.Text = data.FirstName + " " + data.LastName;
                        lbEmail.Text = data.Email;
                        lbPhone.Text = data.CountryCode + " " + data.PhoneNumber;
                    }
                }

            }
            else
            {
                divAppointment.Visible = false;
            }
        }
        public void BindEstimate()
        {
            if (divAppointment.Visible)
            {
                divEstimateForm.Visible = false;
            }
            else
            {
                divEstimateForm.Visible = true;
            }
            Quote quote = new Quote();
            var data = quote.GetQuoteByJobAndProfessionalID(SessionService.Pull(SessionKeys.UserId), Convert.ToInt64(Page.RouteData.Values["Id"]));
            if (data != null)
            {
                txtComments.Value = data.Comments.Replace("<br/>", "\r\n");
                txtEstimate.Text = Convert.ToString(data.Estimate);
                hdnQuoteID.Value = Convert.ToString(data.ID);
            }
        }
        protected void btnSendEstimate_Click(object sender, EventArgs e)
        {
            Quote quote = new Quote();

            QuoteDataContract quoteData = new QuoteDataContract();
            if (!string.IsNullOrEmpty(hdnQuoteID.Value))
            {
                quoteData.ID = Convert.ToInt64(hdnQuoteID.Value);
            }
            quoteData.JobID = Convert.ToInt64(Page.RouteData.Values["Id"]);
            quoteData.ProfessionalID = SessionService.Pull(SessionKeys.UserId);
            quoteData.Estimate = Convert.ToDecimal(txtEstimate.Text);
            quoteData.Comments = txtComments.Value;
            quote.Save(quoteData);
            divMain.Visible = false;
            divQuoteThanks.Visible = true;
            Response.Write("<script>setTimeout(function(){ window.location.href='/referprofessional/searchjob' }, 5000);</script>");
        }
        [WebMethod]
        public static void SendEmailJobLink(string email, string url)
        {
            //EmailMessage message = new EmailMessage();
            //message.Message = Common.BodyJobLink.Replace("##Username##", Convert.ToString(HttpContext.Current.Session[SessionKeys.UserName])).Replace("##JOBURL##", "<a href=" + url + ">" + url + "</a><br/><br/>Thanks<br/>ReferLocals Team");
            //message.Subject = Common.SubjectJobLink.Replace("##Username##", Convert.ToString(HttpContext.Current.Session[SessionKeys.UserName]));
            //message.To = email;
            //bool ret = Common.SendEmail(message);
            var emailBody = Common.EmailBodyOnJobInvitation
                .Replace("##Username##", Convert.ToString(HttpContext.Current.Session[SessionKeys.UserName]))
                .Replace("##Joblink##", url)
                .Replace("##Login##", "<a href='" + Common.WebsiteHostNameForLink + "/login" + "'>Login</a>")
                ;
            Common.SendEmailWithGenericTemplate(email, Common.EmailSubjectOnJobInvitation, emailBody);

        }
        [WebMethod]
        public static void SendTextJobLink(string countryCode, string phoneNumber, string url)
        {
            Common.SendCodeThroughSms(countryCode + phoneNumber, "Hey, " + Convert.ToString(HttpContext.Current.Session[SessionKeys.UserName]) + " shared this job with you posted at referlocals " + Environment.NewLine + url + Environment.NewLine + Environment.NewLine + "Thanks" + Environment.NewLine + "ReferLocals Team");
        }

        protected void rptResults_ItemDataBound(object sender, RepeaterItemEventArgs e)

        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor aShareThisJob = (HtmlAnchor)e.Item.FindControl("aShareThisJob");
                var job = ((Jobs)(e.Item.DataItem));
                HtmlAnchor aFavorite = (HtmlAnchor)e.Item.FindControl("aFavorite");
                HtmlGenericControl iFavorite = (HtmlGenericControl)e.Item.FindControl("iFavorite");
                HtmlAnchor aFlag = (HtmlAnchor)e.Item.FindControl("aFlag");
                HtmlGenericControl iFlag = (HtmlGenericControl)e.Item.FindControl("iFlag");

                if (SessionService.Pull(SessionKeys.UserId) > 0)
                {
                    if (aFavorite != null)
                    {
                        aFavorite.Attributes.Add("onclick", "SetFavoriteJob(" + job.Id + ")");
                        aFavorite.Attributes.Add("class", "aFavorite" + job.Id);

                    }
                    if (iFavorite != null)
                    {
                        iFavorite.Attributes.Remove("class");
                        if (job.IsFavorite)
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
                        aFlag.Attributes.Add("onclick", "OpenFlagJobModal(" + job.Id + ")");
                        aFlag.Attributes.Add("class", "aFlag" + job.Id);

                    }
                    if (iFlag != null)
                    {
                        iFlag.Attributes.Remove("class");
                        if (job.IsFlag)
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
                    aShareThisJob.HRef = "#divShareThisJobModal";
                    aShareThisJob.Attributes.Remove("onclick");
                    // aShareThisJob.Attributes.Add("onclick", "ShowShareModal(" + job.Id + ",'" + job.UrlFriendlyTitle + "');");
                }
                else
                {
                    aShareThisJob.HRef = "javascript:void(0);";
                    aFlag.Attributes.Add("onclick", "ShowLoginAlert('Please login to flag this job');");
                    aFavorite.Attributes.Add("onclick", "ShowLoginAlert('Please login to favorite this job');");
                    //aShareThisJob.HRef = "javascript:void(0);";
                    aShareThisJob.Attributes.Add("onclick", "ShowLoginAlert('Please login to share this job');");
                }
            }
        }
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HtmlAnchor aShareThisJob = (HtmlAnchor)e.Item.FindControl("aShareThisJob");
        //        if (SessionService.Pull(SessionKeys.UserId) > 0)
        //        {
        //            aShareThisJob.HRef = "#divShareThisJobModal";
        //            aShareThisJob.Attributes.Remove("onclick");
        //        }
        //        else
        //        {
        //            aShareThisJob.HRef = "javascript:void(0);";
        //            aShareThisJob.Attributes.Add("onclick", "ShowLoginAlert('Please login to share this job');");
        //        }
        //    }
        //}
    }
}