<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="Complex.Domino.Web.Admin.Courses" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" ID="TitleLable" Text="<%$ Resources:Labels, Courses %>" /></h1>
    <toolbar>
        <asp:HyperLink runat="server" ID="ToolbarCreate">
            <asp:Image runat="server" SkinID="NewCourseButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, NewCourse %>" /></p>
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
        autogeneratecolumns="false" datakeynames="ID" cssclass="grid" allowsorting="true"
        allowpaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="course.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="<%$ Resources:Labels, Name %>"
                SortExpression="Name"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="SemesterID"
                DataNavigateUrlFormatString="courses.aspx?semesterID={0}"
                DataTextField="SemesterName"
                HeaderText="<%$ Resources:Labels, Semester %>" />
            <asp:BoundField HeaderText="Grade type" DataField="GradeType" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="users.aspx?CourseID={0}"
                Text="<%$ Resources:Labels, Students %>"
                HeaderText="<%$ Resources:Labels, Students %>"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="assignments.aspx?CourseID={0}"
                Text="<%$ Resources:Labels, Assignments %>"
                HeaderText="<%$ Resources:Labels, Assignments %>"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="Url"
                DataNavigateUrlFormatString="{0}"
                Text="<%$ Resources:Labels, WebPage %>"
                HeaderText="<%$ Resources:Labels, WebPage %>"/>
            <asp:CheckBoxField HeaderText="<%$ Resources:Labels, Visible %>" DataField="Visible" />
            <asp:CheckBoxField HeaderText="<%$ Resources:Labels, Enabled %>" DataField="Enabled" />
        </Columns>
        <EmptyDataTemplate>
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, NoCourses %>" /></p>
        </EmptyDataTemplate>
    </domino:multiselectgridview>
    <toolbar>
        <asp:LinkButton runat="server" ID="Delete" OnClick="Delete_Click" 
            ValidationGroup="Delete">
            <asp:Image runat="server" SkinID="DeleteButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Delete %>" /></p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
