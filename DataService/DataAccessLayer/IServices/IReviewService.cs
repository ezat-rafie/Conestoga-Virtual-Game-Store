using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.IServices
{
    public interface IReviewService
    {

        /// <summary>
        /// Create a review to DB
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <param name="reviewText"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        bool CreateReview(int userId, int status, string reviewText, int itemId, int rating);

        /// <summary>
        /// Get all reviews from DB
        /// </summary>
        /// <returns></returns>
        List<Review> GetAllPendingReviews();

        /// <summary>
        /// Delete a review from DB
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        bool DeleteReview(int itemId, int reviewId);

        /// <summary>
        /// Get an approved review from DB
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        Review GetApprovedReview(int reviewId);

        /// <summary>
        /// Get a pending review from DB
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        Review GetPendingReview(int reviewId);

        /// <summary>
        /// Update a review to DB
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <param name="reviewText"></param>
        bool UpdateReview(int userId, int status, string reviewText);

        /// <summary>
        /// Approve a review
        /// </summary>
        /// <param name="approvalUserId"></param>
        /// <param name="approvalStatusId"></param>
        /// <param name="approvalDate"></param>
        /// <param name="reviewid"></param>
        /// <returns></returns>
        bool ApproveReview(int reviewid, int approvalUserId, int approvalStatusId, DateTime approvalDate);

        bool RejectReview(int reviewid, int approvalUserId, int approvalStatusId, DateTime approvalDate);
        Review GetRejectedReview(int reviewId);

        /// <summary>
        /// Get all ItemReview information
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        List<ItemReview> GetAllItemReview(int itemId);

        /// <summary>
        /// Get a ItemReview information
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        ItemReview GetAItemReview(int reviewId);
        /// <summary>
        /// Get items rating
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        int GetItemRating(int ratingId);
    }
}
