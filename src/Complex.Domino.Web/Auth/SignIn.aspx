<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Complex.Domino.Web.Auth.SignIn" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" DefaultButton="Ok">
        <h1><asp:Label runat="server" Text="<%$ Resources:Labels, SignInFormLabel %>" /></h1>
        <asp:Panel runat="server" ID="signInIntroPanel">
            <p><asp:Literal runat="server" Text="<%$ Resources:Labels, SignInIntro %>" /></p>
        </asp:Panel>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="UsernameLabel" Text="<%$ Resources:Labels, UserName %>" />:
                    </td>
                    <td class="field">
                        <asp:TextBox ID="Username" runat="server" />
                    </td>
                    <td class="error">
                        <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<%$ Resources:Errors, Required %>" ControlToValidate="Username" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordLabel" Text="<%$ Resources:Labels, Password %>" />
                    </td>
                    <td class="field">
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" />
                    </td>
                    <td class="error">
                        <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<%$ Resources:Errors, Required %>" ControlToValidate="Password" />
                        <asp:CustomValidator ID="PasswordValidator" runat="server" Display="Dynamic"
                            ControlToValidate="Password"
                            ErrorMessage="<%$ Resources:Errors, InvalidPassword %>"
                            OnServerValidate="PasswordValidator_ServerValidate" />
                    </td>
                </tr>
                <tr>
                    <td class="label">&nbsp;
                    </td>
                    <td class="field">
                        <asp:CheckBox ID="Remember" runat="server" Text="<%$ Resources:Labels, RememberMe %>" />
                    </td>
                    <td class="error"></td>
                </tr>
            </table>
            <asp:Panel runat="server" ID="signInDetailsPanel">
                <ul>
                    <li>
                        <asp:HyperLink runat="server" ID="ResetLink" Text="<%$ Resources:Labels, PasswordReset %>" />
                    </li>
                </ul>
            </asp:Panel>
        </div>
        <toolbar class="form">
            <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click">
                <asp:Image runat="server" SkinID="SignInButton" />
                    <p><asp:Label runat="server" Text="<%$ Resources:Labels, SignIn %>" /></p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
