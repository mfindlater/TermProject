﻿using SocialNetwork;
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
                        btnFollow.Visible = true;
                        btnChat.Visible = false;
                        btnAddFriend.Visible = true;

                        ChangeFriendButton();

                        if (socialNetworkManager.IsFollowing(viewingUser.Email, currentUser.Email))
                        {
                            btnFollow.Text = "Unfollow";
                        }
                    }

                    pnUploadPhoto.Visible = false;

                    if (viewingUser != null)
                    {
                        pnPhotos.Visible = true;
                        pnFriends.Visible = true;
                        pnWallNewsFeed.Visible = true;
                        pnCreatePost.Visible = true;
                        btnFollow.Visible = true;
                        pnContactInfo.Visible = true;
                        pnLikesDislikes.Visible = true;


                        // Profile
                        switch (viewingUser.Settings.UserInfoSetting)
                        {
                            case PrivacySettingType.Public:
                                break;
                            case PrivacySettingType.Friends:
                                if (!socialNetworkManager.AreFriends(currentUser.Email, viewingUser.Email))
                                {
                                    pnFriends.Visible = false;
                                    pnWallNewsFeed.Visible = false;
                                    pnCreatePost.Visible = false;
                                    btnFollow.Visible = false;
                                    pnLikesDislikes.Visible = false;
                                }
                                break;
                            case PrivacySettingType.FriendsOfFriends:
                                if (!socialNetworkManager.AreFriends(currentUser.Email, viewingUser.Email) &&
                                 !socialNetworkManager.IsFriendOfFriend(currentUser.Email, viewingUser.Email))
                                {
                                    pnFriends.Visible = false;
                                    pnWallNewsFeed.Visible = false;
                                    pnCreatePost.Visible = false;
                                    btnFollow.Visible = false;
                                    pnLikesDislikes.Visible = false;
                                }
                                break;
                        }

                        // Photos
                        switch(viewingUser.Settings.PhotoPrivacySetting)
                        {
                            case PrivacySettingType.Public:
                                break;
                            case PrivacySettingType.Friends:
                                if(!socialNetworkManager.AreFriends(currentUser.Email, viewingUser.Email))
                                {
                                    pnPhotos.Visible = false;
                                }
                                break;

                            case PrivacySettingType.FriendsOfFriends:
                                if(!socialNetworkManager.AreFriends(currentUser.Email, viewingUser.Email) &&
                                    !socialNetworkManager.IsFriendOfFriend(currentUser.Email, viewingUser.Email))
                                {
                                    pnPhotos.Visible = false;
                                }
                                break;
                        }

                        // Contact
                        switch (viewingUser.Settings.ContactInfoPrivacySetting)
                        {
                            case PrivacySettingType.Public:
                                break;
                            case PrivacySettingType.Friends:
                                if (!socialNetworkManager.AreFriends(currentUser.Email, viewingUser.Email))
                                {
                                    pnContactInfo.Visible = false;
                                }
                                break;

                            case PrivacySettingType.FriendsOfFriends:
                                if (!socialNetworkManager.AreFriends(currentUser.Email, viewingUser.Email) &&
                                    !socialNetworkManager.IsFriendOfFriend(currentUser.Email, viewingUser.Email))
                                {
                                    pnContactInfo.Visible = false;
                                }
                                break;
                        }

                        rptPhoto.DataSource = viewingUser.Photos;
                        rptFriend.DataSource = viewingUser.Friends;
                        lbtnNewsFeed.Visible = false;

                        rptWall.DataSource = socialNetworkManager.GetWall(viewingUser.Email);

                        ShowContactInfo(viewingUser);
                        ShowLikesDislikes(viewingUser);
                    }
                    else
                    {
                        rptPhoto.DataSource = currentUser.Photos;
                        rptFriend.DataSource = currentUser.Friends;
                        lbtnNewsFeed.Visible = true;
                        btnAddFriend.Visible = false;

                        if (ViewState["ViewMode"] == null || ViewState["ViewMode"].ToString() == "Wall")
                        {
                            rptWall.DataSource = socialNetworkManager.GetWall(currentUser.Email);
                        }
                        else
                        {
                            rptWall.DataSource = socialNetworkManager.GetNewsFeed(currentUser.Email);
                        }

                        ShowContactInfo(currentUser);
                        ShowLikesDislikes(currentUser);
                    }
                    rptWall.DataBind();
                    rptPhoto.DataBind();
                    rptFriend.DataBind();
                }
            }
        }

        private void ChangeFriendButton()
        {
            currentUser = Session.GetUser();
            socialNetworkManager = Session.GetSocialNetworkManager();
            viewingUser = GetViewingUser();

            if (currentUser == null || viewingUser == null)
            {
                return;
            }

            var outgoingFriendRequests = socialNetworkManager.GetOutgoingFriendRequests(currentUser.Email).Where(u => u.Email == viewingUser.Email);
            var incomingFriendRequests = socialNetworkManager.GetIncomingFriendRequests(currentUser.Email).Where(u => u.Email == viewingUser.Email);

            if (socialNetworkManager.AreFriends(currentUser.Email, viewingUser.Email))
            {
                btnChat.Visible = true;
                btnAddFriend.Text = "Remove Friend";
            }
            else if (outgoingFriendRequests.Count() > 0)
            {
                btnAddFriend.Text = "Cancel Request";
            }
            else if (incomingFriendRequests.Count() > 0)
            {
                btnAddFriend.Text = "Accept Request";
            }
            else
            {
                btnAddFriend.Text = "Add Friend";
            }
        }

        private void ShowContactInfo(User user)
        {
            lblBirthdate.Text += user.BirthDate.ToString("d");
            lblEmail.Text += user.Email;
            lblPhone.Text += user.ContactInfo.Phone;
            lblAddress.Text += "<br />" + user.Address.AddressLine1 + " " + user.Address.AddressLine2 + "<br />" + user.Address.City + " " + user.Address.State + "" + user.Address.PostalCode;
            lblOrganization.Text += user.Organization;
        }

        private void ShowLikesDislikes(User user)
        {
            lblLikes.Text += user.Likes;
            lblDislikes.Text += user.Dislikes;
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
            viewingUser = GetViewingUser();
            if (viewingUser != null)
            {
                Response.Redirect($"PhotoPage.aspx?Email={viewingUser.Email}");
            }
            else
            {
                Response.Redirect("PhotoPage.aspx");
            }
        }

        protected void lbtnFriends_Click(object sender, EventArgs e)
        {
            viewingUser = GetViewingUser();
            if (viewingUser != null)
            {
                Response.Redirect($"FriendPage.aspx?Email={viewingUser.Email}");
            }
            else
            {
                Response.Redirect("FriendPage.aspx");
            }
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
                        string url = photoUpload.Save(Server,filename);

                        Photo photo = new Photo();
                        photo.URL = url;
                        photo.Description = HttpUtility.HtmlEncode(txtPhotoDescription.Text);

                        photo = socialNetworkManager.AddPhoto(photo, currentUser.Email);
                        post.Photo = photo;
                    }

                    post.Content = HttpUtility.HtmlEncode(txtContent.Text);
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
                    string url = photoUpload.Save(Server,filename);

                    Photo photo = new Photo();
                    photo.URL = url;
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

            txtPhotoDescription.Text = "";
            txtContent.Text = "";
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

        protected void lbtnWall_Click(object sender, EventArgs e)
        {
            socialNetworkManager = Session.GetSocialNetworkManager();
            currentUser = Session.GetUser();
            viewingUser = GetViewingUser();

            ViewState["ViewMode"] = "Wall";

            if (viewingUser == null)
            {
                rptWall.DataSource = socialNetworkManager.GetWall(currentUser.Email);
            }
            else
            {
                rptWall.DataSource = socialNetworkManager.GetWall(viewingUser.Email);
            }
            rptWall.DataBind();
        }

        protected void lbtnNewsFeed_Click(object sender, EventArgs e)
        {
            socialNetworkManager = Session.GetSocialNetworkManager();
            currentUser = Session.GetUser();

            ViewState["ViewMode"] = "NewsFeed";

            rptWall.DataSource = socialNetworkManager.GetNewsFeed(currentUser.Email);
            rptWall.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            Page.ClientScript.RegisterForEventValidation(ChatControl.UniqueID, this.ToString());
            base.Render(writer);
        }

        protected void btnChat_Click(object sender, EventArgs e)
        {
            socialNetworkManager = Session.GetSocialNetworkManager();
            viewingUser = Request.GetViewingUser(socialNetworkManager);

            ChatControl.ShowChat(viewingUser.Email);
        }

        protected void btnAddFriend_Click(object sender, EventArgs e)
        {
            socialNetworkManager = Session.GetSocialNetworkManager();
            currentUser = Session.GetUser();
            viewingUser = Request.GetViewingUser(socialNetworkManager);

            string friendStatus = btnAddFriend.Text;

            switch (friendStatus)
            {
                case "Remove Friend":
                    socialNetworkManager.RemoveFriend(currentUser.Email, viewingUser.Email);
                    break;
                case "Cancel Request":
                    socialNetworkManager.CancelFriendRequest(currentUser.Email, viewingUser.Email);
                    break;
                case "Accept Request":
                    socialNetworkManager.AcceptFriendRequest(viewingUser.Email, currentUser.Email);
                    break;
                case "Add Friend":
                    socialNetworkManager.CreateFriendRequest(currentUser.Email, viewingUser.Email);
                    break;
            }

            Response.Redirect($"MainPage.aspx?Email={viewingUser.Email}");
        }
    }
}