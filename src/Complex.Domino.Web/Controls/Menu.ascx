<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="Complex.Domino.Web.Controls.Menu" %>
<asp:Panel ID="AdminPanel" runat="server" Visible="false">
    <asp:Label runat="server" ID="AdminMenu" Text="<%$ Resources:Menu, AdminMenu %>" />
    <ul class="menu">
        <li>
            <asp:HyperLink runat="server" ID="AdminSemesters" Text="<%$ Resources:Menu, Semesters %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="AdminCourses" Text="<%$ Resources:Menu, Courses %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="AdminAssignments" Text="<%$ Resources:Menu, Assignments %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="AdminUsers" Text="<%$ Resources:Menu, Users %>" /></li>
    </ul>
</asp:Panel>

<asp:Panel ID="TeacherPanel" runat="server" Visible="false">
    <asp:Label runat="server" ID="TeacherMenu" Text="<%$ Resources:Menu, TeacherMenu %>" />
    <ul class="menu">
        <li>
            <asp:HyperLink runat="server" ID="TeacherSemesters" Text="<%$ Resources:Menu, Semesters %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="TeacherCourses" Text="<%$ Resources:Menu, Courses %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="TeacherAssignments" Text="<%$ Resources:Menu, Assignments %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="TeacherStudents" Text="<%$ Resources:Menu, Students %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="TeacherSearch" Text="<%$ Resources:Menu, Search %>" /></li>
    </ul>
</asp:Panel>

<asp:Panel ID="StudentPanel" runat="server" Visible="false">
    <asp:Label runat="server" ID="StudentMenu" Text="<%$ Resources:Menu, StudentMenu %>" />
    <ul class="menu">
        <li>
            <asp:HyperLink runat="server" ID="StudentMain" Text="<%$ Resources:Menu, Main %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="StudentCourses" Text="<%$ Resources:Menu, Courses %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="StudentAssignments" Text="<%$ Resources:Menu, Assignments %>" /></li>
    </ul>
</asp:Panel>

<asp:Panel ID="UserPanel" runat="server" Visible="false">
    <asp:Label runat="server" ID="UserMenu" Text="<%$ Resources:Menu, UserMenu %>" />
    <ul class="menu">
        <li>
            <asp:HyperLink runat="server" ID="UserAccount" Text="<%$ Resources:Menu, UpdateUser %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="UserPassword" Text="<%$ Resources:Menu, ChangePassword %>" /></li>
        <li>
            <asp:HyperLink runat="server" ID="UserSignOut" Text="<%$ Resources:Menu, SignOut %>" /></li>
    </ul>
</asp:Panel>
