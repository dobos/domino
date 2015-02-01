<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Semesters.aspx.cs" Inherits="Complex.Domino.Web.Admin.Semesters" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, Semesters %>" /></h1>
    <toolbar>
        <asp:HyperLink runat="server" ID="ToolbarCreate">
            <asp:Image runat="server" SkinID="NewSemesterButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, NewSemester %>" /></p>
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
                HeaderText="<%$ Resources:Labels, Name %>"
                SortExpression="Name"/>
            <asp:BoundField HeaderText="<%$ Resources:Labels, StartDate %>" DataField="StartDate" />
            <asp:BoundField HeaderText="<%$ Resources:Labels, EndDate %>" DataField="EndDate" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="courses.aspx?SemesterID={0}"
                Text="<%$ Resources:Labels, Courses %>"
                HeaderText="<%$ Resources:Labels, Courses %>"/>
            <asp:CheckBoxField HeaderText="<%$ Resources:Labels, Visible %>" DataField="Visible" />
            <asp:CheckBoxField HeaderText="<%$ Resources:Labels, Enabled %>" DataField="Enabled" />
        </Columns>
        <EmptyDataTemplate>
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, NoSemesters %>" /></p>
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
