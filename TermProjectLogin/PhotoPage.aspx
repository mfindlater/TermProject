<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="PhotoPage.aspx.cs" Inherits="TermProjectLogin.PhotoPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Panel ID="pnPhoto" runat="server">
        <asp:Label ID="lblAllPhotos" runat="server" Text="All Photos"></asp:Label>
        <asp:Repeater ID="rptPhoto" runat="server">
            <HeaderTemplate>
                <div class="container">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                        <asp:Label ID="lblPhotoID" runat="server" Text='<%# Eval("PhotoID") %>' Visible="false"></asp:Label>
                        <asp:CheckBox ID="chkPhoto" runat="server"/>
                        <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("URL") %>' Width="100px" Height="130px" />
                    </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="pnCreateAlbum" runat="server">
        <asp:FileUpload ID="photoUpload" runat="server" /><br />
        <asp:Label ID="lblPhotoDescription" runat="server" Text="Photo Description:"></asp:Label><br />
        <asp:TextBox ID="txtPhotoDescription" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox><br />
        <asp:Label ID="lblAlbumName" runat="server" Text="Album Name:"></asp:Label>
        <asp:TextBox ID="txtAlbumName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
        <br />
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnAlbum" runat="server">
        <asp:Label ID="lblAlbum" runat="server" Text="All Albums"></asp:Label>
        <asp:Repeater ID="rptAlbum" runat="server">
            <HeaderTemplate>
                <div class="container">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                        <asp:ImageButton ID="imgBtnAlbum" runat="server" ImageUrl='<%# Eval("Photos[0].URL") %>' OnClick="imgBtnAlbum_Click" CommandArgument='<%# Eval("PhotoAlbumID") %>' Width="100px" Height="130px" />
                    </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="pnAlbumPhoto" runat="server" Visible="false">
        <asp:Label ID="lblAlbumPhotos" runat="server" Text="Album Photos"></asp:Label>
        <asp:Repeater ID="rptAlbumPhoto" runat="server">
            <HeaderTemplate>
                <div class="container">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                        <asp:Image ID="imgAlbumPhoto" runat="server" ImageUrl='<%# Eval("URL") %>' Width="100px" Height="130px" />
                    </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>
