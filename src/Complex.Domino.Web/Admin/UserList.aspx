<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Complex.Domino.Web.Admin.UserList" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All users</h1>
    <asp:ObjectDataSource runat="server" ID="userDataSource" DataObjectTypeName="Complex.Domino.Lib.User"
        OnObjectCreating="userDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.UserFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true"
        />
    <domino:MultiSelectGridView id="userList" runat="server" datasourceid="userDataSource"
        autogeneratecolumns="false" datakeynames="ID"
        EnablePaging="true" PagerSettings-Mode="NumericFirstLast" PageSize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="V" DataField="Visible" />
            <asp:BoundField HeaderText="E" DataField="Enabled" />
            <asp:BoundField HeaderText="User name" DataField="UserName" />
            <asp:BoundField HeaderText="E-mail" DataField="Email" />
        </Columns>
    </domino:MultiSelectGridView>
</asp:Content>
