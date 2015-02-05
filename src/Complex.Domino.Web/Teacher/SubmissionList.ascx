<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubmissionList.ascx.cs" Inherits="Complex.Domino.Web.Teacher.SubmissionList" %>

<asp:ObjectDataSource runat="server" ID="submissionDataSource" DataObjectTypeName="Complex.Domino.Lib.Submission"
    OnObjectCreating="SubmissionDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.SubmissionFactory"
    SelectMethod="Find"
    SelectCountMethod="Count"
    StartRowIndexParameterName="from"
    MaximumRowsParameterName="max"
    SortParameterName="orderBy"
    EnablePaging="true" />
<asp:ListView runat="server" ID="submissionList" DataSourceID="submissionDataSource"
    OnItemCreated="SubmissionList_ItemCreated">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
    </LayoutTemplate>
    <ItemTemplate>
        <asp:HyperLink runat="server" CssClass="fullbar" ID="submissionsLink"
            NavigateUrl='<%# Complex.Domino.Web.Teacher.Submission.GetUrl((int)Eval("AssignmentID"), (int)Eval("ID")) %>'>
            <asp:Image runat="server" SkinID="SubmissionIcon" />
            <h1>
                <asp:Label runat="server" ID="createdDateLabel" />: <%# Eval("CreatedDate") %></h1>
            <h2><%# Eval("SemesterDescription") %> |
                   <%# Eval("CourseDescription") %> |
                   <%# Eval("AssignmentDescription") %>
            </h2>
            <p>
                <%# Eval("Comments") %>
            </p>
        </asp:HyperLink>
    </ItemTemplate>
    <EmptyItemTemplate>
        <p>
            <asp:Label runat="server" Text="<%$ Resources:Labels, NoSubmissions %>" /></p>
    </EmptyItemTemplate>
</asp:ListView>
