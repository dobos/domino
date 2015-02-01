<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Courses" %>

<%@ Register Src="~/Teacher/CourseList.ascx" TagPrefix="domino" TagName="CourseList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, Courses %>" /></h1>
    <domino:courselist runat="server" id="CourseList" />
</asp:Content>
