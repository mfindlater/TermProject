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
        SocialNetworkManager socialNetworkManager;
        User currentUser;
        User viewingUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.GetUser() != null)
                {
                    socialNetworkManager = Session.GetSocialNetworkManager();
                    currentUser = Session.GetUser();

                    imgProfilePhoto.ImageUrl = currentUser.ProfilePhotoURL;
                    lblName.Text = currentUser.Name;

                    if (Request.QueryString["Email"] != null)
                    {
                        string email = "";
                        foreach (var item in Request.QueryString["Email"])
                        {
                            email += item;
                        }

                        viewingUser = socialNetworkManager.GetUser(email);

                        imgProfilePhoto.ImageUrl = viewingUser.ProfilePhotoURL;
                        lblName.Text = viewingUser.Name;
                        btnAddFriend.Visible = true;
                        btnFollow.Visible = true;
                        btnChat.Visible = true;

                        if (socialNetworkManager.AreFriends(currentUser.Email, viewingUser.Email))
                        {
                            btnAddFriend.Enabled = false;
                        }

                        if (socialNetworkManager.IsFollowing(viewingUser.Email, currentUser.Email))
                        {
                            btnFollow.Text = "Unfollow";
                        }
                    }

                    pnUploadPhoto.Visible = false;
                }
            }
        }

        protected void btnFollow_Click(object sender, EventArgs e)
        {
            currentUser = Session.GetUser();
            viewingUser = GetViewingUser();

            if (btnFollow.Text == "Follow")
            {
                socialNetworkManager.FollowUser(viewingUser.Email, currentUser.Email);
                btnFollow.Text = "Unfollow";
            }
            else
            {
                socialNetworkManager.UnfollowUser(viewingUser.Email, currentUser.Email);
                btnFollow.Text = "Follow";
            }
        }

        protected void lbtnPost_Click(object sender, EventArgs e)
        {
            pnUploadPhoto.Visible = false;
        }

        protected void lbtnPhoto_Click(object sender, EventArgs e)
        {
            pnUploadPhoto.Visible = true;
        }

        protected void lbtnPhotos_Click(object sender, EventArgs e)
        {
            Response.Redirect("PhotoPage.aspx");
        }

        protected void lbtnFriends_Click(object sender, EventArgs e)
        {
            Response.Redirect("FriendPage.aspx");
        }

        private User GetViewingUser()
        {
            socialNetworkManager = Session.GetSocialNetworkManager();

            if (Request.QueryString["Email"] != null)
            {
                string email = "";
                foreach (var item in Request.QueryString["Email"])
                {
                    email += item;
                }

                return socialNetworkManager.GetUser(email);
            }

            return null;
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContent.Text))
            {

            }
        }
    }
}