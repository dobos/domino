<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="Complex.Domino.Web.Admin.User" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>New user</h1>
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="NameLabel" CssClass="required">Name:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />Name is required" ControlToValidate="Name" />
            </td>
        </tr>
        <tr>
            <td class="label">&nbsp;</td>
            <td class="field">
                <asp:CheckBox ID="Enabled" runat="server" Text="Enabled" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="Visible" runat="server" Text="Visible" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="EmailLabel" CssClass="required">E-mail address:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />E-mail address is required" ControlToValidate="Email" />
                <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server" ControlToValidate="Email" Display="Dynamic" ErrorMessage="<br />Invalid format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="UsernameLabel" CssClass="required">User name:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Username" runat="server" CssClass="FormField"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />User name is required" ControlToValidate="Username" />
            </td>
        </tr>
    </table>
    <table class="form">
        <tr>
            <td class="label"><asp:Label runat="server" ID="PasswordLabel">Password:</asp:Label></td>
            <td class="field"><asp:TextBox runat="server" ID="Password" /></td>
        </tr>
        <tr>
            <td class="label"><asp:Label runat="server" ID="PasswordConfirmLabel">Confirmation:</asp:Label></td>
            <td class="field"><asp:TextBox runat="server" ID="PasswordConfirm" /></td>
        </tr>
    </table>
    <h2>User roles</h2>
    <domino:MultiSelectGridView runat="server" id="userRoleList">
        <Columns>
            <asp:BoundField HeaderText="Role name"/>
        </Columns>
        <EmptyDataTemplate>
            <p>User has no roles yet.</p>
        </EmptyDataTemplate>
    </domino:MultiSelectGridView>
    <table class="form">
        <tfoot>
            <tr>
                <td colspan="2">
                    <asp:Button runat="Server" ID="Ok" Text="OK" OnClick="Ok_Click" />
                    <asp:Button runat="Server" ID="Cancel" Text="Cancel" OnClick="Cancel_Click" CausesValidation="False" />
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
