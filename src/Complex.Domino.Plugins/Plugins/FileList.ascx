<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileList.ascx.cs" Inherits="Complex.Domino.Web.FileList" %>

<asp:Panel runat="server" ID="uploadPanel">
    <table class="form">
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
                <asp:RegularExpressionValidator ID="UploadedFileValidator" runat="server" ErrorMessage="<%$ Resources:Errors, InvalidExtension %>"
                    ValidationExpression="^.+(.zip|.ZIP)$" ControlToValidate="UploadedFile" Display="Dynamic" ValidationGroup="Upload" />
                <asp:CustomValidator runat="server" Display="Dynamic" ID="EmptyValidator"
                    OnServerValidate="EmptyValidator_ServerValidate" ErrorMessage="<%$ Resources:Errors, EmptyFileList %>" />
            </td>
            <td class="field" style="text-align: right">
                <toolbar class="button">
                        <asp:LinkButton runat="server" ID="Upload" OnClick="Upload_Click" ValidationGroup="Upload">
                            <asp:Image runat="server" SkinID="UploadButton" />
                            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Upload %>" /></p>
                        </asp:LinkButton>
                    </toolbar>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel runat="server" ID="fileListPanel">
    <asp:ListView runat="server" ID="fileList">
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
                        <asp:LinkButton ID="name" runat="server" CommandName="click" /></h1>
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
            <%--<asp:Label runat="server" Text="<%$ Resources:Labels, NoFiles %>" />--%>
        </EmptyItemTemplate>
    </asp:ListView>
</asp:Panel>