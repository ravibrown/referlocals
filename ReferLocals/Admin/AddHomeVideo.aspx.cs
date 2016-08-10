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
    public partial class AddHomeVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindHomeVideo();
            }
        }
        public void BindHomeVideo()
        {
            HomeVideo obj = new HomeVideo();
            obj.IsDeleted = false;
            if (obj.GetRecord())
            {
                hdnId.Value = obj.Id.ToString();
                lblVideo.Text = obj.Video;
            }
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                HomeVideo obj = new HomeVideo();
                string uploadfilename = "";
                string path = "";
                if (FileVideo.HasFile)
                {
                    if (hdnId.Value != "")
                    {
                        obj.Id = Convert.ToInt64(hdnId.Value);
                        obj.IsDeleted = false;
                        if (obj.GetRecord())
                        {
                            path = Server.MapPath(Common.HomeVideoPath) + obj.Video;
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }
                    }

                    string[] arr = FileVideo.PostedFile.FileName.Split('.');

                    string file_ext = arr[1].ToLower();

                    if (file_ext.ToLower() == "mp4" )
                    {
                        uploadfilename = DateTime.Now.ToFileTimeUtc().ToString();
                        uploadfilename = uploadfilename + "." + file_ext;
                        obj.Video = uploadfilename;
                        FileVideo.SaveAs(Server.MapPath(Common.HomeVideoPath) + uploadfilename);
                    }
                    else
                    {
                        uploadfilename = "no-video.mp4";
                        obj.Video = uploadfilename;
                    }
                    obj.IsApprovedByAdmin = true;
                    obj.UpdatedDate = DateTime.UtcNow;
                    if (hdnId.Value == "")
                    {
                        obj.Add(obj);
                    }
                    else
                    {
                        obj.Id = Convert.ToInt64(hdnId.Value);
                        obj.Edit(obj);
                    }
                }                
                BindHomeVideo();
            }
            catch (Exception ex)
            {

            }
        }
    }
}