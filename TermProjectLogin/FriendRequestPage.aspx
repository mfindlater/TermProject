<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="FriendRequestPage.aspx.cs" Inherits="TermProjectLogin.FriendRequestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:label id="lblIncomingFriendRequests" runat="server" text="Incoming Friend Requests"></asp:label>
    <asp:GridView ID="gvIncomingFriendRequests" runat="server" AutogenerateColumns="False">
        <Columns>
           <asp:TemplateField>
               <ItemTemplate>
                   <asp:Image ID="imgProfile" runat="server" ImageUrl='<%# Bind("ProfilePhotoURL") %>' Width="70px" Height="100px"/>
               </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="FriendRequestStatus" HeaderText="Status" />
         </Columns>     
    </asp:GridView>
    <br />
    <br />
    <asp:label id="lblOutgoingFriendRequests" runat="server" text="Outgoing Friend Requests"></asp:label>
    <asp:GridView ID="gvOutgoingFriendRequests" runat="server" AutoGenerateColumns="False">
            <Columns>
           <asp:TemplateField>
               <ItemTemplate>
                   <asp:Image ID="imgProfile" runat="server" ImageUrl='<%# Bind("ProfilePhotoURL") %>' Width="70px" Height="100px"/>
               </ItemTemplate>
            </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="FriendRequestStatus" HeaderText="Status" />
         </Columns>  
    </asp:GridView>
</asp:Content>
