<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Semester.aspx.cs" Inherits="Complex.Domino.Web.Admin.Semester" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <domino:EntityForm runat="server" ID="entityForm" />
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
