<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="Complex.Domino.Web.Admin.Course" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>Course</h1>
    <domino:entityform runat="server" id="entityForm" />
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="SemesterLabel">Semester:</asp:Label>
                </td>
                <td class="field">
                    <asp:DropDownList runat="server" ID="Semester" DataValueField="ID" DataTextField="Name" AppendDataBoundItems="true">
                        <asp:ListItem Value="-1" Text="(select semester)" />
                    </asp:DropDownList>
                </td>
                <td class="error">
                    <asp:RangeValidator runat="server" ControlToValidate="Semester"
                        Display="Dynamic" MinimumValue="1" MaximumValue="2147483647"
                        ErrorMessage="<br />Select a semester" Type="Integer" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="StartDateLabel" CssClass="required">Start date:</asp:Label>
                </td>
                <td class="field">
                    <asp:TextBox ID="StartDate" runat="server"></asp:TextBox>
                </td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="EndDateLabel" CssClass="required">End date:</asp:Label>
                </td>
                <td class="field">
                    <asp:TextBox ID="EndDate" runat="server"></asp:TextBox>
                </td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="UrlLabel">URL:</asp:Label>
                </td>
                <td class="field">
                    <asp:TextBox ID="Url" runat="server"></asp:TextBox>
                </td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="GradeTypeLabel">Grade type:</asp:Label>
                </td>
                <td class="field">
                    <asp:DropDownList runat="server" ID="GradeType">
                        <asp:ListItem Value="Unknown" Text="(select grade type)" />
                        <asp:ListItem Value="Signature" Text="Signature" />
                        <asp:ListItem Value="Grade" Text="Grade" />
                        <asp:ListItem Value="Points" Text="Points" />
                    </asp:DropDownList>
                </td>
                <td class="error"></td>
            </tr>
        </table>
    </div>
    <toolbar class="form">
        <asp:LinkButton runat="Server" ID="Ok" OnClick="Ok_Click">
            <asp:Image runat="server" SkinID="OkButton" />
            <p>Ok</p>
        </asp:LinkButton>
        <asp:LinkButton runat="Server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="False">
            <asp:Image runat="server" SkinID="CancelButton" />
            <p>Cancel</p>
        </asp:LinkButton>
    </toolbar>
</asp:Content>
