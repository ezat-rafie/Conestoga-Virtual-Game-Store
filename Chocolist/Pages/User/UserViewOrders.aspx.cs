using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataService.DataAccessLayer.Services;
using DataService.Models;
using ShopCarts;

namespace Chocolist.Pages.User
{
    public partial class UserViewOrders : System.Web.UI.Page
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
            var loggedIn = Session["LoggedIn"];
            displayInvoices = invoiceService.GetDisplayInvoicesByUserId(Convert.ToInt32(loggedIn));
            for (int i = displayInvoices.Count - 1; i >= 0; i--) {
                if (displayInvoices[i].items.Equals(""))
                {
                    displayInvoices.RemoveAt(i);
                }
            }
            OrderList.DataSource = displayInvoices;
            OrderList.DataBind();
        }

        protected void OrderList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Ship"))
            {
                var gameName = invoiceService.GetGameNameByInvoiceId(Convert.ToInt32(e.CommandArgument));
                string s = "Congratulations.\r\nYour game has been downloaded.";

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename="+ gameName + ".txt");
                Response.AddHeader("content-type", "text/plain");

                using (StreamWriter writer = new StreamWriter(Response.OutputStream))
                {
                    writer.WriteLine(s);
                }
                Response.End();
            }
        }
    }
}