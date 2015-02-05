<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Students" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" ID="TitleLabel" Text="<%$ Resources:Labels, Users %>" /></h1>
    <asp:ObjectDataSource runat="server" ID="userDataSource" DataObjectTypeName="Complex.Domino.Lib.User"
        OnObjectCreating="userDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.UserFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        SortParameterName="orderBy"
        EnablePaging="true" />
    <domino:multiselectgridview id="userList" runat="server" datasourceid="userDataSource"
        autogeneratecolumns="false" datakeynames="ID"
        allowpaging="true" allowsorting="true"
        pagersettings-mode="NumericFirstLast" pagesize="25"
        cssclass="grid">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="Student.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="<%$ Resources:Labels, UserName %>"
                SortExpression="Name"/>
            <asp:BoundField HeaderText="<%$ Resources:Labels, Name %>" DataField="Description" SortExpression="Description" />
            <asp:BoundField HeaderText="<%$ Resources:Labels, Email %>" DataField="Email" SortExpression="Email" />
            <asp:BoundField HeaderText="<%$ Resources:Labels, CreatedDate %>" DataField="CreatedDate" SortExpression="CreatedDate" />
            <asp:CheckBoxField HeaderText="<%$ Resources:Labels, Visible %>" DataField="Visible" />
            <asp:CheckBoxField HeaderText="<%$ Resources:Labels, Enabled %>" DataField="Enabled" />
        </Columns>
        <EmptyDataTemplate>
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, NoUsers %>" /></p>
        </EmptyDataTemplate>
    </domino:multiselectgridview>
</asp:Content>
