<%@ Page Title="Repeater ODS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepeaterODS.aspx.cs" Inherits="WebApp.SamplePages.RepeaterODS" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />


    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Employees_ListSupportEmployees" TypeName="ChinookSystem.BLL.EmployeeController"></asp:ObjectDataSource>

    <asp:Repeater ID="EmployeeClientList" runat="server"
        DataSourceID="EmployeeClientListODS"
        ItemType="ChinookSystem.Data.DTOs.SupportEmployee">
        <HeaderTemplate>
            <h2>Employee Customer Support List</h2>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="row">
                <div class="col-md-6">
                    <h5><strong><%# Item.Name %> </strong></h5>(<%# Item.clientCount %>)
                    <asp:GridView ID="SupportedClientList" runat="server" DataSource= <%# Item.ClientList %>  CssClass="table" GridLines="Horizontal" BorderStyle="None">

                    </asp:GridView>

                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
