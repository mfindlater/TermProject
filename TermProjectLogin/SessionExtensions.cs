using SocialNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace TermProjectLogin
{
    public static class SessionExtensions
    {
        public static User GetUser(this HttpSessionState session)
        {
            return (User)session[Constants.UserSession];
        }
    }
}