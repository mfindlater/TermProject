using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialNetwork;

namespace TermProjectLogin
{
    public partial class ChatControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session.GetUser();
            if (user != null)
            {
                rptUser.DataSource = user.Friends;
                rptUser.DataBind();
            }
        }

        protected void lbtnName_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string email = linkButton.CommandArgument;

            Response.Redirect($"MainPage.aspx?Email={ email }");
        }

        protected void btnChat_Click(object sender, EventArgs e)
        {
            Button btnChat = (Button)sender;
            string email = btnChat.CommandArgument;
            GetChat(email);
            ViewState["Email"] = email;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            SocialNetworkManager socialNetworkManager = Session.GetSocialNetworkManager();
            User currentUser = Session.GetUser();

            if (ViewState["Email"] != null && !string.IsNullOrEmpty(txtMessage.Text))
            {
                string email = ViewState["Email"].ToString();
                ChatMessage message = new ChatMessage();
                message.FromEmail = currentUser.Email;
                message.ToEmail = email;
                message.Message = txtMessage.Text;
                socialNetworkManager.SendMessage(message);
                GetChat(email);
            }
        }

        private void GetChat(string email)
        {
            SocialNetworkManager socialNetworkManager = Session.GetSocialNetworkManager();
            User currentUser = Session.GetUser();

            rptChat.DataSource = socialNetworkManager.GetMessages(currentUser.Email, email);

        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            if (ViewState["Email"] != null)
            {
                string email = ViewState["Email"].ToString();
                GetChat(email);
            }
        }

        protected void rptChat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Button button = (Button)e.Item.FindControl("btnChat");
            ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(button);
        }
    }
}