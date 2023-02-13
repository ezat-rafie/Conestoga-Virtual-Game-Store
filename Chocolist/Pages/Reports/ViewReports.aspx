<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewReports.aspx.cs" Inherits="Chocolist.Pages.Reports.ViewReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="ViewReports.css" rel="stylesheet" />
    <form class="container p-2" id="frmCreateItem" style="background-color: #282B2E; border-radius: 5px; box-shadow: rgb(0 0 0 / 0.40) 5px 5px 10px;">
        <div class="row m-1">
            <div class="mb-4">
                <h3 class="mx-auto" style="width: fit-content; float: left; color:darkgrey; padding-left: 20px;">View Reports</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-6">
                <div style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;" class="w-100">
                    Select a report:
                    <asp:DropDownList CssClass="col-8 w-100 text-input btn btn-secondary btn-sm dropdown-toggle" ID="ddlSelectReport" runat="server" AutoPostBack="true">
                    </asp:DropDownList>                    
                </div>
            </div>
            <div class="col-6">
                <asp:Button ID="btnRunReport" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Run Report" OnClick="btnReport_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:Button ID="btnExportReport" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Export Report Below as XLS" OnClick="btnExport_Click" />
                <asp:Button ID="btnExportReportPDF" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Export Report Below as PDF" OnClick="btnExportPDF_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-8">
                <div id="divResult" runat="server">
                    <div>
                        <asp:Label ID="ResultMessage" class="float-right" ForeColor="Green" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:GridView ID="ReportResult" runat="server" AutoGenerateColumns="True" ShowFooter="False" GridLines ="Both" CellPadding="4"
                    EmptyDataText="The report returned no results."
                    CssClass="table table-striped table-bordered" style="color:#FFFFFF;" Caption="" CaptionAlign="Top">
                    <HeaderStyle HorizontalAlign="Left" BackColor="#CA5500" ForeColor="#EEEEEE" />
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>
