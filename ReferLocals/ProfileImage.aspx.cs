using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class ProfileImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindImages();
            }
        }

        public void BindImages()
        {
            UserImages user = new UserImages();
            user.UserId = (Int64)SessionService.Pull("UserId");
            user.IsDeleted = false;
            List<UserImages> lst = user.GetAll();
            if (lst != null && lst.Count > 0)
            {
                rptImages.DataSource = lst;
                rptImages.DataBind();
            }
            else
            {
                rptImages.DataSource = null;
                rptImages.DataBind();
                Session["UserImage"] = "";
                Common.UserImage = "";
            }
        }

        protected void rptImages_OnItemCommand(object sender,RepeaterCommandEventArgs e)
        {
            if(e.CommandName=="SetProfilePicture")
            {
                UserImages user = new UserImages();
                user.Id = Convert.ToInt64(e.CommandArgument);
                user.IsDeleted = false;
                if (user.GetRecord())
                {
                    User log = new User();
                    log.Id = (Int64)SessionService.Pull("UserId");
                    log.IsDeleted = false;
                    log.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                    log.IsProfileUpdated = true;
                    if (log.GetRecord())
                    {
                        log.Image = user.Image;                        
                        if(log.Edit(log))
                        {
                            Session["UserImage"] = user.Image;
                            Common.UserImage = user.Image;
                        }
                    }
                }
            }
            else if(e.CommandName=="Delete")
            {
                UserImages user = new UserImages();
                user.Id = Convert.ToInt64(e.CommandArgument);
                user.IsDeleted = false;
                if (user.GetRecord())
                {
                    string path = Server.MapPath(Common.UserImagesPath) + user.Image;
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        user.Delete(user);
                    }
                }
              
            }
            BindImages();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                UserImages obj = new UserImages();
                string uploadfilename = "";
                if (FileImage.HasFile)
                {

                    string[] arr = FileImage.PostedFile.FileName.Split('.');

                    string file_ext = arr[1].ToLower();

                    if (file_ext == "jpeg" || file_ext == "jpg" || file_ext == "png")
                    {
                        uploadfilename = DateTime.Now.ToFileTimeUtc().ToString();
                        uploadfilename = uploadfilename + "." + file_ext;
                        obj.Image = uploadfilename;
                        obj.IsProfilePicture = true;
                        FileImage.SaveAs(Server.MapPath(Common.UserImagesPath) + uploadfilename);
                    }
                    else
                    {
                        uploadfilename = "";
                        obj.Image = uploadfilename;
                    }
                    obj.IsApprovedByAdmin = true;
                    obj.UpdatedDate = DateTime.UtcNow;
                    obj.UserId = (Int64)SessionService.Pull("UserId");
                    if(obj.Add(obj))
                    {
                        User log = new User();
                        log.Id = (Int64)SessionService.Pull("UserId");
                        log.IsDeleted = false;
                        log.IsApproved = (int)HelperEnums.BooleanValues.Approved;
                        log.IsProfileUpdated = true;
                        if(log.GetRecord())
                        {
                            log.Image = obj.Image;
                            log.Edit(log);
                            Session["UserImage"] = obj.Image;
                            Common.UserImage = obj.Image;
                        }
                    }
                }
                BindImages();
            }
            catch (Exception ex)
            {

            }
        }
    }
}