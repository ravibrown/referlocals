using DataAccess.Classes;
using DataAccess.HelperClasses;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class ProfessionalSection : System.Web.UI.Page
    {
        public static bool IsEmail = false;
        public static string profileUrl = string.Empty;
        public static List<ProfessionalUrls> urlData;
        public static string professionalPhone = string.Empty;
        public static long professionalCountryCode ;
        protected void Page_Load(object sender, EventArgs e)
        {
           // Page.ClientScript.RegisterStartupScript(this.GetType(), "openrefermodal", "OpenReferMeUrlModal();", true);
            //ltScript.Text = "<script>OpenReferMeUrlModal();</script>";
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    BindDropDownList();
                    BindData();
                }
                else
                {
                    //ltScript.Text = "<script>OpenReferMeUrlModal();</script>";
                    
                    //Response.Write("<script> $('#responsive1').modal('show');</script>");
                    Response.Redirect("/Login");
                }
            }
        }
        //protected void Page_InIt(object sender, EventArgs e)
        //{
           
        //  //  lblLocation.Text = Session["LocationName"] == null ? DataAccess.HelperClasses.Common.DefaultLocationName : Convert.ToString(Session["LocationName"]);
         
        //}
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

            //States city = new States();
            //List<States> lst_city = city.GetAllByCity();
            //drpAvailableCities.Items.Clear();
            //drpAvailableCities.Items.Add(new ListItem("Select", "0"));
            //if (lst_city != null && lst_city.Count > 0)
            //{
            //    foreach (var item in lst_city)
            //    {
            //        drpAvailableCities.Items.Add(new ListItem(item.City + "," + item.Zip, item.Id.ToString()));
            //    }
            //}
        }

        protected void btnSignUp_OnClick(object sender, EventArgs e)
        {
            if (!IsEmail)
            {
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    User email = new User();
                    email.Email = txtEmail.Text;
                    if (email.GetRecord())
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "EAE", "bootbox.alert('Email Already Exists.');", true);   
                     //   Response.Write("<script>alert('Email Already Exist.');</script>");
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
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "PAE", "bootbox.alert('Phone Number Already Exists.');", true);   
                       // Response.Write("<script>alert('Phone Number Already Exist.');</script>");
                        return;
                    }
                    professionalPhone = phone.PhoneNumber;
                    professionalCountryCode= phone.CountryCode.GetValueOrDefault();
                }
            }

            User log = new User();
            log.UniqueId = Request.QueryString["Id"].ToString();
            log.IsDeleted = false;
            if (log.GetRecord())
            {
                if (log.IsVerified)
                {
                    log.FirstName = txtFirstName.Text;
                    log.LastName = txtLastName.Text;
                    log.PhoneNumber = txtPhoneNumber.Text;
                    log.ProfileUrl = log.CreateProfileUrl(log.FirstName + log.LastName, 0);
                    profileUrl = log.ProfileUrl;
                    log.Email = txtEmail.Text;
                    log.CompanyName = txtCompanyName.Text;
                    log.Website = txtBusinessSite.Text;
                    if (hdnZipId.Value != "")
                    {
                        log.StateId = Convert.ToInt64(hdnZipId.Value);
                        log.CityId = Convert.ToInt64(hdnZipId.Value);
                        log.ZipId = Convert.ToInt64(hdnZipId.Value);
                    }
                    log.CountryCode = Convert.ToInt64(drpCountryCode.SelectedItem.Value);
                    professionalPhone = log.PhoneNumber;
                    professionalCountryCode = log.CountryCode.GetValueOrDefault();
                    log.Type = (int)HelperEnums.UserType.Professional;
                    log.IsApprovedByAdmin = true;
                    log.IsProfileUpdated = true;
                    log.Edit(log);

                    if (!string.IsNullOrEmpty(hdnCategories.Text))
                    {
                        string[] lst_subcat = hdnCategories.Text.Split(',');
                        if (lst_subcat != null)
                        {
                            foreach (var item in lst_subcat)
                            {
                                if (item != "")
                                {
                                    UserSubCategoryMapping subcat = new UserSubCategoryMapping();
                                    subcat.UserId = log.Id;
                                    subcat.SubCategoryId = Convert.ToInt64(item);
                                    subcat.IsApprovedByAdmin = true;
                                    subcat.Add(subcat);
                                }
                            }
                        }
                    }

                    
                    if (!string.IsNullOrEmpty(object_tagsinput.Text))
                    {
                        string[] lst_cities = object_tagsinput.Text.Split(',');
                        if (lst_cities != null)
                        {
                            foreach (var item in lst_cities)
                            {
                                if (item != "")
                                {
                                    UserCityMapping city = new UserCityMapping();
                                    city.UserId = log.Id;
                                    city.CityId = Convert.ToInt64(item);
                                    city.IsApprovedByAdmin = true;
                                    city.Add(city);
                                }
                            }
                        }
                    }
                    
                    urlData=log.GetProfessionalProfileUrls(log.Id);
                    EmailMessage emailmessage = new EmailMessage();
                    emailmessage.To = log.Email;
                    emailmessage.Subject = Common.SignupEmailSubject;

                    var profilelinks=string.Empty;
                    foreach(var url in urlData)
                    {
                     profilelinks+="http://"+Request.Url.Host+"/"+url.SubCategoryName+"/"+log.ProfileUrl+"<br/>";
                    }
                    emailmessage.Message = Common.CreateProfessionalSignupEmailBody(log.FirstName, log.LastName,profilelinks );
                    Common.SendEmail(emailmessage);

                    EmailMessage emailmessageToAdmin = new EmailMessage();
                    emailmessageToAdmin.To = Common.FromEmail;
                    emailmessageToAdmin.Subject = "New Professional Signup";
                    emailmessageToAdmin.Message = "Hi<br/>A new professional registered with emailaddress:" + log.Email + " and phone number:" + log.PhoneNumber + "<br/><br/>Thanks Referlocals Team";
                    Common.SendEmail(emailmessageToAdmin);

                    //Login Code Starts
                    Common.UserId = log.Id;

                    if (!string.IsNullOrEmpty(log.PhoneNumber))
                    {
                        Common.PhoneNumber = log.CountryCode + log.PhoneNumber;

                        Common.SendCodeThroughSms(Common.PhoneNumber, Common.SMSBodyForProfessionalSignup);
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
                        
                    }
                    //Login Code Ends

                    // Response.Write("<script>alert('Added Succesfully.');window.location.href='/Login';</script>");
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "openrefermodal", "OpenReferMeUrlModal();", true);
                    ltScript.Text = "<script>OpenReferMeUrlModal();</script>";
                }
            }
        }
        [WebMethod]
        public static bool SendReferMeUrlsText()
        { 
             string urls=string.Empty;
            foreach (var url in urlData)
            {
                urls +="http://" + HttpContext.Current.Request.Url.Host + "/" + url.SubCategoryName.Replace(" ","%20") + "/" + profileUrl + "/referme" + Environment.NewLine;
            }

            string msg = @"Hi, Your refer me URL:"+Environment.NewLine + urls + Environment.NewLine + " Thanks for registering " + Environment.NewLine + "Team Referlocals";
           return Common.SendCodeThroughSms(professionalCountryCode + professionalPhone, msg);
            
        }

        public void BindData()
        {
            User log = new User();
            log.UniqueId = Request.QueryString["Id"].ToString();
            log.IsDeleted = false;
            if (log.GetRecord())
            {
                if (!log.IsProfileUpdated)
                {
                    if (!string.IsNullOrEmpty(log.Email))
                    {
                        txtEmailOrPhone.Text = log.Email;
                        txtEmail.Text = log.Email;
                        divEmail.Attributes.Add("style", "display:none");
                        divPhone.Attributes.Add("style", "display:block");
                        drpCountryCode.Attributes.Add("style", "display:block");
                        drpCountryCode.SelectedValue = Common.SelectedCountryCode.ToString();
                        IsEmail = true;
                    }
                    else if (!string.IsNullOrEmpty(log.PhoneNumber))
                    {
                        txtEmailOrPhone.Text = log.PhoneNumber;
                        txtPhoneNumber.Text = log.PhoneNumber;
                        drpCountryCode.SelectedValue = log.CountryCode.ToString();
                        divEmail.Attributes.Add("style", "display:block");
                        divPhone.Attributes.Add("style", "display:none");
                        drpCountryCode.Attributes.Add("style", "display:none");
                    }
                    txtPassword.Attributes.Add("value", log.Password);
                }
                else
                {
                    Response.Redirect("/Login");
                }
            }
        }

        #region "WebMethod"
        [WebMethod]
        public static IList GetCategories(int take, int index)
        {
            List<PropCategory> list_category = new List<PropCategory>();

            try
            {
                Category obj = new Category();
                obj.IsDeleted = false;
                obj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                List<PropCategory> lst_cat = obj.GetAllForAjax();
                if (lst_cat != null && lst_cat.Count > 0)
                {
                    foreach (var item in lst_cat)
                    {
                        SubCategory subObj = new SubCategory();
                        subObj.CategoryId = item.Id;
                        subObj.IsDeleted = false;
                        subObj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                        subObj.Take = take;
                        
                        item.lst_subcategory = subObj.GetAllForAjax();
                        list_category.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return list_category;
        }

        [WebMethod]
        public static string GetCitiesByState(string state)
        {
            try
            {
                List<DropDowns> lst_state = new List<DropDowns>();
                States obj = new States();
                obj.State = state;
                lst_state = obj.GetDropDownAllByState();
                var json = JsonConvert.SerializeObject(lst_state, Formatting.None,
    new JsonSerializerSettings()
    {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string GetZipByCity(string city)
        {
            try
            {
                List<DropDowns> lst_city = new List<DropDowns>();
                States obj = new States();
                obj.City = city;
                lst_city = obj.GetDropDownAllByCity();
                var json = JsonConvert.SerializeObject(lst_city, Formatting.None,
    new JsonSerializerSettings()
    {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });

                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static IList GetSubCategories(int take, int index, int categoryid)
        {
            List<PropSubCategory> list_Subcategory = new List<PropSubCategory>();
            try
            {
                SubCategory subObj = new SubCategory();
                subObj.CategoryId = categoryid;
                subObj.IsDeleted = false;
                subObj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                subObj.Take = take;
                subObj.Index = index;
                list_Subcategory = subObj.GetAllForAjax();

            }
            catch (Exception ex)
            {

            }
            return list_Subcategory;
        }
        #endregion
    }
}