using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.IServices
{
    public interface ICreditCardService
    {
        List<CreditCard> GetCreditCards(int userId);
        bool AddCreditCard(int userId, string DisplayName, long CardNumber, DateTime Expiry, int CVV);
        bool RemoveCreditCard(int userId, int cardId);
        bool UpdateCreditCard(int userId, int CreditCardId, string DisplayName, long CardNumber, DateTime Expiry, int CVV);
        CreditCard GetCreditCard(int userId, int cardId);
    }
}
