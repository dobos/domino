<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Complex.Domino.Web.Auth.SignIn" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" DefaultButton="Ok">
        <h1>Sign in to Domino</h1>
        <asp:Panel runat="server" ID="signInIntroPanel">
            <p>
                To start using Domino, please sign in with your existing credentials.
            </p>
        </asp:Panel>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="UsernameLabel">User name:</asp:Label>
                    </td>
                    <td class="field">
                        <asp:TextBox ID="Username" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<br />Username is required" ControlToValidate="Username" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordLabel">Password:</asp:Label>
                    </td>
                    <td class="field">
                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<br />Password is required" ControlToValidate="Password" />
                        <asp:CustomValidator ID="PasswordValidator" runat="server" Display="Dynamic" ErrorMessage="<br />Invalid User name or Password"
                            OnServerValidate="PasswordValidator_ServerValidate" />
                    </td>
                </tr>
                <tr>
                    <td class="label">&nbsp;
                    </td>
                    <td class="field">
                        <asp:CheckBox ID="Remember" runat="server" Text="Remember me on this computer" />
                    </td>
                </tr>
            </table>
            <asp:Panel runat="server" ID="signInDetailsPanel">
                <ul>
                    <li>If you have forgotten you password,
                        <asp:HyperLink runat="server" ID="ResetLink">
                    request a password reset</asp:HyperLink>.</li>
                </ul>
            </asp:Panel>
        </div>
        <toolbar class="form">
            <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click">
                <asp:Image runat="server" SkinID="SignInButton" />
                    <p>Sign In</p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
