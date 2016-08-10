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
    public partial class SearchReferProfessional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            User obj = new User();
            obj.CountryCode = Convert.ToInt64(drpCountryCode.SelectedItem.Value);
            obj.PhoneNumber = txtPhone.Text;
            obj.Email = txtEmail.Text;
            obj.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            if (obj.SearchProfessionalHasRecords(txtPhone.Text.Trim(), txtEmail.Text.Trim()))
            {
               // UserSubCategoryMapping subcat = new UserSubCategoryMapping();
               // subcat.UserId = (Int64)SessionService.Pull("UserId");
                SubCategory subcat = new SubCategory();
                string Title = Page.RouteData.Values["Title"] as string;
                Title = Title.Replace("-", " ");
                subcat.Name= Title;
                if (subcat.GetRecord())
                {
                    //Response.Redirect("/SearchResult?Id=" + obj.UniqueId+"&email="+txtEmail.Text+"&phone="+txtPhone.Text);
                    Response.Redirect("/SearchResult?email=" + txtEmail.Text + "&phone=" + txtPhone.Text+"&countryCode="+drpCountryCode.SelectedValue+"&subCatID="+subcat.Id);
                }
                else
                {

                }
            }
            else
            {
                User Log = new User();
                Log.CountryCode = Convert.ToInt64(drpCountryCode.SelectedItem.Value);
                Log.PhoneNumber = txtPhone.Text;
                if (!Log.GetRecord())
                {
                    Log.CountryCode = 0;
                    Log.PhoneNumber = "";
                    Log.Email = txtEmail.Text;
                    if (!Log.GetRecord())
                    {
                        Log.CountryCode = Convert.ToInt64(drpCountryCode.SelectedItem.Value);
                        Log.PhoneNumber = txtPhone.Text;
                        if (!string.IsNullOrEmpty(txtName.Text))
                        {

                            string[] namesplit = txtName.Text.Split(' ');
                            if (!string.IsNullOrEmpty(namesplit[0]))
                            {
                                obj.FirstName = namesplit[0];
                            }
                            if (namesplit.Length > 1)
                            {
                                if (!string.IsNullOrEmpty(namesplit[1]))
                                {
                                    obj.LastName = namesplit[1];
                                }
                            }
                            obj.Type = (int)HelperEnums.UserType.Professional;
                            obj.RoleId = (int)HelperEnums.Role.User;
                            obj.Password = Common.Encode(Common.DefaultUserPassword);
                            obj.VerificationCode = Common.NewVerificationCode().Substring(0, 6);
                            obj.UniqueId = Common.CreateUniqueId().Substring(0, 6);
                            if (obj.Add(obj))
                            {
                                SubCategory subcat = new SubCategory();
                                string Title = Page.RouteData.Values["Title"] as string;
                                Title = Title.Replace("-", " ");
                                subcat.Name = Title;
                                if (subcat.GetRecord())
                                {
                                    UserSubCategoryMapping cat = new UserSubCategoryMapping();
                                    cat.UserId = obj.Id;
                                    cat.SubCategoryId = subcat.Id;
                                    cat.IsApprovedByAdmin = true;
                                    if (cat.Add(cat))
                                    {
                                        Response.Redirect("/AddReferal?Id=" + obj.UniqueId);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "enm", "bootbox.alert('No Professional matched with entered email or phone number');", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "pnm", "bootbox.alert('Email exists but phone number is not matched');", true);   
                        //Response.Write("<script>alert('Email exist but phone number is not matched');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "enm", "bootbox.alert('No Professional matched with entered email or phone number');", true);
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "enm", "bootbox.alert('Phone number exists but email is not matched');", true);   

                }
            }
        }

    }
}