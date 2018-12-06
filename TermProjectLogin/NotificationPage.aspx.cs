using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialNetwork;

namespace TermProjectLogin
{
    public partial class NotificationPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var socialNetworkManager = Session.GetSocialNetworkManager();

            if (!IsPostBack && Session[Constants.UserSession] != null)
            {
                var user = Session.GetUser();

                List<Notification> notifications = socialNetworkManager.GetNotifications(user.ContactInfo.Email);
                gvNotification.DataKeyNames = new string[] { "NotificationID", "URL" };

                gvNotification.DataSource = notifications;
                gvNotification.DataBind();
            }
        }

        protected void lbtnURL_Click(object sender, EventArgs e)
        {
            var socialNetworkManager = Session.GetSocialNetworkManager();

            LinkButton linkButton = (LinkButton)sender;

            int index = Convert.ToInt32(linkButton.CommandArgument);
            int notificationID = Convert.ToInt32(gvNotification.DataKeys[index].Value);
            string URL = gvNotification.DataKeys[index].Values[1].ToString();

            socialNetworkManager.ReadNotification(notificationID);

            Response.Redirect(URL);
        }
    }
}