using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataService.DataAccessLayer.Services;
using DataService.Models;
using ShopCarts;

namespace Chocolist.Pages.Cart
{
    public partial class ViewOrders : System.Web.UI.Page
    {
        InvoiceService invoiceService = new InvoiceService();

        public static List<DisplayInvoice> displayInvoices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               BindGridList();
            }
        }
        protected void BindGridList()
        {
            displayInvoices = invoiceService.GetDisplayInvoices();
            OrderList.DataSource = displayInvoices;
            OrderList.DataBind();
        }

        protected void OrderList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Ship"))
            {
                invoiceService.UpdateInvoiceStatus(Convert.ToInt32(e.CommandArgument.ToString()), 6);
                BindGridList();
            }
        }
    }
}