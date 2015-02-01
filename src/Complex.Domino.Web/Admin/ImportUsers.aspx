<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="ImportUsers.aspx.cs" Inherits="Complex.Domino.Web.Admin.ImportUsers" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" ID="TitleLabel" Text="<%$ Resources:Labels, ImportUsers %>" /></h1>
    <asp:Panel runat="server" ID="importPanel">
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="FileLabel" Text="<%$ Resources:Labels, UserListFile %>" />:
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
                <tr>
                    <td class="label">
                        <asp:Label runat="server" Text="<%$ Resources:Labels, AddAsStudentTo %>" />:</td>
                    <td class="field">
                        <asp:DropDownList runat="server" ID="Course" DataValueField="ID" DataTextField="Name" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text="<%$ Resources:Labels, DoNotAdd %>" />
                        </asp:DropDownList>
                    </td>
                    <td class="error"></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="listPanel" Visible="false">
        <h2><asp:Label runat="server" Text="<%$ Resources:Labels, NewUsers %>" /></h2>
        <div class="grid">
            <asp:GridView ID="userList" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="grid">
                <Columns>
                    <asp:BoundField HeaderText="<%$ Resources:Labels, UserName %>" DataField="Name" />
                    <asp:BoundField HeaderText="<%$ Resources:Labels, Name %>" DataField="Description" />
                    <asp:BoundField HeaderText="<%$ Resources:Labels, Email %>" DataField="Email" />
                    <asp:BoundField HeaderText="<%$ Resources:Labels, Password %>" DataField="ActivationCode" />
                </Columns>
                <EmptyDataTemplate>
                    <p><asp:Label runat="server" Text="<%$ Resources:Labels, NoUsers %>" /></p>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="duplicatesPanel" Visible="false">
        <h2><asp:Label runat="server" Text="<%$ Resources:Labels, DuplicateUsers %>" /></h2>
        <div class="grid">
            <asp:GridView ID="duplicateList" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="grid">
                <Columns>
                    <asp:BoundField HeaderText="<%$ Resources:Labels, UserName %>" DataField="Name" />
                    <asp:BoundField HeaderText="<%$ Resources:Labels, Name %>" DataField="Description" />
                </Columns>
                <EmptyDataTemplate>
                    <p><asp:Label runat="server" Text="<%$ Resources:Labels, NoUsers %>" /></p>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>
    <toolbar class="form">
        <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click" ValidationGroup="Entity">
            <asp:Image runat="server" SkinID="OkButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
        </asp:LinkButton>
        <asp:LinkButton runat="Server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="False">
            <asp:Image runat="server" SkinID="CancelButton" />
            <p><asp:Label runat="server" Text="<%$ Resources:Labels, Cancel %>" /></p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
