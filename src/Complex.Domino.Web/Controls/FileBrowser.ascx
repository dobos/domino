<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileBrowser.ascx.cs" Inherits="Complex.Domino.Web.Controls.FileBrowser" %>
<div class="frame">
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Current directory:" />
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
                    <asp:Label runat="server" ID="UploadedFileLabel" Text="Upload File:" />
                </td>
                <td class="field" style="text-align: right">
                    <input type="file" runat="server" id="UploadedFile" />
                    <toolbar class="button" style="margin-left: 8px;">
                        <asp:LinkButton runat="server" ID="Upload" OnClick="Upload_Click">
                            <asp:Image runat="server" SkinID="UploadButton" />
                            <p>Upload</p>
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
                                <asp:HyperLink runat="server" ID="view">
                                    <asp:Image runat="server" SkinID="ViewButton" />
                                </asp:HyperLink>
                                <asp:HyperLink runat="server" ID="edit">
                                    <asp:Image runat="server" SkinID="EditButton" />
                                </asp:HyperLink>
                            </td>
                        </tr>
                </itemtemplate>
                <EmptyItemTemplate>
                No files have been uploaded so far.
                </EmptyItemTemplate>
            </domino:multiselectlistview>
        </scroll>
    </list>

    <toolbar class="right">
        <asp:LinkButton runat="server" ToolTip="Download selected files as an archive.">
            <asp:Image runat="server" SkinID="DownloadButton" />
            <p>Download</p>
        </asp:LinkButton>
        <asp:LinkButton runat="server" ToolTip="Delete selected files.">
            <asp:Image runat="server" SkinID="DeleteButton" />
            <p>Delete</p>
        </asp:LinkButton>
    </toolbar>
</div>
