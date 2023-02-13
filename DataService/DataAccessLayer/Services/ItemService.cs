/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using DataService.DataAccessLayer.IServices;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DataService.DataAccessLayer.Services
{
    /// <summary>
    /// Item service class
    /// </summary>
    public class ItemService : IItemService
    {
        #region Fields
        #endregion Fields 

        #region Methods

        /// <summary>
        /// Create an item to DB
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public int CreateItem(string title, string description, double price, int quantity, string gameTag)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO dbo.ITEM (Name, Description, Price, QuantityInStock, GameTag) OUTPUT INSERTED.ItemId VALUES (@field1, @field2, @field3, @field4, @field5)";

                    command.Parameters.AddWithValue("@field1", title);
                    command.Parameters.AddWithValue("@field2", description);
                    command.Parameters.AddWithValue("@field3", price);
                    command.Parameters.AddWithValue("@field4", quantity);
                    command.Parameters.AddWithValue("@field5", gameTag);
                    connection.Open();

                    int createdId = (int)command.ExecuteScalar();

                    return createdId;
                }

            }
        }

        /// <summary>
        /// Get all items from DB
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAllItems()
        {
            List<Item> itemList = new List<Item>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.ITEM;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            itemList.Add(new Item
                            {
                                ItemId = Convert.ToInt32(rdr["ItemId"]),
                                Name = rdr["Name"].ToString(),
                                Description = rdr["Description"].ToString(),
                                Price = Convert.ToDouble(rdr["Price"]),
                                QuantityInStock = Convert.ToInt32(rdr["QuantityInStock"]),
                                GameTag = rdr["GameTag"].ToString()
                            });
                        }
                    }
                }
            }

            return itemList;
        }

        /// <summary>
        /// Delete an item from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool DeleteItem(int itemId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete FROM dbo.ITEM WHERE dbo.ITEM.ItemId = @field;";
                    command.Parameters.AddWithValue("@field", itemId);
                    connection.Open();
                    int createdId = (int)command.ExecuteNonQuery();
                    if (createdId > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// Get an item from DB
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public Item GetItem(int itemid)
        {
            Item item = new Item();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.ITEM WHERE ItemID = @field;";
                    command.Parameters.AddWithValue("@field", itemid);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        item.ItemId = Convert.ToInt32(rdr["ItemId"]);
                        item.Name = rdr["Name"].ToString();
                        item.Description = rdr["Description"].ToString();
                        item.Price = Convert.ToDouble(rdr["Price"]);
                        item.QuantityInStock = Convert.ToInt32(rdr["QuantityInStock"]);
                        item.GameTag = rdr["GameTag"].ToString();
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// Update an item to DB
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool UpdateItem(int itemid, string title, string description, double price, int quantity, string gameTag)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "UPDATE dbo.ITEM " +
                        "SET Name = @field1, Description = @field2, Price = @field3, QuantityInStock = @field4, GameTag = @field6 " +
                        "WHERE ItemId = @field5";

                    command.Parameters.AddWithValue("@field1", title);
                    command.Parameters.AddWithValue("@field2", description);
                    command.Parameters.AddWithValue("@field3", price);
                    command.Parameters.AddWithValue("@field4", quantity);
                    command.Parameters.AddWithValue("@field5", itemid);
                    command.Parameters.AddWithValue("@field6", gameTag);
                    connection.Open();

                    int rows = (int)command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        result = true;
                    }
                    connection.Close();
                }

            }
            return (result);
        }

        public List<Item> SearchItem(string search)
        {
            List<Item> itemList = new List<Item>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.ITEM WHERE Name LIKE '%' + @field + '%';";
                    command.Parameters.AddWithValue("@field", search);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            itemList.Add(new Item
                            {
                                ItemId = Convert.ToInt32(rdr["ItemId"]),
                                Name = rdr["Name"].ToString(),
                                Description = rdr["Description"].ToString(),
                                Price = Convert.ToDouble(rdr["Price"]),
                                QuantityInStock = Convert.ToInt32(rdr["QuantityInStock"])
                            });
                        }
                    }
                }
            }

            return itemList;
        }

        public List<Item> SearchItemGameTag(string search)
        {
            List<Item> itemList = new List<Item>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.ITEM WHERE GameTag LIKE '%' + @field + '%';";
                    command.Parameters.AddWithValue("@field", search);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            itemList.Add(new Item
                            {
                                ItemId = Convert.ToInt32(rdr["ItemId"]),
                                Name = rdr["Name"].ToString(),
                                Description = rdr["Description"].ToString(),
                                Price = Convert.ToDouble(rdr["Price"]),
                                QuantityInStock = Convert.ToInt32(rdr["QuantityInStock"]),
                                GameTag = rdr["GameTag"].ToString()
                            });
                        }
                    }
                }
            }

            return itemList;
        }

        #region UserItem
        /// <summary>
        /// Add an item to a wish list of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int CreateWishList(int userId, int itemId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO dbo.USERITEM (UserId, ItemId) OUTPUT INSERTED.ItemId VALUES (@field1, @field2)";

                    command.Parameters.AddWithValue("@field1", userId);
                    command.Parameters.AddWithValue("@field2", itemId);
                    connection.Open();

                    int createdId = (int)command.ExecuteScalar();

                    return createdId;
                }
            }
        }

        public bool IsItemInWishList(int userId, int itemId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT * FROM dbo.UserItem " +
                        "WHERE ItemId = @field1 and UserId = @field2;";
                    command.Parameters.AddWithValue("@field1", itemId);
                    command.Parameters.AddWithValue("@field2", userId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Item> GetUserItems(int userId)
        {
            List<Item> itemList = new List<Item>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM dbo.UserItem WHERE UserId={userId};";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            itemList.Add(GetItem(Convert.ToInt32(rdr["ItemId"])));                            
                        }
                    }
                }
            }
            return itemList;
        }

        /// <summary>
        /// Delete a wish list item from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool DeleteWishListItem(int itemId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete FROM dbo.UserItem WHERE dbo.UserItem.ItemId = @field1 and dbo.UserItem.UserId = @field2;";
                    command.Parameters.AddWithValue("@field1", itemId);
                    command.Parameters.AddWithValue("@field2", userId);
                    connection.Open();
                    int createdId = (int)command.ExecuteNonQuery();
                    if (createdId > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        #endregion

        #endregion Methods 

    }
}
