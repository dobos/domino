<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Complex.Domino.Web.Admin.Users" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All users</h1>
    <div class="toolbar">
        <asp:HyperLink runat="server" ID="ToolbarCreate" Text="Create User" />
        <asp:HyperLink runat="server" ID="ToolbarImport" Text="Import" />
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
                DataTextField="Name"
                HeaderText="User name"/>
            <asp:BoundField HeaderText="Name" DataField="Description" />
            <asp:BoundField HeaderText="Visible" DataField="Visible" />
            <asp:BoundField HeaderText="Enabled" DataField="Enabled" />
            <asp:BoundField HeaderText="E-mail" DataField="Email" />
        </Columns>
        <EmptyDataTemplate>
            <p>No users match the query.</p>
        </EmptyDataTemplate>
    </domino:multiselectgridview>
    <div class="toolbar">
        <asp:LinkButton runat="server" ID="Delete" Text="Delete" OnClick="Delete_Click" 
            OnClientClick="return confirm('Are you sure you want to delete the selected items?')"
            ValidationGroup="Delete" />
        <asp:LinkButton runat="server" Text="Generate password" />
    </div>
</asp:Content>
