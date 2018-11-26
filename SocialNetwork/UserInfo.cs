using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public class UserInfo
    {
        public string Name { get; set; }
        public Address Address { get; set; } = new Address();
        public ContactInfo ContactInfo { get; set; } = new ContactInfo();
        public DateTime BirthDate { get; set; }
    }
}
