/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : November 2022 
 */
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.IServices
{
    /// <summary>
    /// Interface of Event Service
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Create an event to DB
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="capacity"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        int CreateEvent(string title, string description, double price, int capacity, DateTime startDate, DateTime endDate, string place);

        /// <summary>
        /// Get all events from DB
        /// </summary>
        /// <returns></returns>
        List<Event> GetAllEvents();

        /// <summary>
        /// Delete an event from DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        bool DeleteEvent(int eventId);

        /// <summary>
        /// Get an event from DB
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        Event GetEvent(int eventid);

        /// <summary>
        /// Update an event to DB
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        bool UpdateEvent(int eventid, string title, string description, double price, int capacity, DateTime startDate, DateTime endDate, string place);

        /// <summary>
        /// Register Event 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int RegisterEvent(int eventId, int userId);

        /// <summary>
        /// UnRegister Event 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UnRegisterEvent(int eventId, int userId);

        /// <summary>
        /// Get all event registration information
        /// </summary>
        /// <returns></returns>
        List<EventRegistration> GetAllRegisteredInfo();

        /// <summary>
        /// Event search
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        List<Event> SearchEvent(string keyWord);

    }
}
