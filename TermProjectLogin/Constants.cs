﻿using System;
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
#if DEBUG
        public const string baseURL = "http://localhost:63063/api/SocialNetworkService/";
#else 
        public const string baseURL = "http://cis-iis2.temple.edu/Fall2018/CIS3342_tug98770/TermProjectWS/api/SocialNetworkService/";
#endif
        public const string RequestUriByName = baseURL + "searchByName/";
        public const string RequestUriByLocation = baseURL + "searchByLocation/";
        public const string RequestUriByOrganization = baseURL + "searchByOrganization/";
        public const string RequestUriByLike = baseURL + "searchByLike/";
        public const string RequestUriByDislike = baseURL + "searchByDislike/";



        public const string StoragePath = @"W:\TermProject\Storage\";

        public const string StorageURL = "http://cis-iis2.temple.edu/Fall2018/CIS3342_tug98770/TermProject/Storage/";

        public const string ManagerSession = "ManagerSession";
    }
}