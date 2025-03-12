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
public partial class frmProductReport : System.Web.UI.Page
{
    DataSet dsData = new DataSet();
    string sException = string.Empty;
    decimal AmountPaid = 0;
    int iRegionID = 0;
    int iZoneID = 0;
    string empid = "0";
    int iRoleID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VHMSService.AddPageVisitLog();
            string empid = Request.QueryString["id"];

            LoadCategory();
            LoadSupplier();
            LoadProduct();
            if (empid != null && Convert.ToInt32(empid) > 0)
            {
                VHMS.Entity.User ObjList = new VHMS.Entity.User();
                ObjList = VHMS.DataAccess.User.GetUserByID(Convert.ToInt32(empid));
                iRoleID = ObjList.RoleID;
            }
        }
    }
    private void LoadReport()
    {
        ReportDataSet dsReportData = new ReportDataSet();
        empid = Request.QueryString["id"];

        dsData = VHMS.DataAccess.VHMSReports.PrintProductOrderWise(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubCategory.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), ddlPaymentMode.Text);
        try
        {

            if (dsData.Tables.Count > 0)
            {
                dsData.Tables[0].TableName = "tProduct";
                foreach (DataRow drow in dsData.Tables[0].Rows)
                    dsReportData.tProduct.ImportRow(drow);

                gvReceipt.DataSource = dsData.Tables[0];
                gvReceipt.DataBind();
            }

        }
        catch (Exception ex)
        {
            sException = "frmPrintClosed | " + ex.ToString();
            Log.Write(sException);
        }
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Product.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvReceipt.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        gvReceipt.AllowPaging = true;
        gvReceipt.DataBind();
    }
   

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment; filename=Product.xls");
        Response.ContentType = "application/excel";

        StringWriter stringwriter = new StringWriter();
        HtmlTextWriter htmltextwriter = new HtmlTextWriter(stringwriter);

        gvReceipt.HeaderRow.Style.Add("background-color", "#FFFFFF");

        foreach (TableCell tableCell in gvReceipt.HeaderRow.Cells)
        {
            tableCell.Style["background-color"] = "#d8d4d4";
        }

        foreach (GridViewRow gridViewRow in gvReceipt.Rows)
        {
            gridViewRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
            {
                gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
            }
        }
        gvReceipt.RenderControl(htmltextwriter);
        Response.Write(stringwriter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    public void LoadCategory()
    {
        Collection<VHMS.Entity.Billing.Category> ObjList = new Collection<VHMS.Entity.Billing.Category>();
        ObjList = VHMS.DataAccess.Billing.Category.GetActiveCategory();

        ddlCategory.DataSource = ObjList;
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataValueField = "CategoryID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }

    public void LoadSupplier()
    {
        //Collection<VHMS.Entity.Billing.Supplier> ObjList = new Collection<VHMS.Entity.Billing.Supplier>();
        //ObjList = VHMS.DataAccess.Billing.Supplier.GetSupplier();

        //ddlSupplier.DataSource = ObjList;
        //ddlSupplier.DataTextField = "SupplierName";
        //ddlSupplier.DataValueField = "SupplierID";
        //ddlSupplier.DataBind();
        //ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void tbAccount_TextChanged(object sender, EventArgs e)
    {
        if (txtCode.Text != "")
        {
            Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
            ObjList = VHMS.DataAccess.Master.Product.GetProductByCode(txtCode.Text, false);

            ddlProduct.DataSource = ObjList;
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductID";
            ddlProduct.DataBind();
        }
        else
        {
            LoadProduct();
        }
        //GVSummary.Visible = false;
    }
    public void LoadProduct()
    {
        Collection<VHMS.Entity.Master.Product> ObjList = new Collection<VHMS.Entity.Master.Product>();
       // ObjList = VHMS.DataAccess.Master.Product.GetActiveProductID(0, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubCategory.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue));

        ddlProduct.DataSource = ObjList;
        ddlProduct.DataTextField = "ProductName";
        ddlProduct.DataValueField = "ProductID";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All--", "0"));
    }
}