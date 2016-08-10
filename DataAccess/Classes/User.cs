using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataAccess.WindowAzurePushNotification;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess.Classes
{
    public class User : dbContext
    {
        //IMapper mapper;
        //public User()
        //{
        //    mapper = new AutoMapperWebConfiguration().mapper;
        //}
        public string SocialID { get; set; }
        public HelperEnums.SocialType SocialType { get; set; }
        public string DeviceToken { get; set; }
        public string Platform { get; set; }
        public bool IsFlag { get; set; }

        public bool IsFavorite { get; set; }
        public List<long> CitiesIServeIds { get; set; }
        public List<DropDownsCityZip> CitiesIServe { get; set; }

        public List<ProfessionalUrls> ProfessionalUrls { get; set; }
        public int JobsPosted { get; set; }
        public int JobsDone { get; set; }
        public int JobsCancelled { get; set; }
        public int ReferalCount { get; set; }

        #region "Properties"
        public bool IsProfileUpdated { get; set; }
        public string ProfileUrl { get; set; }
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

        //Password
        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
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

        //SubCategoryId
        private Int64? _SubCategoryId = 0;
        public Int64? SubCategoryId
        {
            get { return _SubCategoryId; }
            set { _SubCategoryId = value; }
        }

        //Type
        private Int64? _Type = 0;
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

        //CompanyName
        private string _CompanyName = string.Empty;
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        //Website
        private string _Website = string.Empty;
        public string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }

        //CityName
        private string _CityName = string.Empty;
        public string CityName
        {
            get { return _CityName; }
            set { _CityName = value; }
        }

        //StateName
        private string _StateName = string.Empty;
        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }

        //ZipCode
        private string _ZipCode = string.Empty;
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        //StreetAddress
        private string _StreetAddress = string.Empty;
        public string StreetAddress
        {
            get { return _StreetAddress; }
            set { _StreetAddress = value; }
        }

        //Appartment
        private string _Appartment = string.Empty;
        public string Appartment
        {
            get { return _Appartment; }
            set { _Appartment = value; }
        }

        //CityId
        private Int64? _CityId = 0;
        public Int64? CityId
        {
            get { return _CityId; }
            set { _CityId = value; }
        }

        //StateId
        private Int64? _StateId = 0;
        public Int64? StateId
        {
            get { return _StateId; }
            set { _StateId = value; }
        }

        //ZipId
        private Int64? _ZipId = 0;
        public Int64? ZipId
        {
            get { return _ZipId; }
            set { _ZipId = value; }
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

        //IsVerified
        private bool _IsVerified = false;
        public bool IsVerified
        {
            get { return _IsVerified; }
            set { _IsVerified = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }


        public List<long?> SubCategoryIds { get; set; }

        #endregion

        #region "Add Delete Update"
        public bool Add(User obj)
        {
            try
            {
                tbl_User checkobj = db.tbl_User.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {


                    //Mapper.Map<User, tbl_User>(obj);

                    tbl_User newobj = Mapper.Map<User, tbl_User>(obj);
                    newobj.IsProfileUpdated = false;
                    db.tbl_User.Add(newobj);
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

        public bool Edit(User obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<User, tbl_User>();
                tbl_User checkobj = db.tbl_User.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {

                    checkobj = Mapper.Map<User, tbl_User>(obj, checkobj);
                    if (checkobj.Image == Common.NoImageIcon)
                    {
                        checkobj.Image = "";
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

        public bool Delete(User obj)
        {
            try
            {
                tbl_User checkobj = db.tbl_User.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_User.Remove(checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public void UpdateVerificationCode(string verificationCode, long userID)
        {
            tbl_User userData = db.tbl_User.FirstOrDefault(a => a.Id == userID);
            if (userData != null)
            {
                userData.VerificationCode = verificationCode;
                db.SaveChanges();
            }
        }

        #endregion

        #region "Get Method"
        public NewUserUpdates GetNewUserUpdates(long userID)
        {
            var result = (from p in new List<string> { "X" }
                         join m in db.tbl_Messages.Where(c=>c.IsViewed==false&&c.tbl_Threads.tbl_ThreadParticipants.FirstOrDefault(p=>p.UserID==userID)!=null&&c.SenderID!=userID)   on 1 equals 1 into msgs
                         join f in db.tbl_Favorite.Where(c => c.IsViewed == false&&c.Favoritetype==(int)(HelperEnums.FavoriteType.User)&&c.FavoriteTypeID==userID) on 1 equals 1 into foll
                         join e in db.tbl_Quotes.Where(c => c.IsViewed == false&&c.tbl_Jobs.UserId==userID) on 1 equals 1 into est
                         join a in db.tbl_Appointments.Where(c => c.IsViewed == false&&c.UserID==userID&&c.RescheduledByProfessional==true&&c.IsRescheduled==true) on 1 equals 1 into app
                         select new NewUserUpdates
                         {
                             NewMessages = msgs.Count(),
                             NewAppointments = app.Count(),
                             NewEstimates = est.Count(),
                             NewFollowers= foll.Count(),
                         }).FirstOrDefault();
            return result;
        }

        public NewProfessionalUpdates GetNewProfessionalUpdates(long userID)
        {
            var result = (from p in new List<string> { "X" }
                          join m in db.tbl_Messages.Where(c => c.IsViewed == false && c.tbl_Threads.tbl_ThreadParticipants.FirstOrDefault(p => p.UserID == userID) != null && c.SenderID != userID) on 1 equals 1 into msgs
                          join f in db.tbl_Favorite.Where(c => c.IsViewed == false && c.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) && c.FavoriteTypeID == userID) on 1 equals 1 into foll
                          join e in db.tbl_Referal.Where(c => c.IsViewed == false && c.UserId== userID) on 1 equals 1 into est
                          join a in db.tbl_Appointments.Where(c => c.IsViewed == false && c.ProfessionalID == userID && c.RescheduledByProfessional == false) on 1 equals 1 into app
                          select new NewProfessionalUpdates
                          {
                              NewMessages = msgs.Count(),
                              NewAppointments = app.Count(),
                              NewReferrals = est.Count(),
                              NewFollowers = foll.Count(),
                          }).FirstOrDefault();
            return result;

        }
        public List<User> GetAll()
        {
            var loggedInUserID = SessionService.Pull(SessionKeys.UserId);
            List<User> lst = new List<User>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_User
                       join state in db.tbl_State
                             on r.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           JobsDone = db.tbl_Jobs.Count(p => (r.Type == (int)HelperEnums.UserType.Professional ? p.ProfessionalID == r.Id : p.UserId == r.Id) && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),

                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (r.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (r.Type == 2 ? (int)(HelperEnums.FlagType.Professional) : (int)(HelperEnums.FlagType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           Email = r.Email,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           CityName = c != null ? c.City + " " + c.State : "",
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image != null ? r.Image : Common.NoImageIconWithoutPath,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_User
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           // JobsDone = db.tbl_Jobs.Count(p => p.ProfessionalID == r.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           JobsDone = db.tbl_Jobs.Count(p => (r.Type == (int)HelperEnums.UserType.Professional ? p.ProfessionalID == r.Id : p.UserId == r.Id) && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (r.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (r.Type == 2 ? (int)(HelperEnums.FlagType.Professional) : (int)(HelperEnums.FlagType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           Password = r.Password,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           Email = r.Email,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image,
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_User
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           //JobsDone = db.tbl_Jobs.Count(p => p.ProfessionalID == r.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           JobsDone = db.tbl_Jobs.Count(p => (r.Type == (int)HelperEnums.UserType.Professional ? p.ProfessionalID == r.Id : p.UserId == r.Id) && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (r.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (r.Type == 2 ? (int)(HelperEnums.FlagType.Professional) : (int)(HelperEnums.FlagType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           Password = r.Password,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           Email = r.Email,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new User
            {
                JobsDone = r.JobsDone,
                IsFlag = r.IsFlag,
                IsFavorite = r.IsFavorite,
                Id = r.Id,
                CompanyName = r.CompanyName,
                Password = r.Password,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Website = r.Website,
                VerificationCode = r.VerificationCode,
                Type = r.Type,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                StreetAddress = r.StreetAddress,
                CityId = r.CityId,
                StateId = r.StateId,
                ZipId = r.ZipId,
                Appartment = r.Appartment,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                IsVerified = r.IsVerified == true ? true : false,
                UniqueId = r.UniqueId,
                CountryCode = r.CountryCode,
                Image = !string.IsNullOrEmpty(r.Image) ? r.Image : Common.NoImageIconWithoutPath,
                SNO = index + 1
            }).ToList();

            return lst;
        }

        public List<User> GetAllUsers()
        {
            var loggedInUserID = SessionService.Pull(SessionKeys.UserId);
            List<User> lst = new List<User>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_User
                       join state in db.tbl_State
                             on r.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                         (Type != 0 ? r.Type == (long?)HelperEnums.UserType.User : r.Type == (long?)HelperEnums.UserType.Professional) &&

                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           JobsDone = db.tbl_Jobs.Count(p => (r.Type == (int)HelperEnums.UserType.Professional ? p.ProfessionalID == r.Id : p.UserId == r.Id) && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),

                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (r.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (r.Type == 2 ? (int)(HelperEnums.FlagType.Professional) : (int)(HelperEnums.FlagType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           Email = r.Email,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           CityName = c != null ? c.City + " " + c.State : "",
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image != null ? r.Image : Common.NoImageIconWithoutPath,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_User
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                         (Type != 0 ? r.Type == (long?)HelperEnums.UserType.User : r.Type == (long?)HelperEnums.UserType.Professional) &&

                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           // JobsDone = db.tbl_Jobs.Count(p => p.ProfessionalID == r.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           JobsDone = db.tbl_Jobs.Count(p => (r.Type == (int)HelperEnums.UserType.Professional ? p.ProfessionalID == r.Id : p.UserId == r.Id) && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (r.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (r.Type == 2 ? (int)(HelperEnums.FlagType.Professional) : (int)(HelperEnums.FlagType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           Password = r.Password,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           Email = r.Email,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image,
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_User
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                         (Type != 0 ? r.Type == (long?)HelperEnums.UserType.User : r.Type == (long?)HelperEnums.UserType.Professional) &&

                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           //JobsDone = db.tbl_Jobs.Count(p => p.ProfessionalID == r.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           JobsDone = db.tbl_Jobs.Count(p => (r.Type == (int)HelperEnums.UserType.Professional ? p.ProfessionalID == r.Id : p.UserId == r.Id) && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (r.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (r.Type == 2 ? (int)(HelperEnums.FlagType.Professional) : (int)(HelperEnums.FlagType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           Password = r.Password,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           Email = r.Email,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new User
            {
                JobsDone = r.JobsDone,
                IsFlag = r.IsFlag,
                IsFavorite = r.IsFavorite,
                Id = r.Id,
                CompanyName = r.CompanyName,
                Password = r.Password,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Website = r.Website,
                VerificationCode = r.VerificationCode,
                Type = r.Type,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                StreetAddress = r.StreetAddress,
                CityId = r.CityId,
                StateId = r.StateId,
                ZipId = r.ZipId,
                Appartment = r.Appartment,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                IsVerified = r.IsVerified == true ? true : false,
                UniqueId = r.UniqueId,
                CountryCode = r.CountryCode,
                Image = !string.IsNullOrEmpty(r.Image) ? r.Image : Common.NoImageIconWithoutPath,
                SNO = index + 1
            }).ToList();

            return lst;
        }

        public List<User> GetAllProfessionals()
        {
            var loggedInUserID = SessionService.Pull(SessionKeys.UserId);
            List<User> lst = new List<User>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_User
                       join state in db.tbl_State
                             on r.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (Type != 0 ? r.Type == (long?)HelperEnums.UserType.User : r.Type == (long?)HelperEnums.UserType.Professional) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           JobsDone = db.tbl_Jobs.Count(p => (r.Type == (int)HelperEnums.UserType.Professional ? p.ProfessionalID == r.Id : p.UserId == r.Id) && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),

                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (r.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (r.Type == 2 ? (int)(HelperEnums.FlagType.Professional) : (int)(HelperEnums.FlagType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           Email = r.Email,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           CityName = c != null ? c.City + " " + c.State : "",
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image != null ? r.Image : Common.NoImageIconWithoutPath,
                           ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == r.Id)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                           CitiesIServe = (from c in db.tbl_User_City_Mapping
                                           where c.UserId == r.Id
                                           select new DropDownsCityZip
                                           {
                                               Id = c.CityId,
                                               City = c.tbl_State.City,
                                               Zip = c.tbl_State.zip,
                                               State = c.tbl_State.State,
                                               CityName = c.tbl_State.City,
                                               StateName = c.tbl_State.State
                                           }).ToList(),
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_User
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                         (Type != 0 ? r.Type == (long?)HelperEnums.UserType.User : r.Type == (long?)HelperEnums.UserType.Professional) &&

                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           // JobsDone = db.tbl_Jobs.Count(p => p.ProfessionalID == r.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           JobsDone = db.tbl_Jobs.Count(p => (r.Type == (int)HelperEnums.UserType.Professional ? p.ProfessionalID == r.Id : p.UserId == r.Id) && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (r.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (r.Type == 2 ? (int)(HelperEnums.FlagType.Professional) : (int)(HelperEnums.FlagType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           Password = r.Password,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           Email = r.Email,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image,
                           ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == r.Id)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                           CitiesIServe = (from c in db.tbl_User_City_Mapping
                                           where c.UserId == r.Id
                                           select new DropDownsCityZip
                                           {
                                               Id = c.CityId,
                                               City = c.tbl_State.City,
                                               Zip = c.tbl_State.zip,
                                               State = c.tbl_State.State
                                           }).ToList(),
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_User
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&

                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                         (Type != 0 ? r.Type == (long?)HelperEnums.UserType.User : r.Type == (long?)HelperEnums.UserType.Professional) &&

                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           //JobsDone = db.tbl_Jobs.Count(p => p.ProfessionalID == r.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           JobsDone = db.tbl_Jobs.Count(p => (r.Type == (int)HelperEnums.UserType.Professional ? p.ProfessionalID == r.Id : p.UserId == r.Id) && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                           IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (r.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (r.Type == 2 ? (int)(HelperEnums.FlagType.Professional) : (int)(HelperEnums.FlagType.User)) && p.UserID == loggedInUserID) > 0 ? true : false,
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           Password = r.Password,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           Email = r.Email,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image,
                           ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == r.Id)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                           CitiesIServe = (from c in db.tbl_User_City_Mapping
                                           where c.UserId == r.Id
                                           select new DropDownsCityZip
                                           {
                                               Id = c.CityId,
                                               City = c.tbl_State.City,
                                               Zip = c.tbl_State.zip,
                                               State = c.tbl_State.State
                                           }).ToList(),
                       }).ToList();
            }
            lst = lst.Select((r, index) => new User
            {
                JobsDone = r.JobsDone,
                IsFlag = r.IsFlag,
                IsFavorite = r.IsFavorite,
                Id = r.Id,
                CompanyName = r.CompanyName,
                Password = r.Password,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Website = r.Website,
                VerificationCode = r.VerificationCode,
                Type = r.Type,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                StreetAddress = r.StreetAddress,
                CityId = r.CityId,
                StateId = r.StateId,
                ZipId = r.ZipId,
                Appartment = r.Appartment,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                IsVerified = r.IsVerified == true ? true : false,
                UniqueId = r.UniqueId,
                CountryCode = r.CountryCode,
                Image = !string.IsNullOrEmpty(r.Image) ? r.Image : Common.NoImageIconWithoutPath,
                SNO = index + 1,
                ProfessionalUrls = r.ProfessionalUrls,
                CitiesIServe = r.CitiesIServe,
            }).ToList();

            return lst;
        }
        public ResultData ChangePassword(string newPassword, string oldPassword, long userID)
        {
            var userdata = db.tbl_User.FirstOrDefault(p => p.Id == userID && p.Password == oldPassword);
            if (userdata != null)
            {
                userdata.Password = newPassword;
                db.SaveChanges();
                return new ResultData { ResultDescription = "Password Changed Successfully", ResultStatus = 1 };
            }
            else
            {
                return new ResultData { ResultDescription = "Incorrect Old Password", ResultStatus = 0 };
            }
        }

        public ResultData ResetPassword(string password, string verificationCode, long userID)
        {
            tbl_User user = db.tbl_User.FirstOrDefault(p => p.Id == userID && p.VerificationCode == verificationCode);
            if (user != null)
            {
                user.Password = password;
                db.SaveChanges();
                return new ResultData { ResultDescription = "Password Updated Successfully", ResultStatus = 1 };
            }
            return new ResultData { ResultDescription = "Invalid OTP Code", ResultStatus = 0 };
        }

        public List<User> GetAllWithJoin()
        {
            List<User> lst = new List<User>();
            if (Take != 0 && Index == 0)
            {
                lst = (from r in db.tbl_User
                       join s in db.tbl_User_SubCategory_Mapping on r.Id equals s.UserId
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (SubCategoryId != 0 ? s.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           Email = r.Email,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image != null ? r.Image : Common.NoImageIconWithoutPath,
                       }).Take(Convert.ToInt32(Take)).ToList();
            }
            else if (Take != 0 && Index != 0)
            {
                lst = (from r in db.tbl_User
                       join s in db.tbl_User_SubCategory_Mapping on r.Id equals s.UserId
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (SubCategoryId != 0 ? s.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           Password = r.Password,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           Email = r.Email,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image != null ? r.Image : Common.NoImageIconWithoutPath,
                       }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
            }
            else
            {
                lst = (from r in db.tbl_User
                       join s in db.tbl_User_SubCategory_Mapping on r.Id equals s.UserId
                       where ((Id != 0 ? r.Id == Id : Id == 0) &&
                       (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                       (SubCategoryId != 0 ? s.SubCategoryId == SubCategoryId : SubCategoryId == 0) &&
                       (CountryCode != 0 ? r.CountryCode == CountryCode : CountryCode == 0) &&
                       (IsDeleted ? r.IsDeleted == true : r.IsDeleted == false) &&
                       (!string.IsNullOrEmpty(PhoneNumber) ? r.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                       (!string.IsNullOrEmpty(Email) ? r.Email == Email : string.IsNullOrEmpty(Email)) &&
                       (!string.IsNullOrEmpty(Password) ? r.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                       (!string.IsNullOrEmpty(UniqueId) ? r.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both))

                       select new User
                       {
                           Id = r.Id,
                           CompanyName = r.CompanyName,
                           Password = r.Password,
                           FirstName = r.FirstName,
                           LastName = r.LastName,
                           Website = r.Website,
                           Email = r.Email,
                           VerificationCode = r.VerificationCode,
                           Type = r.Type,
                           PhoneNumber = r.PhoneNumber,
                           StreetAddress = r.StreetAddress,
                           CityId = r.CityId,
                           StateId = r.StateId,
                           ZipId = r.ZipId,
                           Appartment = r.Appartment,
                           CreatedBy = r.CreatedBy,
                           CreatedDate = r.CreatedDate,
                           UpdatedDate = r.UpdatedDate,
                           IsDeleted = r.IsDeleted == true ? true : false,
                           IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                           IsVerified = r.IsVerified == true ? true : false,
                           UniqueId = r.UniqueId,
                           CountryCode = r.CountryCode,
                           Image = r.Image != null ? r.Image : Common.NoImageIconWithoutPath,
                       }).ToList();
            }
            lst = lst.Select((r, index) => new User
            {
                Id = r.Id,
                CompanyName = r.CompanyName,
                Password = r.Password,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Website = r.Website,
                VerificationCode = r.VerificationCode,
                Type = r.Type,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                StreetAddress = r.StreetAddress,
                CityId = r.CityId,
                StateId = r.StateId,
                ZipId = r.ZipId,
                Appartment = r.Appartment,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedDate = r.UpdatedDate,
                IsDeleted = r.IsDeleted == true ? true : false,
                IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                IsVerified = r.IsVerified == true ? true : false,
                UniqueId = r.UniqueId,
                CountryCode = r.CountryCode,
                Image = !string.IsNullOrEmpty(r.Image) ? r.Image : Common.NoImageIconWithoutPath,
                SNO = index + 1
            }).ToList();

            return lst;
        }
        public bool SearchProfessionalHasRecords(string phone, string email)
        {
            var obj = db.tbl_User.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                   (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                   (CityId != 0 ? a.CityId == CityId : CityId == 0) &&
                   (
                       (
                        ((CountryCode != 0 && !string.IsNullOrEmpty(phone)) ? a.CountryCode == CountryCode : CountryCode == 0) &&
                           //(CountryCode != 0 ? a.CountryCode == CountryCode : CountryCode == 0) &&
                           (!string.IsNullOrEmpty(PhoneNumber) ? a.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber))
                       )
                       ||
                       (!string.IsNullOrEmpty(Email) ? a.Email == Email : string.IsNullOrEmpty(Email))
                    )
                   && a.Type == (int)HelperEnums.UserType.Professional &&
                   (!string.IsNullOrEmpty(Password) ? a.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                   (!string.IsNullOrEmpty(UniqueId) ? a.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                   (IsApproved == (int)HelperEnums.BooleanValues.Approved ?
                   a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ?
                   a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                   //&&a.Type==(int)HelperEnums.UserType.Professional
                   );
            //if (obj != null)
            //{
            //    Id = obj.Id;
            //    CompanyName = obj.CompanyName;
            //    Password = obj.Password;
            //    FirstName = obj.FirstName;
            //    LastName = obj.LastName;
            //    Website = obj.Website;
            //    VerificationCode = obj.VerificationCode;
            //    Type = obj.Type;
            //    Email = obj.Email;
            //    PhoneNumber = obj.PhoneNumber;
            //    StreetAddress = obj.StreetAddress;
            //    CityId = obj.CityId;
            //    StateId = obj.StateId;
            //    ZipId = obj.ZipId;
            //    Appartment = obj.Appartment;
            //    CreatedBy = obj.CreatedBy;
            //    CreatedDate = obj.CreatedDate;
            //    UpdatedDate = obj.UpdatedDate;
            //    IsDeleted = Convert.ToBoolean(obj.IsDeleted);
            //    IsApprovedByAdmin = Convert.ToBoolean(obj.IsApprovedByAdmin);
            //    IsVerified = Convert.ToBoolean(obj.IsVerified);
            //    UniqueId = obj.UniqueId;
            //    CountryCode = obj.CountryCode;
            //    Image = obj.Image;
            //    DataRecieved = true;
            //}
            if (obj > 0)
            {
                DataRecieved = true;
            }
            return DataRecieved;

        }
        public bool SearchProfessionalHasRecords(string countryCode, string phone, string email)
        {
            long? countryCodeLong = Convert.ToInt64(countryCode);
            var obj = db.tbl_User.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                   (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                   (CityId != 0 ? a.CityId == CityId : CityId == 0) &&
                   (
                       (
                           ((countryCodeLong != 0 && !string.IsNullOrEmpty(phone)) ? a.CountryCode == countryCodeLong : countryCodeLong == 0) &&
                           (!string.IsNullOrEmpty(phone) ? a.PhoneNumber.ToLower() == phone.ToLower() : !string.IsNullOrEmpty(phone))
                       )
                       ||
                       (!string.IsNullOrEmpty(email) ? a.Email == email : !string.IsNullOrEmpty(email))
                    )
                   && a.Type == (int)HelperEnums.UserType.Professional &&
                   (!string.IsNullOrEmpty(Password) ? a.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                   (!string.IsNullOrEmpty(UniqueId) ? a.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                   (IsApproved == (int)HelperEnums.BooleanValues.Approved ?
                   a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ?
                   a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                   //&&a.Type==(int)HelperEnums.UserType.Professional
                   );

            if (obj > 0)
            {
                DataRecieved = true;
            }
            return DataRecieved;

        }

        public List<User> SearchProfessionalWithLocationForPushNotification(long locationID, List<long> subCategoryIDs)
        {
            //var loggedInUserID = SessionService.Pull(SessionKeys.UserId);
            var locations = new States().GetLocationIDsNearBy(locationID);
            if (locations != null)
            {
                var data =
                    (from r in db.tbl_User
                     where
                     (
                         (from u in db.tbl_User
                          join c in db.tbl_User_City_Mapping
                          on u.Id equals c.UserId
                          //where (u.CityId == locationID || c.CityId == locationID)
                          where (locations.Contains(u.CityId.Value) || locations.Contains(c.CityId))
                              && (db.tbl_User_SubCategory_Mapping.Count(p => p.UserId == u.Id && subCategoryIDs.Contains(p.SubCategoryId.Value)) > 0)
                          select
                          u.Id
                          )
                          .Distinct()).Contains(r.Id)
                     select new User
                     {

                         Id = r.Id,
                         Platform = r.Platform,
                         DeviceToken = r.DeviceToken

                     }
                         ).ToList();
                return data;

            }
            return null;
        }
        public List<User> SearchProfessionalWithLocation(long locationID, long subCategoryID)
        {
            var loggedInUserID = SessionService.Pull(SessionKeys.UserId);
            var locations = new States().GetLocationIDsNearBy(locationID);
            if (locations != null)
            {
                var data =
                    (from r in db.tbl_User
                     where
                     (
                         (from u in db.tbl_User
                          join c in db.tbl_User_City_Mapping
                          on u.Id equals c.UserId
                          //where (u.CityId == locationID || c.CityId == locationID)
                          where (locations.Contains(u.CityId.Value) || locations.Contains(c.CityId))
                              && (db.tbl_User_SubCategory_Mapping.Count(p => p.UserId == u.Id && p.SubCategoryId == subCategoryID) > 0)
                          select
                          u.Id
                          )
                          .Distinct()).Contains(r.Id)
                     select new User
                     {
                         JobsDone = db.tbl_Jobs.Count(p => p.ProfessionalID == r.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                         Id = r.Id,
                         IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == r.Id && p.FlagType == (int)(HelperEnums.FlagType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                         IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == r.Id && p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                         CompanyName = r.CompanyName,
                         FirstName = r.FirstName,
                         LastName = r.LastName,
                         Website = r.Website,
                         VerificationCode = r.VerificationCode,
                         Type = r.Type,
                         Email = r.Email,
                         PhoneNumber = r.PhoneNumber,
                         StreetAddress = r.StreetAddress,
                         CityId = r.CityId,
                         StateId = r.StateId,
                         ZipId = r.ZipId,
                         Appartment = r.Appartment,
                         CreatedBy = r.CreatedBy,
                         CreatedDate = r.CreatedDate,
                         UpdatedDate = r.UpdatedDate,
                         IsDeleted = r.IsDeleted == true ? true : false,
                         IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                         IsVerified = r.IsVerified == true ? true : false,
                         UniqueId = r.UniqueId,
                         CountryCode = r.CountryCode,
                         SubCategoryIds = db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == r.Id).Select(p => p.SubCategoryId).ToList(),
                         Image = r.Image != null ? r.Image : Common.NoImageIconWithoutPath,
                     }
                         ).ToList();
                return data;

            }
            return null;
        }
        public List<User> SearchProfessional(string phone, string email, string countryCode)
        {
            long? countryCodeLong = Convert.ToInt64(countryCode);
            var obj = db.tbl_User.Where(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                   a.IsDeleted == false &&
                   (
                       (
                           ((countryCodeLong != 0 && !string.IsNullOrEmpty(phone)) ? a.CountryCode == countryCodeLong : countryCodeLong == 0) &&
                           (!string.IsNullOrEmpty(phone) ? a.PhoneNumber.ToLower() == phone.ToLower() : !string.IsNullOrEmpty(phone))
                       )
                       ||
                       (!string.IsNullOrEmpty(email) ? a.Email == email : !string.IsNullOrEmpty(email))
                    )
                   && a.Type == (int)HelperEnums.UserType.Professional &&
                  a.IsApprovedByAdmin == true
                   ).Select(r => new User
                   {
                       ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == r.Id)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                       JobsDone = db.tbl_Jobs.Count(p => p.ProfessionalID == r.Id),
                       Id = r.Id,
                       CompanyName = r.CompanyName,
                       FirstName = r.FirstName,
                       LastName = r.LastName,
                       Website = r.Website,
                       VerificationCode = r.VerificationCode,
                       Type = r.Type,
                       Email = r.Email,
                       PhoneNumber = r.PhoneNumber,
                       StreetAddress = r.StreetAddress,
                       CityId = r.CityId,
                       StateId = r.StateId,
                       ZipId = r.ZipId,
                       Appartment = r.Appartment,
                       CreatedBy = r.CreatedBy,
                       CreatedDate = r.CreatedDate,
                       UpdatedDate = r.UpdatedDate,
                       IsDeleted = r.IsDeleted == true ? true : false,
                       IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false,
                       IsVerified = r.IsVerified == true ? true : false,
                       UniqueId = r.UniqueId,
                       CountryCode = r.CountryCode,
                       SubCategoryIds = db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == r.Id).Select(p => p.SubCategoryId).ToList(),
                       Image = r.Image != null ? r.Image : Common.NoImageIconWithoutPath,
                   }).ToList();

            return obj;

        }

        public bool GetRecord()
        {
            tbl_User obj = db.tbl_User.FirstOrDefault(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                   (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                   (CityId != 0 ? a.CityId == CityId : CityId == 0) &&
                   (CountryCode != 0 ? a.CountryCode == CountryCode : CountryCode == 0) &&
                   (!string.IsNullOrEmpty(PhoneNumber) ? a.PhoneNumber.ToLower() == PhoneNumber.ToLower() : string.IsNullOrEmpty(PhoneNumber)) &&
                   (!string.IsNullOrEmpty(Email) ? a.Email == Email : string.IsNullOrEmpty(Email)) &&
                   (!string.IsNullOrEmpty(Password) ? a.Password.ToLower() == Password.ToLower() : string.IsNullOrEmpty(Password)) &&
                   (!string.IsNullOrEmpty(UniqueId) ? a.UniqueId.ToLower() == UniqueId.ToLower() : string.IsNullOrEmpty(UniqueId)) &&
                   (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                   );
            if (obj != null)
            {
                Id = obj.Id;
                CompanyName = obj.CompanyName;
                Password = obj.Password;
                FirstName = obj.FirstName;
                LastName = obj.LastName;
                Website = obj.Website;
                VerificationCode = obj.VerificationCode;
                Type = obj.Type;
                Email = obj.Email;
                PhoneNumber = obj.PhoneNumber;
                StreetAddress = obj.StreetAddress;
                CityId = obj.CityId;

                StateId = obj.StateId;
                ZipId = obj.ZipId;
                Appartment = obj.Appartment;
                CreatedBy = obj.CreatedBy;
                CreatedDate = obj.CreatedDate;
                UpdatedDate = obj.UpdatedDate;
                IsDeleted = Convert.ToBoolean(obj.IsDeleted);
                IsApprovedByAdmin = Convert.ToBoolean(obj.IsApprovedByAdmin);
                IsVerified = Convert.ToBoolean(obj.IsVerified);
                UniqueId = obj.UniqueId;
                CountryCode = obj.CountryCode;
                Image = !string.IsNullOrEmpty(obj.Image) ? obj.Image : Common.NoImageIcon;
                ProfileUrl = obj.ProfileUrl;
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
                Records = db.tbl_User.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                       (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false) &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                       );
            }
            catch (Exception ex)
            {

            }
            return Records;
        }
        public Int64 GetTotalProfessionalRecords()
        {
            Int64 Records = 0;
            try
            {
                Records = db.tbl_User.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                       (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false)
                       && Type == (long?)HelperEnums.UserType.Professional &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                       );
            }
            catch (Exception ex)
            {

            }
            return Records;
        }
        public Int64 GetTotalUserRecords()
        {
            Int64 Records = 0;
            try
            {
                Records = db.tbl_User.Count(a => (Id != 0 ? a.Id == Id : Id == 0) &&
                       (IsDeleted ? a.IsDeleted == true : a.IsDeleted == false)
                       && Type == (long?)HelperEnums.UserType.User &&
                       (IsApproved == (int)HelperEnums.BooleanValues.Approved ? a.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? a.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                       );
            }
            catch (Exception ex)
            {

            }
            return Records;
        }
        #endregion



        public List<ProfessionalUrls> GetProfessionalProfileUrls(long userID)
        {
            var data = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == userID)
                                    .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                    (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList();
            return data;
        }
        public User GetUserDetails(long userID)
        {

            var userData = (from u in db.tbl_User
                            join c in db.tbl_State
                            on u.CityId equals c.Id
                            where u.IsApprovedByAdmin == true && u.IsDeleted == false && u.IsVerified == true && u.Id == userID
                            select new User
                            {
                                Platform = u.Platform,
                                DeviceToken = u.DeviceToken,
                                Id = u.Id,
                                CompanyName = u.CompanyName,
                                Password = u.Password,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Website = u.Website,
                                VerificationCode = u.VerificationCode,
                                Type = u.Type,
                                Email = u.Email,
                                PhoneNumber = u.PhoneNumber,
                                StreetAddress = u.StreetAddress,
                                CityId = u.CityId,
                                CityName = c.City + " " + c.State,
                                StateId = u.StateId,
                                ZipId = u.ZipId,
                                Appartment = u.Appartment,
                                UniqueId = u.UniqueId,
                                CountryCode = u.CountryCode,
                                Image = u.Image,
                                ProfileUrl = u.ProfileUrl,
                                ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == userID)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                                CitiesIServe = (from r in db.tbl_User_City_Mapping
                                                where r.UserId == userID
                                                select new DropDownsCityZip
                                                {
                                                    Id = r.CityId,
                                                    // City = r.tbl_State.City + " " + r.tbl_State.State + " " + r.tbl_State.zip,
                                                    City = r.tbl_State.City + " " + r.tbl_State.State,
                                                    Zip = r.tbl_State.zip

                                                }).ToList(),
                                JobsPosted = db.tbl_Jobs.Count(p => p.UserId == u.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Open),
                                JobsDone = db.tbl_Jobs.Count(p => p.UserId == u.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Done),
                                JobsCancelled = db.tbl_Jobs.Count(p => p.UserId == u.Id && p.IsDeleted == false && p.IsApprovedByAdmin == true && p.JobStatus == (int)HelperEnums.JobStatus.Cancel),
                                ReferalCount = (u.Type == (int)HelperEnums.UserType.Professional) ? db.tbl_Referal.Count(p => p.UserId == userID && p.IsApprovedByAdmin == true && p.IsDeleted == false)
                                                                 : db.tbl_Referal.Count(p => p.SenderId == userID && p.IsApprovedByAdmin == true && p.IsDeleted == false)
                            }).FirstOrDefault();


            return userData;


        }
        public User GetUserDetails(long subCategoryID, string profileUrl)
        {

            var userData = (from u in db.tbl_User
                            join s in db.tbl_User_SubCategory_Mapping
                            on u.Id equals s.UserId
                            where u.IsApprovedByAdmin == true && u.IsDeleted == false && u.IsVerified == true
                            && u.ProfileUrl == profileUrl && s.SubCategoryId == subCategoryID
                            select new User
                            {
                                Id = u.Id,
                                //  CompanyName = u.CompanyName,
                                UniqueId = u.UniqueId,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                // Website = u.Website,
                                // VerificationCode = u.VerificationCode,
                                // Type = u.Type,
                                // Email = u.Email,
                                // PhoneNumber = u.PhoneNumber,
                                // StreetAddress = u.StreetAddress,
                                // CityId = u.CityId,
                                //// CityName = c.City + " " + c.State + " " + c.zip,
                                // StateId = u.StateId,
                                // ZipId = u.ZipId,
                                // Appartment = u.Appartment,
                                // UniqueId = u.UniqueId,
                                // CountryCode = u.CountryCode,
                                // Image = u.Image,
                                // ProfileUrl = u.ProfileUrl,

                            }).FirstOrDefault();


            return userData;


        }
        public List<DropDownsCityZip> GetAllCitiesServe(long userID)
        {
            List<DropDownsCityZip> lst = new List<DropDownsCityZip>();
            lst = (from r in db.tbl_User_City_Mapping
                   where r.UserId == userID
                   select new DropDownsCityZip
                   {
                       Id = r.CityId,
                       City = r.tbl_State.City + " " + r.tbl_State.State,
                       Zip = r.tbl_State.zip
                   }).ToList();
            return lst;
        }
        public bool IsProfileUrlExits(string profileUrl)
        {
            var count = db.tbl_User.Count(p => p.ProfileUrl == profileUrl);
            return count > 0 ? true : false;
        }
        public string CreateProfileUrl(string profileUrl, int recursionCount)
        {

            if (IsProfileUrlExits(profileUrl))
            {
                profileUrl = profileUrl + recursionCount;
                CreateProfileUrl(profileUrl, recursionCount++);
            }
            return profileUrl;
        }
        public string GetUsernameByUserID(long userID)
        {
            var user = db.tbl_User.FirstOrDefault(p => p.Id == userID);
            if (user != null)
            {
                return user.FirstName + " " + user.LastName;
            }
            return "";
        }

        #region Api Methods

        public TopReferralDataContract ProfessionalsByLocationIDForUserApp(long loggedInUserID, int pageIndex, int pageSize, long locationID)
        {
            var query1 = (from a in db.tbl_User
                          join s in db.tbl_State
                          on a.CityId equals s.Id
                          where
                          (
                              (from u in db.tbl_User
                               join c in db.tbl_User_City_Mapping
                               on u.Id equals c.UserId
                               where (u.CityId == locationID || c.CityId == locationID)
                               select
                               u.Id
                               )
                               .Distinct()).Contains(a.Id)
                               &&
                               db.tbl_Favorite.Count(p=>p.Favoritetype==(int)(HelperEnums.FavoriteType.Professional)&&
                               p.FavoriteTypeID==a.Id&&p.UserID==loggedInUserID)==0
                          select new ProfessionalDataContract
                          {
                             // IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == a.Id && p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                              IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == a.Id && p.FlagType == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                              ShareUrl = Common.WebsiteHostNameForLink + "/ReferDetail?Id=" + a.UniqueId,
                              Id = a.Id,
                              ReferralCount = db.tbl_Referal.Count(t => t.UserId == a.Id && t.IsSatisfied == true && t.IsDeleted == false),
                              CompanyName = a.CompanyName,
                              IsApprovedByAdmin = a.IsApprovedByAdmin,
                              FirstName = a.FirstName,
                              LastName = a.LastName,
                              Website = a.Website,
                              VerificationCode = a.VerificationCode,
                              Type = a.Type,
                              Email = a.Email,
                              PhoneNumber = a.PhoneNumber,
                              //StreetAddress = u.StreetAddress,
                              CityId = a.CityId,
                              WorkLocation = s == null ? "" : s.City + " " + s.State,
                              //Appartment = u.Appartment,
                              UniqueId = a.UniqueId,
                              CountryCode = a.CountryCode,
                              IsVerified = a.IsVerified,
                              IsProfileUpdated = a.IsProfileUpdated,
                              RoleId = a.RoleId,
                              Image = !string.IsNullOrEmpty(a.Image) ? Common.UserImagesPath + a.Image : Common.NoImageIcon,
                              ProfileUrl = a.ProfileUrl,
                              ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == a.Id)
                             .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                             (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                              CitiesIServe = (from r in db.tbl_User_City_Mapping
                                              where r.UserId == a.Id
                                              select new PropStates
                                              {
                                                  Id = r.CityId,
                                                  City = r.tbl_State.City,
                                                  Zip = r.tbl_State.zip,
                                                  State = r.tbl_State.State
                                              }).ToList(),
                          });
            var count = query1.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(count) / Convert.ToDecimal(pageSize));

            //var data = query.OrderByDescending(p => p.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var data = query1.OrderByDescending(p => p.ReferralCount).ToList();


            TopReferralDataContract list = new TopReferralDataContract();
            list.UserList = data;

            if (totalpages <= (pageIndex + 1))
            {
                list.HideShowMore = true;
            }
            else
            {
                list.HideShowMore = false;
            }
            return list;

        }
        public TopReferralDataContract ProfessionalsByLocationIDForProApp(long loggedInUserID, int pageIndex, int pageSize, long locationID,List<long?> citiesServe)
        {
            var query1 = (from a in db.tbl_User
                          join s in db.tbl_State
                          on a.CityId equals s.Id
                          where
                          (
                              ((from u in db.tbl_User
                               join c in db.tbl_User_City_Mapping
                               on u.Id equals c.UserId
                               where (u.CityId == locationID || c.CityId == locationID)||
                               citiesServe.Contains(u.CityId)|| citiesServe.Contains(c.CityId)
                                select
                               u.Id
                               )
                               .Distinct()).Contains(a.Id)
                               
                               
                               )
                               &&
                               db.tbl_Favorite.Count(p => p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) &&
                               p.FavoriteTypeID == a.Id&&p.UserID==loggedInUserID) == 0
                          select new ProfessionalDataContract
                          {
                              IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == a.Id && p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                              IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == a.Id && p.FlagType == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                              ShareUrl = Common.WebsiteHostNameForLink + "/ReferDetail?Id=" + a.UniqueId,
                              Id = a.Id,
                              ReferralCount = db.tbl_Referal.Count(t => t.UserId == a.Id && t.IsSatisfied == true && t.IsDeleted == false),
                              CompanyName = a.CompanyName,
                              IsApprovedByAdmin = a.IsApprovedByAdmin,
                              FirstName = a.FirstName,
                              LastName = a.LastName,
                              Website = a.Website,
                              VerificationCode = a.VerificationCode,
                              Type = a.Type,
                              Email = a.Email,
                              PhoneNumber = a.PhoneNumber,
                              //StreetAddress = u.StreetAddress,
                              CityId = a.CityId,
                              WorkLocation = s == null ? "" : s.City + " " + s.State,
                              //Appartment = u.Appartment,
                              UniqueId = a.UniqueId,
                              CountryCode = a.CountryCode,
                              IsVerified = a.IsVerified,
                              IsProfileUpdated = a.IsProfileUpdated,
                              RoleId = a.RoleId,
                              Image = !string.IsNullOrEmpty(a.Image) ? Common.UserImagesPath + a.Image : Common.NoImageIcon,
                              ProfileUrl = a.ProfileUrl,
                              ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == a.Id)
                             .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                             (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                              CitiesIServe = (from r in db.tbl_User_City_Mapping
                                              where r.UserId == a.Id
                                              select new PropStates
                                              {
                                                  Id = r.CityId,
                                                  City = r.tbl_State.City,
                                                  Zip = r.tbl_State.zip,
                                                  State = r.tbl_State.State
                                              }).ToList(),
                          });
            var count = query1.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(count) / Convert.ToDecimal(pageSize));

            //var data = query.OrderByDescending(p => p.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var data = query1.OrderByDescending(p => p.ReferralCount).ToList();


            TopReferralDataContract list = new TopReferralDataContract();
            list.UserList = data;

            if (totalpages <= (pageIndex + 1))
            {
                list.HideShowMore = true;
            }
            else
            {
                list.HideShowMore = false;
            }
            return list;

        }

        public TopReferralDataContract ProfessionalsByLocationAndSubcategoryID(long subCategoryID, long loggedInUserID, int pageIndex, int pageSize, long locationID)
        {
            var query1 = (from a in db.tbl_User
                          join s in db.tbl_State
                          on a.CityId equals s.Id
                          where
                          (
                              (from u in db.tbl_User
                               join c in db.tbl_User_City_Mapping
                               on u.Id equals c.UserId
                               where (u.CityId == locationID || c.CityId == locationID)
                               && (db.tbl_User_SubCategory_Mapping.Count(p => p.UserId == u.Id && p.SubCategoryId == subCategoryID) > 0)
                               select
                               u.Id
                               )
                               .Distinct()).Contains(a.Id)
                          select new ProfessionalDataContract
                          {
                              IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == a.Id && p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                              IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == a.Id && p.FlagType == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                              ShareUrl = Common.WebsiteHostNameForLink + "/ReferDetail?Id=" + a.UniqueId,
                              Id = a.Id,
                              ReferralCount = db.tbl_Referal.Count(t => t.UserId == a.Id && t.IsSatisfied == true && t.IsDeleted == false),
                              CompanyName = a.CompanyName,
                              IsApprovedByAdmin = a.IsApprovedByAdmin,
                              FirstName = a.FirstName,
                              LastName = a.LastName,
                              Website = a.Website,
                              VerificationCode = a.VerificationCode,
                              Type = a.Type,
                              Email = a.Email,
                              PhoneNumber = a.PhoneNumber,
                              //StreetAddress = u.StreetAddress,
                              CityId = a.CityId,
                              WorkLocation = s == null ? "" : s.City + " " + s.State,
                              //Appartment = u.Appartment,
                              UniqueId = a.UniqueId,
                              CountryCode = a.CountryCode,
                              IsVerified = a.IsVerified,
                              IsProfileUpdated = a.IsProfileUpdated,
                              RoleId = a.RoleId,
                              Image = !string.IsNullOrEmpty(a.Image) ? Common.UserImagesPath + a.Image : Common.NoImageIcon,
                              ProfileUrl = a.ProfileUrl,
                              ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == a.Id)
                             .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                             (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                              CitiesIServe = (from r in db.tbl_User_City_Mapping
                                              where r.UserId == a.Id
                                              select new PropStates
                                              {
                                                  Id = r.CityId,
                                                  City = r.tbl_State.City,
                                                  Zip = r.tbl_State.zip,
                                                  State = r.tbl_State.State
                                              }).ToList(),
                          });
            var count = query1.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(count) / Convert.ToDecimal(pageSize));

            //var data = query.OrderByDescending(p => p.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var data = query1.OrderByDescending(p => p.ReferralCount).ToList();


            TopReferralDataContract list = new TopReferralDataContract();
            list.UserList = data;

            if (totalpages <= (pageIndex + 1))
            {
                list.HideShowMore = true;
            }
            else
            {
                list.HideShowMore = false;
            }
            return list;

        }
        public TopReferralDataContract TopReferrals(long loggedInUserID, int pageIndex, int pageSize, long locationID)
        {
            var query1 = (from r in db.tbl_Referal
                          join a in db.tbl_User
                           on r.UserId equals a.Id

                          where a.IsDeleted == false && locationID == 0 ? true : r.CityId == locationID
                          && a.Type == (int)(HelperEnums.UserType.Professional)
                       && a.IsApprovedByAdmin == true
                          group r by r.UserId into user

                          select new { user.Key, referalCount = user.Count() }).OrderByDescending(p => p.referalCount);

            var count = query1.Count();
            //var userIDData = query1.ToList();
            decimal totalpages = Math.Ceiling(Convert.ToDecimal(count) / Convert.ToDecimal(pageSize));

            var queryData = query1.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var userIDData = queryData.Select(p => p.Key);
            var data = (
                from a in db.tbl_User
                join state in db.tbl_State
            on a.CityId equals state.Id into location
                from c in location.DefaultIfEmpty()
                where
                userIDData.Contains(a.Id)
                select new ProfessionalDataContract
                {
                    IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == a.Id && p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                    IsFlag = db.tbl_Flag.Count(p => p.FlagTypeID == a.Id && p.FlagType == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                    ShareUrl = Common.WebsiteHostNameForLink + "/ReferDetail?Id=" + a.UniqueId,
                    Id = a.Id,
                    ReferralCount = db.tbl_Referal.Count(t => t.UserId == a.Id && t.IsSatisfied == true && t.IsDeleted == false),
                    CompanyName = a.CompanyName,
                    IsApprovedByAdmin = a.IsApprovedByAdmin,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Website = a.Website,
                    VerificationCode = a.VerificationCode,
                    Type = a.Type,
                    Email = a.Email,
                    PhoneNumber = a.PhoneNumber,
                    //StreetAddress = u.StreetAddress,
                    CityId = a.CityId,
                    WorkLocation = c == null ? "" : c.City + " " + c.State,
                    //Appartment = u.Appartment,
                    UniqueId = a.UniqueId,
                    CountryCode = a.CountryCode,
                    IsVerified = a.IsVerified,
                    IsProfileUpdated = a.IsProfileUpdated,
                    RoleId = a.RoleId,
                    Image = !string.IsNullOrEmpty(a.Image) ? Common.UserImagesPath + a.Image : Common.NoImageIcon,
                    ProfileUrl = a.ProfileUrl,
                    ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == a.Id)
                                 .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                 (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                    CitiesIServe = (from r in db.tbl_User_City_Mapping
                                    where r.UserId == a.Id
                                    select new PropStates
                                    {
                                        Id = r.CityId,
                                        City = r.tbl_State.City,
                                        Zip = r.tbl_State.zip,
                                        State = r.tbl_State.State
                                    }).ToList(),
                }).OrderByDescending(p => p.ReferralCount).ToList();


            TopReferralDataContract list = new TopReferralDataContract();
            list.UserList = data;

            if (totalpages <= (pageIndex + 1))
            {
                list.HideShowMore = true;
            }
            else
            {
                list.HideShowMore = false;
            }
            return list;

        }
        public List<ProfessionalDataContract> SearchProfessionalForReferral(string phone, string email, string countryCode)
        {
            long? countryCodeLong = Convert.ToInt64(countryCode);
            var obj = (from a in db.tbl_User
                       join state in db.tbl_State
                        on a.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()
                       where a.IsDeleted == false && a.Type == (int)HelperEnums.UserType.Professional &&
                      //(
                      //   (
                      //       (countryCodeLong != 0 ? a.CountryCode == countryCodeLong : countryCodeLong == 0) &&
                      //       (!string.IsNullOrEmpty(phone) ? a.PhoneNumber.ToLower() == phone.ToLower() : string.IsNullOrEmpty(phone))
                      //   )
                      //   ||
                      //   (!string.IsNullOrEmpty(email) ? a.Email == email : string.IsNullOrEmpty(email))
                      //)&&
                      (
                       (
                           ((countryCodeLong != 0 && !string.IsNullOrEmpty(phone)) ? a.CountryCode == countryCodeLong : countryCodeLong == 0) &&
                           (!string.IsNullOrEmpty(phone) ? a.PhoneNumber.ToLower() == phone.ToLower() : !string.IsNullOrEmpty(phone))
                       )
                       ||
                       (!string.IsNullOrEmpty(email) ? a.Email == email : !string.IsNullOrEmpty(email))
                    )
                     && a.IsApprovedByAdmin == true
                       select new ProfessionalDataContract
                       {
                           Id = a.Id,
                           CompanyName = a.CompanyName,
                           IsApprovedByAdmin = a.IsApprovedByAdmin,
                           FirstName = a.FirstName,
                           LastName = a.LastName,
                           Website = a.Website,
                           VerificationCode = a.VerificationCode,
                           Type = a.Type,
                           Email = a.Email,
                           PhoneNumber = a.PhoneNumber,
                           //StreetAddress = u.StreetAddress,
                           CityId = a.CityId,
                           WorkLocation = c == null ? "" : c.City + " " + c.State,
                           //Appartment = u.Appartment,
                           UniqueId = a.UniqueId,
                           CountryCode = a.CountryCode,
                           IsVerified = a.IsVerified,
                           IsProfileUpdated = a.IsProfileUpdated,
                           RoleId = a.RoleId,
                           Image = !string.IsNullOrEmpty(a.Image) ? Common.UserImagesPath + a.Image : Common.NoImageIcon,
                           ProfileUrl = a.ProfileUrl,
                           ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == a.Id)
                                    .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                    (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                           CitiesIServe = (from r in db.tbl_User_City_Mapping
                                           where r.UserId == a.Id
                                           select new PropStates
                                           {
                                               Id = r.CityId,
                                               City = r.tbl_State.City,
                                               Zip = r.tbl_State.zip,
                                               State = r.tbl_State.State
                                           }).ToList(),
                       }).ToList();

            return obj;

        }
        public CountUserActivites_Result GetUserActivityCounts(long userID)
        {
            return db.CountUserActivites(userID).FirstOrDefault();

        }
        public CountProfessionalActivites_Result GetProfessionalActivityCounts(long userID)
        {
            return db.CountProfessionalActivites(userID).FirstOrDefault();

        }
        public ResultData CheckEmailPhoneAlreadyExists(string email, string phonenumber, long countryCode, long userID)
        {
            var IsExists = false;
            if (!string.IsNullOrEmpty(email))
            {
                var userData = db.tbl_User.FirstOrDefault(p => p.Email == email);
                if (userData != null)
                {
                    if (userData.Id == userID)
                    {
                        IsExists = false;
                    }
                    else
                    {
                        IsExists = true;
                    }
                }
                if (IsExists)
                {
                    return new ResultData { ResultDescription = "Email Already Exists", ResultStatus = 0 };
                }
            }
            else
            {
                var userData = db.tbl_User.FirstOrDefault(p => p.PhoneNumber == phonenumber && p.CountryCode == countryCode);
                if (userData != null)
                {
                    if (userData.Id == userID)
                    {
                        IsExists = false;
                    }
                    else
                    {
                        IsExists = true;
                    }
                }
                if (IsExists)
                {
                    return new ResultData { ResultDescription = "Phone Number Already Exists", ResultStatus = 0 };
                }
            }


            return new ResultData { ResultDescription = "Success", ResultStatus = 1 };


        }
        /// <summary>
        /// This function is used to Login a User.
        /// </summary>
        /// <typeparam name="T">T Type can be UserDataContract or ProfessionalDataContract</typeparam>
        /// <param name="email"></param>
        /// <param name="countryCode"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public UserDataContract GetUserByID(long ID)
        {

            var obj = (from u in db.tbl_User
                       join state in db.tbl_State
                         on u.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()
                       where u.IsDeleted == false &&
                            u.Id == ID && u.Type == (int)HelperEnums.UserType.User
                       select new UserDataContract
                       {
                           notificationSetting = (from r in db.tbl_NotificationSettings
                                                  where r.UserID == u.Id

                                                  select new NotificationSettingDataContract
                                                  {
                                                      ID = r.ID,
                                                      AppointmentRequest = r.AppointmentRequest,
                                                      EstimateFeedback = r.EstimateFeedback,
                                                      NewFollowers = r.NewFollowers,
                                                      NewMessage = r.NewMessage,
                                                      Recommendations = r.Recommendations,
                                                      UserID = r.UserID
                                                  }).FirstOrDefault(),
                           Id = u.Id,
                           Platform = u.Platform,
                           DeviceToken = u.DeviceToken,
                           IsApprovedByAdmin = u.IsApprovedByAdmin,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           VerificationCode = u.VerificationCode,
                           RoleId = u.RoleId,
                           IsProfileUpdated = u.IsProfileUpdated,
                           Type = u.Type,
                           Email = u.Email,
                           PhoneNumber = u.PhoneNumber,
                           StreetAddress = u.StreetAddress,
                           CityId = u.CityId,
                           Appartment = u.Appartment,
                           UniqueId = u.UniqueId,
                           CountryCode = u.CountryCode,
                           Image = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : "",
                           ProfileUrl = u.ProfileUrl,
                           IsVerified = u.IsVerified,
                           Location = c == null ? "" : c.City + " " + c.State,
                           CityName = c == null ? "" : c.City,
                           StateName = c == null ? "" : c.State,
                           ZipName = c == null ? "" : c.zip,
                       }).FirstOrDefault();


            if (obj == null)
            {
                obj = new UserDataContract();

            }
            return obj;

        }

        public UserProfileDataContract GetUserProfile(long ID, long loggedInUserID)
        {

            var userData = (from u in db.tbl_User
                            join state in db.tbl_State
                              on u.CityId equals state.Id into location
                            from c in location.DefaultIfEmpty()
                            where u.IsDeleted == false &&
                                 u.Id == ID && u.Type == (int)HelperEnums.UserType.User
                            select new UserDataContract
                            {
                                ThreadID = (from p in db.tbl_ThreadParticipants
                                            join t in db.tbl_Threads
                                            on p.ThreadID equals t.ID
                                            where (p.UserID == loggedInUserID || p.UserID == ID)
                                            && t.IsJobThread == false
                                            //&& p.tbl_Threads.JobID == null
                                            group p by p.ThreadID into participants
                                            select participants).Where(p => p.Count() == 2)
                                .Select(k => k.Key).FirstOrDefault(),

                                Id = u.Id,
                                //IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == ID && p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) && p.UserID == loggedInUserID) > 0 ? true : false,
                                IsFavorite = (db.tbl_Favorite.Count(p => p.FavoriteTypeID == ID
                                     && (p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) || p.Favoritetype == (int)(HelperEnums.FavoriteType.User))
                                     && p.UserID == loggedInUserID) > 0) ? true : false,

                                IsFlag = (db.tbl_Flag.Count(p => p.FlagTypeID == ID
                                      && (p.FlagType == (int)(HelperEnums.FlagType.Professional) || p.FlagType == (int)(HelperEnums.FlagType.User))
                                      && p.UserID == loggedInUserID) > 0) ? true : false,



                                IsApprovedByAdmin = u.IsApprovedByAdmin,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                VerificationCode = u.VerificationCode,
                                RoleId = u.RoleId,
                                IsProfileUpdated = u.IsProfileUpdated,
                                Type = u.Type,
                                Email = u.Email,
                                PhoneNumber = u.PhoneNumber,
                                StreetAddress = u.StreetAddress,
                                CityId = u.CityId,
                                Appartment = u.Appartment,
                                UniqueId = u.UniqueId,
                                CountryCode = u.CountryCode,
                                Image = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,
                                ProfileUrl = u.ProfileUrl,
                                IsVerified = u.IsVerified,
                                Location = c == null ? "" : c.City + " " + c.State,
                                
                            }).FirstOrDefault();


            var recentJob = new Jobs().GetUserRecentJob(ID);
            var recentReferrals = new Referal().GetReferralsGiven(ID, 0, 5, HelperEnums.BooleanValues.Both);

            UserProfileDataContract userProfile = new UserProfileDataContract();
            userProfile.UserData = userData;
            userProfile.RecentJob = recentJob;
            userProfile.RecentReferrals = recentReferrals;
            return userProfile;
        }
        public ProfessionalDataContract GetProfessionalByID(long ID)
        {


            var obj = (from u in db.tbl_User
                       join state in db.tbl_State
                        on u.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()
                       where u.IsDeleted == false &&
                     u.Id == ID && u.Type == (int)HelperEnums.UserType.Professional
                       select new ProfessionalDataContract
                       {
                           notificationSetting = (from r in db.tbl_NotificationSettings
                                                  where r.UserID == u.Id

                                                  select new NotificationSettingDataContract
                                                  {
                                                      ID = r.ID,
                                                      AppointmentRequest = r.AppointmentRequest,
                                                      EstimateFeedback = r.EstimateFeedback,
                                                      NewFollowers = r.NewFollowers,
                                                      NewMessage = r.NewMessage,
                                                      Recommendations = r.Recommendations,
                                                      UserID = r.UserID
                                                  }).FirstOrDefault(),

                           Platform = u.Platform,
                           DeviceToken = u.DeviceToken,
                           ShareUrl = Common.WebsiteHostNameForLink + "/ReferDetail?Id=" + u.UniqueId,
                           Id = u.Id,
                           CompanyName = u.CompanyName,
                           IsApprovedByAdmin = u.IsApprovedByAdmin,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Website = u.Website,
                           VerificationCode = u.VerificationCode,
                           Type = u.Type,
                           Email = u.Email,
                           PhoneNumber = u.PhoneNumber,
                           StreetAddress = u.StreetAddress,
                           CityId = u.CityId,
                           WorkLocation = c == null ? "" : c.City + " " + c.State,
                           CityName = c == null ? "" : c.City,
                           StateName = c == null ? "" : c.State,
                           ZipName = c == null ? "" : c.zip,
                           Appartment = u.Appartment,
                           UniqueId = u.UniqueId,
                           CountryCode = u.CountryCode,
                           IsVerified = u.IsVerified,
                           IsProfileUpdated = u.IsProfileUpdated,
                           RoleId = u.RoleId,
                           Image = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,
                           ProfileUrl = u.ProfileUrl,
                           ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == u.Id)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                           CitiesIServe = (from r in db.tbl_User_City_Mapping
                                           where r.UserId == u.Id
                                           select new PropStates
                                           {
                                               Id = r.CityId,
                                               City = r.tbl_State.City,
                                               Zip = r.tbl_State.zip,
                                               State = r.tbl_State.State
                                           }).ToList(),

                       }).FirstOrDefault();


            if (obj == null)
            {
                obj = new ProfessionalDataContract();
                //userData = Mapper.Map<DataAccess.Classes.u, ProfessionalDataContract>(obj);
            }
            return obj;

        }
        public ProfessionalDataContract GetProfessionalByID(long ID, long loggedInUserID)
        {


            var obj = (from u in db.tbl_User
                       join state in db.tbl_State
                        on u.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()
                       where u.IsDeleted == false &&
                     u.Id == ID && u.Type == (int)HelperEnums.UserType.Professional
                       select new ProfessionalDataContract
                       {
                           notificationSetting = (from r in db.tbl_NotificationSettings
                                                  where r.UserID == u.Id

                                                  select new NotificationSettingDataContract
                                                  {
                                                      ID = r.ID,
                                                      AppointmentRequest = r.AppointmentRequest,
                                                      EstimateFeedback = r.EstimateFeedback,
                                                      NewFollowers = r.NewFollowers,
                                                      NewMessage = r.NewMessage,
                                                      Recommendations = r.Recommendations,
                                                      UserID = r.UserID
                                                  }).FirstOrDefault(),
                           IsFavorite = (db.tbl_Favorite.Count(p => p.FavoriteTypeID == ID
                                    && (p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) || p.Favoritetype == (int)(HelperEnums.FavoriteType.User))
                                    && p.UserID == loggedInUserID) > 0) ? true : false,

                           IsFlag = (db.tbl_Flag.Count(p => p.FlagTypeID == ID
                                 && (p.FlagType == (int)(HelperEnums.FlagType.Professional) || p.FlagType == (int)(HelperEnums.FlagType.User))
                                 && p.UserID == loggedInUserID) > 0) ? true : false,

                           ThreadID = (from p in db.tbl_ThreadParticipants
                                       join t in db.tbl_Threads
                                       on p.ThreadID equals t.ID
                                       where (p.UserID == loggedInUserID || p.UserID == ID)
                                       && t.IsJobThread == false
                                       //&& p.tbl_Threads.JobID == null
                                       group p by p.ThreadID into participants
                                       select participants).Where(p => p.Count() == 2)
                                .Select(k => k.Key).FirstOrDefault(),
                           Platform = u.Platform,
                           DeviceToken = u.DeviceToken,
                           ShareUrl = Common.WebsiteHostNameForLink + "/ReferDetail?Id=" + u.UniqueId,
                           Id = u.Id,
                           CompanyName = u.CompanyName,
                           IsApprovedByAdmin = u.IsApprovedByAdmin,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Website = u.Website,
                           VerificationCode = u.VerificationCode,
                           Type = u.Type,
                           Email = u.Email,
                           PhoneNumber = u.PhoneNumber,
                           //StreetAddress = u.StreetAddress,
                           CityId = u.CityId,
                           WorkLocation = c == null ? "" : c.City + " " + c.State,
                           CityName = c == null ? "" : c.City,
                           StateName = c == null ? "" : c.State,
                           ZipName = c == null ? "" : c.zip,
                           //Appartment = u.Appartment,
                           UniqueId = u.UniqueId,
                           CountryCode = u.CountryCode,
                           IsVerified = u.IsVerified,
                           IsProfileUpdated = u.IsProfileUpdated,
                           RoleId = u.RoleId,
                           Image = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,
                           ProfileUrl = u.ProfileUrl,
                           ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == u.Id)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                           CitiesIServe = (from r in db.tbl_User_City_Mapping
                                           where r.UserId == u.Id
                                           select new PropStates
                                           {
                                               Id = r.CityId,
                                               City = r.tbl_State.City,
                                               Zip = r.tbl_State.zip,
                                               State = r.tbl_State.State
                                           }).ToList(),

                       }).FirstOrDefault();


            if (obj == null)
            {
                obj = new ProfessionalDataContract();
                //userData = Mapper.Map<DataAccess.Classes.u, ProfessionalDataContract>(obj);
            }
            return obj;

        }
        public UserDataContract GetUserByEmailPwd(string email, long countryCode, string phone, string password, HelperEnums.UserType userType)
        {
            if (!string.IsNullOrEmpty(email))
            {
                phone = "";
                countryCode = 0;
            }
            //tbl_User obj = db.tbl_User
            //           .FirstOrDefault(p =>
            //            (countryCode != 0 ? p.CountryCode == countryCode : countryCode == 0) &&
            //         (!string.IsNullOrEmpty(phone) ? p.PhoneNumber.ToLower() == phone.ToLower() : string.IsNullOrEmpty(phone)) &&
            //         (!string.IsNullOrEmpty(email) ? p.Email == email : string.IsNullOrEmpty(email))
            //         && p.Password == password && p.IsDeleted == false && p.Type == (int)userType);




            var obj = (from u in db.tbl_User
                       join state in db.tbl_State
                       on u.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()

                           //join ns in db.tbl_NotificationSettings
                           //on u.Id equals ns.UserID into ns1
                           //from tblnotificationSetting in ns1.DefaultIfEmpty()

                       where u.IsDeleted == false &&
                             (countryCode != 0 ? u.CountryCode == countryCode : countryCode == 0) &&
                     (!string.IsNullOrEmpty(phone) ? u.PhoneNumber.ToLower() == phone.ToLower() : string.IsNullOrEmpty(phone)) &&
                     (!string.IsNullOrEmpty(email) ? u.Email == email : string.IsNullOrEmpty(email))
                     && u.Password == password && u.IsDeleted == false && u.Type == (int)userType
                       select new UserDataContract
                       {
                           Id = u.Id,

                           notificationSetting = (from r in db.tbl_NotificationSettings
                                                  where r.UserID == u.Id

                                                  select new NotificationSettingDataContract
                                                  {
                                                      ID = r.ID,
                                                      AppointmentRequest = r.AppointmentRequest,
                                                      EstimateFeedback = r.EstimateFeedback,
                                                      NewFollowers = r.NewFollowers,
                                                      NewMessage = r.NewMessage,
                                                      Recommendations = r.Recommendations,
                                                      UserID = r.UserID
                                                  }).FirstOrDefault(),
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           VerificationCode = u.VerificationCode,
                           RoleId = u.RoleId,
                           IsProfileUpdated = u.IsProfileUpdated,
                           Type = u.Type,
                           Email = u.Email,
                           PhoneNumber = u.PhoneNumber,
                           StreetAddress = u.StreetAddress,
                           CityId = u.CityId,
                           Appartment = u.Appartment,
                           UniqueId = u.UniqueId,
                           CountryCode = u.CountryCode,
                           Image = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : "",
                           ProfileUrl = u.ProfileUrl,
                           IsVerified = u.IsVerified,
                           IsApprovedByAdmin = u.IsApprovedByAdmin,
                           Location = c == null ? "" : c.City + " " + c.State,
                           CityName = c == null ? "" : c.City,
                           StateName = c == null ? "" : c.State,
                           ZipName = c == null ? "" : c.zip,

                       }).FirstOrDefault();


            if (obj == null)
            {
                obj = new UserDataContract();

            }
            else
            {

            }
            return obj;

        }
        public UserDataContract GetUserBySocialID(string socialID, int socialType)
        {


            var obj = (from u in db.tbl_User
                       join state in db.tbl_State
                       on u.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()

                           //join ns in db.tbl_NotificationSettings
                           //on u.Id equals ns.UserID into ns1
                           //from tblnotificationSetting in ns1.DefaultIfEmpty()

                       where u.Type == (int)HelperEnums.UserType.User && u.IsDeleted == false && u.SocialID == socialID && u.SocialType == socialType
                       select new UserDataContract
                       {
                           Id = u.Id,

                           notificationSetting = (from r in db.tbl_NotificationSettings
                                                  where r.UserID == u.Id

                                                  select new NotificationSettingDataContract
                                                  {
                                                      ID = r.ID,
                                                      AppointmentRequest = r.AppointmentRequest,
                                                      EstimateFeedback = r.EstimateFeedback,
                                                      NewFollowers = r.NewFollowers,
                                                      NewMessage = r.NewMessage,
                                                      Recommendations = r.Recommendations,
                                                      UserID = r.UserID
                                                  }).FirstOrDefault(),
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           VerificationCode = u.VerificationCode,
                           RoleId = u.RoleId,
                           IsProfileUpdated = u.IsProfileUpdated,
                           Type = u.Type,
                           Email = u.Email,
                           PhoneNumber = u.PhoneNumber,
                           StreetAddress = u.StreetAddress,
                           CityId = u.CityId,
                           Appartment = u.Appartment,
                           UniqueId = u.UniqueId,
                           CountryCode = u.CountryCode,
                           Image = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : "",
                           ProfileUrl = u.ProfileUrl,
                           IsVerified = u.IsVerified,
                           IsApprovedByAdmin = u.IsApprovedByAdmin,
                           Location = c == null ? "" : c.City + " " + c.State,
                           CityName = c == null ? "" : c.City,
                           StateName = c == null ? "" : c.State,
                           ZipName = c == null ? "" : c.zip,

                       }).FirstOrDefault();


            if (obj == null)
            {
                obj = new UserDataContract();

            }
            else
            {

            }
            return obj;

        }

        public bool IsUserDeviceExists(string platform, string deviceToken, long userID)
        {
            var userDevicesCount = db.tbl_UserDevices.Count(p => p.DeviceToken == deviceToken && p.UserID == userID && p.Platform == platform);
            if (userDevicesCount > 0)
            {
                return true;
            }
            return false;
        }
        public void AddUserDevice(string platform, string deviceToken, long userID)
        {
            var deviceData = db.tbl_User.FirstOrDefault(p => p.Id == userID);
            if (deviceData != null)
            {
                deviceData.DeviceToken = deviceToken;
                deviceData.Platform = platform;
                db.SaveChanges();
            }

            //if (!IsUserDeviceExists(platform, deviceToken, userID))
            //{
            //    tbl_UserDevices userDevice = new tbl_UserDevices();
            //    userDevice.DeviceToken = deviceToken;
            //    userDevice.Platform = platform;
            //    userDevice.UserID = userID;
            //    userDevice.LoggedOn = DateTime.UtcNow;
            //    db.tbl_UserDevices.Add(userDevice);
            //    db.SaveChanges();
            //}

        }
        public void UpdateUserDevice(string platform, string deviceToken, string azureNotificationID, long userID)
        {
            //var userDevice = db.tbl_UserDevices.FirstOrDefault(p => p.DeviceToken == deviceToken && p.UserID == userID && p.Platform == platform);
            //if (userDevice != null)
            //{
            //    db.tbl_UserDevices.Remove(userDevice);
            //    db.SaveChanges();

            //}

            var userData = db.tbl_User.FirstOrDefault(p => p.Id == userID);
            if (userData != null)
            {
                if (!string.IsNullOrEmpty(userData.AzureNotificationId))
                {
                    var regID = userData.AzureNotificationId;
                    Task.Run(async () =>
                    {
                        RegisterDeviceToken rdt = new RegisterDeviceToken();
                        await rdt.Delete(regID);

                    });
                }
                userData.DeviceToken = deviceToken;
                userData.Platform = platform;
                userData.AzureNotificationId = azureNotificationID;
                db.SaveChanges();
            }

        }

        public ProfessionalDataContract GetProfessionalByEmailPwd(string email, long countryCode, string phone, string password, HelperEnums.UserType userType)
        {
            if (!string.IsNullOrEmpty(email))
            {
                phone = "";
                countryCode = 0;
            }
            //tbl_User obj = db.tbl_User
            //           .FirstOrDefault(p =>
            //            (countryCode != 0 ? p.CountryCode == countryCode : countryCode == 0) &&
            //         (!string.IsNullOrEmpty(phone) ? p.PhoneNumber.ToLower() == phone.ToLower() : string.IsNullOrEmpty(phone)) &&
            //         (!string.IsNullOrEmpty(email) ? p.Email == email : string.IsNullOrEmpty(email))
            //         && p.Password == password && p.IsDeleted == false && p.Type == (int)userType);


            var obj = (from u in db.tbl_User
                       join state in db.tbl_State
                       on u.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()
                       where u.IsDeleted == false &&
                             (countryCode != 0 ? u.CountryCode == countryCode : countryCode == 0) &&
                     (!string.IsNullOrEmpty(phone) ? u.PhoneNumber.ToLower() == phone.ToLower() : string.IsNullOrEmpty(phone)) &&
                     (!string.IsNullOrEmpty(email) ? u.Email == email : string.IsNullOrEmpty(email))
                     && u.Password == password && u.IsDeleted == false && u.Type == (int)userType
                       select new ProfessionalDataContract
                       {
                           Id = u.Id,
                           CompanyName = u.CompanyName,
                           notificationSetting = (from r in db.tbl_NotificationSettings
                                                  where r.UserID == u.Id

                                                  select new NotificationSettingDataContract
                                                  {
                                                      ID = r.ID,
                                                      AppointmentRequest = r.AppointmentRequest,
                                                      EstimateFeedback = r.EstimateFeedback,
                                                      NewFollowers = r.NewFollowers,
                                                      NewMessage = r.NewMessage,
                                                      Recommendations = r.Recommendations,
                                                      UserID = r.UserID
                                                  }).FirstOrDefault(),
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Website = u.Website,
                           VerificationCode = u.VerificationCode,
                           Type = u.Type,
                           Email = u.Email,
                           PhoneNumber = u.PhoneNumber,
                           StreetAddress = u.StreetAddress,
                           CityId = u.CityId,
                           WorkLocation = c == null ? "" : c.City + " " + c.State,
                           CityName = c == null ? "" : c.City,
                           StateName = c == null ? "" : c.State,
                           ZipName = c == null ? "" : c.zip,
                           Appartment = u.Appartment,
                           UniqueId = u.UniqueId,
                           CountryCode = u.CountryCode,
                           IsVerified = u.IsVerified,
                           IsProfileUpdated = u.IsProfileUpdated,
                           RoleId = u.RoleId,
                           Image = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,
                           IsApprovedByAdmin = u.IsApprovedByAdmin,
                           ProfileUrl = u.ProfileUrl,
                           ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == u.Id)
                                           .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                           (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                           CitiesIServe = (from r in db.tbl_User_City_Mapping
                                           where r.UserId == u.Id
                                           select new PropStates
                                           {
                                               Id = r.CityId,
                                               City = r.tbl_State.City,
                                               Zip = r.tbl_State.zip,
                                               State = r.tbl_State.State
                                           }).ToList(),

                       }).FirstOrDefault();


            if (obj == null)
            {
                obj = new ProfessionalDataContract();
                //userData = Mapper.Map<DataAccess.Classes.u, ProfessionalDataContract>(obj);
            }
            return obj;

        }
        public ProfessionalDataContract GetProfessionalBySocialID(string socialID, int socialType)
        {


            var obj = (from u in db.tbl_User
                       join state in db.tbl_State
                       on u.CityId equals state.Id into location
                       from c in location.DefaultIfEmpty()
                       where u.IsDeleted == false &&
                            u.IsDeleted == false && u.Type == (int)HelperEnums.UserType.Professional
                            && u.SocialID == socialID && u.SocialType == socialType
                       select new ProfessionalDataContract
                       {
                           Id = u.Id,
                           CompanyName = u.CompanyName,
                           notificationSetting = (from r in db.tbl_NotificationSettings
                                                  where r.UserID == u.Id

                                                  select new NotificationSettingDataContract
                                                  {
                                                      ID = r.ID,
                                                      AppointmentRequest = r.AppointmentRequest,
                                                      EstimateFeedback = r.EstimateFeedback,
                                                      NewFollowers = r.NewFollowers,
                                                      NewMessage = r.NewMessage,
                                                      Recommendations = r.Recommendations,
                                                      UserID = r.UserID
                                                  }).FirstOrDefault(),
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Website = u.Website,
                           VerificationCode = u.VerificationCode,
                           Type = u.Type,
                           Email = u.Email,
                           PhoneNumber = u.PhoneNumber,
                           StreetAddress = u.StreetAddress,
                           CityId = u.CityId,
                           WorkLocation = c == null ? "" : c.City + " " + c.State,
                           CityName = c == null ? "" : c.City,
                           StateName = c == null ? "" : c.State,
                           ZipName = c == null ? "" : c.zip,
                           Appartment = u.Appartment,
                           UniqueId = u.UniqueId,
                           CountryCode = u.CountryCode,
                           IsVerified = u.IsVerified,
                           IsProfileUpdated = u.IsProfileUpdated,
                           RoleId = u.RoleId,
                           Image = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,
                           IsApprovedByAdmin = u.IsApprovedByAdmin,
                           ProfileUrl = u.ProfileUrl,
                           ProfessionalUrls = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == u.Id)
                                           .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                           (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                           CitiesIServe = (from r in db.tbl_User_City_Mapping
                                           where r.UserId == u.Id
                                           select new PropStates
                                           {
                                               Id = r.CityId,
                                               City = r.tbl_State.City,
                                               Zip = r.tbl_State.zip,
                                               State = r.tbl_State.State
                                           }).ToList(),

                       }).FirstOrDefault();


            if (obj == null)
            {
                obj = new ProfessionalDataContract();
                //userData = Mapper.Map<DataAccess.Classes.u, ProfessionalDataContract>(obj);
            }
            return obj;

        }
        public UserDataContract Update(User obj)
        {
            tbl_User checkobj = db.tbl_User.FirstOrDefault(a => a.Id == obj.Id);
            if (checkobj != null)
            {
                if (string.IsNullOrEmpty(checkobj.ProfileUrl))
                {
                    checkobj.ProfileUrl = CreateProfileUrl(obj.FirstName + obj.LastName, 0);
                }
                checkobj.FirstName = obj.FirstName;
                checkobj.LastName = obj.LastName;
                checkobj.Email = obj.Email;
                checkobj.CountryCode = obj.CountryCode;
                checkobj.PhoneNumber = obj.PhoneNumber;
                checkobj.CityId = obj.CityId;
                checkobj.StateId = obj.CityId;
                checkobj.ZipId = obj.CityId;
                checkobj.StreetAddress = obj.StreetAddress;
                checkobj.Appartment = obj.Appartment;
                checkobj.IsProfileUpdated = true;
                //  checkobj = Mapper.Map<User, tbl_User>(obj, checkobj);

                try
                {

                    if (!string.IsNullOrEmpty(obj.Image))
                    {
                        var userImageName = DateTime.Now.ToFileTimeUtc() + ".jpg";
                        File.WriteAllBytes(HttpContext.Current.Server.MapPath(Common.UserImagesPath) + userImageName, Convert.FromBase64String(obj.Image));

                        checkobj.Image = userImageName;
                    }
                }
                catch (Exception ex)
                {

                }
                db.SaveChanges();
                return GetUserByID(obj.Id);
                //return Mapper.Map<tbl_User, UserDataContract>(checkobj);


            }

            return null;
        }
        public ProfessionalDataContract UpdateProfessionalSubcategories(List<long?> subCategories, long userID)
        {

            if (subCategories.Count > 0)
            {
                UserSubCategoryMapping subCatMapping = new UserSubCategoryMapping();
                subCatMapping.DeleteSubCategoriesByUserId(userID);

                subCatMapping.AddUserSubcategories(userID, subCategories);
                return GetProfessionalByID(userID);
            }
            else
            {
                return new ProfessionalDataContract { ResultDescription = "No Category Was Supplied for updating", ResultStatus = 1 };
            }

        }
        public ProfessionalDataContract UpdateProfessionalProfile(User obj)
        {
            tbl_User checkobj = db.tbl_User.FirstOrDefault(a => a.Id == obj.Id && a.IsVerified == true && a.IsApprovedByAdmin == true);
            if (checkobj != null)
            {
                if (string.IsNullOrEmpty(checkobj.ProfileUrl))
                {
                    checkobj.ProfileUrl = CreateProfileUrl(obj.FirstName + obj.LastName, 0);
                }
                checkobj.FirstName = obj.FirstName;
                checkobj.LastName = obj.LastName;
                checkobj.Email = obj.Email;
                checkobj.CountryCode = obj.CountryCode;
                checkobj.PhoneNumber = obj.PhoneNumber;
                checkobj.CityId = obj.CityId;
                checkobj.StateId = obj.CityId;
                checkobj.ZipId = obj.CityId;
                checkobj.StreetAddress = obj.StreetAddress;
                checkobj.Appartment = obj.Appartment;
                checkobj.IsProfileUpdated = true;
                checkobj.CompanyName = obj.CompanyName;
                checkobj.Website = obj.Website;

                // checkobj = Mapper.Map<User, tbl_User>(obj, checkobj);


                if (!string.IsNullOrEmpty(obj.Image))
                {
                    var userImageName = DateTime.Now.ToFileTimeUtc() + ".jpg";
                    File.WriteAllBytes(HttpContext.Current.Server.MapPath(Common.UserImagesPath) + userImageName, Convert.FromBase64String(obj.Image));
                    checkobj.Image = userImageName;
                }

                db.SaveChanges();

                UserSubCategoryMapping subCatMapping = new UserSubCategoryMapping();
                subCatMapping.DeleteSubCategoriesByUserId(obj.Id);

                if (obj.SubCategoryIds.Count > 0)
                {

                    subCatMapping.AddUserSubcategories(obj.Id, obj.SubCategoryIds);
                }

                UserCityMapping cityMapping = new UserCityMapping();
                cityMapping.DeleteListByUserId(obj.Id);
                if (obj.CitiesIServeIds.Count > 0)
                {
                    cityMapping.AddCitiesIServe(obj.Id, obj.CitiesIServeIds);

                }

                return GetProfessionalByID(obj.Id);
                //var resultData =Mapper.Map<tbl_User, ProfessionalDataContract>(checkobj);


                //var locationData = db.tbl_State.FirstOrDefault(p => p.Id == checkobj.CityId);

                //if (locationData != null)
                //{
                //    resultData.WorkLocation = locationData.zip+" ("+locationData.City +", "+ locationData.State +")";
                //}
                //return resultData;
            }

            return null;
        }

        public User IsEmailAlreadyExists(string email)
        {
            var userData = db.tbl_User.FirstOrDefault(p => p.Email == email);
            if (userData != null)
            {
                var data = Mapper.Map<DataAccess.tbl_User, DataAccess.Classes.User>(userData);
                return data;
            }
            return null;
        }
        public bool IsSocialIDAlreadyExists(int socialType, string socialID)
        {
            var count = db.tbl_User.Count(p => p.SocialID == socialID && p.SocialType == socialType);
            if (count > 0)
                return true;
            return false;
        }
        #endregion

        public async void AddUpdateWindowAzureTokenForUser(Int64 userid, string deviceToken, string platform)
        {
            try
            {
                tbl_User usr = db.tbl_User.FirstOrDefault(p => p.Id == userid);
                // var userDevice = db.tbl_UserDevices.FirstOrDefault(p => p.DeviceToken == deviceToken && p.UserID == userid &&p.Platform== platform);
                if (usr != null && !string.IsNullOrEmpty(usr.DeviceToken) && (usr.Platform == "android" || usr.Platform == "ios" || usr.Platform == "window"))
                    if (usr != null)
                    {
                        string userId_str = usr.Id.ToString();
                        string str_id = "";

                        string[] arr_tags = { userId_str };
                        RegisterDeviceToken res = new RegisterDeviceToken();
                        RegisterDeviceToken.DeviceRegistration restration = new RegisterDeviceToken.DeviceRegistration();
                        restration.Handle = usr.DeviceToken;
                        restration.Tags = arr_tags;

                        if (usr.Platform == "android")
                            restration.Platform = "gcm";
                        else if (usr.Platform == "ios")
                            restration.Platform = "apns";
                        else if (usr.Platform == "window")
                            restration.Platform = "wns";


                        bool isIdRegistered = false;
                        if (!string.IsNullOrEmpty(usr.AzureNotificationId))
                        {
                            try
                            {
                                str_id = usr.AzureNotificationId;
                                
                                HttpStatusCode result = await res.UpdateWindowAzureTagsAndTokenById(str_id, restration);
                              
                                if (result == HttpStatusCode.OK)
                                {
                                    isIdRegistered = true;
                                }
                            }
                            catch (Exception a2)
                            {
                                await abc("error updated");
                            }
                        }


                        if (isIdRegistered == false)
                        {
                            try
                            {
                              
                                str_id = await res.RegisterWindowAzureIdForDeviceToken(usr.DeviceToken);
                                
                                if (str_id != null)
                                {
                                   
                                    try
                                    {
                                        HttpStatusCode result = await res.UpdateWindowAzureTagsAndTokenById(str_id, restration);
                                        
                                        if (result == HttpStatusCode.OK)
                                        {
                                            isIdRegistered = true;
                                        }
                                    }
                                    catch (Exception a2)
                                    {
                                        await abc(a2.Message);
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                await abc(ex.Message);
                            }
                        }



                        if (isIdRegistered == true) // update IsUpdateDeviceCodeToAzure Field and AzureNotificationId
                        {

                            string str_result = UpdateTableUserAzureNotificationId(usr.Id, str_id);

                        }


                    }
            }
            catch (Exception a1)
            { }

        }
        public async Task abc(string msg)
        {
            string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
            string EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();

            var client = new SmtpClient("smtp.gmail.com", 587)
            {


                Credentials = new NetworkCredential(FromEmail, EmailPassword),
                EnableSsl = true
            };
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add("ravi@impact-works.com");
            mailMessage.From = new MailAddress("support@referlocals.com", "Referlocals");
            mailMessage.Subject = "test";
            mailMessage.Body = msg;

            client.Send(mailMessage);
        }
        public async void AddUpdateWindowAzureTokenForPro(Int64 userid, string deviceToken, string platform)
        {
            try
            {
                
                tbl_User usr = db.tbl_User.FirstOrDefault(p => p.Id == userid);
                // var userDevice = db.tbl_UserDevices.FirstOrDefault(p => p.DeviceToken == deviceToken && p.UserID == userid &&p.Platform== platform);
                if (usr != null && !string.IsNullOrEmpty(usr.DeviceToken) && (usr.Platform == "android" || usr.Platform == "ios" || usr.Platform == "window"))
                    if (usr != null)
                    {
                        string userId_str = usr.Id.ToString();
                        string str_id = "";

                        string[] arr_tags = { userId_str };
                        RegisterDeviceTokenForPro res = new RegisterDeviceTokenForPro();
                        RegisterDeviceTokenForPro.DeviceRegistration restration = new RegisterDeviceTokenForPro.DeviceRegistration();
                        restration.Handle = usr.DeviceToken;
                        restration.Tags = arr_tags;

                        if (usr.Platform == "android")
                            restration.Platform = "gcm";
                        else if (usr.Platform == "ios")
                            restration.Platform = "apns";
                        else if (usr.Platform == "window")
                            restration.Platform = "wns";


                        bool isIdRegistered = false;
                        if (!string.IsNullOrEmpty(usr.AzureNotificationId))
                        {
                          
                            try
                            {
                                str_id = usr.AzureNotificationId;

                                HttpStatusCode result = await res.UpdateWindowAzureTagsAndTokenById(str_id, restration);
                                
                                if (result == HttpStatusCode.OK)
                                {
                                    isIdRegistered = true;
                                }
                            }
                            catch (Exception a2)
                            {
                                await abc("error updated");
                            }
                        }


                        if (isIdRegistered == false)
                        {
                            try
                            {
                               
                                str_id = await res.RegisterWindowAzureIdForDeviceToken(usr.DeviceToken);
                               
                                if (str_id != null)
                                {

                                    try
                                    {
                                        HttpStatusCode result = await res.UpdateWindowAzureTagsAndTokenById(str_id, restration);
                                       
                                        if (result == HttpStatusCode.OK)
                                        {
                                            isIdRegistered = true;
                                        }
                                    }
                                    catch (Exception a2)
                                    {
                                        await abc(a2.Message);
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                await abc(ex.Message);
                            }
                        }



                        if (isIdRegistered == true) // update IsUpdateDeviceCodeToAzure Field and AzureNotificationId
                        {

                            string str_result = UpdateTableUserAzureNotificationId(usr.Id, str_id);

                        }


                    }
            }
            catch (Exception a1)
            { await abc(a1.InnerException.Message); }
            //try
            //{
            //    tbl_User usr = db.tbl_User.FirstOrDefault(p => p.Id == userid);
            //    //var userDevice = db.tbl_UserDevices.FirstOrDefault(p => p.DeviceToken == deviceToken && p.UserID == userid && p.Platform== platform);
            //    if (usr != null && !string.IsNullOrEmpty(usr.DeviceToken) && (usr.Platform == "android" || usr.Platform == "ios" || usr.Platform == "window"))
            //        if (usr != null)
            //        {
            //            string userId_str = usr.Id.ToString();
            //            string str_id = "";

            //            string[] arr_tags = { userId_str };
            //            RegisterDeviceTokenForPro res = new RegisterDeviceTokenForPro();
            //            RegisterDeviceTokenForPro.DeviceRegistration restration = new RegisterDeviceTokenForPro.DeviceRegistration();
            //            restration.Handle = deviceToken;
            //            restration.Tags = arr_tags;

            //            if (usr.Platform == "android")
            //                restration.Platform = "gcm";
            //            else if (usr.Platform == "ios")
            //                restration.Platform = "apns";
            //            else if (usr.Platform == "window")
            //                restration.Platform = "wns";


            //            bool isIdRegistered = false;
            //            if (!string.IsNullOrEmpty(usr.AzureNotificationId))
            //            {
            //                try
            //                {
            //                    str_id = usr.AzureNotificationId;
            //                    HttpStatusCode result = await res.UpdateWindowAzureTagsAndTokenById(str_id, restration);

            //                    if (result == HttpStatusCode.OK)
            //                    {
            //                        isIdRegistered = true;
            //                    }
            //                }
            //                catch (Exception a2)
            //                { }
            //            }


            //            if (isIdRegistered == false)
            //            {

            //                str_id = await res.RegisterWindowAzureIdForDeviceToken(deviceToken);
            //                if (str_id != null)
            //                {
            //                    try
            //                    {
            //                        HttpStatusCode result = await res.UpdateWindowAzureTagsAndTokenById(str_id, restration);

            //                        if (result == HttpStatusCode.OK)
            //                        {
            //                            isIdRegistered = true;
            //                        }
            //                    }
            //                    catch (Exception a2)
            //                    { }

            //                }
            //            }



            //            if (isIdRegistered == true) // update IsUpdateDeviceCodeToAzure Field and AzureNotificationId
            //            {

            //                string str_result = UpdateTableUserAzureNotificationId(usr.Id, str_id);

            //            }


            //        }
            //}
            //catch (Exception a1)
            //{ }

        }
        public string UpdateTableUserAzureNotificationId(long userID, string azureNotificationID)
        {
            tbl_User usr = db.tbl_User.FirstOrDefault(p => p.Id == userID);
            if (usr != null)
            {
                usr.AzureNotificationId = azureNotificationID;
                usr.CanSendPushNotification = false;
                db.SaveChanges();

            }
            return azureNotificationID;
        }


    }
    public class ProfessionalUrls
    {
        public string SubCategoryName { get; set; }
        public long SubCategoryID { get; set; }

    }
}
