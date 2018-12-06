<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="NotificationPage.aspx.cs" Inherits="TermProjectLogin.NotificationPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <h1>Notification Center</h1>
    <asp:GridView ID="gvNotification" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="URL">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnURL" runat="server" Text="View" OnClick="lbtnURL_Click" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="ReadStatus" HeaderText="Read" />
            <asp:BoundField DataField="NotificationDate" DataFormatString="{0:d}" HeaderText="Date" />
        </Columns>
    </asp:GridView>
</asp:Content>
