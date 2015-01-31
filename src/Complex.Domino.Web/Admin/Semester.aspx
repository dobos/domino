<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Semester.aspx.cs" Inherits="Complex.Domino.Web.Admin.Semester" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Semester</h1>
    <domino:entityform runat="server" id="entityForm" />
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="StartDateLabel" CssClass="required">Start date:</asp:Label>
                </td>
                <td class="field">
                    <asp:TextBox ID="StartDate" runat="server"></asp:TextBox>
                </td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="EndDateLabel" CssClass="required">End date:</asp:Label>
                </td>
                <td class="field">
                    <asp:TextBox ID="EndDate" runat="server"></asp:TextBox>
                </td>
                <td class="error"></td>
            </tr>
        </table>
    </div>
    <toolbar class="form">
        <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click">
            <asp:Image runat="server" SkinID="OkButton" />
            <p>Ok</p>
        </asp:LinkButton>
        <asp:LinkButton runat="Server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="False">
            <asp:Image runat="server" SkinID="CancelButton" />
            <p>Cancel</p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
