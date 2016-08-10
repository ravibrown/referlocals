using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Classes;
using DataAccess.HelperClasses;

namespace ReferLocals
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                User log = new User();
                log.Id = (Int64)SessionService.Pull("UserId");
                log.IsDeleted = false;
                log.IsApproved = (int)HelperEnums.BooleanValues.Approved;               
                if ( log.GetRecord())
                {
                    string password = txtOldPassword.Text.Trim();
                    if (log.Password.Trim() == Common.Encode(password))
                    {
                        log.Password = Common.Encode(txtNewPassword.Text);
                        log.IsProfileUpdated = true;
                        log.Edit(log);
                        
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "correct", "bootbox.alert('Password changed successfully.');", true);
                        ResetAll();
                    }
                    else
                    {
                        //Response.Write("<script>alert('Old password is incorrect.');</script>");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "old pwd not correct", "bootbox.alert('Current password is incorrect.');", true);
                    }
                }
                else
                {
                   // Response.Write("<script>alert('User Not Exist.');</script>");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "user not exist", "bootbox.alert('User doesn't exist');", true);
                    ResetAll();
                }
            }
            catch (Exception ex)
            {
               // Response.Write("<script>alert('Error! Try Again Later.');</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "try again", "bootbox.alert('Error! Try again later.');", true);
            }
        }

        public void ResetAll()
        {
            txtConfirmPassword.Text = "";
            txtNewPassword.Text = "";
            txtOldPassword.Text = "";
        }
    }
}