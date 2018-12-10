using System;
using System.Collections.Generic;

namespace SocialNetwork
{
    [Serializable]
    public class UserSettings
    {
        public Theme Theme { get; set; } = new Theme();
        public LoginSettingType LoginSetting { get; set; }
        public PrivacySettingType ContactInfoPrivacySetting { get; set; }
        public PrivacySettingType PhotoPrivacySetting { get; set; }
        public PrivacySettingType UserInfoSetting { get; set; }
        public List<SecurityQuestion> SecurityQuestions { get; set; } = new List<SecurityQuestion>();
        public bool ReceiveEmailNotifications { get; set; }
    }
}
