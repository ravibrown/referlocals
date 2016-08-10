using DataAccess.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReferLocals.Admin
{
    public partial class Professionallist : System.Web.UI.Page
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
                BindCategory();
            }
        }

        public void BindCategory()
        {

            User obj = new User();
            obj.IsDeleted = false;
            obj.Type = (long?)DataAccess.HelperClasses.HelperEnums.UserType.Professional;
            if (Convert.ToInt64(drpPage.SelectedItem.Value) != -1)
            {
                obj.Take = Convert.ToInt64(drpPage.SelectedItem.Value);
                obj.Index = SelectedPage;
            }
            List<User> lst = obj.GetAllProfessionals();
            if (lst != null && lst.Count > 0)
            {
                lblTo.Text = Convert.ToString(Convert.ToInt32(lblFrom.Text) + (lst.Count-1));
                rptCategories.DataSource = lst;
                rptCategories.DataBind();
                divNoRecords.Visible = false;
                divTableShow.Visible = true;
            }
            else
            {
                divNoRecords.Visible = true;
                divTableShow.Visible = false;
            }

        }

        public void GetTotalRecords()
        {
            User obj = new User();
            obj.IsDeleted = false;
            TotalRecords = obj.GetTotalProfessionalRecords();
            lblTotalRecords.Text = TotalRecords.ToString();
            //if (TotalRecords > Convert.ToInt64(drpPage.SelectedItem.Value))
            //{
            //    lblTotalRecordShow.Text = drpPage.SelectedItem.Value.ToString();
            //}
            //else
            //{
            //    lblTotalRecordShow.Text = TotalRecords.ToString();
            //}
        }
        public void BindPaging()
        {
            try
            {
                if (TotalRecords != 0 && TotalRecords > 5)
                {
                    double Page = Convert.ToDouble(Convert.ToDouble(TotalRecords) / Convert.ToDouble(drpPage.SelectedItem.Value));

                    PageIndex = Convert.ToInt32(Math.Ceiling(Page));
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
            BindCategory();
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
                lblFrom.Text = Convert.ToString(Convert.ToInt32(drpPage.SelectedValue) * (SelectedPage) + 1);

                BindCategory();
            }
        }

        protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var data = (User)e.Item.DataItem;
                if (data != null)
                {
                    var rptCitiesIServe = (Repeater)e.Item.FindControl("rptCitiesIServe");
                    if (data.CitiesIServe != null)
                    {
                        rptCitiesIServe.DataSource = data.CitiesIServe;
                        rptCitiesIServe.DataBind();
                    }
                    
                }
                
            }
        }
    }
}