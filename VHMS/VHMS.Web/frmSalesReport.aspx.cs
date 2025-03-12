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

public partial class frmSalesReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    int CompanyID = 0;
    string sException = string.Empty;
    decimal InvoiceAmount = 0, NetAmount = 0, DiscountAmount = 0, TotalQty = 0, OtherCharges = 0, ProfitLossAmount=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            CompanyID = Convert.ToInt32(HttpContext.Current.Session["CompanyID"].ToString());
            LoadCustomer();
            LoadCompany();
            hdnBillNo.Value = HttpContext.Current.Session["CompanyID"].ToString();
            ddlCompany.SelectedValue = hdnBillNo.Value;
            ddlCompany.Enabled = false;
            txtDOB.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtDOR.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }

    public void LoadCompany()
    {
        Collection<VHMS.Entity.Company> ObjList = new Collection<VHMS.Entity.Company>();
        ObjList = VHMS.DAL.Company.GetCompany();

        ddlCompany.DataSource = ObjList;
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "CompanyID";
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadReport();
    }
    //protected void BindData()
    //{
    //    DataTable result = (DataTable)Session["tSales"];
    //    if (result.Rows.Count > 0)
    //    {
    //        gvSales.DataSource = result;
    //        gvSales.DataBind();
    ////    }
    //  }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvSales.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            try
            {
                this.gvSales.AllowPaging = false;
                this.gvSales.AllowSorting = false;
                this.gvSales.EditIndex = -1;

                Response.Clear();
                Response.ContentType = "application/vnd.xls";
                Response.AddHeader("content-disposition", "attachment;filename=SalesSummaryReports.xls");
                Response.Charset = "";
                StringWriter swriter = new StringWriter();
                HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
                gvSales.RenderControl(hwriter);
                Response.Write(swriter.ToString());
                Response.End();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        if (gvSales.Rows.Count < 1)
        {
            string display = "no Records Found.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
        else
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=SalesSummaryReports.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvSales.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            gvSales.AllowPaging = true;
            gvSales.DataBind();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void LoadReport()
    {
        DateTime dtFrom = DateTime.MinValue, dtTo = DateTime.MinValue;
        ReportDataSet dsReportData = new ReportDataSet();

        dsData = VHMS.DataAccess.VHMSReports.PrintSalesEntry(txtDOB.Text, txtDOR.Text, ddlCustomer.SelectedValue, ddlCompany.SelectedValue);
        try
        {
            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tSalesEntry";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tSalesEntry.ImportRow(drow);

                gvSales.DataSource = dsData.Tables[0];
                gvSales.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintSales | " + ex.ToString();
            Log.Write(sException);
        }
    }
    public void LoadCustomer()
    {
        Collection<VHMS.Entity.Customer> ObjList = new Collection<VHMS.Entity.Customer>();
        ObjList = VHMS.DataAccess.Customer.GetCustomer();
        ddlCustomer.DataSource = ObjList;
        ddlCustomer.DataTextField = "CustomerName";
        ddlCustomer.DataValueField = "CustomerID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
            InvoiceAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
            ProfitLossAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ProfitLossAmount"));
            DiscountAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscountAmount"));
            OtherCharges += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OtherCharges"));
            TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalQuantity"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 5;
            e.Row.Cells.RemoveAt(2);
            e.Row.Cells.RemoveAt(3);
            e.Row.Cells.RemoveAt(4);
            e.Row.Cells.RemoveAt(5);

            e.Row.Cells[1].Text = "Total Amount :";
            e.Row.Cells[2].Text = Convert.ToDecimal(TotalQty).ToString();
            e.Row.Cells[3].Text = Convert.ToDecimal(DiscountAmount).ToString();
            e.Row.Cells[4].Text = Convert.ToDecimal(NetAmount).ToString();
            e.Row.Cells[5].Text = Convert.ToDecimal(OtherCharges).ToString();
            e.Row.Cells[6].Text = Convert.ToDecimal(InvoiceAmount).ToString();
            e.Row.Cells[7].Text = Convert.ToDecimal(ProfitLossAmount).ToString();
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[6].Font.Bold = true;
            e.Row.Cells[7].Font.Bold = true;
            e.Row.Cells[1].Font.Size = 14;
            e.Row.Cells[2].Font.Size = 14;
            e.Row.Cells[3].Font.Size = 14;
            e.Row.Cells[4].Font.Size = 14;
            e.Row.Cells[5].Font.Size = 14;
            e.Row.Cells[6].Font.Size = 14;
            e.Row.Cells[7].Font.Size = 14;
        }
    }
}

