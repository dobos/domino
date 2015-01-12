<%@ Page Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Submission.aspx.cs" Inherits="Complex.Domino.Web.Student.Submission" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>New submission</h1>
    <domino:FileBrowser runat="server" ID="fileBrowser" />
    <div class="toolbar">
        <asp:LinkButton runat="server" ID="OK" OnClick="Ok_Click" Text="Submit" />
        <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" Text="Cancel" CausesValidation="false" />
    </div>
</asp:Content>
