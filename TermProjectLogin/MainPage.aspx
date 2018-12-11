<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="TermProjectLogin.MainPage" EnableEventValidation="false" ValidateRequest="false" %>


<%@ Register Src="~/ChatControl.ascx" TagPrefix="uc1" TagName="ChatControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Image ID="imgProfilePhoto" CssClass="rounded-circle" ImageUrl="~/img/person_temp.jpg" runat="server" Width="80px" Height="104px" />
    <asp:Label ID="lblName" CssClass="boldName" runat="server"></asp:Label>
    <asp:Button ID="btnAddFriend" CssClass="btn btn-info" runat="server" Text="Add Friend" Visible="false" OnClick="btnAddFriend_Click" />
    <asp:Button ID="btnFollow" CssClass="btn btn-info" runat="server" Text="Follow" Visible="false" OnClick="btnFollow_Click" />
    <asp:Button ID="btnChat" CssClass="btn btn-info" runat="server" Visible="false" Text="Chat" OnClick="btnChat_Click" /><br />
    <br />
    <div>
        <asp:Panel ID="pnContactInfo" runat="server">
            <asp:Label ID="lblContactInfo" runat="server" Text="ContactInfo"></asp:Label><br />
            <asp:Label ID="lblBirthdate" runat="server" Text="Birthdate: "></asp:Label><br />
            <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label><br />
            <asp:Label ID="lblPhone" runat="server" Text="Phone: "></asp:Label><br />
            <asp:Label ID="lblAddress" runat="server" Text="Address: "></asp:Label><br />
            <asp:Label ID="lblOrganization" runat="server" Text="Organization: "></asp:Label><br />
        </asp:Panel>
        <asp:Panel ID="pnLikesDislikes" runat="server">
            <asp:Label ID="lblLikes" runat="server" Text="Likes: "></asp:Label><br />
            <asp:Label ID="lblDislikes" runat="server" Text="Dislikes: "></asp:Label>
        </asp:Panel>
    </div>
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
        <asp:Button ID="btnPost" CssClass="btn btn-info" runat="server" Text="Post" OnClick="btnPost_Click" />
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </asp:Panel>
    <div id="divPnPhotos">
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
                        <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("URL") %>' Width="100px" Height="130px" />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </div>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
    <div id="divPnFriends">
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
                        <asp:ImageButton ID="imgFriend" runat="server" ImageUrl='<%# Eval("ProfilePhotoURL") %>' OnClick="imgFriend_Click" CommandArgument='<%# Eval("Email") %>' Width="100px" Height="130px" />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </div>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnWallNewsFeed" runat="server">
        <asp:LinkButton ID="lbtnWall" runat="server" Text="Wall" OnClick="lbtnWall_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtnNewsFeed" runat="server" Text="News Feed" OnClick="lbtnNewsFeed_Click"></asp:LinkButton>
        <asp:Repeater ID="rptWall" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div>
                        <asp:Image ID="imgProfilePhoto" runat="server" ImageUrl='<%# Eval("Poster.ProfilePhotoURL") %>' Width="50px" Height="70px" />
                        <asp:LinkButton ID="lbtnName" runat="server" Text='<%# Eval("Poster.Name") %>' CommandArgument='<%# Eval("Poster.Email") %>' OnClick="lbtnName_Click"></asp:LinkButton>
                        <%# DataBinder.Eval(Container.DataItem, "Content") %><br />
                        <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Photo.URL") %>' Visible='<%# Eval("HasPhoto") %>' Width="250px" Height="324px" />
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <uc1:ChatControl runat="server" ID="ChatControl" Visible="false" />
</asp:Content>
