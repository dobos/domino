<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileBrowser.ascx.cs" Inherits="Complex.Domino.Web.Controls.FileBrowser" %>
<div class="frame">
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Current directory:" />
            </td>
            <td class="field">
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
    <div class="list">
        <div class="scroll">
            <domino:multiselectlistview runat="server" id="fileList" datakeynames="Name"
                onitemcreated="fileList_ItemCreated" onitemcommand="fileList_ItemCommand"
                onitemdeleting="fileList_ItemDeleting">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </layouttemplate>
    <itemtemplate>
        <div>
            <asp:CheckBox runat="server" ID="selectionCheckbox" />
            <asp:Image ID="icon" runat="server" Width="16px" Height="16px" />
            <asp:LinkButton ID="name" runat="server" CommandName="click" />
            <asp:Label ID="size" runat="server" />
            <asp:LinkButton runat="server" Text="delete" ID="delete" CommandName="delete" />
            <asp:LinkButton runat="server" Text="download" ID="download" CommandName="download" />
            <asp:LinkButton runat="server" Text="view" ID="view" CommandName="view" />
            <asp:LinkButton runat="server" Text="edit" ID="edit" CommandName="edit" />
        </div>
    </itemtemplate>
                <EmptyItemTemplate>
                No files have been uploaded so far.
            </EmptyItemTemplate>
</domino:multiselectlistview>
        </div>
    </div>

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
