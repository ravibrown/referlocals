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
    
    public partial class tbl_Messages
    {
        public long ID { get; set; }
        public Nullable<long> SenderID { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> MessageOn { get; set; }
        public Nullable<bool> IsJobMessage { get; set; }
        public Nullable<long> JobID { get; set; }
        public Nullable<bool> IsDeletedBySender { get; set; }
        public Nullable<bool> IsDeletedByReceiver { get; set; }
        public Nullable<long> ThreadID { get; set; }
        public Nullable<bool> IsViewed { get; set; }
    
        public virtual tbl_Jobs tbl_Jobs { get; set; }
        public virtual tbl_Threads tbl_Threads { get; set; }
        public virtual tbl_User tbl_User { get; set; }
    }
}
