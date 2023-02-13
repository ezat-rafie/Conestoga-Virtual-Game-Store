/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : November 2022 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validations
{
    /// <summary>
    /// Event validation class
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Event Title
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        /// <summary>
        /// Description of an event
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        /// <summary>
        /// Event Start Date
        /// </summary>
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Event End Date
        /// </summary>
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Location of an event
        /// </summary>
        [Required(ErrorMessage = "Location is required")]
        public string Place { get; set; }

        /// <summary>
        /// Price of an event
        /// </summary>
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Price can only be numbers and 2 after a decimal")]
        [Required(ErrorMessage = "Price is required")]
        public string Price
        {
            get;
            set;
        }

        /// <summary>
        /// Return start date validation result
        /// </summary>
        /// <param name="returnedUserId"></param>
        /// <returns></returns>
        public Boolean IsStartDateValid(DateTime date)
        {
            Boolean result = false;

            if (date > DateTime.Now)
            {
                result = true;
            }

            return (result);
        }

        /// <summary>
        /// Return end date validation result
        /// </summary>
        /// <param name="returnedUserId"></param>
        /// <returns></returns>
        public Boolean IsEndDateValid()
        {
            Boolean result = false;

            if (EndDate > StartDate)
            {
                result = true;
            }

            return (result);
        }

    }
}
