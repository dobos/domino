<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">Pattern:</td>
                <td class="field"><asp:TextBox runat="server" ID="pattern" /></td>
                <td class="error"></td>
            </tr>
        </table>
        <asp:Button runat="server" ID="ok" Text="OK" OnClick="Ok_Click"/>
    </div>
    <asp:Panel runat="server" ID="matchListPanel" Visible="false">
        <div class="frame">
            <asp:ListView runat="server" ID="matchList" OnItemCreated="MatchList_ItemCreated">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                </LayoutTemplate>
                <ItemTemplate>
                    <p>
                        <asp:HyperLink runat="server" ID="submission" />
                    </p>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </asp:Panel>
</asp:Content>
