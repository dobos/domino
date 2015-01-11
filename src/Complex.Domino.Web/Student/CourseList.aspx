<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="CourseList.aspx.cs" Inherits="Complex.Domino.Web.Student.CourseList" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Courses</h1>
    <asp:ObjectDataSource runat="server" ID="courseDataSource" DataObjectTypeName="Complex.Domino.Lib.Course"
        OnObjectCreating="courseDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.CourseFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <asp:ListView ID="courseList" runat="server" DataSourceID="courseDataSource">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </LayoutTemplate>
        <ItemTemplate>
            <div>
                <p>Course: <%# Eval("Name") %></p>
                <p>Semester: <%# Eval("SemesterName") %></p>
                <a href="AssignmentList.aspx?CourseID=<%# Eval("ID") %>">view assignments</a>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
