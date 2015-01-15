<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Assignments.aspx.cs" Inherits="Complex.Domino.Web.Student.Assignments" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All assignments</h1>
    <asp:ObjectDataSource runat="server" ID="assignmentDataSource" DataObjectTypeName="Complex.Domino.Lib.Assignment"
        OnObjectCreating="assignmentDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.AssignmentFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <asp:ListView runat="server" ID="assignmentList" DataSourceID="assignmentDataSource">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </LayoutTemplate>
        <ItemTemplate>
            <div>
                <p>Assigment <%# Eval("Name") %></p>
                <p>Course: <%# Eval("CourseName") %></p>
                <p>Semester: <%# Eval("SemesterName") %></p>
                <a href="SubmissionList.aspx?AssignmentID=<%# Eval("ID") %>">view submissions</a>
                <a href="Submission.aspx?AssignmentID=<%# Eval("ID") %>">new submission</a>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
