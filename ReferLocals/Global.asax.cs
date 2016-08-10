using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ReferLocals
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
           
            //Admin Section
            RouteTable.Routes.MapPageRoute("AdminAddHomeCardRoute", "Admin/AddHomeCard", "~/Admin/AddHomeCard.aspx");
            RouteTable.Routes.MapPageRoute("AdminAddHomeVideoRoute", "Admin/AddHomeVideo", "~/Admin/AddHomeVideo.aspx");
            RouteTable.Routes.MapPageRoute("AdminLoginRoute", "Admin/Login", "~/Admin/Login.aspx");
            RouteTable.Routes.MapPageRoute("AdminAddCategoryRoute", "Admin/AddCategory", "~/Admin/AddCategory.aspx");
            RouteTable.Routes.MapPageRoute("AdminAddSubCategoryRoute", "Admin/AddSubCategory", "~/Admin/AddSubCategory.aspx");
            RouteTable.Routes.MapPageRoute("AdminAddTestimonialRoute", "Admin/AddTestimonial", "~/Admin/AddTestimonial.aspx");

            //User Section
            RouteTable.Routes.MapPageRoute("IndexRoute", "Index", "~/Index.aspx");
            RouteTable.Routes.MapPageRoute("LoginRoute", "Login", "~/Login.aspx");
            RouteTable.Routes.MapPageRoute("VerificationRoute", "Verification", "~/Verification.aspx");
            RouteTable.Routes.MapPageRoute("ResetPasswordRoute", "ResetPassword", "~/ResetPassword.aspx");
            RouteTable.Routes.MapPageRoute("SelectUserRoute", "SelectUser", "~/SelectUser.aspx");
            RouteTable.Routes.MapPageRoute("UserSectionRoute", "UserSection", "~/UserSection.aspx");
            RouteTable.Routes.MapPageRoute("ProfessionalSectionRoute", "ProfessionalSection", "~/ProfessionalSection.aspx");
            RouteTable.Routes.MapPageRoute("ProfileRoute", "Profile", "~/Profile.aspx");
            RouteTable.Routes.MapPageRoute("ProfileImageRoute", "ProfileImage", "~/ProfileImage.aspx");
            RouteTable.Routes.MapPageRoute("ChangePasswordRoute", "ChangePassword", "~/ChangePassword.aspx");
            RouteTable.Routes.MapPageRoute("ReferProfessionalRoute", "ReferProfessional/{type}", "~/ReferProfessional.aspx");
            RouteTable.Routes.MapPageRoute("SearchReferProfessionalRoute", "SearchReferProfessional/{Title}", "~/SearchReferProfessional.aspx");
            RouteTable.Routes.MapPageRoute("SearchResultRoute", "SearchResult", "~/SearchResult.aspx");
            RouteTable.Routes.MapPageRoute("ReferDetailRoute", "ReferDetail", "~/ReferDetail.aspx");
            RouteTable.Routes.MapPageRoute("AddReferalRoute", "AddReferal", "~/AddReferal.aspx");
            RouteTable.Routes.MapPageRoute("ReferalRoute", "{subcategory}/{profileurl}/referme", "~/AddReferal.aspx");
            RouteTable.Routes.MapPageRoute("PublicProfileRoute", "{subcategory}/{profileurl}", "~/ReferDetail.aspx");
            RouteTable.Routes.MapPageRoute("AddNewJobRoute", "AddNewJob", "~/AddNewJob.aspx");
            RouteTable.Routes.MapPageRoute("dashboard", "user_dashboard", "~/userdashboard.aspx");
            RouteTable.Routes.MapPageRoute("prodashboard", "professional_dashboard", "~/professionaldashboard.aspx");
            RouteTable.Routes.MapPageRoute("SearchJobResultRoute", "JobBoard", "~/SearchJobResult.aspx");
            RouteTable.Routes.MapPageRoute("JobDetailRoute", "JobDetail/{Id}/{Title}", "~/JobDetail.aspx");
            RouteTable.Routes.MapPageRoute("JobQuoteRoute", "JobQuotes/{Id}/{Title}", "~/JobQuotes.aspx");
            RouteTable.Routes.MapPageRoute("ComingsoonRoute", "comingsoon", "~/comingsoon.aspx");
            RouteTable.Routes.MapPageRoute("UserPublicProfileRoute", "user/profile/{ID}", "~/userpublicprofile.aspx");
            RouteTable.Routes.MapPageRoute("faqRoute", "faq", "~/faq.aspx");
            RouteTable.Routes.MapPageRoute("contactus", "contactus", "~/contactus.aspx");
            RouteTable.Routes.MapPageRoute("aboutusRoute", "aboutus", "~/aboutus.aspx");
            RouteTable.Routes.MapPageRoute("TermAndConditionsRoute", "Terms_And_Services", "~/terms_and_conditions.aspx");
            RouteTable.Routes.MapPageRoute("PrivacyRoute", "PrivacyPolicy", "~/privacypolicy.aspx");
            RouteTable.Routes.MapPageRoute("appointmentform", "appointmentform", "~/appointmentform.aspx");
            RouteTable.Routes.MapPageRoute("ForProfessionals", "ForProfessionals", "~/ForProfessional.aspx");

            RouteTable.Routes.MapPageRoute("viewProreferrals", "pro/referrals/{ID}", "~/viewproreferrals.aspx");
            RouteTable.Routes.MapPageRoute("viewreferrals", "user/referrals/{ID}", "~/viewreferrals.aspx");
            RouteTable.Routes.MapPageRoute("userjobs", "user/jobs/{ID}", "~/userjobs.aspx");
            RouteTable.Routes.MapPageRoute("Favorites", "Favorites", "~/favorites.aspx");
            RouteTable.Routes.MapPageRoute("Followers", "Followers", "~/Followers.aspx");
            RouteTable.Routes.MapPageRoute("Inbox", "inbox", "~/inbox.aspx");
            RouteTable.Routes.MapPageRoute("Messages", "messages", "~/messages.aspx");
            RouteTable.Routes.MapPageRoute("JobInbox", "JobInbox", "~/JobInbox.aspx");

            RouteTable.Routes.MapPageRoute("launch", "launch", "~/launch.aspx");

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //if (!HttpContext.Current.Request.IsLocal&& !HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("beta.referlocals.com") && !HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("comingsoon"))
            //{
            //    Response.Redirect("/comingsoon");
            //}
            if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("launch.referlocals.com")&& !HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("launch.aspx"))
            {
                HttpContext.Current.Response.Redirect("/launch.aspx");
            }
            if (!HttpContext.Current.Request.IsSecureConnection && !HttpContext.Current.Request.IsLocal&&!HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("launch.referlocals.com"))
            {
                HttpContext.Current.Response.Redirect(Common.WebsiteHostNameForLink+ HttpContext.Current.Request.Url.PathAndQuery);
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}