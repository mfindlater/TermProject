using SocialNetwork;
using System;
using System.Web;

namespace TermProjectLogin
{
    public partial class Common : System.Web.UI.MasterPage
    {
        private const string UserCookie = "UserCookie";
        private const string UserEmailCookie = "UserEmail";
        private const string UserLoggedInCookie = "UserIsLoggedIn";
        private const string UserPasswordCookie = "UserPassword";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnLogin.Visible = true;
                pnLoggedIn.Visible = false;

                bool isLoggedIn = false;

                if(Request.Cookies[UserCookie] != null)
                {
                    var userCookie = Request.Cookies[UserCookie];

                    isLoggedIn = Convert.ToBoolean(userCookie.Values[UserLoggedInCookie]);

                    if (!isLoggedIn)
                    {
                        string email = "";
                        string password = "";

                        if (userCookie.Values[UserEmailCookie] != null)
                        {
                            email = userCookie.Values[UserEmailCookie];
                        }

                        if (userCookie.Values[UserPasswordCookie] != null)
                        {
                            password = EncryptionManager.Decode(userCookie.Values[UserPasswordCookie]);
                        }

                        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                        {
                            HandleLogin(txtEmail.Text, txtPassword.Text, LoginSettingType.FastLogin);
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
                    if(name != "ASP.registrationpage_aspx")
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

            HandleLogin(email, password, LoginSettingType.AutoLogin);
        }

        private void HandleLogin(string email, string password, LoginSettingType loginSetting)
        {
            HttpCookie userCookie = new HttpCookie(UserCookie);
        
            if(Request.Cookies[UserCookie] != null)
            {
                userCookie = Request.Cookies[UserCookie];
            }

            switch (loginSetting)
            {
                case LoginSettingType.AutoLogin:
                    userCookie.Values[UserEmailCookie] = email;
                    userCookie.Values[UserPasswordCookie] = EncryptionManager.Encode(password);
                    break;
                case LoginSettingType.FastLogin:
                    userCookie.Values[UserEmailCookie] = email;
                    userCookie.Values[UserPasswordCookie] = null;
                    break;
                case LoginSettingType.None:
                    userCookie.Values[UserEmailCookie] = null;
                    userCookie.Values[UserPasswordCookie] = null;
                    break;
            }

            userCookie.Values[UserLoggedInCookie] = "true";
            userCookie.Expires = DateTime.Now.AddYears(10);
            Response.Cookies.Add(userCookie);

            Response.Redirect("MainPage.aspx");
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

            if (Request.Cookies[UserCookie] != null)
            {
                Response.Cookies[UserCookie].Expires = DateTime.Now.AddDays(-1);
            }

            Response.Redirect("RegistrationPage.aspx");
        }
    }
}