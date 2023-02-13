using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Models;

namespace DataService.DataAccessLayer.IServices
{
    public interface IInvoiceService
    {
        bool CreateInvoice(int userId, int creditCardId, int addressId, int billingAddressId, List<InvoiceItem> cartItems);
        bool UpdateInvoiceStatus(int invoiceId, int statusId);
        List<DisplayInvoice> GetDisplayInvoices();
        List<DisplayInvoice> GetDisplayInvoicesByUserId(int UserId);
        List<InvoiceItem> GetInvoiceItems(int invoiceId);
    }
}
