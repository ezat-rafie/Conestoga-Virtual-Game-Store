using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCarts
{
    public class CartItem
    {
        private DataService.DataAccessLayer.Services.ItemService itemService = new DataService.DataAccessLayer.Services.ItemService();
        public int Quantity { get; set; }
        private int _itemId;
        private Item _item = null;
        public int ItemId
        {
            get { return _itemId; }
            set
            {
                _item = null;
                _itemId = value;
            }
        }
        public Item Item
        {
            get
            {
                if (_item == null)
                {
                    _item = itemService.GetItem(ItemId);
                }
                return (_item);
            }
        }
        public String Description
        {
            get { return Item.Description; }
        }
        public String Name
        {
            get { return Item.Name; }
        }
        public double UnitPrice
        {
            get { return Item.Price; }
        }
        public double TotalPrice
        {
            get { return UnitPrice * Quantity; }
        }
        public CartItem(int itemId)
        {
            this.ItemId = itemId;
        }
        public bool Equals(CartItem other)
        {
            return (other.ItemId == this.ItemId);
        }
    }
}
