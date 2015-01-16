<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="AssignmentList.aspx.cs" Inherits="Complex.Domino.Web.Admin.AssignmentList" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All assignments</h1>
    <div class="toolbar">
        <asp:HyperLink runat="server" ID="ToolbarCreate" Text="Create Assignment" />
    </div>
    <asp:ObjectDataSource runat="server" ID="assignmentDataSource" DataObjectTypeName="Complex.Domino.Lib.Assignment"
        OnObjectCreating="assignmentDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.AssignmentFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <domino:multiselectgridview id="assignmentList" runat="server" datasourceid="assignmentDataSource"
        autogeneratecolumns="false" datakeynames="ID"
        allowpaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="SemesterID"
                DataNavigateUrlFormatString="semester.aspx?ID={0}"
                DataTextField="SemesterName"
                HeaderText="Semester"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="CourseID"
                DataNavigateUrlFormatString="course.aspx?ID={0}"
                DataTextField="CourseName"
                HeaderText="Course"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="assignment.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="Name"/>
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
    <div class="toolbar">
        <asp:LinkButton runat="server" ID="Delete" Text="Delete" OnClick="Delete_Click" 
            OnClientClick="return confirm('Are you sure you want to delete the selected items?')"
            ValidationGroup="Delete" />
    </div>
</asp:Content>
