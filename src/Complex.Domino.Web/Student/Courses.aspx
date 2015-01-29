<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="Complex.Domino.Web.Student.Courses" %>

<%@ Register Src="~/Student/CourseList.ascx" TagPrefix="domino" TagName="CourseList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, AllCourses %>" /></h1>
    <domino:courselist runat="server" id="CourseList" />
</asp:Content>
