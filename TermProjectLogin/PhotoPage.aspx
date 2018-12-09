<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoPage.aspx.cs" Inherits="TermProjectLogin.PhotoPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Photos</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
            <asp:Panel ID="pnAlbum" runat="server">
                <asp:Repeater ID="rptAlbum" runat="server">
                    <HeaderTemplate>
                        <div class="container">
                            <div class="row">
                    </HeaderTemplate>
                    <ItemTemplate>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                                <asp:ImageButton ID="imgBtnAlbum" runat="server"  Width="100px" Height="130px"/>
                            </div>
                    </ItemTemplate>
                    <FooterTemplate>
                            </div>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel ID="pbPhoto" runat="server">
                <asp:Repeater ID="rptPhoto" runat="server">
                    <HeaderTemplate>
                        <div class="container">
                            <div class="row">
                    </HeaderTemplate>
                    <ItemTemplate>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                                <asp:CheckBox ID="chkPhoto" runat="server" Visible="false"/>
                                <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="130px" />
                            </div>
                    </ItemTemplate>
                    <FooterTemplate>
                            </div>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
