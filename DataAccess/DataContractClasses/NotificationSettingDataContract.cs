using DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataContracts
{

    public class NotificationSettingDataContract
    {

        public long? UserID { get; set; }
        public long? ID{ get; set; }

        public bool? NewMessage { get; set; }
        public bool? Recommendations { get; set; }
        public bool? NewFollowers { get; set; }
        public bool? EstimateFeedback{ get; set; }
        public bool? AppointmentRequest { get; set; }

    }
}
