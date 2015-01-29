<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="Complex.Domino.Web.Student.Course" %>

<%@ Register Src="~/Student/AssignmentList.ascx" TagPrefix="domino" TagName="AssignmentList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, Course %>" />:
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
    <h2><asp:Label runat="server" Text="<%$ Resources:Labels, Assignments %>" /></h2>
    <domino:assignmentlist runat="server" id="AssignmentList" />
</asp:Content>
