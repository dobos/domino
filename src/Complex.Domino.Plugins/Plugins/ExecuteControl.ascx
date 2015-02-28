<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExecuteControl.ascx.cs" Inherits="Complex.Domino.Plugins.ExecuteControl, Complex.Domino.Plugins" %>

<%@ Register Src="~/Plugins/FileList.ascx" TagPrefix="domino" TagName="FileList" %>

<domino:FileList runat="server" ID="fileList" ValidationGroup="ExecutePlugin" />
<table class="form">
    <tr>
        <td class="label">
            <asp:Label runat="server" ID="commandLineLabel" Text="Command-line"/>:
        </td>
        <td class="field wide">
            <asp:TextBox runat="server" ID="commandLine" /></td>
    </tr>
</table>
<toolbar class="form">
    <asp:LinkButton runat="server" ID="ok" OnClick="Ok_Click">
        <asp:Image runat="server" SkinID="OkButton" />
        <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
    </asp:LinkButton>
</toolbar>
<asp:Panel runat="server" ID="console" Visible="false">
<asp:Label runat="server" Text="Build output" />:
    <console>
<asp:Literal runat="server" ID="output" />
    </console>
</asp:Panel>
