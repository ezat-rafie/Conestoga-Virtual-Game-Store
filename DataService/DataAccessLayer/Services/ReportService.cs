using DataService.DataAccessLayer.IServices;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.Services
{
    public class ReportService : IReportService
    {
        public List<Report> GetAllReports()
        {
            List<Report> reportList = new List<Report>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.Report;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            reportList.Add(new Report
                            {
                                ReportId = Convert.ToInt32(rdr["ReportId"]),
                                Name = rdr["Name"].ToString(),
                                Script = rdr["Script"].ToString(),
                                ChartTitle = rdr["ChartTitle"].ToString(),
                                ChartType = rdr["ChartType"].ToString(),
                                ChartXLabel = rdr["ChartXLabel"].ToString(),
                                ChartYLabel = rdr["ChartYLabel"].ToString(),
                                ChartXColumn = rdr["ChartXColumn"].ToString(),
                                ChartYColumn = rdr["ChartYColumn"].ToString(),
                                ColumnFormats = rdr["ColumnFormats"].ToString(),
                                Description = rdr["Description"].ToString()
                            });
                        }
                    }
                }
            }

            return reportList;
        }

        public Report GetReport(int reportId)
        {
            Report report = new Report();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.Report WHERE ReportId = @field1;";
                    command.Parameters.AddWithValue("@field1", reportId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        report.ReportId = Convert.ToInt32(rdr["ReportId"]);
                        report.Name = rdr["Name"].ToString();
                        report.Script = rdr["Script"].ToString();
                        report.ChartTitle = rdr["ChartTitle"].ToString();
                        report.ChartType = rdr["ChartType"].ToString();
                        report.ChartXLabel = rdr["ChartXLabel"].ToString();
                        report.ChartYLabel = rdr["ChartYLabel"].ToString();
                        report.ChartXColumn = rdr["ChartXColumn"].ToString();
                        report.ChartYColumn = rdr["ChartYColumn"].ToString();
                        report.ColumnFormats = rdr["ColumnFormats"].ToString();
                        report.Description = rdr["Description"].ToString();
                    }
                }
            }

            return report;
        }

        public DataTable RunReport(int reportId)
        {
            DataTable result = new DataTable();
            Report report = new Report();
            String reportScript = "";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Script FROM dbo.Report WHERE ReportId = @field1;";
                    command.Parameters.AddWithValue("@field1", reportId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        reportScript = rdr["Script"].ToString();
                    }
                    rdr.Close();
                }
                if (!reportScript.Equals(""))
                {
                    using (SqlCommand command = new SqlCommand(reportScript))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            command.Connection = connection;
                            sda.SelectCommand = command;
                            try
                            {
                                sda.Fill(result);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            return (result);
        }
    }
}
