using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string Name { get; set; }
        public string Script { get; set; }
        public string ChartTitle { get; set; }
        public string ChartType { get; set; }
        public string ChartXLabel { get; set; }
        public string ChartYLabel { get; set; }
        public string ChartXColumn { get; set; }
        public string ChartYColumn { get; set; }
        public string ColumnFormats { get; set; }
        public string Description { get; set; }
    }
}
