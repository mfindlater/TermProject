﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public class Photo
    {
        public int PhotoID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
