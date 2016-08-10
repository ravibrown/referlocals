using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals.Admin
{
    public partial class AddHomeCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindHomeCards();
            }
        }

        public void BindHomeCards()
        {
            HomeCards obj = new HomeCards();
            obj.IsDeleted = false;
            obj.Take = 4;
            List<HomeCards> lst = obj.GetAll();
            if (lst != null && lst.Count > 0)
            {
                rptHomeCards.DataSource = lst;
                rptHomeCards.DataBind();
            }
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                HomeCards obj = new HomeCards();
                string uploadfilename = "";
                string uploadIconFileName = "";
                string path = "";
                int Position = 0;
                Int64 Records = obj.GetTotalRecords();
                if (hdnId.Value == "" && Records >= 4)
                {
                    lblSuccessMsg.Text = "4 records already added.";
                    alertSuccess.Style.Add("display", "block !important");
                    alertDanger.Style.Add("display", "none !important");
                }
                else
                {
                    if (hdnId.Value != "")
                    {
                        obj.Id = Convert.ToInt64(hdnId.Value);
                        if (obj.GetRecord())
                        {
                            if (FileImage.HasFile)
                            {
                                path = Server.MapPath(Common.HomeCardImagePath) + obj.Image;
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                }
                            }
                            if (FileIconImage.HasFile)
                            {
                                path = Server.MapPath(Common.HomeCardIconImagePath) + obj.IconImage;
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                }
                            }
                            Position = Convert.ToInt32(obj.Position);
                        }
                    }
                    obj.Title = txtTitle.Text;
                    obj.Link = txtLink.Text;
                    obj.Position = Convert.ToInt32(drpPosition.SelectedItem.Value);

                    if (FileImage.HasFile)
                    {
                        string[] arr = FileImage.PostedFile.FileName.Split('.');

                        string file_ext = arr[1].ToLower();

                        if (file_ext == "jpeg" || file_ext == "jpg" || file_ext == "png")
                        {
                            uploadfilename = DateTime.Now.ToFileTimeUtc().ToString();
                            uploadfilename = uploadfilename + "." + file_ext;
                            obj.Image = uploadfilename;
                            FileImage.SaveAs(Server.MapPath(Common.HomeCardImagePath) + uploadfilename);
                        }
                        else
                        {
                            uploadfilename = "no-image-icon.png";
                            obj.Image = uploadfilename;
                        }
                    }

                    if (FileIconImage.HasFile)
                    {
                        string[] arr = FileIconImage.PostedFile.FileName.Split('.');

                        string file_ext = arr[1].ToLower();

                        if (file_ext == "jpeg" || file_ext == "jpg" || file_ext == "png")
                        {
                            uploadIconFileName = DateTime.Now.ToFileTimeUtc().ToString();
                            uploadIconFileName = uploadIconFileName + "." + file_ext;
                            obj.IconImage = uploadIconFileName;
                            FileIconImage.SaveAs(Server.MapPath(Common.HomeCardIconImagePath) + uploadIconFileName);
                        }
                        else
                        {
                            uploadIconFileName = "no-image-icon.png";
                            obj.IconImage = uploadIconFileName;
                        }
                    }

                    if (chkHomeCard.Checked)
                    {
                        obj.IsApprovedByAdmin = true;
                    }
                    else
                    {
                        obj.IsApprovedByAdmin = false;
                    }

                    bool flag = obj.SetPosition(Position);
                    if (hdnId.Value == "")
                    {
                        if (!flag)
                        {
                            obj.Add(obj);
                            lblSuccessMsg.Text = "Added Succesfully";
                        }
                        else
                        {
                            lblSuccessMsg.Text = "Already exist this position record";
                        }
                        alertSuccess.Style.Add("display", "block !important");
                        alertDanger.Style.Add("display", "none !important");
                    }
                    else
                    {

                        obj.Id = Convert.ToInt64(hdnId.Value);
                        obj.Edit(obj);
                        lblSuccessMsg.Text = "Updated Successfully";
                        //if (!flag)
                        //{
                        //    lblSuccessMsg.Text = "Updated Successfully";
                        //}
                        //else
                        //{
                        //    lblSuccessMsg.Text = "Updated Succesfully and position " + obj.Position + " replaced with this item";
                        //}
                        alertSuccess.Style.Add("display", "block !important");
                        alertDanger.Style.Add("display", "none !important");
                    }
                }
                BindHomeCards();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Error! Try Again Later";
                alertDanger.Style.Add("display", "block !important");
                alertSuccess.Style.Add("display", "none !important");
            }
        }
    }
}