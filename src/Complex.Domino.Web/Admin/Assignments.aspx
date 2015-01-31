<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Assignments.aspx.cs" Inherits="Complex.Domino.Web.Admin.Assignments" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All assignments</h1>
    <toolbar>
        <asp:HyperLink runat="server" ID="ToolbarCreate">
            <asp:Image runat="server" SkinID="NewAssignmentButton" />
            <p>Create Assignment</p>
        </asp:HyperLink>
    </toolbar>
    <asp:ObjectDataSource runat="server" ID="assignmentDataSource" DataObjectTypeName="Complex.Domino.Lib.Assignment"
        OnObjectCreating="assignmentDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.AssignmentFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        SortParameterName="orderBy"
        EnablePaging="true" />
    <domino:multiselectgridview id="assignmentList" runat="server" datasourceid="assignmentDataSource"
        autogeneratecolumns="false" datakeynames="ID" cssclass="grid" allowsorting="true"
        allowpaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="SemesterID"
                DataNavigateUrlFormatString="semester.aspx?ID={0}"
                DataTextField="SemesterName"
                HeaderText="Semester"
                SortExpression="SemesterName"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="CourseID"
                DataNavigateUrlFormatString="course.aspx?ID={0}"
                DataTextField="CourseName"
                HeaderText="Course"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="assignment.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="Name"
                SortExpression="Name"/>
            <asp:BoundField HeaderText="Visible" DataField="Visible" />
            <asp:BoundField HeaderText="Enabled" DataField="Enabled" />
            <asp:BoundField HeaderText="Start date" DataField="StartDate" />
            <asp:BoundField HeaderText="End date" DataField="EndDate" />
            <asp:BoundField HeaderText="Grade type" DataField="GradeType" />
            <asp:BoundField HeaderText="Grade weight" DataField="GradeWeight" />
            <asp:HyperLinkField
                DataNavigateUrlFields="Url"
                DataNavigateUrlFormatString="{0}"
                Text="URL"
                HeaderText="Url"/>
        </Columns>
        <EmptyDataTemplate>
            <p>No assignments match the query.</p>
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
