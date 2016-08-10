using AutoMapper;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class Testimonial : dbContext
    {
        //IMapper mapper;
        //public Testimonial()
        //{
        //    mapper = new AutoMapperWebConfiguration().mapper;
        //}
        #region "Properties"
        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //SNO
        private Int64 _SNO = 0;
        public Int64 SNO
        {
            get { return _SNO; }
            set { _SNO = value; }
        }

        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        //UserId
        private Int64? _UserId = 0;
        public Int64? UserId
        {
            get { return _UserId ?? 0; }
            set { _UserId = value; }
        }

        //IsApproved
        private Int64 _IsApproved = (int)HelperEnums.BooleanValues.Both;
        public Int64 IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
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

        //CreatedDate
        private DateTime? _CreatedDate = DateTime.UtcNow;
        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        //UpdatedDate
        private DateTime? _UpdatedDate = DateTime.UtcNow;
        public DateTime? UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        //CreatedBy
        private Int64? _CreatedBy = 0;
        public Int64? CreatedBy
        {
            get { return _CreatedBy ?? 0; }
            set { _CreatedBy = value; }
        }

        //IsDeleted
        private bool _IsDeleted = false;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        //IsApprovedByAdmin
        private bool _IsApprovedByAdmin = false;
        public bool IsApprovedByAdmin
        {
            get { return _IsApprovedByAdmin; }
            set { _IsApprovedByAdmin = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }

        #endregion

        #region "Add Delete Update"
        public bool Add(Testimonial obj)
        {
            try
            {
                tbl_Testimonial checkobj = db.tbl_Testimonial.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {
                    //AutoMapper.Mapper.CreateMap<Testimonial, tbl_Testimonial>();
                    tbl_Testimonial newobj = Mapper.Map<Testimonial, tbl_Testimonial>(obj);
                    db.tbl_Testimonial.Add(newobj);
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

        public bool Edit(Testimonial obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<Testimonial, tbl_Testimonial>();
                tbl_Testimonial checkobj = db.tbl_Testimonial.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<Testimonial, tbl_Testimonial>(obj, checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(Testimonial obj)
        {
            try
            {
                tbl_Testimonial checkobj = db.tbl_Testimonial.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_Testimonial.Remove(checkobj);
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

        public List<Testimonial> GetAll()
        {
            List<Testimonial> lst = new List<Testimonial>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_Testimonial
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new Testimonial
                       {
                           Id = r.Id,
                           Name = r.Name,
                           UserId=r.UserId,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_Testimonial
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new Testimonial
                       {
                           Id = r.Id,
                           Name = r.Name,
                           UserId = r.UserId,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_Testimonial
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? r.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)))

                       select new Testimonial
                       {
                           Id = r.Id,
                           Name = r.Name,
                           UserId = r.UserId,
                           Description = r.Description,
                           Image = r.Image,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new Testimonial
            {
                Id = r.Id,
                Name = r.Name,
                UserId = r.UserId,
                Description = r.Description,
                Image = r.Image,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                SNO = index + 1
            }).ToList();

            return lst;
        }
        public bool GetRecord()
        {
            tbl_Testimonial obj = db.tbl_Testimonial.FirstOrDefault(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                   (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                   (UserId != 0 ? a.UserId == UserId : UserId == 0) &&
                   (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                   (!string.IsNullOrEmpty(Name) ? a.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)));
            if (obj != null)
            {
                Id = obj.Id;
                Name = obj.Name;
                Description = obj.Description;
                UserId = obj.UserId;
                Image = obj.Image;
                CreatedBy = obj.CreatedBy;
                CreatedDate = obj.CreatedDate;
                UpdatedDate = obj.UpdatedDate;
                IsDeleted = Convert.ToBoolean(obj.IsDeleted);
                IsApprovedByAdmin = Convert.ToBoolean(obj.IsApprovedByAdmin);
                DataRecieved = true;
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
                Records = db.tbl_Testimonial.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                       (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                       (!string.IsNullOrEmpty(Name) ? a.Name.ToLower() == Name.ToLower() : string.IsNullOrEmpty(Name)));
            }
            catch(Exception ex)
            {

            }
            return Records;
        }
        #endregion
    }
}
