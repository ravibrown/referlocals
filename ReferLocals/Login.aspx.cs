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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["email"]))
                {
                    txtRegisterEmail.Text = Request.QueryString["email"];
                }
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

                drpForgetCountryCode.DataSource = lst;
                drpForgetCountryCode.DataTextField = "TeleCode";
                drpForgetCountryCode.DataValueField = "TeleCode";
                drpForgetCountryCode.DataBind();

                drpRegisterCountryCode.DataSource = lst;
                drpRegisterCountryCode.DataTextField = "TeleCode";
                drpRegisterCountryCode.DataValueField = "TeleCode";
                drpRegisterCountryCode.DataBind();

                drpCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();
                drpForgetCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();
                drpRegisterCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();
            }
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            try
            {
                User log = new User();
                if (!string.IsNullOrEmpty(txtPhone.Text))
                {
                    log.CountryCode = Convert.ToInt64(drpCountryCode.SelectedItem.Value);
                    log.PhoneNumber = txtPhone.Text;
                }
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    log.Email = txtEmail.Text;
                }
                log.Password = Common.Encode(txtPassword.Text);
                log.IsDeleted = false;
                log.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                if ((log.PhoneNumber != "" || log.Email != "") && log.Password != "")
                {
                    bool flag = log.GetRecord();
                    if (flag && log.RoleId != (int)HelperEnums.Role.Admin)
                    {
                        if (!log.IsVerified)
                        {
                            log.UpdateUserDevice("Web", null,null, log.Id);
                            Response.Redirect("/verification?Id=" + log.UniqueId);
                        }
                        else
                        {
                            if (log.Type == 0)
                            {
                                log.UpdateUserDevice("Web", null,null, log.Id);
                                Response.Redirect("/SelectUser?Id=" + log.UniqueId);
                            }
                            else
                            {
                                Common.UserId = log.Id;

                                if (!string.IsNullOrEmpty(log.PhoneNumber))
                                {
                                    Common.PhoneNumber = log.CountryCode + log.PhoneNumber;
                                }
                                else
                                {
                                    Common.PhoneNumber = "";
                                }

                                if (!string.IsNullOrEmpty(log.FirstName))
                                {
                                    Common.UserName = log.FirstName;
                                }
                                else
                                {
                                    Common.UserName = "";
                                }

                                if (log.Type != null && log.Type != 0)
                                {
                                    Common.UserType = Convert.ToInt32(log.Type);
                                }
                                else
                                {
                                    Common.UserType = (int)HelperEnums.UserType.User;
                                }

                                if (!string.IsNullOrEmpty(log.Email))
                                {
                                    Common.UserEmail = log.Email;
                                }
                                else
                                {
                                    Common.UserEmail = "";
                                }
                                if (log.RoleId != null)
                                {
                                    Common.RoleId = Convert.ToInt32(log.RoleId);
                                }
                                else
                                {
                                    Common.RoleId = 0;
                                }

                                //if (log.CityId != null && log.CityId != 0)
                                //{
                                //    Common.LocationId = Convert.ToInt64(log.CityId);
                                //    States state = new States();
                                //    state.Id = Convert.ToInt64(log.StateId);
                                //    if (state.GetRecord())
                                //    {
                                //        Common.Location = state.City + " " + state.State ;
                                //    }
                                //}
                                //else
                                //{
                                //    Common.LocationId = Convert.ToInt64(Common.DefaultLocationId);
                                //}

                                if (!string.IsNullOrEmpty(log.Image))
                                {
                                    Common.UserImage = log.Image;
                                }
                                else
                                {
                                    Common.UserImage = null;
                                }

                                bool SetSession = Common.SetSession();

                                if (SetSession)
                                {
                                    if (Request.QueryString["ReturnUrl"] != null)
                                    {
                                        Response.Redirect(Request.QueryString["ReturnUrl"].ToString(), false);
                                    }
                                    else
                                    {
                                        log.UpdateUserDevice("Web", null,null, log.Id);
                                        Response.Redirect("/Index");
                                    }
                                }
                                else
                                {
                                    //Response.Write("<script>alert('Invalid Credentials.');</script>");
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "invalid", "bootbox.alert('Invalid Credentials.');", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Response.Write("<script>alert('Invalid Credentials.');</script>");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "invalid1", "bootbox.alert('Invalid Credentials.');", true);
                    }
                }
                else
                {
                    //Response.Write("<script>alert('Please enter email or phone number and password.');</script>");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "emailphn", "bootbox.alert('Please enter email or phone number and password.');", true);
                }
                ResetAll();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnForget_OnClick(object sender, EventArgs e)
        {
            try
            {
                User log = new User();
                if (!string.IsNullOrEmpty(txtForgetPhone.Text))
                {
                    log.CountryCode = Convert.ToInt64(drpForgetCountryCode.SelectedItem.Value);
                    log.PhoneNumber = txtForgetPhone.Text;
                }
                if (!string.IsNullOrEmpty(txtForgetEmail.Text))
                {
                    log.Email = txtForgetEmail.Text;
                }
                log.IsDeleted = false;
                log.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                if (log.GetRecord() && log.RoleId != (int)HelperEnums.Role.Admin)
                {
                    log.VerificationCode = Common.NewVerificationCode().Substring(0, 6);
                    log.Edit(log);
                    if (!string.IsNullOrEmpty(txtForgetPhone.Text) && !string.IsNullOrEmpty(txtForgetEmail.Text))
                    {
                        Common.SendCodeThroughSms(log.CountryCode + log.PhoneNumber, log.VerificationCode);

                        EmailMessage message = new EmailMessage();
                        message.Message = "Hi ," + log.FirstName + "<br/>" + Common.ForgetPasswordBodyMessage.Replace("##OTP##", log.VerificationCode);
                        message.Subject = Common.OTPSubject;
                        message.To = log.Email;
                        bool ret = Common.SendEmail(message);
                        if (ret)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "emailphnotp", "bootbox.alert('Please check your email or phone and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "&Type=Forget';", true);
                            //Response.Write("<script>alert('Please check your email or phone and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "&Type=Forget';</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "mailnotsent", "bootbox.alert('Mail not sent.Please try again later..')", true);
                            //Response.Write("<script>alert('Mail not sent.Please try again later..');</script>");
                        }
                    }
                    else if (!string.IsNullOrEmpty(txtForgetPhone.Text))
                    {
                        Common.SendCodeThroughSms(log.CountryCode + log.PhoneNumber, log.VerificationCode);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "emailphnotp1", "bootbox.alert('Please check your phone and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "&Type=Forget';", true);
                        //Response.Write("<script>alert('Please check your phone and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "&Type=Forget';</script>");
                    }
                    else if (!string.IsNullOrEmpty(txtForgetEmail.Text))
                    {
                        EmailMessage message = new EmailMessage();
                        message.Message = "Hi ," + log.FirstName + "<br/>" + Common.ForgetPasswordBodyMessage.Replace("##OTP##", log.VerificationCode);
                        message.Subject = Common.OTPSubject;
                        message.To = log.Email;
                        bool ret = Common.SendEmail(message);
                        if (ret)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "emailphnotp2", "bootbox.alert('Please check your email and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "&Type=Forget';", true);
                            //Response.Write("<script>alert('Please check your email and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "&Type=Forget';</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "emailphnotp3", "bootbox.alert('Mail not sent.Please try again later..');", true);
                            //Response.Write("<script>alert('Mail not sent.Please try again later..');</script>");
                        }
                    }

                }
                else
                {
                    // Response.Write("<script>alert('User not exist.');</script>");
                }
                ResetAll();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "er", "bootbox.alert('Error! Try Again Later.');", true);
                //Response.Write("<script>alert('Error! Try Again Later.');</script>");
            }
        }

        protected void btnRegister_OnClick(object sender, EventArgs e)
        {
            try
            {
                User log = new User();
                if (!string.IsNullOrEmpty(txtRegisterPhone.Text))
                {
                    log.CountryCode = Convert.ToInt64(drpRegisterCountryCode.SelectedItem.Value);
                    log.PhoneNumber = txtRegisterPhone.Text;
                }
                if (!string.IsNullOrEmpty(txtRegisterEmail.Text))
                {
                    log.Email = txtRegisterEmail.Text;
                }
                log.IsDeleted = false;
                log.RoleId = (int)HelperEnums.Role.User;
                if (!log.GetRecord() && log.RoleId != (int)HelperEnums.Role.Admin)
                {
                    log.Password = Common.Encode(txtRegisterPassword.Text);
                    log.VerificationCode = Common.NewVerificationCode().Substring(0, 6);
                    log.UniqueId = Common.CreateUniqueId().Substring(0, 6);
                    log.IsApprovedByAdmin = true;
                    log.Add(log);
                    if (!string.IsNullOrEmpty(txtRegisterPhone.Text) && !string.IsNullOrEmpty(txtRegisterEmail.Text))
                    {
                        Common.SendCodeThroughSms(log.CountryCode + log.PhoneNumber, log.VerificationCode);

                        EmailMessage message = new EmailMessage();
                        message.Message = "Hi ," + log.FirstName + "<br/>" + Common.VerifyBodyMessage.Replace("##OTP##", log.VerificationCode);
                        message.Subject = Common.OTPSubject;
                        message.To = log.Email;
                        bool ret = Common.SendEmail(message);
                        if (ret)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "otp1", "bootbox.alert('Please check your email or phone and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "';", true);
                            //Response.Write("<script>alert('Please check your email or phone and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "';</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "tg", "bootbox.alert('Mail not sent.Please try again later..');", true);
                            // Response.Write("<script>alert('Mail not sent.Please try again later..');</script>");
                        }
                    }
                    else if (!string.IsNullOrEmpty(txtRegisterPhone.Text))
                    {
                        Common.SendCodeThroughSms(log.CountryCode + log.PhoneNumber, log.VerificationCode);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "otp2", "bootbox.alert('Please check your phone and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "';", true);
                        //Response.Write("<script>alert('Please check your phone and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "';</script>");
                    }
                    else if (!string.IsNullOrEmpty(txtRegisterEmail.Text))
                    {
                        EmailMessage message = new EmailMessage();
                        message.Message = "Hi ," + log.FirstName + "<br/>" + Common.VerifyBodyMessage.Replace("##OTP##", log.VerificationCode);
                        message.Subject = Common.OTPSubject;
                        message.To = log.Email;
                        bool ret = Common.SendEmail(message);
                        if (ret)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "otp3", "bootbox.alert('Please check your email and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "';", true);
                            //Response.Write("<script>alert('Please check your email and put otp here.');window.location.href='/Verification?Id=" + log.UniqueId + "';</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "tg1", "bootbox.alert('Mail not sent.Please try again later..');", true);
                            //Response.Write("<script>alert('Mail not sent.Please try again later..');</script>");
                        }
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "uae", "bootbox.alert('User already exists.');", true);
                    //   Response.Write("<script>alert('User already exists.');</script>");
                }
                ResetAll();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "tg2", "bootbox.alert('Error! Try Again Later.');", true);
                // Response.Write("<script>alert('Error! Try Again Later.');</script>");
            }
        }

        public void ResetAll()
        {
            txtForgetEmail.Text = "";
            txtForgetPhone.Text = "";
            //txtEmail.Text = "";
            //txtPhone.Text = "";
            txtPassword.Text = "";
            txtRegisterPassword.Text = "";
            txtRegisterEmail.Text = "";
            txtRegisterPhone.Text = "";
            //drpCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();
            drpForgetCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();
            drpRegisterCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();
        }
    }
}