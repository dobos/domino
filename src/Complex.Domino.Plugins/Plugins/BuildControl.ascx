<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuildControl.ascx.cs" Inherits="Complex.Domino.Plugins.BuildControl, Complex.Domino.Plugins" %>


<table class="form">
    <tr>
        <td class="label">
            <asp:Label runat="server" ID="commandLineLabel" Text="Command-line" />:
        </td>
        <td class="field wide" colspan="2">
            <asp:TextBox runat="server" ID="commandLine" />
        </td>
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
<asp:Panel runat="server" ID="console" Visible="false">
    <asp:Label runat="server" Text="Build output" />:
    <console>
<asp:Literal runat="server" ID="output" />
    </console>
</asp:Panel>
