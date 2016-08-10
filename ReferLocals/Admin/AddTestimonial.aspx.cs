using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Classes;
using DataAccess.HelperClasses;
using System.IO;
using System.Web.Services;

namespace ReferLocals.Admin
{
    public partial class AddTestimonial : System.Web.UI.Page
    {
        public static Int64 TotalRecords = 0;
        public static Int64 PageIndex = 0;
        public static Int64 SelectedPage = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTotalRecords();
                BindPaging();
                BindTestimonial();
            }
        }

        public void BindTestimonial()
        {
            try
            {
                Testimonial obj = new Testimonial();
                obj.IsDeleted = false;
                if (Convert.ToInt64(drpPage.SelectedItem.Value) != -1)
                {
                    obj.Take = Convert.ToInt64(drpPage.SelectedItem.Value);
                    obj.Index = SelectedPage;
                }
                List<Testimonial> lst = obj.GetAll();
                if (lst != null && lst.Count > 0)
                {
                    rptTestimonial.DataSource = lst;
                    rptTestimonial.DataBind();
                    divNoRecords.Visible = false;
                    divTableShow.Visible = true;
                }
                else
                {
                    divNoRecords.Visible = true;
                    divTableShow.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void GetTotalRecords()
        {
            Testimonial obj = new Testimonial();
            obj.IsDeleted = false;
            TotalRecords = obj.GetTotalRecords();
            lblTotalRecords.Text = TotalRecords.ToString();
            if (TotalRecords > Convert.ToInt64(drpPage.SelectedItem.Value))
            {
                lblTotalRecordShow.Text = drpPage.SelectedItem.Value.ToString();
            }
            else
            {
                lblTotalRecordShow.Text = TotalRecords.ToString();
            }
        }
        public void BindPaging()
        {
            try
            {
                if (TotalRecords != 0 && TotalRecords > 5)
                {
                    double Page = Convert.ToDouble(Convert.ToDouble(TotalRecords) / Convert.ToDouble(drpPage.SelectedItem.Value));
                    var splitvalue = Page.ToString().Split('.');
                    PageIndex = Convert.ToInt32(splitvalue[0]);
                    if (Convert.ToInt32(splitvalue[1]) >= 1)
                    {
                        PageIndex = PageIndex + 1;
                    }
                    List<Paging> lst_paging = new List<Paging>();
                    for (int i = 0; i < PageIndex; i++)
                    {
                        Paging p = new Paging();
                        p.Text = Convert.ToInt32(i + 1).ToString();
                        p.Value = i;
                        lst_paging.Add(p);
                    }
                    if (lst_paging != null && lst_paging.Count > 0)
                    {
                        rptPager.DataSource = lst_paging;
                        rptPager.DataBind();
                    }
                }
                else
                {
                    rptPager.DataSource = null;
                    rptPager.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void drpPage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedPage = 0;
            GetTotalRecords();
            BindPaging();
            BindTestimonial();
        }

        public void rptPager_OnItemCommand(Object Sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Paging")
            {
                foreach (RepeaterItem item in rptPager.Items)
                {
                    LinkButton linkButton = item.FindControl("lnkPaging") as LinkButton;
                    linkButton.Enabled = true;
                    linkButton.CssClass = "";
                }
                LinkButton localLink = (LinkButton)e.Item.FindControl("lnkPaging");
                localLink.Enabled = false;
                localLink.CssClass = "active";
                SelectedPage = Convert.ToInt32(e.CommandArgument);
                BindTestimonial();
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                Testimonial obj = new Testimonial();
                string uploadfilename = "";
                string path = "";
                if (hdnId.Value != "")
                {
                    obj.Id = Convert.ToInt64(hdnId.Value);
                    if (obj.GetRecord())
                    {
                        if (FileImage.HasFile)
                        {
                            path = Server.MapPath(Common.TestimonialImagesPath) + obj.Image;
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }
                    }
                }
                obj.Name = txtName.Text;
                if (obj.GetRecord() && hdnId.Value == "")
                {
                    lblErrorMsg.Text = "Already exist this name record.";
                    alertDanger.Style.Add("display", "block !important");
                    alertSuccess.Style.Add("display", "none !important");
                    return;
                }
                obj.Description = txtDescription.Text;
                obj.UserId = 0;

                if (FileImage.HasFile)
                {
                    string[] arr = FileImage.PostedFile.FileName.Split('.');

                    string file_ext = arr[1].ToLower();

                    if (file_ext == "jpeg" || file_ext == "jpg" || file_ext == "png")
                    {
                        uploadfilename = DateTime.Now.ToFileTimeUtc().ToString();
                        uploadfilename = uploadfilename + "." + file_ext;
                        obj.Image = uploadfilename;
                        FileImage.SaveAs(Server.MapPath(Common.TestimonialImagesPath) + uploadfilename);
                    }
                    else
                    {
                        uploadfilename = "no-image-icon.png";
                        obj.Image = uploadfilename;
                    }
                }

                if (chkTestimonial.Checked)
                {
                    obj.IsApprovedByAdmin = true;
                }
                else
                {
                    obj.IsApprovedByAdmin = false;
                }

                if (hdnId.Value == "")
                {
                    obj.Add(obj);
                    lblSuccessMsg.Text = "Added Succesfully";
                    alertSuccess.Style.Add("display", "block !important");
                    alertDanger.Style.Add("display", "none !important");
                }
                else
                {
                    obj.Id = Convert.ToInt64(hdnId.Value);
                    obj.Edit(obj);
                    lblSuccessMsg.Text = "";
                    lblSuccessMsg.Text = "Updated Successfully";
                    alertSuccess.Style.Add("display", "block !important");
                    alertDanger.Style.Add("display", "none !important");
                }
                SelectedPage = 0;
                GetTotalRecords();
                BindPaging();
                BindTestimonial();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Error! Try Again Later";
                alertDanger.Style.Add("display", "block !important");
                alertSuccess.Style.Add("display", "none !important");
            }
        }

        #region "WebMethods"
        [WebMethod]
        public static string DisableTestimonial(int Id, string Approved)
        {
            string result = "";
            if (Id > 0)
            {
                Testimonial cat = new Testimonial();
                cat.Id = Id;
                if (cat.GetRecord())
                {
                    if (Approved == "True")
                    {
                        cat.IsApprovedByAdmin = false;
                        result = "Disabled Successfully";
                    }
                    else
                    {
                        cat.IsApprovedByAdmin = true;
                        result = "Enabled Successfully";
                    }
                    cat.Edit(cat);
                }
            }
            return result;
        }
        #endregion
    }
}