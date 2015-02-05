<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Assignment.aspx.cs" Inherits="Complex.Domino.Web.Student.Assignment" %>

<%@ Register Src="~/Student/SubmissionList.ascx" TagPrefix="domino" TagName="SubmissionList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" Text="<%$ Resources:Labels, Assignment %>" />:
        <asp:Label runat="server" ID="Description" /></h1>
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
            <tr runat="server" id="UrlRow">
                <td class="label">
                    <asp:Label runat="server" ID="UrlLabel" Text="<%$ Resources:Labels, WebPage %>" />:
                </td>
                <td class="field">
                    <asp:HyperLink runat="server" ID="Url" Target="_blank" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="GradeLabel" Text="<%$ Resources:Labels, Grade %>" />:
                </td>
                <td class="field">
                    <asp:Label runat="server" ID="Grade" />
                </td>
            </tr>
        </table>
    </div>
    <div runat="server" class="frame" id="CommentsPanel">
        <asp:Label runat="server" ID="Comments" />
    </div>
    <h2>
        <asp:Label runat="server" Text="<%$ Resources:Labels, Submissions %>" /></h2>
    <asp:HyperLink runat="server" CssClass="fullbar" ID="NewSubmission">
        <asp:Image runat="server" SkinID="NewSubmissionIcon" />
        <h1><asp:Label runat="server" Text="<%$ Resources:Labels,NewSubmission %>" /></h1>
        <h2><asp:Label runat="server" Text="<%$ Resources:Labels,NewSubmissionDetails %>" /></h2>
    </asp:HyperLink>
    <domino:submissionlist runat="server" id="SubmissionList" />
</asp:Content>
