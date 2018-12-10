using SocialNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProjectLogin
{
    public partial class FriendPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.GetUser() != null)
                {
                    SocialNetworkManager socialNetworkManager = Session.GetSocialNetworkManager();
                    User currentUser = Session.GetUser();
                    User viewingUser = Request.GetViewingUser(socialNetworkManager);

                    if (viewingUser != null)
                    {
                        rptFriend.DataSource = viewingUser.Friends;
                    }
                    else
                    {
                        rptFriend.DataSource = currentUser.Friends;
                    }
                    rptFriend.DataBind();
                }
            }
        }

        protected void imgFriend_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            string email = imageButton.CommandArgument;

            Response.Redirect($"MainPage.aspx?Email={ email }");
        }
    }
}