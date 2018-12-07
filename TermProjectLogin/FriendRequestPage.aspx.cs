using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialNetwork;

namespace TermProjectLogin
{
    public partial class FriendRequestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if(!IsPostBack)
            {
                PopulateFriendRequests();
            }
        }

        private void PopulateFriendRequests()
        {
            if (Session.GetUser() != null)
            {
                var user = Session.GetUser();
                var socialNetworkManager = Session.GetSocialNetworkManager();
                gvIncomingFriendRequests.DataKeyNames = new string[] { "Email" };
                gvIncomingFriendRequests.DataSource = socialNetworkManager.GetIncomingFriendRequests(user.ContactInfo.Email);
                gvIncomingFriendRequests.DataBind();
                gvOutgoingFriendRequests.DataKeyNames = new string[] { "Email" };
                gvOutgoingFriendRequests.DataSource = socialNetworkManager.GetOutgoingFriendRequests(user.ContactInfo.Email);
                gvOutgoingFriendRequests.DataBind();
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            var user = Session.GetUser();
            var socialNetworkManager = Session.GetSocialNetworkManager();
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.CommandArgument);
            string fromEmail = gvIncomingFriendRequests.DataKeys[index].Value.ToString();
            string toEmail = user.Email;

            if(socialNetworkManager.AcceptFriendRequest(fromEmail, toEmail))
            {
                var notification = new Notification()
                {
                    URL = "FriendRequestPage.aspx",
                    Description = user.Name + " accepted your friend request!"
                };

                lblMsg.Text = "Friend Request Accepted!";

                PopulateFriendRequests();
                socialNetworkManager.CreateNotification(notification, fromEmail);
            }
        }

        protected void btnDecline_Click(object sender, EventArgs e)
        {
            var user = Session.GetUser();
            var socialNetworkManager = Session.GetSocialNetworkManager();
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.CommandArgument);
            string fromEmail = gvIncomingFriendRequests.DataKeys[index].Value.ToString();
            string toEmail = user.Email;

            if (socialNetworkManager.DeclineFriendRequest(fromEmail, toEmail))
            {
                var notification = new Notification()
                {
                    URL = "FriendRequestPage.aspx",
                    Description = user.Name + " declined your friend request."
                };

                lblMsg.Text = "Friend Request Declined.";

                PopulateFriendRequests();
                socialNetworkManager.CreateNotification(notification, fromEmail);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            var user = Session.GetUser();
            var socialNetworkManager = Session.GetSocialNetworkManager();
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.CommandArgument);
            string fromEmail = user.Email;
            string toEmail =  gvOutgoingFriendRequests.DataKeys[index].Value.ToString();

            if (socialNetworkManager.CancelFriendRequest(fromEmail, toEmail))
            { 
                
                var notification = new Notification()
                {
                    URL = "FriendRequestPage.aspx",
                    Description = user.Name + " canceled their friend request."
                };
                

                lblMsg.Text = "Canceled Friend Request.";

                PopulateFriendRequests();
                socialNetworkManager.CreateNotification(notification, fromEmail);
            }
        }
    }
}