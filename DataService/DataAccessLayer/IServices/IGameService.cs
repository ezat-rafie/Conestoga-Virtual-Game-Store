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
    /// Inferface of Game Service 
    /// </summary>
    public interface IGameService : IItemService
    {
        /// <summary>
        /// Create game to DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="platformId"></param>
        /// <returns></returns>
        string CreateGame(int itemId, int platformId,int genreId);

        /// <summary>
        /// Update game to DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="platformId"></param>
        /// <returns></returns>
        bool UpdateGame(int itemId, int platformId,int genreId);

        /// <summary>
        /// Get a game from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Game GetGame(int itemId);

        /// <summary>
        /// Get all games from DB
        /// </summary>
        /// <returns></returns>
        List<Game> GetAllGames();

        /// <summary>
        /// Delete a game from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        bool DeleteGame(int itemId);

        /// <summary>
        /// Search Games
        /// </summary>
        /// <returns></returns>
        List<Game> SearchGames(string search);

        /// <summary>
        /// Get a user's wishList
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Game> GetUserGames(int userId);
    }
}
