<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Complex.Domino.Web.Admin.Users" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" ID="TitleLabel" Text="<%$ Resources:Labels, Users %>" /></h1>
    <toolbar>
        <asp:HyperLink runat="server" ID="ToolbarCreate">
            <asp:Image runat="server" SkinID="NewUserButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, NewUser %>" /></p>
        </asp:HyperLink>
        <asp:HyperLink runat="server" ID="ToolbarImport">
            <asp:Image runat="server" SkinID="ImportButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Import %>" /></p>
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
        allowpaging="true" allowsorting="true"
        pagersettings-mode="NumericFirstLast" pagesize="25"
        cssclass="grid">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="User.aspx?ID={0}"
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
    <toolbar>
        <asp:LinkButton runat="server" ID="Delete" OnClick="Delete_Click" 
            ValidationGroup="Delete">
            <asp:Image runat="server" SkinID="DeleteButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Delete %>" /></p>
        </asp:LinkButton>
        <asp:LinkButton runat="server">
            <asp:Image runat="server" SkinID="SignInButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, GeneratePassword %>" /></p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
