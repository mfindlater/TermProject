<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="ForgotPasswordPage.aspx.cs" Inherits="TermProjectLogin.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
<asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
<asp:Button ID="btnCheckEmail" runat="server" OnClick="btnCheckEmail_Click" Text="Check Email" />
<asp:Label ID="lblAnswer" runat="server" Visible="false"></asp:Label>
<br />
<asp:Panel ID="pnQuestion" runat="server" Visible="false">
    <asp:Label ID="lblQuestion" runat="server" Text=""></asp:Label>
    <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit Answer" OnClick="btnSubmit_Click"/>
</asp:Panel>
<asp:Label ID="lblMessage" runat="server"></asp:Label>
</asp:Content>
