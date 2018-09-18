<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExecuteControl.ascx.cs" Inherits="Complex.Domino.Plugins.ExecuteControl, Complex.Domino.Plugins" %>

<%@ Register Src="~/Plugins/FileList.ascx" TagPrefix="domino" TagName="FileList" %>

<domino:FileList runat="server" ID="fileList" ValidationGroup="ExecutePlugin" />
<table class="form">
    <tr>
        <td class="label">
            <asp:Label runat="server" ID="commandLineLabel" Text="Command-line" />:
        </td>
        <td class="field wide" colspan="2">
            <asp:TextBox runat="server" ID="commandLine" /></td>
    </tr>
    <tr runat="server" id="executeRow">
        <td class="label"></td>
        <td class="error"></td>
        <td class="field" style="text-align: right">
            <toolbar class="button">
                <asp:LinkButton runat="server" ID="ok" OnClick="Ok_Click">
                    <asp:Image runat="server" SkinID="OkButton" />
                    <p><asp:Label runat="server" Text="<%$ Resources:Labels, Execute %>" /></p>
                </asp:LinkButton>
            </toolbar>
        </td>
    </tr>
</table>
<asp:Panel runat="server" ID="outputPanel" Visible="false">
    <asp:Label runat="server" Text="Execute output" />:
    <console>
<asp:Literal runat="server" ID="output" />
    </console>
</asp:Panel>
<asp:Panel runat="server" ID="stdoutPanel" Visible="false">
    <asp:Label runat="server" Text="Standard output" />:
    <console>
<pre><asp:Literal runat="server" ID="stdout" /></pre>
    </console>
</asp:Panel>
<asp:Panel runat="server" ID="stderrPanel" Visible="false">
    <asp:Label runat="server" Text="Standard error" />:
    <console>
<pre><asp:Literal runat="server" ID="stderr" /></pre>
    </console>
</asp:Panel>
<asp:Panel runat="server" ID="debugPanel" Visible="false">
    <asp:Label runat="server" Text="Debug output" />:
    <console>
<pre><asp:Literal runat="server" ID="debug" /></pre>
    </console>
</asp:Panel>
