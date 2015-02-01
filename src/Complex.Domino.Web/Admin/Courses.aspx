<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="Complex.Domino.Web.Admin.Courses" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All courses</h1>
    <toolbar>
        <asp:HyperLink runat="server" ID="ToolbarCreate">
            <asp:Image runat="server" SkinID="NewCourseButton" />
            <p>Create Course</p>
        </asp:HyperLink>
    </toolbar>
    <asp:ObjectDataSource runat="server" ID="courseDataSource" DataObjectTypeName="Complex.Domino.Lib.Course"
        OnObjectCreating="courseDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.CourseFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        SortParameterName="orderBy"
        EnablePaging="true" />
    <domino:multiselectgridview id="courseList" runat="server" datasourceid="courseDataSource"
        autogeneratecolumns="false" datakeynames="ID" CssClass="grid" AllowSorting="true"
        allowpaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="SemesterID"
                DataNavigateUrlFormatString="courses.aspx?semesterID={0}"
                DataTextField="SemesterName"
                HeaderText="Semester" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="course.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="Name"
                SortExpression="Name"/>
            <asp:BoundField HeaderText="Grade type" DataField="GradeType" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="users.aspx?CourseID={0}"
                Text="students"
                HeaderText="Students"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="assignments.aspx?CourseID={0}"
                Text="assignments"
                HeaderText="Assignment"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="Url"
                DataNavigateUrlFormatString="{0}"
                Text="URL"
                HeaderText="Url"/>
            <asp:CheckBoxField HeaderText="Visible" DataField="Visible" />
            <asp:CheckBoxField HeaderText="Enabled" DataField="Enabled" />
        </Columns>
        <EmptyDataTemplate>
            <p>No courses match the query.</p>
        </EmptyDataTemplate>
    </domino:multiselectgridview>
    <toolbar>
        <asp:LinkButton runat="server" ID="Delete" OnClick="Delete_Click" 
            OnClientClick="return confirm('Are you sure you want to delete the selected items?')"
            ValidationGroup="Delete">
            <asp:Image runat="server" SkinID="DeleteButton" />
            <p>Delete</p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
