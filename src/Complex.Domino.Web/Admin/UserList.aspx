<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Complex.Domino.Web.Admin.UserList" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All users</h1>
    <div class="toolbar">
        <asp:HyperLink runat="server" ID="ToolbarCreate" Text="Create User" />
    </div>
    <asp:ObjectDataSource runat="server" ID="userDataSource" DataObjectTypeName="Complex.Domino.Lib.User"
        OnObjectCreating="userDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.UserFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <domino:multiselectgridview id="userList" runat="server" datasourceid="userDataSource"
        autogeneratecolumns="false" datakeynames="ID"
        allowPaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="User.aspx?ID={0}"
                DataTextField="Username"
                HeaderText="User name"/>
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="V" DataField="Visible" />
            <asp:BoundField HeaderText="E" DataField="Enabled" />
            <asp:BoundField HeaderText="E-mail" DataField="Email" />
        </Columns>
        <EmptyDataTemplate>
            <p>No users match the query.</p>
        </EmptyDataTemplate>
    </domino:multiselectgridview>
</asp:Content>
