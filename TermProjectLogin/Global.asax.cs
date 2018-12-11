using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using SocialNetwork;

namespace TermProjectLogin
{
    public class Global : System.Web.HttpApplication
    {
        private readonly SqlRepository sqlRepository = new SqlRepository();
        private SocialNetworkManager socialNetworkManager;

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            socialNetworkManager = new SocialNetworkManager(sqlRepository);
            Session[Constants.ManagerSession] = socialNetworkManager;
            Application.UnLock();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            var user = Session.GetUser();
            var socialNetworkManager = Session.GetSocialNetworkManager();
            socialNetworkManager.SetOnlineStatus(user.Email, false);
            socialNetworkManager.UpdateUser(user);
            Application.UnLock();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}