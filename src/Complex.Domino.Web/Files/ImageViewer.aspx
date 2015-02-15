<%@ Page Title="" Language="C#" MasterPageFile="~/Files/Files.master" AutoEventWireup="true" CodeBehind="ImageViewer.aspx.cs" Inherits="Complex.Domino.Web.Files.ImageViewer"
    ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <table style="height: 100%; width: 100%;">
        <tr>
            <td style="width: 100%; height: 100%; text-align: center; vertical-align: middle;">
                <asp:Image runat="server" ID="ImageView" />
            </td>
        </tr>
    </table>
</asp:Content>
