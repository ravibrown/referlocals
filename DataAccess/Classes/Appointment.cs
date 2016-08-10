using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class Appointment : dbContext
    {
        //public long? ID { get; set; }
        //public long? QuoteID { get; set; }
        //public DateTime? AppointmentDateTime { get; set; }
        //public string AppointmentDateTimeString { get { return AppointmentDateTime.GetValueOrDefault().ToLongDateString() + AppointmentDateTime.GetValueOrDefault().ToLongTimeString(); } }
        //public string Message { get; set; }
        //public bool? IsAcceptedByProfessional { get; set; }
        //public long? ProfessionalID { get; set; }
        //public bool? IsRescheduled { get; set; }
        //public bool? RescheduledByUser { get; set; }
        //public bool? RescheduledByProfessional { get; set; }


        public long Save(long? id, long? quoteID, string message, bool? isAcceptedByProfessional, long? professionalID,
            bool? isRescheduled, bool? rescheduledByUser, bool? rescheduledByProfessional, long? userID)
        {
            tbl_Appointments appointment = new tbl_Appointments();

            if (id.GetValueOrDefault() > 0)
            {
                appointment = db.tbl_Appointments.FirstOrDefault(p => p.ID == id);
            }

            appointment.AddedOn = DateTime.UtcNow;
            appointment.IsViewed = false;
            appointment.IsAcceptedByProfessional = isAcceptedByProfessional;
            appointment.IsRescheduled = isRescheduled;
            appointment.Message = message;
            appointment.ProfessionalID = professionalID;
            appointment.QuoteID = quoteID;
            appointment.RescheduledByProfessional = rescheduledByProfessional;
            appointment.RescheduledByUser = rescheduledByUser;
            appointment.UserID = userID;
            if (id.GetValueOrDefault() == 0)
            {
                db.tbl_Appointments.Add(appointment);
            }
            db.SaveChanges();
            if (rescheduledByUser.GetValueOrDefault() == true)
            {
                var professionalData = new User().GetProfessionalByID(appointment.ProfessionalID.GetValueOrDefault()); //appointment.tbl_User;
                var jobData = appointment.tbl_Quotes.tbl_Jobs;
                Common.SendEmailWithGenericTemplate(professionalData.Email, Common.EmailSubjectOnAppointmentRescheduleToPro,
                    Common.EmailBodyOnAppointmentRescheduleToPro
                    .Replace("##ProfessionalName##", professionalData.FirstName + " " + professionalData.LastName)
                    .Replace("##JobLink##", Common.WebsiteHostNameForLink + "/jobdetail/" + jobData.Id + "/" + Common.TextToFriendlyUrl(jobData.Title))
                    .Replace("##AppointmentLink##", Common.WebsiteHostNameForLink + "/professional_dashboard")
                   .Replace("##Username##", jobData.tbl_User.FirstName + " " + jobData.tbl_User.LastName));

                #region PushNoitification
                Task.Run(async () =>
                {

                    NotificationSetting notificationSettings = new NotificationSetting();
                    var notificationSettingData = notificationSettings.GetByUserID(professionalData.Id);
                    if (notificationSettingData != null)
                    {
                        if (notificationSettingData.AppointmentRequest.GetValueOrDefault())
                        {

                            var dataForPush = new NewUpdateWithPush
                            {
                                NewProfessionalUpdates = new User().GetNewProfessionalUpdates(professionalData.Id)

                            };
                            var jsonString = JsonConvert.SerializeObject(dataForPush);

                            DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                            {
                                await pro.SendWindowAzurePushNotification(professionalData.Platform, Convert.ToString(professionalData.Id), jobData.tbl_User.FirstName + " " + jobData.tbl_User.LastName + " requested to reschedule the appointment", HelperEnums.PushNotificationType.AppointmentRescheduleByUser, jsonString);
                            }
                        }

                    }
                    else
                    {

                        var dataForPush = new NewUpdateWithPush
                        {
                            NewProfessionalUpdates = new User().GetNewProfessionalUpdates(professionalData.Id)

                        };
                        var jsonString = JsonConvert.SerializeObject(dataForPush);

                        DataAccess.WindowAzurePushNotification.NotificationsForPro pro = new DataAccess.WindowAzurePushNotification.NotificationsForPro();
                        {
                            await pro.SendWindowAzurePushNotification(professionalData.Platform, Convert.ToString(professionalData.Id), jobData.tbl_User.FirstName + " " + jobData.tbl_User.LastName + " requested to reschedule the appointment", HelperEnums.PushNotificationType.AppointmentRescheduleByUser, jsonString);
                        }
                    }
                });
                #endregion

            }
            if (rescheduledByProfessional.GetValueOrDefault() == true)
            {
                //var professionalData = appointment.tbl_User;
                var professionalData = appointment.tbl_Quotes.tbl_User;
                var jobData = appointment.tbl_Quotes.tbl_Jobs;
                Common.SendEmailWithGenericTemplate(professionalData.Email, Common.EmailSubjectOnAppointmentRescheduleToUser,
                    Common.EmailBodyOnAppointmentRescheduleToUser
                    .Replace("##ProfessionalName##", professionalData.FirstName + " " + professionalData.LastName)
                    .Replace("##JobLink##", Common.WebsiteHostNameForLink + "/jobdetail/" + jobData.Id + "/" + Common.TextToFriendlyUrl(jobData.Title))
                    .Replace("##AppointmentLink##", Common.WebsiteHostNameForLink + "/user_dashboard")
                   .Replace("##Username##", jobData.tbl_User.FirstName + " " + jobData.tbl_User.LastName));

                #region PushNoitification
                Task.Run(async () =>
                {
                    NotificationSetting notificationSettings = new NotificationSetting();
                    var notificationSettingData = notificationSettings.GetByUserID(jobData.tbl_User.Id);
                    if (notificationSettingData != null)
                    {
                        if (notificationSettingData.AppointmentRequest.GetValueOrDefault())
                        {
                            var dataForPush = new NewUpdateWithPush
                            {
                                NewUserUpdates= new User().GetNewUserUpdates(jobData.tbl_User.Id)

                            };
                            var jsonString = JsonConvert.SerializeObject(dataForPush);

                            DataAccess.WindowAzurePushNotification.Notifications pro = new DataAccess.WindowAzurePushNotification.Notifications();
                            {
                                await pro.SendWindowAzurePushNotification(jobData.tbl_User.Platform, Convert.ToString(jobData.tbl_User.Id), professionalData.FirstName + " " + professionalData.LastName + " requested to reschedule the appointment", HelperEnums.PushNotificationType.AppointmentRescheduleByPro, jsonString);
                            }
                        }
                    }
                    else
                    {
                        var dataForPush = new NewUpdateWithPush
                        {
                            NewUserUpdates = new User().GetNewUserUpdates(jobData.tbl_User.Id)

                        };
                        var jsonString = JsonConvert.SerializeObject(dataForPush);

                        DataAccess.WindowAzurePushNotification.Notifications pro = new DataAccess.WindowAzurePushNotification.Notifications();
                        {
                            await pro.SendWindowAzurePushNotification(jobData.tbl_User.Platform, Convert.ToString(jobData.tbl_User.Id), professionalData.FirstName + " " + professionalData.LastName + " requested to reschedule the appointment", HelperEnums.PushNotificationType.AppointmentRescheduleByPro, jsonString);
                        }
                    }
                });
                #endregion

            }
            return appointment.ID;
        }

        public long SaveAppointmentDate(long? appointmentID, DateTime? appointmentDateTime)
        {
            tbl_AppointmentDates appointmentDate = new tbl_AppointmentDates();
            appointmentDate.AppointmentID = appointmentID;
            appointmentDate.AppointmentDateTime = appointmentDateTime;
            appointmentDate.IsAcceptedByUser = false;
            appointmentDate.IsAcceptedByProfessional = false;
            db.tbl_AppointmentDates.Add(appointmentDate);
            db.SaveChanges();
            return appointmentDate.ID;
        }
        public long AcceptAppointmentDate(long appointmentDateID, HelperEnums.UserType userType)
        {
            tbl_AppointmentDates appointmentDate = db.tbl_AppointmentDates.FirstOrDefault(p => p.ID == appointmentDateID);
            if (appointmentDate != null)
            {
                if (userType == HelperEnums.UserType.Professional)
                {
                    appointmentDate.IsAcceptedByProfessional = true;
                    var userData = appointmentDate.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_User;
                    var jobData = appointmentDate.tbl_Appointments.tbl_Quotes.tbl_Jobs;
                    var professionalData = appointmentDate.tbl_Appointments.tbl_User;
                    if (userData != null && professionalData != null && jobData != null)
                    {
                        Common.SendEmailWithGenericTemplate(userData.Email, Common.EmailSubjectOnAppointmentConfirm,
                            Common.EmailBodyOnAppointmentConfirm
                            .Replace("##Username##", userData.FirstName + " " + userData.LastName)
                            .Replace("##JobLink##", Common.WebsiteHostNameForLink + "/jobdetail/" + jobData.Id + "/" + Common.TextToFriendlyUrl(jobData.Title))
                            .Replace("##AppointmentDateTime##", appointmentDate.AppointmentDateTime.GetValueOrDefault().ToLongDateString() + " " + appointmentDate.AppointmentDateTime.GetValueOrDefault().ToLongTimeString())
                            .Replace("##AppointmentLink##", Common.WebsiteHostNameForLink + "/user_dashboard")
                            .Replace("##ProfessionalName##", professionalData.FirstName + " " + professionalData.LastName)
                            );

                        Task.Run(async () =>
                        {
                            var jsonString = "{}";
                            WindowAzurePushNotification.Notifications pro = new WindowAzurePushNotification.Notifications();
                            {
                                await pro.SendWindowAzurePushNotification(userData.Platform, Convert.ToString(userData.Id), "Appointment confirmed for your job", HelperEnums.PushNotificationType.AppointmentConfirmed, jsonString);
                            }
                        });
                    }
                }
                else
                {
                    appointmentDate.IsAcceptedByUser = true;
                }
                db.SaveChanges();
                return appointmentDateID;
            }
            else
            {
                return 0;
            }

        }
        public void DeleteAppointmentDates(long? appointmentID)
        {
            db.tbl_AppointmentDates.RemoveRange(db.tbl_AppointmentDates.Where(p => p.AppointmentID == appointmentID));
            db.SaveChanges();
        }

        public AppointmentWithDatesDataContract GetAppointmentDatesByQuoteID(long quoteID)
        {
            var data = (from p in db.tbl_Appointments
                        where p.tbl_Quotes.ID == quoteID
                        select new AppointmentWithDatesDataContract
                        {
                            ID = p.ID,
                            IsRescheduled = p.IsRescheduled,
                            IsAcceptedByProfessional = p.IsAcceptedByProfessional,
                            Message = p.Message,
                            ProfessionalID = p.ProfessionalID,
                            UserID = p.UserID,
                            RescheduledByProfessional = p.RescheduledByProfessional,
                            RescheduledByUser = p.RescheduledByUser,
                            QuoteID = p.QuoteID,
                            Dates = p.tbl_AppointmentDates.Select(a => new AppointmentDatesDataContrant { AppointmentDateTime = a.AppointmentDateTime, AppointmentID = a.AppointmentID, ID = a.ID }).ToList(),

                        }).FirstOrDefault();

            return data;
        }

        public AppointmentWithDatesDataContract GetAppointmentDatesByAppointmentID(long appointmentID)
        {
            var data = (from p in db.tbl_Appointments
                        where p.ID == appointmentID
                        select new AppointmentWithDatesDataContract
                        {
                            ID = p.ID,
                            IsRescheduled = p.IsRescheduled,
                            IsAcceptedByProfessional = p.IsAcceptedByProfessional,
                            Message = p.Message,
                            ProfessionalID = p.ProfessionalID,
                            RescheduledByProfessional = p.RescheduledByProfessional,
                            RescheduledByUser = p.RescheduledByUser,
                            QuoteID = p.QuoteID,
                            Dates = p.tbl_AppointmentDates.Select(a => new AppointmentDatesDataContrant { AppointmentDateTime = a.AppointmentDateTime, AppointmentID = a.AppointmentID, ID = a.ID }).ToList(),

                        }).FirstOrDefault();

            return data;
        }

        public AppointmentDataContract Get(long ID, long userID, int userType)
        {
            tbl_Appointments appointment;
            if (userType == (int)HelperEnums.UserType.Professional)
            {
                appointment = db.tbl_Appointments.FirstOrDefault(p => p.ProfessionalID == userID && p.ID == ID);
            }
            else
            {
                appointment = db.tbl_Appointments.FirstOrDefault(p => p.UserID == userID && p.ID == ID);
            }

            if (appointment != null)
            {
                AppointmentDataContract obj = new AppointmentDataContract();
                obj.ID = appointment.ID;
                obj.IsAcceptedByProfessional = appointment.IsAcceptedByProfessional;
                obj.IsRescheduled = appointment.IsRescheduled;
                obj.Message = appointment.Message;
                obj.ProfessionalID = appointment.ProfessionalID;
                obj.QuoteID = appointment.QuoteID;
                obj.RescheduledByProfessional = appointment.RescheduledByProfessional;
                obj.RescheduledByUser = appointment.RescheduledByUser;
                return obj;
            }
            return null;


        }
        public int GetUpcomingAppointmentCountByUserID(long userID, long userType)
        {

            var query = (from p in db.tbl_AppointmentDates
                         where p.AppointmentDateTime >= DateTime.UtcNow && (p.IsAcceptedByProfessional == true || p.IsAcceptedByUser == true)
                         select p);

            if (userType == (int)HelperEnums.UserType.Professional)
            {
                query = query.Where(p => p.tbl_Appointments.ProfessionalID == userID);
            }
            else
            {
                query = query.Where(p => p.tbl_Appointments.UserID == userID);
            }

            return query.Count();


        }
        public AppointmentDatesWithJobListWrapper GetAppointments(HelperEnums.DateType dateType, HelperEnums.UserType userType, long userID, int pageIndex, int pageSize, int month = 0)
        {
            var query = (from p in db.tbl_AppointmentDates
                         where p.IsAcceptedByProfessional == true || p.IsAcceptedByUser == true

                         select new AppointmentDatesWithJobDataContract
                         {
                             QuoteID = p.tbl_Appointments.QuoteID,
                             JobID = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Id,
                             JobTitle = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Title,
                             JobLocation = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.City + ", " + p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.State,
                             AppointmentDateTime = p.AppointmentDateTime,
                             AppointmentID = p.AppointmentID,
                             IsAcceptedByProfessional = p.IsAcceptedByProfessional,
                             IsAcceptedByUser = p.IsAcceptedByUser,
                             ID = p.ID,
                             ProfessionalID = p.tbl_Appointments.ProfessionalID,
                             UserID = p.tbl_Appointments.UserID,
                         });

            if (userType == HelperEnums.UserType.User)
            {
                query = query.Where(p => p.UserID == userID);
            }
            else if (userType == HelperEnums.UserType.Professional)
            {
                query = query.Where(p => p.ProfessionalID == userID);
            }

            if (dateType == HelperEnums.DateType.Today)
            {
                var ToDate = DateTime.UtcNow.AddDays(1);
                //query = query.Where(p => p.AppointmentDateTime > DbFunctions.TruncateTime(DateTime.UtcNow) && p.AppointmentDateTime < DbFunctions.TruncateTime(ToDate));
                query = query.Where(p => DbFunctions.DiffDays(p.AppointmentDateTime, DateTime.UtcNow) == 0);
            }
            else if (dateType == HelperEnums.DateType.Week)
            {
                var ToDate = DateTime.UtcNow.AddDays(7);
                query = query.Where(p => p.AppointmentDateTime > DbFunctions.TruncateTime(DateTime.UtcNow) && p.AppointmentDateTime < DbFunctions.TruncateTime(ToDate));
            }
            else if (dateType == HelperEnums.DateType.Month)
            {
                var currentDate = DateTime.UtcNow;
                var currentMonth = currentDate.Month;
                var monthDifferenc = month > 0 ? currentMonth - (month) : 0;
                var ToDate = currentDate.AddMonths(-(monthDifferenc));
                //query = query.Where(p => p.AppointmentDateTime > DbFunctions.TruncateTime(DateTime.UtcNow) && p.AppointmentDateTime < DbFunctions.TruncateTime(ToDate));
                query = query.Where(p => DbFunctions.DiffMonths(p.AppointmentDateTime, ToDate) == 0);
            }

            var count = query.Count();
            decimal totalpages = Math.Ceiling(Convert.ToDecimal(count) / Convert.ToDecimal(pageSize));
            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            AppointmentDatesWithJobListWrapper appointments = new AppointmentDatesWithJobListWrapper();
            appointments.AppointmentCount = count;
            if (totalpages <= (pageIndex + 1))
            {
                appointments.HideShowMore = true;
            }
            else
            {
                appointments.HideShowMore = false;
            }

            appointments.DatesWithJob = data;
            return appointments;
        }

        public AppointmentDatesWithJobDataContract GetAppointmentsByJobID(long jobID, long userID, HelperEnums.UserType userType)
        {
            var query = (from p in db.tbl_AppointmentDates

                         where p.IsAcceptedByProfessional == true || p.IsAcceptedByUser == true

                         select new AppointmentDatesWithJobDataContract
                         {
                             QuoteID = p.tbl_Appointments.QuoteID,
                             JobID = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Id,
                             //JobTitle = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Title,
                             //JobLocation = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.City + ", " + p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.State + ", " + p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.zip,
                             AppointmentDateTime = p.AppointmentDateTime,
                             AppointmentID = p.AppointmentID,
                             //IsAcceptedByProfessional = p.IsAcceptedByProfessional,
                             //IsAcceptedByUser = p.IsAcceptedByUser,
                             ID = p.ID,
                             ProfessionalID = p.tbl_Appointments.ProfessionalID,
                             UserID = p.tbl_Appointments.UserID,

                         });

            query = query.Where(p => p.JobID == jobID);
            if (userType == HelperEnums.UserType.Professional)
            {
                query = query.Where(p => p.ProfessionalID == userID);
            }
            else if (userType == HelperEnums.UserType.User)
            {
                query = query.Where(p => p.UserID == userID);
            }
            else
            {
                return null;
            }
            return query.FirstOrDefault();
        }
        public AppointmentDatesWithJobListWrapper GetPastAppointmentsForGivenDate(HelperEnums.UserType userType,
            long userID, int pageIndex, int pageSize, DateTime givenDate)
        {
            var query = (from p in db.tbl_AppointmentDates
                         where p.IsAcceptedByProfessional == true || p.IsAcceptedByUser == true

                         select new AppointmentDatesWithJobDataContract
                         {
                             QuoteID = p.tbl_Appointments.QuoteID,
                             JobID = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Id,
                             JobTitle = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Title,
                             JobAddress = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Address,
                             JobLocation = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.City + ", " + p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.State,
                             AppointmentDateTime = p.AppointmentDateTime,
                             AppointmentID = p.AppointmentID,
                             IsAcceptedByProfessional = p.IsAcceptedByProfessional,
                             IsAcceptedByUser = p.IsAcceptedByUser,
                             ID = p.ID,
                             ProfessionalID = p.tbl_Appointments.ProfessionalID,
                             UserID = p.tbl_Appointments.UserID,
                             Username = p.tbl_Appointments.tbl_User.FirstName + " " + p.tbl_Appointments.tbl_User.FirstName,
                             Email = p.tbl_Appointments.tbl_User.Email,
                             Phone = p.tbl_Appointments.tbl_User.PhoneNumber
                         });

            if (userType == HelperEnums.UserType.User)
            {
                query = query.Where(p => p.UserID == userID);
            }
            else if (userType == HelperEnums.UserType.Professional)
            {
                query = query.Where(p => p.ProfessionalID == userID);
            }



            var fromDate = givenDate;
            //query = query.Where(p => p.AppointmentDateTime > DbFunctions.TruncateTime(DateTime.UtcNow) && p.AppointmentDateTime < DbFunctions.TruncateTime(ToDate));
            query = query.Where(p => p.AppointmentDateTime < givenDate);



            var count = query.Count();
            decimal totalpages = Math.Ceiling(Convert.ToDecimal(count) / Convert.ToDecimal(pageSize));
            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            AppointmentDatesWithJobListWrapper appointments = new AppointmentDatesWithJobListWrapper();
            appointments.AppointmentCount = count;
            if (totalpages <= (pageIndex + 1))
            {
                appointments.HideShowMore = true;
            }
            else
            {
                appointments.HideShowMore = false;
            }

            appointments.DatesWithJob = data;
            return appointments;

        }
        public List<AppointmentDatesWithJobDataContract> GetAppointmentsForGivenDate(HelperEnums.DateType dateType, HelperEnums.UserType userType, long userID, int pageIndex, int pageSize, DateTime givenDate, int month = 0)
        {
            var query = (from p in db.tbl_AppointmentDates
                         where p.IsAcceptedByProfessional == true || p.IsAcceptedByUser == true

                         select new AppointmentDatesWithJobDataContract
                         {
                             QuoteID = p.tbl_Appointments.QuoteID,
                             JobID = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Id,
                             JobTitle = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Title,
                             JobLocation = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.City + ", " + p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.State,
                             AppointmentDateTime = p.AppointmentDateTime,
                             AppointmentID = p.AppointmentID,
                             IsAcceptedByProfessional = p.IsAcceptedByProfessional,
                             IsAcceptedByUser = p.IsAcceptedByUser,
                             ID = p.ID,
                             ProfessionalID = p.tbl_Appointments.ProfessionalID,
                             UserID = p.tbl_Appointments.UserID,
                             Username = p.tbl_Appointments.tbl_User.FirstName + " " + p.tbl_Appointments.tbl_User.FirstName,
                             Email = p.tbl_Appointments.tbl_User.Email,
                             Phone = p.tbl_Appointments.tbl_User.PhoneNumber
                         });

            if (userType == HelperEnums.UserType.User)
            {
                query = query.Where(p => p.UserID == userID);
            }
            else if (userType == HelperEnums.UserType.Professional)
            {
                query = query.Where(p => p.ProfessionalID == userID);
            }

            if (dateType == HelperEnums.DateType.Today)
            {
                var ToDate = givenDate;
                //query = query.Where(p => p.AppointmentDateTime > DbFunctions.TruncateTime(DateTime.UtcNow) && p.AppointmentDateTime < DbFunctions.TruncateTime(ToDate));
                query = query.Where(p => DbFunctions.DiffDays(p.AppointmentDateTime, ToDate) == 0);
            }
            else if (dateType == HelperEnums.DateType.Week)
            {
                var ToDate = givenDate.AddDays(7);
                query = query.Where(p => p.AppointmentDateTime >= DbFunctions.TruncateTime(givenDate) && p.AppointmentDateTime <= DbFunctions.TruncateTime(ToDate));
            }
            else if (dateType == HelperEnums.DateType.Month)
            {
                var currentDate = givenDate;
                var currentMonth = currentDate.Month;
                var monthDifferenc = month > 0 ? currentMonth - (month) : 0;
                var ToDate = currentDate.AddMonths(-(monthDifferenc));
                //query = query.Where(p => p.AppointmentDateTime > DbFunctions.TruncateTime(DateTime.UtcNow) && p.AppointmentDateTime < DbFunctions.TruncateTime(ToDate));
                query = query.Where(p => DbFunctions.DiffMonths(p.AppointmentDateTime, ToDate) == 0 && p.AppointmentDateTime >= DbFunctions.TruncateTime(givenDate));
            }

            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            return data;
        }
        public List<AppointmentDatesWithJobDataContract> GetAppointmentsFoCalendar(HelperEnums.DateType dateType, HelperEnums.UserType userType, long userID, int pageIndex, int pageSize, int month = 0)
        {
            var query = (from p in db.tbl_AppointmentDates
                         where p.IsAcceptedByProfessional == true || p.IsAcceptedByUser == true

                         select new AppointmentDatesWithJobDataContract
                         {
                             QuoteID = p.tbl_Appointments.QuoteID,
                             JobID = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Id,
                             JobTitle = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.Title,
                             JobLocation = p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.City + ", " + p.tbl_Appointments.tbl_Quotes.tbl_Jobs.tbl_State.State,
                             AppointmentDateTime = p.AppointmentDateTime,
                             AppointmentID = p.AppointmentID,
                             IsAcceptedByProfessional = p.IsAcceptedByProfessional,
                             IsAcceptedByUser = p.IsAcceptedByUser,
                             ID = p.ID,
                             ProfessionalID = p.tbl_Appointments.ProfessionalID,
                             UserID = p.tbl_Appointments.UserID,
                         });

            if (userType == HelperEnums.UserType.User)
            {
                query = query.Where(p => p.UserID == userID);
            }
            else if (userType == HelperEnums.UserType.Professional)
            {
                query = query.Where(p => p.ProfessionalID == userID);
            }

            if (dateType == HelperEnums.DateType.Today)
            {
                var ToDate = DateTime.UtcNow.AddDays(1);
                //query = query.Where(p => p.AppointmentDateTime > DbFunctions.TruncateTime(DateTime.UtcNow) && p.AppointmentDateTime < DbFunctions.TruncateTime(ToDate));
                query = query.Where(p => DbFunctions.DiffDays(p.AppointmentDateTime, DateTime.UtcNow) == 0);
            }
            else if (dateType == HelperEnums.DateType.Week)
            {
                var ToDate = DateTime.UtcNow.AddDays(7);
                query = query.Where(p => p.AppointmentDateTime > DbFunctions.TruncateTime(DateTime.UtcNow) && p.AppointmentDateTime < DbFunctions.TruncateTime(ToDate));
            }
            else if (dateType == HelperEnums.DateType.Month)
            {
                var currentDate = DateTime.UtcNow;
                var currentMonth = currentDate.Month;
                var monthDifferenc = month > 0 ? currentMonth - (month) : 0;
                var ToDate = currentDate.AddMonths(-(monthDifferenc));
                //query = query.Where(p => p.AppointmentDateTime > DbFunctions.TruncateTime(DateTime.UtcNow) && p.AppointmentDateTime < DbFunctions.TruncateTime(ToDate));
                query = query.Where(p => DbFunctions.DiffMonths(p.AppointmentDateTime, ToDate) == 0);
            }

            var data = query.OrderByDescending(p => p.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            return data;
        }
    }

}
