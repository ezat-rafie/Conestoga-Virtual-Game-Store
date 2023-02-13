using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.IServices
{
    public interface IAddressService
    {
        Address GetAddress(int userID, int addressId);
        bool UpdateAddress(Address address);
        int CreateAddress(Address address);
        List<Address> GetAddresses(int userId);
        bool RemoveAddress(int userId, int cardId);
    }
}
