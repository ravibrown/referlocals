using AutoMapper;
using DataAccess.DataContractClasses;
using DataAccess.HelperClasses;
using DataContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class Flag : dbContext
    {
        public long? ID { get; set; }
        public long? UserID { get; set; }
        public long? FlagTypeID { get; set; }
        public int? FlagType { get; set; }
        public DateTime? AddedOn{ get; set; }

      
        public ResultData Save(long? userID,long? flagTypeID,HelperEnums.FlagType flagType,string reason )
        {
            tbl_Flag flag= db.tbl_Flag.FirstOrDefault(p => p.UserID == userID && p.FlagTypeID == flagTypeID && p.FlagType == (int)flagType);
            if (flag!=null)
            {
                
                return new ResultData { ResultDescription = "Already Flagged", ResultStatus = 1 };
            }
            else
            {
                flag = new tbl_Flag();
                flag.FlagReason = reason;
                flag.AddedOn = DateTime.UtcNow;
                flag.FlagType = (int)flagType;
                flag.FlagTypeID = flagTypeID;
                flag.UserID = userID;

                db.tbl_Flag.Add(flag);

                db.SaveChanges();
                return new ResultData { ResultDescription = "Flag Added Successfully", ResultStatus = 1 };


            }
        }
        
        public FlagJobListWrapper FlagJobs(long? userID, int pageIndex,int pageSize )
        {
            var query = (from f in db.tbl_Flag
                         join j in db.tbl_Jobs
                         on f.FlagTypeID equals j.Id
                         where f.FlagType == (int)HelperEnums.FlagType.Job 
                        && j.IsApprovedByAdmin==true&&j.IsDeleted==false
                         select new FlagJobDataContract
                         {
                             FlagID = f.ID,
                             JobID = j.Id,
                             JobLocation = j.tbl_State.City + ", " + j.tbl_State.State + ", " + j.tbl_State.zip,
                             JobStatus = (HelperEnums.JobStatus)(j.JobStatus),
                             JobTitle = j.Title,
                             JobImage = !string.IsNullOrEmpty(j.Image) ? Common.JobImagesPath + j.Image : Common.JobDefaultImage,
                         });
            var countJobs = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countJobs) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.FlagID).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            
            FlagJobListWrapper jobs = new FlagJobListWrapper();
            
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
        public FlagProfessionalListWrapper FlagProfessionals(long? userID, int pageIndex, int pageSize)
        {
            var query = (from f in db.tbl_Flag
                         join u in db.tbl_User
                         on f.FlagTypeID equals u.Id
                         where f.FlagType == (int)HelperEnums.FlagType.Professional
                        && u.IsApprovedByAdmin == true && u.IsDeleted == false&&u.IsVerified==true
                        && u.Type == (int)(HelperEnums.UserType.Professional)
                         select new FlagProfessionalDataContract 
                         {
                             FlagID = f.ID,
                             UserID = u.Id,
                             UserImage = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath+ u.Image : Common.NoImageIcon,
                             Username=u.FirstName+" "+u.FirstName,
                             Categories = (db.tbl_User_SubCategory_Mapping.Where(p => p.UserId == userID)
                                .Join(db.tbl_SubCategory, m => m.SubCategoryId, s => s.Id,
                                (m, s) => new ProfessionalUrls { SubCategoryName = s.Name, SubCategoryID = s.Id })).ToList(),

                         });
            var countJobs = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countJobs) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.FlagID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            FlagProfessionalListWrapper professionals = new FlagProfessionalListWrapper();

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
        public FlagUserListWrapper FlagUsers(long? userID, int pageIndex, int pageSize)
        {
            var query = (from f in db.tbl_Flag
                         join u in db.tbl_User
                         on f.FlagTypeID equals u.Id
                         join c in db.tbl_State
                          on u.CityId equals c.Id
                         where f.FlagType == (int)HelperEnums.FlagType.User
                        && u.IsApprovedByAdmin == true && u.IsDeleted == false && u.IsVerified == true
                        &&u.Type==(int)(HelperEnums.UserType.User)
                         select new FlagUserDataContract
                         {
                             FlagID = f.ID,
                             UserID = u.Id,
                             UserImage = !string.IsNullOrEmpty(u.Image) ? Common.UserImagesPath + u.Image : Common.NoImageIcon,
                             Username = u.FirstName + " " + u.FirstName,
                             UserLocation=c==null?"":c.City+", "+c.State+", "+c.zip,

                         });
            var countJobs = query.Count();

            decimal totalpages = Math.Ceiling(Convert.ToDecimal(countJobs) / Convert.ToDecimal(pageSize));

            var data = query.OrderByDescending(p => p.FlagID).Skip(pageSize * pageIndex).Take(pageSize).ToList();

            FlagUserListWrapper users = new FlagUserListWrapper();

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
    }

}
