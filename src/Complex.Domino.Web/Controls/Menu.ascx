<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="Complex.Domino.Web.Controls.Menu" %>
<h1>&nbsp;</h1>
admin menu
<ul class="menu">
    <li><asp:HyperLink runat="server" ID="AdminUsers">users</asp:HyperLink></li>
    <li><asp:HyperLink runat="server" ID="AdminSemesters">semesters</asp:HyperLink></li>
    <li><asp:HyperLink runat="server" ID="AdminCourses">courses</asp:HyperLink></li>
    <li><asp:HyperLink runat="server" ID="AdminAssignments">assignments</asp:HyperLink></li>
</ul>
teacher menu
<ul class="menu">
    <li>semesters</li>
    <li><asp:HyperLink runat="server" ID="TeacherCourses">courses</asp:HyperLink></li>
    <li>students</li>
    <li>submission</li>
</ul>
<asp:HyperLink runat="server" ID="StudentMenu">student menu</asp:HyperLink>
<ul class="menu">
    <li><asp:HyperLink runat="server" ID="StudentCourses">courses</asp:HyperLink></li>
    <li><asp:HyperLink runat="server" ID="StudentAssignments">assignments</asp:HyperLink></li>
    <li><asp:HyperLink runat="server" ID="StudentSubmissions">submissions</asp:HyperLink></li>
</ul>
