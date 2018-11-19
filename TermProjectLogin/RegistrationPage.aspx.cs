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
        ContactInfo contactInfo = new ContactInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDate();
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            txtName.Text = registerInfo.Name;
            txtEmail.Text = contactInfo.Email;
            txtPassword.Text = registerInfo.Password;

            string month = ddlMonth.SelectedValue;
            string day = ddlDay.SelectedValue;
            string year = ddlYear.SelectedValue;
            registerInfo.BirthDate = DateTime.Parse(month + "/" + day + "/" + year);

            registerInfo.ContactInfo = contactInfo;
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);

            int days = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= days; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
        }

        private void SetDate()
        {
            int MONTH = 12;
            int currentYear = DateTime.Now.Year;

            for (int i = 1; i <= MONTH; i++)
            {
                ddlMonth.Items.Add(i.ToString());
            }

            for (int i = currentYear; i >= currentYear - 50; i--)
            {
                ddlYear.Items.Add(i.ToString());
            }
        }
    }
}