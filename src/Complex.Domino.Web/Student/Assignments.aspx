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
            <asp:HyperLink runat="server" CssClass="fullbar" ID="SubmissionsLink"
                NavigateUrl='<%# Complex.Domino.Web.Student.Submissions.GetUrl((int)Eval("ID")) %>'>
                <asp:Image ID="Image1" runat="server" SkinID="AssignmentIcon" />
                <h1>Assigment <%# Eval("Name") %></h1>
                <p>Course: <%# Eval("CourseName") %> | Semester: <%# Eval("SemesterName") %></p>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
