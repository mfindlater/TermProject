using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    [Serializable]
    public class Theme
    {
        public string Name { get; set; }
        public string FontColor { get; set; }
        public string BackgroundColor { get; set; }
        public int FontSize { get; set; }
        public string FontWeight { get; set; }
    }
}
