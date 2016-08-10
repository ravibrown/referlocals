using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Classes;
using DataAccess.HelperClasses;
using System.Web.Services;
using Newtonsoft.Json;

namespace ReferLocals
{
    public partial class Profile : System.Web.UI.Page
    {

        public static int UserType = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionService.HasKey("UserId"))
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

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            User log = new User();
            log.Id = (Int64)SessionService.Pull("UserId");
            log.IsDeleted = false;
            log.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            if (log.GetRecord())
            {
                log.Appartment = txtAppartment.Text;
                log.CompanyName = txtCompanyName.Text;
                log.FirstName = txtFirstName.Text;
                log.LastName = txtLastName.Text;
                if (string.IsNullOrEmpty(log.ProfileUrl))
                {
                    log.ProfileUrl = log.CreateProfileUrl(log.FirstName + log.LastName, 0);
                }
                //log.PhoneNumber = txtPhoneNumber.Text;
                if (hdnCountryCode.Value != "")
                {
                    //   log.CountryCode = Convert.ToInt64(hdnCountryCode.Value.ToString());
                }
                else {
                    //  log.CountryCode = Convert.ToInt64(drpCountryCode.SelectedItem.Value.ToString());
                }
                log.Website = txtWebsite.Text;
                log.StreetAddress = txtStreetAddress.Text;
                log.Appartment = txtAppartment.Text;
                log.IsProfileUpdated = true;
                if (hdnZipId.Value != "")
                {
                    log.StateId = Convert.ToInt64(hdnZipId.Value);
                    log.CityId = Convert.ToInt64(hdnZipId.Value);
                    log.ZipId = Convert.ToInt64(hdnZipId.Value);
                    Common.SetGlobalLocation(hdnZipId.Value, txtWorkSearchCity.Value);
                }
                log.Edit(log);


                UserCityMapping obj = new UserCityMapping();
                obj.DeleteListByUserId((Int64)SessionService.Pull("UserId"));

                if (!string.IsNullOrEmpty(hdnSelectedCities.Value))
                {
                    string[] lst_cities = hdnSelectedCities.Value.Split(',');
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

            }
            BindDropDownList();
            BindData();
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
        public void BindCitiesIServe()
        {
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

        public void BindData()
        {
            User log = new User();
            log.Id = (Int64)SessionService.Pull("UserId");
            log.IsDeleted = false;
            log.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            if (log.GetRecord())
            {
                if (log.Type == (int)HelperEnums.UserType.User)
                {
                    divUser.Visible = true;
                    divProfessional.Visible = false;

                }
                else {
                    //BindCitiesIServe();
                    divUser.Visible = false;
                    divProfessional.Visible = true;
                }
                UserType = Convert.ToInt32(log.Type);
                txtEmail.Text = log.Email;
                txtAppartment.Text = log.Appartment;
                txtCompanyName.Text = log.CompanyName;
                txtFirstName.Text = log.FirstName;
                txtLastName.Text = log.LastName;
                txtPhoneNumber.Text = log.PhoneNumber;
                txtStreetAddress.Text = log.StreetAddress;
                txtWebsite.Text = log.Website;
                drpCountryCode.SelectedItem.Text = log.CountryCode.ToString();
                hdnCountryCode.Value = log.CountryCode.ToString();
                States state = new States();
                state.Id = Convert.ToInt64(log.StateId);
                if (state.GetRecord())
                {
                    txtWorkSearchCity.Value = state.City + " " + state.State;
                    //drpState.Items.FindByText(state.State).Selected = true;

                    //States city = new States();
                    //List<DropDowns> lst_City = new List<DropDowns>();
                    //city.State = state.State;
                    //lst_City = city.GetDropDownAllByState();
                    //drpCity.Items.Clear();
                    //drpCity.Items.Add(new ListItem("Select", "0"));
                    //if (lst_City != null && lst_City.Count > 0)
                    //{
                    //    foreach (var item in lst_City)
                    //    {
                    //        drpCity.Items.Add(new ListItem(item.Name, item.Id.ToString()));
                    //    }
                    //}
                    //drpCity.Items.FindByText(state.City).Selected = true;

                    //States zip = new States();
                    //List<DropDowns> lst_zip = new List<DropDowns>();
                    //zip.City = state.City;
                    //lst_zip = zip.GetDropDownAllByCity();
                    //drpZip.Items.Clear();
                    //drpZip.Items.Add(new ListItem("Select", "0"));
                    //if (lst_zip != null && lst_zip.Count > 0)
                    //{
                    //    foreach (var item in lst_zip)
                    //    {
                    //        drpZip.Items.Add(new ListItem(item.Name, item.Id.ToString()));
                    //    }
                    //}
                    //drpZip.Items.FindByText(state.Zip).Selected = true;
                }
            }
        }

        #region "WebMethod"

        [WebMethod]
        public static string GetCitiesOfServe(string state)
        {
            try
            {
                List<DropDownsCityZip> lst_city = new List<DropDownsCityZip>();
                UserCityMapping obj = new UserCityMapping();
                obj.UserId = (Int64)SessionService.Pull("UserId");
                lst_city = obj.GetDropDownAllCitiesServe();
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

        [WebMethod(EnableSession =true)]
        public static string SetCurrentLocation(string postalCode, string locationName,int isUseCurrentPositionclicked)
        {
            string result = "";

            if (HttpContext.Current.Session["LocationId"] == null||isUseCurrentPositionclicked>0)
            {
                States state = new States();
                state.Zip = postalCode;
                var data = state.GetRecord();
                if (state.DataRecieved)
                {
                    //HttpContext.Current.Session["currentLocationId"] = state.Id;
                    //HttpContext.Current.Session["currentLocationName"] = state.City + " " + state.State;

                    HttpContext.Current.Session["LocationId"] = state.Id;
                    HttpContext.Current.Session["LocationName"] = state.City + " " + state.State;
                }
                else
                {

                    //HttpContext.Current.Session["currentLocationId"] = 0;
                    //HttpContext.Current.Session["currentLocationName"] = locationName;

                    HttpContext.Current.Session["LocationId"] = 0;
                    HttpContext.Current.Session["LocationName"] = locationName;
                }

                if (SessionService.HasKey("USerId"))
                {
                    HttpContext.Current.Session["CheckSession"] = true;
                }
            }
            else
            {

            }

            result = Convert.ToString(HttpContext.Current.Session["LocationName"]);

            //result = "done";
            return result;
        }

            
        [WebMethod]
        public static string SetLocation(int Id, string Location)
        {
            string result = "";

            {
                //HttpContext.Current.Session["currentLocationId"] = Id;
                HttpContext.Current.Session["LocationId"] = Id;
                HttpContext.Current.Session["LocationName"] = Location;
                if (SessionService.HasKey("USerId"))
                {

                    HttpContext.Current.Session["CheckSession"] = true;
                }
            }
            result = "done";
            return result;
        }

        #endregion

    }
}