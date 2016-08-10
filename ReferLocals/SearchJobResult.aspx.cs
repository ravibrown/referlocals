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
    public partial class SearchJobResult : System.Web.UI.Page
    {
        public static string subCategoryName = string.Empty;
        protected void Page_InIt(object sender, EventArgs e)
        {
            //if ((Int64)SessionService.Pull("UserType") == (int)HelperEnums.UserType.User)
            //{
            //    Response.Write("<script>alert('Job search not allowed for user.');window.location.href='/index';</script>");
            //}

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
            if (Request.QueryString["type"] != null)
            {
                if (SessionService.HasKey("LocationId"))
                {
                    obj.CityId = (Int64)SessionService.Pull("LocationId");
                }
                else
                {
                    obj.CityId = Convert.ToInt64(Common.DefaultLocationId);
                }
                SubCategory sub = new SubCategory();
                sub.Name = Request.QueryString["type"].ToString().Replace("-", " ");
                subCategoryName = sub.Name;
                if (sub.GetRecord())
                {
                    obj.SubCategoryId = sub.Id;
                    obj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                    List<Jobs> lst = obj.GetAllWithJoin();
                    if(lst!=null&&lst.Count>0)
                    {
                        rptResults.DataSource = lst;
                        rptResults.DataBind();
                    }
                }
                
            }
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


                    if (aFlag!= null)
                    {
                        aFlag.Attributes.Add("onclick", "OpenFlagJobModal(" + job.Id + ")");
                        aFlag.Attributes.Add("class", "aFlag" + job.Id);

                    }
                    if (iFlag!= null)
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
                    //   aShareThisJob.HRef = "#divShareThisJobModal"+jobID+"";
                    //  aShareThisJob.Attributes.Remove("onclick");
                    aShareThisJob.Attributes.Add("onclick", "ShowShareModal("+job.Id+",'"+job.UrlFriendlyTitle+"');");
                }
                else
                {
                    aFlag.Attributes.Add("onclick", "ShowLoginAlert('Please login to flag this job');");
                    aFavorite.Attributes.Add("onclick", "ShowLoginAlert('Please login to favorite this job');");
                    //aShareThisJob.HRef = "javascript:void(0);";
                    aShareThisJob.Attributes.Add("onclick", "ShowLoginAlert('Please login to share this job');");
                }
            }
        }
        [WebMethod]
        public static ResultData SetFavoriteJob(long jobID)
        {
            Favorite favorite = new Favorite();
            return favorite.Save(SessionService.Pull(SessionKeys.UserId), jobID, HelperEnums.FavoriteType.Job);

        }
        //[WebMethod]
        //public static ResultData SetFlagJob(long jobID)
        //{
        //    Flag flag= new Flag();
        //    return flag.Save(SessionService.Pull(SessionKeys.UserId), jobID, HelperEnums.FlagType.Job);

        //}
        [WebMethod]
        public static ResultData SetFlagJob(long jobID,string reason)
        {
            Flag flag = new Flag();
            return flag.Save(SessionService.Pull(SessionKeys.UserId), jobID, HelperEnums.FlagType.Job, reason);

        }


    }
}