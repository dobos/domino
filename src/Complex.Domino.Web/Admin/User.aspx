<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="Complex.Domino.Web.Admin.User" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>New user</h1>
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="NameLabel" CssClass="required">User name:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Name" runat="server" ValidationGroup="User" />
                <asp:RequiredFieldValidator ID="NameRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />Username is required" ControlToValidate="Name" ValidationGroup="User" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="DescriptionLabel" CssClass="required">Name:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Description" runat="server" ValidationGroup="User" />
                <asp:RequiredFieldValidator ID="DescriptionRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />Name is required" ControlToValidate="Description" ValidationGroup="User" />
            </td>
        </tr>
        <tr>
            <td class="label">&nbsp;</td>
            <td class="field">
                <asp:CheckBox ID="Enabled" runat="server" Text="Enabled" ValidationGroup="User" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="Visible" runat="server" Text="Visible" ValidationGroup="User"/>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="EmailLabel" CssClass="required">E-mail address:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Email" runat="server" ValidationGroup="User" />
                <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />E-mail address is required" ControlToValidate="Email" ValidationGroup="User" />
                <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server" 
                    ControlToValidate="Email" Display="Dynamic" ErrorMessage="<br />Invalid format" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="User" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="UsernameLabel" CssClass="required">User name:</asp:Label>
            </td>
            <td class="field">
                <asp:TextBox ID="Username" runat="server" CssClass="FormField" ValidationGroup="User" />
                <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server" Display="Dynamic"
                    ErrorMessage="<br />User name is required" ControlToValidate="Username" ValidationGroup="User"/>
            </td>
        </tr>
    </table>
    <table class="form">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="PasswordLabel">Password:</asp:Label></td>
            <td class="field">
                <asp:TextBox runat="server" ID="Password" ValidationGroup="User" /></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="PasswordConfirmLabel">Confirmation:</asp:Label></td>
            <td class="field">
                <asp:TextBox runat="server" ID="PasswordConfirm" ValidationGroup="User" /></td>
        </tr>
    </table>
    <div class="toolbar">
        <asp:LinkButton runat="Server" ID="Ok" Text="OK" OnClick="Ok_Click" ValidationGroup="User" />
        <asp:LinkButton runat="Server" ID="Cancel" Text="Cancel" OnClick="Cancel_Click" CausesValidation="False" />
    </div>
    <asp:Panel runat="server" ID="RolesPanel" Visible="false">
        <h2>User roles</h2>
        <div class="toolbar">
            Role:
            <asp:DropDownList runat="server" ID="RoleType" ValidationGroup="AddRole">
                <asp:ListItem Value="Unknown" Text="(select role)" />
                <asp:ListItem Value="Student" Text="Student" />
                <asp:ListItem Value="Teacher" Text="Teacher" />
                <asp:ListItem Value="Admin" Text="Admin" />
            </asp:DropDownList>
            Course:
            <asp:DropDownList runat="server" ID="Course" AppendDataBoundItems="true"
                DataTextField="Name" DataValueField="ID" ValidationGroup="AddRole">
                <asp:ListItem Value="-1" Text="(select course)" />
            </asp:DropDownList>
            <asp:LinkButton runat="Server" ID="AddRole" Text="Add Role" OnClick="AddRole_Click" ValidationGroup="AddRole" />
        </div>
        <domino:multiselectgridview runat="server" id="userRoleList" autogeneratecolumns="false" datakeynames="UserID, CourseID">
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
        <div class="toolbar">
            <asp:LinkButton runat="Server" ID="DeleteRole" ValidationGroup="DeleteRole" Text="Delete Role" OnClick="DeleteRole_Click"
                OnClientClick="return confirm('Are you sure you want to revoke the selected roles?')" />
        </div>
    </asp:Panel>
</asp:Content>
