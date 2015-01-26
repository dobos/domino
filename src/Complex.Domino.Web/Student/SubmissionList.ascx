<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubmissionList.ascx.cs" Inherits="Complex.Domino.Web.Student.SubmissionList" %>

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
                <asp:Image ID="Image1" runat="server" SkinID="SubmissionIcon" />
                <h1>Submitted: <%# Eval("Date") %></h1>
                <p>Assigment: <%# Eval("AssignmentDescription") %> |
                   Course: <%# Eval("CourseDescription") %> |
                   Semester: <%# Eval("SemesterDescription") %></p>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>