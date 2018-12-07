using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProjectLogin
{
    public partial class FriendRequestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack && Session.GetUser() != null)
            {
                var user = Session.GetUser();
                var socialNetworkManager = Session.GetSocialNetworkManager();

                gvIncomingFriendRequests.DataSource = socialNetworkManager.GetIncomingFriendRequests(user.ContactInfo.Email);
                gvOutgoingFriendRequests.DataSource = socialNetworkManager.GetOutgoingFriendRequests(user.ContactInfo.Email);
            }
        }
    }
}