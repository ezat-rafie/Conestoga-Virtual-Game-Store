using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShopCarts
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; private set; }

        public static readonly ShoppingCart Instance;

        static ShoppingCart()
        {
            if (HttpContext.Current.Session["CVGSShoppingCart"] == null)
            {
                Instance = new ShoppingCart();
                Instance.Items = new List<CartItem>();
                HttpContext.Current.Session["CVGSShoppingCart"] = Instance;
            }
            else
            {
                Instance = (ShoppingCart)HttpContext.Current.Session["CVGSShoppingCart"];
            }
        }

        protected ShoppingCart() { }

        public void AddItem(int itemId)
        {
            CartItem newItem = new CartItem(itemId);
            bool isFound = false;
            foreach (CartItem item in Items)
            {
                if (item.Equals(newItem))
                {
                    item.Quantity++;
                    isFound = true;
                    return;
                }
            }
            if (!isFound)
            {
                newItem.Quantity = 1;
                Items.Add(newItem);
            }
        }

        public void ClearCart()
        {
            Items = new List<CartItem>();
        }

        public void SetItemQuantity(int itemId, int quantity)
        {
            if (quantity <= 0)
            {
                RemoveItem(itemId);
                return;
            }

            for (int index = Items.Count - 1; index >= 0; index--)
            {
                if (Items[index].ItemId == itemId)
                {
                    Items[index].Quantity = quantity;
                }
            }
        }

        public void RemoveItem(int itemId)
        {
            for (int index = Items.Count-1; index >= 0; index --)
            {
                if (Items[index].ItemId == itemId)
                {
                    Items.RemoveAt(index);
                }
            }
        }

        public double GetSubTotal()
        {
            double subTotal = 0;
            foreach (CartItem item in Items)
            {
                subTotal += item.TotalPrice;
            }
            return (subTotal);
        }

        public int GetItemCount()
        {
            Debug.WriteLine("**CART** Getting ItemCount");
            int totalCount = 0;
            foreach (CartItem item in Items)
            {
                totalCount += item.Quantity;
            }
            return (totalCount);
        }
    }
}
