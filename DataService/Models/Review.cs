using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataService.Models
{
    /// <summary>
    /// Review Class
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Review ID
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// User ID who writes a reivew
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User name who writes a reivew
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User ID who approves the review
        /// </summary>
        public int ApprovalUserId { get; set; }

        /// <summary>
        /// Review status 
        /// </summary>
        public int ApprovalStatusId { get; set; }

        /// <summary>
        /// Approved date
        /// </summary>
        public DateTime ApprovalDate { get; set; }

        /// <summary>
        /// Review text
        /// </summary>
        public string WrittenReview { get; set; }
        
        /// <summary>
        /// Game of the reivew
        /// </summary>
        public Game Game { get; set; }

        public string ApprovalDateString { get; set; }

        /// <summary>
        /// Game rating Id
        /// </summary>
        public int RatingId { get; set; }

        /// <summary>
        /// Game rating
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Review Item Name
        /// </summary>
        public string ReviewItemTitle { get; set; }
    }

    /// <summary>
    /// ItemReview class
    /// </summary>
    public class ItemReview
    {
        /// <summary>
        /// Identifier of an item
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Identifier of a review
        /// </summary>
        public int ReviewId { get; set; }

    }
}
