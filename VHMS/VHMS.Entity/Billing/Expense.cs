﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        public string ExpenseNo { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string sExpenseDate { get; set; }
        public Ledger Party { get; set; }
        public Company Company { get; set; }
        public Tax Tax { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string GSTIN { get; set; }
        public string Narration { get; set; }
        public string BillNo { get; set; }
        public Int16 ReceiptModeID { get; set; }
        public Ledger Bank { get; set; }
        public string ChequeNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string sIssueDate { get; set; }
        public string Status { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CUserName { get; set; }
        public Collection<ExpenseTrans> ExpenseTrans { get; set; }
    }

    public class ExpenseTrans
    {
        public int ExpenseTransID { get; set; }
        public int ExpenseID { get; set; }
        public decimal Amount { get; set; }
        public Ledger Ledger { get; set; }
        public string Notes { get; set; }
        public string StatusFlag { get; set; }
    }

    public class ExpenseFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int DoctorID { get; set; }
        public int UserID { get; set; }
    }
}
