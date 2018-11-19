﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="SettingsPage.aspx.cs" Inherits="TermProjectLogin.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <h1>Account Setting</h1>
    <asp:Panel ID="pnlMoreInfo" runat="server">
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
        <br />
        <asp:Label ID="lblSecurityQuestion" runat="server" Text="Security Question"></asp:Label><br />
        <asp:DropDownList ID="ddlSecurityQuestion1" runat="server"></asp:DropDownList><br />
        <asp:Label ID="lblAnswer1" runat="server" Text="Answer:"></asp:Label>
        <asp:TextBox ID="txtSecurityQuestion1" runat="server"></asp:TextBox><br />
        <asp:DropDownList ID="ddlSecurityQuestion2" runat="server"></asp:DropDownList><br />
        <asp:Label ID="lblAnswer2" runat="server" Text="Answer:"></asp:Label>
        <asp:TextBox ID="txtSecurityQuestion2" runat="server"></asp:TextBox><br />
        <asp:DropDownList ID="ddlSecurityQuestion3" runat="server"></asp:DropDownList><br />
        <asp:Label ID="lblAnswer3" runat="server" Text="Answer:"></asp:Label>
        <asp:TextBox ID="txtSecurityQuestion3" runat="server"></asp:TextBox><br />
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save" />
    </asp:Panel>
</asp:Content>