<%@ Page Title="" Language="C#" MasterPageFile="~/Files/Files.master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Complex.Domino.Web.Files.TextEditor"
    ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="ImagePanel" Visible="false">
        <asp:Image runat="server" ID="ImageView" />
    </asp:Panel>
    <asp:Panel runat="server" ID="CodePanel" Visible="false">
        <toolbar class="editor">
            <asp:LinkButton runat="server" ID="Save" OnClick="Save_Click">
                <asp:Image runat="server" SkinId="SaveButton" />
                <p>Save</p>
            </asp:LinkButton>
        </toolbar>
        <domino:codemirror runat="server" id="CodeView" theme="default" CssClass="CodeMirror" />
    </asp:Panel>
    <asp:Panel runat="server" ID="DownloadPanel" Visible="false">
        <asp:HyperLink runat="server" ID="DownloadLink">
            Download file
        </asp:HyperLink>
    </asp:Panel>
</asp:Content>
