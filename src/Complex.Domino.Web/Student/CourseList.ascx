<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseList.ascx.cs" Inherits="Complex.Domino.Web.Student.CourseList" %>

<asp:ObjectDataSource runat="server" ID="courseDataSource" DataObjectTypeName="Complex.Domino.Lib.Course"
        OnObjectCreating="courseDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.CourseFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        SortParameterName="orderBy"
        EnablePaging="true" />
    <asp:ListView ID="courseList" runat="server" DataSourceID="courseDataSource">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </LayoutTemplate>
        <ItemTemplate>
            <asp:HyperLink runat="server" CssClass="fullbar" ID="AssignmentsLink"
                NavigateUrl='<%# Complex.Domino.Web.Student.Course.GetUrl((int)Eval("ID")) %>'>
                <asp:Image runat="server" SkinID="CourseIcon" />
                <h1><%# Eval("Description") %></h1>
                <h2><%# Eval("SemesterDescription") %></h2>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>