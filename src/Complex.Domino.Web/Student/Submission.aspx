<%@ Page Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Submission.aspx.cs" Inherits="Complex.Domino.Web.Student.Submission" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="emptyPanel">
        <h1>New submission</h1>
        <p>
            You have chosen to start a new submission but files from an earlier
            submission are available. Do you want to keep these or start from
            an empty folder?
        </p>

            <asp:LinkButton CssClass="fullbar" runat="server" ValidationGroup="NewSubmission" ID="NewSubmissionKeep"
                OnClick="NewSubmissionKeep_Click">
                <asp:Image runat="server" SkinID="KeepUploadsIcon" />
                <h1>Keep files</h1>
                <p>Please verify your files before submission.</p>
            </asp:LinkButton>
            <asp:LinkButton CssClass="fullbar" runat="server" ValidationGroup="NewSubmission" ID="NewSubmissionEmpty"
                OnClick="NewSubmissionEmpty_Click">
                <asp:Image runat="server" SkinID="DeleteUploadsIcon" />
                <h1>Delete files</h1>
                <p>Start with empty folder and upload files again.</p>
            </asp:LinkButton>
    </asp:Panel>
    <asp:Panel runat="server" ID="filesPanel" Visible="false">
        <h1>New submission</h1>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="SemesterLabel" Text="Semester:" />
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="SemesterDescription" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="CourseLabel" Text="Course:" />
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="CourseDescription" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="AssignmentLabel" Text="Assignment:" />
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="AssignmentDescription" />
                    </td>
                </tr>
            </table>
        </div>
        <h2>Uploaded files</h2>
        <domino:filebrowser runat="server" id="fileBrowser" AllowSelection="false"
            AllowDownload="false" AllowEdit="false" />
        <domino:EntityForm runat="server" id="entityForm"
            NameVisible="false" DescriptionVisible="false" OptionsVisible="false" />
        <toolbar class="form">
            <asp:LinkButton runat="server" ID="OK" OnClick="Ok_Click">
                <asp:Image runat="server" SkinID="OkButton" />
                <p>Submit</p>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="false">
                <asp:Image runat="server" SkinID="CancelButton" />
                <p>Cancel</p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
