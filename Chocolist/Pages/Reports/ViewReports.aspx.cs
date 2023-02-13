using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace Chocolist.Pages.Reports
{
    public partial class ViewReports : System.Web.UI.Page
    {
        IReportService reportService = new ReportService();

        static private List<Report> allReports = new List<Report>();
        static DataTable reportGridSource = new DataTable();
        static int reportIdGrid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateReports();
                reportIdGrid = 0;
                reportGridSource = new DataTable();
            }
        }
        private void PopulateReports()
        {
            allReports = reportService.GetAllReports();
            ddlSelectReport.Items.Clear();
            ddlSelectReport.Items.Add(new System.Web.UI.WebControls.ListItem(text: "-None-", value: ""));
            foreach (Report report in allReports)
            {
                System.Web.UI.WebControls.ListItem ddlAdd = new System.Web.UI.WebControls.ListItem();
                ddlAdd.Value = report.ReportId.ToString();
                ddlAdd.Text = report.Name;
                ddlSelectReport.Items.Add(ddlAdd);
            }
            ddlSelectReport.SelectedIndex = 0;
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (!ddlSelectReport.SelectedValue.Equals(""))
            {
                reportGridSource = reportService.RunReport(Int32.Parse(ddlSelectReport.SelectedValue));
                reportIdGrid = Int32.Parse(ddlSelectReport.SelectedValue);
                ReportResult.DataSource = reportGridSource;
                ReportResult.Caption = ddlSelectReport.SelectedItem.Text;
                ReportResult.Attributes.CssStyle.Add("caption-side", "top");
                ReportResult.DataBind();
                ResultMessage.Text = "";
                ResultMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                ResultMessage.Text = "Please select and run a report before exporting.";
                ResultMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (reportIdGrid == 0)
            {
                ResultMessage.Text = "Please select and run a report before exporting.";
                ResultMessage.ForeColor = System.Drawing.Color.Red;
            } else
            {
                string reportTitle = reportService.GetReport(reportIdGrid).Name;
                string reportDescription = reportService.GetReport(reportIdGrid).Description;
                string reportFileTitle = reportTitle.Replace(" ", "_");
                string attachment = "attachment; filename=" + reportFileTitle + ".xls";
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
                Response.Write("<BR><BR>");

                Response.Write("<Table border='0' bgColor='#ffffff' " +
                "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                "style='font-size:11.0pt; font-family:Calibri; background:white;'>");


                #region Report Header      
                Response.Write("<TR valign='top'>");
                Response.Write("<B><U><TD align='center' colspan='3' style='font-size:14.0pt;text-weight:bold;text-decoration:underline;'>" + reportTitle + "</TD>");
                Response.Write("</U></B></TR>");

                Response.Write("<TR valign='top'><TD align='left' colspan='3'> " + reportDescription + " </TD></TR>");
                Response.Write("<TR valign='top'><TD align='left' colspan='3'> <STRONG>Date Generated:</STRONG>" + DateTime.Now.ToString("yyyy-MM-dd") + " </TD></TR>");

                Response.Write("<TR valign='top'><TD align='left' colspan='3' style='whitespace:normal;'>");
                Response.Write("</TD></TR>");

                #endregion
                #region Header Row      

                Response.Write("<TR valign='top'><td></td><td>");
                Response.Write("<Table border='1' bgColor='#FFFFFF' " +
                "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                Response.Write("<TR  valign='top' style='background:#D8D8D8;'>");
                foreach (DataColumn dc in reportGridSource.Columns)
                {
                    Response.Write("<TD align='left' style='width:20%;'>" + dc.ColumnName + "</TD>");
                }
                Response.Write("</TR>");
                #endregion

                #region Detail Row      
                int i;
                foreach (DataRow dr in reportGridSource.Rows)
                {
                    Response.Write("<TR valign='top'>");
                    for (i = 0; i < reportGridSource.Columns.Count; i++)
                    {
                        Response.Write("<TD align='left'>" + dr[i].ToString() + "</TD>");
                    }
                    Response.Write("</TR>");
                }
                Response.Write("</Table></TD><td></td></TR>");
                #endregion

                Response.Write("</Table>");
                Response.Write("</font>");
                Response.Flush();
                Response.End();
            }
        }
        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (reportIdGrid == 0)
            {
                ResultMessage.Text = "Please select and run a report before exporting.";
                ResultMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                string reportTitle = reportService.GetReport(reportIdGrid).Name;
                string reportDescription = reportService.GetReport(reportIdGrid).Description;
                string reportFileTitle = reportTitle.Replace(" ", "_");
                string attachment = "attachment; filename=" + reportFileTitle + ".pdf";
                string htmlOutput = "";
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/pdf";
                //Response.Charset = "utf-8";
                //Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                htmlOutput +=(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                htmlOutput +=("<font style='font-size:11.0pt; font-family:Calibri;'>");
                htmlOutput +=("<BR><BR>");

                htmlOutput +=("<Table border='0' bgColor='#ffffff' " +
                "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                "style='font-size:11.0pt; font-family:Calibri; background:white;'>");


                #region Report Header      
                htmlOutput +=("<TR valign='top'>");
                htmlOutput +=("<B><U><TD align='center' colspan='3' style='font-size:14.0pt;text-weight:bold;text-decoration:underline;'>" + reportTitle + "</TD>");
                htmlOutput +=("</U></B></TR>");

                htmlOutput +=("<TR valign='top'><TD align='left' colspan='3'> " + reportDescription + " </TD></TR>");
                htmlOutput +=("<TR valign='top'><TD align='left' colspan='3'> <STRONG>Date Generated:</STRONG>" + DateTime.Now.ToString("yyyy-MM-dd") + " </TD></TR>");

                htmlOutput +=("<TR valign='top'><TD align='left' colspan='3' style='whitespace:normal;'>");
                htmlOutput +=("</TD></TR>");

                #endregion
                #region Header Row      

                htmlOutput +=("<TR valign='top'><td colspan='3'>");
                htmlOutput +=("<Table border='1' bgColor='#FFFFFF' " +
                "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                htmlOutput +=("<TR  valign='top' style='background:#D8D8D8;'>");
                foreach (DataColumn dc in reportGridSource.Columns)
                {
                    htmlOutput +=("<TD align='left' style='width:20%;'><STRONG>" + dc.ColumnName + "</STRONG></TD>");
                }
                htmlOutput +=("</TR>");
                #endregion

                #region Detail Row      
                int i;
                foreach (DataRow dr in reportGridSource.Rows)
                {
                    htmlOutput +=("<TR valign='top'>");
                    for (i = 0; i < reportGridSource.Columns.Count; i++)
                    {
                        htmlOutput +=("<TD align='left'>" + dr[i].ToString() + "</TD>");
                    }
                    htmlOutput +=("</TR>");
                }
                htmlOutput +=("</Table></TD></TR>");
                #endregion

                htmlOutput +=("</Table>");
                htmlOutput +=("</font>");
                Document pdfDoc = new Document(PageSize.A4, 80f, 80f, -2f, 35f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                StringReader sr = new StringReader(htmlOutput);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
        }
    }
}