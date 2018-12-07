using SocialNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProjectLogin
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.GetUser() != null)
                {
                    var socialNetworkManager = Session.GetSocialNetworkManager();
                    User currentUser = Session.GetUser();
                    User viewingUser = null;

                    lblStatus.Text = $"{currentUser.Name} is logged in.";

                    if (Request.QueryString["Email"] != null)
                    {
                        string email = "";
                        foreach (var item in Request.QueryString["Email"])
                        {
                            email += item;
                        }

                       viewingUser = socialNetworkManager.GetUser(email);

                        lblStatus.Text += $"<br/> {viewingUser.Name}";
                    }
                }
            }
        }
    }
}