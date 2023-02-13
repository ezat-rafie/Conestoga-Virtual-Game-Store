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
using System.Web.UI.WebControls;
using static Chocolist.Enums.Enum;

namespace Chocolist.Pages.Products
{
    /// <summary>
    /// CreateItem page code-hehind partial class
    /// </summary>
    public partial class CreateItem : System.Web.UI.Page
    {
        #region Fields

        /// <summary>
        /// Game service for data access
        /// </summary>
        IGameService gameService = new GameService();
        IMerchandiseService merchandiseService = new MerchandiseService();
        IColourService colourService = new ColourService();

        static List<Colour> allColours = new List<Colour>();

        #endregion Fields 

        #region Handlers

        /// <summary>
        /// Change input fields to the game craetion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonGame_Click(object sender, EventArgs e)
        {
            divItemInput.Visible = true;
        }

        /// <summary>
        /// Change input fields to the merchandise craetion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonMerchandise_Click(object sender, EventArgs e)
        {
            divItemInput.Visible = true;
        }

        /// <summary>
        /// Validate inputs and create item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
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
            if (isValid)
            {
                try
                {
                    int createdItemId;
                    if (radTypeGame.Checked)
                    {
                        createdItemId = gameService.CreateItem(name, description, price, quantity, gameTag);
                    } else
                    {
                        createdItemId = merchandiseService.CreateItem(name, description, price, quantity, gameTag);
                    }

                    divResult.Visible = true;

                    if (createdItemId == 0)
                    {
                        ResultMessage.Text = "Error creating item";
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

                            result = gameService.CreateGame(createdItemId, platformId, genreId);
                        } else
                        {
                            result = merchandiseService.CreateMerchandise(createdItemId, Int32.Parse(ddlSelectColour.SelectedValue), txtSize.Text);
                        }

                        ResultMessage.Text = result;
                    }
                }
                catch (Exception ex)
                {
                    Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
                }
            } else
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
                coverImage.Src = "/images/1.jpg";
                itemImage1.Src = "/images/1.jpg";
                itemImage2.Src = "/images/2.jpg";
                itemImage3.Src = "/images/3.jpg";
                divGame.Visible = true;
                divMerchandise.Visible = false;
            } else
            {
                coverImage.Src = "/images/MerchandiseAll.jpg";
                itemImage1.Src = "/images/merchandise2.jpg";
                itemImage2.Src = "/images/merchandise2.jpg";
                itemImage3.Src = "/images/merchandise2.jpg";
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

        #region Methods

        /// <summary>
        /// Initial page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                divItemInput.Visible = true;
                divResult.Visible = false;
                divMerchandise.Visible = false;
                divGame.Visible = true;
                PopulateColours();
            }

        }

        #endregion Methods 
       
    }
}