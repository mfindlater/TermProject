using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public class RegisterInfo : UserInfo
    {
        public string Password { get; set; }
        public List<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
