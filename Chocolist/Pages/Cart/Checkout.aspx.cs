using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShopCarts;

namespace Chocolist.Pages.Cart
{
    public partial class Checkout : System.Web.UI.Page
    {
        IAddressService addressService = new AddressService();
        ICreditCardService cardService = new CreditCardService();
        InvoiceService invoiceService = new InvoiceService();

        public static int creditCardId = 0;
        public static int shipAddressId = 0;
        public static int billAddressId = 0;
        public static int loggedInUserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            loggedInUserID = Session["LoggedIn"] != null ? (int)Session["LoggedIn"] : 0;
            if (!IsPostBack)
            {
                PopulateAddresses();
                PopulateCreditCards();
                BindGridList();
                ((SiteMaster)(Page.Master)).UpdateCart(ShopCarts.ShoppingCart.Instance.GetItemCount());
                lblMessage.Text = "";
            }
            double subTotal = ShopCarts.ShoppingCart.Instance.GetSubTotal();
            lblCheckoutSubtotal.Text = String.Format("{0:c}", subTotal);
            lblCheckoutTaxes.Text = String.Format("{0:c}", subTotal * 0.13);
            lblCheckoutTotal.Text = String.Format("{0:c}", subTotal * 1.13);
        }

        
        protected void BindGridList()
        {
            CartList.DataSource = ShopCarts.ShoppingCart.Instance.Items;
            CartList.DataBind();
        }

        private void PopulateAddresses()
        {
            List<Address> userAddresses;
            if (loggedInUserID > 0)
            {
                userAddresses = addressService.GetAddresses(loggedInUserID);
                foreach (Address address in userAddresses)
                {
                    ListItem ddlAdd = new ListItem();
                    ddlAdd.Value = address.AddressId.ToString();
                    ddlAdd.Text = address.FullName + "-" + address.AddressLine1;
                    ddlSelectShipAddress.Items.Add(ddlAdd);
                    ddlSelectBillAddress.Items.Add(ddlAdd);
                }
            }
        }

        protected void BillUseShip_Changed(object sender, EventArgs e)
        {
            if (chkBillUseShipAddress.Checked)
            {
                divBillAddress.Visible = false;
            } else
            {
                divBillAddress.Visible = true;
            }
        }

        private void PopulateCreditCards()
        {
            List<CreditCard> userCCs;
            loggedInUserID = Session["LoggedIn"] != null ? (int)Session["LoggedIn"] : 0;
            if (loggedInUserID > 0)
            {
                userCCs = cardService.GetCreditCards(loggedInUserID);
                foreach (CreditCard creditCard in userCCs)
                {
                    ListItem ddlAdd = new ListItem();
                    ddlAdd.Value = creditCard.CreditCardId.ToString();
                    String tempCC = creditCard.CardNumber.ToString();
                    ddlAdd.Text = creditCard.DisplayName + "-xxxx" + tempCC.Substring(tempCC.Length-4);
                    ddlSelectCC.Items.Add(ddlAdd);
                }
            }
        }
        protected void CC_Changed(object sender, EventArgs e)
        {
            creditCardId = int.Parse(ddlSelectCC.SelectedValue);
            if (creditCardId > 0 && loggedInUserID > 0)
            {
                CreditCard userCC = cardService.GetCreditCard(loggedInUserID, creditCardId);
                txtCCDisplayName.Text = userCC.DisplayName;
                txtCCCvv.Text = userCC.CVV.ToString();
                txtCCCardNumber.Text = userCC.CardNumber.ToString();
                CCExpireMonth.SelectedValue = userCC.Expiry.Month.ToString();
                CCExpireYear.SelectedValue = userCC.Expiry.Year.ToString();
            }
        }
        protected void ShipAddress_Changed(object sender, EventArgs e)
        {
            shipAddressId = int.Parse(ddlSelectShipAddress.SelectedValue);
            if (shipAddressId > 0 && loggedInUserID > 0)
            {
                Address userAddress = addressService.GetAddress(loggedInUserID, shipAddressId);
                txtShipAddressName.Text = userAddress.FullName;
                txtShipAddress1.Text = userAddress.AddressLine1;
                txtShipAddress2.Text = userAddress.AddressLine2;
                txtShipAddress3.Text = userAddress.AddressLine3;
                txtShipCity.Text = userAddress.City;
                txtShipPostal.Text = userAddress.PostalCode;
                ddlShipProvince.SelectedValue = userAddress.Province;
            }
        }
        protected void BillAddress_Changed(object sender, EventArgs e)
        {
            billAddressId = int.Parse(ddlSelectBillAddress.SelectedValue);
            if (billAddressId > 0 && loggedInUserID > 0)
            {
                Address userAddress = addressService.GetAddress(loggedInUserID, billAddressId);
                txtBillAddressName.Text = userAddress.FullName;
                txtBillAddress1.Text = userAddress.AddressLine1;
                txtBillAddress2.Text = userAddress.AddressLine2;
                txtBillAddress3.Text = userAddress.AddressLine3;
                txtBillCity.Text = userAddress.City;
                txtBillPostal.Text = userAddress.PostalCode;
                ddlBillProvince.SelectedValue = userAddress.Province;
            }
        }
        protected void btnCheckoutPlace_Click(object sender, EventArgs e)
        {
            String outText = "";

            if (billAddressId == 0 && !chkBillUseShipAddress.Checked)
            {
                outText += "Billing Address needs to be selected.<br>";
            }
            if (shipAddressId == 0)
            {
                outText += "Shipping Address needs to be selected.<br>";
            }
            if (creditCardId == 0)
            {
                outText += "Credit Card needs to be selected.<br>";
            }
            if (outText.Equals(""))
            {
                List<InvoiceItem> invoiceItems = new List<InvoiceItem>();
                foreach (CartItem cartItem in ShopCarts.ShoppingCart.Instance.Items)
                {
                    InvoiceItem invoiceItem = new InvoiceItem();
                    invoiceItem.itemId = cartItem.ItemId;
                    invoiceItem.quantity = cartItem.Quantity;
                    invoiceItem.invoicePrice = cartItem.UnitPrice;
                    invoiceItems.Add(invoiceItem);
                }
                invoiceService.CreateInvoice(loggedInUserID, creditCardId, shipAddressId, chkBillUseShipAddress.Checked?shipAddressId:billAddressId, invoiceItems);
                ShopCarts.ShoppingCart.Instance.ClearCart();
                ((SiteMaster)(Page.Master)).UpdateCart(ShopCarts.ShoppingCart.Instance.GetItemCount());
                BindGridList();
                lblMessage.Text = "Order successfully placed.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                divSelections.Visible = false;
            } else
            {
                lblMessage.Text = "There were problems:<br>" + outText;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnCheckoutBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Cart/ViewCart.aspx");
        }
    }
}