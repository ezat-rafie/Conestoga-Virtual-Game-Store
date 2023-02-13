<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Chocolist.Pages.Cart.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .text-input {
            background-color: #d9d9d912;
            border: none;
            color: #fff;
            font-size: 20px;
            border-radius: 5px;
            padding: 5px;
            box-shadow: rgb(0 0 0 / 0.40) 2px 2px 10px;
        }
        .panel {
            background-color: #303030;
        }
        hr {
            color: #ddd;
            background-color: #ddd;
        }
        .text-right {
            text-align: right;
        }
        .inline-flex {
            display: inline-flex;
        }
        .mt-auto {
            margin-top:auto;
        }
    </style>
    <h2>< Continue Shopping</h2>
    <asp:Label Text="" ID ="lblMessage" runat="server"/>
    <div class="row" id="divSelections" runat="server">
        <div class="col-9 panel">
            <div class="row">
                <div class="col-6 mt-auto">
                    <h2>Shipping Address</h2>
                </div>
                <div class="col-6">
                    <asp:Label
                        Text="Select Address"
                        AssociatedControlID="ddlSelectShipAddress"
                        runat="server"
                        CssClass="w-100">
                        <asp:DropDownList class="w-100 text-input btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="ddlSelectShipAddress" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ShipAddress_Changed">
                            <asp:ListItem Text="None" Value="0" Selected="True" />
                        </asp:DropDownList>
                    </asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <hr />
                    <div class="row">
                        <div class="col-6">
                            <asp:Label
                                Text="Address Name"
                                AssociatedControlID="txtShipAddressName"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtShipAddressName" placeholder="Address Name" runat="server" Enabled="false"/>
                            </asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:Label
                                Text="Address Line 1"
                                AssociatedControlID="txtShipAddress1"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtShipAddress1" placeholder="Address Line 1" runat="server" Enabled="false"/>
                            </asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:Label
                                Text="City"
                                AssociatedControlID="txtShipCity"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtShipCity" placeholder="City" runat="server" Enabled="false" />
                            </asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:Label
                                Text="Address Line 2"
                                AssociatedControlID="txtShipAddress2"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtShipAddress2" placeholder="Address Line 2" runat="server" Enabled="false" />
                            </asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:Label
                                Text="Province"
                                AssociatedControlID="ddlShipProvince"
                                runat="server"
                                CssClass="w-100">
                                <asp:DropDownList CssClass="text-input ml-2 btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="ddlShipProvince" runat="server" Enabled="false">
                                    <asp:ListItem Text="ON" Value="ON" Selected="True" />
                                    <asp:ListItem Text="QC" Value="QC" />
                                    <asp:ListItem Text="BC" Value="BC" />
                                    <asp:ListItem Text="AB" Value="AB" />
                                    <asp:ListItem Text="MB" Value="MB" />
                                    <asp:ListItem Text="NB" Value="NB" />
                                    <asp:ListItem Text="NL" Value="NL" />
                                    <asp:ListItem Text="NT" Value="NT" />
                                    <asp:ListItem Text="NS" Value="NS" />
                                    <asp:ListItem Text="NU" Value="NU" />
                                    <asp:ListItem Text="PE" Value="PE" />
                                    <asp:ListItem Text="SK" Value="SK" />
                                    <asp:ListItem Text="YT" Value="YT" />
                                </asp:DropDownList>
                            </asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:Label
                                Text="Address Line 3"
                                AssociatedControlID="txtShipAddress3"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtShipAddress3" placeholder="Address Line 3" runat="server"  Enabled="false"/>
                            </asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:Label
                                Text="Postal Code"
                                AssociatedControlID="txtShipPostal"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtShipPostal" placeholder="Postal Code" runat="server" Enabled="false" />
                            </asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <br /><br />
            <div class="row">
                <div class="col-6 mt-auto">
                    <h2 class="inline-flex">Billing Address</h2> <asp:CheckBox ID="chkBillUseShipAddress" runat="server" Text="Use Shipping Address" AutoPostBack="true" OnCheckedChanged="BillUseShip_Changed" />
                </div>
                <div class="col-6">
                    <asp:Label
                        Text="Select Address"
                        AssociatedControlID="ddlSelectBillAddress"
                        runat="server"
                        CssClass="w-100">
                        <asp:DropDownList CssClass="w-100 text-input btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="ddlSelectBillAddress" runat="server" AutoPostBack="true" OnSelectedIndexChanged="BillAddress_Changed">
                            <asp:ListItem Text="None" Value="0" Selected="True" />
                        </asp:DropDownList>
                    </asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <hr />
                    <div class="row" id="divBillAddress" runat="server"><div class="col-12">
                        <div class="row">
                            <div class="col-6">
                                <asp:Label
                                    Text="Address Name"
                                    AssociatedControlID="txtBillAddressName"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtBillAddressName" placeholder="Address Name" runat="server" Enabled="false" />
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Label
                                    Text="Address Line 1"
                                    AssociatedControlID="txtBillAddress1"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtBillAddress1" placeholder="Address Line 1" runat="server" Enabled="false" />
                                </asp:Label>
                            </div>
                            <div class="col-6">
                                <asp:Label
                                    Text="City"
                                    AssociatedControlID="txtBillCity"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtBillCity" placeholder="City" runat="server" Enabled="false" />
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Label
                                    Text="Address Line 2"
                                    AssociatedControlID="txtBillAddress2"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtBillAddress2" placeholder="Address Line 2" runat="server" Enabled="false" />
                                </asp:Label>
                            </div>
                            <div class="col-6">
                                <asp:Label
                                    Text="Province"
                                    AssociatedControlID="ddlBillProvince"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:DropDownList CssClass="text-input ml-2 btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="ddlBillProvince" runat="server" Enabled="false">
                                        <asp:ListItem Text="ON" Value="ON" Selected="True" />
                                        <asp:ListItem Text="QC" Value="QC" />
                                        <asp:ListItem Text="BC" Value="BC" />
                                        <asp:ListItem Text="AB" Value="AB" />
                                        <asp:ListItem Text="MB" Value="MB" />
                                        <asp:ListItem Text="NB" Value="NB" />
                                        <asp:ListItem Text="NL" Value="NL" />
                                        <asp:ListItem Text="NT" Value="NT" />
                                        <asp:ListItem Text="NS" Value="NS" />
                                        <asp:ListItem Text="NU" Value="NU" />
                                        <asp:ListItem Text="PE" Value="PE" />
                                        <asp:ListItem Text="SK" Value="SK" />
                                        <asp:ListItem Text="YT" Value="YT" />
                                    </asp:DropDownList>
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Label
                                    Text="Address Line 3"
                                    AssociatedControlID="txtBillAddress3"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtBillAddress3" placeholder="Address Line 3" runat="server" Enabled="false" />
                                </asp:Label>
                            </div>
                            <div class="col-6">
                                <asp:Label
                                    Text="Postal Code"
                                    AssociatedControlID="txtBillPostal"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" CssClass="text-input ml-2" ID="txtBillPostal" placeholder="Postal Code" runat="server" Enabled="false" />
                                </asp:Label>
                            </div>
                        </div>
                    </div></div>
                </div>
            </div>
            <br /><br />
            <div class="row">
                <div class="col-6 mt-auto">
                    <h2>Payment Method</h2>
                </div>
                <div class="col-6">
                    <asp:Label
                        Text="Select Credit Card"
                        AssociatedControlID="ddlSelectCC"
                        runat="server"
                        CssClass="w-100">
                        <asp:DropDownList CssClass="w-100 text-input btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="ddlSelectCC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CC_Changed">
                            <asp:ListItem Text="None" Value="0" Selected="True" />
                        </asp:DropDownList>
                    </asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <hr />
                    <div class="row">
                        <div class="col">
                            <asp:Label
                                Text="Display Name"
                                AssociatedControlID="txtCCDisplayName"
                                runat="server">
                                <asp:TextBox CssClass="text-input" ClientIDMode="Static" ID="txtCCDisplayName" runat="server" placeholder="Display name on card" Enabled="false"></asp:TextBox>
                            </asp:Label>
                        </div>
                        <div class="col">
                            <asp:Label
                                Text="Card Number"
                                AssociatedControlID="txtCCCardNumber"
                                runat="server">
                                <asp:TextBox CssClass="text-input" ClientIDMode="Static" ID="txtCCCardNumber" runat="server" placeholder="Card number" Enabled="false"></asp:TextBox>
                            </asp:Label>
                        </div>
                        <div class="col">
                            <asp:Label
                                Text="Expiry Date"
                                AssociatedControlID="expireCCDateDiv"
                                runat="server">
                                <div class="row m-0" runat="server" id="expireCCDateDiv">
                                    <asp:DropDownList CssClass="w-45 text-input" ID="CCExpireMonth" runat="server"  Enabled="false">
                                        <asp:ListItem Text="1" Value="1" /> 
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" />
                                        <asp:ListItem Text="6" Value="6" />
                                        <asp:ListItem Text="7" Value="7" />
                                        <asp:ListItem Text="8" Value="8" />
                                        <asp:ListItem Text="9" Value="9" />
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="11" Value="11" />
                                        <asp:ListItem Text="12" Value="12" />
                                    </asp:DropDownList>
                                    <span style="margin: 0 5px; font-size: 25px;">/</span>
                                    <asp:DropDownList CssClass="w-45 text-input" ID="CCExpireYear" runat="server" Enabled="false">
                                        <asp:ListItem Text="22" Value="2022" /> 
                                        <asp:ListItem Text="23" Value="2023" />
                                        <asp:ListItem Text="24" Value="2024" />
                                        <asp:ListItem Text="25" Value="2025" />
                                        <asp:ListItem Text="26" Value="2026" />
                                        <asp:ListItem Text="27" Value="2027" />
                                        <asp:ListItem Text="28" Value="2028" />
                                        <asp:ListItem Text="29" Value="2029" />
                                        <asp:ListItem Text="30" Value="2030" />
                                        <asp:ListItem Text="31" Value="2031" />
                                        <asp:ListItem Text="32" Value="2032" />
                                    </asp:DropDownList>
                                </div>
                            </asp:Label>
                        </div>
                        <div class="col">
                            <asp:Label
                                Text="CVV"
                                AssociatedControlID="txtCCCvv"
                                runat="server">
                                <asp:TextBox CssClass="text-input" ClientIDMode="Static" ID="txtCCCvv" runat="server" placeholder="CVV" Enabled="false"></asp:TextBox>
                            </asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-3">
            <div class="row">
                <div class="col-2">&nbsp;</div>
                <div class="col-10 panel">
                    <div class="row">
                        <div class="col-6">
                            <asp:Button ID="btnCheckoutBack" CssClass="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Back" OnClick="btnCheckoutBack_Click" CausesValidation="False" />
                        </div>
                        <div class="col-6">
                            <asp:Button ID="btnCheckoutOrder" CssClass="btn btn-primary mx-auto mb-2 buttons" runat="server" Text="Place Order" OnClick="btnCheckoutPlace_Click" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-12">
                            <h2>Summary</h2>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-6">
                            Total
                        </div>
                        <div class="col-6 text-right">
                            <asp:Label id="lblCheckoutSubtotal" Text="$--.--" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            Shipping
                        </div>
                        <div class="col-6 text-right">
                            FREE
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            Taxes
                        </div>
                        <div class="col-6 text-right">
                            <asp:Label id="lblCheckoutTaxes" Text="$--.--" runat="server" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-6">
                            <h2>Total</h2>
                        </div>
                        <div class="col-6 text-right">
                            <asp:Label id="lblCheckoutTotal" Text="$--.--" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-2">&nbsp;</div>
                <div class="col-10 panel">
                    <div class="row">
                        <div class="col-12">
                            <h2>In Your Cart</h2>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-12">
                            <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="False" ShowHeader="False" ShowFooter="False" GridLines ="Vertical" CellPadding="4"
                                ItemType="ShopCarts.CartItem" EmptyDataText="Empty Cart."
                                CssClass="table table-striped table-bordered" style="color:#EEE;"
                                DataKeyNames="ItemId">
                                <Columns>
                                    <asp:BoundField DataField="Name" />
                                    <asp:BoundField DataField="Quantity" />
                                    <asp:BoundField DataField="UnitPrice" />
                                </Columns>    
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>