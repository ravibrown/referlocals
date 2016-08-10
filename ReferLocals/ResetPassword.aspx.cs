using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.HelperClasses;
using DataAccess.Classes;

namespace ReferLocals
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] == null)
            {
                Response.Redirect("/Login");
            }
        }

        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            try
            {
                User log = new User();
                log.IsDeleted = false;
                log.UniqueId = Request.QueryString["Id"].ToString();
                if (log.GetRecord())
                {
                    if (!string.IsNullOrEmpty(txtNewPassword.Text))
                    {
                        log.Password = Common.Encode(txtNewPassword.Text);
                    }
                    log.Edit(log);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "su", "bootbox.alert('Successfully Updated.');window.location.href='/Index';", true);   
                    //Response.Write("<script>alert('Successfully Updated.');window.location.href='/Index';</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "une", "bootbox.alert('User not exist.');", true);   
                   // Response.Write("<script>alert('User not exist.');</script>");
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "tal", "bootbox.alert('Error! Try Again Later.');", true);   
               // Response.Write("<script>alert('Error! Try Again Later.');</script>");
            }
        }
    }
}