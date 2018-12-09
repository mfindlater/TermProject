using SocialNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

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

                    if (viewingUser != null)
                    {
                        rptWall.DataSource = socialNetworkManager.GetWall(viewingUser.Email);
                        rptPhoto.DataSource = viewingUser.Photos;
                        rptFriend.DataSource = viewingUser.Friends;
                    }
                    else
                    {
                        rptWall.DataSource = socialNetworkManager.GetWall(currentUser.Email);
                        rptPhoto.DataSource = currentUser.Photos;
                        rptFriend.DataSource = currentUser.Friends;
                    }
                    rptWall.DataBind();
                    rptPhoto.DataBind();
                    rptFriend.DataBind();
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
            currentUser = Session.GetUser();
            socialNetworkManager = Session.GetSocialNetworkManager();
            viewingUser = GetViewingUser();

            if (viewingUser == null)
            {
                if (!string.IsNullOrEmpty(txtContent.Text))
                {
                    Post post = new Post();

                    if (photoUpload.HasFile)
                    {
                        string filename = currentUser.UserId + "_" + Path.GetFileName(photoUpload.PostedFile.FileName);
                        photoUpload.SaveAs(Constants.StoragePath + filename);

                        Photo photo = new Photo();
                        photo.URL = Constants.StorageURL + filename;
                        photo.Description = txtPhotoDescription.Text;

                        photo = socialNetworkManager.AddPhoto(photo, currentUser.Email);
                        post.Photo = photo;
                    }

                    post.Content = txtContent.Text;
                    post = socialNetworkManager.CreatePost(post, currentUser.Email);
                    lblMsg.Text = "Posted!";

                    List<User> followers = socialNetworkManager.GetFollowers(currentUser.Email);
                    for (int i = 0; i < followers.Count; i++)
                    {
                        Notification notification = new Notification();
                        notification.Description = $"{currentUser.Name} shared a post.";
                        notification.URL = $"MainPage.aspx?Email={currentUser.Email}";

                        socialNetworkManager.CreateNotification(notification, followers[i].Email);
                    }
                }
            }
            else
            {
                Post post = new Post();

                if (photoUpload.HasFile)
                {
                    string filename = currentUser.UserId + "_" + Path.GetFileName(photoUpload.PostedFile.FileName);
                    photoUpload.SaveAs(Constants.StoragePath + filename);

                    Photo photo = new Photo();
                    photo.URL = Constants.StorageURL + filename;
                    photo.Description = txtPhotoDescription.Text;

                    photo = socialNetworkManager.AddPhoto(photo, currentUser.Email);
                    post.Photo = photo;
                }

                post.Content = txtContent.Text;
                post = socialNetworkManager.CreatePost(post, currentUser.Email, viewingUser.Email);
                lblMsg.Text = "Posted!";

                List<User> followers = socialNetworkManager.GetFollowers(currentUser.Email);
                followers = followers.Where(u => u.Email != viewingUser.Email).ToList();

                for (int i = 0; i < followers.Count; i++)
                {
                    Notification notification = new Notification();
                    notification.Description = $"{currentUser.Name} shared a post on {viewingUser.Name}'s wall.";
                    notification.URL = $"MainPage.aspx?Email={viewingUser.Email}";

                    socialNetworkManager.CreateNotification(notification, followers[i].Email);
                }

                Notification notificationTwo = new Notification();
                notificationTwo.Description = $"{currentUser.Name} shared a post on your wall.";
                notificationTwo.URL = $"MainPage.aspx?Email={viewingUser.Email}";
                socialNetworkManager.CreateNotification(notificationTwo, viewingUser.Email);
            }

            if (viewingUser != null)
            {
                rptWall.DataSource = socialNetworkManager.GetWall(viewingUser.Email);
            }
            else
            {
                rptWall.DataSource = socialNetworkManager.GetWall(currentUser.Email);
            }
            rptWall.DataBind();
        }

        protected void imgFriend_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            string email = imageButton.CommandArgument;

            Response.Redirect($"MainPage.aspx?Email={ email }");
        }

        protected void lbtnName_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string email = linkButton.CommandArgument;

            Response.Redirect($"MainPage.aspx?Email={ email }");
        }
    }
}