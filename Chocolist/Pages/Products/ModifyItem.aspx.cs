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
using System.ComponentModel.DataAnnotations;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Chocolist.Enums.Enum;

namespace Chocolist.Pages.Products
{
    /// <summary>
    /// ModifyItem page code-hehind partial class
    /// </summary>
    public partial class ModifyItem : System.Web.UI.Page
    {

        #region Fields

        /// <summary>
        /// Game service for data access
        /// </summary>
        IItemService itemService = new ItemService();
        IGameService gameService = new GameService();
        IMerchandiseService merchandiseService = new MerchandiseService();
        IColourService colourService = new ColourService();

        static List<Colour> allColours = new List<Colour>();

        /// <summary>
        /// Item id from the query string
        /// </summary>
        private int itemId;

        #endregion Fields


        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int queryItemId = Convert.ToInt32(Request.QueryString["itemId"]);
                if (queryItemId == 0)
                {
                    Response.Redirect("~/Pages/Products/ItemList.aspx");
                    return;
                }
                PopulateColours();
                try
                {
                    ViewState["modifyItemId"] = queryItemId;
                    this.itemId = queryItemId;
                    Item item = itemService.GetItem(itemId);
                    Game game = gameService.GetGame(itemId);
                    Merchandise merchandise = merchandiseService.GetMerchandise(itemId);
                    txtTitle.Text = item.Name;
                    txtDescription.Text = item.Description;
                    txtPrice.Text = item.Price.ToString("N2");
                    txtQuantity.Text = item.QuantityInStock.ToString();
                    if (game.ItemId != 0) {
                        coverImage.Src = "/images/1.jpg";
                        itemImage1.Src = "/images/1.jpg";
                        itemImage2.Src = "/images/2.jpg";
                        itemImage3.Src = "/images/3.jpg";
                        divGame.Visible = true;
                        divMerchandise.Visible = false;
                        if (game.PlatformId == Convert.ToInt32(PlatformEnum.Macintosh))
                        {
                            radMacintosh.Checked = true;
                        }
                        else if (game.PlatformId == Convert.ToInt32(PlatformEnum.Windows))
                        {
                            radWindows.Checked = true;
                        }
                        else if (game.PlatformId == Convert.ToInt32(PlatformEnum.Nintendo))
                        {
                            radNintendo.Checked = true;
                        }
                        else if (game.PlatformId == Convert.ToInt32(PlatformEnum.Playstation))
                        {
                            radPlayStation.Checked = true;
                        }
                        else if (game.PlatformId == Convert.ToInt32(PlatformEnum.XBox))
                        {
                            radXbox.Checked = true;
                        }

                        if (game.GenreId == Convert.ToInt32(GenreEnum.Action))
                        {
                            radAction.Checked = true;
                        }
                        else if (game.GenreId == Convert.ToInt32(GenreEnum.Adventure))
                        {
                            radAdventure.Checked = true;
                        }
                        else if (game.GenreId == Convert.ToInt32(GenreEnum.Family))
                        {
                            radFamily.Checked = true;
                        }
                        else if (game.GenreId == Convert.ToInt32(GenreEnum.Puzzle))
                        {
                            radPuzzle.Checked = true;
                        }
                        else if (game.GenreId == Convert.ToInt32(GenreEnum.Sports))
                        {
                            radSports.Checked = true;
                        }
                        else if (game.GenreId == Convert.ToInt32(GenreEnum.Strategy))
                        {
                            radStrategy.Checked = true;
                        }
                    }
                    if (merchandise.ItemId != 0)
                    {
                        coverImage.Src = "/images/MerchandiseAll.jpg";
                        itemImage1.Src = "/images/merchandise2.jpg";
                        itemImage2.Src = "/images/merchandise2.jpg";
                        itemImage3.Src = "/images/merchandise2.jpg";
                        divMerchandise.Visible = true;
                        divGame.Visible = false;
                        txtSize.Text = merchandise.Size;
                        ddlSelectColour.SelectedValue = merchandise.ColourId.ToString();
                        canvasColour.Attributes.CssStyle.Remove("background");
                        if (ddlSelectColour.SelectedValue != "0")
                        {
                            canvasColour.Attributes.CssStyle.Add("background", colourService.GetColour(Int32.Parse(ddlSelectColour.SelectedValue)).Value);
                        }
                    }
                }
                catch (Exception)
                {
                    Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
                }
            }
            else
            {
                this.itemId = (int)ViewState["modifyItemId"];
            }
        }

        #endregion Mdthods


        #region Handlers

        /// <summary>
        /// Validate inputs and update item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtTitle.Text;
                string description = txtDescription.Text;
                double price = double.Parse(txtPrice.Text);
                int quantity = int.Parse(txtQuantity.Text);
                string gameTag = txtGameTag.Text;

                Validations.ItemCRUD itemValidation = new Validations.ItemCRUD();

                itemValidation.title = name;
                itemValidation.description = description;
                itemValidation.price = txtPrice.Text;
                itemValidation.quantity = txtQuantity.Text;

                var context = new ValidationContext(itemValidation, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                Boolean isValid = Validator.TryValidateObject(itemValidation, context, validationResults, true);

                divResult.Visible = true;

                if (isValid)
                {
                    bool updateSuccess = gameService.UpdateItem(itemId, name, description, price, quantity, gameTag);
                    if (!updateSuccess)
                    {
                        ResultMessage.Text = "Error updating item";
                        ResultMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        string result = "";
                        if (radTypeGame.Checked)
                        {
                            int platformId = 0;
                            if (radMacintosh.Checked)
                                platformId = Convert.ToInt32(PlatformEnum.Macintosh);
                            else if (radWindows.Checked)
                                platformId = Convert.ToInt32(PlatformEnum.Windows);
                            else if (radNintendo.Checked)
                                platformId = Convert.ToInt32(PlatformEnum.Nintendo);
                            else if (radPlayStation.Checked)
                                platformId = Convert.ToInt32(PlatformEnum.Playstation);
                            else
                                platformId = Convert.ToInt32(PlatformEnum.XBox);

                            int genreId = 0;
                            if (radAction.Checked)
                                genreId = Convert.ToInt32(GenreEnum.Action);
                            else if (radAdventure.Checked)
                                genreId = Convert.ToInt32(GenreEnum.Adventure);
                            else if (radFamily.Checked)
                                genreId = Convert.ToInt32(GenreEnum.Family);
                            else if (radPuzzle.Checked)
                                genreId = Convert.ToInt32(GenreEnum.Puzzle);
                            else if (radStrategy.Checked)
                                genreId = Convert.ToInt32(GenreEnum.Strategy);
                            else
                                genreId = Convert.ToInt32(GenreEnum.Sports);

                            updateSuccess = gameService.UpdateGame(itemId, platformId, genreId);
                        } else
                        {
                            updateSuccess = merchandiseService.UpdateMerchandise(itemId, Int32.Parse(ddlSelectColour.SelectedValue), txtSize.Text);
                        }

                        ResultMessage.Text = updateSuccess ? "Update was successful" : "Update was not successful";
                    }
                }
                else
                {
                    String errorOutput = "<ul>";
                    foreach (var validationResult in validationResults)
                    {
                        errorOutput += "<li>" + validationResult.ErrorMessage.ToString() + "</li>";
                    }
                    errorOutput += "</ul>";
                    ResultMessage.Text = errorOutput;
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }

        private void PopulateColours()
        {
            allColours = colourService.GetAllColours();
            ddlSelectColour.Items.Clear();
            foreach (Colour colour in allColours)
            {
                ListItem ddlAdd = new ListItem();
                ddlAdd.Value = colour.ColourId.ToString();
                ddlAdd.Text = colour.Name;
                ddlSelectColour.Items.Add(ddlAdd);
            }
            ddlSelectColour.SelectedIndex = 0;
            canvasColour.Attributes.CssStyle.Remove("background");
            if (ddlSelectColour.SelectedValue != "0")
            {
                canvasColour.Attributes.CssStyle.Add("background", colourService.GetColour(Int32.Parse(ddlSelectColour.SelectedValue)).Value);
            }
        }

        protected void GameType_Changed(object sender, EventArgs e)
        {
            if (radTypeGame.Checked)
            {
                divGame.Visible = true;
                divMerchandise.Visible = false;
            }
            else
            {
                divGame.Visible = false;
                divMerchandise.Visible = true;
            }
        }


        protected void Colour_Changed(object sender, EventArgs e)
        {
            canvasColour.Attributes.CssStyle.Remove("background");
            if (ddlSelectColour.SelectedValue != "0")
            {
                canvasColour.Attributes.CssStyle.Add("background", colourService.GetColour(Int32.Parse(ddlSelectColour.SelectedValue)).Value);
            }
        }
        /// <summary>
        /// Route to the ItemList page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Products/ItemList.aspx");
        }

        #endregion Handlers




    }
}