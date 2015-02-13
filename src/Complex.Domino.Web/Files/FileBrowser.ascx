<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileBrowser.ascx.cs" Inherits="Complex.Domino.Web.Files.FileBrowser" %>
<div class="frame">
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label ID="DirectoryLabel" runat="server" Text="<%$ Resources:Labels, CurrentDirectory %>" />:
            </td>
            <td class="field">
                <field style="width: 442px">
                    <asp:ListView runat="server" ID="directoryList" OnItemCreated="directoryList_ItemCreated"
                        OnItemCommand="directoryList_ItemCommand">
                        <LayoutTemplate>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </LayoutTemplate>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="name" />
                        </ItemTemplate>
                        <ItemSeparatorTemplate>
                            /
                        </ItemSeparatorTemplate>
                    </asp:ListView>
                </field>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="uploadPanel">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="UploadFileLabel" Text="<%$ Resources:Labels, UploadFile %>" />:
                </td>
                <td class="field" style="text-align: right">
                    <input type="file" runat="server" id="UploadedFile" />
                    <toolbar class="button" style="margin-left: 8px;">
                        <asp:LinkButton runat="server" ID="Upload" OnClick="Upload_Click">
                            <asp:Image runat="server" SkinID="UploadButton" />
                            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Upload %>" /></p>
                        </asp:LinkButton>
                    </toolbar>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <list>
        <scroll style="height:140px">
            <domino:multiselectlistview runat="server" id="fileList" datakeynames="Name"
                onitemcreated="fileList_ItemCreated" onitemcommand="fileList_ItemCommand"
                onitemdeleting="fileList_ItemDeleting" On>
                <layouttemplate>
                    <table>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                    </table>
                </layouttemplate>
                <itemtemplate>
                        <tr>
                            <td class="icon">
                                <asp:CheckBox runat="server" ID="selectionCheckbox" />
                                <asp:Image ID="icon" runat="server" SkinID="UnknownFileIcon" />
                            </td>
                            <td class="text">
                                <h1><asp:LinkButton ID="name" runat="server" CommandName="click" /></h1>
                                <p><asp:Label ID="size" runat="server" /></p>
                            </td>
                            <td class="buttons">
                                <asp:HyperLink runat="server" ID="view" Target="_blank">
                                    <asp:Image runat="server" SkinID="ViewButton" />
                                </asp:HyperLink>
                                <asp:LinkButton runat="server" ID="delete" CommandName="delete">
                                    <asp:Image runat="server" SkinID="DelsmlButton" />
                                </asp:LinkButton>
                            </td>
                        </tr>
                </itemtemplate>
                <EmptyItemTemplate>
                <asp:Label runat="server" Text="<%$ Resources:Labels, NoFiles %>" />
                </EmptyItemTemplate>
            </domino:multiselectlistview>
        </scroll>
    </list>

    <toolbar runat="server" class="right" ID="ButtonsPanel">
        <asp:LinkButton runat="server" ID="Download"
            OnClick="Download_Click">
            <asp:Image runat="server" SkinID="DownloadButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Download %>" /></p>
        </asp:LinkButton>
        <asp:LinkButton runat="server" ID="Delete" OnClick="Delete_Click">
            <asp:Image runat="server" SkinID="DeleteButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Delete %>" /></p>
        </asp:LinkButton>
    </toolbar>
</div>
