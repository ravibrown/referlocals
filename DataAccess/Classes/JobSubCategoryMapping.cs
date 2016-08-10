using AutoMapper;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class JobSubCategoryMapping : dbContext
    {
        //IMapper mapper;
        //public JobSubCategoryMapping()
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

        private Int64? _SubCategoryId = 0;
        public Int64? SubCategoryId
        {
            get { return _SubCategoryId; }
            set { _SubCategoryId = value; }
        }

        private string _SubCategoryName = string.Empty;
        public string SubCategoryName
        {
            get { return _SubCategoryName; }
            set { _SubCategoryName = value; }
        }


        private Int64? _UserId = 0;
        public Int64? UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private Int64? _JobId = 0;
        public Int64? JobId
        {
            get { return _JobId; }
            set { _JobId = value; }
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
        public bool Add(JobSubCategoryMapping obj)
        {
            try
            {
                tbl_Job_SubCategory_Mapping checkobj = db.tbl_Job_SubCategory_Mapping.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {
                   // AutoMapper.Mapper.CreateMap<JobSubCategoryMapping, tbl_Job_SubCategory_Mapping>();
                    tbl_Job_SubCategory_Mapping newobj = Mapper.Map<JobSubCategoryMapping, tbl_Job_SubCategory_Mapping>(obj);
                    db.tbl_Job_SubCategory_Mapping.Add(newobj);
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

        public bool Edit(JobSubCategoryMapping obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<JobSubCategoryMapping, tbl_Job_SubCategory_Mapping>();
                tbl_Job_SubCategory_Mapping checkobj = db.tbl_Job_SubCategory_Mapping.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<JobSubCategoryMapping, tbl_Job_SubCategory_Mapping>(obj, checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(JobSubCategoryMapping obj)
        {
            try
            {
                tbl_Job_SubCategory_Mapping checkobj = db.tbl_Job_SubCategory_Mapping.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_Job_SubCategory_Mapping.Remove(checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }
        public bool DeleteByJobID(long jobID)
        {
            try
            {
                var subcategoryMappingData = db.tbl_Job_SubCategory_Mapping.Where(a => a.JobId == jobID);
                db.tbl_Job_SubCategory_Mapping.RemoveRange(subcategoryMappingData);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region "Get Method"

        public List<JobSubCategoryMapping> GetAll()
        {
            List<JobSubCategoryMapping> lst = new List<JobSubCategoryMapping>();
            try
            {
                if (Take != 0 && Index == 0)
                {
                    lst = (from r in db.tbl_Job_SubCategory_Mapping
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SubCategoryId != 0 ? r.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                               (!string.IsNullOrEmpty(SubCategoryName) ? r.tbl_SubCategory.Name.ToLower() == SubCategoryName.ToLower() : string.IsNullOrEmpty(SubCategoryName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new JobSubCategoryMapping
                           {
                               Id = r.Id,
                               SubCategoryId = r.SubCategoryId,
                               JobId = r.JobId,
                               UserId = r.UserId,
                               SubCategoryName = r.tbl_SubCategory.Name,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).Take(Convert.ToInt32(Take)).ToList();

                }
                else if (Take != 0 && Index != 0)
                {
                    lst = (from r in db.tbl_Job_SubCategory_Mapping
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SubCategoryId != 0 ? r.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                               (!string.IsNullOrEmpty(SubCategoryName) ? r.tbl_SubCategory.Name.ToLower() == SubCategoryName.ToLower() : string.IsNullOrEmpty(SubCategoryName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new JobSubCategoryMapping
                           {
                               Id = r.Id,
                               SubCategoryId = r.SubCategoryId,
                               JobId = r.JobId,
                               UserId = r.UserId,
                               SubCategoryName = r.tbl_SubCategory.Name,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
                }
                else
                {
                    lst = (from r in db.tbl_Job_SubCategory_Mapping
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SubCategoryId != 0 ? r.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                               (!string.IsNullOrEmpty(SubCategoryName) ? r.tbl_SubCategory.Name.ToLower() == SubCategoryName.ToLower() : string.IsNullOrEmpty(SubCategoryName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new JobSubCategoryMapping
                           {
                               Id = r.Id,
                               JobId = r.JobId,
                               SubCategoryId = r.SubCategoryId,
                               UserId = r.UserId,
                               SubCategoryName = r.tbl_SubCategory.Name,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).ToList();
                }
                lst = lst.Select((r, index) => new JobSubCategoryMapping
                {
                    Id = r.Id,
                    SubCategoryId = r.SubCategoryId,
                    JobId = r.JobId,
                    UserId = r.UserId,
                    SubCategoryName = r.SubCategoryName,
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
                tbl_Job_SubCategory_Mapping obj = db.tbl_Job_SubCategory_Mapping.FirstOrDefault(r => (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SubCategoryId != 0 ? r.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (!string.IsNullOrEmpty(SubCategoryName) ? r.tbl_SubCategory.Name.ToLower() == SubCategoryName.ToLower() : string.IsNullOrEmpty(SubCategoryName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           ));
                if (obj != null)
                {
                    Id = obj.Id;
                    SubCategoryId = obj.SubCategoryId;
                    UserId = obj.UserId;
                    JobId = obj.JobId;
                    SubCategoryName = obj.tbl_SubCategory.Name;
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
        #endregion
    }
}
