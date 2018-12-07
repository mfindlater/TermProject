using SocialNetwork;
using System;
using System.Web;
using System.Collections.Generic;

using System.Web.Script.Serialization;  // needed for JSON serializers
using System.IO;                        // needed for Stream and Stream Reader
using System.Net;                       // needed for the Web Request
using System.Web.UI.WebControls;
using System.Linq;

namespace TermProjectLogin
{
    public partial class Common : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            var socialNetworkManager = Session.GetSocialNetworkManager();

            if (!IsPostBack)
            {
                pnLogin.Visible = true;
                pnLoggedIn.Visible = false;

                bool isLoggedIn = false;

                if(Request.Cookies[Constants.UserCookie] != null)
                {
                    var userCookie = Request.Cookies[Constants.UserCookie];

                    User user = null;

                    if (Session[Constants.UserSession] != null)
                    {
                        user = Session.GetUser();

                        // Get Updated User
                        user = socialNetworkManager.GetUser(user.ContactInfo.Email);

                        // Reset Session
                        Session.SetUser(user);

                        lblName.Text = user.Name;
                        imgBtnProfilePhoto.ImageUrl = user.ProfilePhotoURL;
                    }
                    
                    isLoggedIn = user != null;

                    if (!isLoggedIn)
                    {
                        string email = "";
                        string password = "";

                        if (userCookie.Values[Constants.UserEmailCookie] != null)
                        {
                            email = userCookie.Values[Constants.UserEmailCookie];
                            txtEmail.Text = email;
                        }

                        if (userCookie.Values[Constants.UserPasswordCookie] != null)
                        {
                            password = EncryptionManager.Decrypt(userCookie.Values[Constants.UserPasswordCookie]);
                        }

                        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                        {
                            HandleLogin(txtEmail.Text, password);
                        }
                    }
                    else
                    {
                        pnLoggedIn.Visible = true;
                        pnLogin.Visible = false;
                        pnSearch.Visible = true;
                    }
                }
                else
                {
                    string name = Page.ToString();
                    if (name != "ASP.registrationpage_aspx" && name != "ASP.forgotpasswordpage_aspx")
                    {
                        Response.Redirect("RegistrationPage.aspx");
                    }
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            HandleLogin(email, password);
        }

        private void HandleLogin(string email, string password)
        {
            HttpCookie userCookie = new HttpCookie(Constants.UserCookie);

            var socialNetworkManager = Session.GetSocialNetworkManager();

            if (Request.Cookies[Constants.UserCookie] != null)
            {
                userCookie = Request.Cookies[Constants.UserCookie];
            }

            var user = socialNetworkManager.Login(email, password);
            if(user != null)
            {
                switch (user.Settings.LoginSetting)
                {
                    case LoginSettingType.AutoLogin:
                        userCookie.Values[Constants.UserEmailCookie] = email;
                        userCookie.Values[Constants.UserPasswordCookie] = EncryptionManager.Encrypt(password);
                        break;
                    case LoginSettingType.FastLogin:
                        userCookie.Values[Constants.UserEmailCookie] = email;
                        userCookie.Values[Constants.UserPasswordCookie] = null;
                        break;
                    case LoginSettingType.None:
                        userCookie.Values[Constants.UserEmailCookie] = null;
                        userCookie.Values[Constants.UserPasswordCookie] = null;
                        break;
                }
                Session[Constants.UserSession] = user;
                userCookie.Expires = DateTime.Now.AddYears(10);
                Response.Cookies.Add(userCookie);
                Response.Redirect("MainPage.aspx");
                return;
            }

            lblMessage.Text = "Invalid email or password provided.";
        }

        protected void btnSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("SettingsPage.aspx");
        }

        protected void imgBtnProfilePhoto_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            if (Session[Constants.UserSession] != null)
            {
                var user = (User)Session[Constants.UserSession];
                if (user.Settings.LoginSetting == LoginSettingType.AutoLogin)
                {
                    if (Request.Cookies[Constants.UserCookie] != null)
                    {
                        var cookie = Request.Cookies[Constants.UserCookie];
                        cookie.Expires = DateTime.Now.AddDays(-1);
                    }
                }
                Session.Remove(Constants.UserSession);
            }

            if (Request.Cookies[Constants.UserCookie] != null)
            {
                var cookie = Request.Cookies[Constants.UserCookie];
                Response.Cookies.Add(cookie);
            }

            Response.Redirect("RegistrationPage.aspx");
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPasswordPage.aspx");
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSearch.SelectedValue == "City & State")
            {
                txtSearch2.Visible = true;
            }
            else
            {
                txtSearch2.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = ddlSearch.SelectedValue;
            string requestUriString = "";
            string input1 = txtSearch1.Text;
            string input2 = "";

            switch (search)
            {
                case "Name":
                    requestUriString = Constants.RequestUriByName;
                    break;
                case "City & State":
                    input2 = txtSearch2.Text;
                    requestUriString = Constants.RequestUriByLocation;
                    break;
                case "Organization":
                    requestUriString = Constants.RequestUriByOrganization;
                    break;
                case "Like":
                    requestUriString = Constants.RequestUriByLike;
                    break;
                case "Dislike":
                    requestUriString = Constants.RequestUriByDislike;
                    break;
            }

            WebRequest request = null;

            if (search == "Name" || search == "Organization" || search == "Like" || search == "Dislike")
            {
                if (!string.IsNullOrEmpty(input1))
                {
                    request = WebRequest.Create(requestUriString + input1);
                }
            }
            else if (search == "City & State")
            {
                if (!string.IsNullOrEmpty(input1) && !string.IsNullOrEmpty(input2))
                {
                    request = WebRequest.Create(requestUriString + input1 + "/" + input2);
                }
            }
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            User user = Session.GetUser();
            var socialNetworkManager = Session.GetSocialNetworkManager();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<User> userList = js.Deserialize<List<User>>(data);
            gvSearchResult.DataKeyNames = new string[] { "Email" };
            userList = userList.Where(u => u.Email != user.Email).ToList();

            gvSearchResult.DataSource = userList;
            gvSearchResult.DataBind();

            for (int i = 0; i < gvSearchResult.Rows.Count; i++)
            {
                Button button = (Button)gvSearchResult.Rows[i].FindControl("btnAddFriend");
                string email = gvSearchResult.DataKeys[i].Value.ToString();
                if (socialNetworkManager.AreFriends(user.ContactInfo.Email, email))
                {
                    button.Enabled = false;
                }
            }

            gvSearchResult.Visible = true;
        }

        protected void btnNotification_Click(object sender, EventArgs e)
        {
            Response.Redirect("NotificationPage.aspx");
        }

        protected void btnAddFriend_Click(object sender, EventArgs e)
        {
            var user = Session.GetUser();
            var socialNetworkManager = Session.GetSocialNetworkManager();

            Notification notification = new Notification();

            Button button = (Button)sender;
            int index = Convert.ToInt32(button.CommandArgument);
            string email = gvSearchResult.DataKeys[index].Value.ToString();

            notification.URL = "FriendRequestPage.aspx";
            notification.Description = user.Name + " sent you a friend request!";

            socialNetworkManager.CreateFriendRequest(user.ContactInfo.Email, email);
            socialNetworkManager.CreateNotification(notification, email);
        }
    }
}