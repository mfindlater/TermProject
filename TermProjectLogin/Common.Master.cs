using SocialNetwork;
using System;

namespace TermProjectLogin
{
    public partial class Common : System.Web.UI.MasterPage
    {
        private const string UserEmailCookie = "UserEmail";
        private const string UserLoggedInCookie = "UserIsLoggedIn";
        private const string UserPasswordCookie = "UserPassword";

        EncryptionManager encryptionManager = new EncryptionManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnLogin.Visible = true;
                pnLoggedIn.Visible = false;

                bool isLoggedIn = false;

                if(Response.Cookies[UserLoggedInCookie] != null)
                {
                    isLoggedIn = Convert.ToBoolean(Response.Cookies[UserLoggedInCookie].Value);
                }

                if (!isLoggedIn)
                {
                    if (Response.Cookies[UserEmailCookie] != null)
                    {
                        txtEmail.Text = Response.Cookies[UserEmailCookie].Value;
                    }

                    if (Response.Cookies[UserPasswordCookie] != null)
                    {
                        //txtPassword.Text = encryptionManager.Decrypt(Response.Cookies[UserPasswordCookie].Value);
                    }

                    if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                    {
                        HandleLogin(txtEmail.Text, txtPassword.Text, LoginSettingType.FastLogin);
                    }

                    return;
                }

                pnLogin.Visible = false;
                pnLoggedIn.Visible = true;

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            HandleLogin(email, password, LoginSettingType.None);
        }

        private void HandleLogin(string email, string password, LoginSettingType loginSetting)
        {
            switch (loginSetting)
            {
                case LoginSettingType.AutoLogin:
                    Response.Cookies[UserEmailCookie].Value = email;
                    Response.Cookies[UserPasswordCookie].Value = encryptionManager.Encrypt(password);
                    break;
                case LoginSettingType.FastLogin:
                    Response.Cookies[UserEmailCookie].Value = email;
                    Response.Cookies[UserPasswordCookie].Value = null;
                    break;
                case LoginSettingType.None:
                    Response.Cookies[UserEmailCookie].Value = null;
                    Response.Cookies[UserPasswordCookie].Value = null;
                    break;
            }

            Response.Cookies["IsLoggedIn"].Value = "true";
        }
    }
}