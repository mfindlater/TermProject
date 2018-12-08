using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    public class Post
    {
        public int PostID { get; set; }
        public int PosterID { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        public Photo Photo { get; set; }
        public bool HasPhoto
        {
            get { return Photo != null; }
        }
    }
}
