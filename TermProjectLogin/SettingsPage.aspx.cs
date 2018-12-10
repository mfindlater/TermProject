using SocialNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace TermProjectLogin
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private SqlRepository sqlRepository = new SqlRepository();
        private SocialNetworkManager socialNetworkManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            socialNetworkManager = new SocialNetworkManager(sqlRepository);

            if(!IsPostBack && Session[Constants.UserSession] != null)
            {
                var user = (User)Session[Constants.UserSession];

                txtAddressLine1.Text = user.Address.AddressLine1;
                txtAddressLine2.Text = user.Address.AddressLine2;
                txtCity.Text = user.Address.City;
                txtPostalCode.Text = user.Address.PostalCode;
                txtState.Text = user.Address.State;
                txtPhone.Text = user.ContactInfo.Phone;
                txtOrganization.Text = user.Organization;
                txtLikes.Text = user.Likes;
                txtDislikes.Text = user.Dislikes;
                chkReceiveEmailNotifications.Checked = user.Settings.ReceiveEmailNotifications;
                
                var privacyNames = Enum.GetNames(typeof(PrivacySettingType));
                var privacyValues = Enum.GetValues(typeof(PrivacySettingType));
                var loginSetting = Enum.GetNames(typeof(LoginSettingType));

                for(int i=0; i < privacyNames.Length; i++)
                {
                    ddlContactPrivacy.Items.Add(new ListItem(privacyNames[i], privacyValues.GetValue(i).ToString()));
                    ddlPhotoPrivacy.Items.Add(new ListItem(privacyNames[i], privacyValues.GetValue(i).ToString()));
                    ddlProfilePrivacy.Items.Add(new ListItem(privacyNames[i], privacyValues.GetValue(i).ToString()));
                }
                for(int i=0; i< loginSetting.Length; i++)
                {
                    ddlLoginSetting.Items.Add(new ListItem(loginSetting[i], loginSetting[i]));
                }

                ddlContactPrivacy.SelectedValue = user.Settings.ContactInfoPrivacySetting.ToString();
                ddlPhotoPrivacy.SelectedValue = user.Settings.PhotoPrivacySetting.ToString();
                ddlProfilePrivacy.SelectedValue = user.Settings.UserInfoSetting.ToString();
                ddlLoginSetting.SelectedValue = user.Settings.LoginSetting.ToString();
                cpBackgroundColor.Color = user.Settings.Theme.BackgroundColor;
                cpFontColor.Color = user.Settings.Theme.FontColor;
                ddlFontWeight.SelectedValue = user.Settings.Theme.FontWeight;
                txtFontSize.Text = user.Settings.Theme.FontSize.ToString();

                ddlSecurityQuestion1.DataSource = Constants.SecurityQuestion1;
                ddlSecurityQuestion2.DataSource = Constants.SecurityQuestion2;
                ddlSecurityQuestion3.DataSource = Constants.SecurityQuestion3;
                ddlSecurityQuestion1.DataBind();
                ddlSecurityQuestion2.DataBind();
                ddlSecurityQuestion3.DataBind();

                if(user.Settings.SecurityQuestions.Count == 3)
                {
                    ddlSecurityQuestion1.SelectedValue = user.Settings.SecurityQuestions[0].Question;
                    ddlSecurityQuestion2.SelectedValue = user.Settings.SecurityQuestions[1].Question;
                    ddlSecurityQuestion3.SelectedValue = user.Settings.SecurityQuestions[2].Question;

                    txtSecurityQuestion1.Text = user.Settings.SecurityQuestions[0].Answer;
                    txtSecurityQuestion2.Text = user.Settings.SecurityQuestions[1].Answer;
                    txtSecurityQuestion3.Text = user.Settings.SecurityQuestions[2].Answer;
                }
            }
        }


        private void UpdateLikesDislikes(string email)
        {
            string[] likes = txtLikes.Text.Split(',').Select(s => s.Trim()).ToArray();
            string[] dislikes = txtDislikes.Text.Split(',').Select(s => s.Trim()).ToArray();
            socialNetworkManager.UpdateLikesDislikes(email, likes, dislikes);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session[Constants.UserSession] != null)
            {
                var user = (User)Session[Constants.UserSession];

                user.Address.AddressLine1 = txtAddressLine1.Text;
                user.Address.AddressLine2 = txtAddressLine2.Text;
                user.Address.City = txtCity.Text;
                user.Address.PostalCode = txtPostalCode.Text;
                user.Address.State = txtState.Text;
                user.ContactInfo.Phone = txtPhone.Text;
                user.Organization = txtOrganization.Text;

                var sq1 = new SecurityQuestion()
                {
                    Question = ddlSecurityQuestion1.SelectedValue,
                    Answer = txtSecurityQuestion1.Text
                };

                var sq2 = new SecurityQuestion()
                {
                    Question = ddlSecurityQuestion2.SelectedValue,
                    Answer = txtSecurityQuestion2.Text
                };

                var sq3 = new SecurityQuestion()
                {
                    Question = ddlSecurityQuestion3.SelectedValue,
                    Answer = txtSecurityQuestion3.Text
                };

                var securityQuestions = new List<SecurityQuestion>() { sq1, sq2, sq3 };

                user.Settings.SecurityQuestions = securityQuestions;

                user.Settings.ContactInfoPrivacySetting = (PrivacySettingType)Enum.Parse(typeof(PrivacySettingType),ddlContactPrivacy.SelectedValue);
                user.Settings.PhotoPrivacySetting = (PrivacySettingType)Enum.Parse(typeof(PrivacySettingType), ddlPhotoPrivacy.SelectedValue);
                user.Settings.UserInfoSetting = (PrivacySettingType)Enum.Parse(typeof(PrivacySettingType), ddlProfilePrivacy.SelectedValue);
                user.Settings.LoginSetting = (LoginSettingType)Enum.Parse(typeof(LoginSettingType), ddlLoginSetting.SelectedValue);
                user.Settings.ReceiveEmailNotifications = chkReceiveEmailNotifications.Checked;
                ddlLoginSetting.SelectedValue = user.Settings.LoginSetting.ToString();
                user.Settings.Theme.BackgroundColor = cpBackgroundColor.Color;
                user.Settings.Theme.FontColor = cpFontColor.Color;
                user.Settings.Theme.FontWeight = ddlFontWeight.SelectedValue;
                user.Settings.Theme.FontSize = Convert.ToInt32(txtFontSize.Text); 


                HttpCookie userCookie = new HttpCookie(Constants.UserCookie);

                if (Request.Cookies[Constants.UserCookie] != null)
                {
                    userCookie = Request.Cookies[Constants.UserCookie];
                }

                switch (user.Settings.LoginSetting)
                {
                    case LoginSettingType.AutoLogin:
                        userCookie.Values[Constants.UserEmailCookie] = user.ContactInfo.Email;
                        break;
                    case LoginSettingType.FastLogin:
                        userCookie.Values[Constants.UserEmailCookie] = user.ContactInfo.Email;
                        userCookie.Values[Constants.UserPasswordCookie] = null;
                        break;
                    case LoginSettingType.None:
                        userCookie.Values[Constants.UserEmailCookie] = null;
                        userCookie.Values[Constants.UserPasswordCookie] = null;
                        break;
                }
                Response.Cookies.Add(userCookie);

                bool result = socialNetworkManager.UpdateUser(user);
                UpdateLikesDislikes(user.ContactInfo.Email);

                if(result)
                {
                    Session.SetUser(user);
                    lblMessage.Text = "User Settings Saved!";
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var user = Session.GetUser();
            string filename = "";

            if (profilePhotoUpload.HasFile)
            {
                filename = user.UserId + "_" + Path.GetFileName(profilePhotoUpload.PostedFile.FileName);
                profilePhotoUpload.SaveAs(Constants.StoragePath + filename);
                lblMsg.Text = "File Uploaded Successfully";

                Photo photo = new Photo();
                photo.URL = Constants.StorageURL + filename;
                photo.Description = "Profile Photo";

                photo = socialNetworkManager.AddPhoto(photo, user.ContactInfo.Email);
                user.ProfilePhotoID = photo.PhotoID;
                Session.SetUser(user);
            }
            else
            {
                lblMsg.Text = "Please select a file";
            }
        }

    }
}