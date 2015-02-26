<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PluginList.ascx.cs" Inherits="Complex.Domino.Web.Teacher.PluginList" %>
<div class="frame">
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" Text="<%$ Resources:Labels, AddPlugin %>" />:
            </td>
            <td class="field">
                <asp:DropDownList runat="server" ID="PluginType" ValidationGroup="AddPlugin" OnSelectedIndexChanged="PluginType_SelectedIndexChanged" AutoPostBack="true" />
            </td>
            <td class="error">
            </td>
        </tr>
    </table>
    <asp:ListView runat="server" ID="plugins" OnItemCreated="Plugins_ItemCreated" >
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </LayoutTemplate>
        <ItemTemplate>
            <div class="frame">
                <%# Eval("Description") %>
                remove
                <asp:PlaceHolder runat="server" ID="pluginControlPlaceholder" />
            </div>
        </ItemTemplate>
    </asp:ListView>
</div>
