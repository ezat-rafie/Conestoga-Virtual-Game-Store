/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using DataService.Models;
using System.Collections.Generic;

namespace DataService.DataAccessLayer.IServices
{
    /// <summary>
    /// Inferface of Item Service 
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Create an item to DB
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        int CreateItem(string title, string description, double price, int quantity, string gameTag);

        /// <summary>
        /// Get all items from DB
        /// </summary>
        /// <returns></returns>
        List<Item> GetAllItems();

        /// <summary>
        /// Delete an item from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        bool DeleteItem(int itemId);

        /// <summary>
        /// Get an item from DB
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        Item GetItem(int itemid);
        
        /// <summary>
        /// Update an item to DB
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        bool UpdateItem(int itemid, string title, string description, double price, int quantity, string gameTag);

        /// <summary>
        /// Add an item to a wish list of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        int CreateWishList(int userId, int itemId);

        /// <summary>
        /// Add an item to the wish list
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        bool IsItemInWishList(int userId, int itemId);

        /// <summary>
        /// Search Item
        /// </summary>
        /// <returns></returns>
        List<Item> SearchItem(string search);
        List<Item> SearchItemGameTag(string search);

        /// <summary>
        /// Get Wish List of the selected user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Item> GetUserItems(int userId);

        /// <summary>
        /// Delete a wish list item from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        bool DeleteWishListItem(int itemId, int userId);
    }
}
