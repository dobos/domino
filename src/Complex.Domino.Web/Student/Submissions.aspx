<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Submissions.aspx.cs" Inherits="Complex.Domino.Web.Student.Submissions" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All submissions</h1>
    <div class="toolbar">
        <asp:HyperLink runat="server" ID="NewSubmission" CssClass="button">
            <asp:Image runat="server" SkinID="NewSubmissionButton" />
            <p>New submission</p>
        </asp:HyperLink>
    </div>
    <asp:ObjectDataSource runat="server" ID="submissionDataSource" DataObjectTypeName="Complex.Domino.Lib.Submission"
        OnObjectCreating="submissionDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.SubmissionFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <asp:ListView runat="server" ID="submissionList" DataSourceID="submissionDataSource">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </LayoutTemplate>
        <ItemTemplate>
            <asp:HyperLink runat="server" CssClass="fullbar" ID="SubmissionsLink"
                NavigateUrl='<%# Complex.Domino.Web.Student.Submission.GetUrl((int)Eval("AssignmentID"), (int)Eval("ID")) %>'>
                <asp:Image runat="server" SkinID="SubmissionIcon" />
                <h1>Date <%# Eval("Date") %></h1>
                <p>Assigment: <%# Eval("AssignmentName") %> |
                   Course: <%# Eval("CourseName") %> |
                   Semester: <%# Eval("SemesterName") %></p>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
