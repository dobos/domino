<%@ Page Title="" Language="C#" MasterPageFile="~/Files/Files.master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Complex.Domino.Web.Files.Edit" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="ImagePanel" Visible="false">
        <asp:Image runat="server" ID="ImageView" />
    </asp:Panel>
    <asp:Panel runat="server" ID="CodePanel" Visible="false">
        <domino:CodeMirror runat="server" ID="CodeView" Theme="default" />
    </asp:Panel>
    <asp:Panel runat="server" ID="DownloadPanel" Visible="false">
        <asp:HyperLink runat="server" ID="DownloadLink">
            Download file
        </asp:HyperLink>
    </asp:Panel>
</asp:Content>
