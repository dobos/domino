<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Assignments.aspx.cs" Inherits="Complex.Domino.Web.Admin.Assignments" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, Assignments %>" /></h1>
    <toolbar>
        <asp:HyperLink runat="server" ID="ToolbarCreate">
            <asp:Image runat="server" SkinID="NewAssignmentButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, NewAssignment %>" /></p>
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
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="assignment.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="<%$ Resources:Labels, Name %>"
                SortExpression="Name"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="CourseID"
                DataNavigateUrlFormatString="course.aspx?ID={0}"
                DataTextField="CourseName"
                HeaderText="<%$ Resources:Labels, Course %>"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="SemesterID"
                DataNavigateUrlFormatString="semester.aspx?ID={0}"
                DataTextField="SemesterName"
                HeaderText="<%$ Resources:Labels, Semester %>"
                SortExpression="SemesterName"/>
            <asp:BoundField HeaderText="<%$ Resources:Labels, GradeType %>" DataField="GradeType" />
            <asp:BoundField HeaderText="<%$ Resources:Labels, GradeWeight %>" DataField="GradeWeight" />
            <asp:HyperLinkField
                DataNavigateUrlFields="Url"
                DataNavigateUrlFormatString="{0}"
                Text="<%$ Resources:Labels, WebPage %>"
                HeaderText="<%$ Resources:Labels, WebPage %>"/>
            <asp:CheckBoxField HeaderText="<%$ Resources:Labels, Visible %>" DataField="Visible" />
            <asp:CheckBoxField HeaderText="<%$ Resources:Labels, Enabled %>" DataField="Enabled" />
        </Columns>
        <EmptyDataTemplate>
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, NoAssignments %>" /></p>
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
