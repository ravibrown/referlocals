//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tbl_NotificationSettings
    {
        public long ID { get; set; }
        public Nullable<bool> NewFollowers { get; set; }
        public Nullable<bool> Recommendations { get; set; }
        public Nullable<bool> NewMessage { get; set; }
        public Nullable<bool> EstimateFeedback { get; set; }
        public Nullable<bool> AppointmentRequest { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual tbl_User tbl_User { get; set; }
    }
}
