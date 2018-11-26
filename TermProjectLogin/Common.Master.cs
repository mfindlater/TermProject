﻿using SocialNetwork;
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

                    isLoggedIn = Convert.ToBoolean(userCookie.Values[Constants.UserLoggedInCookie]);

                    if (!isLoggedIn)
                    {
                        string email = "";
                        string password = "";

                        if (userCookie.Values[Constants.UserEmailCookie] != null)
                        {
                            email = userCookie.Values[Constants.UserEmailCookie];
                        }

                        if (userCookie.Values[Constants.UserPasswordCookie] != null)
                        {
                            password = EncryptionManager.Decode(userCookie.Values[Constants.UserPasswordCookie]);
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
            HttpCookie userCookie = new HttpCookie(Constants.UserCookie);
        
            if(Request.Cookies[Constants.UserCookie] != null)
            {
                userCookie = Request.Cookies[Constants.UserCookie];
            }

            switch (loginSetting)
            {
                case LoginSettingType.AutoLogin:
                    userCookie.Values[Constants.UserEmailCookie] = email;
                    userCookie.Values[Constants.UserPasswordCookie] = EncryptionManager.Encode(password);
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

            var user = socialNetworkManager.Login(email, password);
            if(user != null)
            {
                Session[Constants.UserSession] = user;
                userCookie.Values[Constants.UserLoggedInCookie] = "true";
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
                Session.Remove(Constants.UserSession);
            }

            if (Request.Cookies[Constants.UserCookie] != null)
            {
                var cookie = Response.Cookies[Constants.UserCookie];
                cookie.Values[Constants.UserLoggedInCookie] = "false";
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            Response.Redirect("RegistrationPage.aspx");
        }
    }
}