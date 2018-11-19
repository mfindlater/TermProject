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
            /*
            var user = Response.Cookies.Get("User");

            bool isLoggedIn = Convert.ToBoolean(user.Values["IsLoggedIn"]);

            if(isLoggedIn)
            {
                pnLogin.Visible = false;
                pnLoggedIn.Visible = true;
            }
            else
            {
                pnLogin.Visible = true;
                pnLoggedIn.Visible = false;
            } 
            */
        }
    }
}