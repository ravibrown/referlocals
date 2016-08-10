using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.HelperClasses
{
    public class HelperEnums
    {
        public enum CustomerType
        {
            Regular=1,Manual=2
        }
        public enum SocialType
        {
            Web=0,
            Facebook=1,Google=2
        }
        public enum PushNotificationType
        {
            Message=1,AppointmentRequest=2,Follow=3,NewJob=4,EstimateAccepted=5, EstimateDeclined= 6,AppointmentRescheduleByUser=7,
            AppointmentRescheduleByPro = 8,ReferredPro=9,JobInvitation=10,EstimateReceived=11,AppointmentConfirmed=12
        }
        public enum FlagType
        {
            User = 1, Professional = 2, Job = 3
        }
        public enum FavoriteType
        {
            User=1,Professional=2,Job=3
        }
        public enum BooleanValues
        {
            Both = 2,
            Approved = 1,
            Disapproved = 0
        }
        public enum QuoteStatus
        {
            Rejected= 2,
            Accepted = 1,
            Nothing= 0
        }
        public enum Role
        {
            Admin = 1,
            User = 2
        }

        public enum UserType
        {
            User = 1,
            Professional = 2
        }
        public enum JobStatus
        { 
            Open=1,Done=2,Cancel=3
        }
        public enum DateType
        {
            Today= 1,
            Week= 2,
            Month=3,
        }
    }
}
