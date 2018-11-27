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
        private RegisterInfo registerInfo = new RegisterInfo();
        private ContactInfo contactInfo = new ContactInfo();
        private SqlRepository sqlRepository = new SqlRepository();
        private SocialNetworkManager socialNetworkManager;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDate();
                ddlSecurityQuestion1.DataSource = Constants.SecurityQuestion1;
                ddlSecurityQuestion2.DataSource = Constants.SecurityQuestion2;
                ddlSecurityQuestion3.DataSource = Constants.SecurityQuestion3;
                ddlSecurityQuestion1.DataBind();
                ddlSecurityQuestion2.DataBind();
                ddlSecurityQuestion3.DataBind();

                if (Session[Constants.UserSession] != null)
                {
                    Response.Redirect("MainPage.aspx");
                    return;
                }
            }
            socialNetworkManager = new SocialNetworkManager(sqlRepository);
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblMessage.Text = "Please enter Name!";
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblMessage.Text = "Please enter Email!";
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMessage.Text = "Please enter Password!";
                return;
            }
            if (string.IsNullOrEmpty(txtSecurityQuestion1.Text))
            {
                lblMessage.Text = "Please anser all Security Questions!";
                return;
            }
            if (string.IsNullOrEmpty(txtSecurityQuestion2.Text))
            {
                lblMessage.Text = "Please anser all Security Questions!";
                return;
            }
            if (string.IsNullOrEmpty(txtSecurityQuestion3.Text))
            {
                lblMessage.Text = "Please anser all Security Questions!";
                return;
            }

            SecurityQuestion securityQuestion1 = new SecurityQuestion();
            SecurityQuestion securityQuestion2 = new SecurityQuestion();
            SecurityQuestion securityQuestion3 = new SecurityQuestion();

            List<SecurityQuestion> securityQuestions = new List<SecurityQuestion>();
            securityQuestion1.Question = ddlSecurityQuestion1.SelectedItem.Text;
            
            securityQuestion1.Answer = txtSecurityQuestion1.Text;
            securityQuestion2.Question = ddlSecurityQuestion2.SelectedItem.Text;
            securityQuestion2.Answer = txtSecurityQuestion2.Text;
            securityQuestion3.Question = ddlSecurityQuestion3.SelectedItem.Text;
            securityQuestion3.Answer = txtSecurityQuestion3.Text;

            registerInfo.Name = txtName.Text;
            contactInfo.Email = txtEmail.Text;
            registerInfo.Password = txtPassword.Text;

            securityQuestions.Add(securityQuestion1);
            securityQuestions.Add(securityQuestion2);
            securityQuestions.Add(securityQuestion3);
            registerInfo.SecurityQuestions = securityQuestions;

            string month = ddlMonth.SelectedValue;
            string day = ddlDay.SelectedValue;
            string year = ddlYear.SelectedValue;
            registerInfo.BirthDate = DateTime.Parse(month + "/" + day + "/" + year);

            registerInfo.ContactInfo = contactInfo;

            bool result = socialNetworkManager.RegisterNewUser(registerInfo);
            if (result)
            {
                lblMessage.Text = "Successfully registered!";
            }
            else
            {
                lblMessage.Text = "The email is already an existing account!";
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);

            ddlDay.Items.Clear();
            int days = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= days; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);

            ddlDay.Items.Clear();
            int days = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= days; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
        }

        private void SetDate()
        {
            int MAX_MONTH = 12;
            int MAX_DAY = 31;
            int PAST_YEAR = 50;
            int currentYear = DateTime.Now.Year;

            for (int i = 1; i <= MAX_MONTH; i++)
            {
                ddlMonth.Items.Add(i.ToString());
            }

            for (int i = 1; i <= MAX_DAY; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }

            for (int i = currentYear; i >= currentYear - PAST_YEAR; i--)
            {
                ddlYear.Items.Add(i.ToString());
            }
        }
    }
}