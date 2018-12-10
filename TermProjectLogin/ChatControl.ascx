<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChatControl.ascx.cs" Inherits="TermProjectLogin.ChatControl" %>

<asp:Panel ID="pnChat" runat="server">
    <asp:Panel ID="pnUser" runat="server">
        <asp:Repeater ID="rptUser" runat="server">
            <HeaderTemplate>
                <ul class="list-group">
            </HeaderTemplate>
            <ItemTemplate>
                <li class="list-group-item">
                    <asp:Image ID="imgProfilePhoto" runat="server" ImageUrl='<%# Eval("ProfilePhotoURL") %>' Width="25px" Height="40px" />
                    <asp:LinkButton ID="lbtnName" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("Email") %>' OnClick="lbtnName_Click"></asp:LinkButton>
                    <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("OnlineStatus") %>'></asp:Label>
                    <asp:Button ID="btnChat" runat="server" Text="Chat" CommandArgument='<%# Eval("Email") %>' OnClick="btnChat_Click" />
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
     <asp:Timer ID="timer" Interval="1000" runat="server" OnTick="timer_Tick"></asp:Timer>
    <asp:UpdatePanel ID="upnMessages" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="rptChat" runat="server">
                <HeaderTemplate>
                    <ul class="list-group">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="list-group-item">
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>: 
                <asp:Label ID="lblMessage" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("MessageID") %>' OnClick="btnDelete_Click" />
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
           
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rptUser" />
            <asp:AsyncPostBackTrigger ControlID="timer" EventName="Tick" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnSend" EventName="Click" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
    <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" />
</asp:Panel>
