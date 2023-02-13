using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls.Primitives;
using static Chocolist.Enums.Enum;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Chocolist.Pages.Products
{
    public partial class ItemReviews : System.Web.UI.Page
    {

        /// <summary>
        /// Game service for data access
        /// </summary>
        IGameService gameService = new GameService();

        /// <summary>
        /// Merch service for data access
        /// </summary>
        IMerchandiseService merchandiseService = new MerchandiseService();

        /// <summary>
        /// User id
        /// </summary>
        private int loggedInUserId = 0;

        /// <summary>
        /// Game list with pending reviews
        /// </summary>
        List<Game> gamesWithReviews = new List<Game>();

        /// <summary>
        /// Pending review list
        /// </summary>
        List<Review> reviewsToCheck = new List<Review>();

        /// <summary>
        /// Review service for data access
        /// </summary>
        IReviewService reviewService = new ReviewService();

        /// <summary>
        /// User service for data access
        /// </summary>
        IUserService userService = new UserService();

        /// <summary>
        /// Selected review id
        /// </summary>
        private int selectedReviewId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            var loggedIn = Session["LoggedIn"];
            if (loggedIn != null)
            {
                loggedInUserId = Convert.ToInt32(loggedIn);

                List<Review> results = reviewService.GetAllPendingReviews();

                if (results.Count > 0)
                {
                    foreach (Review review in results)
                    {
                        if (review != null )
                        {
                            review.Rating = reviewService.GetItemRating(review.RatingId);
                            review.UserName = userService.GetProfile(review.UserId).DisplayName;
                            ItemReview itemReview = reviewService.GetAItemReview(review.ReviewId);
                            
                            review.Game = gameService.GetGame(itemReview.ItemId);
                            if (review.Game.Item == null)
                            {
                                review.ReviewItemTitle = merchandiseService.GetMerchandise(itemReview.ItemId).Item.Name;
                            }
                            else
                            {
                                review.ReviewItemTitle = review.Game.Item.Name;
                            }
                            reviewsToCheck.Add(review);

                            switch (review.Game.PlatformId)
                            {
                                case 1:
                                    review.Game.PlatformName = PlatformEnum.Macintosh.ToString();
                                    break;
                                case 2:
                                    review.Game.PlatformName = PlatformEnum.Windows.ToString();
                                    break;
                                case 3:
                                    review.Game.PlatformName = PlatformEnum.Nintendo.ToString();
                                    break;
                                case 4:
                                    review.Game.PlatformName = PlatformEnum.Playstation.ToString();
                                    break;
                                case 5:
                                    review.Game.PlatformName = PlatformEnum.XBox.ToString();
                                    break;
                                default:
                                    break;
                            }

                            switch (review.Game.GenreId)
                            {
                                case 1:
                                    review.Game.GenreName = GenreEnum.Action.ToString();
                                    break;
                                case 2:
                                    review.Game.GenreName = GenreEnum.Adventure.ToString();
                                    break;
                                case 3:
                                    review.Game.GenreName = GenreEnum.Strategy.ToString();
                                    break;
                                case 4:
                                    review.Game.GenreName = GenreEnum.Family.ToString();
                                    break;
                                case 5:
                                    review.Game.GenreName = GenreEnum.Puzzle.ToString();
                                    break;
                                case 6:
                                    review.Game.GenreName = GenreEnum.Sports.ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                DisplayItem();
            }
            else
            {
                Response.Redirect("~/Pages/User/Login.aspx");
            }
        }

        /// <summary>
        /// Diplay item by binding Repeater
        /// </summary>
        protected void DisplayItem()
        {
            rptPendingReview.DataSource = reviewsToCheck;
            rptPendingReview.DataBind();
        }


        protected void ButtonRejectReview_Click(object sender, CommandEventArgs e)
        {
            try
            {
                selectedReviewId = Convert.ToInt32(e.CommandArgument);
                if (loggedInUserId > 0 && selectedReviewId > 0)
                {
                    bool result = reviewService.RejectReview(selectedReviewId, loggedInUserId, Convert.ToInt32(StatusEnum.ReviewRejected), DateTime.Now);

                    if (result == true)
                    {
                        reviewsToCheck.Remove(reviewsToCheck.FirstOrDefault(r => r.ReviewId == selectedReviewId));
                        DisplayItem();
                    }
                } 
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }
        }

        protected void ButtonPublish_Click(object sender, CommandEventArgs e)
        {
            selectedReviewId = Convert.ToInt32(e.CommandArgument);
            if (loggedInUserId > 0 && selectedReviewId > 0)
            {
                bool result = reviewService.ApproveReview(selectedReviewId, loggedInUserId, Convert.ToInt32(StatusEnum.ReviewApproved), DateTime.Now);
               
                if (result == true)
                {
                    reviewsToCheck.Remove(reviewsToCheck.FirstOrDefault(r => r.ReviewId == selectedReviewId));
                    DisplayItem();
                }
            }
        }
    }
}