<%@ Page Title="" Language="C#" MasterPageFile="~/Files/Files.master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Complex.Domino.Web.Files.View" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="ImagePanel" Visible="false">
        <asp:Image runat="server" ID="ImageView" />
    </asp:Panel>
    <asp:Panel runat="server" ID="CodePanel" Visible="false">
    </asp:Panel>
</asp:Content>