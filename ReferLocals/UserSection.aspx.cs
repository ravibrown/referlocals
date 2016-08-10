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
    public partial class UserSection : System.Web.UI.Page
    {
        public static bool IsEmail = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString["Id"]!=null)
                {
                    BindDropDownList();
                    BindData();
                }
                else
                {
                    Response.Redirect("/Login");
                }
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
            }

            //States state = new States();
            //List<States> lst_State = state.GetAllByState();
            //drpState.Items.Clear();
            //drpState.Items.Add(new ListItem("Select", "0"));
            //if (lst_State != null && lst_State.Count > 0)
            //{
            //    foreach (var item in lst_State)
            //    {
            //        drpState.Items.Add(new ListItem(item.State, item.Id.ToString()));
            //    }
            //}
        }

        public void BindData()
        {
            User log = new User();
            log.UniqueId = Request.QueryString["Id"].ToString();
            log.IsDeleted = false;
            if(log.GetRecord())
            {
                if (!log.IsProfileUpdated)
                {
                    if (!string.IsNullOrEmpty(log.Email))
                    {
                        txtEmail.Text = log.Email;
                        txtEmail.ReadOnly = true;
                        txtPhoneNumber.ReadOnly = false;
                        drpCountryCode.Enabled = true;
                        drpCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();
                        IsEmail = true;
                    }
                    else if (!string.IsNullOrEmpty(log.PhoneNumber))
                    {
                        txtPhoneNumber.Text = log.PhoneNumber;
                        txtEmail.ReadOnly = false;
                        txtPhoneNumber.ReadOnly = true;
                        drpCountryCode.Enabled = false;
                        drpCountryCode.SelectedValue = log.CountryCode.ToString();
                    }
                }
                else
                {
                    Response.Redirect("/Login");
                }
            }
        }

        protected void btnSignup_OnClick(object sender, EventArgs e)
        {
            if (!IsEmail)
            {
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    User email = new User();
                    email.Email = txtEmail.Text;
                    if (email.GetRecord())
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "eae", "bootbox.alert('Email Already Exists.');", true);   
                        //Response.Write("<script>alert('Email Already Exist.');</script>");
                        return;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtPhoneNumber.Text))
                {
                    User phone = new User();
                    phone.CountryCode = Convert.ToInt64(drpCountryCode.SelectedItem.Value);
                    phone.PhoneNumber = txtPhoneNumber.Text;
                    if (phone.GetRecord())
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "pae", "bootbox.alert('Phone Number Already Exists.');", true);   
                     //   Response.Write("<script>alert('Phone Number Already Exist.');</script>");
                        return;
                    }
                }
            }

            User log = new User();
            log.UniqueId = Request.QueryString["Id"].ToString();
            log.IsDeleted = false;
            if(log.GetRecord())
            {
                if(log.IsVerified)
                {
                    log.FirstName = txtFirstName.Text;
                    log.LastName = txtLastName.Text;
                    log.ProfileUrl= log.CreateProfileUrl(log.FirstName + log.LastName, 0);
                    log.PhoneNumber = txtPhoneNumber.Text;
                    log.Email = txtEmail.Text;
                    log.StreetAddress = txtStreetAddress.Text;
                    log.Appartment = txtApartment.Text;
                    if (hdnLocationId.Value != "")
                    {
                        log.StateId = Convert.ToInt64(hdnLocationId.Value);
                        log.CityId = Convert.ToInt64(hdnLocationId.Value);
                        log.ZipId = Convert.ToInt64(hdnLocationId.Value);
                    }
                    log.CountryCode = Convert.ToInt64(drpCountryCode.SelectedItem.Value);
                    log.Type = (int)HelperEnums.UserType.User;
                    log.IsApprovedByAdmin = true;
                    log.IsProfileUpdated = true;
                    log.Edit(log);
                    
                    EmailMessage emailmessage = new EmailMessage();
                    emailmessage.To = log.Email;
                    emailmessage.Subject = Common.SignupEmailSubject;

                    emailmessage.Message = Common.CreateUserSignupEmailBody(log.FirstName, log.LastName);
                    Common.SendEmail(emailmessage);

                    EmailMessage emailmessageToAdmin = new EmailMessage();
                    emailmessageToAdmin.To = Common.FromEmail;
                    emailmessageToAdmin.Subject = "New User Signup";
                    emailmessageToAdmin.Message = "Hi<br/>A new user registered with emailaddress:" + log.Email + " and phone number:" + log.PhoneNumber + "<br/><br/>Thanks Referlocals Team";
                    Common.SendEmail(emailmessageToAdmin);
                    //Response.Write("<script>alert('Added Succesfully.');window.location.href='/Login';</script>");

                    //Login Code Starts
                    Common.UserId = log.Id;

                    if (!string.IsNullOrEmpty(log.PhoneNumber))
                    {
                        Common.PhoneNumber = log.CountryCode + log.PhoneNumber;
                        Common.SendCodeThroughSms(Common.PhoneNumber, Common.SMSBodyForUserSignup);
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
                            log.UpdateUserDevice("Web", null, null, log.Id);
                            Response.Redirect("/user_dashboard");
                        
                    }
                    ////Login Code Ends
                    
                }
            }
        }
    }
}