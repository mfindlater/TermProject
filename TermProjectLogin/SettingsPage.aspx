<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="SettingsPage.aspx.cs" Inherits="TermProjectLogin.WebForm2" %>
<%@ Register src="ColorPicker.ascx" tagname="ColorPicker" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <h1>Account Setting</h1>
    <asp:Panel ID="pnlMoreInfo" runat="server">
        <asp:FileUpload ID="profilePhotoUpload" runat="server" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
        <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
        <br />
        <asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblAddressLine1" runat="server" Text="Address Line 1:"></asp:Label>
        <asp:TextBox ID="txtAddressLine1" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblAddressLine2" runat="server" Text="Address Line 2:"></asp:Label>
        <asp:TextBox ID="txtAddressLine2" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblState" runat="server" Text="State:"></asp:Label>
        <asp:TextBox ID="txtState" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code:"></asp:Label>
        <asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox><br />
        <asp:Panel ID="pnProfile" runat="server">
            <asp:Label ID="lblOrganization" runat="server" Text="Organization:"></asp:Label>
            <br />
            <asp:TextBox ID="txtOrganization" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblLikes" runat="server" Text="Likes:"></asp:Label>
            <br />
            <asp:TextBox ID="txtLikes" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblDislike" runat="server" Text="Dislikes:"></asp:Label>
            <br />
            <asp:TextBox ID="txtDislikes" runat="server" TextMode="MultiLine"></asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="pnTheme" runat="server">
            <asp:Label ID="lblTheme" runat="server" Text="Theme"></asp:Label>
            <br />
            <asp:Label ID="lblPreset" runat="server" Text="Presets:"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlThemePresets" runat="server">
            </asp:DropDownList>
            <br />
            <asp:Button ID="btnApplyPreset" runat="server" OnClick="btnApplyPreset_Click" Text="Apply Preset" />
            <br />
            <br />
            <asp:Label ID="lblFontColor" runat="server" Text="Font Color:"></asp:Label>
            <uc1:ColorPicker ID="cpFontColor" runat="server" />
            <br />
            <asp:Label ID="lblFontWeight" runat="server" Text="Font Weight:"></asp:Label>
            <asp:DropDownList ID="ddlFontWeight" runat="server">
                <asp:ListItem>Normal</asp:ListItem>
                <asp:ListItem>Bold</asp:ListItem>
                <asp:ListItem>Italics</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblFontSize" runat="server" Text="Font Size:"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtFontSize" runat="server" TextMode="Number" Width="70px"></asp:TextBox>
            <br />
            <asp:Label ID="lblBackgroundColor" runat="server" Text="Background Color:"></asp:Label>
            <uc1:ColorPicker ID="cpBackgroundColor" runat="server" />
            <br />
        </asp:Panel>
        <br />
        <asp:CheckBox ID="chkReceiveEmailNotifications" runat="server" />
        <asp:Label ID="lblReceiveEmailNotifications" runat="server" Text="Receive Email Notifications"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblSecurityQuestion" runat="server" Text="Security Question"></asp:Label><br />
        <asp:DropDownList ID="ddlSecurityQuestion1" runat="server"></asp:DropDownList><br />
        <asp:Label ID="lblAnswer1" runat="server" Text="Answer:"></asp:Label>
        <asp:TextBox ID="txtSecurityQuestion1" runat="server"></asp:TextBox><br />
        <asp:DropDownList ID="ddlSecurityQuestion2" runat="server"></asp:DropDownList>&nbsp;<br />
        <asp:Label ID="lblAnswer2" runat="server" Text="Answer:"></asp:Label>
        <asp:TextBox ID="txtSecurityQuestion2" runat="server"></asp:TextBox><br />
        <asp:DropDownList ID="ddlSecurityQuestion3" runat="server"></asp:DropDownList><br />
        <asp:Label ID="lblAnswer3" runat="server" Text="Answer:"></asp:Label>
        <asp:TextBox ID="txtSecurityQuestion3" runat="server"></asp:TextBox><br />
        <asp:Panel ID="pnPrivacy" runat="server">
            <asp:Label ID="lblPhotoPrivacy" runat="server" Text="Photo Privacy"></asp:Label>
            <asp:DropDownList ID="ddlPhotoPrivacy" runat="server">
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblProfilePrivacy" runat="server" Text="Profile Privacy"></asp:Label>
            <asp:DropDownList ID="ddlProfilePrivacy" runat="server">
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblContact" runat="server" Text="Contact Privacy"></asp:Label>
            <asp:DropDownList ID="ddlContactPrivacy" runat="server">
            </asp:DropDownList>
        </asp:Panel>
        <br />
        <asp:Label ID="lblLoginSetting" runat="server" Text="Login Setting"></asp:Label>
        <asp:DropDownList ID="ddlLoginSetting" runat="server"></asp:DropDownList><br />
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </asp:Panel>
</asp:Content>