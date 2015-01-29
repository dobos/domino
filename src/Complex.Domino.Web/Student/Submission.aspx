<%@ Page Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Submission.aspx.cs" Inherits="Complex.Domino.Web.Student.Submission" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="emptyPanel">
        <h1><asp:Label runat="server" Text="<%$ Resources:Labels, NewSubmission %>" /></h1>
        <p><asp:Literal runat="server" Text="<%$ Resources:Labels, NewSubmissionIntro %>" /></p>

            <asp:LinkButton CssClass="fullbar" runat="server" ValidationGroup="NewSubmission" ID="NewSubmissionKeep"
                OnClick="NewSubmissionKeep_Click">
                <asp:Image runat="server" SkinID="KeepUploadsIcon" />
                <h1><asp:Label runat="server" Text="<%$ Resources:Labels, KeepFiles %>" /></h1>
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, KeepFilesDetails %>" /></p>
            </asp:LinkButton>
            <asp:LinkButton CssClass="fullbar" runat="server" ValidationGroup="NewSubmission" ID="NewSubmissionEmpty"
                OnClick="NewSubmissionEmpty_Click">
                <asp:Image runat="server" SkinID="DeleteUploadsIcon" />
                <h1><asp:Label runat="server" Text="<%$ Resources:Labels, DeleteFiles %>" /></h1>
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, DeleteFilesDetails %>" /></p>
            </asp:LinkButton>
    </asp:Panel>
    <asp:Panel runat="server" ID="filesPanel" Visible="false">
        <h1><asp:Label runat="server" ID="FormLabel" /></h1>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="SemesterLabel" Text="<%$ Resources:Labels, Semester %>" />:
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="SemesterDescription" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="CourseLabel" Text="<%$ Resources:Labels, Course %>" />:
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="CourseDescription" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="AssignmentLabel" Text="<%$ Resources:Labels, Assignment %>" />:
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="AssignmentDescription" />
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
            <asp:LinkButton runat="server" ID="OK" OnClick="Ok_Click">
                <asp:Image runat="server" SkinID="OkButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Submit %>" /></p>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="false">
                <asp:Image runat="server" SkinID="CancelButton" />
                <p><asp:Label runat="server" ID="CancelLabel" /></p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
