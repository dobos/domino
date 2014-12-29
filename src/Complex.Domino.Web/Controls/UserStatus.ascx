<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserStatus.ascx.cs" Inherits="Complex.Domino.Web.Controls.UserStatus" %>
<asp:Panel ID="authenticatedPanel" runat="server" Visible="false">
    <asp:Label ID="UsernameLabel" runat="server" Text="user:"> </asp:Label>
    <asp:HyperLink ID="Username" runat="server">username</asp:HyperLink>
    |
    <asp:HyperLink ID="SignOut" runat="server">sign out</asp:HyperLink>
</asp:Panel>
<asp:Panel ID="anonymousPanel" runat="server" Visible="false">
    <asp:HyperLink ID="SignIn" runat="server">sign in</asp:HyperLink>
</asp:Panel>

