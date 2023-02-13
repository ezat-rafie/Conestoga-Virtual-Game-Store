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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.Services
{
    /// <summary>
    /// Game service class
    /// </summary>
    public class GameService : ItemService, IGameService
    {
        #region Fields
        #endregion Fields 

        #region Methods

        /// <summary>
        /// Create game to DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="platformId"></param>
        /// <returns></returns>
        public string CreateGame(int itemId, int platformId, int genreId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO dbo.Game (ItemId, PlatformId,genreId) VALUES (@field1, @field2,@field3)";

                    command.Parameters.AddWithValue("@field1", itemId);
                    command.Parameters.AddWithValue("@field2", platformId);
                    command.Parameters.AddWithValue("@field3", genreId);
                    connection.Open();

                    int rowNum = command.ExecuteNonQuery();

                    if (rowNum > 0)
                    {
                        return "Game Created";
                    }
                    else
                    {
                        return "Error Creating Game";
                    }
                }

            }
        }

        /// <summary>
        /// Get all games from DB
        /// </summary>
        /// <returns></returns>

        public List<Game> GetAllGames()
        {
            List<Game> gameList = new List<Game>();
            List<Item> itemList = GetAllItems();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.GAME;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            gameList.Add(new Game
                            {
                                ItemId = Convert.ToInt32(rdr["ItemId"]),
                                PlatformId = Convert.ToInt32(rdr["PlatformId"]),
                                GenreId = Convert.ToInt32(rdr["GenreId"]),
                                ESRBRating = rdr["ESRBRating"].ToString(),
                                Item = itemList.FirstOrDefault(i => i.ItemId == Convert.ToInt32(rdr["ItemId"]))
                            });
                        }
                    }
                }
            }

            foreach (var game in gameList)
            {
                game.Rating = GetGameRating(game.ItemId);
            }

            return gameList;
        }

        /// <summary>
        /// Delete a game from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool DeleteGame(int itemId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete FROM dbo.Game WHERE dbo.Game.ItemId = @field;";
                    command.Parameters.AddWithValue("@field", itemId);
                    connection.Open();
                    int rowNum = command.ExecuteNonQuery();
                    if (rowNum > 0)
                        return DeleteItem(itemId);
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// Update game to DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="platformId"></param>
        /// <returns></returns>
        public bool UpdateGame(int itemId, int platformId, int genreId)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "UPDATE dbo.Game " +
                        "SET PlatformId = @field2 " +
                        ",Genreid = @field3 " +
                        "WHERE ItemId = @field1";

                    command.Parameters.AddWithValue("@field1", itemId);
                    command.Parameters.AddWithValue("@field2", platformId);
                    command.Parameters.AddWithValue("@field3", genreId);
                    connection.Open();

                    int rowNum = command.ExecuteNonQuery();

                    if (rowNum > 0)
                    {
                        result = true;
                    }
                }
            }
            return (result);
        }

        /// <summary>
        /// Get a game from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Game GetGame(int itemId)
        {
            Item item = GetItem(itemId);
            Game result = new Game();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT * FROM dbo.GAME " +
                        "WHERE ItemId = @field;";
                    command.Parameters.AddWithValue("@field", itemId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        result.ItemId = Convert.ToInt32(rdr["ItemId"]);
                        result.PlatformId = Convert.ToInt32(rdr["PlatformId"]);
                        result.GenreId = Convert.ToInt32(rdr["GenreId"]);
                        result.ESRBRating = rdr["ESRBRating"].ToString();
                        result.Item = item;
                    }
                }
            }
            return (result);
        }

        public List<Game> SearchGames(string search)
        {
            List<Game> gameList = new List<Game>();
            List<Item> itemList = SearchItem(search);

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    int[] itemIds = itemList.Select(x => x.ItemId).ToArray();
                    string ids = string.Join(",", itemIds);
                    if (string.IsNullOrEmpty(ids))
                    {
                        return gameList;
                    }
                    command.CommandText = "SELECT * FROM dbo.GAME WHERE ItemId IN ("+ ids + ");";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            gameList.Add(new Game
                            {
                                ItemId = Convert.ToInt32(rdr["ItemId"]),
                                PlatformId = Convert.ToInt32(rdr["PlatformId"]),
                                ESRBRating = rdr["ESRBRating"].ToString(),
                                Item = itemList.FirstOrDefault(i => i.ItemId == Convert.ToInt32(rdr["ItemId"])),
                                Rating = GetGameRating(Convert.ToInt32(rdr["ItemId"]))
                            });
                        }
                    }
                }
            }

            return gameList;
        }

        /// <summary>
        /// Get a user's wishList
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Game> GetUserGames(int userId)
        {
            List<Game> gameList = new List<Game>();
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
                            gameList.Add(GetGame(Convert.ToInt32(rdr["ItemId"])));
                        }
                    }
                }
            }
            return gameList;
        }

        public decimal GetGameRating(int itemId)
        {
            ReviewService reviewService = new ReviewService();
            List<ItemReview> allReviews = reviewService.GetAllItemReview(itemId);
            List<int> ratings = new List<int>();
            if (allReviews.Count > 0)
            {
                foreach (ItemReview itemReview in allReviews)
                {
                    Review approvedReview = reviewService.GetApprovedReview(itemReview.ReviewId);
                    if (approvedReview != null)
                    {
                        ratings.Add(reviewService.GetItemRating(approvedReview.RatingId));
                    }
                }
                int fiveStarCount = ratings.Count(x => x == 5);
                int fourStarCount = ratings.Count(x => x == 4);
                int threeStarCount = ratings.Count(x => x == 3);
                int twoStarCount = ratings.Count(x => x == 2);
                int oneStarCount = ratings.Count(x => x == 1);
                decimal averageRating = 0;
                if ((fiveStarCount + fourStarCount + threeStarCount + twoStarCount + oneStarCount) > 0)
                {
                    double allRatings = (5 * fiveStarCount) + (4 * fourStarCount) + (3 * threeStarCount) + (2 * twoStarCount) + (1 * oneStarCount);
                    double totalRatings = fiveStarCount + fourStarCount + threeStarCount + twoStarCount + oneStarCount;
                    averageRating = (decimal)Math.Round((allRatings / totalRatings), 1);
                }
                return averageRating;
            }
            return 0;
        }
        #endregion Methods 

        #region Constructors

        /// <summary>
        /// Contructor of Game service
        /// </summary>
        public GameService() : base()
        {

        }

        #endregion Constructors

    }


}
