using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Models
{
    public class Event
    {
        /// <summary>
        /// Identifier of an Event
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Event Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of an event
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Event Start Date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Event End Date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Location of an event
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Max amound of number to RSVP
        /// </summary>
        public int Capacity { get; set; }
        
        /// <summary>
        /// Event price
        /// </summary>
        public double Price { get; set; }


    }
}
