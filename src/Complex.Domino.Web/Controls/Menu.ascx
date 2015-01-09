<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="Complex.Domino.Web.Controls.Menu" %>
<h1>&nbsp;</h1>
admin menu
<ul class="menu">
    <li><asp:HyperLink runat="server" ID="Semesters">semesters</asp:HyperLink></li>
    <li><asp:HyperLink runat="server" ID="Courses">courses</asp:HyperLink></li>
    <li><asp:HyperLink runat="server" ID="Users">users</asp:HyperLink></li>
</ul>
teacher menu
<ul class="menu">
    <li>semesters</li>
    <li>courses</li>
    <li>assignments</li>
    <li>students</li>
    <li>submission</li>
</ul>
student menu
<ul class="menu">
    <li>semesters</li>
    <li>courses</li>
    <li>assignments</li>
    <li>submissions</li>
</ul>
