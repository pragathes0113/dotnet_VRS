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
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class frmPurchaseReturnReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0, TotalQty = 0, TotalAmount = 0;
    int CompanyID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            CompanyID = Convert.ToInt32(HttpContext.Current.Session["CompanyID"].ToString());
            LoadSupplier();
            txtDOB.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtDOR.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadReport();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (ddluser.Text == "Summary")
        {
            gvPurchaseReturn.Visible = true;
            gvDetailed.Visible = false;
            if (gvPurchaseReturn.Rows.Count < 1)
            {
                string display = "no Records Found.";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            }
            else
            {
                try
                {
                    this.gvPurchaseReturn.AllowPaging = false;
                    this.gvPurchaseReturn.AllowSorting = false;
                    this.gvPurchaseReturn.EditIndex = -1;

                    Response.Clear();
                    Response.ContentType = "application/vnd.xls";
                    Response.AddHeader("content-disposition", "attachment;filename=PurchaseReturnReports.xls");
                    Response.Charset = "";
                    StringWriter swriter = new StringWriter();
                    HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
                    gvPurchaseReturn.RenderControl(hwriter);
                    Response.Write(swriter.ToString());
                    Response.End();
                }
                catch (Exception exe)
                {
                    throw exe;
                }
            }
        }
        else
        {
            gvPurchaseReturn.Visible = false;
            gvDetailed.Visible = true;
            if (gvDetailed.Rows.Count < 1)
            {
                string display = "no Records Found.";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            }
            else
            {
                try
                {
                    this.gvDetailed.AllowPaging = false;
                    this.gvDetailed.AllowSorting = false;
                    this.gvDetailed.EditIndex = -1;

                    Response.Clear();
                    Response.ContentType = "application/vnd.xls";
                    Response.AddHeader("content-disposition", "attachment;filename=PurchaseReturnDetailedReports.xls");
                    Response.Charset = "";
                    StringWriter swriter = new StringWriter();
                    HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
                    gvDetailed.RenderControl(hwriter);
                    Response.Write(swriter.ToString());
                    Response.End();
                }
                catch (Exception exe)
                {
                    throw exe;
                }
            }
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        if (ddluser.Text == "Summary")
        {
            gvPurchaseReturn.Visible = true;
            gvDetailed.Visible = false;
            if (gvPurchaseReturn.Rows.Count < 1)
            {
                string display = "no Records Found.";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            }
            else
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=PurchaseReturnReports.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvPurchaseReturn.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
                gvPurchaseReturn.AllowPaging = true;
                gvPurchaseReturn.DataBind();
            }
        }
        else
        {
            gvPurchaseReturn.Visible = false;
            gvDetailed.Visible = true;
            if (gvDetailed.Rows.Count < 1)
            {
                string display = "no Records Found.";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            }
            else
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=PurchaseReturnDetailedReports.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvDetailed.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
                gvDetailed.AllowPaging = true;
                gvDetailed.DataBind();
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        if (ddluser.Text == "Summary")
        {
            gvPurchaseReturn.Visible = true;
            gvDetailed.Visible = false;

            DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
            ReportDataSet dsReportData = new ReportDataSet();
            dsData = new DataSet();
            dsData = VHMS.DataAccess.VHMSReports.PrintPurchaseReturn(txtDOB.Text, txtDOR.Text, 0, ddlSupplier.SelectedValue, 1);
            try
            {
                if (dsData.Tables.Count > 0)
                {
                    dsData.Tables[0].TableName = "tPurchase";

                    gvPurchaseReturn.DataSource = dsData.Tables[0];
                    gvPurchaseReturn.DataBind();
                }

            }
            catch (Exception ex)
            {
                sException = "frmPrintPurchase | " + ex.ToString();
                Log.Write(sException);
            }
        }
        else
        {
            gvPurchaseReturn.Visible = false;
            gvDetailed.Visible = true;

            DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
            ReportDataSet dsReportData = new ReportDataSet();
            dsData = new DataSet();
            dsData = VHMS.DataAccess.VHMSReports.PrintPurchaseReturnDetailed(txtDOB.Text, txtDOR.Text, ddlSupplier.SelectedValue);
            try
            {
                if (dsData.Tables.Count > 0)
                {
                    dsData.Tables[0].TableName = "tPurchaseReturnTrans";

                    gvDetailed.DataSource = dsData.Tables[0];
                    gvDetailed.DataBind();
                }

            }
            catch (Exception ex)
            {
                sException = "frmPrintPurchase | " + ex.ToString();
                Log.Write(sException);
            }
        }
    }
    public void LoadSupplier()
    {
        Collection<VHMS.Entity.Billing.Supplier> ObjList = new Collection<VHMS.Entity.Billing.Supplier>();
        ObjList = VHMS.DataAccess.Billing.Supplier.GetSupplier(CompanyID);

        ddlSupplier.DataSource = ObjList;
        ddlSupplier.DataTextField = "SupplierName";
        ddlSupplier.DataValueField = "SupplierID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
            TotalAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
            InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
            DiscountAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscountAmount"));
            TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalQuantity"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 5;
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);

            e.Row.Cells[2].Text = "Total Amount :";
            e.Row.Cells[3].Text = Convert.ToDecimal(TotalQty).ToString();
            e.Row.Cells[4].Text = Convert.ToDecimal(TotalAmount).ToString();
            e.Row.Cells[5].Text = Convert.ToDecimal(DiscountAmount).ToString();
            e.Row.Cells[6].Text = Convert.ToDecimal(NetAmount).ToString();
            e.Row.Cells[7].Text = Convert.ToDecimal(InvoiceAmount).ToString();
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[6].Font.Bold = true;
            e.Row.Cells[7].Font.Bold = true;
            e.Row.Cells[2].Font.Size = 14;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[4].Font.Size = 14;
            e.Row.Cells[5].Font.Size = 14;
            e.Row.Cells[6].Font.Size = 14;
            e.Row.Cells[7].Font.Size = 14;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotalAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SubTotal"));
                InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
                DiscountAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscountAmount"));
                TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Quantity"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].ColumnSpan = 5;
                e.Row.Cells.RemoveAt(1);
                e.Row.Cells.RemoveAt(2);
                e.Row.Cells.RemoveAt(3);
                e.Row.Cells.RemoveAt(4);

                e.Row.Cells[2].Text = "Total Amount :";
                e.Row.Cells[4].Text = Convert.ToDecimal(TotalQty).ToString();
                e.Row.Cells[6].Text = Convert.ToDecimal(DiscountAmount).ToString();
                e.Row.Cells[8].Text = Convert.ToDecimal(InvoiceAmount).ToString();
                e.Row.Cells[9].Text = Convert.ToDecimal(TotalAmount).ToString();
                e.Row.Cells[2].Font.Bold = true;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[8].Font.Bold = true;
                e.Row.Cells[9].Font.Bold = true;
                e.Row.Cells[2].Font.Size = 14;
                e.Row.Cells[4].Font.Size = 14;
                e.Row.Cells[6].Font.Size = 14;
                e.Row.Cells[8].Font.Size = 14;
                e.Row.Cells[9].Font.Size = 14;
            }
    }
    protected void gvManageSales_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int PaymentMode = Convert.ToInt32(e.CommandArgument);
            Session["PurchaseReturnID"] = PaymentMode;
            Response.Redirect("frmPurchaseReturn.aspx", true);
        }
       
    }
}

