<%@ Page Title="" Language="C#" MasterPageFile="~/Domino.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Complex.Domino.Web.Default" %>

<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" ID="Welcome" Text="<%$ Resources:Labels, Welcome %>" /></h1>

    <asp:HyperLink runat="server" CssClass="fullbar" ID="SignInLink">
        <asp:Image runat="server" SkinID="SignInIcon" />
        <h1><asp:Label runat="server" Text="<%$ Resources:Labels, SignInFormLabel %>" /></h1>
        <h2><asp:Label runat="server" Text="<%$ Resources:Labels, SignInFormDetails %>" /></h2>
    </asp:HyperLink>

</asp:Content>
