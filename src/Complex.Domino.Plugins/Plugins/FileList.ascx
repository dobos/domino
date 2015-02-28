<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileList.ascx.cs" Inherits="Complex.Domino.Plugins.FileList" %>

<asp:Panel runat="server" ID="uploadPanel">
    <table class="form" runat="server" id="uploadForm">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="UploadFileLabel" Text="<%$ Resources:Labels, UploadFile %>" />:
            </td>
            <td class="field" colspan="2">
                <input type="file" runat="server" id="UploadedFile" style="width: 442px" />
            </td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="error">
                <asp:RequiredFieldValidator ID="UploadedFileRequiredValidator" runat="server" ErrorMessage="<%$ Resources:Errors, Required %>"
                    ControlToValidate="UploadedFile" Display="Dynamic" ValidationGroup="Upload" />
            </td>
            <td class="field" style="text-align: right">
                <toolbar class="button">
                <asp:LinkButton runat="server" ID="upload" OnClick="Upload_Click" ValidationGroup="Upload">
                    <asp:Image runat="server" SkinID="UploadButton" />
                    <p><asp:Label runat="server" Text="<%$ Resources:Labels, Upload %>" /></p>
                </asp:LinkButton>
            </toolbar>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel runat="server" ID="fileListPanel">
    <list>
        <scroll style="height:140px">
            <asp:ListView runat="server" ID="fileList" OnItemCreated="FileList_ItemCreated" OnItemDeleting="FileList_ItemDeleting"
               DataKeyNames="ID">
                <LayoutTemplate>
                    <table>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="icon">
                            <asp:CheckBox runat="server" ID="selectionCheckbox" />
                            <asp:Image ID="icon" runat="server" SkinID="UnknownFileIcon" />
                        </td>
                        <td class="text">
                            <h1>
                                <asp:Label ID="name" runat="server" /></h1>
                            <p>
                                <asp:Label ID="size" runat="server" />
                            </p>
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
                </ItemTemplate>
                <EmptyItemTemplate>
                    <asp:Label runat="server" Text="<%$ Resources:Labels, NoFiles %>" />
                </EmptyItemTemplate>
            </asp:ListView>
        </scroll>
    </list>
</asp:Panel>
