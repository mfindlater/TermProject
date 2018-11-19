using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public class UserInfo
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
