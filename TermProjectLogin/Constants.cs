using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermProjectLogin
{
    public class Constants
    {
        public const string UserSession = "UserSession";
        public const string UserCookie = "UserCookie";
        public const string UserEmailCookie = "UserEmail";
        public const string UserPasswordCookie = "UserPassword";

        public static string[] SecurityQuestion1 = { "What is your favorite color?", "In what city did your mother and father meet?"
            , "What is your maternal grandmother's maiden name?", "Who is your favorite actor, musician, or artist?",
            "In what city were you born?" };
        public static string[] SecurityQuestion2 = { "What is your favorite movie?", "Who was your childhood hero?",
            "What was your favorite food?", "What is the make of your first car?", "What is the name of your first pet?" };
        public static string[] SecurityQuestion3 = { "What is your favorite team?", "What was your childhood nickname?",
            "In what city was your first full time job?", "What primary school did you attend?", "What is your oldest sibling's middle name?" };
    }
}