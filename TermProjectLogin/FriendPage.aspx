<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="FriendPage.aspx.cs" Inherits="TermProjectLogin.FriendPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Panel ID="pnFriend" runat="server">
        <asp:Label ID="lblAllFriends" runat="server" Text="All Friends"></asp:Label>
        <asp:Repeater ID="rptFriend" runat="server">
            <HeaderTemplate>
                <div class="container">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                        <asp:ImageButton ID="imgFriend" runat="server" ImageUrl='<%# Eval("ProfilePhotoURL") %>' OnClick="imgFriend_Click" CommandArgument='<%# Eval("Email") %>' Width="100px" Height="130px" />
                    </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>