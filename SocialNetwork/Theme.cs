using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    [Serializable]
    public class Theme
    {
        public int ThemeID { get; set; }
        public string Name { get; set; } = "default";
        public string FontColor { get; set; } = "#000000";
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public int FontSize { get; set; } = 12;
        public string FontWeight { get; set; } = "normal";
    }
}
