using DataAccess.Classes;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using ReferLocals.UserControl;
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
    public partial class UserPublicProfile : System.Web.UI.Page
    {
        public static string UniqueId = "";
        public static Int64 UserId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.RouteData.Values.Count > 0)
                {
                    BindData(Convert.ToString(Page.RouteData.Values["ID"]));

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
                    BreadCrumbs bc = (BreadCrumbs)(this.Master.FindControl("BreadCrumbs"));
                    if (bc != null)
                    {
                        bc.BreadCrumbName = lblFirstName.Text + " " + lblLastName.Text + "'s Profile";
                    }
                    rptDetail.DataSource = lst;
                    rptDetail.DataBind();
                    SetFavorite(lst[0]);
                    SetFlag(lst[0]);
                    ltJobsDone.Text =Convert.ToString( lst[0].JobsDone);
                }
            }
            else
            {
                divUserData.Visible = false;
                divDataNotFound.Visible = true;

            }
        }

        protected string BindAddress(Int64 Id)
        {
            string Result = "";
            States state = new States();
            state.Id = Id;
            if (state.GetRecord())
            {
                Result = state.City + " " + state.State  ;
            }
            return Result;
        }



        #region "WebMethod"
        [WebMethod]
        public static ResultData SetFlagUser(long userID,string reason)
        {
            Flag flag = new Flag();
            return flag.Save(SessionService.Pull(SessionKeys.UserId), userID, HelperEnums.FlagType.User, reason);

        }

        [WebMethod]
        public static ResultData SetFavoriteUser(long userID)
        {
            Favorite favorite = new Favorite();
            return favorite.Save(SessionService.Pull(SessionKeys.UserId), userID, HelperEnums.FavoriteType.User);

        }
        [WebMethod]
        public static ReferalListWrapper GetReferList(int index, int take, int IsReferd)
        {
            Referal obj = new Referal();
            var referals = obj.GetAllReferalsBySenderID(UserId, index, take, (HelperEnums.BooleanValues)IsReferd); //obj.GetAllForAjax();
            return referals;
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
                        else
                        {
                            aFavorite.Visible = true;
                            aFavorite.Attributes.Add("onclick", "SetFavoriteUser(" + data.Id + ")");
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
                        aFlag.Attributes.Add("onclick", "SetFlagUser(" + data.Id + ")");
                        aFlag.Attributes.Add("class", "aFlag" + data.Id);



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
                    aFlag.Attributes.Add("onclick", "ShowLoginAlert('Please login to flag this user');");
                }

            }
        }

        protected void rptDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor aSendMsgModal = (HtmlAnchor)e.Item.FindControl("aSendMsgModal");

                var data = (User)e.Item.DataItem;
                if (SessionService.Pull(SessionKeys.UserId) > 0)
                {
                    aSendMsgModal.Attributes.Add("onclick", " ShowSendMsgModal(" + data.Id + ")");

                }
                else
                {
                    aSendMsgModal.Attributes.Add("onclick", "ShowLoginAlert('Please login to send message');");

                }
            }
        }
    }
}