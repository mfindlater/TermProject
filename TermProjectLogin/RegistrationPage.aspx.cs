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
        int year;
        int month;

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

            registerInfo.BirthDate.Month = DateTime.Parse(month.ToString());


            registerInfo.ContactInfo = contactInfo;
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            year = Convert.ToInt32(ddlYear.SelectedValue);
            month = Convert.ToInt32(ddlMonth.SelectedValue);

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