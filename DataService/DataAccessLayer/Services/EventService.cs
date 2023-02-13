/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : November 2022 
 */
using DataService.DataAccessLayer.IServices;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace DataService.DataAccessLayer.Services
{
    /// <summary>
    /// Event Service Class
    /// </summary>
    public class EventService : IEventService
    {
        /// <summary>
        /// Create Event
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="capacity"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="place"></param>
        /// <returns></returns>
        public int CreateEvent(string title, string description, double price, int capacity, DateTime startDate, DateTime endDate, string place)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO dbo.EVENT (Title, Description, StartDate, EndDate, Place, Capacity, Price) OUTPUT INSERTED.EventId VALUES (@field1, @field2, @field3, @field4, @field5, @field6, @field7)";

                    command.Parameters.AddWithValue("@field1", title);
                    command.Parameters.AddWithValue("@field2", description);
                    command.Parameters.AddWithValue("@field3", startDate);
                    command.Parameters.AddWithValue("@field4", endDate);
                    command.Parameters.AddWithValue("@field5", place);
                    command.Parameters.AddWithValue("@field6", capacity);
                    command.Parameters.AddWithValue("@field7", price);
                    connection.Open();

                    int createdId = (int)command.ExecuteScalar();
                    return createdId;
                }
            }
        }

        /// <summary>
        /// Delete Event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool DeleteEvent(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete FROM dbo.EVENT WHERE dbo.EVENT.EventId = @field;";
                    command.Parameters.AddWithValue("@field", eventId);
                    connection.Open();
                    int createdId = (int)command.ExecuteNonQuery();
                    if (createdId > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// Get All Events
        /// </summary>
        /// <returns></returns>
        public List<Event> GetAllEvents()
        {
            List<Event> eventList = new List<Event>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.EVENT;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            eventList.Add(new Event
                            {
                                EventId = Convert.ToInt32(rdr["EventId"]),
                                Title = rdr["Title"].ToString(),
                                Description = rdr["Description"].ToString(),
                                Price = Convert.ToDouble(rdr["Price"]),
                                Capacity = Convert.ToInt32(rdr["Capacity"]),
                                StartDate = Convert.ToDateTime(rdr["StartDate"]),
                                EndDate = Convert.ToDateTime(rdr["EndDate"]),
                                Place = rdr["Place"].ToString()
                            });
                        }
                    }
                }
            }

            return eventList;
        }

        /// <summary>
        /// Get all registered info
        /// </summary>
        /// <returns></returns>
        public List<EventRegistration> GetAllRegisteredInfo()
        {
            List<EventRegistration> eventRegistrationList = new List<EventRegistration>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.EVENTREGISTRATION;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            eventRegistrationList.Add(new EventRegistration
                            {
                                EventId = Convert.ToInt32(rdr["EventId"]),
                                UserId = Convert.ToInt32(rdr["UserId"])
                            }) ;
                        }
                    }
                }
            }

            return eventRegistrationList;
        }

        /// <summary>
        /// Get event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event GetEvent(int eventId)
        {
            Event theEvent = new Event();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.EVENT WHERE EventId = @field;";
                    command.Parameters.AddWithValue("@field", eventId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        theEvent.EventId = Convert.ToInt32(rdr["EventId"]);
                        theEvent.Title = rdr["Title"].ToString();
                        theEvent.Description = rdr["Description"].ToString();
                        theEvent.Price = Convert.ToDouble(rdr["Price"]);
                        theEvent.Capacity = Convert.ToInt32(rdr["Capacity"]);
                        theEvent.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                        theEvent.EndDate = Convert.ToDateTime(rdr["EndDate"]);
                        theEvent.Place = rdr["Place"].ToString();
                    }
                }
            }

            return theEvent;
        }

        /// <summary>
        /// Register a user to an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RegisterEvent(int eventId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO dbo.EVENTREGISTRATION (EventId, UserId) OUTPUT INSERTED.UserId VALUES (@field1, @field2)";

                    command.Parameters.AddWithValue("@field1", eventId);
                    command.Parameters.AddWithValue("@field2", userId);
                    connection.Open();

                    int createdId = (int)command.ExecuteScalar();
                    return createdId;
                }
            }
        }

        /// <summary>
        /// UnRegister a user from an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UnRegisterEvent(int eventId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM dbo.EVENTREGISTRATION WHERE EventId = @field1 AND UserId = @field2;";

                    command.Parameters.AddWithValue("@field1", eventId);
                    command.Parameters.AddWithValue("@field2", userId);
                    connection.Open();

                    int result = command.ExecuteNonQuery();
                    return (result > 0);
                }
            }
        }

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="eventid"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="capacity"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool UpdateEvent(int eventid, string title, string description, double price, int capacity, DateTime startDate, DateTime endDate, string place)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "UPDATE dbo.EVENT " +
                        "SET Title = @field1, Description = @field2, StartDate = @field3, EndDate = @field4, Place = @field5, Capacity = @field6, Price = @field7 " +
                        "WHERE EventId = @field8";

                    command.Parameters.AddWithValue("@field1", title);
                    command.Parameters.AddWithValue("@field2", description);
                    command.Parameters.AddWithValue("@field3", startDate);
                    command.Parameters.AddWithValue("@field4", endDate);
                    command.Parameters.AddWithValue("@field5", place);
                    command.Parameters.AddWithValue("@field6", capacity);
                    command.Parameters.AddWithValue("@field7", price);
                    command.Parameters.AddWithValue("@field8", eventid);
                    connection.Open();

                    int rows = (int)command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        result = true;
                    }
                    connection.Close();
                }

            }
            return (result);
        }

        public List<Event> SearchEvent(string search)
        {
            List<Event> eventList = new List<Event>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.EVENT WHERE Title LIKE '%' + @field + '%';";
                    command.Parameters.AddWithValue("@field", search);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            eventList.Add(new Event
                            {
                                EventId = Convert.ToInt32(rdr["EventId"]),
                                Title = rdr["Title"].ToString(),
                                Description = rdr["Description"].ToString(),
                                StartDate = Convert.ToDateTime(rdr["StartDate"]),
                                EndDate = Convert.ToDateTime(rdr["EndDate"]),
                                Place = rdr["Place"].ToString(),
                                Capacity = Convert.ToInt32(rdr["Capacity"]),
                                Price = Convert.ToDouble(rdr["Price"])
                            });
                        }
                    }
                }
            }

            return eventList;
        }
    }
}
