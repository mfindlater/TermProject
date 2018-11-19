﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="TermProjectLogin.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <h1>Create a New Account</h1>
    <asp:Panel ID="pnlNewAccount" runat="server">
        <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblBirthday" runat="server" Text="Birthday:"></asp:Label>
        <asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="ddlDay" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList><br />
        <br />
        <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" />
    </asp:Panel>
</asp:Content>
