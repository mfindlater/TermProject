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
        private SqlRepository sqlRepository = new SqlRepository();
        private SocialNetworkManager socialNetworkManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            socialNetworkManager = new SocialNetworkManager(sqlRepository);

            if (!IsPostBack && Session[Constants.UserSession] != null)
            {
                var user = Session.GetUser();

                List<Notification> notifications = sqlRepository.GetNotifications(user.ContactInfo.Email);
                gvNotification.DataKeyNames = new string[] { "NotificationID" };

                gvNotification.DataSource = notifications;
                gvNotification.DataBind();
            }
        }

        protected void lbtnURL_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;

            int index = Convert.ToInt32(linkButton.CommandArgument);
            int notificationID = Convert.ToInt32(gvNotification.DataKeys[index].Value);

            socialNetworkManager.ReadNotification(notificationID);

            Response.Redirect(linkButton.Text);
        }
    }
}