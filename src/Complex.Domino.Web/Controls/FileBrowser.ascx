<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileBrowser.ascx.cs" Inherits="Complex.Domino.Web.Controls.FileBrowser" %>
<h1>File list</h1>
<asp:Label runat="server" Text="Current directory:" />
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
<domino:multiselectlistview runat="server" id="fileList" datakeynames="Name"
    onitemcreated="fileList_ItemCreated" onitemcommand="fileList_ItemCommand"
    onItemDeleting="fileList_ItemDeleting">
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
</domino:multiselectlistview>
<div class="toolbar">
    <asp:LinkButton runat="server" Text="Download" />
    <asp:LinkButton runat="server" Text="Delete" />
</div>
<asp:Panel runat="server" ID="uploadPanel">
    <h2>Upload files</h2>
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="UploadedFileLabel" Text="File:" />
            </td>
            <td class="field">
                <input type="file" runat="server" id="UploadedFile" />
            </td>
        </tr>
    </table>
    <div class="toolbar">
        <asp:LinkButton runat="server" Text="Upload" ID="Upload" OnClick="Upload_Click" />
    </div>
</asp:Panel>
