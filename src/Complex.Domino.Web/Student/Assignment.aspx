<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Assignment.aspx.cs" Inherits="Complex.Domino.Web.Student.Assignment" %>

<%@ Register Src="~/Student/SubmissionList.ascx" TagPrefix="domino" TagName="SubmissionList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <div class="placeholder">
        <grade>
            <h1><asp:Label runat="server" ID="grade" /></h1>
            <p><asp:Label runat="server" ID="gradeLabel" /></p>
        </grade>
        <h1>
            <asp:Label runat="server" ID="Description" /></h1>
        <h2>
            <asp:Label runat="server" ID="SemesterDescription" />
            |
        <asp:HyperLink runat="server" ID="CourseDescription" />
            |
        <asp:HyperLink runat="server" ID="Url" Target="_blank" Text="<%$ Resources:Labels, CourseWebPage %>" />
        </h2>
    </div>
    <div class="placeholder">
        <asp:Literal runat="server" ID="Comments" />
    </div>
    <h3>
        <asp:Label runat="server" Text="<%$ Resources:Labels, Submissions %>" /></h3>
    <asp:HyperLink runat="server" CssClass="fullbar" ID="NewSubmission">
        <asp:Image runat="server" SkinID="NewSubmissionIcon" />
        <h1><asp:Label runat="server" Text="<%$ Resources:Labels,NewSubmission %>" /></h1>
        <h2><asp:Label runat="server" Text="<%$ Resources:Labels,NewSubmissionDetails %>" /></h2>
    </asp:HyperLink>
    <domino:submissionlist runat="server" id="SubmissionList" />
</asp:Content>
