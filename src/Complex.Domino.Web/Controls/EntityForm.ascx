<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityForm.ascx.cs" Inherits="Complex.Domino.Web.Controls.EntityForm" %>
<h1>
    <asp:Label runat="server" ID="FormTitle" /></h1>
<div class="frame">
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="NameLabel" CssClass="required" Text="Name:" />
            </td>
            <td class="field">
                <asp:TextBox ID="Name" runat="server" MaxLength="50" ValidationGroup="Entity" />
                <asp:RequiredFieldValidator ID="NameRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />Name is required" ControlToValidate="Name" ValidationGroup="Entity" />
                <asp:RegularExpressionValidator ID="NameFormatValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />Invalid format" ControlToValidate="Name"
                    ValidationExpression="[a-zA-Z0-9\-_]+" ValidationGroup="Entity" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="DescriptionLabel" Text="Description:" />
            </td>
            <td class="field">
                <asp:TextBox ID="Description" runat="server" MaxLength="250" ValidationGroup="Entity" />
            </td>
        </tr>
        <tr>
            <td class="label">&nbsp;</td>
            <td class="field">
                <asp:CheckBox ID="Enabled" runat="server" Text="Enabled" ValidationGroup="Entity" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="Visible" runat="server" Text="Visible" ValidationGroup="Entity" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="CommentsLabel" Text="Comments:" />
            </td>
            <td class="field">
                <asp:TextBox runat="server" ID="Comments" ValidationGroup="Entity" />
            </td>
        </tr>
    </table>
</div>
