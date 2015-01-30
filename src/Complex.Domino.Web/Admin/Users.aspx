<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Complex.Domino.Web.Admin.Users" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All users</h1>
    <toolbar>
        <asp:HyperLink runat="server" ID="ToolbarCreate">
            <asp:Image runat="server" SkinID="NewUserButton" />
            <p>Create User</p>
        </asp:HyperLink>
        <asp:HyperLink runat="server" ID="ToolbarImport">
            <asp:Image runat="server" SkinID="ImportButton" />
            <p>Import</p>
        </asp:HyperLink>
    </toolbar>
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
        allowPaging="true"  AllowSorting="true"
        pagersettings-mode="NumericFirstLast" pagesize="25"
        CssClass="grid">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="User.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="User name"
                SortExpression="Name "/>
            <asp:BoundField HeaderText="Name" DataField="Description" SortExpression="Description" />
            <asp:BoundField HeaderText="E-mail" DataField="Email" SortExpression="Email" />
            <asp:CheckBoxField HeaderText="Visible" DataField="Visible" />
            <asp:CheckBoxField HeaderText="Enabled" DataField="Enabled" />
        </Columns>
        <EmptyDataTemplate>
            <p>No users match the query.</p>
        </EmptyDataTemplate>
    </domino:multiselectgridview>
    <toolbar>
        <asp:LinkButton runat="server" ID="Delete" OnClick="Delete_Click" 
            OnClientClick="return confirm('Are you sure you want to delete the selected items?')"
            ValidationGroup="Delete">
            <asp:Image runat="server" SkinID="DeleteButton" />
            <p>Delete</p>
        </asp:LinkButton>
        <asp:LinkButton runat="server">
            <asp:Image runat="server" SkinID="SignInButton" />
            <p>Generate password</p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
