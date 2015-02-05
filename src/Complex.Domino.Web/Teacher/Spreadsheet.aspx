<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Spreadsheet.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Spreadsheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <div class="spreadsheet">
        <asp:PlaceHolder runat="server" ID="tablePlaceholder" />
    </div>
</asp:Content>
