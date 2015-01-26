<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Assignment.aspx.cs" Inherits="Complex.Domino.Web.Student.Assignment" %>

<%@ Register Src="~/Student/SubmissionList.ascx" TagPrefix="domino" TagName="SubmissionList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Assignment:
        <asp:Label runat="server" ID="TitleLabel" /></h1>
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
            <tr runat="server" id="UrlRow">
                <td class="label">
                    <asp:Label runat="server" ID="UrlLabel" Text="Course web page:" />
                </td>
                <td class="field">
                    <asp:HyperLink runat="server" ID="Url" Target="_blank" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="GradeLabel" Text="Grade:" />
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
    <h2>Submissions</h2>
    <asp:HyperLink runat="server" CssClass="fullbar" ID="NewSubmission">
        <asp:Image runat="server" SkinID="NewSubmissionIcon" />
        <h1>New submission</h1>
        <p>upload files and submit homework</p>
    </asp:HyperLink>
    <domino:submissionlist runat="server" id="SubmissionList" />
</asp:Content>
