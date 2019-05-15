<%@ Page Title="Filter Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FilterSearch.aspx.cs" Inherits="WebApp.SamplePages.FilterSearch" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Filter Search</h1>
    <blockquote class="alert alert-info">
        This page will review filter search techniques. This page will be using code-behind and ObjectDataSource on multi-record controls.
        This page will use various form control. This page will Review event driven logic.
    </blockquote>
    <div class="col-md-offset-3">

        <br />
        <br />
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-info"
                HeaderText="Please Correct these following Information" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="EditTitleOfAlbum"
                ErrorMessage="Title Is Required" Display="None" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                ControlToValidate="EditYear" Display="None" 
                MinimumValue="1950" MaximumValue="2019"
                ErrorMessage="Year must be between 1950 and 2019" />

        </div>
        <asp:Label ID="Label1" runat="server" Text="Select an Artist"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="ArtistList" runat="server">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:LinkButton ID="FetchAlbums" runat="server" CausesValidation="false">Fetch Albums</asp:LinkButton>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        <br />
        <asp:GridView ID="AlbumList" runat="server" DataSourceID="AlbumListODS" AllowPaging="true" PageSize="5"
            CssClass="table table-striped" OnSelectedIndexChanged="AlbumList_SelectedIndexChanged">
            <%-- More Gridview Styling: --%>
            <%--BorderStyle="None" GridLines="Horizontal"--%>
            <Columns>
                <asp:CommandField SelectText="View"
                    ShowSelectButton="true" CausesValidation="false" />
                <asp:TemplateField HeaderText="Album">
                    <ItemTemplate>
                        <asp:Label ID="AlbumTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        <asp:Label ID="AlbumId" runat="server" Text='<%# Eval("AlbumId") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Year">
                    <ItemTemplate>
                        <asp:Label ID="Title" runat="server" Text='<%# Eval("ReleaseYear") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Label">
                    <ItemTemplate>
                        <asp:Label ID="ReleaseLabel" runat="server" Text='<%# Eval("ReleaseLabel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No Albums for Selected Artists.
            </EmptyDataTemplate>
        </asp:GridView>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Album ID"></asp:Label>
        &nbsp;&nbsp;

       <asp:Label ID="EditAlbumID" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Title"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="EditTitleOfAlbum" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Artist"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="EditAlbumArtistList" runat="server" DataSourceID="AlbumArtistListODS" DataTextField="Name" DataValueField="ArtistId">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Year:"></asp:Label>
        &nbsp;&nbsp;
       <asp:TextBox ID="EditYear" runat="server" type="integer"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Label:"></asp:Label>
        &nbsp;&nbsp;
       <asp:TextBox ID="EditReleaseLabel" runat="server"></asp:TextBox>
        <br />
        <br />
        <fieldset>
            <asp:LinkButton ID="Add_Button" runat="server" OnClick="Add_Button_Click"> Add </asp:LinkButton>
            &nbsp; &nbsp;
            <asp:LinkButton ID="Update_Button" runat="server" OnClick="Update_Button_Click"> Update</asp:LinkButton>
            &nbsp; &nbsp;
            <asp:LinkButton ID="Remove_Button" runat="server" OnClientClick="return confirm('Are You Sure You Wish to Remove This Album From the Collection?')"
                CausesValidation="false" OnClick="Remove_Button_Click"> Remove </asp:LinkButton>
            &nbsp; &nbsp;
        </fieldset>
    </div>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Album_GetByArtist" TypeName="ChinookSystem.BLL.AlbumController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ArtistList" PropertyName="SelectedValue" DefaultValue="0" Name="artistId" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="AlbumArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Artist_List" TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>

</asp:Content>
