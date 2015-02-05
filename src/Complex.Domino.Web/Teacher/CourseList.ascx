<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseList.ascx.cs" Inherits="Complex.Domino.Web.Teacher.CourseList" %>

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
            <div class="fullbar" ID="AssignmentsLink" />
                <asp:Image runat="server" SkinID="CourseIcon" />
                <h1><%# Eval("Description") %></h1>
                <p><%# Eval("SemesterDescription") %> |
                    <asp:HyperLink runat="server" NavigateUrl='<%# Complex.Domino.Web.Teacher.Course.GetUrl((int)Eval("ID")) %>' Text="<%$ Resources: Labels, ModifyCourse %>" /> |
                    <asp:HyperLink runat="server" NavigateUrl='<%# Complex.Domino.Web.Teacher.Students.GetUrl((int)Eval("ID")) %>' Text="<%$ Resources: Labels, Students %>" /> |
                    <asp:HyperLink runat="server" NavigateUrl='<%# Complex.Domino.Web.Teacher.Assignments.GetUrl((int)Eval("ID")) %>' Text="<%$ Resources:Labels, Assignments %>" /> |
                    <asp:HyperLink runat="server" NavigateUrl='<%# Complex.Domino.Web.Teacher.Spreadsheet.GetUrl((int)Eval("ID")) %>' Text="<%$ Resources:Labels, Submissions %>" />
                </p>
            </div>
        </ItemTemplate>
    </asp:ListView>