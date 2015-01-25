<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Complex.Domino.Web.Auth.User" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Sign in to Domino</h1>
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="NameLabel" Text="User name:" />
            </td>
            <td class="field">
                <asp:TextBox ID="Name" runat="server" CausesValidation="false" ReadOnly="true" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="DescriptionLabel" Text="Name:" />
            </td>
            <td class="field">
                <asp:TextBox ID="Description" runat="server" CausesValidation="false" ReadOnly="true" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="EmailLabel" CssClass="required">E-mail address:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Email" runat="server" ValidationGroup="User" />
                <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />E-mail address is required" ControlToValidate="Email" ValidationGroup="User" />
                <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server" 
                    ControlToValidate="Email" Display="Dynamic" ErrorMessage="<br />Invalid format" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="User" />
            </td>
        </tr>
    </table>
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="PasswordLabel">Password:</asp:Label></td>
            <td class="field">
                <asp:TextBox runat="server" ID="Password" ValidationGroup="User" /></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="PasswordConfirmLabel">Confirmation:</asp:Label></td>
            <td class="field">
                <asp:TextBox runat="server" ID="PasswordConfirm" ValidationGroup="User" /></td>
        </tr>
    </table>
    <div class="toolbar">
        <asp:LinkButton runat="Server" ID="LinkButton1" Text="OK" OnClick="Ok_Click" ValidationGroup="User" />
        <asp:LinkButton runat="Server" ID="Cancel" Text="Cancel" OnClick="Cancel_Click" CausesValidation="False" />
    </div>
</asp:Content>
