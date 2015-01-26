<%@ Page Title="" Language="C#" MasterPageFile="~/Domino.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Complex.Domino.Web.Default" %>

<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <h1>Welcome to Domino</h1>
    <p>
        Domino is a homework submission system.
    </p>

    <asp:HyperLink runat="server" CssClass="fullbar" ID="SignInLink">
        <asp:Image runat="server" SkinID="SignInIcon" />
        <h1>Sign in to Domino</h1>
        <p>you need to be a registered user to use Domino</p>
    </asp:HyperLink>

</asp:Content>
