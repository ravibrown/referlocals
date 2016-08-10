using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess.Classes
{
    public class Jobs : dbContext
    {
        //IMapper mapper;
        //public Jobs()
        //{
        //    mapper = new AutoMapperWebConfiguration().mapper;
        //}

        #region"properties"
        public bool IsFlag { get; set; }
        public bool IsFavorite { get; set; }
        public string UrlFriendlyTitle
        {
            get { return Common.TextToFriendlyUrl(Title); }
        }
        public string CreaterUniqueId { get; set; }
        public int? JobStatus { get; set; }
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

        private Int64? _CityId = 0;
        public Int64? CityId
        {
            get { return _CityId; }
            set { _CityId = value; }
        }

        private string _CityName = string.Empty;
        public string CityName
        {
            get { return _CityName; }
            set { _CityName = value; }
        }

        private string _ZipName = string.Empty;
        public string ZipName
        {
            get { return _ZipName; }
            set { _ZipName = value; }
        }

        private string _UserName = string.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private string _Address = string.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private Int64? _UserId = 0;
        public Int64? UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
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
        public bool Add(Jobs obj)
        {
            try
            {
                tbl_Jobs checkobj = db.tbl_Jobs.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {

                    //AutoMapper.Mapper.CreateMap<Jobs, tbl_Jobs>();
                    tbl_Jobs newobj = Mapper.Map<Jobs, tbl_Jobs>(obj);
                    db.tbl_Jobs.Add(newobj);
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

        public bool Edit(Jobs obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<Jobs, tbl_Jobs>();
                tbl_Jobs checkobj = db.tbl_Jobs.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<Jobs, tbl_Jobs>(obj, checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(Jobs obj)
        {
            try
            {
                tbl_Jobs checkobj = db.tbl_Jobs.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_Jobs.Remove(checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool DeleteListByUserId(Int64 UserId)
        {
            try
            {
                List<tbl_Jobs> checkobj = db.tbl_Jobs.Where(a => a.UserId == UserId).ToList();
                if (checkobj != null && checkobj.Count > 0)
                {
                    foreach (var item in checkobj)
                    {
                        db.tbl_Jobs.Remove(item);
                    }
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

        public List<Jobs> GetAll(long loggedInUserID)
        {

            List<Jobs> lst = new List<Jobs>();

            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_Jobs
                       where (
                           (Id != 0 ? r.Id == Id : Id == 0) &&
                           (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                           (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                           (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                           (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                           (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                       )
                       select new Jobs
                       {
                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (int)HelperEnums.FlagType.Job && p.UserID == loggedInUserID) > 0 ? true : false,

                           Id = r.Id,
                           CityId = r.CityId,
                           Image = r.Image,
                           UserName = r.tbl_User.FirstName + " " + r.tbl_User.LastName,
                           Description = r.Description,
                           Address = r.Address,
                           CreaterUniqueId = r.tbl_User.UniqueId,
                           Title = r.Title,
                           UserId = r.UserId,
                           CityName = r.tbl_State.City,

                           ZipName = r.tbl_State.State,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           CreatedBy = r.CreatedBy,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                       }).Take(Convert.ToInt32(Take)).ToList();

            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_Jobs
                       where (
                           (Id != 0 ? r.Id == Id : Id == 0) &&
                           (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                           (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                           (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                           (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                           (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                       )
                       select new Jobs
                       {
                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (int)HelperEnums.FlagType.Job && p.UserID == loggedInUserID) > 0 ? true : false,

                           Id = r.Id,
                           CityId = r.CityId,
                           UserId = r.UserId,
                           Image = r.Image,
                           CreaterUniqueId = r.tbl_User.UniqueId,
                           UserName = r.tbl_User.FirstName + " " + r.tbl_User.LastName,
                           Address = r.Address,
                           Description = r.Description,
                           Title = r.Title,
                           CityName = r.tbl_State.City,
                           ZipName = r.tbl_State.State,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           CreatedBy = r.CreatedBy,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_Jobs
                       where (
                           (Id != 0 ? r.Id == Id : Id == 0) &&
                           (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                           (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                           (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                           (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                           (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                       )
                       select new Jobs
                       {
                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (int)HelperEnums.FlagType.Job && p.UserID == loggedInUserID) > 0 ? true : false,

                           Id = r.Id,
                           CityId = r.CityId,
                           UserId = r.UserId,
                           UserName = r.tbl_User.FirstName + " " + r.tbl_User.LastName,
                           Image = r.Image,
                           Address = r.Address,
                           Description = r.Description,
                           Title = r.Title,
                           CreaterUniqueId = r.tbl_User.UniqueId,
                           CityName = r.tbl_State.City,
                           ZipName = r.tbl_State.State,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           CreatedBy = r.CreatedBy,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                       }).ToList();
            }
            lst = lst.Select((r, index) => new Jobs
            {
                IsFavorite = r.IsFavorite,
                IsFlag = r.IsFlag,

                Id = r.Id,
                CityId = r.CityId,
                UserId = r.UserId,
                UserName = r.UserName,
                Image = r.Image,
                Description = r.Description,
                Title = r.Title,
                Address = r.Address,
                CreaterUniqueId = r.CreaterUniqueId,
                CityName = r.CityName,
                ZipName = r.ZipName,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                CreatedBy = r.CreatedBy,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                SNO = index + 1,
            }).ToList();


            return lst;
        }

        public List<Jobs> GetAllWithJoin()
        {
            var loggedInUserID = SessionService.Pull(SessionKeys.UserId);
            List<Jobs> lst = new List<Jobs>();
            try
            {
                List<long> locations = null;
                if (CityId.HasValue)
                {
                    locations = new States().GetLocationIDsNearBy(CityId.Value);
                }

                if (Take != 0 && Index == 0)
                {
                    lst = (from r in db.tbl_Jobs
                           join s in db.tbl_Job_SubCategory_Mapping on r.Id equals s.JobId
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                                // (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                                (CityId != 0 ? locations.Contains(r.CityId.Value) : CityId == 0) &&
                               (SubCategoryId != 0 ? s.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                               && r.JobStatus == (int)HelperEnums.JobStatus.Open
                           )
                           select new Jobs
                           {
                               IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                               IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (int)HelperEnums.FlagType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                               Id = r.Id,
                               CityId = r.CityId,
                               SubCategoryId = s.SubCategoryId,
                               UserName = r.tbl_User.FirstName + " " + r.tbl_User.LastName,
                               Image = r.Image,
                               Description = r.Description,
                               Address = r.Address,
                               Title = r.Title,
                               UserId = r.UserId,
                               CreaterUniqueId = r.tbl_User.UniqueId,
                               CityName = r.tbl_State.City,
                               ZipName = r.tbl_State.zip,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).Take(Convert.ToInt32(Take)).ToList();

                }
                else if (Take != 0 && Index != 0)
                {
                    lst = (from r in db.tbl_Jobs
                           join s in db.tbl_Job_SubCategory_Mapping on r.Id equals s.JobId
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                              // (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                              (CityId != 0 ? locations.Contains(r.CityId.Value) : CityId == 0) &&
                               (SubCategoryId != 0 ? s.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           && r.JobStatus == (int)HelperEnums.JobStatus.Open
                               )
                           select new Jobs
                           {
                               IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                               IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (int)HelperEnums.FlagType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               UserName = r.tbl_User.FirstName + " " + r.tbl_User.LastName,
                               SubCategoryId = s.SubCategoryId,
                               Image = r.Image,
                               Address = r.Address,
                               CreaterUniqueId = r.tbl_User.UniqueId,
                               Description = r.Description,
                               Title = r.Title,
                               CityName = r.tbl_State.City,
                               ZipName = r.tbl_State.zip,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
                }
                else
                {
                    lst = (from r in db.tbl_Jobs
                           join s in db.tbl_Job_SubCategory_Mapping on r.Id equals s.JobId
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                              // (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                              (CityId != 0 ? locations.Contains(r.CityId.Value) : CityId == 0) &&
                               (SubCategoryId != 0 ? s.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           && r.JobStatus == (int)HelperEnums.JobStatus.Open
                               )
                           select new Jobs
                           {
                               IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                               IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (int)HelperEnums.FlagType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               CreaterUniqueId = r.tbl_User.UniqueId,
                               UserName = r.tbl_User.FirstName + " " + r.tbl_User.LastName,
                               Image = r.Image,
                               Address = r.Address,
                               SubCategoryId = s.SubCategoryId,
                               Description = r.Description,
                               Title = r.Title,
                               CityName = r.tbl_State.City,
                               ZipName = r.tbl_State.zip,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).ToList();
                }
                lst = lst.Select((r, index) => new Jobs
                {
                    IsFavorite = r.IsFavorite,
                    IsFlag = r.IsFlag,
                    Id = r.Id,
                    CityId = r.CityId,
                    UserId = r.UserId,
                    UserName = r.UserName,
                    Image = r.Image,
                    SubCategoryId = r.SubCategoryId,
                    Description = r.Description,
                    Title = r.Title,
                    CreaterUniqueId = r.CreaterUniqueId,
                    Address = r.Address,
                    CityName = r.CityName,
                    ZipName = r.ZipName,
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
                tbl_Jobs obj = db.tbl_Jobs.FirstOrDefault(r => (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           ));
                if (obj != null)
                {
                    Id = obj.Id;
                    CityId = obj.CityId;
                    UserId = obj.UserId;
                    UserName = obj.tbl_User.FirstName + " " + obj.tbl_User.LastName;
                    Image = obj.Image;
                    Address = obj.Address;
                    Description = obj.Description;
                    ZipName = obj.tbl_State.zip;
                    Title = obj.Title;
                    CityName = obj.tbl_State.City;
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



        public JobListWrapper GetJobsByUserID(int jobStatus, long userID, int pageIndex, int pageSize)
        {
            var countJobs = db.tbl_Jobs.Count(j => j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.JobStatus == jobStatus);

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countJobs) / Convert.ToDecimal(pageSize));


            var query = (from j in db.tbl_Jobs
                         join c in db.tbl_State
                         on j.CityId equals c.Id

                         where j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.JobStatus == jobStatus

                         select new Job
                         {
                             Title = j.Title,
                             Address = j.Address,
                             CityID = j.CityId,
                             CityName = c.City + " " + c.State,
                             Description = j.Description,
                             Id = j.Id,
                             Image = j.Image,
                             IsDeleted = j.IsDeleted,
                             IsApprovedByAdmin = j.IsApprovedByAdmin,
                             CreatedDate = j.CreatedDate,
                             JobStatus = j.JobStatus,
                             JobQuoteCount = j.tbl_Quotes.Count(p => p.Status == (int)HelperEnums.QuoteStatus.Nothing),
                             SubCategoryName = (from s in db.tbl_SubCategory
                                                join m in db.tbl_Job_SubCategory_Mapping
                                                on s.Id equals m.SubCategoryId
                                                where m.JobId == j.Id
                                                select s.Name).ToList()


                         }
                        ).OrderByDescending(p => p.Id).Skip(pageSize * pageIndex).Take(pageSize);
            var data = query.ToList();
            //data.ForEach(p=>p.)
            JobListWrapper jobs = new JobListWrapper();
            jobs.JobCount = countJobs;
            if (totalpages <= (pageIndex + 1))
            {
                jobs.HideShowMore = true;
            }
            else
            {
                jobs.HideShowMore = false;
            }
            jobs.JobCount = countJobs;
            jobs.Jobs = data;
            return jobs;
        }

        public ResultData ChangeJobStatus(int jobStatus, long jobID, long professionalID=0)
        {
            var job = db.tbl_Jobs.FirstOrDefault(p => p.Id == jobID && p.JobStatus != jobStatus);
            if (job != null)
            {
                if (jobStatus == (int)HelperEnums.JobStatus.Done && professionalID > 0)
                {
                    job.ProfessionalID = professionalID;
                    job.UpdatedDate = DateTime.UtcNow;
                }
                job.JobStatus = jobStatus;
                db.SaveChanges();
              return  new ResultData { ResultDescription = "Status Changed", ResultStatus = 1 };
            }
            if (jobStatus == (int)HelperEnums.JobStatus.Open)
            {
                Quote quote = new Quote();
                quote.ChangeAllQuotesStatusToPendingByJobID(jobID);
            }
          return  new ResultData { ResultDescription = "Job not found", ResultStatus = 0 };
        }

        public Job GetJobByUserIDAndJobID(long userID, long jobID)
        {
            var jobData = (from j in db.tbl_Jobs
                           join c in db.tbl_State
                           on j.CityId equals c.Id

                           where j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.Id == jobID

                           select new Job
                           {
                               Title = j.Title,
                               Address = j.Address,
                               CityID = j.CityId,
                               CityName = c.City + " " + c.State,
                               Description = j.Description,
                               Id = j.Id,
                               Image = j.Image,
                               IsDeleted = j.IsDeleted,
                               IsApprovedByAdmin = j.IsApprovedByAdmin,
                               CreatedDate = j.CreatedDate,
                               JobStatus = j.JobStatus,
                               SubCategoryName = (from s in db.tbl_SubCategory
                                                  join m in db.tbl_Job_SubCategory_Mapping
                                                  on s.Id equals m.SubCategoryId
                                                  where m.JobId == j.Id
                                                  select s.Name).ToList()


                           }).FirstOrDefault();
            return jobData;

        }

        #region API Methods
        public JobWithEstimateDataContract GetJobByJobID(long jobID, long userID)
        {


            var data = (from j in db.tbl_Jobs
                        join c in db.tbl_State
                        on j.CityId equals c.Id
                        where j.Id == jobID && j.IsApprovedByAdmin == true && j.IsDeleted == false
                        let q = j.tbl_Quotes.FirstOrDefault(p => p.ProfessionalID == userID)
                        select new JobWithEstimateDataContract
                        {
                            IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == j.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job && p.UserID == userID) > 0 ? true : false,
                            IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == j.Id && p.FlagType == (int)HelperEnums.FlagType.Job && p.UserID == userID) > 0 ? true : false,
                            UserID = j.UserId,
                            

                            Estimate = q != null ? q.Estimate : 0,
                            Comment = q != null ? q.Comments : "",
                            QuoteID = q != null ? q.ID : 0,
                            Id = j.Id,
                            UserImage = !string.IsNullOrEmpty(j.tbl_User.Image) ? Common.UserImagesPath + j.tbl_User.Image : Common.NoImageIcon,
                            UserName = j.tbl_User.FirstName + " " + j.tbl_User.LastName,
                            Image = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage,
                            Description = j.Description,
                            Address = j.Address,
                            Title = j.Title,
                            CityName = j.tbl_State.City,
                            ZipName = j.tbl_State.State,
                            LocationID=j.CityId,
                            CreatedDate = j.CreatedDate,
                            JobStatus = j.JobStatus,
                          
                            SubcategoryNames = (from s in db.tbl_SubCategory
                                                join m in db.tbl_Job_SubCategory_Mapping
                                                on s.Id equals m.SubCategoryId
                                                where m.JobId == j.Id
                                                select new ProfessionalUrls { SubCategoryID = s.Id, SubCategoryName = s.Name }).ToList(),
                            // QuoteCount = j.tbl_Quotes.Count(p => p.Status == (int)HelperEnums.QuoteStatus.Nothing),
                        }
                        ).FirstOrDefault();

            return data;
        }
        public RecentJobDataContract GetUserRecentJob(long userID)
        {


            var recentJob = (from j in db.tbl_Jobs
                             join c in db.tbl_State
                             on j.CityId equals c.Id

                             where j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.JobStatus == (int)HelperEnums.JobStatus.Open

                             select new RecentJobDataContract
                             {
                                 ID = j.Id,
                                
                                 Image = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage,
                                 Description = j.Description,
                                 Address = j.Address,
                                 Title = j.Title,
                                 Location = j.tbl_State.City + ", " + j.tbl_State.State,
                                 CreatedDate = j.CreatedDate,
                                 JobStatus = j.JobStatus,

                             }
                        ).OrderByDescending(p => p.ID).FirstOrDefault();

            return recentJob;

        }
        public UserJobDataContractList GetUserJobs(int jobStatus, long userID, int pageIndex, int pageSize, long? loggedInUserID)
        {
            var countJobs = db.tbl_Jobs.Count(j => j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.JobStatus == jobStatus);

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countJobs) / Convert.ToDecimal(pageSize));


            var query = (from j in db.tbl_Jobs
                         join c in db.tbl_State
                         on j.CityId equals c.Id

                         where j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.JobStatus == jobStatus

                         select new UserJobDataContract
                         {
                             Id = j.Id,
                             UserID = j.UserId,
                             LoggedInUserID = loggedInUserID,
                             UserImage = !string.IsNullOrEmpty(j.tbl_User.Image) ? Common.UserImagesPath + j.tbl_User.Image : Common.NoImageIcon,
                             UserName = j.tbl_User.FirstName + " " + j.tbl_User.LastName,
                             Image = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage,
                             Description = j.Description,
                             Address = j.Address,
                             Title = j.Title,
                             CityName = j.tbl_State.City,
                             ZipName = j.tbl_State.State,
                             IsFavorite = db.tbl_Favorite.Count(p => p.UserID == loggedInUserID && p.FavoriteTypeID == j.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job) > 0 ? true : false,
                             IsFlag = db.tbl_Flag.Count(p => p.UserID == loggedInUserID && p.FlagTypeID == j.Id && p.FlagType == (int)HelperEnums.FlagType.Job) > 0 ? true : false,
                             CreatedDate = j.CreatedDate,
                             JobStatus = j.JobStatus,
                             //SubcategoryNames = (from s in db.tbl_SubCategory
                             //                    join m in db.tbl_Job_SubCategory_Mapping
                             //                    on s.Id equals m.SubCategoryId
                             //                    where m.JobId == j.Id
                             //                    select s.Name).ToList(),
                             //QuoteCount = j.tbl_Quotes.Count(p => p.Status == (int)HelperEnums.QuoteStatus.Nothing),
                         }
                        ).OrderByDescending(p => p.Id).Skip(pageSize * pageIndex).Take(pageSize);
            var data = query.ToList();
            //data.ForEach(p=>p.)
            UserJobDataContractList jobs = new UserJobDataContractList();
            jobs.JobCount = countJobs;
            if (totalpages <= (pageIndex + 1))
            {
                jobs.HideShowMore = true;
            }
            else
            {
                jobs.HideShowMore = false;
            }
            jobs.JobCount = countJobs;
            jobs.Jobs = data;
            return jobs;
        }
        public UserJobDataContractList GetUserJobs(int jobStatus, long userID, int pageIndex, int pageSize)
        {
            var countJobs = db.tbl_Jobs.Count(j => j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.JobStatus == jobStatus);

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countJobs) / Convert.ToDecimal(pageSize));


            var query = (from j in db.tbl_Jobs
                         join c in db.tbl_State
                         on j.CityId equals c.Id

                         where j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.JobStatus == jobStatus

                         select new UserJobDataContract
                         {
                             Id = j.Id,
                             UserID = j.UserId,
                             LocationID=j.CityId,
                             UserImage = !string.IsNullOrEmpty(j.tbl_User.Image) ? Common.UserImagesPath + j.tbl_User.Image : Common.NoImageIcon,
                             UserName = j.tbl_User.FirstName + " " + j.tbl_User.LastName,
                             Image = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage,
                             Description = j.Description,
                             Address = j.Address,
                             Title = j.Title,
                             CityName = j.tbl_State.City,
                             ZipName = j.tbl_State.State,

                             CreatedDate = j.CreatedDate,
                             JobStatus = j.JobStatus,
                             SubcategoryNames = (from s in db.tbl_SubCategory
                                                 join m in db.tbl_Job_SubCategory_Mapping
                                                 on s.Id equals m.SubCategoryId
                                                 where m.JobId == j.Id
                                                 select new ProfessionalUrls {SubCategoryID=s.Id,SubCategoryName= s.Name }).ToList(),
                             QuoteCount = j.tbl_Quotes.Count(p => p.Status == (int)HelperEnums.QuoteStatus.Nothing),
                             NewQuoteCount= j.tbl_Quotes.Count(p => p.IsViewed==false),
                         }
                        ).OrderByDescending(p => p.Id).Skip(pageSize * pageIndex).Take(pageSize);
            var data = query.ToList();
            //data.ForEach(p=>p.)
            UserJobDataContractList jobs = new UserJobDataContractList();
            jobs.JobCount = countJobs;
            if (totalpages <= (pageIndex + 1))
            {
                jobs.HideShowMore = true;
            }
            else
            {
                jobs.HideShowMore = false;
            }
            jobs.JobCount = countJobs;
            jobs.Jobs = data;
            return jobs;
        }
        public UserJobDataContractList GetJobArchive(int jobStatus, long userID, int pageIndex, int pageSize)
        {
            var countJobs = db.tbl_Jobs.Count(j => j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.JobStatus == jobStatus);

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countJobs) / Convert.ToDecimal(pageSize));


            var query = (from j in db.tbl_Jobs
                         join c in db.tbl_State
                         on j.CityId equals c.Id

                         where j.ProfessionalID == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false && j.JobStatus == jobStatus
                         &&DbFunctions.DiffDays(j.UpdatedDate,DateTime.UtcNow)>30
                         select new UserJobDataContract
                         {
                             Id = j.Id,
                             UserID = j.UserId,
                             UserEmail=j.tbl_User.Email,
                             UserPhone=j.tbl_User.PhoneNumber,
                             UserImage = !string.IsNullOrEmpty(j.tbl_User.Image) ? Common.UserImagesPath + j.tbl_User.Image : Common.NoImageIcon,
                             UserName = j.tbl_User.FirstName + " " + j.tbl_User.LastName,
                             Image = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage,
                             Description = j.Description,
                             Address = j.Address,
                             Title = j.Title,
                             CityName = j.tbl_State.City,
                             ZipName = j.tbl_State.State,

                             CreatedDate = j.CreatedDate,
                             JobStatus = j.JobStatus,
                            LocationID=j.CityId,
                             SubcategoryNames = (from s in db.tbl_SubCategory
                                                 join m in db.tbl_Job_SubCategory_Mapping
                                                 on s.Id equals m.SubCategoryId
                                                 where m.JobId == j.Id
                                                 select new ProfessionalUrls { SubCategoryID = s.Id, SubCategoryName = s.Name }).ToList(),
                             QuoteCount = j.tbl_Quotes.Count(p => p.Status == (int)HelperEnums.QuoteStatus.Nothing),
                         }
                        ).OrderByDescending(p => p.Id).Skip(pageSize * pageIndex).Take(pageSize);
            var data = query.ToList();
            //data.ForEach(p=>p.)
            UserJobDataContractList jobs = new UserJobDataContractList();
            jobs.JobCount = countJobs;
            if (totalpages <= (pageIndex + 1))
            {
                jobs.HideShowMore = true;
            }
            else
            {
                jobs.HideShowMore = false;
            }
            jobs.JobCount = countJobs;
            jobs.Jobs = data;
            return jobs;
        }
        public JobDataContractList GetAllJobs(long loggedInUserID, int pageIndex, int pageSize)
        {

            var Query = (from j in db.tbl_Jobs
                         join s in db.tbl_Job_SubCategory_Mapping on j.Id equals s.JobId
                         where j.JobStatus == (int)HelperEnums.JobStatus.Open
                         select new JobDataContract
                         {
                             IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == j.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                             IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == j.Id && p.FlagType == (int)HelperEnums.FlagType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                             UserID = j.UserId,
                             Id = j.Id,
                             UserImage = !string.IsNullOrEmpty(j.tbl_User.Image) ? Common.UserImagesPath + j.tbl_User.Image : Common.NoImageIcon,
                             UserName = j.tbl_User.FirstName + " " + j.tbl_User.LastName,
                             Image = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage,
                             Description = j.Description,
                             Address = j.Address,
                             Title = j.Title,
                             CityName = j.tbl_State.City,
                             ZipName = j.tbl_State.zip,
                             CreatedDate = j.CreatedDate,
                         }).Distinct();
            var count = Query.Count();

            var data = Query.OrderByDescending(p => p.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            JobDataContractList list = new JobDataContractList();
            list.Jobs = data;
            list.totalCount = count;
            return list;
        }
        public JobDataContractList Search(long loggedInUserID, List<long?> subcategoryIDs, long locationId, int pageIndex, int pageSize)
        {

            List<long> locations = null;
            if (CityId.HasValue)
            {
                locations = new States().GetLocationIDsNearBy(locationId);
            }



            var Query = (from j in db.tbl_Jobs
                         join s in db.tbl_Job_SubCategory_Mapping on j.Id equals s.JobId
                         where subcategoryIDs.Contains(s.SubCategoryId)
                         //&& j.CityId == locationId
                         && locations.Contains(j.CityId.Value)
                         && j.JobStatus == (int)HelperEnums.JobStatus.Open
                         select new JobDataContract
                         {
                             IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == j.Id && p.Favoritetype == (int)HelperEnums.FavoriteType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                             IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == j.Id && p.FlagType == (int)HelperEnums.FlagType.Job && p.UserID == loggedInUserID) > 0 ? true : false,
                             UserID = j.UserId,
                             Id = j.Id,
                             UserImage = !string.IsNullOrEmpty(j.tbl_User.Image) ? Common.UserImagesPath + j.tbl_User.Image : Common.NoImageIcon,
                             UserName = j.tbl_User.FirstName + " " + j.tbl_User.LastName,
                             Image = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage,
                             Description = j.Description,
                             Address = j.Address,
                             Title = j.Title,
                             CityName = j.tbl_State.City,
                             ZipName = j.tbl_State.zip,
                             CreatedDate = j.CreatedDate,
                         }).Distinct();
            var count = Query.Count();

            var data = Query.OrderByDescending(p => p.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            JobDataContractList list = new JobDataContractList();
            list.Jobs = data;
            list.totalCount = count;
            return list;
        }
        public ResultData Save(string title, string description, string image, long locationID, string address, long jobID, long userID, List<long> subcategoryIDs)
        {
            tbl_Jobs tbljob = new tbl_Jobs();
            if (jobID > 0)
            {
                tbljob = db.tbl_Jobs.FirstOrDefault(p => p.Id == jobID);
            }

            tbljob.Id = jobID;
            tbljob.Image = "";
            tbljob.IsApprovedByAdmin = true;
            tbljob.IsDeleted = false;
            tbljob.UserId = userID;
            tbljob.Title = title;
            tbljob.Description = description;
            tbljob.CityId = locationID;
            tbljob.Address = address;
            tbljob.CreatedDate = DateTime.UtcNow;
            tbljob.UpdatedDate = DateTime.UtcNow;
            tbljob.JobStatus = (int)HelperClasses.HelperEnums.JobStatus.Open;
            if (!string.IsNullOrEmpty(image))
            {
                var ImageName = DateTime.Now.ToFileTimeUtc() + ".jpg";
                File.WriteAllBytes(HttpContext.Current.Server.MapPath(Common.JobImagesPath) + ImageName, Convert.FromBase64String(image));

                tbljob.Image = ImageName;
            }
            if (jobID == 0)
            {
                db.tbl_Jobs.Add(tbljob);
            }
            db.SaveChanges();

            if (tbljob.Id > 0)
            {
                var data = new JobSubCategoryMapping().DeleteByJobID(tbljob.Id);
                foreach (var item in subcategoryIDs)
                {
                    JobSubCategoryMapping jobmapping = new JobSubCategoryMapping();
                    jobmapping.JobId = tbljob.Id;
                    jobmapping.SubCategoryId = item;
                    jobmapping.UserId = userID;
                    jobmapping.IsApprovedByAdmin = true;
                    jobmapping.Add(jobmapping);

                }


                if (jobID == 0)
                {
                    Task.Run(async () =>
                {

                    States state = new States();
                    state.Id = locationID;
                    var cityData = state.GetRecord();

                    SubCategory subcategory = new SubCategory();
                    var subCategoryNames = subcategory.GetSubcategoryNamesStringByIDs(subcategoryIDs);

                    User user = new User();
                    var proData = user.SearchProfessionalWithLocationForPushNotification(locationID, subcategoryIDs);
                    var jsonString = "{\"jobID\":\"" + tbljob.Id + "\"}";
                    foreach (var item in proData)
                    {

                        WindowAzurePushNotification.NotificationsForPro pro = new WindowAzurePushNotification.NotificationsForPro();
                        {
                            await pro.SendWindowAzurePushNotification(item.Platform, Convert.ToString(item.Id), "A new job has been posted under " + subCategoryNames.Remove(subCategoryNames.Length - 1) + " in " + state.City, HelperEnums.PushNotificationType.NewJob, jsonString);
                        }
                    }

                });
                }

                return new ResultData { ResultDescription = "Success", ResultStatus = 1 };
            }
            else
            {
                return new ResultData { ResultDescription = "Fail", ResultStatus = 0 };
            }
        }
        #endregion
    }
}
