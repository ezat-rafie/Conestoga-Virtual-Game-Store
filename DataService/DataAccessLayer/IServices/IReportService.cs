using DataService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.IServices
{
    public interface IReportService
    {
        List<Report> GetAllReports();

        Report GetReport(int reportId);

        DataTable RunReport(int reportId);
    }
}
