<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Complex.Domino.Web.Auth.SignIn" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Sign in to Domino</h1>
    <asp:Panel runat="server" ID="signInIntroPanel">
        <p>
            To start using the services, please sign in with your existing credentials.
        </p>
    </asp:Panel>
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="UsernameLabel">User name:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Username" runat="server" CssClass="FormField"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />Username is required" ControlToValidate="Username" />
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
                <asp:Label runat="server" ID="PasswordLabel">Password:</asp:Label>
            </td>
            <td class="FormField">
                <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="FormField"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />Password is required" ControlToValidate="Password" />
                <asp:CustomValidator ID="PasswordValidator" runat="server" Display="Dynamic" ErrorMessage="<br />Invalid User name or Password"
                    OnServerValidate="PasswordValidator_ServerValidate" />
            </td>
        </tr>
        <tr>
            <td class="FormLabel">&nbsp;
            </td>
            <td class="FormField">
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
    <asp:Button runat="Server" ID="Ok" Text="Sign in" CssClass="FormButton" OnClick="Ok_Click" />
</asp:Content>
