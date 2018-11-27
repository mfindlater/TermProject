using SocialNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProjectLogin
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        private SqlRepository sqlRepository = new SqlRepository();
        private SocialNetworkManager socialNetworkManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            socialNetworkManager = new SocialNetworkManager(sqlRepository);
        }

        protected void btnCheckEmail_Click(object sender, EventArgs e)
        {
            var user = socialNetworkManager.GetUser(txtEmail.Text);
  
            if(user == null)
            {
                lblMessage.Text = "User not found.";
                return;
            }
        }
    }
}