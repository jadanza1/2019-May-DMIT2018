<%@ Page Title="CRUD ODS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlbumCRUD_ODS.aspx.cs" Inherits="WebApp.SamplePages.AlbumCRUD_ODS" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>CRUD ODS: Albums</h1>

    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <asp:ObjectDataSource ID="AlbumListODS" runat="server" DataObjectTypeName="ChinookSystem.Data.Entities.Album" 
        OldValuesParameterFormatString="original_{0}" TypeName="ChinookSystem.BLL.AlbumController" 
        DeleteMethod="Album_Delete" InsertMethod="Album_Add" 
        SelectMethod="Album_List"  UpdateMethod="Album_Update">

    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Artist_List" TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
    
    <asp:ListView ID="ListView1" runat="server"></asp:ListView>


</asp:Content>
