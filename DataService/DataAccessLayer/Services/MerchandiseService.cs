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
    /// Merchandise service class
    /// </summary>
    public class MerchandiseService : ItemService, IMerchandiseService
    {
        #region Fields
        #endregion Fields 

        #region Methods

        public string CreateMerchandise(int itemId, int colourId, string size)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO dbo.Merchandise (ItemId, ColourId, Size) VALUES (@field1, @field2,@field3)";

                    command.Parameters.AddWithValue("@field1", itemId);
                    command.Parameters.AddWithValue("@field2", colourId);
                    command.Parameters.AddWithValue("@field3", size);
                    connection.Open();

                    int rowNum = command.ExecuteNonQuery();

                    if (rowNum > 0)
                    {
                        return "Merchandise Created";
                    }
                    else
                    {
                        return "Error Creating Merchandise";
                    }
                }

            }
        }

        public List<Merchandise> GetAllMerchandise()
        {
            List<Merchandise> merchandiseList = new List<Merchandise>();
            List<Item> itemList = GetAllItems();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.Merchandise;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            merchandiseList.Add(new Merchandise
                            {
                                ItemId = Convert.ToInt32(rdr["ItemId"]),
                                ColourId = Convert.ToInt32(rdr["ColourId"]),
                                Size = rdr["Size"].ToString(),
                                Item = itemList.FirstOrDefault(i => i.ItemId == Convert.ToInt32(rdr["ItemId"]))
                            });
                        }
                    }
                }
            }

            foreach (var merch in merchandiseList)
            {
                merch.Rating = GetMerchRating(merch.ItemId);
            }

            return merchandiseList;
        }

        public bool DeleteMerchandise(int itemId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete FROM dbo.Merchandise WHERE dbo.Merchandise.ItemId = @field;";
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

        public bool UpdateMerchandise(int itemId, int colourId, string size)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "UPDATE dbo.Merchandise " +
                        "SET ColourId = @field2 " +
                        ",Size = @field3 " +
                        "WHERE ItemId = @field1";

                    command.Parameters.AddWithValue("@field1", itemId);
                    command.Parameters.AddWithValue("@field2", colourId);
                    command.Parameters.AddWithValue("@field3", size);
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

        public Merchandise GetMerchandise(int itemId)
        {
            Item item = GetItem(itemId);
            Merchandise result = new Merchandise();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT * FROM dbo.Merchandise " +
                        "WHERE ItemId = @field;";
                    command.Parameters.AddWithValue("@field", itemId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        result.ItemId = Convert.ToInt32(rdr["ItemId"]);
                        result.ColourId = Convert.ToInt32(rdr["ColourId"]);
                        result.Size = rdr["Size"].ToString();
                        result.Item = item;
                    }
                }
            }
            return (result);
        }

        public List<Merchandise> SearchMerchandise(string search)
        {
            List<Merchandise> merchandiseList = new List<Merchandise>();
            List<Item> itemList = SearchItem(search);

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    int[] itemIds = itemList.Select(x => x.ItemId).ToArray();
                    string ids = string.Join(",", itemIds);
                    if (string.IsNullOrEmpty(ids))
                    {
                        return merchandiseList;
                    }
                    command.CommandText = "SELECT * FROM dbo.Merchandise WHERE ItemId IN (" + ids + ");";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            merchandiseList.Add(new Merchandise
                            {
                                ItemId = Convert.ToInt32(rdr["ItemId"]),
                                ColourId = Convert.ToInt32(rdr["ColourId"]),
                                Size = rdr["Size"].ToString(),
                                Item = itemList.FirstOrDefault(i => i.ItemId == Convert.ToInt32(rdr["ItemId"])),
                                Rating = GetMerchRating(Convert.ToInt32(rdr["ItemId"]))
                            });
                        }
                    }
                }
            }

            return merchandiseList;
        }

        public decimal GetMerchRating(int itemId)
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

        public MerchandiseService() : base()
        {

        }

        #endregion Constructors

    }


}
