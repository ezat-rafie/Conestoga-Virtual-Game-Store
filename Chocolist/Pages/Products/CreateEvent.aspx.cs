/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : November 2022 
 */

using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chocolist.Pages.Products
{
    public partial class CreateEvent : System.Web.UI.Page
    {

        #region Fields

        /// <summary>
        /// Game service for data access
        /// </summary>
        IEventService eventService = new EventService();

        #endregion Fields 

        /// <summary>
        /// Load page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                divResult.Visible = false;
            }
        }

        /// <summary>
        /// btn Save handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEventSave_Click(object sender, EventArgs e)
        {
            string name = txtEventTitle.Text;
            string description = txtEventDescription.Text;
            int capacity = 0;
            double price = double.Parse(txtEventPrice.Text);
            DateTime startDate = DateTime.Parse(txtStartDate.Text + ' ' + txtStartTime.Text);
            DateTime endDate = DateTime.Parse(txtEndDate.Text + ' ' + txtEndTime.Text);
            string location = txtEventLocation.Text;

            Validations.Event eventValidation = new Validations.Event();

            eventValidation.Title = name;
            eventValidation.Description = description;
            eventValidation.Price = txtEventPrice.Text;
            eventValidation.StartDate = startDate;
            eventValidation.EndDate = endDate;
            eventValidation.Place = location;

            var context = new ValidationContext(eventValidation, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            Boolean isValid = Validator.TryValidateObject(eventValidation, context, validationResults, true);

            if (isValid)
            {
                try
                {
                    Boolean isStartDateValied = eventValidation.IsStartDateValid(startDate);

                    if(isStartDateValied == false)
                    {
                        divResult.Visible = true;
                        ResultMessage.Text = "Start date should ba greater than Now";
                        ResultMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    Boolean isEndDateValied = eventValidation.IsEndDateValid();

                    if(isEndDateValied == false)
                    {
                        divResult.Visible = true;
                        ResultMessage.Text = "End date should ba greater than Start date";
                        ResultMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    int createdItemId = eventService.CreateEvent(name, description, price, capacity, startDate, endDate, location);

                    divResult.Visible = true;

                    if (createdItemId == 0)
                    {
                        ResultMessage.Text = "Error creating item";
                        ResultMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        ResultMessage.ForeColor = System.Drawing.Color.Green;
                        ResultMessage.Text = "Event Created";
                        btnEventSave.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
                }
            }
            else
            {
                divResult.Visible = true;
                String errorOutput = "<ul>";
                foreach (var validationResult in validationResults)
                {
                    errorOutput += "<li>" + validationResult.ErrorMessage.ToString() + "</li>";
                }
                errorOutput += "</ul>";
                ResultMessage.Text = errorOutput;
            }

        }

        /// <summary>
        /// btn Back handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEventBack_Click(object sender, EventArgs e)
        {
            Session["ItemListTabIndex"] = 2;
            Response.Redirect("~/Pages/Products/ItemList.aspx");
        }
    }
}