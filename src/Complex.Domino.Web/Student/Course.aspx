<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="Complex.Domino.Web.Student.Course" %>

<%@ Register Src="~/Student/AssignmentList.ascx" TagPrefix="domino" TagName="AssignmentList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" Text="<%$ Resources:Labels, Course %>" />:
        <asp:Label runat="server" ID="Description" /></h1>
    <h2>
        <asp:Label runat="server" ID="SemesterLabel" Text="<%$ Resources:Labels, Semester %>" />:
        <asp:Label runat="server" ID="SemesterDescription" />
        |
        <asp:HyperLink runat="server" ID="Url" Target="_blank" Text="<%$ Resources:Labels, CourseWebPage %>" />
    </h2>
    <asp:Literal runat="server" ID="Comments" />
    <h3>
        <asp:Label runat="server" Text="<%$ Resources:Labels, Assignments %>" /></h3>
    <domino:assignmentlist runat="server" id="AssignmentList" />
    <asp:Panel runat="server" CssClass="placeholder" ID="gradePanel">
        <grade>
            <h1><asp:Label runat="server" ID="grade" /></h1>
            <p><asp:Label runat="server" ID="gradeLabel" /></p>
        </grade>
    </asp:Panel>
</asp:Content>
