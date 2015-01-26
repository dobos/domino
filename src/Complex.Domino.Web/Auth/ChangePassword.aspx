<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Complex.Domino.Web.Auth.ChangePassword" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" DefaultButton="Ok">
        <h1>Change your password</h1>
        <div class="frame">
            <table class="form">
                <tr runat="server" id="OldPasswordRow">
                    <td class="label">
                        <asp:Label runat="server" ID="OldPasswordLabel">Old password:</asp:Label>
                    </td>
                    <td class="field">
                        <asp:TextBox ID="OldPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<br />Password is required" ControlToValidate="OldPassword" />
                        <asp:CustomValidator ID="PasswordValidator" runat="server" Display="Dynamic"
                            ControlToValidate="OldPassword"
                            ErrorMessage="<br />Invalid User name or Password"
                            OnServerValidate="PasswordValidator_ServerValidate" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordNewLabel" Text="New password:" /></td>
                    <td class="field">
                        <asp:TextBox runat="server" ID="PasswordNew" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="PasswordNewRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<br />Password is required" ControlToValidate="PasswordNew" />
                        <asp:CustomValidator ID="PasswordComplexityValidator" runat="server" Display="Dynamic"
                            ControlToValidate="PasswordNew"
                            ErrorMessage="<br />Password must contain lowercase and uppercase letter, numerical digits and must be longer than 8 characters"
                            OnServerValidate="PasswordComplexityValidator_ServerValidate" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordConfirmLabel">Confirmation:</asp:Label></td>
                    <td class="field">
                        <asp:TextBox runat="server" ID="PasswordConfirm" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                            ErrorMessage="<br />Password is required" ControlToValidate="PasswordNew" />
                        <asp:CustomValidator runat="server" ID="PasswordConfirmValidator" Display="Dynamic"
                            ErrorMessage="<br />Password and confirmation do not match" ControlToValidate="PasswordConfirm"
                            OnServerValidate="PasswordConfirmValidator_ServerValidate" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                            ErrorMessage="<br />Password is required" ControlToValidate="PasswordNew" />
                </tr>
            </table>
        </div>
        <toolbar class="form">
            <asp:LinkButton runat="server" ID="Ok" OnClick="Ok_Click">
                <asp:Image ID="Image3" runat="server" SkinID="OkButton" />
                <p>Ok</p>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="false">
                <asp:Image ID="Image4" runat="server" SkinID="CancelButton" />
                <p>Cancel</p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
