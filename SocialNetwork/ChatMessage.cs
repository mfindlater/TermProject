﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    public class ChatMessage
    {
        public int MessageID { get; set; }
        public int ChatID { get; set; }
        public int FromUserID { get; set; }
        public int ToUserID { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
    }
}