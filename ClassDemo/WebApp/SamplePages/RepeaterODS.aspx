<%@ Page Title="Repeater ODS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepeaterODS.aspx.cs" Inherits="WebApp.SamplePages.RepeaterODS" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Demonstrate Repeater Control using ODS</h1>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <br />
    <asp:Repeater ID="EmployeeClientList" runat="server" 
        DataSourceID="EmployeeClientListODS"
         ItemType="ChinookSystem.Data.DTOs.SupportEmployee">
        <HeaderTemplate>
            <h3>Employee Customer Support List</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="row">
                <div class="col-md-6">
                   <h5><strong>Employee: <%# Item.Name %> (<%# Item.ClientCount %>) </strong></h5>
                    <br />
                    <asp:GridView ID="SupportedClientList" runat="server"
                         DataSource= <%# Item.ClientList %>
                         CssClass="table" GridLines="Horizontal" BorderStyle="None">

                    </asp:GridView>
                </div>
                    <div class="col-md-6">
                   <h5><strong>Employee: <%# Item.Name %> (<%# Item.ClientCount %>) </strong></h5>
                    <br />
                        <asp:ListView ID="ListView1" runat="server"
                             ItemType="ChinookSystem.Data.POCOs.Client"
                             DataSource=<%# Item.ClientList %>>
                            <ItemTemplate>
                                <tr>
                                    <td style="padding:3px">
                                        <asp:Label ID="label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </td>
                                    <td style="padding:3px">
                                        <asp:Label ID="label2" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                    </td>
                                </tr>
                               
                            </ItemTemplate>
                            <LayoutTemplate>
                                 <table runat="server">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none;  font-family: Verdana, Arial, Helvetica, sans-serif;" >
                                                <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                                    <th runat="server">Name</th>
                                                    <th runat="server">Phone</th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder"></tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>

                </div>
            </div>
        </ItemTemplate>

    </asp:Repeater>

    <asp:ObjectDataSource ID="EmployeeClientListODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Employees_ListSupportEmployees" 
        TypeName="ChinookSystem.BLL.EmployeeController"
         OnSelected="CheckForException">
    </asp:ObjectDataSource>
</asp:Content>
