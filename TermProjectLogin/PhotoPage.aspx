<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="PhotoPage.aspx.cs" Inherits="TermProjectLogin.PhotoPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:Panel ID="pnPhoto" runat="server">
        <asp:Repeater ID="rptPhoto" runat="server">
            <HeaderTemplate>
                <div class="container">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                        <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="130px" />
                    </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="pnCreateAlbum" runat="server">
        <asp:Label ID="lblAlbumName" runat="server" Text="Album Name:"></asp:Label>
        <asp:TextBox ID="txtAlbumName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
        <br />
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Button ID="btnCreate" runat="server" Text="Create" />
    </asp:Panel>
    <asp:LinkButton ID="lbtnAlbum" runat="server" Text="Album" OnClick="lbtnAlbum_Click"></asp:LinkButton>
    <asp:LinkButton ID="lbtnPhoto" runat="server" Text="Photo" OnClick="lbtnPhoto_Click"></asp:LinkButton>
    <asp:Panel ID="pnAlbum" runat="server">
        <asp:Repeater ID="rptAlbum" runat="server">
            <HeaderTemplate>
                <div class="container">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                        <asp:ImageButton ID="imgBtnAlbum" runat="server"  Width="100px" Height="130px" />
                    </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="pnAlbumPhoto" runat="server" Visible="false">
        <asp:Repeater ID="rptAlbumPhoto" runat="server">
            <HeaderTemplate>
                <div class="container">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                        <asp:Image ID="imgAlbumPhoto" runat="server" Width="100px" Height="130px" />
                    </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>
