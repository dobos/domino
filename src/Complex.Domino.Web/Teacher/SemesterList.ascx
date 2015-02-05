<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SemesterList.ascx.cs" Inherits="Complex.Domino.Web.Teacher.SemesterList" %>

<asp:ObjectDataSource runat="server" ID="semesterDataSource" DataObjectTypeName="Complex.Domino.Lib.Semester"
        OnObjectCreating="courseDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.SemesterFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        SortParameterName="orderBy"
        EnablePaging="true" />
    <asp:ListView ID="semesterList" runat="server" DataSourceID="semesterDataSource">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </LayoutTemplate>
        <ItemTemplate>
            <asp:HyperLink runat="server" CssClass="fullbar" ID="CoursesLink"
                NavigateUrl='<%# Complex.Domino.Web.Teacher.Courses.GetUrl((int)Eval("ID")) %>'>
                <asp:Image runat="server" SkinID="SemesterIcon" />
                <h1><%# Eval("Description") %></h1>
                <h2>...</h2>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>