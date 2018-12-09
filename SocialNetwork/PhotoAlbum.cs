using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    public class PhotoAlbum
    {
        public int PhotoAlbumID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
