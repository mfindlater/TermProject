using SocialNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProjectLogin
{
    public partial class PhotoPage1 : System.Web.UI.Page
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
                        rptAlbum.DataSource = viewingUser.PhotoAlbums;
                    }
                    else
                    {
                        rptAlbum.DataSource = currentUser.PhotoAlbums;
                    }

                    rptAlbum.DataBind();
                }
            }
        }

        protected void lbtnAlbum_Click(object sender, EventArgs e)
        {
            pnAlbum.Visible = true;
            pnPhoto.Visible = false;
        }

        protected void lbtnPhoto_Click(object sender, EventArgs e)
        {
            pnPhoto.Visible = true;
            pnAlbum.Visible = false;
        }
    }
}