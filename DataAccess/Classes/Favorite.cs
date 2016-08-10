using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class Favorite : dbContext
    {
        public long? ID { get; set; }
        public long? UserID { get; set; }
        public long? FavoriteTypeID { get; set; }
        public int? FavoriteType { get; set; }
        public DateTime? AddedOn{ get; set; }
        public ResultData Save(long? userID, long? favoriteTypeID, HelperEnums.FavoriteType favoriteType, bool status)
        {
            tbl_Favorite favorite = db.tbl_Favorite.FirstOrDefault(p => p.UserID == userID &&
            p.FavoriteTypeID == favoriteTypeID && p.Favoritetype == (int)favoriteType);

            if (favorite != null)
            {
                if (status == false)
                {
                    db.tbl_Favorite.Remove(favorite);
                    db.SaveChanges();
                    return new ResultData { ResultDescription = "Favorite Deleted Successfully", ResultStatus = 1 };
                }
                else
                {
                    return new ResultData { ResultDescription = "Favorite Added Successfully", ResultStatus = 1 };

                }
            }
            else
            {
                if (status)
                {

                    favorite = new tbl_Favorite();
                    favorite.AddedOn = DateTime.UtcNow;
                    favorite.IsViewed = false;
                    favorite.Favoritetype = (int)favoriteType;
                    favorite.FavoriteTypeID = favoriteTypeID;
                    favorite.UserID = userID;

                    db.tbl_Favorite.Add(favorite);

                    db.SaveChanges();

                    #region PushNoitification
                    if (favoriteType == HelperEnums.FavoriteType.Professional)
                    {
                        Task.Run(async () =>
                        {

                            NotificationSetting notificationSettings = new NotificationSetting();
                            var notificationSettingData = notificationSettings.GetByUserID(favorite.FavoriteTypeID);
                            if (notificationSettingData != null)
                            {
                                if (notificationSettingData.NewFollowers.GetValueOrDefault())
                                {
                                    var user = new User();
                                    var username = user.GetUsernameByUserID(favorite.UserID.Value);
                                    var proData = user.GetProfessionalByID(favorite.FavoriteTypeID.Value);
                                    var dataForPush = new NewUpdateWithPush
                                    {
                                        NewProfessionalUpdates = new User().GetNewProfessionalUpdates(proData.Id)

                                    };
                                    var jsonString = JsonConvert.SerializeObject(dataForPush);

                                    DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                                    {
                                        await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                                    }
                                }
                            }
                            else
                            {
                                var user = new User();
                                var username = user.GetUsernameByUserID(favorite.UserID.Value);
                                var proData = user.GetProfessionalByID(favorite.FavoriteTypeID.Value);
                                var dataForPush = new NewUpdateWithPush
                                {
                                    NewProfessionalUpdates = new User().GetNewProfessionalUpdates(proData.Id)

                                };
                                var jsonString = JsonConvert.SerializeObject(dataForPush);

                                DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                                {
                                    await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                                }
                            }

                        });
                    }
                    else if (favoriteType == HelperEnums.FavoriteType.User)
                    {
                        Task.Run(async () =>
                        {

                            NotificationSetting notificationSettings = new NotificationSetting();
                            var notificationSettingData = notificationSettings.GetByUserID(favorite.FavoriteTypeID);
                            if (notificationSettingData != null)
                            {
                                if (notificationSettingData.NewFollowers.GetValueOrDefault())
                                {
                                    var user = new User();
                                    var username = user.GetUsernameByUserID(favorite.UserID.Value);
                                    var proData = user.GetUserByID(favorite.FavoriteTypeID.Value);

                                    var dataForPush = new NewUpdateWithPush
                                    {
                                        NewUserUpdates = new User().GetNewUserUpdates(proData.Id)

                                    };
                                    var jsonString = JsonConvert.SerializeObject(dataForPush);

                                    DataAccess.WindowAzurePushNotification.Notifications pro = new DataAccess.WindowAzurePushNotification.Notifications();
                                    {
                                        await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                                    }
                                }
                            }
                            else
                            {
                                var user = new User();
                                var username = user.GetUsernameByUserID(favorite.UserID.Value);
                                var proData = user.GetUserByID(favorite.FavoriteTypeID.Value);
                                var dataForPush = new NewUpdateWithPush
                                {
                                    NewUserUpdates = new User().GetNewUserUpdates(proData.Id)

                                };
                                var jsonString = JsonConvert.SerializeObject(dataForPush);

                                DataAccess.WindowAzurePushNotification.Notifications pro = new DataAccess.WindowAzurePushNotification.Notifications();
                                {
                                    await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                                }
                            }

                        });
                    }
                    #endregion
                    return new ResultData { ResultDescription = "Favorite Added Successfully", ResultStatus = 1 };
                }
                else
                {
                    return new ResultData { ResultDescription = "Favorite Deleted Successfully", ResultStatus = 1 };
                }

            }
        }
        public ResultData Save(long? userID,long? favoriteTypeID,HelperEnums.FavoriteType favoriteType )
        {
            tbl_Favorite favorite = db.tbl_Favorite.FirstOrDefault(p => p.UserID == userID && p.FavoriteTypeID == favoriteTypeID && p.Favoritetype == (int)favoriteType);
            if (favorite!=null)
            {
                
                db.tbl_Favorite.Remove(favorite);
                db.SaveChanges();
                return new ResultData { ResultDescription = "Favorite Deleted Successfully", ResultStatus = 1 };
            }
            else
            {
                favorite = new tbl_Favorite();
                favorite.IsViewed = false;
                favorite.AddedOn = DateTime.UtcNow;
                favorite.Favoritetype = (int)favoriteType;
                favorite.FavoriteTypeID = favoriteTypeID;
                favorite.UserID = userID;

                db.tbl_Favorite.Add(favorite);

                db.SaveChanges();
                
                

                    #region PushNoitification
                    if (favoriteType == HelperEnums.FavoriteType.Professional)
                    {
                        Task.Run(async () =>
                        {

                            NotificationSetting notificationSettings = new NotificationSetting();
                            var notificationSettingData = notificationSettings.GetByUserID(favorite.FavoriteTypeID);
                            if (notificationSettingData != null)
                            {
                                if (notificationSettingData.NewFollowers.GetValueOrDefault())
                                {
                                    var user = new User();
                                    var username = user.GetUsernameByUserID(favorite.UserID.Value);
                                    var proData = user.GetProfessionalByID(favorite.FavoriteTypeID.Value);
                                    var jsonString = "{}";
                                    DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                                    {
                                        await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                                    }
                                }
                            }
                            else
                            {
                                var user = new User();
                                var username = user.GetUsernameByUserID(favorite.UserID.Value);
                                var proData = user.GetProfessionalByID(favorite.FavoriteTypeID.Value);
                                var jsonString = "{}";
                                DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                                {
                                    await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                                }
                            }

                        });
                    }
                    else if (favoriteType == HelperEnums.FavoriteType.User)
                    {
                        Task.Run(async () =>
                        {

                            NotificationSetting notificationSettings = new NotificationSetting();
                            var notificationSettingData = notificationSettings.GetByUserID(favorite.FavoriteTypeID);
                            if (notificationSettingData != null)
                            {
                                if (notificationSettingData.NewFollowers.GetValueOrDefault())
                                {
                                    var user = new User();
                                    var username = user.GetUsernameByUserID(favorite.UserID.Value);
                                    var proData = user.GetUserByID(favorite.FavoriteTypeID.Value);
                                    var jsonString = "{}";
                                    DataAccess.WindowAzurePushNotification.Notifications pro = new DataAccess.WindowAzurePushNotification.Notifications();
                                    {
                                        await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                                    }
                                }
                            }
                            else
                            {
                                var user = new User();
                                var username = user.GetUsernameByUserID(favorite.UserID.Value);
                                var proData = user.GetUserByID(favorite.FavoriteTypeID.Value);
                                var jsonString = "{}";
                                DataAccess.WindowAzurePushNotification.Notifications pro = new DataAccess.WindowAzurePushNotification.Notifications();
                                {
                                    await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                                }
                            }

                        });
                    }
                    #endregion
                    return new ResultData { ResultDescription = "Favorite Added Successfully", ResultStatus = 1 };
                

                //#region PushNoitification
                //if (favoriteType == HelperEnums.FavoriteType.Professional)
                //{
                //    Task.Run(async () =>
                //    {

                //        NotificationSetting notificationSettings = new NotificationSetting();
                //        var notificationSettingData = notificationSettings.GetByUserID(favorite.FavoriteTypeID);
                //        if (notificationSettingData != null)
                //        {
                //            if (notificationSettingData.NewFollowers.GetValueOrDefault())
                //            {
                //                var user = new User();
                //                var username = user.GetUsernameByUserID(favorite.UserID.Value);
                //                var proData = user.GetProfessionalByID(favorite.FavoriteTypeID.Value);
                //                var jsonString = "{}";
                //                DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                //                {
                //                    await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            var user = new User();
                //            var username = user.GetUsernameByUserID(favorite.UserID.Value);
                //            var proData = user.GetProfessionalByID(favorite.FavoriteTypeID.Value);
                //            var jsonString = "{}";
                //            DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                //            {
                //                await pro.SendWindowAzurePushNotification(proData.Platform, Convert.ToString(proData.Id), username + " started following you", HelperEnums.PushNotificationType.Follow, jsonString);
                //            }
                //        }

                //    });
                //}
                //#endregion
                //return new ResultData { ResultDescription = "Favorite Added Successfully", ResultStatus = 1 };


            }
        }
        
        public FavoriteJobListWrapper FavoriteJobs(long? userID, int pageIndex,int pageSize )
        {
            var query = (from f in db.tbl_Favorite
                         join j in db.tbl_Jobs
                         on f.FavoriteTypeID equals j.Id
                         where f.Favoritetype == (int)HelperEnums.FavoriteType.Job 
                        && j.IsApprovedByAdmin==true&&j.IsDeleted==false&&f.UserID==userID
                         select new FavoriteJobDataContract
                         {
                             FavoriteID = f.ID,
                             JobID = j.Id,
                             JobDescription=j.Description,
                             JobLocation = j.tbl_State.City + ", " + j.tbl_State.State + ", " + j.tbl_State.zip,
                             JobStatus = (HelperEnums.JobStatus)(j.JobStatus),
                             JobTitle = j.Title,
                             JobImage = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage,
                         });
            var countJobs = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countJobs) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.FavoriteID).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            
            FavoriteJobListWrapper jobs = new FavoriteJobListWrapper();
            jobs.Count = countJobs;
            if (totalpages <= (pageIndex + 1))
            {
                jobs.HideShowMore = true;
            }
            else
            {
                jobs.HideShowMore = false;
            }
            
            jobs.Jobs = data;
            return jobs;
        }

        public FavoriteProfessionalListWrapper FavoriteProfessionals(long? userID, int pageIndex, int pageSize)
        {
            var query = (from f in db.tbl_Favorite
                         join u in db.tbl_User
                         on f.FavoriteTypeID equals u.Id
                         where f.Favoritetype == (int)HelperEnums.FavoriteType.Professional
                        && u.IsApprovedByAdmin == true && u.IsDeleted == false&&u.IsVerified==true
                        && u.Type == (int)(HelperEnums.UserType.Professional)&&f.UserID==userID
                         select new FavoriteProfessionalDataContract
                         {
                             ThreadID = (from p in db.tbl_ThreadParticipants
                                         where (p.UserID == userID || p.UserID == f.FavoriteTypeID)
                                         && p.tbl_Threads.IsJobThread == false
                                         //&& p.tbl_Threads.JobID == null
                                         group p by p.ThreadID into participants
                                         select participants).Where(p => p.Count() == 2)
                                .Select(k => k.Key).FirstOrDefault(),
                             FavoriteID = f.ID,
                             UserID = u.Id,
                             UserImage = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath+ u.Image : Common.NoImageIcon,
                             Username=u.FirstName+" "+u.LastName,
                             Categories = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == u.Id)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                         });
            var countProfessionals= query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countProfessionals) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.FavoriteID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            FavoriteProfessionalListWrapper professionals = new FavoriteProfessionalListWrapper();
            professionals.Count = countProfessionals;
            if (totalpages <= (pageIndex + 1))
            {
                professionals.HideShowMore = true;
            }
            else
            {
                professionals.HideShowMore = false;
            }

            professionals.Professionals = data;
            return professionals;
        }
        public FavoriteUserListWrapper FavoriteUsers(long? userID, int pageIndex, int pageSize)
        {
            var query = (from f in db.tbl_Favorite
                         join u in db.tbl_User
                         on f.FavoriteTypeID equals u.Id
                         join c in db.tbl_State
                          on u.CityId equals c.Id
                         where f.Favoritetype == (int)HelperEnums.FavoriteType.User
                        && u.IsApprovedByAdmin == true && u.IsDeleted == false && u.IsVerified == true
                        &&u.Type==(int)(HelperEnums.UserType.User)&&f.UserID==userID
                         select new FavoriteUserDataContract
                         {
                             ThreadID = (from p in db.tbl_ThreadParticipants
                                         where (p.UserID == userID || p.UserID == f.FavoriteTypeID)
                                         && p.tbl_Threads.IsJobThread == false
                                         //&& p.tbl_Threads.JobID == null
                                         group p by p.ThreadID into participants
                                         select participants).Where(p => p.Count() == 2)
                                .Select(k => k.Key).FirstOrDefault(),
                             FavoriteID = f.ID,
                             UserID = u.Id,
                             UserImage = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,
                             Username = u.FirstName + " " + u.LastName,
                             UserLocation=c==null?"":c.City+", "+c.State+", "+c.zip,

                         });
            var countUsers = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countUsers) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.FavoriteID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            FavoriteUserListWrapper users = new FavoriteUserListWrapper();
            
            users.Count = countUsers;
            if (totalpages <= (pageIndex + 1))
            {
                users.HideShowMore = true;
            }
            else
            {
                users.HideShowMore = false;
            }

            users.Users = data;
            return users;
        }
        public FavoriteUserListWrapper GetMyUserFollowers(long? userID, int pageIndex, int pageSize)
        {
            
            var query = (from f in db.tbl_Favorite
                         join u in db.tbl_User
                         on f.UserID equals u.Id
                         join c in db.tbl_State
                          on u.CityId equals c.Id
                         where //f.Favoritetype == (int)HelperEnums.FavoriteType.User &&
                        u.IsApprovedByAdmin == true && u.IsDeleted == false && u.IsVerified == true
                        && u.Type == (int)(HelperEnums.UserType.User)&&f.FavoriteTypeID==userID
                         select new FavoriteUserDataContract
                         {
                             IsFavoriteByMe = (db.tbl_Favorite.Count(p => p.FavoriteTypeID == u.Id 
                             && (p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional) || p.Favoritetype == (int)(HelperEnums.FavoriteType.User))
                             && p.UserID== userID) > 0) ? true : false,
                             FavoriteID = f.ID,
                             UserID = u.Id,
                             UserImage = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,
                             Username = u.FirstName + " " + u.LastName,
                             UserLocation = c == null ? "" : c.City + ", " + c.State + ", " + c.zip,

                         });
            var countUsers= query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countUsers) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.FavoriteID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            FavoriteUserListWrapper users = new FavoriteUserListWrapper();
            users.Count = countUsers;
            if (totalpages <= (pageIndex + 1))
            {
                users.HideShowMore = true;
            }
            else
            {
                users.HideShowMore = false;
            }
            if (data.Count > 0)
            {
                UpdateFollowerIsViewed(data.Select(p => p.FavoriteID).ToList());
            }
            users.Users = data;

            return users;
        }

        private void UpdateFollowerIsViewed(List<long?> favoriteIDs)
        {
            var favoriteIDsCSV = String.Join(",", favoriteIDs.Select(x => x.ToString()).ToArray());

            db.Database.ExecuteSqlCommand("update tbl_favorite set isviewed=@isviewed where id in(" + favoriteIDsCSV+")",new SqlParameter("@isviewed", 1));

        }
        public FavoriteProfessionalListWrapper GetMyProfessionalFollowers(long? userID, int pageIndex, int pageSize)
        {
            var query = (from f in db.tbl_Favorite
                         join u in db.tbl_User
                         on f.UserID equals u.Id
                         where //f.Favoritetype == (int)HelperEnums.FavoriteType.Professional &&
                         u.IsApprovedByAdmin == true && u.IsDeleted == false && u.IsVerified == true
                        && u.Type == (int)(HelperEnums.UserType.Professional) 
                        && f.FavoriteTypeID== userID
                         select new FavoriteProfessionalDataContract
                         {
                             IsFavoriteByMe = db.tbl_Favorite.Count(p => p.FavoriteTypeID == u.Id &&
                             (p.Favoritetype == (int)(HelperEnums.FavoriteType.Professional)||p.Favoritetype== (int)(HelperEnums.FavoriteType.User)) && p.UserID == userID) > 0 ? true : false,
                             FavoriteID = f.ID,
                             UserID = u.Id,
                             UserImage = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,
                             Username = u.FirstName + " " + u.LastName,
                             Categories = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == u.Id)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                         });
            var countProfessionals= query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countProfessionals) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.FavoriteID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            FavoriteProfessionalListWrapper professionals = new FavoriteProfessionalListWrapper();
            professionals.Count = countProfessionals;
            if (totalpages <= (pageIndex + 1))
            {
                professionals.HideShowMore = true;
            }
            else
            {
                professionals.HideShowMore = false;
            }

            professionals.Professionals = data;
            if (data.Count > 0)
            {
                UpdateFollowerIsViewed(data.Select(p => p.FavoriteID).ToList());
            }
            return professionals;
        }
    }

}
