﻿<%@ Page Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="Submission.aspx.cs" Inherits="Complex.Domino.Web.Student.Submission" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="emptyPanel">
        <h1>New submission</h1>
        <p>
            You have chosen to start a new submission but files from an earlier
            submission are available. Do you want to keep these or start from
            an empty folder?
        </p>
        <p>
            <asp:LinkButton runat="server" ValidationGroup="NewSubmission" ID="NewSubmissionKeep"
                OnClick="NewSubmissionKeep_Click"
                Text="Keep files" />
            <asp:LinkButton runat="server" ValidationGroup="NewSubmission" ID="NewSubmissionEmpty"
                OnClick="NewSubmissionEmpty_Click"
                Text="Start with empty folder" />
        </p>
    </asp:Panel>
    <asp:Panel runat="server" ID="filesPanel" Visible="false">
        <h1>Upload your files</h1>
        <domino:filebrowser runat="server" id="fileBrowser" />
        <domino:EntityForm runat="server" id="entityForm"
            NameVisible="false" DescriptionVisible="false" OptionsVisible="false" />
        <toolbar class="form">
            <asp:LinkButton runat="server" ID="OK" OnClick="Ok_Click">
                <asp:Image runat="server" SkinID="OkButton" />
                <p>Submit</p>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="Cancel" OnClick="Cancel_Click" CausesValidation="false">
                <asp:Image runat="server" SkinID="CancelButton" />
                <p>Cancel</p>
            </asp:LinkButton>
        </toolbar>
    </asp:Panel>
</asp:Content>
