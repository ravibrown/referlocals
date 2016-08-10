using AutoMapper;
using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class HomeVideo:dbContext
    {
        //IMapper mapper;
        //public HomeVideo()
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

        private string _Video = string.Empty;
        public string Video
        {
            get { return _Video; }
            set { _Video = value; }
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

        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }

        #endregion

        #region "Add Delete Update"
        public bool Add(HomeVideo obj)
        {
            try
            {
                tbl_HomeVideo checkobj = db.tbl_HomeVideo.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {
                    //AutoMapper.Mapper.CreateMap<HomeVideo, tbl_HomeVideo>();
                    tbl_HomeVideo newobj = Mapper.Map<HomeVideo, tbl_HomeVideo>(obj);
                    db.tbl_HomeVideo.Add(newobj);
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

        public bool Edit(HomeVideo obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<HomeVideo, tbl_HomeVideo>();
                tbl_HomeVideo checkobj = db.tbl_HomeVideo.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<HomeVideo, tbl_HomeVideo>(obj, checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(HomeVideo obj)
        {
            try
            {
                tbl_HomeVideo checkobj = db.tbl_HomeVideo.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_HomeVideo.Remove(checkobj);
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

        public List<HomeVideo> GetAll()
        {
            List<HomeVideo> lst = new List<HomeVideo>();
            try
            {
                lst = (from r in db.tbl_HomeVideo
                       where (
                           (Id != 0 ? r.Id == Id : Id == 0) &&
                           (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                           (!String.IsNullOrEmpty(Video) ? r.Video.ToLower() == Video.ToLower() : String.IsNullOrEmpty(Video)) &&
                           (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                       )
                       select new HomeVideo
                       {
                           Id = r.Id,
                           Video = r.Video,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           CreatedBy = r.CreatedBy,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                       }).ToList();
                lst = lst.Select((r, index) => new HomeVideo
                {
                    Id = r.Id,
                    Video = r.Video,
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
                tbl_HomeVideo obj = db.tbl_HomeVideo.FirstOrDefault(r => (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (!String.IsNullOrEmpty(Video) ? r.Video.ToLower() == Video.ToLower() : String.IsNullOrEmpty(Video)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           ));
                if (obj != null)
                {
                    Id = obj.Id;
                    Video = obj.Video;
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
