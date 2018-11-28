using SocialNetwork;
using System;
using System.Web;

namespace TermProjectLogin
{
    public partial class Common : System.Web.UI.MasterPage
    {
        private SqlRepository sqlRepository = new SqlRepository();
        private SocialNetworkManager socialNetworkManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            socialNetworkManager = new SocialNetworkManager(sqlRepository);

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
                        user = (User)Session[Constants.UserSession];
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
        
            if(Request.Cookies[Constants.UserCookie] != null)
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
    }
}