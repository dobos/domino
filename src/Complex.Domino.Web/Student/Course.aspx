<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="Complex.Domino.Web.Student.Course" %>

<%@ Register Src="~/Student/AssignmentList.ascx" TagPrefix="domino" TagName="AssignmentList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Course:
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
    <h2>Assignments</h2>
    <domino:assignmentlist runat="server" id="AssignmentList" />
</asp:Content>
