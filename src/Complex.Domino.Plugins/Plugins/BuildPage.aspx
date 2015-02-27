<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Complex.Domino.Plugins.BuildPage" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="Build" /></h1>
    <div class="frame">
        <table class="form">
            <td class="label"><asp:Label runat="server" ID="CommandLineLabel" Text="Command-line" />:</td>
            <td class="field"><asp:TextBox runat="server" ID="CommandLine" /></td>
        </table>
    </div>
    <toolbar class="form">
        <asp:LinkButton runat="server" ID="ok" OnClick="Ok_Click">
            <asp:Image runat="server" SkinID="OkButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
        </asp:LinkButton>
    </toolbar>
    <div class="frame">
        <pre><asp:Literal runat="server" ID="Output" /></pre>
    </div>
</asp:Content>
