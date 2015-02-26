<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuildControl.ascx.cs" Inherits="Complex.Domino.Plugins.BuildControl, Complex.Domino.Plugins" %>


<asp:Panel runat="server" ID="adminView" Visible="false">
    <table class="form">
        <tr>
            <td class="label"></td>
            <td class="field">
                <asp:TextBox runat="server" ID="commandLine" /></td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel runat="server" ID="publicView" Visible="false">
    <asp:HyperLink runat="server">
            Build program
    </asp:HyperLink>
</asp:Panel>
