<%@ Page Title="" Language="C#" MasterPageFile="~/Files/Files.master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Complex.Domino.Web.Files.TextEditor"
    ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <toolbar class="editor">
        <asp:LinkButton runat="server" ID="Save" OnClick="Save_Click">
            <asp:Image runat="server" SkinId="SaveButton" />
            <p>Save</p>
        </asp:LinkButton>
    </toolbar>
    <domino:codemirror runat="server" id="CodeView" theme="default" cssclass="CodeMirror" />
</asp:Content>
