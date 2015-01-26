<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseList.ascx.cs" Inherits="Complex.Domino.Web.Student.CourseList" %>

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
                NavigateUrl='<%# Complex.Domino.Web.Student.Course.GetUrl((int)Eval("ID")) %>'>
                <asp:Image ID="Image1" runat="server" SkinID="CourseIcon" />
                <h1>Course: <%# Eval("Description") %></h1>
                <p>Semester: <%# Eval("SemesterDescription") %></p>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>