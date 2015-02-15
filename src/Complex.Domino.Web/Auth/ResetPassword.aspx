<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Complex.Domino.Web.Auth.ResetPassword" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, ResetPassword %>" /></h1>
    <asp:Panel runat="server" ID="resetPanel" DefaultButton="Ok">
        <asp:Panel runat="server" ID="resetIntroPanel">
            <p><asp:Literal runat="server" Text="<%$ Resources:Labels, ResetPasswordIntro %>" /></p>
        </asp:Panel>
        <div class="frame">
            <table class="form">
                <tr class="required">
                    <td class="label">
                        <asp:Label runat="server" ID="EmailLabel" Text="<%$ Resources:Labels, Email %>" />:
                    </td>
                    <td class="field">
                        <asp:TextBox ID="Email" runat="server" />
                    </td>
                    <td class="error">
                        <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<%$ Resources:Errors, Required %>" ControlToValidate="Email" />
                        <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server"
                            ControlToValidate="Email" Display="Dynamic" ErrorMessage="<%$ Resources:Errors, InvalidFormat %>"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                        <asp:CustomValidator ID="EmailValidator" runat="server"
                            ControlToValidate="Email" Display="Dynamic" ErrorMessage="<%$ Resources:Errors, UnknownEmail %>"
                            OnServerValidate="EmailValidator_ServerValidate" />
                    </td>
                </tr>
            </table>
        </div>
        <toolbar class="form">
        <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click">
            <asp:Image runat="server" SkinID="OkButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
        </asp:LinkButton>
    </toolbar>
    </asp:Panel>
    <asp:Panel runat="server" ID="messagePanel" Visible="false">
        <p><asp:Literal runat="server" Text="<%$ Resources:Labels, ResetPasswordResults %>" /></p>
    </asp:Panel>
</asp:Content>
