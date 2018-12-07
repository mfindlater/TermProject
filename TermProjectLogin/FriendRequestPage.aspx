<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="FriendRequestPage.aspx.cs" Inherits="TermProjectLogin.FriendRequestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <br />
    <br />
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
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnAccept" runat="server" Text="Accept" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnAccept_Click" />
                    <asp:Button ID="btnDecline" runat="server" Text="Decline" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnDecline_Click"  />
                </ItemTemplate>
            </asp:TemplateField>
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
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel Request" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnCancel_Click"  />
                    </ItemTemplate>
                </asp:TemplateField>
         </Columns>  
    </asp:GridView>
</asp:Content>
