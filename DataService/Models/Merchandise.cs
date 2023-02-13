using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Models
{
    public class Merchandise
    {
        public int ItemId { get; set; }

        public int ColourId { get; set; }
        public string ColourName { get; set; }
        public string CanvasStyle { get; set; }

        public string Size { get; set; }

        /// <summary>
        /// Item class info of the merch
        /// </summary>
        public Item Item { get; set; }

        public decimal Rating { get; set; }
    }
}
