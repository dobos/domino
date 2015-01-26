<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="Complex.Domino.Web.Controls.Menu" %>
<asp:Panel ID="AdminPanel" runat="server" Visible="false">
    <asp:Label runat="server" ID="AdminMenu" Text="admin menu" />
    <ul class="menu">
        <li>
            <asp:HyperLink runat="server" ID="AdminUsers">users</asp:HyperLink></li>
        <li>
            <asp:HyperLink runat="server" ID="AdminSemesters">semesters</asp:HyperLink></li>
        <li>
            <asp:HyperLink runat="server" ID="AdminCourses">courses</asp:HyperLink></li>
        <li>
            <asp:HyperLink runat="server" ID="AdminAssignments">assignments</asp:HyperLink></li>
    </ul>
</asp:Panel>

<asp:Panel ID="TeacherPanel" runat="server" Visible="false">
    <asp:Label runat="server" ID="TeacherMenu" Text="teacher menu" />
    <ul class="menu">
        <li>semesters</li>
        <li>
            <asp:HyperLink runat="server" ID="TeacherCourses">courses</asp:HyperLink></li>
        <li>students</li>
        <li>submissions</li>
    </ul>
</asp:Panel>

<asp:Panel ID="StudentPanel" runat="server" Visible="false">
    <asp:Label runat="server" ID="StudentMenu" Text="student menu" />
    <ul class="menu">
        <li>
            <asp:HyperLink runat="server" ID="StudentMain" Text="main" /></li>
        <li>
            <asp:HyperLink runat="server" ID="StudentCourses" Text="courses" /></li>
        <li>
            <asp:HyperLink runat="server" ID="StudentAssignments" Text="assignments" /></li>
    </ul>
</asp:Panel>

<asp:Panel ID="UserPanel" runat="server" Visible="false">
    <asp:Label runat="server" ID="UserMenu" Text="your account" />
    <ul class="menu">
        <li>
            <asp:HyperLink runat="server" ID="UserAccount" Text="update account" /></li>
        <li>
            <asp:HyperLink runat="server" ID="UserPassword" Text="reset password" /></li>
        <li>
            <asp:HyperLink runat="server" ID="UserSignOut" Text="sign out" /></li>
    </ul>
</asp:Panel>
