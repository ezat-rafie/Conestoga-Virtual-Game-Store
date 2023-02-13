using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Models
{
    public class EventRegistration
    {
        /// <summary>
        /// Identifier of an Event
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Identifier of a user
        /// </summary>
        public int UserId { get; set; }

    }
}
