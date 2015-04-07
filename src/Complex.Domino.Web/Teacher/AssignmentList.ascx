<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignmentList.ascx.cs" Inherits="Complex.Domino.Web.Teacher.AssignmentList" %>

<asp:ObjectDataSource runat="server" ID="assignmentDataSource" DataObjectTypeName="Complex.Domino.Lib.Assignment"
    OnObjectCreating="assignmentDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.AssignmentFactory"
    SelectMethod="Find"
    SelectCountMethod="Count"
    StartRowIndexParameterName="from"
    MaximumRowsParameterName="max"
    SortParameterName="orderBy"
    EnablePaging="true" />
<asp:ListView runat="server" ID="assignmentList" DataSourceID="assignmentDataSource">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
    </LayoutTemplate>
    <ItemTemplate>
        <div class="fullbar">
            <asp:Image runat="server" SkinID="AssignmentIcon" />
            <h1><%# Eval("Description") %></h1>
            <h2>
                <asp:Label runat="server" ID="semester" Text='<%# Eval("SemesterName") %>' /> |
                <asp:Label runat="server" ID="course" Text='<%# Eval("CourseName") %>' /> |
                <asp:HyperLink runat="server" NavigateUrl='<%# Complex.Domino.Web.Teacher.Assignment.GetUrl((int)Eval("CourseID"), (int)Eval("ID")) %>' Text="<%$ Resources:Labels, ModifyAssignment %>" />
                <asp:HyperLink runat="server" NavigateUrl='<%# Complex.Domino.Web.Teacher.Spreadsheet.GetUrl((int)Eval("CourseID"), (int)Eval("ID")) %>' Text="<%$ Resources:Labels, Submissions %>" />
            </h2>
        </div>
    </ItemTemplate>
    <EmptyDataTemplate>
        <asp:Label runat="server" Text="<%$ Resources:Labels, NoAssignments %>" />
    </EmptyDataTemplate>
</asp:ListView>
