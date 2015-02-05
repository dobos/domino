<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Submissions.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Submissions" %>

<%@ Register Src="~/Teacher/SubmissionList.ascx" TagPrefix="dt" TagName="SubmissionList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, Submissions %>" /></h1>
    <dt:SubmissionList runat="server" id="submissionList" />
</asp:Content>
