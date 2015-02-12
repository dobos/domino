<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.master" AutoEventWireup="true" CodeBehind="SignOut.aspx.cs" Inherits="Complex.Domino.Web.Auth.SignOut" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" Text="<%$ Resources:Labels, SignOut %>" /></h1>
    <asp:Panel runat="server" DefaultButton="Ok" ID="messagePanel">
        <div class="frame">
            <asp:Label runat="server" Text="<%$ Resources:Labels, SignOutSuccess %>" />
        </div>
        <toolbar class="form">
            <asp:LinkButton runat="server" OnClick="Ok_Click" ID="Ok">
                <asp:Image runat="server" SkinID="OkButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
