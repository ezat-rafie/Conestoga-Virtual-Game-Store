using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.IServices
{
    public interface IFriendService
    {
        List<FriendUser> GetFriends(int userId);
        List<FriendUser> GetRequests(int userId, bool isSent);
        List<FriendUser> SearchUsers(int userId, string keyword);
        bool CreateLinkedUser(int requesterId, int requesteeId);
        bool DeleteLinkedUser(int user1Id, int user2Id);
        bool CheckDuplicate(int user1Id, int user2Id);
        bool UpdateApproved(int requesterId, int requesteeId, bool isApproved);
    }
}
