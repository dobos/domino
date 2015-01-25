<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Complex.Domino.Web.Auth.User" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Update your user account</h1>
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="NameLabel" Text="User name:" />
                </td>
                <td class="field">
                    <asp:Label ID="Name" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="DescriptionLabel" Text="Name:" />
                </td>
                <td class="field">
                    <asp:Label ID="Description" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="EmailLabel" CssClass="required" Text="E-mail address:" />
                </td>
                <td class="field">
                    <asp:TextBox ID="Email" runat="server" ValidationGroup="User" />
                    <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server" Display="Dynamic"
                        ErrorMessage="<br />E-mail address is required" ControlToValidate="Email" />
                    <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server"
                        ControlToValidate="Email" Display="Dynamic" ErrorMessage="<br />Invalid format"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                </td>
            </tr>
        </table>
    </div>
    <toolbar class="form">
        <asp:LinkButton runat="server" ID="OK" OnClick="Ok_Click" ValidationGroup="User">
            <asp:Image ID="Image1" runat="server" SkinID="OkButton" />
            <p>Ok</p>
        </asp:LinkButton>
        <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" ValidationGroup="User" CausesValidation="false">
            <asp:Image ID="Image2" runat="server" SkinID="CancelButton" />
            <p>Cancel</p>
        </asp:LinkButton>
    </toolbar>
    <h1>Change your password</h1>
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="PasswordLabel">Password:</asp:Label>
                </td>
                <td class="field">
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" ValidationGroup="Password" />
                    <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" Display="Dynamic"
                        ErrorMessage="<br />Password is required" ControlToValidate="Password" ValidationGroup="Password" />
                    <asp:CustomValidator ID="PasswordValidator" runat="server" Display="Dynamic"
                        ControlToValidate="Password"
                        ErrorMessage="<br />Invalid User name or Password"
                        OnServerValidate="PasswordValidator_ServerValidate" ValidationGroup="Password" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="PasswordNewLabel" Text="New password:" /></td>
                <td class="field">
                    <asp:TextBox runat="server" ID="PasswordNew" TextMode="Password" ValidationGroup="Password" />
                    <asp:RequiredFieldValidator ID="PasswordNewRequiredValidator" runat="server" Display="Dynamic"
                        ErrorMessage="<br />Password is required" ControlToValidate="PasswordNew" ValidationGroup="Password" />
                    <asp:CustomValidator ID="PasswordComplexityValidator" runat="server" Display="Dynamic"
                        ControlToValidate="PasswordNew"
                        ErrorMessage="<br />Password must contain lowercase and uppercase letter, numerical digits and must be longer than 8 characters"
                        OnServerValidate="PasswordComplexityValidator_ServerValidate"
                        ValidationGroup="Password" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="PasswordConfirmLabel">Confirmation:</asp:Label></td>
                <td class="field">
                    <asp:TextBox runat="server" ID="PasswordConfirm" TextMode="Password" ValidationGroup="Password" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                        ErrorMessage="<br />Password is required" ControlToValidate="PasswordNew" ValidationGroup="Password" />
                    <asp:CustomValidator runat="server" ID="PasswordConfirmValidator" Display="Dynamic"
                        ErrorMessage="<br />Password and confirmation do not match" ControlToValidate="PasswordConfirm"
                        OnServerValidate="PasswordConfurmValidator_ServerValidate"
                        ValidationGroup="Password" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                        ErrorMessage="<br />Password is required" ControlToValidate="PasswordNew" ValidationGroup="Password" />
            </tr>
        </table>
    </div>
    <toolbar class="form">
        <asp:LinkButton runat="server" ID="OkPassword" OnClick="OkPassword_Click" ValidationGroup="Password">
            <asp:Image ID="Image3" runat="server" SkinID="OkButton" />
            <p>Ok</p>
        </asp:LinkButton>
        <asp:LinkButton runat="server" ID="CancelPassword" OnClick="Cancel_Click" ValidationGroup="Password" CausesValidation="false">
            <asp:Image ID="Image4" runat="server" SkinID="CancelButton" />
            <p>Cancel</p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
