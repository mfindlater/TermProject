using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public bool ReadStatus { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
