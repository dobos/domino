<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.master" AutoEventWireup="true" CodeBehind="Spreadsheet.aspx.cs" Inherits="Complex.Domino.Web.Teacher.Spreadsheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <h1><asp:Label runat="server" Text="<%$ Resources:Labels, Results %>" /></h1>
    <div class="spreadsheet">
        <asp:PlaceHolder runat="server" ID="tablePlaceholder" />
    </div>
</asp:Content>
