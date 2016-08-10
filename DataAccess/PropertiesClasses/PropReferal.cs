using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class ReferalListWrapper
    {
        public List<PropReferal> Referals { get; set; }
        public int ReferalCount { get; set; }
        public bool HideShowMore { get; set; }
    
    }
    public class PropReferal 
    {
        public string ProfessionalName { get; set; }
        #region"properties"
        public string SenderUniqueId { get; set; }
        public string UserUniqueId { get; set; }
        public long? SenderType { get; set; }
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

        private Int64? _UserCityId = 0;
        public Int64? UserCityId
        {
            get { return _UserCityId; }
            set { _UserCityId = value; }
        }

        private string _CityName = string.Empty;
        public string CityName
        {
            get { return _CityName; }
            set { _CityName = value; }
        }

        private string _Comment = string.Empty;
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        private string _UserName = string.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _SenderName = string.Empty;
        public string SenderName
        {
            get { return _SenderName; }
            set { _SenderName = value; }
        }

        private string _UserCityName = string.Empty;
        public string UserCityName
        {
            get { return _UserCityName; }
            set { _UserCityName = value; }
        }

        private string _UserZipName = string.Empty;
        public string UserZipName
        {
            get { return _UserZipName; }
            set { _UserZipName = value; }
        }

        private string _Zip = string.Empty;
        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }

        private Int64? _UserId = 0;
        public Int64? UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private Int64? _SenderId = 0;
        public Int64? SenderId
        {
            get { return _SenderId; }
            set { _SenderId = value; }
        }

        private bool _IsApprovedByAdmin = false;
        public bool IsApprovedByAdmin
        {
            get { return _IsApprovedByAdmin; }
            set { _IsApprovedByAdmin = value; }
        }

        private bool _IsSatisfied = false;
        public bool IsSatisfied
        {
            get { return _IsSatisfied; }
            set { _IsSatisfied = value; }
        }

        private bool _IsRefered = false;
        public bool IsRefered
        {
            get { return _IsRefered; }
            set { _IsRefered = value; }
        }

        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private bool _IsFlag = false;
        public bool IsFlag
        {
            get { return _IsFlag; }
            set { _IsFlag = value; }
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
        public string CreatedDateString { get { return CreatedDate.GetValueOrDefault().ToShortDateString(); } }
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
      
    }
}
