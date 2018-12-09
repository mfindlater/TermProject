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
    public partial class PhotoPage1 : System.Web.UI.Page
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
                    viewingUser = Request.GetViewingUser(socialNetworkManager);

                    if (viewingUser != null)
                    {
                        rptPhoto.DataSource = viewingUser.Photos;
                        pnCreateAlbum.Visible = false;
                        rptAlbum.DataSource = viewingUser.PhotoAlbums;
                    }
                    else
                    {
                        rptPhoto.DataSource = currentUser.Photos;
                        rptAlbum.DataSource = currentUser.PhotoAlbums;
                    }
                    rptPhoto.DataBind();
                    rptAlbum.DataBind();
                }
            }
        }

        protected void imgBtnAlbum_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;

            socialNetworkManager = Session.GetSocialNetworkManager();
            
            currentUser = Session.GetUser();
            viewingUser = Request.GetViewingUser(socialNetworkManager);

            int photoAlbumID = Convert.ToInt32(imageButton.CommandArgument);
            List<Photo> photos;

            if (viewingUser != null)
            {
                photos = viewingUser.PhotoAlbums.Where(photoAlbum => photoAlbum.PhotoAlbumID == photoAlbumID).First().Photos;
            }
            else
            {
                photos = currentUser.PhotoAlbums.Where(photoAlbum => photoAlbum.PhotoAlbumID == photoAlbumID).First().Photos;
            }
            rptAlbumPhoto.DataSource = photos;
            rptAlbumPhoto.DataBind();
            pnAlbumPhoto.Visible = true;
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

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            currentUser = Session.GetUser();
            socialNetworkManager = Session.GetSocialNetworkManager();

            if (!string.IsNullOrEmpty(txtAlbumName.Text) && !string.IsNullOrEmpty(txtDescription.Text))
            {
                if (photoUpload.HasFile)
                {
                    string filename = currentUser.UserId + "_" + Path.GetFileName(photoUpload.PostedFile.FileName);
                    photoUpload.SaveAs(Constants.StoragePath + filename);

                    Photo photo = new Photo();
                    photo.URL = Constants.StorageURL + filename;
                    photo.Description = txtPhotoDescription.Text;

                    PhotoAlbum photoAlbum = new PhotoAlbum();
                    photo = socialNetworkManager.AddPhoto(photo, currentUser.Email);
                    photoAlbum.Name = txtAlbumName.Text;
                    photoAlbum.Description = txtDescription.Text;
                    photoAlbum.Photos.Add(photo);

                    socialNetworkManager.CreatePhotoAlbum(photoAlbum, currentUser.Email);
                    lblMsg.Text = "Album Created!";
                }
            }
        }
    }
}