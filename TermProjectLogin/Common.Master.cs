using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProjectLogin
{
    public partial class Common : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnLogin.Visible = true;
            pnLoggedIn.Visible = false;
            var userCookie = Response.Cookies.Get("User");

            if(userCookie != null)
            {
                bool isLoggedIn = Convert.ToBoolean(userCookie.Values["IsLoggedIn"]);

                if (isLoggedIn)
                {
                    pnLogin.Visible = false;
                    pnLoggedIn.Visible = true;
                }
            } 
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            Login(email, password);
        }

        private void Login(string email, string password)
        {
            HttpCookie cookie;

            if (Response.Cookies["User"] != null)
            {
                cookie = Response.Cookies["User"];
            }
            else
            {
                cookie = new HttpCookie("User");

            }

            cookie.Values["IsLoggedIn"] = "true";
            cookie.Values["Email"] = email;
            cookie.Values["Password"] = "";
        }
    }
}