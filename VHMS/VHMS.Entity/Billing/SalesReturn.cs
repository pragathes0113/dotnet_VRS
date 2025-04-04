﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class SalesReturn
    {
        public int SalesReturnID { get; set; }
        public string ReturnNo { get; set; }
        public DateTime ReturnDate { get; set; }
        public string sReturnDate { get; set; }
        public string BillNo { get; set; }
        public string OldInvoiceNo { get; set; }
        public DateTime BillDate { get; set; }
        public string sBillDate { get; set; }
        public int SalesEntryID { get; set; }
        public string InvoiceNo { get; set; }
        public Customer Customer { get; set; }
        public Company Company { get; set; }
        public Tax Tax { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal TotalQty { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean InvoiceBillType { get; set; }
        public Boolean CustomerBillType { get; set; }
        public decimal Roundoff { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public Collection<SalesReturnTrans> SalesReturnTrans { get; set; }
    }

    public class SalesReturnTrans
    {
        public int SalesReturnTransID { get; set; }
        public int SalesReturnID { get; set; }
        public string Barcode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int SalesEntryTransID { get; set; }
        public decimal DiscountAmount { get; set; }
        public Master.Product Product { get; set; }
        public Tax Tax { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string StatusFlag { get; set; }
        public string Notes { get; set; }
    }

    public class SalesReturnFilter
    {
        public int CustomerID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int BranchID { get; set; }
        public int SalesReturnID { get; set; }
        public int UserID { get; set; }       
    }
}
