using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    public class NewsFeedPost
    {
        public int NewsFeedPostID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
