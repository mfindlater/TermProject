using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SocialNetwork;

namespace TermProjectLogin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        RegisterInfo registerInfo = new RegisterInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            txtName.Text = registerInfo.Name;
            txtEmail.Text = 
            txtPassword.Text = registerInfo.Password;

        }
    }
}