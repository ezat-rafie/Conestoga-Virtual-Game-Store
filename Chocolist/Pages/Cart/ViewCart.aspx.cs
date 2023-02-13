using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShopCarts;

namespace Chocolist.Pages.Cart
{
    public partial class ViewCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridList();
                ((SiteMaster)(Page.Master)).UpdateCart(ShopCarts.ShoppingCart.Instance.GetItemCount());
                UpdateTotal();
            }
        }
        protected void UpdateTotal()
        {
            if (CartList.FooterRow != null)
            {
                ((Label)CartList.FooterRow.Cells[CartList.FooterRow.Cells.Count - 1].Controls[1]).Text =
                    "Total: " + String.Format("{0:c}", ShopCarts.ShoppingCart.Instance.GetSubTotal());
            }
        }
        protected void BindGridList()
        {
            CartList.DataSource = ShopCarts.ShoppingCart.Instance.Items;
            CartList.DataBind();
        }

        protected void btnUpdateCart_Click(object sender, EventArgs e)
        {
            int offset = 0;
            foreach (GridViewRow row in CartList.Rows)
            {
                TextBox txtQuantity = (TextBox)row.FindControl("PurchaseQuantity");
                int newAmount = Convert.ToInt32(txtQuantity.Text);
                int itemId = ShopCarts.ShoppingCart.Instance.Items[offset].ItemId;
                if (newAmount <= 0)
                {
                    offset--;
                }

                ShopCarts.ShoppingCart.Instance.SetItemQuantity(itemId, newAmount);
                offset++;
            }
            BindGridList();
            ((SiteMaster)(Page.Master)).UpdateCart(ShopCarts.ShoppingCart.Instance.GetItemCount());
            UpdateTotal();
        }

        protected void CartList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Remove"))
            {
                ShopCarts.ShoppingCart.Instance.RemoveItem(Convert.ToInt32(e.CommandArgument.ToString()));
                BindGridList();
                UpdateTotal();
                ((SiteMaster)(Page.Master)).UpdateCart(ShopCarts.ShoppingCart.Instance.GetItemCount());
            }
        }

        protected void btnCartCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Cart/Checkout.aspx");
        }
        protected void btnCartBack_Click(object sender, EventArgs e)
        {
            Session["ItemListTabIndex"] = 0;
            Response.Redirect("~/Default.aspx");
        }
    }
}