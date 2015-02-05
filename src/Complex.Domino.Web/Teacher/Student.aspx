<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Student" %>

<%@ Register Src="~/Teacher/SubmissionList.ascx" TagPrefix="domino" TagName="SubmissionList" %>


<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>
        <asp:Label runat="server" ID="TitleLabel" Text="<%$ Resources:Labels, Student %>" /></h1>
    <div class="frame">
        <domino:entityform runat="server" id="entityForm" />
        <table class="form">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="EmailLabel" Text="<%$ Resources:Labels, Email %>" />:
                </td>
                <td class="field">
                    <asp:HyperLink ID="Email" runat="server"/>
                </td>
                <td class="error">
                </td>
            </tr>
        </table>
    </div>
    <domino:submissionlist runat="server" id="submissionList" />
</asp:Content>
