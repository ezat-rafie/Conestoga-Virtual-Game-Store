using DataService.DataAccessLayer.IServices;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.Services
{
    public class ReviewService : IReviewService
    {
        public bool ApproveReview(int reviewid, int approvalUserId, int approvalStatusId, DateTime approvalDate)
        {
            int updatedCount = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"UPDATE dbo.Review SET ApprovalUserId = {approvalUserId}, ApprovalStatusId = {approvalStatusId}, ApprovalDate = '{approvalDate.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                        $"WHERE ReviewId = {reviewid};";
                    connection.Open();
                    updatedCount = (int)sqlCmd.ExecuteNonQuery();
                }
            }
            return updatedCount > 0;
        }

        public bool RejectReview(int reviewid, int approvalUserId, int approvalStatusId, DateTime approvalDate)
        {
            int updatedCount = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"UPDATE dbo.Review SET ApprovalUserId = {approvalUserId}, ApprovalStatusId = {approvalStatusId}, ApprovalDate = '{approvalDate.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                        $"WHERE ReviewId = {reviewid};";
                    connection.Open();
                    updatedCount = (int)sqlCmd.ExecuteNonQuery();
                }
            }
            return updatedCount > 0;
        }

        public bool CreateReview(int userId, int status, string reviewText, int itemId, int rating)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                int createdId = 0;
                int itemReviewResult = 0;
                int ratingResult = 0;
                int itemRatingResult = 0;

                using (SqlCommand command = connection.CreateCommand())
                {

                    command.CommandText = "INSERT INTO dbo.Rating (UserId, RatingValue) OUTPUT INSERTED.RatingId VALUES (@field1, @field2);";

                    command.Parameters.AddWithValue("@field1", userId);
                    command.Parameters.AddWithValue("@field2", rating);
                    connection.Open();

                    ratingResult = (int)command.ExecuteScalar();
                    connection.Close();
                }

                if (ratingResult > 0)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO dbo.Review (UserId, ApprovalStatusId, WrittenReview, ApprovalUserId, RatingId) OUTPUT INSERTED.ReviewId VALUES (@field1, @field2, @field3, @field4, @field5);";

                        command.Parameters.AddWithValue("@field1", userId);
                        command.Parameters.AddWithValue("@field2", status);
                        command.Parameters.AddWithValue("@field3", reviewText);
                        command.Parameters.AddWithValue("@field4", 0);
                        command.Parameters.AddWithValue("@field5", ratingResult);
                        connection.Open();

                        createdId = (int)command.ExecuteScalar();

                        connection.Close();
                    }
                }

                if(createdId > 0)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText = "INSERT INTO dbo.ItemReview (ItemId, ReviewId) OUTPUT INSERTED.ReviewId VALUES (@field1, @field2);";

                        command.Parameters.AddWithValue("@field1", itemId);
                        command.Parameters.AddWithValue("@field2", createdId);
                        connection.Open();

                        itemReviewResult = (int)command.ExecuteScalar();
                        connection.Close();
                    }
                }

                if (itemReviewResult > 0)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText = "INSERT INTO dbo.ItemRating (ItemId, RatingId) OUTPUT INSERTED.RatingId VALUES (@field1, @field2);";

                        command.Parameters.AddWithValue("@field1", itemId);
                        command.Parameters.AddWithValue("@field2", ratingResult);
                        connection.Open();

                        itemRatingResult = (int)command.ExecuteScalar();
                        connection.Close();
                        if (itemRatingResult > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return false;

            }
        }

        public bool DeleteReview(int itemId, int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"Delete FROM dbo.ItemReview WHERE dbo.ItemReview.ItemId = {itemId} and dbo.ItemReview.reviewId = {reviewId} ;";
                    connection.Open();
                    int rowNum = command.ExecuteNonQuery();
                    if (rowNum > 0)
                        return DeleteItem(reviewId);
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// Delete an item from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool DeleteItem(int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete FROM dbo.Review WHERE dbo.Review.ReviewId = @field;";
                    command.Parameters.AddWithValue("@field", reviewId);
                    connection.Open();
                    int createdId = (int)command.ExecuteNonQuery();
                    if (createdId > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        public List<ItemReview> GetAllItemReview(int itemId)
        {
            List<ItemReview> itemReviewList = new List<ItemReview>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM dbo.ItemReview WHERE ItemId={itemId};";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            itemReviewList.Add(new ItemReview
                            {
                                ItemId = itemId,
                                ReviewId = Convert.ToInt32(rdr["ReviewId"])
                            });
                        }
                    }
                }
            }
            return itemReviewList;
        }

        public ItemReview GetAItemReview(int reviewId)
        {
            ItemReview itemReview = new ItemReview();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM dbo.ItemReview WHERE ReviewId={reviewId};";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            itemReview.ReviewId = reviewId;
                            itemReview.ItemId = Convert.ToInt32(rdr["ItemId"]);
                        }
                    }
                }
            }
            return itemReview;
        }

        public List<Review> GetAllPendingReviews()
        {
            List<Review> reviews = new List<Review>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM dbo.Review WHERE ApprovalStatusId = 7;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            reviews.Add(new Review()
                            {
                                RatingId = rdr["RatingId"] as int? ?? 0,
                                ReviewId = Convert.ToInt32(rdr["ReviewId"]),
                                UserId = Convert.ToInt32(rdr["UserId"]),
                                ApprovalStatusId = Convert.ToInt32(rdr["ApprovalStatusId"]),
                                WrittenReview = rdr["WrittenReview"] == null? "" : rdr["WrittenReview"].ToString()
                            });
                        }
                        return reviews;
                    }
                }
            }
            return reviews;
        }

        public Review GetApprovedReview(int reviewId)
        {
            Review review = new Review();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM dbo.Review WHERE ReviewId={reviewId} and ApprovalStatusId = 8;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            review.ReviewId = reviewId;
                            review.UserId = Convert.ToInt32(rdr["UserId"]);
                            review.ApprovalStatusId = Convert.ToInt32(rdr["ApprovalStatusId"]);
                            review.ApprovalDate = Convert.ToDateTime(rdr["ApprovalDate"]);
                            review.WrittenReview = rdr["WrittenReview"].ToString();
                            review.RatingId = rdr["RatingId"] as int? ?? 0;
                        }
                        return review;
                    }
                }
            }
            return null;
        }

        public Review GetPendingReview(int reviewId)
        {
            Review review = new Review();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM dbo.Review WHERE ReviewId={reviewId} and ApprovalStatusId = 7;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            review.ReviewId = reviewId;
                            review.UserId = Convert.ToInt32(rdr["UserId"]);
                            review.ApprovalStatusId = Convert.ToInt32(rdr["ApprovalStatusId"]);
                            review.WrittenReview = rdr["WrittenReview"].ToString();
                        }
                        return review;
                    }
                }
            }
            return null;
        }

        public Review GetRejectedReview(int reviewId)
        {
            Review review = new Review();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM dbo.Review WHERE ReviewId={reviewId} and ApprovalStatusId = 15;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            review.ReviewId = reviewId;
                            review.UserId = Convert.ToInt32(rdr["UserId"]);
                            review.ApprovalStatusId = Convert.ToInt32(rdr["ApprovalStatusId"]);
                            review.WrittenReview = rdr["WrittenReview"].ToString();
                        }
                        return review;
                    }
                }
            }
            return null;
        }

        public bool UpdateReview(int userId, int status, string reviewText)
        {
            throw new NotImplementedException();
        }

        public int GetItemRating(int ratingId)
        {
            int rating = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT RatingValue FROM dbo.Rating WHERE RatingId = @field1;";
                    command.Parameters.AddWithValue("@field1", ratingId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            rating = Convert.ToInt32(rdr["RatingValue"]);
                        }
                        return rating;
                    }
                }
            }
            return rating;
        }
    }
}
