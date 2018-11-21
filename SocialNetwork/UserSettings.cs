using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public class UserSettings
    {
       public Theme Theme { get; set; }
       public LoginSettingType LoginSetting { get; set; }
       public PrivacySettingType ContactInfoPrivacySetting { get; set; }
       public PrivacySettingType PhotoPrivacySetting { get; set; }
       public PrivacySettingType UserInfoSetting { get; set; }
       public List<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
