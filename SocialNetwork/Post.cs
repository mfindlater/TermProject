﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    public class Post
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        public string PhotoURL { get; set; }
    }
}