<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityForm.ascx.cs" Inherits="Complex.Domino.Web.Controls.EntityForm" %>
<table class="form" runat="server">
    <tr runat="server" id="NameRow">
        <td class="label">
            <asp:Label runat="server" ID="NameLabel" Text="<%$ Resources:Labels, Name %>" />:
        </td>
        <td class="field">
            <asp:TextBox ID="Name" runat="server" MaxLength="50" ValidationGroup="Entity" />
        </td>
        <td class="error">
            <asp:RequiredFieldValidator ID="NameRequiredValidator" runat="server" Display="Dynamic"
                ErrorMessage="<%$ Resources:Errors, Required %>" ControlToValidate="Name" ValidationGroup="Entity" />
            <asp:RegularExpressionValidator ID="NameFormatValidator" runat="server" Display="Dynamic"
                ErrorMessage="<%$ Resources:Errors, InvalidFormat %>" ControlToValidate="Name"
                ValidationExpression="[a-zA-Z0-9]{1}[a-zA-Z0-9\-\._]+" ValidationGroup="Entity" />
        </td>
    </tr>
    <tr runat="server" id="DescriptionRow">
        <td class="label">
            <asp:Label runat="server" ID="DescriptionLabel" Text="<%$ Resources:Labels, Description %>" />:
        </td>
        <td class="field">
            <asp:TextBox ID="Description" runat="server" MaxLength="250" ValidationGroup="Entity" />
        </td>
        <td class="error"></td>
    </tr>
    <tr runat="server" id="OptionsRow">
        <td class="label">&nbsp;</td>
        <td class="field">
            <asp:CheckBox ID="Enabled" runat="server" Text="<%$ Resources:Labels, Enabled %>" ValidationGroup="Entity" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="Visible" runat="server" Text="<%$ Resources:Labels, Visible %>" ValidationGroup="Entity" />
        </td>
    </tr>
    <tr runat="server" id="CommentsRow">
        <td class="label">
            <asp:Label runat="server" ID="CommentsLabel" Text="<%$ Resources:Labels, Comments %>" />:
        </td>
        <td class="field"></td>
    </tr>
    <tr runat="server" id="CommentsRow2">
        <td colspan="2" class="field">
            <asp:TextBox runat="server" ID="Comments" TextMode="MultiLine" ValidationGroup="Entity" />
        </td>
    </tr>
</table>
