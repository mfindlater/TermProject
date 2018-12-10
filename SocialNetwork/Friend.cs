using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public class Friend
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ProfilePhotoURL { get; set; }
        public bool OnlineStatus { get; set; }
        public FriendRequestStatus FriendRequestStatus { get; set; }
    }
}
