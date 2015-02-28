<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DownloadsControl.ascx.cs" Inherits="Complex.Domino.Plugins.DownloadsControl, Complex.Domino.Plugins" %>
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
