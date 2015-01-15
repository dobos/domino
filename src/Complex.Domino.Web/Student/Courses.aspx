<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="Complex.Domino.Web.Student.Courses" %>

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
            <asp:HyperLink runat="server" CssClass="fullbar" ID="AssignmentsLink"
                NavigateUrl='<%# Complex.Domino.Web.Student.Assignments.GetUrl((int)Eval("ID")) %>'>
                <asp:Image runat="server" SkinID="CourseIcon" />
                <h1>Course: <%# Eval("Name") %></h1>
                <p>Semester: <%# Eval("SemesterName") %></p>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
