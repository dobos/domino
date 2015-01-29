<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserStatus.ascx.cs" Inherits="Complex.Domino.Web.Controls.UserStatus" %>
<asp:Panel ID="authenticatedPanel" runat="server" Visible="false">
    <asp:Label ID="UsernameLabel" runat="server" Text="<%$ Resources:Labels, User %>" />:
    <asp:HyperLink ID="Username" runat="server" />
    |
    <asp:HyperLink ID="SignOut" runat="server" Text="<%$ Resources:Labels, SignOut %>" />
</asp:Panel>
<asp:Panel ID="anonymousPanel" runat="server" Visible="false">
    <asp:HyperLink ID="SignIn" runat="server" Text="<%$ Resources:Labels, SignIn %>" />
</asp:Panel>

