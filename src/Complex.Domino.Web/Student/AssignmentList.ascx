<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignmentList.ascx.cs" Inherits="Complex.Domino.Web.Student.AssignmentList" %>

<asp:ObjectDataSource runat="server" ID="assignmentDataSource" DataObjectTypeName="Complex.Domino.Lib.Assignment"
        OnObjectCreating="assignmentDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.AssignmentFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <asp:ListView runat="server" ID="assignmentList" DataSourceID="assignmentDataSource"
        OnItemCreated="assignmentList_OnItemCreated">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </LayoutTemplate>
        <ItemTemplate>
            <asp:HyperLink runat="server" CssClass="fullbar" ID="SubmissionsLink"
                NavigateUrl='<%# Complex.Domino.Web.Student.Assignment.GetUrl((int)Eval("ID")) %>'>
                <asp:Image ID="Image1" runat="server" SkinID="AssignmentIcon" />
                <h1><%# Eval("Description") %></h1>
                <p>
                    <asp:Label runat="server" ID="semester" /> |
                    <asp:Label runat="server" ID="course" /> |
                    Due: <asp:Label runat="server" ID="endDateSoft" />
                </p>
            </asp:HyperLink>
        </ItemTemplate>
        <EmptyDataTemplate>
            No assignments found.
        </EmptyDataTemplate>
    </asp:ListView>