<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DownloadsControl.ascx.cs" Inherits="Complex.Domino.Plugins.DownloadsControl, Complex.Domino.Plugins" %>

<%@ Register Src="~/Plugins/FileList.ascx" TagPrefix="domino" TagName="FileList" %>

<domino:FileList runat="server" ID="fileList" />