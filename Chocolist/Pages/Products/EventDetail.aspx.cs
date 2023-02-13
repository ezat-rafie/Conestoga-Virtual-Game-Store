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
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Chocolist.Pages.Products
{
    public partial class EventDetail : System.Web.UI.Page
    {
        #region Fields

        /// <summary>
        /// Event service for data access
        /// </summary>
        IEventService eventService = new EventService();

        /// <summary>
        /// User service for data access
        /// </summary>
        IUserService userService = new UserService(); 

        /// <summary>
        /// Event id from the query string
        /// </summary>
        private int eventId;

        /// <summary>
        /// User list from DB
        /// </summary>
        public List<DataService.Models.User> employeeList = new List<DataService.Models.User>();

        /// <summary>
        /// Loged in user id
        /// </summary>
        public int loggedInUserId = 0;

        /// <summary>
        /// User profile
        /// </summary>
        public Profile userProfile = new Profile();

        /// <summary>
        /// User profile
        /// </summary>
        public List<Profile> profileList = new List<Profile>();

        /// <summary>
        /// Event registration list
        /// </summary>
        public List<EventRegistration> eventRegistrationList = new List<EventRegistration>();

        /// <summary>
        /// Attendees name list
        /// </summary>
        public List<string> attendeesNameList = new List<string>();

        #endregion Fields 

        /// <summary>
        /// Load page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int queryItemId = Convert.ToInt32(Request.QueryString["eventId"]);
            if (queryItemId == 0)
            {
                Response.Redirect("~/Pages/Products/Default.aspx");
                return;
            }
            ViewState["EventId"] = queryItemId;
            this.eventId = queryItemId;

            var loggedIn = Session["LoggedIn"];
            if (loggedIn != null)
            {
                loggedInUserId = Convert.ToInt32(loggedIn);
                if (loggedInUserId > 0)
                {
                    userProfile = userService.GetProfile(loggedInUserId);
                }
            }
            else
            {
                Session["EventDetailItem"] = eventId;
                Response.Redirect("~/Pages/User/Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                try
                {
                    profileList = userService.GetAllProfiles();
                    eventRegistrationList = eventService.GetAllRegisteredInfo();
                    var attendees = eventRegistrationList.FindAll(x => x.EventId == eventId);
                    if (attendees.Any())
                    {                     
                        foreach( EventRegistration eventRegistration in attendees)
                        {
                            attendeesNameList.Add(profileList.FirstOrDefault(p => p.UserId == eventRegistration.UserId).FirstName);
                        }
                    }

                    if (eventRegistrationList.Any(x=> x.UserId == loggedInUserId && x.EventId == eventId))
                    {
                        btnRSVP.Visible = false;
                        btnCancelRSVP.Visible = true;
                    }
                    else
                    {
                        btnRSVP.Visible = true;
                        btnCancelRSVP.Visible = false;
                    }

                    DisplayInformation();

                }
                catch (Exception)
                {
                    Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
                }
            }
            else
            {
                this.eventId = (int)ViewState["EventId"];
            }
        }

        /// <summary>
        /// btn Save handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonRSVP_Click(object sender, EventArgs e)
        {
            
            try
            {
                int registeredUserId = eventService.RegisterEvent(eventId, loggedInUserId);

                if (registeredUserId < 1)
                {
                    ResultMessage.Text = "Error Registering event";
                    ResultMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    profileList = userService.GetAllProfiles();
                    eventRegistrationList = eventService.GetAllRegisteredInfo();
                    var attendees = eventRegistrationList.FindAll(x => x.EventId == eventId);
                    if (attendees.Any())
                    {
                        foreach (EventRegistration eventRegistration in attendees)
                        {
                            attendeesNameList.Add(profileList.FirstOrDefault(p => p.UserId == eventRegistration.UserId).FirstName);
                        }
                    }
                    btnRSVP.Visible = false;
                    btnCancelRSVP.Visible = true;
                    DisplayInformation();
                    ResultMessage.Text = "Event Registered";

                }
            }
            catch (Exception ex)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }
        }

        /// <summary>
        /// btn Save handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonCancelRSVP_Click(object sender, EventArgs e)
        {

            try
            {
                bool success = eventService.UnRegisterEvent(eventId, loggedInUserId);

                if (success)
                {
                    ResultMessage.Text = "Successfully un RSVPed from event";
                    ResultMessage.ForeColor = System.Drawing.Color.Green;
                    profileList = userService.GetAllProfiles();
                    eventRegistrationList = eventService.GetAllRegisteredInfo();
                    var attendees = eventRegistrationList.FindAll(x => x.EventId == eventId);
                    if (attendees.Any())
                    {
                        foreach (EventRegistration eventRegistration in attendees)
                        {
                            attendeesNameList.Add(profileList.FirstOrDefault(p => p.UserId == eventRegistration.UserId).FirstName);
                        }
                    }
                    btnCancelRSVP.Visible = false;
                    btnRSVP.Visible = true;
                    DisplayInformation();
                    return;
                }
                else
                {
                    ResultMessage.Text = "There was a problem canceling the RSVP.";
                    ResultMessage.ForeColor = System.Drawing.Color.Red;
                    btnRSVP.Visible = false;
                    btnCancelRSVP.Visible = true;
                    DisplayInformation();
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
            Session["DefaultTabIndex"] = 2;
            Response.Redirect("~/Default.aspx");
        }


        protected void DisplayInformation()
        {
            Event theEvent = eventService.GetEvent(eventId);
            txtEventTitle.Text = theEvent.Title;
            txtEventDescription.Text = theEvent.Description;
            txtEventPrice.Text = theEvent.Price.ToString("N2");
            txtEventLocation.Text = theEvent.Place;
            txtStartDate.Text = theEvent.StartDate.Date.ToString("yyyy-MM-dd");
            txtStartTime.Text = theEvent.StartDate.TimeOfDay.ToString();
            txtEndDate.Text = theEvent.EndDate.Date.ToString("yyyy-MM-dd");
            txtEndTime.Text = theEvent.EndDate.TimeOfDay.ToString();
            txtAttendance.Text = string.Join(", ", attendeesNameList);
        }
    }
}