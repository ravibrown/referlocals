﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ReferLocalsEntities : DbContext
    {
        public ReferLocalsEntities()
            : base("name=ReferLocalsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_HomeCards> tbl_HomeCards { get; set; }
        public virtual DbSet<tbl_HomeVideo> tbl_HomeVideo { get; set; }
        public virtual DbSet<tbl_Category> tbl_Category { get; set; }
        public virtual DbSet<tbl_SubCategory> tbl_SubCategory { get; set; }
        public virtual DbSet<tbl_Testimonial> tbl_Testimonial { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<tbl_State> tbl_State { get; set; }
        public virtual DbSet<tbl_User_City_Mapping> tbl_User_City_Mapping { get; set; }
        public virtual DbSet<tbl_User_SubCategory_Mapping> tbl_User_SubCategory_Mapping { get; set; }
        public virtual DbSet<tbl_UserImages> tbl_UserImages { get; set; }
        public virtual DbSet<tbl_Referal> tbl_Referal { get; set; }
        public virtual DbSet<tbl_Job_SubCategory_Mapping> tbl_Job_SubCategory_Mapping { get; set; }
        public virtual DbSet<tbl_Jobs> tbl_Jobs { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }
        public virtual DbSet<tbl_UserDevices> tbl_UserDevices { get; set; }
        public virtual DbSet<tbl_Quotes> tbl_Quotes { get; set; }
        public virtual DbSet<tbl_AppointmentDates> tbl_AppointmentDates { get; set; }
        public virtual DbSet<tbl_Appointments> tbl_Appointments { get; set; }
        public virtual DbSet<tbl_Favorite> tbl_Favorite { get; set; }
        public virtual DbSet<tbl_Flag> tbl_Flag { get; set; }
        public virtual DbSet<tbl_Messages> tbl_Messages { get; set; }
        public virtual DbSet<tbl_Threads> tbl_Threads { get; set; }
        public virtual DbSet<tbl_ThreadParticipants> tbl_ThreadParticipants { get; set; }
        public virtual DbSet<tbl_NotificationSettings> tbl_NotificationSettings { get; set; }
        public virtual DbSet<tbl_AppFeedBack> tbl_AppFeedBack { get; set; }
        public virtual DbSet<tbl_ManualCustomers> tbl_ManualCustomers { get; set; }
        public virtual DbSet<tbl_ManualCustomerNotes> tbl_ManualCustomerNotes { get; set; }
    
        public virtual ObjectResult<CountUserActivites_Result> CountUserActivites(Nullable<long> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CountUserActivites_Result>("CountUserActivites", userIDParameter);
        }
    
        public virtual ObjectResult<CountProfessionalActivites_Result> CountProfessionalActivites(Nullable<long> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CountProfessionalActivites_Result>("CountProfessionalActivites", userIDParameter);
        }
    }
}
