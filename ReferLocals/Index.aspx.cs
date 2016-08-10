using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class Index : System.Web.UI.Page
    {

        protected void Page_InIt(object sender, EventArgs e)
        {
           // lblLocation.Text = Session["LocationName"] == null ? DataAccess.HelperClasses.Common.DefaultLocationName : Convert.ToString(Session["LocationName"]);

        }
        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!IsPostBack)
            {   

                BindVideo();
                BindHomeCard();
               // BindTestimonial();
                BindSubCategory();
                BindLocation();
            }
        }

        public void BindVideo()
        {
            HomeVideo video = new HomeVideo();
            video.IsDeleted = false;
            List<HomeVideo> lst = video.GetAll();
            if (lst != null && lst.Count > 0)
            {
                rptVideos.DataSource = lst;
                rptVideos.DataBind();
            }

        }

        public void BindHomeCard()
        {
            HomeCards card = new HomeCards();
            card.IsDeleted = false;
            card.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            card.Position = 1;
            List<HomeCards> lst = card.GetAll();
            if (lst != null && lst.Count > 0)
            {
                rptCards.DataSource = lst;
                rptCards.DataBind();
            }

        }

        public void BindTestimonial()
        {
            Testimonial test = new Testimonial();
            test.IsDeleted = false;
            test.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            test.Take = 3;
            List<Testimonial> lst = test.GetAll();
            if (lst != null && lst.Count > 0)
            {
                rptTestimonial.DataSource = lst;
                rptTestimonial.DataBind();
            }

        }

        public void BindSubCategory()
        {
            SubCategory test = new SubCategory();
            test.IsDeleted = false;
            test.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            test.Take = 4;
            test.CategoryId = 1;
            List<SubCategory> lst = test.GetAll();
            if (lst != null && lst.Count > 0)
            {
                rptSubCategory.DataSource = lst;
                rptSubCategory.DataBind();
                rptJobCategory.DataSource = lst;
                rptJobCategory.DataBind();
            }

        }
        public void BindLocation()
        {
            if (SessionService.HasKey("UserId") && !SessionService.HasKey("CheckSession"))
            {
                //States state = new States();
                //state.Id = Convert.ToInt64(Common.LocationId);
                //Session["LocationId"] = Common.LocationId;
                //if (state.GetRecord())
                //{
                //    Session["LocationName"] = state.City + " " + state.State + " " + state.Zip;
                //}

            }
            else if (SessionService.HasKey("LocationId"))
            {   
                States state = new States();
                state.Id = (Int64)SessionService.Pull("LocationId");
                if (state.Id > 0)
                {
                    if (state.GetRecord())
                    {
                       HttpContext.Current.Session["LocationName"] = state.City + " " + state.State;
                    }
                }
            }
            //else
            //{
            //    Session["LocationId"] = Common.DefaultLocationId;
            //    Session["LocationName"] = Common.DefaultLocationName;
            //}
        }



        #region "WebMethod"
        [WebMethod]
        public static string Logout()
        {
            string result = "";
            Common.RemoveSession();
            result = "/Index";
            return result;
        }

        [WebMethod]
        public static void GetAppLink(string mobile,string countryCode)
        {
            Common.SendCodeThroughSms(countryCode + mobile, "App link for referlocals is " + Common.IOSAppLink);

        }
        [WebMethod]
        public static List<PropStates> GetLocations(string Keyword)
        {
            try
            {
                States city = new States();
                city.Take = 40;
                List<PropStates> lst_city = city.GetCityZipByKeyword(Keyword);
                return lst_city;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static bool IsloggedIn()
        {
            if (SessionService.HasKey("UserId"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}