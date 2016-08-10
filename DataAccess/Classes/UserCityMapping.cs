using AutoMapper;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class UserCityMapping : dbContext
    {
        //IMapper mapper;
        //public UserCityMapping()
        //{
        //    mapper = new AutoMapperWebConfiguration().mapper;
        //}

        #region"properties"

        public string State { get; set; }
        public string Zip { get; set; }

        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
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
        public bool Add(UserCityMapping obj)
        {
            try
            {
                tbl_User_City_Mapping checkobj = db.tbl_User_City_Mapping.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {
                    //AutoMapper.Mapper.CreateMap<UserCityMapping, tbl_User_City_Mapping>();
                    tbl_User_City_Mapping newobj = Mapper.Map<UserCityMapping, tbl_User_City_Mapping>(obj);
                    db.tbl_User_City_Mapping.Add(newobj);
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

        public bool Edit(UserCityMapping obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<UserCityMapping, tbl_User_City_Mapping>();
                tbl_User_City_Mapping checkobj = db.tbl_User_City_Mapping.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<UserCityMapping, tbl_User_City_Mapping>(obj, checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(UserCityMapping obj)
        {
            try
            {
                tbl_User_City_Mapping checkobj = db.tbl_User_City_Mapping.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_User_City_Mapping.Remove(checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }
        public bool DeleteCityMappingByUserIdAndCityID(Int64 UserId, long cityID)
        {

            var checkobj = db.tbl_User_City_Mapping.FirstOrDefault(a => a.UserId == UserId && a.CityId == cityID);
            if (checkobj != null)
            {

                db.tbl_User_City_Mapping.Remove(checkobj);
                db.SaveChanges();
                return true;
            }


            return false;
        }

        public bool DeleteListByUserId(Int64 UserId)
        {
            try
            {

                List<tbl_User_City_Mapping> checkobj = db.tbl_User_City_Mapping.Where(a => a.UserId == UserId).ToList();
                if (checkobj != null && checkobj.Count > 0)
                {
                    foreach (var item in checkobj)
                    {
                        db.tbl_User_City_Mapping.Remove(item);
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

        public List<UserCityMapping> GetAll()
        {
            List<UserCityMapping> lst = new List<UserCityMapping>();
            try
            {
                if (Take != 0 && Index == 0)
                {
                    lst = (from r in db.tbl_User_City_Mapping
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new UserCityMapping
                           {
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               CityName = r.tbl_State.City,
                               State = r.tbl_State.State,
                               Zip = r.tbl_State.zip,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).Take(Convert.ToInt32(Take)).ToList();

                }
                else if (Take != 0 && Index != 0)
                {
                    lst = (from r in db.tbl_User_City_Mapping
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new UserCityMapping
                           {
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               CityName = r.tbl_State.City,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               State = r.tbl_State.State,
                               Zip = r.tbl_State.zip,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
                }
                else
                {
                    lst = (from r in db.tbl_User_City_Mapping
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new UserCityMapping
                           {
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               CityName = r.tbl_State.City,
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               State = r.tbl_State.State,
                               Zip = r.tbl_State.zip,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).ToList();
                }
                lst = lst.Select((r, index) => new UserCityMapping
                {
                    Id = r.Id,
                    CityId = r.CityId,
                    UserId = r.UserId,
                    CityName = r.CityName,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    State = r.State,
                    Zip = r.Zip,
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
                tbl_User_City_Mapping obj = db.tbl_User_City_Mapping.FirstOrDefault(r => (
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

        #region "DropDowns"
        public List<DropDownsCityZip> GetDropDownAllCitiesServe()
        {
            List<DropDownsCityZip> lst = new List<DropDownsCityZip>();
            lst = (from r in db.tbl_User_City_Mapping
                   where ((Id != 0 ? r.Id == Id : Id == 0) &&
                   (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                   (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)))
                   select new DropDownsCityZip
                   {
                       Id = r.CityId,
                       City = r.tbl_State.City,
                       Zip = r.tbl_State.zip,
                       State=r.tbl_State.State
                   }).GroupBy(g => new { g.City })
                         .Select(g => g.FirstOrDefault()).ToList();
            return lst;
        }


        #endregion

        #region API Methods
        public void AddCityIServe(long? userID, long cityID)
        {
                tbl_User_City_Mapping obj = new tbl_User_City_Mapping();
                obj.UserId = userID;
                obj.CityId = cityID;
                obj.IsApprovedByAdmin = true;
                obj.IsDeleted = false;
                obj.CreatedDate = DateTime.UtcNow;
                if (db.tbl_User_City_Mapping.Count(p => p.UserId == UserId && CityId == cityID) == 0)
                {
                    db.tbl_User_City_Mapping.Add(obj);
                    db.SaveChanges();
                }
            

        }
        public void AddCitiesIServe(long userID, List<long> CityIDs)
        {
            foreach (var ID in CityIDs)
            {
                tbl_User_City_Mapping obj = new tbl_User_City_Mapping();
                obj.UserId = userID;
                obj.CityId= ID;
                obj.IsApprovedByAdmin = true;
                obj.IsDeleted = false;
                obj.CreatedDate = DateTime.UtcNow;
                if (db.tbl_User_City_Mapping.Count(p => p.UserId == UserId && CityId == ID) == 0)
                {
                    db.tbl_User_City_Mapping.Add(obj);
                    db.SaveChanges();
                }
            }

        }
        public List<PropStates> GetCitiesIServe(long userID)
        {
            List<PropStates> lst = new List<PropStates>();
            lst = (from r in db.tbl_User_City_Mapping
                   where r.UserId == userID 
                   
                   select new PropStates
                   {
                       Id = r.CityId,
                       City = r.tbl_State.City,
                       Zip = r.tbl_State.zip,
                       State=r.tbl_State.State
                   }).GroupBy(g => new { g.City })
                         .Select(g => g.FirstOrDefault()).ToList();
            return lst;
        }
        public int CheckCityIServe(long userID,long cityID)
        {

            return db.tbl_User_City_Mapping.Count(p => p.CityId == cityID && p.UserId == userID); 
                  
            
        }
        #endregion


    }
}
