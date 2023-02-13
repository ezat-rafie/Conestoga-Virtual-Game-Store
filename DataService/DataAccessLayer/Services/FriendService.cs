using DataService.DataAccessLayer.IServices;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataService.DataAccessLayer.Services
{
    public class FriendService : IFriendService
    {
        #region Friends
        public List<FriendUser> GetFriends(int userId)
        {
            List<FriendUser> friendList = new List<FriendUser>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                string friendIdList = "";
                connection.Open();
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"SELECT * FROM dbo.[LinkedUser(FriendsFamily)] WHERE Approved=1 AND RequesterUserId={userId};";
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            friendIdList += string.IsNullOrWhiteSpace(friendIdList) ? "" : ",";
                            friendIdList += reader["RequesteeUserId"].ToString();
                        }
                    }
                }
                connection.Close();
                connection.Open();
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"SELECT * FROM dbo.[LinkedUser(FriendsFamily)] WHERE Approved=1 AND RequesteeUserId={userId};";
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            friendIdList += string.IsNullOrWhiteSpace(friendIdList) ? "" : ",";
                            friendIdList += reader["RequesterUserId"].ToString();
                        }
                    }
                }
                connection.Close();

                if (string.IsNullOrWhiteSpace(friendIdList)) return friendList;

                connection.Open();
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"SELECT UserId, FirstName, LastName, DisplayName, EmailAddress FROM dbo.[User]" +
                        $"WHERE UserId IN ({friendIdList});";
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    FriendUser friend = new FriendUser();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            friend = new FriendUser()
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DisplayName = reader["DisplayName"].ToString(),
                                EmailAddress = reader["EmailAddress"].ToString()
                                //Approved = Convert.ToBoolean(Convert.ToInt32(reader["Approved"]))
                            };
                            friendList.Add(friend);
                        }
                    }
                }
            }
            return friendList;
        }

        public List<FriendUser> GetRequests(int userId, bool isSent)
        {
            List<FriendUser> requestList = new List<FriendUser>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    // TODO: in case of (userId = l.RequesteeUserId) and distinct
                    string cmd = isSent ?
                        $"ON u.UserId = l.RequesteeUserId WHERE l.RequesterUserId = {userId} "
                        : $"ON u.UserId = l.RequesterUserId WHERE l.RequesteeUserId = {userId} ";

                    sqlCmd.CommandText = $"SELECT u.UserId, u.FirstName, u.LastName, u.DisplayName, u.EmailAddress, Approved " +
                            $"FROM dbo.[User] u JOIN dbo.[LinkedUser(FriendsFamily)] l " +
                            $"{cmd} AND Approved=0 AND Blocked<>1;";
                    connection.Open();

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    FriendUser friend = new FriendUser();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            friend = new FriendUser()
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DisplayName = reader["DisplayName"].ToString(),
                                EmailAddress = reader["EmailAddress"].ToString(),
                                Approved = Convert.ToBoolean(Convert.ToInt32(reader["Approved"]))
                            };
                            requestList.Add(friend);
                        }
                    }
                }
            }
            return requestList;
        }

        public List<FriendUser> SearchUsers(int userId, string keyword)
        {
            List<FriendUser> foundUserList = new List<FriendUser>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"SELECT UserId, UserTypeId, FirstName, LastName, DisplayName, EmailAddress FROM dbo.[User] " +
                        $"WHERE UserTypeId=2 AND UserId <> {userId} AND (FirstName LIKE '%{keyword.ToLower()}%' " +
                        $"OR LastName LIKE '%{keyword.ToLower()}%' OR DisplayName LIKE '%{keyword.ToLower()}%' " +
                        $"OR EmailAddress LIKE '%{keyword.ToLower()}%');";
                    connection.Open();

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    FriendUser foundUser = new FriendUser();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            foundUser = new FriendUser()
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DisplayName = reader["DisplayName"].ToString(),
                                EmailAddress = reader["EmailAddress"].ToString()
                            };
                            foundUserList.Add(foundUser);
                        }
                    }
                }
            }

            List<FriendUser> friends = GetFriends(userId);
            foreach (FriendUser friend in friends)
            {
                foundUserList.Remove(friend);
            }

            return foundUserList;
        }

        public bool CreateLinkedUser(int requesterId, int requesteeId)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                if (this.CheckDuplicate(requesterId, requesteeId))
                {
                    Debug.Print("Already in DB");
                    return false;
                }

                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = $"INSERT INTO dbo.[LinkedUser(FriendsFamily)] VALUES(@requesterId, @requesteeId, 0,0);";
                        sqlCmd.Parameters.AddWithValue("@requesterId", requesterId);
                        sqlCmd.Parameters.AddWithValue("@requesteeId", requesteeId);
                        connection.Open();
                        id = (int)sqlCmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.Print(e.Message);
                        Debug.Print("Failed in Saving into DB");
                    }
                }
            }
            return id > 0;
        }
        public bool DeleteLinkedUser(int user1Id, int user2Id)
        {
            int deletedCount = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = "Delete FROM dbo.[LinkedUser(FriendsFamily)] " +
                        "WHERE (RequesterUserId=@user1 AND RequesteeUserId=@user2)" +
                        "OR (RequesterUserId=@user2 AND RequesteeUserId=@user1)";
                    sqlCmd.Parameters.AddWithValue("@user1", user1Id);
                    sqlCmd.Parameters.AddWithValue("@user2", user2Id);
                    connection.Open();
                    deletedCount = (int)sqlCmd.ExecuteNonQuery();
                }
            }
            return deletedCount > 0;
        }

        /// <summary>
        /// return TRUE when there are duplicates
        /// </summary>
        /// <param name="user1Id"></param>
        /// <param name="user2Id"></param>
        /// <returns></returns>
        public bool CheckDuplicate(int user1Id, int user2Id)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    // TODO: in case of (userId = l.RequesteeUserId) and distinct
                    sqlCmd.CommandText = $"SELECT * FROM dbo.[LinkedUser(FriendsFamily)] " +
                        $"WHERE (RequesterUserId=@user1 AND RequesteeUserId=@user2)" +
                        "OR (RequesterUserId=@user2 AND RequesteeUserId=@user1);";
                    sqlCmd.Parameters.AddWithValue("@user1", user1Id);
                    sqlCmd.Parameters.AddWithValue("@user2", user2Id);
                    connection.Open();

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows) return true;
                }
            }
            return false;
        }

        public bool UpdateApproved(int requesterId, int requesteeId, bool isApproved)
        {
            int updatedCount = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    string apr = isApproved ? "Approved=1" : "Blocked=1";
                    sqlCmd.CommandText = $"UPDATE dbo.[LinkedUser(FriendsFamily)] SET {apr} " +
                        $"WHERE (RequesterUserId=@requesterId AND RequesteeUserId=@requesteeId);";
                    sqlCmd.Parameters.AddWithValue("@requesterId", requesterId);
                    sqlCmd.Parameters.AddWithValue("@requesteeId", requesteeId);
                    connection.Open();
                    updatedCount = (int)sqlCmd.ExecuteNonQuery();
                }
            }
            return updatedCount > 0;
        }
        #endregion
    }
}
