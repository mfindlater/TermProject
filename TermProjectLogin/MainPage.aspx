<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="TermProjectLogin.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Image ID="imgProfilePhoto" ImageUrl="~/img/person_temp.jpg" runat="server" Width="80px" Height="110px"/>
    <asp:Label ID="lblName" runat="server"></asp:Label>
    <asp:Button ID="btnAddFriend" runat="server" Text="Add Friend" Visible="false"/>
    <asp:Button ID="btnFollow" runat="server" Text="Follow" Visible="false" OnClick="btnFollow_Click" />
    <asp:Button ID="btnChat" runat="server" Visible="false" Text="Chat" /><br />
    <br />
    <asp:Panel ID="pnCreatePost" runat="server">
        <asp:LinkButton ID="lbtnPost" runat="server" Text="Post" OnClick="lbtnPost_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtnPhoto" runat="server" Text="Photo/Album" OnClick="lbtnPhoto_Click"></asp:LinkButton>
        <br />
        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox><br />
        <asp:Panel ID="pnUploadPhoto" runat="server">
            <br />
            <asp:FileUpload ID="photoUpload" runat="server" /><br />
            <asp:Label ID="lblAlbumName" runat="server" Text="Album Name:"></asp:Label>
            <asp:TextBox ID="txtAlbumName" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox><br />
            <br />
        </asp:Panel>
        <asp:Button ID="btnPost" runat="server" Text="Post" OnClick="btnPost_Click" />
    </asp:Panel>
    <asp:Panel ID="pnPhotos" runat="server">
        <asp:LinkButton ID="lbtnPhotos" runat="server" Text="Photos" OnClick="lbtnPhotos_Click"></asp:LinkButton>
        <br />
        <asp:ImageButton ID="imgBtnPhoto1" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <asp:ImageButton ID="imgBtnPhoto2" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <asp:ImageButton ID="imgBtnPhoto3" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <br />
        <asp:ImageButton ID="imgBtnPhoto4" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <asp:ImageButton ID="imgBtnPhoto5" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <asp:ImageButton ID="imgBtnPhoto6" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
    </asp:Panel>
    <asp:Panel ID="pnFriends" runat="server">
        <asp:LinkButton ID="lbtnFriends" runat="server" Text="Friends" OnClick="lbtnFriends_Click"></asp:LinkButton>
        <br />
        <asp:ImageButton ID="imgBtnFriend1" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <asp:ImageButton ID="imgBtnFriend2" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <asp:ImageButton ID="imgBtnFriend3" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <br />
        <asp:ImageButton ID="imgBtnFriend4" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <asp:ImageButton ID="imgBtnFriend5" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
        <asp:ImageButton ID="imgBtnFriend6" runat="server" ImageUrl="~/img/person_temp.jpg" Width="100px" Height="130px"/>
    </asp:Panel>
    <asp:Panel ID="pnWall" runat="server">


    </asp:Panel>
</asp:Content>
