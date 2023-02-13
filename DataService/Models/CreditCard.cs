using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Models
{
    /// <summary>
    /// Credit card model
    /// </summary>
    public class CreditCard
    {
        /// <summary>
        /// Card ID
        /// </summary>
        public int CreditCardId { get; set; }
        /// <summary>
        /// Display name on card
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Card number
        /// </summary>
        public long CardNumber { get; set; }
        /// <summary>
        /// Expiry date on card
        /// </summary>
        public DateTime Expiry { get; set; }
        /// <summary>
        /// CVV of card
        /// </summary>
        public int CVV { get; set; }
    }
}
