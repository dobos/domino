<%@ Page Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Submission.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Submission" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="filesPanel" Visible="false">
        <h1><asp:Label runat="server" ID="formLabel" /></h1>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="SemesterLabel" Text="<%$ Resources:Labels, Semester %>" />:
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="semesterDescription" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="CourseLabel" Text="<%$ Resources:Labels, Course %>" />:
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="courseDescription" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="AssignmentLabel" Text="<%$ Resources:Labels, Assignment %>" />:
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="assignmentDescription" />
                    </td>
                </tr>
            </table>
        </div>
        <h2><asp:Label runat="server" Text="<%$ Resources:Labels, UploadedFiles %>" /></h2>
        <domino:filebrowser runat="server" id="fileBrowser" AllowSelection="false"
            AllowDownload="false" AllowEdit="false" />
        <domino:EntityForm runat="server" id="entityForm"
            NameVisible="false" DescriptionVisible="false" OptionsVisible="false" />
        <toolbar class="form">
            <asp:LinkButton runat="server" ID="ok" OnClick="Ok_Click">
                <asp:Image runat="server" SkinID="OkButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Submit %>" /></p>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="cancel" OnClick="Cancel_Click" CausesValidation="false">
                <asp:Image runat="server" SkinID="CancelButton" />
                <p><asp:Label runat="server" ID="cancelLabel" /></p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
