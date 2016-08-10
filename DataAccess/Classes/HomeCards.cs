using AutoMapper;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class HomeCards : dbContext
    {
        //IMapper mapper;
        //public HomeCards()
        //{
        //    mapper = new AutoMapperWebConfiguration().mapper;
        //}
        #region"properties"

        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Link = string.Empty;
        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }

        private Int64? _Position= 0;
        public Int64? Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private string _IconImage = string.Empty;
        public string IconImage
        {
            get { return _IconImage; }
            set { _IconImage = value; }
        }

        private bool _IsApprovedByAdmin = false;
        public bool IsApprovedByAdmin
        {
            get { return _IsApprovedByAdmin; }
            set { _IsApprovedByAdmin = value; }
        }

        private bool _IsDeleted = false;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        private DateTime? _UpdatedDate = DateTime.UtcNow;
        public DateTime? UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        private DateTime? _CreatedDate = DateTime.UtcNow;
        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        private Int64? _CreatedBy = 0;
        public Int64? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private Int64 _IsApproved = (int)HelperEnums.BooleanValues.Both;
        public Int64 IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        private Int64 _SNO = 0;
        public Int64 SNO
        {
            get { return _SNO; }
            set { _SNO = value; }
        }

        //Take
        private Int64 _Take = 0;
        public Int64 Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Index
        private Int64 _Index = 0;
        public Int64 Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }

        #endregion

        #region "Add Delete Update"
        public bool Add(HomeCards obj)
        {
            try
            {
                tbl_HomeCards checkobj = db.tbl_HomeCards.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {
                   // AutoMapper.Mapper.CreateMap<HomeCards, tbl_HomeCards>();
                    tbl_HomeCards newobj = Mapper.Map<HomeCards, tbl_HomeCards>(obj);
                    db.tbl_HomeCards.Add(newobj);
                    db.SaveChanges();
                    Id = newobj.Id;
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Edit(HomeCards obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<HomeCards, tbl_HomeCards>();
                tbl_HomeCards checkobj = db.tbl_HomeCards.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<HomeCards, tbl_HomeCards>(obj,checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(HomeCards obj)
        {
            try
            {
                tbl_HomeCards checkobj = db.tbl_HomeCards.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_HomeCards.Remove(checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }
        #endregion

        #region "Get Method"

        public List<HomeCards> GetAll()
        {
            List<HomeCards> lst = new List<HomeCards>();
            try
            {
                if (Position != 0)
                {
                    lst = (from r in db.tbl_HomeCards
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (Position != 0 ? r.Position !=0 : Position == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (!String.IsNullOrEmpty(Title) ? r.Title.ToLower() == Title.ToLower() : String.IsNullOrEmpty(Title)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new HomeCards
                           {
                               Id = r.Id,
                               Title = r.Title,
                               Position = r.Position,
                               Image = r.Image,
                               Link = r.Link,
                               IconImage = r.IconImage,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).OrderBy(a=>a.Position).ToList();
                }
                else if(Take!=0 && Index==0)
                {
                    lst = (from r in db.tbl_HomeCards
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (Position != 0 ? r.Position == r.Position : Position == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (!String.IsNullOrEmpty(Title) ? r.Title.ToLower() == Title.ToLower() : String.IsNullOrEmpty(Title)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new HomeCards
                           {
                               Id = r.Id,
                               Title = r.Title,
                               Position = r.Position,
                               Image = r.Image,
                               Link = r.Link,
                               IconImage = r.IconImage,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).Take(Convert.ToInt32(Take)).ToList();
                }
                else if(Take!=0 && Index!=0)
                {
                    lst = (from r in db.tbl_HomeCards
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (Position != 0 ? r.Position == r.Position : Position == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (!String.IsNullOrEmpty(Title) ? r.Title.ToLower() == Title.ToLower() : String.IsNullOrEmpty(Title)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new HomeCards
                           {
                               Id = r.Id,
                               Title = r.Title,
                               Position = r.Position,
                               Image = r.Image,
                               Link = r.Link,
                               IconImage = r.IconImage,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).OrderBy(a=>a.Id).Take(Convert.ToInt32(Take)).Skip(Convert.ToInt32(Take * Index)).ToList();
                }
                else
                {
                    lst = (from r in db.tbl_HomeCards
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (Position != 0 ? r.Position == r.Position : Position == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (!String.IsNullOrEmpty(Title) ? r.Title.ToLower() == Title.ToLower() : String.IsNullOrEmpty(Title)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new HomeCards
                           {
                               Id = r.Id,
                               Title = r.Title,
                               Position = r.Position,
                               Image = r.Image,
                               Link = r.Link,
                               IconImage = r.IconImage,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).ToList();
                }


                lst = lst.Select((r, index) => new HomeCards
{
    Id = r.Id,
    Title = r.Title,
    Position = r.Position,
    Image = r.Image,
    Link = r.Link,
    IconImage = r.IconImage,
    CreatedDate = r.CreatedDate,
    UpdatedDate = r.UpdatedDate,
    CreatedBy = r.CreatedBy,
    IsDeleted = r.IsDeleted == true ? true : false,
    IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
    SNO = index + 1,
}).ToList();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return lst;
        }

        public bool GetRecord()
        {
            try
            {
                tbl_HomeCards obj = db.tbl_HomeCards.FirstOrDefault(r => (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (Position != 0 ? r.Position == Position : Position == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (!String.IsNullOrEmpty(Title) ? r.Title.ToLower() == Title.ToLower() : String.IsNullOrEmpty(Title)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           ));
                if (obj != null)
                {
                    Id = obj.Id;
                    Title = obj.Title;
                    IconImage = obj.IconImage;
                    Position = obj.Position;
                    Link = obj.Link;
                    Image = obj.Image;
                    CreatedDate = obj.CreatedDate;
                    UpdatedDate = obj.UpdatedDate;
                    CreatedBy = obj.CreatedBy;
                    IsDeleted = Convert.ToBoolean(obj.IsDeleted);
                    IsApprovedByAdmin = Convert.ToBoolean(obj.IsApprovedByAdmin);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool SetPosition(int SelectedPosition)
        {
            try
            {
                tbl_HomeCards obj = db.tbl_HomeCards.FirstOrDefault(r => (
                               (Position != 0 ? r.Position == Position : Position == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           ));
                if (obj != null)
                {
                    //AutoMapper.Mapper.CreateMap<tbl_HomeCards, HomeCards>();
                    HomeCards card = Mapper.Map<tbl_HomeCards, HomeCards>(obj);
                    if (SelectedPosition != 0)
                    {
                        card.Position = SelectedPosition;
                        Edit(card);                        
                    }
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }
        #endregion

        #region "TotalRecords"
        public Int64 GetTotalRecords()
        {
            Int64 Records = 0;
            try
            {
                Records = db.tbl_HomeCards.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                      (Position != 0 ? a.Position == Position : Position == 0) &&
                       (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Title) ? a.Title.ToLower() == Title.ToLower() : string.IsNullOrEmpty(Title)));
            }
            catch (Exception ex)
            {

            }
            return Records;
        }
        #endregion
    }
}
