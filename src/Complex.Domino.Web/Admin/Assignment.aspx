﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Assignment.aspx.cs" Inherits="Complex.Domino.Web.Admin.Assignment" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" ID="TitleLabel" /></h1>
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="SemesterLabel" Text="<%$ Resources:Labels, Course %>" />:
                </td>
                <td class="field">
                    <asp:DropDownList runat="server" ID="Course" DataValueField="ID" DataTextField="FullName" AppendDataBoundItems="true">
                        <asp:ListItem Value="-1" Text="<%$ Resources:Labels, SelectCourse %>" />
                    </asp:DropDownList>
                </td>
                <td class="error">
                    <asp:RangeValidator runat="server" ControlToValidate="Course" ValidationGroup="Entity"
                        Display="Dynamic" MinimumValue="1" MaximumValue="2147483647"
                        ErrorMessage="<%$ Resources:Errors, SelectItem %>" Type="Integer" />
                </td>
            </tr>
        </table>
        <domino:entityform runat="server" id="entityForm" />
    </div>
    <div class="frame">
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="StartDateLabel" Text="<%$ Resources:Labels, StartDate %>" />:
                </td>
                <td class="field">
                    <asp:TextBox ID="StartDate" runat="server" TextMode="DateTimeLocal" />
                </td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="EndDateSoftLabel" Text="<%$ Resources:Labels, EndDateSoft %>" />:
                </td>
                <td class="field">
                    <asp:TextBox ID="EndDateSoft" runat="server" TextMode="DateTimeLocal" />
                </td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="EndDateLabel" Text="<%$ Resources:Labels, EndDate %>" />:
                </td>
                <td class="field">
                    <asp:TextBox ID="EndDate" runat="server" />
                </td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="UrlLabel" Text="<%$ Resources:Labels, WebPage %>" />
                </td>
                <td class="field">
                    <asp:TextBox ID="Url" runat="server" />
                </td>
                <td class="error"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="GradeTypeLabel" Text="<%$ Resources:Labels, GradeType %>" />
                </td>
                <td class="field">
                    <asp:DropDownList runat="server" ID="GradeType">
                        <asp:ListItem Value="Unknown" Text="<%$ Resources:Labels, SelectGradeType %>" />
                        <asp:ListItem Value="Signature" Text="<%$ Resources:Grades, Signature %>" />
                        <asp:ListItem Value="Grade" Text="<%$ Resources:Grades, Grade %>" />
                        <asp:ListItem Value="Points" Text="<%$ Resources:Grades, Points %>" />
                    </asp:DropDownList>
                </td>
                <td class="error">
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="GradeWeightLabel" Text="<%$ Resources:Labels, GradeWeight %>" />:
                </td>
                <td class="field">
                    <asp:TextBox runat="server" ID="GradeWeight" />
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
</asp:Content>
