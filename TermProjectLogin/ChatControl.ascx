<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChatControl.ascx.cs" Inherits="TermProjectLogin.ChatControl" %>
<asp:Panel ID="pnUser" runat="server">
    <asp:Repeater ID="rptUser" runat="server">
        <HeaderTemplate>
            <div class="container">
                <div class="row">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                <asp:Image ID="imgProfilePhoto" runat="server" ImageUrl='<%# Eval("ProfilePhotoURL") %>' Width="25px" Height="40px" />
                <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("OnlineStatus") %>'></asp:Label>
                <asp:Button ID="vtnChat" runat="server" Text="Chat" />
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
                </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Panel>
<asp:Panel ID="pnChat" runat="server">
    <asp:LinkButton ID="lbtnName" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("Email") %>' OnClick="lbtnName_Click"></asp:LinkButton>
    <asp:UpdatePanel ID="upnMessages" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="rptChat" runat="server">
                <HeaderTemplate>
                    <div class="container">
                        <div class="row">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        <asp:Label ID="lblMessage" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </div>
                </FooterTemplate>
            </asp:Repeater>
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnSend" runat="server" Text="Send" />
            <asp:Timer ID="timer" Interval="1000" runat="server"></asp:Timer>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="timer" EventName="Tick" />
            <asp:AsyncPostBackTrigger ControlID="btnSend" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
