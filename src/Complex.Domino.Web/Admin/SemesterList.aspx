<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="SemesterList.aspx.cs" Inherits="Complex.Domino.Web.Admin.SemesterList" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All semesters</h1>
    <div class="toolbar">
        <asp:HyperLink runat="server" ID="ToolbarCreate" Text="Create Semester" />
    </div>
    <asp:ObjectDataSource runat="server" ID="semesterDataSource" DataObjectTypeName="Complex.Domino.Lib.Semester"
        OnObjectCreating="semesterDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.SemesterFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <domino:MultiSelectGridView id="semesterList" runat="server" datasourceid="semesterDataSource"
        autogeneratecolumns="false" datakeynames="ID"
        allowPaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="Visible" DataField="Visible" />
            <asp:BoundField HeaderText="Enabled" DataField="Enabled" />
            <asp:BoundField HeaderText="Start date" DataField="StartDate" />
            <asp:BoundField HeaderText="End date" DataField="EndDate" />
        </Columns>
        <EmptyDataTemplate>
            <p>No semesters match the query.</p>
        </EmptyDataTemplate>
    </domino:MultiSelectGridView>
</asp:Content>
