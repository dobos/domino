<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Semesters.aspx.cs" Inherits="Complex.Domino.Web.Admin.Semesters" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All semesters</h1>
    <toolbar>
        <asp:HyperLink runat="server" ID="ToolbarCreate">
            <asp:Image runat="server" SkinID="NewSemesterButton" />
            <p>Create Semester</p>
        </asp:HyperLink>
    </toolbar>
    <asp:ObjectDataSource runat="server" ID="semesterDataSource" DataObjectTypeName="Complex.Domino.Lib.Semester"
        OnObjectCreating="semesterDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.SemesterFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        SortParameterName="orderBy"
        EnablePaging="true" />
    <domino:multiselectgridview id="semesterList" runat="server" datasourceid="semesterDataSource"
        autogeneratecolumns="false" datakeynames="ID" CssClass="grid"
        allowpaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="Semester.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="Name"
                SortExpression="Name"/>
            <asp:BoundField HeaderText="Start date" DataField="StartDate" />
            <asp:BoundField HeaderText="End date" DataField="EndDate" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="courses.aspx?SemesterID={0}"
                Text="courses"
                HeaderText="Courses"/>
            <asp:CheckBoxField HeaderText="Visible" DataField="Visible" />
            <asp:CheckBoxField HeaderText="Enabled" DataField="Enabled" />
        </Columns>
        <EmptyDataTemplate>
            <p>No semesters match the query.</p>
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
