<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="Complex.Domino.Web.Admin.User" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="userPanel" DefaultButton="Ok">
        <h1>
            <asp:Label runat="server" ID="TitleLabel" /></h1>
        <div class="frame">
            <domino:entityform runat="server" id="entityForm" />
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="EmailLabel" Text="<%$ Resources:Labels, Email %>" />:
                    </td>
                    <td class="field">
                        <asp:TextBox ID="Email" runat="server" ValidationGroup="Entity" />
                    </td>
                    <td class="error">
                        <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server"
                            ControlToValidate="Email" Display="Dynamic" ErrorMessage="<%$ Resources:Errors, InvalidFormat %>"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Entity" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordLabel" Text="<%$ Resources:Labels, Password %>" />:
                    </td>
                    <td class="field">
                        <asp:TextBox runat="server" ID="Password" ValidationGroup="Entity" />
                    </td>
                    <td class="error"></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordConfirmLabel" Text="<%$ Resources:Labels, Confirmation %>" />:
                    </td>
                    <td class="field">
                        <asp:TextBox runat="server" ID="PasswordConfirm" ValidationGroup="Entity" />
                    </td>
                    <td class="error"></td>
                </tr>
            </table>
        </div>
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
    </asp:Panel>
    <asp:Panel runat="server" ID="rolesPanel" Visible="false">
        <h2>
            <asp:Label runat="server" Text="<%$ Resources:Labels, UserRoles %>" /></h2>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" Text="<%$ Resources:Labels, Role %>" />:
                    </td>
                    <td class="field">
                        <asp:DropDownList runat="server" ID="RoleType" ValidationGroup="AddRole">
                            <asp:ListItem Value="Unknown" Text="<%$ Resources:Labels, SelectRole %>" />
                            <asp:ListItem Value="Student" Text="<%$ Resources:Roles, Student %>" />
                            <asp:ListItem Value="Teacher" Text="<%$ Resources:Roles, Teacher %>" />
                            <asp:ListItem Value="Admin" Text="<%$ Resources:Roles, Admin %>" />
                        </asp:DropDownList>
                    </td>
                    <td class="error">
                        <asp:RangeValidator runat="server" ControlToValidate="RoleType"
                            Display="Dynamic" MinimumValue="1" MaximumValue="2147483647" ValidationGroup="AddRole"
                            ErrorMessage="<%$ Resources:Errors, SelectItem %>" Type="Integer" />
                    </td>
                </tr>
                <tr>
                    <td class="label"><asp:Label runat="server" Text="<%$ Resources:Labels, Course %>" />:</td>
                    <td class="field">
                        <asp:DropDownList runat="server" ID="Course" AppendDataBoundItems="true"
                            DataTextField="FullName" DataValueField="ID" ValidationGroup="AddRole">
                            <asp:ListItem Value="-1" Text="<%$ Resources:Labels, SelectCourse %>" />
                        </asp:DropDownList>
                        <asp:RangeValidator runat="server" ControlToValidate="Course"
                            Display="Dynamic" MinimumValue="1" MaximumValue="2147483647" ValidationGroup="AddRole"
                            ErrorMessage="<%$ Resources:Errors, SelectItem %>" Type="Integer" />
                    </td>
                    <td>
                        <toolbar class="button" style="margin-left: 8px;">
                            <asp:LinkButton runat="Server" ID="AddRole" OnClick="AddRole_Click" ValidationGroup="AddRole">
                                <asp:Image runat="server" SkinID="OkButton" />
                                <p><asp:Label runat="server" Text="<%$ Resources:Labels, AddRole %>" /></p>
                            </asp:LinkButton>
                        </toolbar>
                    </td>
                </tr>
            </table>
            <domino:multiselectgridview runat="server" id="userRoleList" autogeneratecolumns="false"
                datakeynames="UserID, CourseID" cssclass="grid">
                <Columns>
                    <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField
                        DataNavigateUrlFields="SemesterID"
                        DataNavigateUrlFormatString="Semester.aspx?ID={0}"
                        DataTextField="SemesterName"
                        HeaderText="<%$ Resources:Labels, Semester %>"/>
                    <asp:HyperLinkField
                        DataNavigateUrlFields="CourseID"
                        DataNavigateUrlFormatString="Course.aspx?ID={0}"
                        DataTextField="CourseName"
                        HeaderText="<%$ Resources:Labels, Course %>"/>
                    <asp:BoundField HeaderText="<%$ Resources:Labels, Role %>" DataField="RoleType"/>
                </Columns>
                <EmptyDataTemplate>
                    <p><asp:Label runat="server" Text="<%$ Resources:Labels, NoRoles %>" /></p>
                </EmptyDataTemplate>
            </domino:multiselectgridview>
            <toolbar class="right">
                <asp:LinkButton runat="Server" ID="DeleteRole" ValidationGroup="DeleteRole" OnClick="DeleteRole_Click"
                OnClientClick="return confirm('<%$ Resources:Labels, ConfirmDeleteRole %>')">
                    <asp:Image runat="server" SkinID="DeleteButton" />
                    <p><asp:Label runat="server" Text="<%$ Resources:Labels, DeleteRole %>" /></p>
                </asp:LinkButton>
            </toolbar>
        </div>
    </asp:Panel>
</asp:Content>
