﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserViewOrders.aspx.cs" Inherits="Chocolist.Pages.User.UserViewOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="OrdersTitle" runat="server" class="ContentHead"><h1>Orders to Download</h1></div>
    <asp:GridView ID="OrderList" runat="server" AutoGenerateColumns="False" ShowFooter="False" GridLines ="None" CellPadding="4"
        ItemType="DataService.Models.DisplayInvoice" EmptyDataText="There are no games available for download."
        CssClass="table table-striped table-bordered" style="color:#FFFFFF;"
        DataKeyNames="invoiceId" OnRowCommand="OrderList_RowCommand">
        <HeaderStyle HorizontalAlign="Left" BackColor="#CA5500" ForeColor="#EEEEEE" />
        <Columns>
        <asp:BoundField DataField="invoiceId" HeaderText="Invoice #" />
        <asp:TemplateField HeaderText="Order Info">
            <ItemTemplate>
                User: <%# Eval("userEmail") %><br />
                <ul>
                    <%# Eval("items") %>
                </ul>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="date" HeaderText="Date"  DataFormatString="{0:yyyy-MM-dd}"/>
        <asp:BoundField DataField="total" HeaderText="Total"  DataFormatString="{0:c}"/>
        <asp:TemplateField HeaderText="Download">
            <ItemTemplate>
                <asp:Button runat="server" ID="btnShip" Text="Download" CommandName="Ship" CommandArgument='<%# Eval("invoiceId") %>' style="font-size:12px;"></asp:Button>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>    
    </asp:GridView>
</asp:Content>
