<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PluginList.ascx.cs" Inherits="Complex.Domino.Web.Teacher.PluginList" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="addPluginPanel" Visible="false">
            <div class="frameHeader">
                <asp:Label runat="server" Text="<%$ Resources:Labels, Plugins %>" />
            </div>
            <div class="frameBody">
                <table class="form">
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" Text="<%$ Resources:Labels, AddPlugin %>" />:
                        </td>
                        <td class="field">
                            <asp:DropDownList runat="server" ID="PluginType" ValidationGroup="AddPlugin" OnSelectedIndexChanged="PluginType_SelectedIndexChanged" AutoPostBack="true" />
                        </td>
                        <td class="error"></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:ListView runat="server" ID="plugins" OnItemCreated="Plugins_ItemCreated" OnItemDeleting="Plugins_ItemDeleting" DataKeyNames="ID">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </LayoutTemplate>
            <ItemTemplate>
                <div class="frameHeader">
                    <asp:Label runat="server" Text="<%$ Resources:Labels, Plugin %>" />:
            <asp:Label runat="server" ID="description" />
                    <asp:LinkButton runat="server" ID="delete" Text="<%$ Resources:Labels, Delete %>" CommandName="delete" />
                </div>
                <div class="frameBody">
                    <asp:PlaceHolder runat="server" ID="pluginControlPlaceholder" />
                </div>
            </ItemTemplate>
        </asp:ListView>
    </ContentTemplate>
</asp:UpdatePanel>
