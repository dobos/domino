<%@ Page Title="" Language="C#" MasterPageFile="~/Domino.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Complex.Domino.Web.Default" %>

<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <h1>Welcome to Domino</h1>
    <p>
        Domino is a homework submission system.
    </p>

    <asp:HyperLink runat="server" CssClass="fullbar" ID="SignInLink">
        <asp:Image runat="server" SkinID="SignInIcon" />
        <h1>Sign in to Domino</h1>
        <h2>you need to be a registered user to use Domino</h2>
    </asp:HyperLink>

</asp:Content>
