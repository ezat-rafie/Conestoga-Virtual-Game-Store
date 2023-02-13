using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Chocolist.Enums.Enum;

namespace Chocolist.Pages.Cart
{
    public partial class ViewWishList : Page
    {
        /// <summary>
        /// Game service for data access
        /// </summary>
        IGameService gameService = new GameService();

        /// <summary>
        /// User id
        /// </summary>
        private int loggedInUserId = 0;

        /// <summary>
        /// Game wish list
        /// </summary>
        List<Game> wishList = new List<Game>();

        /// <summary>
        /// Initial page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var loggedIn = Session["LoggedIn"];
            if (loggedIn != null)
            {
                loggedInUserId = Convert.ToInt32(loggedIn);

                if (Session["FriendId"] != null)
                {
                    Profile friend = (new UserService()).GetProfile(Convert.ToInt32(Session["FriendId"]));
                    wishListTitle.Text = $"{friend.FirstName}'s Wish List";
                    shareButtons.Visible = false;
                    wishList = gameService.GetUserGames(Convert.ToInt32(Session["FriendId"]));
                } else
                {
                    wishList = gameService.GetUserGames(Convert.ToInt32(loggedInUserId));
                }

                foreach (var game in wishList)
                {
                    switch (game.PlatformId)
                    {
                        case 1:
                            game.PlatformName = PlatformEnum.Macintosh.ToString();
                            break;
                        case 2:
                            game.PlatformName = PlatformEnum.Windows.ToString();
                            break;
                        case 3:
                            game.PlatformName = PlatformEnum.Nintendo.ToString();
                            break;
                        case 4:
                            game.PlatformName = PlatformEnum.Playstation.ToString();
                            break;
                        case 5:
                            game.PlatformName = PlatformEnum.XBox.ToString();
                            break;
                        default:
                            break;
                    }

                    switch (game.GenreId)
                    {
                        case 1:
                            game.GenreName = GenreEnum.Action.ToString();
                            break;
                        case 2:
                            game.GenreName = GenreEnum.Adventure.ToString();
                            break;
                        case 3:
                            game.GenreName = GenreEnum.Strategy.ToString();
                            break;
                        case 4:
                            game.GenreName = GenreEnum.Family.ToString();
                            break;
                        case 5:
                            game.GenreName = GenreEnum.Puzzle.ToString();
                            break;
                        case 6:
                            game.GenreName = GenreEnum.Sports.ToString();
                            break;
                        default:
                            break;
                    }
                }
                DisplayItem();
            }
            else
            {
                Response.Redirect("~/Pages/User/Login.aspx");
            }
            Session["FriendId"] = null;
        }

        /// <summary>
        /// Handler for clicking btnAddCart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonWishListCart_Click(object sender, CommandEventArgs e)
        {
            int selectedItemId = Convert.ToInt32(e.CommandArgument);
            if (loggedInUserId > 0 && selectedItemId > 0)
            {
                ShopCarts.ShoppingCart.Instance.AddItem(selectedItemId);
                ((SiteMaster)(Page.Master)).UpdateCart(ShopCarts.ShoppingCart.Instance.GetItemCount());
                bool result = gameService.DeleteWishListItem(selectedItemId, loggedInUserId);

                if (result == true)
                {
                    Game game = wishList.FirstOrDefault(i => i.ItemId == selectedItemId);
                    wishList.Remove(game);
                    DisplayItem();
                }
            }
        }

        /// <summary>
        /// Diplay item by binding Repeater
        /// </summary>
        protected void DisplayItem()
        {
            rptWishListGame.DataSource = wishList;
            rptWishListGame.DataBind();
        }

        /// <summary>
        /// Remove a wish list item from DB and refresh the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveWishList(object sender, CommandEventArgs e)
        {
            try
            {
                int selectedItemId = Convert.ToInt32(e.CommandArgument);
               
                bool result = gameService.DeleteWishListItem(selectedItemId, loggedInUserId);

                if (result == true)
                {
                    Game game = wishList.FirstOrDefault(i => i.ItemId == selectedItemId);
                    wishList.Remove(game);
                    DisplayItem();
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }

        protected void rptWishListGame_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Session["FriendId"] != null)
                {
                    LinkButton btnRemove = e.Item.FindControl("btnRemove") as LinkButton;
                    btnRemove.Visible = false;

                    LinkButton btnCart = e.Item.FindControl("btnCart") as LinkButton;
                    btnCart.Visible = false;
                }
            }
        }
    }
}