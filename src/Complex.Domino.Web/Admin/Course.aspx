<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="Complex.Domino.Web.Admin.Course" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>New semester</h1>
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="SemesterLabel">Semester:</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="Semester" DataValueField="ID" DataTextField="Name" AppendDataBoundItems="true">
                    <asp:ListItem Value="-1" Text="(select semester)" />
                </asp:DropDownList>
                <asp:RangeValidator runat="server" ControlToValidate="Semester"
                    Display="Dynamic" MinimumValue="1" MaximumValue="2147483647"
                    ErrorMessage="<br />Select a semester" Type="Integer" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="NameLabel" CssClass="required">Name:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />Name is required" ControlToValidate="Name" />
            </td>
        </tr>
        <tr>
            <td class="label">&nbsp;</td>
            <td class="field">
                <asp:CheckBox ID="Enabled" runat="server" Text="Enabled" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="Visible" runat="server" Text="Visible" />
            </td>
        </tr>
    </table>
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="StartDateLabel" CssClass="required">Start date:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="StartDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="EndDateLabel" CssClass="required">End date:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="EndDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="UrlLabel">URL:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Url" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="GradeTypeLabel">Grade type:</asp:Label>
            </td>
            <td class="field">
                <asp:DropDownList runat="server" ID="GradeType">
                    <asp:ListItem Value="Unknown" Text="(select grade type)" />
                    <asp:ListItem Value="Signature" Text="Signature" />
                    <asp:ListItem Value="Grade" Text="Grade" />
                    <asp:ListItem Value="Points" Text="Points" />
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table class="form">
        <tfoot>
            <tr>
                <td colspan="2">
                    <asp:Button runat="Server" ID="Ok" Text="OK" OnClick="Ok_Click" />
                    <asp:Button runat="Server" ID="Cancel" Text="Cancel" OnClick="Cancel_Click" CausesValidation="False" />
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
