<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="TermProjectLogin.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Image ID="imgProfilePhoto" ImageUrl="~/img/person_temp.jpg" runat="server" Width="80px" Height="110px" />
    <asp:Label ID="lblName" runat="server"></asp:Label>
    <asp:Button ID="btnAddFriend" runat="server" Text="Add Friend" Visible="false" />
    <asp:Button ID="btnFollow" runat="server" Text="Follow" Visible="false" OnClick="btnFollow_Click" />
    <asp:Button ID="btnChat" runat="server" Visible="false" Text="Chat" /><br />
    <br />
    <asp:Panel ID="pnCreatePost" runat="server">
        <asp:LinkButton ID="lbtnPost" runat="server" Text="Post" OnClick="lbtnPost_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtnPhoto" runat="server" Text="Photo" OnClick="lbtnPhoto_Click"></asp:LinkButton>
        <br />
        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox><br />
        <asp:Panel ID="pnUploadPhoto" runat="server">
            <br />
            <asp:FileUpload ID="photoUpload" runat="server" /><br />
            <asp:Label ID="lblPhotoDescription" runat="server" Text="Photo Description:"></asp:Label>
            <br />
            <asp:TextBox ID="txtPhotoDescription" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox><br />
        </asp:Panel>
        <asp:Button ID="btnPost" runat="server" Text="Post" OnClick="btnPost_Click" />
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnWall" runat="server">
        <asp:Repeater ID="rptWall" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div>
                        <%# DataBinder.Eval(Container.DataItem, "Content") %><br />
                        <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Photo.URL") %>' Visible='<%# Eval("HasPhoto") %>' Width="300px" Height="200px"/>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="pnPhotos" runat="server">
        <asp:LinkButton ID="lbtnPhotos" runat="server" Text="Photos" OnClick="lbtnPhotos_Click"></asp:LinkButton>
        <br />
        <asp:Repeater ID="rptPhoto" runat="server">
            <HeaderTemplate>
                <div class="container">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                            <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("URL") %>' Width="100px" Height="130px"/>
                        </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="pnFriends" runat="server">
        <asp:LinkButton ID="lbtnFriends" runat="server" Text="Friends" OnClick="lbtnFriends_Click"></asp:LinkButton>
        <br />
        <asp:Repeater ID="rptFriend" runat="server">
            <HeaderTemplate>
                <div class="container">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                            <asp:ImageButton ID="imgFriend" runat="server" ImageUrl='<%# Eval("ProfilePhotoURL") %>' OnClick="imgFriend_Click" CommandArgument='<%# Eval("Email") %>' Width="100px" Height="130px"/>
                        </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>
