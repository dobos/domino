<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Assignments.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Assignments" %>

<%@ Register Src="~/Teacher/AssignmentList.ascx" TagPrefix="dt" TagName="AssignmentList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, Assignments %>" /></h1>
    <asp:HyperLink runat="server" CssClass="fullbar" ID="NewAssignment">
        <asp:Image runat="server" SkinID="NewAssignmentIcon" />
        <h1><asp:Label runat="server" Text="<%$ Resources:Labels,NewAssignment %>" /></h1>
        <h2><asp:Label runat="server" Text="<%$ Resources:Labels,NewAssignmentDetails %>" /></h2>
    </asp:HyperLink>
    <dt:AssignmentList runat="server" id="AssignmentList" />
</asp:Content>
