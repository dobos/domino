<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Complex.Domino.Web.Auth.ChangePassword" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" Text="<%$ Resources:Labels, ChangePassword %>" /></h1>
    <asp:Panel runat="server" DefaultButton="Ok" ID="formPanel">
        <asp:Panel runat="server" ID="changeIntroPanel">
            <p>
                <asp:Literal runat="server" Text="<%$ Resources:Labels, ChangePasswordIntro %>" />
            </p>
        </asp:Panel>
        <div class="frame">
            <table class="form">
                <tr runat="server" id="OldPasswordRow" class="required">
                    <td class="label">
                        <asp:Label runat="server" ID="OldPasswordLabel" Text="<%$ Resources:Labels, OldPassword %>" />:
                    </td>
                    <td class="field">
                        <asp:TextBox ID="OldPassword" runat="server" TextMode="Password" />
                    </td>
                    <td class="error">
                        <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<%$ Resources:Errors, Required %>" ControlToValidate="OldPassword" />
                        <asp:CustomValidator ID="PasswordValidator" runat="server" Display="Dynamic"
                            ControlToValidate="OldPassword"
                            ErrorMessage="<%$ Resources:Errors, InvalidPassword %>"
                            OnServerValidate="PasswordValidator_ServerValidate" />
                    </td>
                </tr>
                <tr class="required">
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordNewLabel" Text="<%$ Resources:Labels, NewPassword %>" />:
                    </td>
                    <td class="field">
                        <asp:TextBox runat="server" ID="PasswordNew" TextMode="Password" />
                    </td>
                    <td class="error">
                        <asp:RequiredFieldValidator ID="PasswordNewRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<%$ Resources:Errors, Required %>" ControlToValidate="PasswordNew" />
                        <asp:CustomValidator ID="PasswordComplexityValidator" runat="server" Display="Dynamic"
                            ControlToValidate="PasswordNew"
                            ErrorMessage="<%$ Resources:Errors, InvalidFormat %>"
                            OnServerValidate="PasswordComplexityValidator_ServerValidate" />
                    </td>
                </tr>
                <tr class="required">
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordConfirmLabel" Text="<%$ Resources:Labels, Confirmation %>" />:
                    </td>
                    <td class="field">
                        <asp:TextBox runat="server" ID="PasswordConfirm" TextMode="Password" />
                    </td>
                    <td class="error">
                        <asp:RequiredFieldValidator ID="PasswordConfirmRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<%$ Resources:Errors, Required %>" ControlToValidate="PasswordConfirm" />
                        <asp:CustomValidator runat="server" ID="PasswordConfirmValidator" Display="Dynamic"
                            ErrorMessage="<%$ Resources:Errors, ConfirmationNoMatch %>" ControlToValidate="PasswordConfirm"
                            OnServerValidate="PasswordConfirmValidator_ServerValidate" />
                    </td>
                </tr>
            </table>
        </div>
        <toolbar class="form">
            <asp:LinkButton runat="server" ID="Ok" OnClick="Ok_Click">
                <asp:Image runat="server" SkinID="OkButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="false">
                <asp:Image runat="server" SkinID="CancelButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Cancel %>" /></p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
    <asp:Panel runat="server" ID="messagePanel" Visible="false">
        <div class="frame">
            <asp:Label runat="server" Text="<%$ Resources:Labels, ChangePasswordSuccess %>" />
        </div>
        <toolbar class="form">
            <asp:LinkButton runat="server" OnClick="Cancel_Click">
                <asp:Image runat="server" SkinID="OkButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
