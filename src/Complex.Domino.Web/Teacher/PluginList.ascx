<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PluginList.ascx.cs" Inherits="Complex.Domino.Web.Teacher.PluginList" %>
<div class="frame">
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" Text="<%$ Resources:Labels, Plugin %>" />:
            </td>
            <td class="field">
                <asp:DropDownList runat="server" ID="PluginType" ValidationGroup="AddPlugin" />
            </td>
            <td class="error">
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <toolbar class="right">
                    <asp:LinkButton runat="Server" ID="AddPlugin" OnClick="AddPlugin_Click" ValidationGroup="AddPlugin">
                        <asp:Image runat="server" SkinID="OkButton" />
                        <p><asp:Label runat="server" Text="<%$ Resources:Labels, AddPlugin %>" /></p>
                    </asp:LinkButton>
                </toolbar>
            </td>
            <td></td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="pluginsPlaceholder" />
</div>
