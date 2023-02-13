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
using System.Linq;
using System.Web.UI.WebControls;
using static Chocolist.Enums.Enum;

namespace Chocolist.Pages.Products
{
    /// <summary>
    /// ItemList page code-hehind partial class
    /// </summary>
    public partial class ItemList : System.Web.UI.Page
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
        /// Game list from DB
        /// </summary>
        List<Game> gameList = new List<Game>();
        List<Merchandise> merchandiseList = new List<Merchandise>();
        /// <summary>
        /// Event list from DB
        /// </summary>
        List<Event> eventList = new List<Event>();

        #endregion Fields

        #region Handlers

        protected void ItemListMenuClick(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
        }

        /// <summary>
        /// Remove item from DB and refresh the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveGame(object sender, CommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                bool result = gameService.DeleteGame(itemId);

                if (result == true)
                {
                    Game game = gameList.FirstOrDefault(i => i.ItemId == itemId);
                    gameList.Remove(game);
                    DisplayItem();
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }

        protected void RemoveMerchandise(object sender, CommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                bool result = merchandiseService.DeleteMerchandise(itemId);

                if (result == true)
                {
                    Merchandise merchandise = merchandiseList.FirstOrDefault(i => i.ItemId == itemId);
                    merchandiseList.Remove(merchandise);
                    DisplayItem();
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }

        /// <summary>
        /// Remove item from DB and refresh the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveEvent(object sender, CommandEventArgs e)
        {
            try
            {
                int eventId = Convert.ToInt32(e.CommandArgument);
                bool result = eventService.DeleteEvent(eventId);

                if (result == true)
                {
                    Event theEvent = eventList.FirstOrDefault(i => i.EventId == eventId);
                    eventList.Remove(theEvent);
                    DisplayItem();
                }
            }
            catch (Exception)
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
            try
            {
                var index = Session["ItemListTabIndex"];
                if (index != null)
                {
                    int indexInt = Int32.Parse(index.ToString());
                    MultiView1.ActiveViewIndex = indexInt;
                    ItemListMenu.FindItem(indexInt.ToString()).Selected = true;
                    Session["ItemListTabIndex"] = null;
                }

                gameList = gameService.GetAllGames();
                merchandiseList = merchandiseService.GetAllMerchandise();

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
        protected void DisplayItem()
        {
            rptGame.DataSource = gameList;
            rptGame.DataBind();

            rptMerchandise.DataSource = merchandiseList;
            rptMerchandise.DataBind();

            rptEvent.DataSource = eventList;
            rptEvent.DataBind();

        }

        #endregion Methods

    }
}