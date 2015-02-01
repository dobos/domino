<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Semesters.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Semesters" %>

<%@ Register Src="~/Teacher/SemesterList.ascx" TagPrefix="domino" TagName="SemesterList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, Semesters %>" /></h1>
    <domino:SemesterList runat="server" id="SemesterList" />
</asp:Content>
