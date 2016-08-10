using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class Referal : dbContext
    {
        //IMapper mapper;
        //public Referal()
        //{
        //    mapper = new AutoMapperWebConfiguration().mapper;
        //}
        #region"properties"
        public bool IsViewed { get; set; }
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

        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
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

        private Int64 _CheckIsRefered = (int)HelperEnums.BooleanValues.Both;
        public Int64 CheckIsRefered
        {
            get { return _CheckIsRefered; }
            set { _CheckIsRefered = value; }
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
        public bool Add(Referal obj)
        {
            try
            {
                tbl_Referal checkobj = db.tbl_Referal.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj == null)
                {
                    //AutoMapper.Mapper.CreateMap<Referal, tbl_Referal>();
                    //tbl_Referal newobj = Mapper.Map<Referal, tbl_Referal>(obj);
                    checkobj = new tbl_Referal();
                    checkobj.IsViewed = false;
                    checkobj.CityId = obj.CityId;
                    checkobj.Comment = obj.Comment;
                    checkobj.CreatedDate = obj.CreatedDate;
                    checkobj.IsApprovedByAdmin = obj.IsApprovedByAdmin;
                    checkobj.IsDeleted = obj.IsDeleted;
                    checkobj.IsFlag = obj.IsFlag;
                    checkobj.IsRefered = obj.IsRefered;
                    checkobj.IsSatisfied = obj.IsSatisfied;
                    checkobj.SenderId = obj.SenderId;
                    checkobj.UpdatedDate = obj.UpdatedDate;
                    checkobj.UserId = obj.UserId;

                    db.tbl_Referal.Add(checkobj);
                    db.SaveChanges();
                    var proData= db.tbl_User.FirstOrDefault(p => p.Id == checkobj.UserId&&p.Type==(int)HelperEnums.UserType.Professional);
                    if (proData!= null)
                    {
                        if (proData.IsApprovedByAdmin == true)
                        {
                            var userData =db.tbl_User.FirstOrDefault(p=>p.Id==obj.SenderId);
                            if (userData != null)
                            {
                                var emailBody = Common.EmailBodyOnReferral.Replace("##ProfessionalName##", proData.FirstName + " " + proData.LastName)
                                    .Replace("##Username##", userData.FirstName + " " + userData.LastName)
                                    .Replace("##ProfileLink##", "/Profile_dashboard");
                                Common.SendEmailWithGenericTemplate(proData.Email, Common.EmailSubjectOnReferral, emailBody);

                                #region PushNoitification
                                Task.Run(async () =>
                                {
                                    
                                    NotificationSetting notificationSettings = new NotificationSetting();
                                    var notificationSettingData = notificationSettings.GetByUserID(proData.Id);
                                    if (notificationSettingData != null)
                                    {
                                        if (notificationSettingData.Recommendations.GetValueOrDefault())
                                        {
                                            var dataForPush = new NewUpdateWithPush
                                            {
                                                NewProfessionalUpdates= new User().GetNewProfessionalUpdates(proData.Id)
                                                
                                            };
                                            var jsonString = JsonConvert.SerializeObject(dataForPush);
                                            DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                                            {
                                                await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), userData.FirstName + " " + userData.LastName + " referred you on Referlocals", HelperEnums.PushNotificationType.ReferredPro, jsonString);
                                            }
                                        }

                                    }
                                    else
                                    {

                                        var dataForPush = new NewUpdateWithPush
                                        {
                                            NewProfessionalUpdates = new User().GetNewProfessionalUpdates(proData.Id)

                                        };
                                        var jsonString = JsonConvert.SerializeObject(dataForPush);
                                        DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                                        {
                                            await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), proData.FirstName + " " + proData.LastName + " referred you on Referlocals", HelperEnums.PushNotificationType.ReferredPro, jsonString);
                                        }
                                    }
                                });
                                #endregion
                            }
                        }
                    }
                    Id = checkobj.Id;
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public bool Edit(Referal obj)
        {
            try
            {
                //AutoMapper.Mapper.CreateMap<Referal, tbl_Referal>();
                tbl_Referal checkobj = db.tbl_Referal.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    checkobj = Mapper.Map<Referal, tbl_Referal>(obj, checkobj);
                    db.SaveChanges();
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {

            }
            return DataRecieved;
        }

        public bool Delete(Referal obj)
        {
            try
            {
                tbl_Referal checkobj = db.tbl_Referal.FirstOrDefault(a => a.Id == obj.Id);
                if (checkobj != null)
                {
                    db.tbl_Referal.Remove(checkobj);
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
                List<tbl_Referal> checkobj = db.tbl_Referal.Where(a => a.UserId == UserId).ToList();
                if (checkobj != null && checkobj.Count > 0)
                {
                    foreach (var item in checkobj)
                    {
                        db.tbl_Referal.Remove(item);
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

        public List<Referal> GetAll()
        {
            List<Referal> lst = new List<Referal>();
            try
            {
                if (Take != 0 && Index == 0)
                {
                    lst = (from r in db.tbl_Referal
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SenderId != 0 ? r.SenderId == SenderId : SenderId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new Referal
                           {
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               SenderId = r.SenderId,
                               UserCityId = r.tbl_User.CityId,
                               Comment = r.Comment,
                               Image = r.tbl_User.Image,
                               IsFlag = r.IsFlag == true ? true : false,
                               IsSatisfied = r.IsSatisfied == true ? true : false,
                               IsRefered = r.IsRefered == true ? true : false,
                               CityName = r.tbl_State.City,
                               Zip = r.tbl_State.zip,
                               SenderName = (r.tbl_User.FirstName + " " + r.tbl_User.LastName),
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).Take(Convert.ToInt32(Take)).ToList();

                }
                else if (Take != 0 && Index != 0)
                {
                    lst = (from r in db.tbl_Referal
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SenderId != 0 ? r.SenderId == SenderId : SenderId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new Referal
                           {
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               SenderId = r.SenderId,
                               Comment = r.Comment,
                               UserCityId = r.tbl_User.CityId,
                               Image = r.tbl_User.Image,
                               IsFlag = r.IsFlag == true ? true : false,
                               IsSatisfied = r.IsSatisfied == true ? true : false,
                               IsRefered = r.IsRefered == true ? true : false,
                               CityName = r.tbl_State.City,
                               Zip = r.tbl_State.zip,
                               SenderName = (r.tbl_User.FirstName + " " + r.tbl_User.LastName),
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
                }
                else
                {
                    lst = (from r in db.tbl_Referal
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SenderId != 0 ? r.SenderId == SenderId : SenderId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new Referal
                           {
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               UserCityId = r.tbl_User.CityId,
                               SenderId = r.SenderId,
                               Image = r.tbl_User.Image,
                               Comment = r.Comment,
                               IsFlag = r.IsFlag == true ? true : false,
                               IsSatisfied = r.IsSatisfied == true ? true : false,
                               IsRefered = r.IsRefered == true ? true : false,
                               CityName = r.tbl_State.City,
                               Zip = r.tbl_State.zip,
                               SenderName = (r.tbl_User.FirstName + " " + r.tbl_User.LastName),
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).ToList();
                }
                lst = lst.Select((r, index) => new Referal
                {
                    Id = r.Id,
                    CityId = r.CityId,
                    UserId = r.UserId,
                    UserCityId = r.UserCityId,
                    SenderId = r.SenderId,
                    Comment = r.Comment,
                    Image = r.Image,
                    IsFlag = r.IsFlag,
                    IsSatisfied = r.IsSatisfied,
                    IsRefered = r.IsRefered,
                    CityName = r.CityName,
                    Zip = r.Zip,
                    SenderName = r.SenderName,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    CreatedBy = r.CreatedBy,
                    IsDeleted = r.IsDeleted,
                    IsApprovedByAdmin = r.IsApprovedByAdmin,
                    SNO = index + 1,
                }).ToList();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return lst;
        }

        public List<PropReferal> GetAllReferals(long userID, HelperEnums.BooleanValues orderBy, int take, int index)
        {//(CheckIsRefered == (int)HelperEnums.BooleanValues.Approved ? r.IsRefered == true : CheckIsRefered == (int)HelperEnums.BooleanValues.Disapproved ? r.IsRefered == false : CheckIsRefered == (int)HelperEnums.BooleanValues.Both)
            List<PropReferal> lst = new List<PropReferal>();


            var query = (from r in db.tbl_Referal
                         where r.UserId == userID

                         select new PropReferal
                         {
                             Id = r.Id,
                             CityId = r.CityId,
                             UserId = r.UserId,
                             SenderType=r.tbl_User.Type,
                             SenderId = r.SenderId,
                             SenderUniqueId=r.tbl_User.UniqueId,
                             UserCityId = r.tbl_User.CityId,
                             Image = r.tbl_User.Image,
                             Comment = r.Comment,
                             IsFlag = r.IsFlag == true ? true : false,
                             IsSatisfied = r.IsSatisfied == true ? true : false,
                             IsRefered = r.IsRefered == true ? true : false,
                             CityName = r.tbl_State.City,
                             Zip = r.tbl_State.zip,
                             SenderName = (r.tbl_User.FirstName + " " + r.tbl_User.LastName),
                             CreatedDate = r.CreatedDate,
                             UpdatedDate = r.UpdatedDate,
                             CreatedBy = r.CreatedBy,
                             IsDeleted = r.IsDeleted == true ? true : false,
                             IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                         });

            if (orderBy == HelperEnums.BooleanValues.Both)
            {
                query = query.OrderByDescending(p => p.Id);
            }
            else if (orderBy == HelperEnums.BooleanValues.Approved)
            {
                query = query.OrderByDescending(p => p.IsRefered);
            }
            else if (orderBy == HelperEnums.BooleanValues.Disapproved)
            {
                query = query.OrderBy(p => p.IsRefered);
            }
            lst = query.Skip((take * index)).Take(take).ToList();
            return lst;
        }
        public List<PropReferal> GetAllForAjax()
        {
            List<PropReferal> lst = new List<PropReferal>();
            try
            {
                if (Take != 0 && Index == 0)
                {
                    lst = (from r in db.tbl_Referal
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SenderId != 0 ? r.SenderId == SenderId : SenderId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                               (CheckIsRefered == (int)HelperEnums.BooleanValues.Approved ? r.IsRefered == true : CheckIsRefered == (int)HelperEnums.BooleanValues.Disapproved ? r.IsRefered == false : CheckIsRefered == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new PropReferal
                           {
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               SenderId = r.SenderId,
                               UserCityId = r.tbl_User.CityId,
                               Image = r.tbl_User.Image,
                               Comment = r.Comment,
                               IsFlag = r.IsFlag == true ? true : false,
                               IsSatisfied = r.IsSatisfied == true ? true : false,
                               IsRefered = r.IsRefered == true ? true : false,
                               CityName = r.tbl_State.City,
                               Zip = r.tbl_State.zip,
                               SenderName = (r.tbl_User.FirstName + " " + r.tbl_User.LastName),
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).Take(Convert.ToInt32(Take)).ToList();

                }
                else if (Take != 0 && Index != 0)
                {
                    lst = (from r in db.tbl_Referal
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SenderId != 0 ? r.SenderId == SenderId : SenderId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                               (CheckIsRefered == (int)HelperEnums.BooleanValues.Approved ? r.IsRefered == true : CheckIsRefered == (int)HelperEnums.BooleanValues.Disapproved ? r.IsRefered == false : CheckIsRefered == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new PropReferal
                           {
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               SenderId = r.SenderId,
                               UserCityId = r.tbl_User.CityId,
                               Comment = r.Comment,
                               Image = r.tbl_User.Image,
                               IsFlag = r.IsFlag == true ? true : false,
                               IsSatisfied = r.IsSatisfied == true ? true : false,
                               IsRefered = r.IsRefered == true ? true : false,
                               CityName = r.tbl_State.City,
                               Zip = r.tbl_State.zip,
                               SenderName = (r.tbl_User.FirstName + " " + r.tbl_User.LastName),
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).OrderBy(a => a.Id).Skip(Convert.ToInt32(Take * Index)).Take(Convert.ToInt32(Take)).ToList();
                }
                else
                {
                    lst = (from r in db.tbl_Referal
                           where (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SenderId != 0 ? r.SenderId == SenderId : SenderId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                               (CheckIsRefered == (int)HelperEnums.BooleanValues.Approved ? r.IsRefered == true : CheckIsRefered == (int)HelperEnums.BooleanValues.Disapproved ? r.IsRefered == false : CheckIsRefered == (int)HelperEnums.BooleanValues.Both)
                           )
                           select new PropReferal
                           {
                               Id = r.Id,
                               CityId = r.CityId,
                               UserId = r.UserId,
                               SenderId = r.SenderId,
                               UserCityId = r.tbl_User.CityId,
                               Comment = r.Comment,
                               Image = r.tbl_User.Image,
                               IsFlag = r.IsFlag == true ? true : false,
                               IsSatisfied = r.IsSatisfied == true ? true : false,
                               IsRefered = r.IsRefered == true ? true : false,
                               CityName = r.tbl_State.City,
                               Zip = r.tbl_State.zip,
                               SenderName = (r.tbl_User.FirstName + " " + r.tbl_User.LastName),
                               CreatedDate = r.CreatedDate,
                               UpdatedDate = r.UpdatedDate,
                               CreatedBy = r.CreatedBy,
                               IsDeleted = r.IsDeleted == true ? true : false,
                               IsApprovedByAdmin = r.IsApprovedByAdmin == true ? true : false
                           }).ToList();
                }
                lst = lst.Select((r, index) => new PropReferal
                {
                    Id = r.Id,
                    CityId = r.CityId,
                    UserId = r.UserId,
                    SenderId = r.SenderId,
                    UserCityId = r.UserCityId,
                    Comment = r.Comment,
                    IsFlag = r.IsFlag,
                    Image = r.Image,
                    IsSatisfied = r.IsSatisfied,
                    IsRefered = r.IsRefered,
                    CityName = r.CityName,
                    Zip = r.Zip,
                    SenderName = r.SenderName,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    CreatedBy = r.CreatedBy,
                    IsDeleted = r.IsDeleted,
                    IsApprovedByAdmin = r.IsApprovedByAdmin,
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
                tbl_Referal obj = db.tbl_Referal.FirstOrDefault(r => (
                               (Id != 0 ? r.Id == Id : Id == 0) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SenderId != 0 ? r.SenderId == SenderId : SenderId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                               (CheckIsRefered == (int)HelperEnums.BooleanValues.Approved ? r.IsRefered == true : CheckIsRefered == (int)HelperEnums.BooleanValues.Disapproved ? r.IsRefered == false : CheckIsRefered == (int)HelperEnums.BooleanValues.Both)
                           ));
                if (obj != null)
                {
                    Id = obj.Id;
                    CityId = obj.CityId;
                    UserId = obj.UserId;
                    SenderId = obj.SenderId;
                    UserCityId = obj.tbl_User.CityId;
                    Comment = obj.Comment;
                    Image = obj.tbl_User.Image;
                    IsFlag = Convert.ToBoolean(obj.IsFlag);
                    IsSatisfied = Convert.ToBoolean(obj.IsSatisfied);
                    IsRefered = Convert.ToBoolean(obj.IsRefered);
                    CityName = obj.tbl_State.City;
                    Zip = obj.tbl_State.zip;
                    SenderName = (obj.tbl_User.FirstName + " " + obj.tbl_User.LastName);
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

        #region "TotalRecords"
       

        public Int64 GetTotalRecords()
        {
            Int64 Records = 0;
            try
            {
                Records = db.tbl_Referal.Count(r => (Id != 0 ? r.Id == Id : Id == 0) &&
                               (UserId != 0 ? r.UserId == UserId : UserId == 0) &&
                               (SenderId != 0 ? r.SenderId == SenderId : SenderId == 0) &&
                               (CityId != 0 ? r.CityId == CityId : CityId == 0) &&
                               (!string.IsNullOrEmpty(CityName) ? r.tbl_State.City.ToLower() == CityName.ToLower() : string.IsNullOrEmpty(CityName)) &&
                               (IsDeleted == true ? r.IsDeleted == true : r.IsDeleted == false) &&
                               (IsApproved == (int)HelperEnums.BooleanValues.Approved ? r.IsApprovedByAdmin == true : IsApproved == (int)HelperEnums.BooleanValues.Disapproved ? r.IsApprovedByAdmin == false : IsApproved == (int)HelperEnums.BooleanValues.Both) &&
                               (CheckIsRefered == (int)HelperEnums.BooleanValues.Approved ? r.IsRefered == true : CheckIsRefered == (int)HelperEnums.BooleanValues.Disapproved ? r.IsRefered == false : CheckIsRefered == (int)HelperEnums.BooleanValues.Both));
            }
            catch (Exception ex)
            {

            }
            return Records;
        }
        #endregion

        public ReferalListWrapper GetAllReferalsBySenderID(long SenderID, int pageIndex, int pageSize,HelperEnums.BooleanValues orderBy)
        {

            var countReferals = (from r in db.tbl_Referal
                                 join u in db.tbl_User on r.UserId equals u.Id
                                 join c in db.tbl_State on u.CityId equals c.Id

                                 where r.SenderId == SenderID
                                 && r.IsApprovedByAdmin == true && r.IsDeleted == false
                                 && u.IsApprovedByAdmin == true
                                 select r).Count();

                    decimal totalpages = Math.Ceiling(Convert.ToDecimal(countReferals) / Convert.ToDecimal(pageSize));
            List<PropReferal> lst = new List<PropReferal>();


            var query = (from r in db.tbl_Referal
                         join u in db.tbl_User on r.UserId equals u.Id
                         join c in db.tbl_State on u.CityId equals c.Id

                         where r.SenderId == SenderID
                         && r.IsApprovedByAdmin == true && r.IsDeleted == false
                         && u.IsApprovedByAdmin == true
                         select new PropReferal
                         {
                             Id = r.Id,
                             CityId = r.CityId,
                             UserId = r.UserId,
                             SenderId = r.SenderId,
                             
                             UserUniqueId=u.UniqueId,//r.tbl_User.UniqueId,
                             UserCityId = u.CityId,//r.tbl_User.CityId,
                             Comment = r.Comment,
                             IsFlag = r.IsFlag == true ? true : false,
                             IsSatisfied = r.IsSatisfied == true ? true : false,
                             IsRefered = r.IsRefered == true ? true : false,
                             CityName =c.City + " " + c.State ,
                             Zip = r.tbl_State.zip,
                             ProfessionalName = (u.FirstName + " " + u.LastName),
                             Image = u.Image

                         });

            if (orderBy == HelperEnums.BooleanValues.Both)
            {
                query = query.OrderByDescending(p => p.Id);
            }
            else if (orderBy == HelperEnums.BooleanValues.Approved)
            {
                query = query.OrderByDescending(p => p.IsRefered);
            }
            else if (orderBy == HelperEnums.BooleanValues.Disapproved)
            {
                query = query.OrderBy(p => p.IsRefered);
            }
           
            var data = query.Skip(pageSize * pageIndex).Take(pageSize).ToList();
            //data.ForEach(p=>p.)
            ReferalListWrapper referals = new ReferalListWrapper();
            referals.ReferalCount = countReferals;
            if (totalpages <= (pageIndex + 1))
            {
                referals.HideShowMore = true;
            }
            else
            {
                referals.HideShowMore = false;
            }
            referals.ReferalCount = countReferals;
            referals.Referals = data;
            return referals;
        }
        public ReferalListWrapper GetAllReferalsByUserID(long userID, int pageIndex, int pageSize)
        {

            var countReferals = (from r in db.tbl_Referal
                                 join u in db.tbl_User
                                 on r.UserId equals u.Id
                                 where r.SenderId == userID
                                 && r.IsApprovedByAdmin == true && r.IsDeleted == false
                                 && u.IsApprovedByAdmin == true && u.IsDeleted == false
                                 select r).Count();

                    decimal totalpages = Math.Ceiling(Convert.ToDecimal(countReferals) / Convert.ToDecimal(pageSize));
            List<PropReferal> lst = new List<PropReferal>();


            var query = (from r in db.tbl_Referal
                         join u in db.tbl_User
                         on r.UserId equals u.Id                         
                         where r.SenderId == userID
                         && r.IsApprovedByAdmin == true && r.IsDeleted == false
                         &&u.IsApprovedByAdmin==true&&u.IsDeleted==false
                         select new PropReferal
                         {
                             Id = r.Id,
                             CityId = r.CityId,
                             UserId = r.UserId,
                             SenderId = r.SenderId,
                             UserCityId = r.tbl_User.CityId,                             
                             Comment = r.Comment,
                             IsFlag = r.IsFlag == true ? true : false,
                             IsSatisfied = r.IsSatisfied == true ? true : false,
                             IsRefered = r.IsRefered == true ? true : false,
                             CityName = r.tbl_State.City + " " + r.tbl_State.State ,
                             Zip = r.tbl_State.zip,
                             ProfessionalName = (u.FirstName + " " + u.LastName),
                            
                         }).OrderByDescending(p => p.Id).Skip(pageSize * pageIndex).Take(pageSize);


            var data = query.ToList();
           
            ReferalListWrapper referals = new ReferalListWrapper();
            referals.ReferalCount = countReferals;
            if (totalpages <= (pageIndex + 1))
            {
                referals.HideShowMore = true;
            }
            else
            {
                referals.HideShowMore = false;
            }
            referals.ReferalCount = countReferals;
            referals.Referals= data;
            return referals;
        }

        public ReferalListWrapper GetWhoReferedMe(long professionalID, int pageIndex, int pageSize)
        {

            var countReferals = db.tbl_Referal.Count(j => j.UserId == professionalID && j.IsApprovedByAdmin == true && j.IsDeleted == false);

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countReferals) / Convert.ToDecimal(pageSize));
            List<PropReferal> lst = new List<PropReferal>();


            var query = (from r in db.tbl_Referal
                         join u in db.tbl_User
                         on r.UserId equals u.Id
                         where r.UserId == professionalID
                         && r.IsApprovedByAdmin == true && r.IsDeleted == false
                         select new PropReferal
                         {
                             Id = r.Id,
                             CityId = r.CityId,
                             UserId = r.UserId,
                             SenderId = r.SenderId,
                             UserCityId = r.tbl_User.CityId,
                             Comment = r.Comment,
                             IsFlag = r.IsFlag == true ? true : false,
                             IsSatisfied = r.IsSatisfied == true ? true : false,
                             IsRefered = r.IsRefered == true ? true : false,
                             CityName = r.tbl_State.City + " " + r.tbl_State.State ,
                             Zip = r.tbl_State.zip,
                             UserName = (u.FirstName + " " + u.LastName),
                             CreatedDate=r.CreatedDate,
                         }).OrderByDescending(p => p.Id).Skip(pageSize * pageIndex).Take(pageSize);


            var data = query.ToList();
            //data.ForEach(p=>p.)
            ReferalListWrapper referals = new ReferalListWrapper();
            referals.ReferalCount = countReferals;
            if (totalpages <= (pageIndex + 1))
            {
                referals.HideShowMore = true;
            }
            else
            {
                referals.HideShowMore = false;
            }
            referals.ReferalCount = countReferals;
            referals.Referals = data;
            return referals;
        }

        #region API Methods
        public bool FlagReferral(long referralID)
        {
            var referral = db.tbl_Referal.FirstOrDefault(p => p.Id == referralID);
            if(referral!=null)
            {
                referral.IsFlag = true;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public ReferralWithUserDataWrapper GetReferralsGiven(long SenderID, int pageIndex, int pageSize, HelperEnums.BooleanValues orderBy)
        {

            var countReferals = (from r in db.tbl_Referal
                                 join u in db.tbl_User
                                 on r.UserId equals u.Id
                                 join c in db.tbl_State
                                 on u.CityId equals c.Id
                                 where r.SenderId == SenderID
                                 && r.IsApprovedByAdmin == true && r.IsDeleted == false
                                 && u.IsApprovedByAdmin == true && u.IsDeleted == false
                                 select r
                                 ).Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countReferals) / Convert.ToDecimal(pageSize));
            List<ReferralWithUserData> lst = new List<ReferralWithUserData>();


            var query = (from r in db.tbl_Referal
                         join u in db.tbl_User
                         on r.UserId equals u.Id
                         join c in db.tbl_State
                         on u.CityId equals c.Id
                         where r.SenderId == SenderID
                         && r.IsApprovedByAdmin == true && r.IsDeleted == false
                         && u.IsApprovedByAdmin == true && u.IsDeleted == false
                         select new ReferralWithUserData
                         {
                             ShareUrl = Common.WebsiteHostNameForLink + "/ReferDetail?Id=" + u.UniqueId,
                             //Location=c==null?"":c.City+" "+c.State+" "+c.zip,
                             Location = c == null ? "" : c.City + " " + c.State ,
                             LocationID =u.CityId,
                             UserType = (HelperEnums.UserType)u.Type,
                             IsFavorite = db.tbl_Favorite.Count(p => p.FavoriteTypeID == u.Id && p.Favoritetype == (u.Type == 2 ? (int)(HelperEnums.FavoriteType.Professional) : (int)(HelperEnums.FavoriteType.User)) && p.UserID == SenderID) > 0 ? true : false,
                             IsFlag = r.IsFlag,
                             ID =r.Id,
                             UserID=u.Id,
                             Comment = r.Comment,
                             IsSatisfied = r.IsSatisfied == true ? true : false,
                             IsReferred = r.IsRefered == true ? true : false,
                             Username = (u.FirstName + " " + u.LastName),
                             UserImage =string.IsNullOrEmpty(u.Image)?Common.NoImageIcon:Common.UserImagesPath+u.Image,
                              SubCategories = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == u.Id)
                                           .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                           (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                         });

            if (orderBy == HelperEnums.BooleanValues.Both)
            {
                query = query.OrderByDescending(p => p.ID);
            }
            else if (orderBy == HelperEnums.BooleanValues.Approved)
            {
                query = query.OrderByDescending(p => p.IsReferred);
            }
            else if (orderBy == HelperEnums.BooleanValues.Disapproved)
            {
                query = query.OrderBy(p => p.IsReferred);
            }

            var data = query.Skip(pageSize * pageIndex).Take(pageSize).ToList();
            //data.ForEach(p=>p.)
            ReferralWithUserDataWrapper referals = new ReferralWithUserDataWrapper();
            
            if (totalpages <= (pageIndex + 1))
            {
                referals.HideShowMore = true;
            }
            else
            {
                referals.HideShowMore = false;
            }
            referals.ReferralList = data;
            return referals;
        }
        public void UpdateReferralIsViewed(long userID)
        {
            db.Database.ExecuteSqlCommand("update tbl_Referal set isviewed=1 where userid=@userID", new SqlParameter("@userID", userID));
        }
        public ReferralWithUserDataWrapper GetReferrals(long userID, int pageIndex, int pageSize)
        {
            UpdateReferralIsViewed(userID);
            var countReferals = db.tbl_Referal.Count(j => j.UserId == userID && j.IsApprovedByAdmin == true && j.IsDeleted == false);

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countReferals) / Convert.ToDecimal(pageSize));
            List<ReferralWithUserData> lst = new List<ReferralWithUserData>();


            var query = (from r in db.tbl_Referal
                         join u in db.tbl_User
                         on r.SenderId equals u.Id
                         join l in db.tbl_State
                         on u.CityId equals l.Id into lc
                         from location in lc.DefaultIfEmpty()
                         where r.UserId == userID
                         && r.IsApprovedByAdmin == true && r.IsDeleted == false
                         select new ReferralWithUserData
                         {
                             //Location=location!=null?location.City+", "+location.State+", "+location.zip:"",
                             Location = location != null ? location.City + ", " + location.State : "",
                             UserType =(HelperEnums.UserType)u.Type,
                             IsFlag=r.IsFlag,
                             UserID=u.Id,
                             ID = r.Id,
                             Comment = r.Comment,
                             IsSatisfied = r.IsSatisfied == true ? true : false,
                             IsReferred = r.IsRefered == true ? true : false,
                             Username = (u.FirstName + " " + u.LastName),
                             UserImage = string.IsNullOrEmpty(u.Image) ? Common.NoImageIcon : Common.UserImagesPath + u.Image,
                             SubCategories = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == u.Id)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),


                         }).OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize);


            var data = query.ToList();

            ReferralWithUserDataWrapper referals = new ReferralWithUserDataWrapper();
            
            if (totalpages <= (pageIndex + 1))
            {
                referals.HideShowMore = true;
            }
            else
            {
                referals.HideShowMore = false;
            }
           
            referals.ReferralList = data;
            return referals;
        }
        #endregion
    }
}
