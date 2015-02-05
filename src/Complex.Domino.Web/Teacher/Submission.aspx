<%@ Page Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Submission.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Submission" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="filesPanel" Visible="false">
        <h1>
            <asp:Label runat="server" ID="formLabel" Text="<%$ Resources:Labels, EvaluateSubmission %>" /></h1>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="courseLabel" Text="<%$ Resources:Labels, Course %>" />:
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="semesterDescription" /> |
                        <asp:Label runat="server" ID="courseDescription" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="assignmentLabel" Text="<%$ Resources:Labels, Assignment %>" />:
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="assignmentDescription" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="studentLabel" Text="<%$ Resources:Labels, Student %>" />:
                    </td>
                    <td class="field">
                        <asp:HyperLink runat="server" ID="studentName" Target="_blank" /> |
                        <asp:HyperLink runat="server" ID="studentDescription" Target="_blank" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="createdDateLabel" Text="<%$ Resources:Labels, SubmissionDate %>" />:
                    </td>
                    <td class="field">
                        <asp:Label runat="server" ID="createdDate" />
                    </td>
                </tr>
            </table>
        </div>
        <h2><asp:Label runat="server" ID="commentsLabel" Text="<%$ Resources:Labels, StudentComments %>" /></h2>
        <div class="frame">
            <asp:Label runat="server" ID="comments" />
        </div>
        <h2>
            <asp:Label runat="server" Text="<%$ Resources:Labels, UploadedFiles %>" /></h2>
        <domino:filebrowser runat="server" id="fileBrowser" allowselection="true"
            allowdownload="true" AllowDelete="true" allowedit="true" />
        <h2>
            <asp:Label runat="server" Text="<%$ Resources:Labels, Evaluation %>" /></h2>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label" colspan="3">
                        <asp:CheckBox runat="server" ID="markRead" Text="<%$ Resources:Labels, MarkRead %>" Checked="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox runat="server" ID="sendReply" Text="<%$ Resources:Labels, SendReply %>" Checked="false"
                            AutoPostBack="true" CausesValidation="false" OnCheckedChanged="SendReply_CheckedChanged" />
                    </td>
                </tr>
                <tr runat="server" id="CommentsRow3" visible="false">
                    <td class="label" colspan="3">
                        <asp:Label runat="server" ID="replyLabel" Text="<%$ Resources:Labels, Reply %>" />:
                        <asp:Label runat="server" Text="<%$ Resources:Labels, VisibleToStudent %>" />
                    </td>
                </tr>
                <tr runat="server" id="CommentsRow4" visible="false">
                    <td colspan="3" class="field">
                        <asp:TextBox runat="server" ID="reply" TextMode="MultiLine" ValidationGroup="Entity" />
                    </td>
                </tr>
                <tr runat="server" id="CommentsRow5">
                    <td class="label" colspan="3">
                        <asp:Label runat="server" ID="gradeCommentsLabel" Text="<%$ Resources:Labels, Comments %>" />:
                        <asp:Label runat="server" Text="<%$ Resources:Labels, HiddenFromStudent %>" />
                    </td>
                </tr>
                <tr runat="server" id="CommentsRow6">
                    <td colspan="3" class="field">
                        <asp:TextBox runat="server" ID="gradeComments" TextMode="MultiLine" ValidationGroup="Entity" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="gradeLabel" />:
                    </td>
                    <td class="field">
                        <asp:TextBox runat="server" ID="grade" />
                    </td>
                    <td class="error"></td>
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
                <p><asp:Label runat="server" ID="cancelLabel" Text="<%$ Resources:Labels, Cancel %>" /></p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
