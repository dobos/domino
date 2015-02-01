<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Assignments.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Assignments" %>

<%@ Register Src="~/Teacher/AssignmentList.ascx" TagPrefix="dt" TagName="AssignmentList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, Assignments %>" /></h1>
    <dt:AssignmentList runat="server" id="AssignmentList" />
</asp:Content>
