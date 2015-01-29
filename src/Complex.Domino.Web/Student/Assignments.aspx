<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Assignments.aspx.cs" Inherits="Complex.Domino.Web.Student.Assignments" %>

<%@ Register Src="~/Student/AssignmentList.ascx" TagPrefix="domino" TagName="AssignmentList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, AllAssignments %>" /></h1>
    <domino:AssignmentList runat="server" id="AssignmentList" />
</asp:Content>
