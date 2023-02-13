/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : November 2022 
 */
using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using DataService.Models;
using System;
using System.Web.UI;

namespace Chocolist.Pages.Products
{
    public partial class ModifyEvent : System.Web.UI.Page
    {

        #region Fields

        /// <summary>
        /// Event service for data access
        /// </summary>
        IEventService eventService = new EventService();

        /// <summary>
        /// Event id from the query string
        /// </summary>
        private int eventId;

        #endregion Fields 

        /// <summary>
        /// Load page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int queryItemId = Convert.ToInt32(Request.QueryString["eventId"]);
                if (queryItemId == 0)
                {
                    Response.Redirect("~/Pages/Products/ItemList.aspx");
                    return;
                }

                try
                {
                    ViewState["modifyEventId"] = queryItemId;
                    this.eventId = queryItemId;
                    Event theEvent = eventService.GetEvent(eventId);
                    txtEventTitle.Text = theEvent.Title;
                    txtEventDescription.Text = theEvent.Description;
                    txtEventPrice.Text = theEvent.Price.ToString("N2");
                    txtEventLocation.Text = theEvent.Place;
                    txtStartDate.Text = theEvent.StartDate.Date.ToString("yyyy-MM-dd");
                    txtStartTime.Text = theEvent.StartDate.TimeOfDay.ToString();
                    txtEndDate.Text = theEvent.EndDate.Date.ToString("yyyy-MM-dd");
                    txtEndTime.Text = theEvent.EndDate.TimeOfDay.ToString();

                }
                catch (Exception)
                {
                    Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
                }
            }
            else
            {
                this.eventId = (int)ViewState["modifyEventId"];
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
            double price = double.Parse(txtEventPrice.Text);
            int capacity = 0;
            string check = txtStartDate.Text + txtStartTime.Text; 
            DateTime startDate = DateTime.Parse(txtStartDate.Text + ' ' +  txtStartTime.Text);
            DateTime endDate = DateTime.Parse(txtEndDate.Text + ' ' + txtEndTime.Text);
            string location = txtEventLocation.Text;

            try
            {
                bool updateSuccess = eventService.UpdateEvent(eventId, name, description, price, capacity, startDate, endDate, location);

                divResult.Visible = true;

                if (updateSuccess == false)
                {
                    ResultMessage.Text = "Error updating event";
                    ResultMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    ResultMessage.Text = "Event Updated";
                }
            }
            catch (Exception ex)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }
        }

        /// <summary>
        /// btn Back Handler
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