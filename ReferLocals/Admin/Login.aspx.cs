using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string a=ClsCommon.Encode("admin@123");
            //string b = a;
            if (!IsPostBack)
            {
                if (Request.Cookies["YourAppLogin"] != null)
                {
                    string username = Request.Cookies["YourAppLogin"].Values["username"];
                    string password = Request.Cookies["YourAppLogin"].Values["password"];
                    txtUserName.Text = username;
                    txtPassword.Attributes.Add("value", password);
                }
            }
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            if (Common.AdminUserName == txtUserName.Text.Trim() && Common.AdminPassword == txtPassword.Text.Trim())
            {
                    Common.UserId = -1;
                    Common.UserName = Common.AdminUserName;
                    Common.RoleId = (int)HelperEnums.Role.Admin;
                    Common.UserImage = "";
                    bool SetSession = Common.SetSession();

                    if (SetSession)
                    {

                        if (chkRember.Checked)
                        {
                            HttpCookie cookie = new HttpCookie("YourAppLogin");
                            cookie.Values.Add("username", txtUserName.Text);
                            cookie.Values.Add("password", txtPassword.Text);
                            cookie.Expires = DateTime.Now.AddYears(1);
                            Response.Cookies.Add(cookie);
                        }

                        if (Request.QueryString["ReturnUrl"] != null)
                        {
                            Response.Redirect(Request.QueryString["ReturnUrl"].ToString());
                        }
                        else
                        {
                            Response.Redirect("/Admin/AddHomeCard");
                        }
                    }

                }
                else
                {
                    lblErrorMsg.Text = "Invalid Credentials!";
                    alertDanger.Style.Add("display", "block !important");
                    ResetAll();
                } 
        }

        protected void btnForget_OnClick(object sender, EventArgs e)
        {
           
        }

        public void ResetAll()
        {
            txtPassword.Attributes.Add("value", "");
            txtUserName.Text = "";
            txtEmail.Text = "";
            chkRember.Checked = false;
        }

        #region "Web Methods"

        [WebMethod]
        public static string Logout()
        {
            string result = "";
            HttpContext.Current.Session.Clear();
            result = "/Admin/Login";
            return result;
        }
        #endregion
    }
}