using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Models
{
    public class Invoice
    {
        public int invoiceId { get; set; }
        public int userId { get; set; }
        public int statusId { get; set; }
        public int creditCardId { get; set; }
        public int addressId { get; set; }
        public int billingAddressId { get; set; }
        public DateTime date { get; set; }
        public bool paid { get; set; }
    }

    public class DisplayInvoice
    {
        public int invoiceId { get; set; }
        public string userEmail { get; set; }
        public int statusId { get; set; }
        public DateTime date { get; set; }
        public double total { get; set; }
        public String items { get; set; }
    }
    public class InvoiceItem
    {
        public int itemId { get; set; }
        public int invoiceId { get; set; }
        public int invoiceLine { get; set; }
        public double invoicePrice { get; set; }
        public int quantity { get; set; }
    }

    public enum InvoiceStatus
    {
        Received = 3,
        PaymentProcessed = 4,
        PaymentError = 5,
        Processed = 6
    }
}
