<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Complex.Domino.Web.Student.Default" %>
<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Hello <asp:Label runat="server" ID="NameLabel" /></h1>
    <asp:HyperLink runat="server" CssClass="fullbar" ID="CoursesLink">
        <asp:Image runat="server" SkinID="CourseIcon" />
        <h1><asp:Label runat="server" Text="<%$ Resources:Labels, ViewCourses %>" /></h1>
        <h2><asp:Label runat="server" Text="<%$ Resources:Labels, ViewCoursesDetails %>" /></h2>
    </asp:HyperLink>
    <asp:HyperLink runat="server" CssClass="fullbar" ID="AssignmentsLink">
        <asp:Image runat="server" SkinID="AssignmentIcon" />
        <h1><asp:Label runat="server" Text="<%$ Resources:Labels, ViewAssignments %>" /></h1>
        <h2><asp:Label runat="server" Text="<%$ Resources:Labels, ViewAssignmentsDetails %>" /></h2>
    </asp:HyperLink>
</asp:Content>
