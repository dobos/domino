<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubmissionList.ascx.cs" Inherits="Complex.Domino.Web.Teacher.SubmissionList" %>

<asp:ObjectDataSource runat="server" ID="submissionDataSource" DataObjectTypeName="Complex.Domino.Lib.Submission"
    OnObjectCreating="submissionDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.SubmissionFactory"
    SelectMethod="Find"
    SelectCountMethod="Count"
    StartRowIndexParameterName="from"
    MaximumRowsParameterName="max"
    SortParameterName="orderBy"
    EnablePaging="true" />
<asp:ListView runat="server" ID="submissionList" DataSourceID="submissionDataSource">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
    </LayoutTemplate>
    <ItemTemplate>
        <asp:HyperLink runat="server" CssClass="fullbar" ID="SubmissionsLink"
            NavigateUrl='<%# Complex.Domino.Web.Student.Submission.GetUrl((int)Eval("AssignmentID"), (int)Eval("ID")) %>'>
                <asp:Image runat="server" SkinID="SubmissionIcon" />
                <h1><asp:Label runat="server" Text="<%$ Resources:Labels, SubmissionDate %>" />: <%# Eval("CreatedDate") %></h1>
                <p><%# Eval("SemesterDescription") %> |
                   <%# Eval("CourseDescription") %> |
                   <%# Eval("AssignmentDescription") %>
                   </p>
        </asp:HyperLink>
    </ItemTemplate>
    <EmptyItemTemplate>
        <p><asp:Label runat="server" Text="<%$ Resources:Labels, NoSubmissions %>" /></p>
    </EmptyItemTemplate>
</asp:ListView>
