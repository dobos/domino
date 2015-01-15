<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Complex.Domino.Web.Student.Default" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Hello <asp:Label runat="server" ID="Name" /></h1>
    <asp:HyperLink runat="server" CssClass="fullbar" ID="CoursesLink">
        <asp:Image runat="server" SkinID="CourseIcon" />
        <h1>View your courses</h1>
        <p>second line of text</p>
    </asp:HyperLink>
    <asp:HyperLink runat="server" CssClass="fullbar" ID="AssignmentsLink">
        <asp:Image runat="server" SkinID="AssignmentIcon" />
        <h1>View your assignments</h1>
        <p>second line of text</p>
    </asp:HyperLink>
    <asp:HyperLink runat="server" CssClass="fullbar" ID="SubmissionsLink">
        <asp:Image runat="server" SkinID="SubmissionIcon" />
        <h1>View your submissions and replies</h1>
        <p>second line of text</p>
    </asp:HyperLink>
</asp:Content>
