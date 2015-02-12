<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Auth.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Complex.Domino.Web.Auth.User" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" Text="<%$ Resources:Labels, UpdateUserAccount %>" /></h1>
    <asp:Panel runat="server" DefaultButton="Ok" ID="formPanel">
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="NameLabel" Text="<%$ Resources:Labels, UserName %>" />:
                    </td>
                    <td class="field">
                        <asp:Label ID="Name" runat="server" />
                    </td>
                    <td class="error"></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="DescriptionLabel" Text="<%$ Resources:Labels, Name %>" />:
                    </td>
                    <td class="field">
                        <asp:Label ID="Description" runat="server" />
                    </td>
                    <td class="error"></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="EmailLabel" CssClass="required" Text="<%$ Resources:Labels, Email %>" />
                    </td>
                    <td class="field">
                        <asp:TextBox ID="Email" runat="server" ValidationGroup="User" />
                    </td>
                    <td class="error">
                        <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<%$ Resources:Errors, Required %>" ControlToValidate="Email" />
                        <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server"
                            ControlToValidate="Email" Display="Dynamic" ErrorMessage="<%$ Resources:Errors, InvalidFormat %>"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </td>
                </tr>
            </table>
        </div>
        <toolbar class="form">
            <asp:LinkButton runat="server" ID="OK" OnClick="Ok_Click">
                <asp:Image runat="server" SkinID="OkButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="false">
                <asp:Image runat="server" SkinID="CancelButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Cancel %>" /></p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
    <asp:Panel runat="server" DefaultButton="Ok" ID="messagePanel" Visible="false">
        <div class="frame">
            <asp:Label runat="server" Text="<%$ Resources:Labels, UpdateUserAccountSuccess %>" />
        </div>
        <toolbar class="form">
            <asp:LinkButton runat="server" OnClick="Cancel_Click">
                <asp:Image runat="server" SkinID="OkButton" />
                <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
