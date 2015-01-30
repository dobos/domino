<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="ImportUsers.aspx.cs" Inherits="Complex.Domino.Web.Admin.ImportUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <h1>Import users</h1>
    <asp:Panel runat="server" ID="importPanel">
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="FileLabel" Text="User list file" />:
                    </td>
                    <td class="field">
                        <asp:FileUpload runat="server" ID="File" />
                    </td>
                    <td class="error">
                        <asp:RequiredFieldValidator runat="server" ID="FileRequiredValidator"
                            ErrorMessage="<%$ Resources:Errors, Required %>" ControlToValidate="File"
                            Display="Dynamic" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="listPanel" Visible="false">
        <div class="grid">
            <asp:GridView ID="userList" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="grid">
                <Columns>
                    <asp:BoundField HeaderText="User name" DataField="Name" />
                    <asp:BoundField HeaderText="Name" DataField="Description" />
                    <asp:BoundField HeaderText="E-mail" DataField="Email" />
                    <asp:BoundField HeaderText="Password" DataField="ActivationCode" />
                </Columns>
                <EmptyDataTemplate>
                    <p>No users match the query.</p>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <h2>Duplicates</h2>
        <div class="grid">
            <asp:GridView ID="duplicateList" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="grid">
                <Columns>
                    <asp:BoundField HeaderText="User name" DataField="Name" />
                    <asp:BoundField HeaderText="Name" DataField="Description" />
                    <asp:BoundField HeaderText="E-mail" DataField="Email" />
                </Columns>
                <EmptyDataTemplate>
                    <p>No users match the query.</p>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>
    <toolbar class="form">
        <asp:LinkButton runat="server" ID="OK" OnClick="Ok_Click">
            <asp:Image runat="server" SkinID="OkButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
        </asp:LinkButton>
        <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="false">
            <asp:Image runat="server" SkinID="CancelButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Cancel %>" /></p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
