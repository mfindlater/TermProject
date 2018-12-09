using SocialNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermProjectLogin
{
    public static class HttpRequestExtensions
    {
        public static User GetViewingUser(this HttpRequest request, SocialNetworkManager socialNetworkManager)
        {
            if (request.QueryString["Email"] != null)
            {
                string email = "";
                foreach (var item in request.QueryString["Email"])
                {
                    email += item;
                }

                User viewingUser = socialNetworkManager.GetUser(email);

                return viewingUser;
            }
            return null;
        }
    }
}