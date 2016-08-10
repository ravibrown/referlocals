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
    public partial class Verification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    BindData();
                }
            }
        }

        public void BindData()
        {
            User log = new User();
            log.UniqueId = Request.QueryString["Id"].ToString();
            if (log.GetRecord())
            {
                if (!string.IsNullOrEmpty(log.Email))
                {
                    txtEmailOrPhone.Text = log.Email;
                }
                else if (!string.IsNullOrEmpty(log.PhoneNumber))
                {
                    txtEmailOrPhone.Text = log.PhoneNumber;
                }
            }
        }

        protected void btnVerify_OnClick(object sender, EventArgs e)
        {
            try
            {
                User log = new User();
                if (!string.IsNullOrEmpty(txtEmailOrPhone.Text))
                {
                    log.PhoneNumber = txtEmailOrPhone.Text;
                }
                log.IsDeleted = false;
                bool flag = log.GetRecord();
                if (!string.IsNullOrEmpty(txtEmailOrPhone.Text) && !flag)
                {
                    log.PhoneNumber = "";
                    log.Email = txtEmailOrPhone.Text;
                    flag = log.GetRecord();
                }
                if (flag)
                {
                    if (log.VerificationCode == txtOTP.Text)
                    {
                        if (Request.QueryString["Type"] != null)
                        {
                            Response.Redirect("/ResetPassword?Id=" + log.UniqueId);
                        }
                        else
                        {
                            log.IsVerified = true;
                            log.Edit(log);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "pae", "bootbox.alert('Phone Number Already Exists.');", true);   
                            Response.Write("<script>window.location.href='/SelectUser?Id="+log.UniqueId+"';</script>");
                        }
                       
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ic", "bootbox.alert('Incorrect Code.');", true);   
                        //Response.Write("<script>alert('Incorrect Code.');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ude", "bootbox.alert('User doesn't exist.');", true);   
                   // Response.Write("<script>alert('User not exist.');</script>");
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ETAE", "bootbox.alert('Error! Try Again Later.');", true);   
                //Response.Write("<script>alert('Error! Try Again Later.');</script>");
            }
        }

        protected void btnResend_OnClick(object sender, EventArgs e)
        {
            try
            {
                User log = new User();
                bool IsPhone = false;
                if (!string.IsNullOrEmpty(txtEmailOrPhone.Text))
                {
                    log.PhoneNumber = txtEmailOrPhone.Text;
                }
                log.IsDeleted = false;
                bool flag = log.GetRecord();

                if (!flag && !string.IsNullOrEmpty(txtEmailOrPhone.Text))
                {
                    log.PhoneNumber = "";
                    log.Email = txtEmailOrPhone.Text;
                    flag = log.GetRecord();
                }
                else
                {
                    IsPhone = true;
                }

                if (flag && log.RoleId != (int)HelperEnums.Role.Admin)
                {
                    log.VerificationCode = Common.NewVerificationCode().Substring(0, 6);
                    log.Edit(log);
                    if (!string.IsNullOrEmpty(txtEmailOrPhone.Text) && IsPhone)
                    {
                        Common.SendCodeThroughSms(log.CountryCode+log.PhoneNumber, log.VerificationCode);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "cpotp", "bootbox.alert('Please check your phone and put otp here.');", true);   
                        //Response.Write("<script>alert('Please check your phone and put otp here.');</script>");
                    }
                    else if (!string.IsNullOrEmpty(txtEmailOrPhone.Text))
                    {
                        EmailMessage message = new EmailMessage();
                        message.Message = "Hi ," + log.FirstName + "<br/>" + Common.ForgetPasswordBodyMessage.Replace("##OTP##", log.VerificationCode);
                        message.Subject = Common.OTPSubject;
                        message.To = log.Email;
                        bool ret = Common.SendEmail(message);
                        if (ret)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "ceotp", "bootbox.alert('Please check your email and put otp here.');", true);   
                           // Response.Write("<script>alert('Please check your email and put otp here.');</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "mneta", "bootbox.alert('Mail not sent.Please try again later..');", true);   
                          //  Response.Write("<script>alert('Mail not sent.Please try again later..');</script>");
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "une1", "bootbox.alert('User doesn't exist');", true);   
                    //Response.Write("<script>alert('User not exist.');</script>");
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "etal", "bootbox.alert('Error! Try Again Later.');", true);   
               // Response.Write("<script>alert('Error! Try Again Later.');</script>");
            }
        }
    }
}