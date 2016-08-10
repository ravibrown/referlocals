using DataAccess.Classes;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContractClasses
{
    
    public class ProfessionalDataContract:ResultData
    { //#region "Properties"
      //public string Password { get; set; }

        public string SocialID { get; set; }
        public HelperEnums.SocialType SocialType { get; set; }
        //public bool IsDeleted { get; set; }
        public long? ThreadID { get; set; }
        public NotificationSettingDataContract notificationSetting { get; set; }
        public string Platform { get; set; }

        public string DeviceToken { get; set; }

        public string CityName { get; set; }
        public string ZipName { get; set; }
        public string StateName { get; set; }
        public string ShareUrl { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsFlag { get; set; }
        public bool? IsProfileUpdated { get; set; }
        public string ProfileUrl { get; set; }
        //Id

        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //FirstName
        private string _FirstName = string.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        //LastName
        private string _LastName = string.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }


        //Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //PhoneNumber
        private string _PhoneNumber = string.Empty;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }



        //VerificationCode
        private string _VerificationCode = string.Empty;
        public string VerificationCode
        {
            get { return _VerificationCode; }
            set { _VerificationCode = value; }
        }

        //UniqueId
        private string _UniqueId = string.Empty;
        public string UniqueId
        {
            get { return _UniqueId; }
            set { _UniqueId = value; }
        }

        //CountryCode
        private Int64? _CountryCode = 0;

        public Int64? CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }

        //Type
        private Int64? _Type = (int)HelperEnums.UserType.User;
        public Int64? Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //RoleId
        private Int64? _RoleId = (int)HelperEnums.Role.User;

        public Int64? RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }


        
        private string _StreetAddress = string.Empty;

        public string StreetAddress
        {
            get { return _StreetAddress; }
            set { _StreetAddress = value; }
        }

        
        private string _Appartment = string.Empty;
        public string Appartment
        {
            get { return _Appartment; }
            set { _Appartment = value; }
        }

        public long? CityId { get; set; }

        //IsApprovedByAdmin
        private bool? _IsApprovedByAdmin = false;

        public bool? IsApprovedByAdmin
        {
            get { return _IsApprovedByAdmin; }
            set { _IsApprovedByAdmin = value; }
        }

        //IsVerified
        private bool? _IsVerified = false;

        public bool? IsVerified
        {
            get { return _IsVerified; }
            set { _IsVerified = value; }
        }
        public string Website { get; set; }
        public string CompanyName { get; set; }
        public string WorkLocation { get; set; }
        public List<PropStates>  CitiesIServe { get; set; }
        public List<ProfessionalUrls> ProfessionalUrls { get; set; }
        public  int ReferralCount{ get; set; }
    }

}
