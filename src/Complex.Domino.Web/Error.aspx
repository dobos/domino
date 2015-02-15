<%@ Page Title="" Language="C#" MasterPageFile="~/Domino.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Complex.Domino.Web.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" Text="<%$ Resources:Labels, Error %>" /></h1>
    <div class="placeholder">
        <asp:Literal runat="server" Text="<%$ Resources:Labels, ErrorIntro %>" />
    </div>
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="ExceptionTypeLabel" Text="<%$ Resources:Labels, ExceptionType %>" />:</td>
                <td class="field">
                    <asp:Label runat="server" ID="ExceptionType" /></td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="ExceptionMessageLabel" Text="<%$ Resources:Labels, ExceptionMessage %>" />:</td>
                <td class="field">
                    <asp:Label runat="server" ID="ExceptionMessage" /></td>
                <td class="error"></td>
            </tr>
        </table>
    </div>
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label"><asp:Label runat="server" Text="<%$ Resources:Labels, Comments %>" />:</td>
            </tr>
            <tr>
                <td class="field">
                    <asp:TextBox runat="server" ID="comments" TextMode="MultiLine" />
                </td>
            </tr>
        </table>
    </div>
    <toolbar class="form">
        <asp:LinkButton runat="server" ID="ok" OnClick="Ok_Click">
            <asp:Image runat="server" SkinID="OkButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
        </asp:LinkButton>
        <asp:LinkButton runat="server" ID="cancel" OnClick="Cancel_Click" CausesValidation="false">
            <asp:Image runat="server" SkinID="CancelButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Cancel %>" /></p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
