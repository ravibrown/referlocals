using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class AddReferal : System.Web.UI.Page
    {
        public static Int64 UserId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (!SessionService.HasKey("UserId"))
            {
                Response.Redirect("/Login?ReturnUrl=" + url);
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Page.RouteData.Values.Count > 0)
                    {
                        string subcategoryName = Page.RouteData.Values["subcategory"] as string;
                        string profileUrl = Page.RouteData.Values["profileurl"] as string;
                        SubCategory subcat = new SubCategory();

                        var subcatID = subcat.GetSubCategoryIDByName(subcategoryName);
                        if (subcatID > 0)
                        {
                            User user = new User();
                            var userData = user.GetUserDetails(subcatID, profileUrl);
                            if (userData != null)
                            {
                                UserId = Convert.ToInt64(userData.Id);
                                lblName.Text = userData.FirstName + " " + userData.LastName;

                                divData.Visible = true;
                                divDataNotFound.Visible = false;
                            }
                            else
                            {
                                divData.Visible = false;
                                divDataNotFound.Visible = true;
                            }

                        }
                        else
                        {
                            divData.Visible = false;
                            divDataNotFound.Visible = true;
                        }
                    }
                    else if (Request.QueryString["Id"] != null)
                    {
                        // BindDropDownList();
                        User log = new User();
                        log.UniqueId = Request.QueryString["Id"].ToString();
                        if (log.GetRecord())
                        {
                            UserId = Convert.ToInt64(log.Id);
                            lblName.Text = log.FirstName + " " + log.LastName;
                            divData.Visible = true;
                            divDataNotFound.Visible = false;
                        }
                        else
                        {
                            divData.Visible = false;
                            divDataNotFound.Visible = true;
                        }
                    }
                    else
                    {
                        Response.Redirect("/Index");
                    }
                }
            }
        }

        //public void BindDropDownList()
        //{
        //    States city = new States();
        //    List<States> lst_city = city.GetAllByCity();
        //    drpCity.Items.Clear();
        //    drpCity.Items.Add(new ListItem("Select", "0"));
        //    if (lst_city != null && lst_city.Count > 0)
        //    {
        //        foreach (var item in lst_city)
        //        {
        //            drpCity.Items.Add(new ListItem(item.City + "," + item.Zip, item.Id.ToString()));
        //        }
        //    }
        //}
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Referal obj = new Referal();
            obj.SenderId = (Int64)SessionService.Pull("UserId");
            if (UserId != 0)
            {
                obj.UserId = UserId;
                if (obj.SenderId != obj.UserId)
                {
                    obj.Comment = txtComment.Text;

                    if (drpSatisfied.SelectedItem.Value == "1")
                    {
                        obj.IsSatisfied = true;
                    }
                    else
                    {
                        obj.IsSatisfied = false;
                    }

                    if (drpRefered.SelectedItem.Value == "1")
                    {
                        obj.IsRefered = true;
                    }
                    else
                    {
                        obj.IsRefered = false;
                    }
                    obj.CityId = Convert.ToInt64(hdnLocationID.Value); //Convert.ToInt64(drpCity.SelectedItem.Value);
                    obj.IsFlag = false;
                    obj.IsApprovedByAdmin = true;
                    obj.Add(obj);
                    ResetAll();
                    divReferalForm.Visible = false;
                    divReferalThanks.Visible = true;
                    //divReferalThanks.InnerHtml = " Thank You for referring " + lblName.Text + ". " + "Your referral will help us a lot.";
                    Response.Write("<script>setTimeout(function(){ window.location.href='/ReferDetail?Id="+Request.QueryString["Id"]+"' }, 5000);</script>");
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(),"unable refer","bootbox.alert('You are not able to refer yourself.')",true);
                    //Response.Write("<script>bootbox.alert('You are not able to refer yourself.');</script>");
                }
            }
        }

        public void ResetAll()
        {
            txtComment.Text = "";
            txtWorkSearchCity.Value = "";
            //drpCity.SelectedIndex = 0;
            drpRefered.SelectedIndex = 0;
            drpSatisfied.SelectedIndex = 0;
        }
        
    }
}