using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public class User : UserInfo
    {
        public int UserId { get; set; }
        public string ProfilePhotoURL { get; set; }
        public List<Friend> Friends { get; set; }
        public UserSettings Settings { get; set; }
        public List<Photo> Photos { get; set; }
        public string EncryptedPassword { get; set; }
    }
}
