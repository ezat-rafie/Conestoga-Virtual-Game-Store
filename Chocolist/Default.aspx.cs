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
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Chocolist.Enums.Enum;

namespace Chocolist
{
    /// <summary>
    /// Default page code-hehind partial class
    /// </summary>
    public partial class _Default : Page
    {
        #region Fields

        /// <summary>
        /// Game service for data access
        /// </summary>
        IGameService gameService = new GameService();
        IMerchandiseService merchandiseService = new MerchandiseService();
        IColourService colourService = new ColourService();

        /// <summary>
        /// Game service for data access
        /// </summary>
        IEventService eventService = new EventService();

        /// <summary>
        /// Game list to store game data from DB
        /// </summary>
        List<Game> gameList = new List<Game>();
        List<Merchandise> merchandiseList = new List<Merchandise>();

        /// <summary>
        /// Event list from DB
        /// </summary>
        List<Event> eventList = new List<Event>();

        private int loggedInUserId = 0;

        public static int pageTab = 0;


        #endregion Fields 

        #region Handlers

        protected void DefaultMain_MenuItemClick(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            pageTab = index;
            MultiView1.ActiveViewIndex = index;
        }

        protected void SearchPage(object sender, CommandEventArgs e)
        {
            try
            {
                switch (pageTab)
                {
                    //Games
                    case 0:
                        gameList = gameService.SearchGames(txtSearch.Text);

                        if (gameList.Count < 1)
                        {
                            gameSearchResult.Visible = true;
                            gameSearchResult.Text = "No Data Found";
                        }
                        foreach (var game in gameList)
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
                        break;
                    //Merch
                    case 1:
                        merchandiseList = merchandiseService.SearchMerchandise(txtSearch.Text);

                        if (merchandiseList.Count < 1)
                        {
                            merchandiseSearchResult.Visible = true;
                            merchandiseSearchResult.Text = "No Data Found";
                        }
                        foreach (Merchandise merchandise in merchandiseList)
                        {
                            merchandise.ColourName = colourService.GetColour(merchandise.ColourId).Name;
                            merchandise.CanvasStyle = colourService.GetColour(merchandise.ColourId).Value;
                        }
                        DisplayItem();
                        break;
                    //Event
                    case 2:
                        eventList = eventService.SearchEvent(txtSearch.Text);

                        if (eventList.Count < 1)
                        {
                            EventSearchResult.Visible = true;
                            EventSearchResult.Text = "No Data Found";
                        }
                        DisplayItem();
                        break;
                }
            }
            catch (Exception ex)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }
        }
        #endregion Handlers

        #region Methods

        /// <summary>
        /// Initial page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var loggedIn = Session["LoggedIn"];
            txtSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSearch.ClientID + "')");
            if (loggedIn != null)
            {
                loggedInUserId = Convert.ToInt32(loggedIn);
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["tabIndex"] != null)
                {
                    int itemTypeIndex = Convert.ToInt32(Request.QueryString["tabIndex"]);
                    if (itemTypeIndex >=0 && itemTypeIndex <= 2)
                    {
                        MultiView1.ActiveViewIndex = itemTypeIndex;
                        Session["DefaultTabIndex"] = null;
                    }
                }
            }
            try
            {
                gameSearchResult.Visible = false;
                merchandiseSearchResult.Visible = false;
                EventSearchResult.Visible = false;
                var index = Session["DefaultTabIndex"];
                if (index != null)
                {
                    int indexInt = Int32.Parse(index.ToString());
                    pageTab = indexInt;
                    MultiView1.ActiveViewIndex = indexInt;
                    DefaultMain.FindItem(indexInt.ToString()).Selected = true;
                    Session["DefaultTabIndex"] = null;
                }

                gameList = gameService.GetAllGames();
                merchandiseList = merchandiseService.GetAllMerchandise();
                var pricesort = ddlPrice.SelectedValue;
                if (pricesort != "All")
                {
                    if (pricesort.ToLower() == "high")
                    {
                        gameList = gameList.OrderByDescending(x=>x.Item.Price).ToList();
                        merchandiseList = merchandiseList.OrderByDescending(x => x.Item.Price).ToList();
                    }
                    else 
                    { 
                        gameList = gameList.OrderBy(x=>x.Item.Price).ToList();
                        merchandiseList = merchandiseList.OrderBy(x => x.Item.Price).ToList();
                    }
                }
                var genre = ddlGenre.SelectedValue;
                if (genre != "All")
                {
                    gameList = gameList.Where(x => x.GenreId == Convert.ToInt32(genre)).ToList();

                }
                var platform = ddlPlatform.SelectedValue;

                if (platform != "All")
                { 
                        gameList = gameList.Where(x=>x.PlatformId == Convert.ToInt32(platform)).ToList();

                }

                var range = ddlPriceRange.SelectedValue;
                if (range != "All")
                {
                    if (range == "50")
                    {
                        gameList = gameList.Where(x => x.Item.Price <= 50).ToList();
                    }
                    else if (range == "100")
                    {
                        gameList = gameList.Where(x => x.Item.Price <= 100 && x.Item.Price >= 50).ToList();

                    }
                    else { 
                        gameList = gameList.Where(x => x.Item.Price > 100).ToList();

                    }

                }

                foreach (var game in gameList)
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
                foreach (Merchandise merchandise in merchandiseList)
                {
                    merchandise.ColourName = colourService.GetColour(merchandise.ColourId).Name;
                    merchandise.CanvasStyle = colourService.GetColour(merchandise.ColourId).Value;
                }

                eventList = eventService.GetAllEvents();

                DisplayItem();
            }
            catch (Exception ex)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }
        }

        /// <summary>
        /// Diplay item by binding Repeater
        /// </summary>
        public void DisplayItem()
        {
            rptGame.DataSource = gameList;
            rptGame.DataBind();

            rptMerchandise.DataSource = merchandiseList;
            rptMerchandise.DataBind();

            rptEvent.DataSource = eventList;
            rptEvent.DataBind();           
        }

        #endregion Methods 

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            int itemId = int.Parse(((LinkButton)sender).CommandArgument);
            if (loggedInUserId == 0)
            {
                Session["PreviewItem"] = itemId;
                Response.Redirect("~/Pages/User/Login.aspx");
            } else if (itemId > 0)
            {
                ShopCarts.ShoppingCart.Instance.AddItem(itemId);
                ((SiteMaster)(Page.Master)).UpdateCart(ShopCarts.ShoppingCart.Instance.GetItemCount());
            }
        }
    }
}