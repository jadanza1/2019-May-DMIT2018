<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlbumSongRepeater.aspx.cs" Inherits="WebApp.SamplePages.AlbumSongRepeater" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

</asp:Content>
