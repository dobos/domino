<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Complex.Domino.Web.Auth.ResetPassword" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Reset password</h1>
    <asp:Panel runat="server" ID="resetPanel">
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
                            ErrorMessage="<br />E-mail address is required" ControlToValidate="Email" />
                        <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server"
                            ControlToValidate="Email" Display="Dynamic" ErrorMessage="<br />Invalid format"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                        <asp:CustomValidator ID="EmailValidator" runat="server"
                            ControlToValidate="Email" Display="Dynamic" ErrorMessage="<br />Unknown e-mail"
                            OnServerValidate="EmailValidator_ServerValidate" />
                    </td>
                </tr>
            </table>
        </div>
        <toolbar class="form">
        <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click">
            <asp:Image runat="server" SkinID="OkButton" />
                <p>OK</p>
        </asp:LinkButton>
    </toolbar>
    </asp:Panel>
    <asp:Panel runat="server" ID="messagePanel" Visible="false">
        <p>An e-mail with a password reset link has been sent to your e-mail address.</p>
    </asp:Panel>
</asp:Content>
