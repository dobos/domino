<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="CourseList.aspx.cs" Inherits="Complex.Domino.Web.Student.CourseList" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All courses</h1>
    <asp:ObjectDataSource runat="server" ID="courseDataSource" DataObjectTypeName="Complex.Domino.Lib.Course"
        OnObjectCreating="courseDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.CourseFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <domino:multiselectgridview id="courseList" runat="server" datasourceid="courseDataSource"
        autogeneratecolumns="false" datakeynames="ID"
        allowpaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="assignmentlist.aspx?CourseID={0}"
                DataTextField="Name"
                HeaderText="Name"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="Url"
                DataNavigateUrlFormatString="{0}"
                Text="URL"
                HeaderText="Url"/>
        </Columns>
        <EmptyDataTemplate>
            <p>No courses match the query.</p>
        </EmptyDataTemplate>
    </domino:multiselectgridview>
</asp:Content>
