<%@ Page Title="Using Linq Query" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LinqQueryODS.aspx.cs" Inherits="WebApp.SamplePages.LinqQueryODS" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Using Linq/Entity Query</h1>

    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <br />
    <asp:ListView ID="AlbumArtistList" runat="server" DataSourceID="AlbumstArtistListODS" GroupItemCount="3">
        <AlternatingItemTemplate>
            <td runat="server" style="background-color: #FFFFFF; color: #284775;">Title:
                <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /><br />
                Year:
                <asp:Label Text='<%# Eval("Year") %>' runat="server" ID="YearLabel" /><br />
                AritstName:
                <asp:Label Text='<%# Eval("AritstName") %>' runat="server" ID="AritstNameLabel" /><br />
            </td>
        </AlternatingItemTemplate>
<%--        <EditItemTemplate>
            <td runat="server" style="background-color: #999999;">Title:
                <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBox" /><br />
                Year:
                <asp:TextBox Text='<%# Bind("Year") %>' runat="server" ID="YearTextBox" /><br />
                AritstName:
                <asp:TextBox Text='<%# Bind("AritstName") %>' runat="server" ID="AritstNameTextBox" /><br />
                <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" /><br />
                <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" /><br />
            </td>
        </EditItemTemplate>--%>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <td runat="server" />
        </EmptyItemTemplate>
        <GroupTemplate>
            <tr runat="server" id="itemPlaceholderContainer">
                <td runat="server" id="itemPlaceholder"></td>
            </tr>
        </GroupTemplate>
<%--        <InsertItemTemplate>
            <td runat="server" style="">Title:
                <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBox" /><br />
                Year:
                <asp:TextBox Text='<%# Bind("Year") %>' runat="server" ID="YearTextBox" /><br />
                AritstName:
                <asp:TextBox Text='<%# Bind("AritstName") %>' runat="server" ID="AritstNameTextBox" /><br />
                <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" /><br />
                <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" /><br />
            </td>
        </InsertItemTemplate>--%>
        <ItemTemplate>
            <td runat="server" style="background-color: #E0FFFF; color: #333333;">Title:
                <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /><br />
                Year:
                <asp:Label Text='<%# Eval("Year") %>' runat="server" ID="YearLabel" /><br />
                AritstName:
                <asp:Label Text='<%# Eval("AritstName") %>' runat="server" ID="AritstNameLabel" /><br />
            </td>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="groupPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                            <tr runat="server" id="groupPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                        <asp:DataPager runat="server" PageSize="12" ID="DataPager1">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                <asp:NumericPagerField></asp:NumericPagerField>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <td runat="server" style="background-color: #E2DED6; font-weight: bold; color: #333333;">Title:
                <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /><br />
                Year:
                <asp:Label Text='<%# Eval("Year") %>' runat="server" ID="YearLabel" /><br />
                AritstName:
                <asp:Label Text='<%# Eval("AritstName") %>' runat="server" ID="AritstNameLabel" /><br />
            </td>
        </SelectedItemTemplate>
    </asp:ListView>

    <asp:ObjectDataSource ID="AlbumstArtistListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Album_ListAlbumArtist" 
        TypeName="ChinookSystem.BLL.AlbumController" 
        OnSelected="CheckForException"/>

</asp:Content>
