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
    onitemcreated="fileList_ItemCreated" onitemcommand="fileList_ItemCommand">
    <layouttemplate>
        <table>
            <thead>
                <tr>
                    <td></td>
                    <td></td>
                    <td>Name</td>
                    <td>Size</td>
                </tr>
            </thead>
            <tbody>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />        
            </tbody>
        </table>
    </layouttemplate>
    <itemtemplate>
        <tr>
            <td><asp:CheckBox runat="server" ID="selectionCheckbox" /></td>
            <td><asp:Image ID="icon" runat="server" Width="16px" Height="16px" /></td>
            <td><asp:LinkButton ID="name" runat="server" /></td>
            <td><asp:Label ID="size" runat="server" /></td>
        </tr>
    </itemtemplate>
</domino:multiselectlistview>
<div class="toolbar">
    <asp:LinkButton runat="server" Text="Download" />
    <asp:LinkButton runat="server" Text="Delete" />
</div>
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
    <asp:LinkButton runat="server" Text="Upload" />
</div>
