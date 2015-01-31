<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="Complex.Domino.Web.Admin.User" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="userPanel" DefaultButton="Ok">
        <h1>User</h1>
        <domino:entityform runat="server" id="entityForm" />
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="EmailLabel" CssClass="required">E-mail address:</asp:Label>
                    </td>
                    <td class="field">
                        <asp:TextBox ID="Email" runat="server" ValidationGroup="User" />
                    </td>
                    <td class="error">
                        <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server"
                            ControlToValidate="Email" Display="Dynamic" ErrorMessage="<br />Invalid format"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="User" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordLabel">Password:</asp:Label></td>
                    <td class="field">
                        <asp:TextBox runat="server" ID="Password" ValidationGroup="User" /></td>
                    <td class="error"></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="PasswordConfirmLabel">Confirmation:</asp:Label></td>
                    <td class="field">
                        <asp:TextBox runat="server" ID="PasswordConfirm" ValidationGroup="User" /></td>
                    <td class="error"></td>
                </tr>
            </table>
        </div>
        <toolbar class="form">
            <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click" ValidationGroup="User">
                <asp:Image runat="server" SkinID="OkButton" />
                <p>Ok</p>
            </asp:LinkButton>
            <asp:LinkButton runat="Server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="False">
                <asp:Image runat="server" SkinID="CancelButton" />
                <p>Cancel</p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
    <asp:Panel runat="server" ID="rolesPanel" Visible="false">
        <h2>User roles</h2>
        <div class="frame">
            <table class="form">
                <tr>
                    <td class="label">Role:</td>
                    <td class="field">
                        <asp:DropDownList runat="server" ID="RoleType" ValidationGroup="AddRole">
                            <asp:ListItem Value="Unknown" Text="(select role)" />
                            <asp:ListItem Value="Student" Text="Student" />
                            <asp:ListItem Value="Teacher" Text="Teacher" />
                            <asp:ListItem Value="Admin" Text="Admin" />
                        </asp:DropDownList>
                    </td>
                    <td class="error"></td>
                </tr>
                <tr>
                    <td class="label">Course:</td>
                    <td class="field">
                        <asp:DropDownList runat="server" ID="Course" AppendDataBoundItems="true"
                            DataTextField="Name" DataValueField="ID" ValidationGroup="AddRole">
                            <asp:ListItem Value="-1" Text="(select course)" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <toolbar class="button" style="margin-left: 8px;">
                            <asp:LinkButton runat="Server" ID="AddRole" OnClick="AddRole_Click" ValidationGroup="AddRole">
                                <asp:Image runat="server" SkinID="OkButton" />
                                <p>Add Role</p>
                            </asp:LinkButton>
                        </toolbar>
                    </td>
                </tr>
            </table>
            <domino:multiselectgridview runat="server" id="userRoleList" autogeneratecolumns="false"
                datakeynames="UserID, CourseID" CssClass="grid">
                <Columns>
                    <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
                    <asp:HyperLinkField
                        DataNavigateUrlFields="SemesterID"
                        DataNavigateUrlFormatString="Semester.aspx?ID={0}"
                        DataTextField="SemesterName"
                        HeaderText="Semester"/>
                    <asp:HyperLinkField
                        DataNavigateUrlFields="CourseID"
                        DataNavigateUrlFormatString="Course.aspx?ID={0}"
                        DataTextField="CourseName"
                        HeaderText="Course"/>
                    <asp:BoundField HeaderText="Role" DataField="RoleType"/>
                </Columns>
                <EmptyDataTemplate>
                    <p>User has no roles yet.</p>
                </EmptyDataTemplate>
            </domino:multiselectgridview>
            <toolbar class="right">
                <asp:LinkButton runat="Server" ID="DeleteRole" ValidationGroup="DeleteRole" OnClick="DeleteRole_Click"
                OnClientClick="return confirm('Are you sure you want to revoke the selected roles?')">
                    <asp:Image runat="server" SkinID="DeleteButton" />
                    <p>Delete Role</p>
                </asp:LinkButton>
            </toolbar>
        </div>
    </asp:Panel>
</asp:Content>
