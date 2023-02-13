<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="Chocolist.Pages.Cart.ViewCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ShoppingCartTitle" runat="server" class="ContentHead"><h1>Cart</h1></div>
    <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="False" ShowFooter="True" GridLines ="None" CellPadding="4"
        ItemType="ShopCarts.CartItem" EmptyDataText="There is nothing in your cart."
        CssClass="table table-striped table-bordered" style="color:#FFFFFF;"
        DataKeyNames="ItemId" OnRowCommand="CartList_RowCommand">
        <HeaderStyle HorizontalAlign="Left" BackColor="#CA5500" ForeColor="#EEEEEE" />
        <FooterStyle HorizontalAlign="Right" BackColor="#CA5500" ForeColor="#EEEEEE" />
        <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="UnitPrice" HeaderText="Price (each)" DataFormatString="{0:c}"/>
        <asp:TemplateField HeaderText="Quantity">
            <ItemTemplate>
                <asp:TextBox ID="PurchaseQuantity" Width="40" runat="server" Text='<%# Eval("Quantity")%>'></asp:TextBox><br />
                <asp:LinkButton runat="server" ID="btnRemove" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ItemId") %>' style="font-size:12px;"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Item Total">
            <ItemTemplate>
                <%# String.Format("{0:c}", Eval("TotalPrice")) %>
            </ItemTemplate>
            <FooterTemplate>
                <strong>
                    <asp:Label ID="lblTotalText" runat="server" Text=""></asp:Label>
                </strong>
            </FooterTemplate>
        </asp:TemplateField> 
        </Columns>    
    </asp:GridView>
    <asp:Button runat="server" ID="btnUpdateCart" Text="Update Cart" OnClick="btnUpdateCart_Click" /><br /><br />
    <asp:Button ID="btnCartBack" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Back" OnClick="btnCartBack_Click" CausesValidation="False" />
    <asp:Button ID="btnCartCheckout" class="btn btn-primary mx-auto mb-2 buttons" runat="server" Text="Checkout" OnClick="btnCartCheckout_Click" />
</asp:Content>
