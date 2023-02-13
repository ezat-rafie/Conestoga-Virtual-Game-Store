/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Xml.Linq;
using static Chocolist.Enums.Enum;

namespace Chocolist.Pages.Products
{
    /// <summary>
    /// Preview page code-hehind partial class
    /// </summary>
    public partial class Preview : System.Web.UI.Page
    {
        #region Fields

        /// <summary>
        /// Game service for data access
        /// </summary>
        IItemService itemService = new ItemService();
        IGameService gameService = new GameService();
        IMerchandiseService merchandiseService = new MerchandiseService();
        IColourService colourService = new ColourService();

        /// <summary>
        /// Review service for data access
        /// </summary>
        IReviewService reviewService = new ReviewService();

        /// <summary>
        /// User service for data access
        /// </summary>
        IUserService userService = new UserService();

        /// <summary>
        /// Invoice service for data access
        /// </summary>
        IInvoiceService invoiceService = new InvoiceService();

        /// <summary>
        /// Item id from the query string
        /// </summary>
        private int itemId;

        /// <summary>
        /// User id
        /// </summary>
        private int loggedInUserId = 0;

        
        public List<DisplayInvoice> displayInvoices;

        private Review rejectedReview = new Review();

        int rejectedReviewId = 0;

        public int totalRating = 0;
        public string averageRating = "0";

        #endregion Fields 

        #region Handlers
        /// <summary>
        /// Route to the Default page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        /// <summary>
        /// Handler for clicking btnAddCart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonCart_Click(object sender, EventArgs e)
        {
            if (loggedInUserId == 0)
            {
                Session["PreviewItem"] = itemId;
                Response.Redirect("~/Pages/User/Login.aspx");
            }
            if (loggedInUserId > 0 && itemId > 0)
            {
                ShopCarts.ShoppingCart.Instance.AddItem(itemId);
                ((SiteMaster)(Page.Master)).UpdateCart(ShopCarts.ShoppingCart.Instance.GetItemCount());
                divResult.Visible = true;
                ResultMessage.Text = "Item added to Cart";
            }
        }

        /// <summary>
        /// Handler for clicking btnWishList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonWishList_Click(object sender, EventArgs e)
        {
            if (loggedInUserId == 0)
            {
                Session["PreviewItem"] = itemId;
                Response.Redirect("~/Pages/User/Login.aspx");
            }

            if (loggedInUserId > 0 && itemId > 0)
            {
                int createdItemId = gameService.CreateWishList(loggedInUserId, itemId);
                divResult.Visible = true;

                if (createdItemId == 0)
                {
                    ResultMessage.Text = "Error creating wish list";
                    ResultMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    ResultMessage.Text = "Wish list Added";
                }

                ButtonWishList.Enabled = false;
            }
        }

        /// <summary>
        /// Handler for clicking btnReview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonAddReview_Click(object sender, EventArgs e)
        {
            reqReviewText.Enabled = true;
            reviewTextBox.Visible = true;
            reviewTextBox.Enabled = true;
            reviewTextBox.ReadOnly = false;
            btnAddReview.Visible = false;
            reqReviewText.Enabled = true;
            btnReviewItem.Enabled = true;
            btnReviewItem.Visible = true;
            messageLabel.Text = "";
            oneStar.Checked = false;
            twoStar.Checked = false;
            threeStar.Checked = false;
            fourStar.Checked = false;
            fiveStar.Checked = false;
        }

        /// <summary>
        /// Handler for clicking btnReview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReview_Click(object sender, EventArgs e)
        {
            int gameRating = getGameRate();
            bool result = reviewService.CreateReview(loggedInUserId, Convert.ToInt32(StatusEnum.ReviewApprovalPending), reviewTextBox.Text, itemId, gameRating);

            if (result == true)
            {
                rejectedReviewId = Convert.ToInt32(Session["RejectedReviewId"]);
                if (btnAddReview.Text == "Re Submit" && rejectedReviewId > 0)
                {
                    bool deleteResult = reviewService.DeleteReview(itemId, rejectedReviewId);
                    if(deleteResult == false)
                    {
                        MessageBox.Show("Review is not updated", "Error Creation", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        Session["RejectedReviewId"] = null;
                        rejectedReviewId = 0;
                    }
                }

                MessageBox.Show("Review is created/updated and wating for being approved", "Review Created", MessageBoxButton.OK, MessageBoxImage.Information);

                btnAddReview.Visible = false;
                btnReviewItem.Visible = false;
                reviewTextBox.Visible = false;
                messageLabel.Text = "Your review is waiting to get approved";
            }
            else
            {
                MessageBox.Show("Review is not created", "Error Creation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion Handlers

        #region Methods

        private int getGameRate()
        {
            if (oneStar.Checked)
            {
                return 1;
            }
            else if (twoStar.Checked)
            {
                return 2;
            }
            else if (threeStar.Checked)
            {
                return 3;
            }
            else if (fourStar.Checked)
            {
                return 4;
            }
            else if (fiveStar.Checked)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Initial page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            reqReviewText.Enabled = false;
            var loggedIn = Session["LoggedIn"];
            if (loggedIn != null)
            {
                loggedInUserId = Convert.ToInt32(loggedIn);
            }
            else
            {
                btnAddReview.Visible = false;
                btnReviewItem.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                int queryItemId = Convert.ToInt32(Request.QueryString["itemId"]);
                if (queryItemId == 0)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                try
                {
                    ViewState["previewItemId"] = queryItemId;
                    this.itemId = queryItemId;
                    Item item = itemService.GetItem(itemId);
                    Game game = gameService.GetGame(itemId);
                    Merchandise merchandise = merchandiseService.GetMerchandise(itemId);

                    lblTitle.InnerText = item.Name;
                    lblDescription.InnerText = item.Description;
                    lblPrice.InnerText = "$" + item.Price.ToString("N2");
                    if (game.ItemId != 0)
                    {
                        coverImage.Src = "/images/1.jpg";
                        thumbImage.Src = "/images/1.jpg";
                        posterImage.Src = "/images/1.jpg";
                        divPlatform.Visible = true;
                        divMerchandise.Visible = false;
                        string platformText = "";
                        if (game.PlatformId == Convert.ToInt32(PlatformEnum.Macintosh))
                        {
                            platformText += " <i class=\"fa-brands fa-apple\"></i>";
                        }
                        else if (game.PlatformId == Convert.ToInt32(PlatformEnum.Windows))
                        {
                            platformText += " <i class=\"fa-brands fa-windows\"></i>";
                        }
                        else if (game.PlatformId == Convert.ToInt32(PlatformEnum.Nintendo))
                        {
                            platformText += " <i class=\"fa-solid fa-gamepad\"></i>";
                        }
                        else if (game.PlatformId == Convert.ToInt32(PlatformEnum.Playstation))
                        {
                            platformText += " <i class=\"fa-brands fa-playstation\"></i>";
                        }
                        else if (game.PlatformId == Convert.ToInt32(PlatformEnum.XBox))
                        {
                            platformText += " <i class=\"fa-brands fa-xbox\"></i>";
                        }
                        lblPlatforms.InnerHtml = platformText;
                    }
                    if (merchandise.ItemId != 0)
                    {
                        coverImage.Src = "/images/merchandiseAll.jpg";
                        thumbImage.Src = "/images/merchandiseAll.jpg";
                        posterImage.Src = "/images/merchandise2.jpg";
                        divPlatform.Visible = false;
                        divMerchandise.Visible = true;
                        merchandise.ColourName = colourService.GetColour(merchandise.ColourId).Name;
                        merchandise.CanvasStyle = colourService.GetColour(merchandise.ColourId).Value;
                        lblSize.InnerText = merchandise.Size;
                        lblColour.InnerText = merchandise.ColourName;
                        canvasColour.Attributes.CssStyle.Remove("Background");
                        canvasColour.Attributes.CssStyle.Add("Background", merchandise.CanvasStyle);
                    }
                }
                catch (Exception)
                {
                    Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
                }


                bool value = itemService.IsItemInWishList(loggedInUserId, itemId);

                if (value == true)
                {
                    ButtonWishList.Enabled = false;
                }

                reviewTextBox.Visible = false;
                btnReviewItem.Visible = false;

                List<ItemReview> results = reviewService.GetAllItemReview(itemId);
                List<Review> reviews = new List<Review>();
                Review pendingReview = new Review();
                

                if (results.Count > 0)
                {
                    foreach (ItemReview itemReview in results)
                    {

                        Review reviewToAdd = reviewService.GetApprovedReview(itemReview.ReviewId);

                        if (reviewToAdd != null)
                        {
                            reviewToAdd.Rating = reviewService.GetItemRating(reviewToAdd.RatingId);
                            reviewToAdd.UserName = userService.GetProfile(reviewToAdd.UserId).DisplayName;
                            reviewToAdd.ApprovalDateString = reviewToAdd.ApprovalDate.ToString().Substring(0, 10);
                            reviews.Add(reviewToAdd);
                        }

                        pendingReview = reviewService.GetPendingReview(itemReview.ReviewId);
                        if (pendingReview != null && pendingReview.UserId == loggedInUserId)
                        {
                            btnAddReview.Visible = false;
                            reviewTextBox.Visible = false;
                            reviewTextBox.ReadOnly = true;
                            btnReviewItem.Enabled = false;
                            reviewTextBox.Text = pendingReview.WrittenReview;
                            reviewTextBox.Enabled = false;
                            reviewTextBox.ReadOnly = true;
                            messageLabel.Text = "Your review is waiting to get approved";
                        }

                        rejectedReview = reviewService.GetRejectedReview(itemReview.ReviewId);
                        if (rejectedReview != null && rejectedReview.UserId == loggedInUserId)
                        {
                            btnAddReview.Visible = true;
                            reviewTextBox.Visible = true;
                            reviewTextBox.Enabled = false;
                            btnReviewItem.Enabled = false;
                            btnReviewItem.Visible = false;
                            reviewTextBox.Text = rejectedReview.WrittenReview;
                            btnAddReview.Text = "Re Submit";
                            messageLabel.Text = "Your review is rejected";
                            Session["RejectedReviewId"] = rejectedReview.ReviewId; 
                        }

                    }
                    
                    rptReview.DataSource = reviews;
                    rptReview.DataBind();
                }
                int fiveStarCount = reviews.Count(x => x.Rating == 5);
                int fourStarCount = reviews.Count(x => x.Rating == 4);
                int threeStarCount = reviews.Count(x => x.Rating == 3);
                int twoStarCount = reviews.Count(x => x.Rating == 2);
                int oneStarCount = reviews.Count(x => x.Rating == 1);
                
                if ((fiveStarCount + fourStarCount + threeStarCount + twoStarCount + oneStarCount) > 0)
                {
                    double allRatings = (5 * fiveStarCount) + (4 * fourStarCount) + (3 * threeStarCount) + (2 * twoStarCount) + (1 * oneStarCount);
                    double totalRatings = fiveStarCount + fourStarCount + threeStarCount + twoStarCount + oneStarCount;
                    averageRating = Math.Round((allRatings / totalRatings), 1).ToString();
                }
                totalRating = fiveStarCount + fourStarCount + threeStarCount + twoStarCount + oneStarCount;

            }
            else
            {
                this.itemId = (int)ViewState["previewItemId"];
            }


            
        }

        #endregion Methods 

    }
}