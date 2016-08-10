using DataAccess.Classes;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals
{
    public partial class AddNewJob : System.Web.UI.Page
    {
        protected void Page_InIt(object sender, EventArgs e)
        {
            //if ((Int64)SessionService.Pull("UserType") == (int)HelperEnums.UserType.Professional)
            //{
            //    Response.Write("<script>alert('Job post not allowed for professional user.');window.location.href='/index';</script>");
            //}

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Int64)SessionService.Pull("UserType") == (int)HelperEnums.UserType.Professional)
            {
                btnSave.Enabled = false;
                divMsgToProfessional.Visible = true;
            }
            else
            {
                btnSave.Enabled = true;
                divMsgToProfessional.Visible = false;
            }
            if (!IsPostBack)
            {
                BindDropDownList();
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    BindJobData(SessionService.Pull(SessionKeys.UserId), Convert.ToInt64(Request.QueryString["ID"]));
                }
            }
        }
        public void BindJobData(long userID, long jobID)
        {
            Jobs job = new Jobs();
            var jobData = job.GetJobByUserIDAndJobID(userID, jobID);
            if (jobData != null)
            {
                txtTitle.Text = jobData.Title;
                txtAddress.Text = jobData.Address;
                txtDescription.Text = jobData.Description;
                txtWorkSearchCity.Value = jobData.CityName;
                hdnLocationID.Value = Convert.ToString(jobData.CityID);
                hdnJobStatus.Value = Convert.ToString(jobData.JobStatus);
                hdnImageUrl.Value = jobData.Image;
                if (!string.IsNullOrEmpty(jobData.Image))
                {
                    imgPreview.Src = DataAccess.HelperClasses.Common.JobImagesPath + jobData.Image;
                }

                for (int i = 0; i < chkSubCategories.Items.Count; i++)
                {
                    for (int j = 0; j < jobData.SubCategoryName.Count; j++)
                    {
                        if (chkSubCategories.Items[i].Text == jobData.SubCategoryName[j])
                        {
                            chkSubCategories.Items[i].Selected = true;
                        }

                    }

                }
            }
            else
            {
                Response.Write("This job is not related to you or has been deleted or your session has been expired");
            }
        }

        public void BindDropDownList()
        {
            //States city = new States();
            //List<States> lst_city = city.GetAllByCity();
            //drpJobLocation.Items.Clear();
            //drpJobLocation.Items.Add(new ListItem("Select", "0"));
            //if (lst_city != null && lst_city.Count > 0)
            //{
            //    foreach (var item in lst_city)
            //    {
            //        drpJobLocation.Items.Add(new ListItem(item.City + " " + item.State + " " + item.Zip, item.Id.ToString()));
            //    }
            //}

            SubCategory sub = new SubCategory();
            sub.IsApproved = (int)HelperEnums.BooleanValues.Approved;
            List<SubCategory> lst_sub = sub.GetAll();
            chkSubCategories.Items.Clear();
            if (lst_sub != null && lst_sub.Count > 0)
            {
                foreach (var item in lst_sub)
                {
                    chkSubCategories.Items.Add(new ListItem(item.Name, item.Id.ToString()));
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Jobs job = new Jobs();

            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                job.Id = Convert.ToInt64(Request.QueryString["ID"]);
                JobSubCategoryMapping subcategoryMapping = new JobSubCategoryMapping();
                subcategoryMapping.DeleteByJobID(Convert.ToInt64(Request.QueryString["ID"]));
                if (FileImage.HasFile)
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(imgPreview.Src)))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(imgPreview.Src));
                    }
                }
                job.JobStatus = Convert.ToInt32(hdnJobStatus.Value);
            }
            else
            {
                job.JobStatus = 1;
            }
            string uploadfilename = "";
            job.Title = txtTitle.Text;
            job.Address = txtAddress.Text;
            job.Description = txtDescription.Text;
            job.CityId = Convert.ToInt64(hdnLocationID.Value); //Convert.ToInt64(drpJobLocation.SelectedItem.Value);
            job.UserId = (Int64)SessionService.Pull("UserId");

            if (FileImage.HasFile)
            {
                string[] arr = FileImage.PostedFile.FileName.Split('.');

                string file_ext = arr[1].ToLower();

                if (file_ext == "jpeg" || file_ext == "jpg" || file_ext == "png")
                {
                    uploadfilename = DateTime.Now.ToFileTimeUtc().ToString();
                    uploadfilename = uploadfilename + "." + file_ext;
                    job.Image = uploadfilename;
                    FileImage.SaveAs(Server.MapPath(Common.JobImagesPath) + uploadfilename);
                }
                else
                {
                    uploadfilename = "";
                    job.Image = uploadfilename;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    job.Image = hdnImageUrl.Value;
                }
            }
            job.IsApprovedByAdmin = true;
            bool jobSaved;
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                jobSaved = job.Edit(job);
            }
            else
            {
                jobSaved = job.Add(job);
            }

            if (jobSaved)
            {
                var subcategoryNames = string.Empty;
                List<long> subcategoryIDs = new List<long>();
                foreach (ListItem item in chkSubCategories.Items)
                {
                    if (item.Selected)
                    {
                        subcategoryIDs.Add(Convert.ToInt64(item.Value));
                        subcategoryNames += item.Text + ", ";
                        JobSubCategoryMapping jobmapping = new JobSubCategoryMapping();
                        jobmapping.JobId = job.Id;
                        jobmapping.SubCategoryId = Convert.ToInt64(item.Value);
                        jobmapping.UserId = (Int64)SessionService.Pull("UserId");
                        jobmapping.IsApprovedByAdmin = true;
                        jobmapping.Add(jobmapping);
                    }
                }
                if (string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    
                    
                    var EmailBody = Common.EmailBodyOnPostJobToUser.Replace("##Username##", SessionService.Pull<string>(SessionKeys.UserName)).Replace("##Title##", job.Title).Replace("##JobLink##", Common.WebsiteHostNameForLink + "/jobdetail/" + job.Id + "/" + Common.TextToFriendlyUrl(job.Title)).Replace("##Category##", subcategoryNames.Trim().Substring(0,subcategoryNames.Length-1));
                    Common.SendEmailWithGenericTemplate(SessionService.Pull<string>(SessionKeys.UserEmail), Common.EmailSubjectOnPostJobToUser, EmailBody);

                    Task.Run(async () =>
                    {

                        States state = new States();
                        state.Id = job.CityId.Value;
                        var cityData = state.GetRecord();


                        User user = new User();
                        var proData = user.SearchProfessionalWithLocationForPushNotification(state.Id, subcategoryIDs);
                        //var jsonString = "{jobID:\"" + job.Id + "\"}";
                        var jsonString = "{\"jobID\":\"" + job.Id + "\"}";
                        foreach (var item in proData)
                        {

                            DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                            {
                                await pro.SendWindowAzurePushNotification(item.Platform, Convert.ToString(item.Id), "A new job has been posted under " + subcategoryNames.Remove(subcategoryNames.Length - 1) + " in " + state.City, HelperEnums.PushNotificationType.NewJob, jsonString);
                            }
                        }

                    });
                }
                ResetAll();
                divJobForm.Visible = false;
                divJobThanks.Visible = true;

               

                Response.Write("<script>setTimeout(function(){ window.location.href='/referprofessional/search' }, 5000);</script>");
                // Response.Write("<script>alert('Added Successfully.');</script>");
            }
            else
            {
                Response.Write("<script>bootbox.alert('Unable to add.');</script>");
            }


        }

        public void ResetAll()
        {
            //drpJobLocation.SelectedIndex = 0;
            txtWorkSearchCity.Value = "";
            txtDescription.Text = "";
            txtTitle.Text = "";
        }
    }
}