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
        <asp:LinkButton runat="server" ID="OK" OnClick="Ok_Click">
            <asp:Image ID="Image1" runat="server" SkinID="OkButton" />
            <p>Ok</p>
        </asp:LinkButton>
        <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="false">
            <asp:Image ID="Image2" runat="server" SkinID="CancelButton" />
            <p>Cancel</p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
