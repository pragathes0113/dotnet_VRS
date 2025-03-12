﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;

public partial class frmExpenseLedger : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    int CompanyID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            CompanyID = Convert.ToInt32(HttpContext.Current.Session["CompanyID"].ToString());
            LoadCustomer();
            txtDOB.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtDOR.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        }
    }
    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Ledger> ObjList = new Collection<VHMS.Entity.Ledger>();
        ObjList = VHMS.DataAccess.Ledger.GetLedger();

        ddlCustomer.DataSource = ObjList;
        ddlCustomer.DataTextField = "LedgerName";
        ddlCustomer.DataValueField = "LedgerID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rprt.Close();
        rprt.Dispose();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {

        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment; filename=ExpenseLedger.xls");
        Response.ContentType = "application/excel";

        StringWriter stringwriter = new StringWriter();
        HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        //divPdf.HeaderRow.Style.Add("background-color", "#FFFFFF");

        //foreach (TableCell tableCell in divPdf.HeaderRow.Cells)
        //{
        //    tableCell.Style["background-color"] = "#d8d4d4";
        //}

        //foreach (GridViewRow gridViewRow in divPdf.Rows)
        //{
        //    gridViewRow.BackColor = System.Drawing.Color.White;
        //    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
        //    {
        //        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
        //    }
        //}
        divPdf.RenderControl(htmltextwriter);
        Response.Write(stringwriter.ToString());
        Response.End();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (ddluser.Text == "Summary")
            LoadReport();
        else
        {
            if (Convert.ToInt32(ddlCustomer.SelectedValue) > 0)
                LoadReport();
            else
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Select Ledger');", true);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddluser.Text == "Detailed")
        {
            Response.Clear();
            Response.ContentType = "text/html";
            var strBuilder = new StringBuilder();
            var strWriter = new StringWriter(strBuilder);
            var htmlWriter = new HtmlTextWriter(strWriter);
            var streamWriter = new StreamWriter(Response.OutputStream);
            var javascript = @"<script type='text/javascript'>window.onload = function() {setTimeout(function () { window.print(); }, 2000);}</script>";
            var stylecss = @"<link href='css/print.css' rel='stylesheet type='text/css' /><link href='css/fonts/font-awesome.min.css' rel='stylesheet type='text/css' />";
            htmlWriter.Write(javascript);
            htmlWriter.Write(stylecss);
            divPdf.RenderControl(htmlWriter);
            streamWriter.Write(strBuilder.ToString());
            streamWriter.Flush();
            Response.End();
        }
        else
        {
            string sTemp = string.Empty;
            try
            {
                ReportDataSet dsReportData = new ReportDataSet();

                dsData = VHMS.DataAccess.VHMSReports.PrintExpenseLedgerReport(txtDOB.Text, txtDOR.Text, ddlCustomer.SelectedValue);

                if (dsData.Tables.Count > 0)
                {
                    dsData.Tables[0].TableName = "tStatement";
                    foreach (DataRow drow in dsData.Tables[0].Rows)
                        dsReportData.tStatement.ImportRow(drow);

                    dsData.Tables[1].TableName = "tLedger";
                    foreach (DataRow drow in dsData.Tables[1].Rows)
                        dsReportData.tLedger.ImportRow(drow);

                    dsData.Tables[2].TableName = "tCompany";
                    foreach (DataRow drow in dsData.Tables[2].Rows)
                        dsReportData.tCompany.ImportRow(drow);
                }
                //  dsReportData = LoadData();
                rprt = new ReportDocument();
                rprt.Load(Server.MapPath("~/Reports/Rpt_SalesStatementSummary.rpt"));
                rprt.SetDataSource(dsReportData);
                this.CRDischargeSummaryReport.ReportSource = rprt;
                CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                CRDischargeSummaryReport.Zoom(100);
                this.CRDischargeSummaryReport.DataBind();
                rprt.ExportToHttpResponse
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "ExpenseLedger summary");
            }
            catch (Exception ex)
            {
                sException = "ReportPage_RepDischargeSummary | " + ex.ToString();
                Log.Write(sException);
            }
        }
    }


    protected void Page_UnLoad()
    {
        if (rprt != null)
        {
            rprt.Dispose();
            rprt.Clone();
            rprt.Close();
            CRDischargeSummaryReport.Dispose();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=ExpenseLedger.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        divPdf.RenderControl(htmlTextWriter);
        StringReader stringReader = new StringReader(stringWriter.ToString());
        Document Doc = new Document(PageSize.A4);
        HTMLWorker htmlparser = new HTMLWorker(Doc);
        PdfWriter.GetInstance(Doc, Response.OutputStream);
        Doc.Open();
        htmlparser.Parse(stringReader);
        Doc.Close();
        Response.Write(Doc);
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintExpenseLedgerReport(txtDOB.Text, txtDOR.Text, ddlCustomer.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tStatement";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tStatement.ImportRow(drow);

                dsData.Tables[1].TableName = "tLedger";
                foreach (DataRow drow in dsData.Tables[1].Rows)
                    dsReportData.tLedger.ImportRow(drow);

                dsData.Tables[2].TableName = "tCompany";
                foreach (DataRow drow in dsData.Tables[2].Rows)
                    dsReportData.tCompany.ImportRow(drow);
            }
            if (ddluser.Text == "Detailed")
            {
                if (dsReportData.tLedger.Rows.Count > 0)
                {
                    string TableHTML = ""; string CompanyName = ""; string iRate = ""; decimal gRate = 0; string date = "";
                    foreach (ReportDataSet.tLedgerRow drtJobCardRow in dsReportData.tLedger)
                    {
                        if (dsReportData.tCompany.Rows.Count > 0)
                        {
                            TableHTML += "<table cellpadding='0' cellspacing='0'>";
                            foreach (ReportDataSet.tCompanyRow drtBranchRow in dsReportData.tCompany)
                            {
                                CompanyName = drtBranchRow.CompanyName.ToString();
                                if (drtBranchRow.PhoneNo2.Length <= 0)
                                    TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + drtBranchRow.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + drtBranchRow.PhoneNo1 + "</th></tr>";
                                else
                                    TableHTML += "<tr valign='middle'><th rowspan='3' align='right' style='padding-left:25px;width:10%'><img src='images/meenakchi-amman-1.jpg' width='100%' alt=''/><th rowspan='3' align='right' style='padding-left:25px;width:15%'><img src='images/5.png' width='100%' alt=''/></th><th style='font-size:19px;text-align:center;width: 48%;height: 1%;'>" + drtBranchRow.CompanyName + "</th><th style='text-align:end;height: 1%;'> " + drtBranchRow.PhoneNo1 + " / " + drtBranchRow.PhoneNo2 + "</th></tr>";

                                TableHTML += "<tr><th style='text-align:center;height: 1%;' >Pure Handloom Silk Sarees Manufacturer</th><th></th></tr>";

                                TableHTML += "<tr><th style='text-align:center;height: 1%;'>" + drtBranchRow.CompanyAddress + "</th><th style='text-align:end;height: 1%;'>" + drtBranchRow.CSTNo + " / State Code: 33 </th></tr>";
                                TableHTML += "<tr><th style='text-align:center;'Colspan='4'> Email : " + drtBranchRow.Email + "</th></tr>";

                            }

                            TableHTML += "</table>";

                        }
                        TableHTML += "<table cellpadding='0' cellspacing='0' ><thead><tr><th  style='text-align:center;'Colspan='5'>Expense Ledger</th></tr></thead></table>";
                        TableHTML += "<table cellpadding='0' cellspacing='0'><thead>";
                       // TableHTML += "<tr><td colspan='3'><b>Mr./Ms." + drtJobCardRow.LedgerName + "</b></td><td colspan='2'><b>Phone No.: " + drtJobCardRow.MobileNo + "</td></tr>";
                       // TableHTML += "<tr><td colspan='5'> " + drtJobCardRow. + "</td></tr>";
                        TableHTML += "<br/>";
                        TableHTML += "</thead></table>";
                        TableHTML += "<table cellpadding='0' cellspacing='0' class='border_details' ><thead>";
                        TableHTML += "<tr><th style='width:20%;'><b> Date</b></th><th style='width:35%;'><b>Particulars</b></th></th><th style='width:15%;text-align:right'><b>Debit</b></th><th style='width:15%;text-align:right'><b>Credit</b></th><th style='width:15%;text-align:right'><b>Balance</b></th></tr></thead>";
                        decimal CAmount = 0, DAmount = 0;
                        string BalanceType = "";
                        if (dsReportData.tStatement.Rows.Count > 0)
                        {
                            int sno = 1;

                            foreach (ReportDataSet.tStatementRow drtJobCardComplaintsRow in dsReportData.tStatement)
                            {
                                CAmount += drtJobCardComplaintsRow.Credit;
                                DAmount += drtJobCardComplaintsRow.Debit;
                                decimal TotalBalance = CAmount - DAmount;
                                if (TotalBalance > 0)
                                    BalanceType = TotalBalance + "  Cr ";
                                else
                                {
                                    TotalBalance = DAmount - CAmount;
                                    BalanceType = TotalBalance + "  Dr ";
                                }

                                string VDate = "";
                                if (!drtJobCardComplaintsRow.IsVoucherDateNull())
                                    VDate = drtJobCardComplaintsRow.VoucherDate.ToString("dd-MM-yyyy");
                                TableHTML += "<tr><td style='width:20%;'>" + VDate + "</td><td style='width:35%;'>" + drtJobCardComplaintsRow.Particulars + "</td><td  style='text-align:right'> " + drtJobCardComplaintsRow.Debit + "</td><td  style='text-align:right'>" + drtJobCardComplaintsRow.Credit + "</td><td  style='text-align:right'>" + BalanceType + "</td></tr>";
                                sno++;
                            }
                        }

                        TableHTML += "<tr style='text-align:right'><td colspan='1'></td><td style='text-align:right'><b>Total Amount  </b></td><td style='text-align:right'><b> " + DAmount + "</b></td><td style='text-align:right'><b> " + CAmount + "</b></td></tr>";
                        if (CAmount > DAmount)
                            TableHTML += "<tr style='text-align:right'><td colspan='1'></td><td style='text-align:right'><b>Closing Balance</b></td><td style='text-align:right'></td><td style='text-align:right'><b> " + (CAmount - DAmount).ToString("0.00") + "</b></td></tr>";
                        else
                            TableHTML += "<tr style='text-align:right'><td colspan='1'></td><td style='text-align:right'><b>Closing Balance</b></td><td style='text-align:right'><b> " + (DAmount - CAmount).ToString("0.00") + "</b></td><td style='text-align:right'></td></tr>";

                        TableHTML += "</table>";
                        TableHTML += "<br/>";
                        TableHTML += "<br/>";
                        TableHTML += "<br/>";
                        TableHTML += "<br/>";
                    }

                    divOPInvoice1.InnerHtml = TableHTML;
                }
            }
            else
            {
                rprt = new ReportDocument();
                rprt.Load(Server.MapPath("~/Reports/Rpt_SalesStatementSummary.rpt"));
                rprt.SetDataSource(dsReportData);
                this.CRDischargeSummaryReport.ReportSource = rprt;
                CRDischargeSummaryReport.AllowedExportFormats = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats ^ CrystalDecisions.Shared.ViewerExportFormats.RptFormat);
                CRDischargeSummaryReport.Zoom(100);
                this.CRDischargeSummaryReport.DataBind();
            }
        }

        catch (Exception ex)
        {
            sException = "frmPrintJobCard | " + ex.ToString();
            Log.Write(sException);
        }
    }


    // int totalItems = 0;
    //  decimal percent = 0M;
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InvoiceAmount"));
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    (e.Row.FindControl("lblTotalAmount") as Label).Text = InvoiceAmount.ToString();
        //}

    }

    //public void LoadBranch()
    //{
    //    Collection<VHMS.Entity.Branch> ObjList = new Collection<VHMS.Entity.Branch>();
    //    ObjList = VHMS.DataAccess.Branch.GetBranch();

    //    ddlBranch.DataSource = ObjList;
    //    ddlBranch.DataTextField = "BranchName";
    //    ddlBranch.DataValueField = "BranchID";
    //    ddlBranch.DataBind();
    //    ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    //}
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        if (ddluser.Text == "Detailed")
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=BuyerLedger.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            divPdf.RenderControl(htmlTextWriter);
            StringReader stringReader = new StringReader(stringWriter.ToString());
            Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(Doc);
            PdfWriter.GetInstance(Doc, Response.OutputStream);
            Doc.Open();
            htmlparser.Parse(stringReader);
            Doc.Close();
            Response.Write(Doc);
            Response.End();
        }
    }
}
