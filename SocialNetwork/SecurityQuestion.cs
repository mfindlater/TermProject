using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    [Serializable]
    public class SecurityQuestion
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public bool CheckAnswer(string answer)
        {
            return answer == Answer;
        }
    }
}
