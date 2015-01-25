<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="Reset.aspx.cs" Inherits="Complex.Domino.Web.Auth.Reset" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Sign in to Domino</h1>
    <asp:Panel runat="server" ID="resetIntroPanel">
        <p>
            To reset your password, please enter your e-mail address.
        </p>
    </asp:Panel>
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="UsernameLabel" Text="E-mail address:" />
                </td>
                <td class="field">
                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server" Display="Dynamic"
                        ErrorMessage="<br />E-mail address is required" ControlToValidate="Email" ValidationGroup="User" />
                    <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server"
                        ControlToValidate="Email" Display="Dynamic" ErrorMessage="<br />Invalid format"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="User" />
                </td>
            </tr>
        </table>
    </div>
    <toolbar class="form">
        <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click">
            <asp:Image runat="server" SkinID="SignInButton" />
                <p>OK</p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
