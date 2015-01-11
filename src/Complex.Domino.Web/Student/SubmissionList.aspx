<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="SubmissionList.aspx.cs" Inherits="Complex.Domino.Web.Student.SubmissionList" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All submissions</h1>
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
            <div>
                <p>Date <%# Eval("Date") %></p>
                <p>Assigment <%# Eval("AssignmentName") %></p>
                <p>Course: <%# Eval("CourseName") %></p>
                <p>Semester: <%# Eval("SemesterName") %></p>
                <a href="SubmissionList.aspx?AssignmentID=<%# Eval("ID") %>">view submissions</a>
                <a >new submission</a>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
