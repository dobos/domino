<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Semester.aspx.cs" Inherits="Complex.Domino.Web.Admin.Semester" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" ID="TitleLabel" /></h1>
    <div class="frame">
        <domino:entityform runat="server" id="entityForm" />
    </div>
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="StartDateLabel" Text="<%$ Resources:Labels, StartDate %>" />:
                </td>
                <td class="field">
                    <asp:TextBox ID="StartDate" runat="server" />
                </td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="EndDateLabel" Text="<%$ Resources:Labels, EndDate %>" />:
                </td>
                <td class="field">
                    <asp:TextBox ID="EndDate" runat="server" />
                </td>
                <td class="error"></td>
            </tr>
        </table>
    </div>
    <toolbar class="form">
        <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click" ValidationGroup="Entity">
            <asp:Image runat="server" SkinID="OkButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
        </asp:LinkButton>
        <asp:LinkButton runat="Server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="False">
            <asp:Image runat="server" SkinID="CancelButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Cancel %>" /></p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
