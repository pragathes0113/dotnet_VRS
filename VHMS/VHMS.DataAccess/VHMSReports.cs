﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using VHMS;


namespace VHMS.DataAccess
{
    public class VHMSReports
    {
        public static DataSet PrintOPBill(int OPID = 0, string BillNo = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_OPBILLING);
                db.AddInParameter(cmd, "@PK_OPID", DbType.Int32, OPID);
                //db.AddInParameter(cmd, "@BillNo", DbType.String, BillNo);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports ] | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrinDayBook(string iDatefrom = "", string iDateto = "", string iCustomer = "", int CompanyID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_DAYBOOK_GETBYDATE);
                db.AddInParameter(cmd, "@Date1", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@Date2", DbType.String, iDateto);
                db.AddInParameter(cmd, "@VoucherType", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@FK_CompanyID", DbType.Int32, CompanyID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintPurchaseByID(int ID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASEBARCODE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, ID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintProductPaymentInvoice(int v)
        {
            throw new NotImplementedException();
        }

        public static DataSet PrintPurchaseOrder(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASEORDERINVOICE);
                db.AddInParameter(cmd, "@PK_PurchaseOrderID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintExpenseLedgerReport(string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_EXPENSESTATEMENT_GETBYDATE);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_PartyID", DbType.String, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }


        public static DataSet PrintPayment(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PAYMENTINVOICE);
                db.AddInParameter(cmd, "@PK_PaymentID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintReceiptInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_RECEIPTINVOICE);
                db.AddInParameter(cmd, "@PK_ReceiptID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesInvoice(int OPID = 0, string BillNo = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESINVOICE);
                db.AddInParameter(cmd, "@PK_SalesID", DbType.Int32, OPID);
                //db.AddInParameter(cmd, "@BillNo", DbType.String, BillNo);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesEntryInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESENTRYINVOICE);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintServiceDCInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SERVICEDCINVOICE);
                db.AddInParameter(cmd, "@PK_ServiceDCID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintEstimateSalesEntryInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_ESTIMATESALESENTRYINVOICE);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPurchaseEntryInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASEENTRYINVOICE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintBarcode(string Code)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRICING);
                db.AddInParameter(cmd, "@Code", DbType.String, Code);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSMSCode(string Code)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRICING_SMSCODE);
                db.AddInParameter(cmd, "@SMSCode", DbType.String, Code);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintAddress(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_CUSTOMERADDRESS);
                db.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, ID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSupplierAddress(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SUPPLIERADDRESS);
                db.AddInParameter(cmd, "@PK_SupplierID", DbType.Int32, ID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPurchaseReturnInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASERETURNINVOICE);
                db.AddInParameter(cmd, "@PK_PurchaseReturnID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintProfitLoss(string iDatefrom = "", string iDateto = "", string iCustomer = "", string iPaymentMode = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_TB_PROFITLOSS_GETBYDATE);
                db.AddInParameter(cmd, "@Date1", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@Date2", DbType.String, iDateto);
                db.AddInParameter(cmd, "@WhereCondition", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@OrderByExpression", DbType.String, iPaymentMode);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintProfitStatement(string iDatefrom = "", string iDateto = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_PROFITSTATEMENT_GETBYDATE);
                db.AddInParameter(cmd, "@Date1", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@Date2", DbType.String, iDateto);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintAllDownload(string SalesOrderID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESORDERALLDOWNLOAD);
                db.AddInParameter(cmd, "@PK_SalesOrderID", DbType.String, SalesOrderID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintEstimationOrderInvoice(string iDatefrom = "", string iDateto = "",int iIsWithoutGSTFlag=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESORDER);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@IsWithoutGSTFlag", DbType.Boolean, iIsWithoutGSTFlag);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesOrderInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESORDERREPORTDETAILS);
                db.AddInParameter(cmd, "@PK_SalesOrderID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintWorkOrderInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_WORKORDER);
                db.AddInParameter(cmd, "@PK_WorkOrderID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesReturnInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESRETURNINVOICE);
                db.AddInParameter(cmd, "@PK_SalesReturnID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintRetailsSalesInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_RETAILSSALESENTRYINVOICE);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintEstimateRetailsSalesInvoice(int OPID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_RETAILSESTIMATESALESENTRYINVOICE);
                db.AddInParameter(cmd, "@PK_EstimateSalesEntryID", DbType.Int32, OPID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintExchangeInvoice(int OPID = 0, string BillNo = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_EXCHANGEINVOICE);
                db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, OPID);
                //db.AddInParameter(cmd, "@BillNo", DbType.String, BillNo);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesReturn(int OPID = 0, string BillNo = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESRETURNBILL);
                db.AddInParameter(cmd, "@PK_SalesReturnID", DbType.Int32, OPID);
                //db.AddInParameter(cmd, "@BillNo", DbType.String, BillNo);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintQuotation(int OPID = 0, string BillNo = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_QUOTATIONINVOICE);
                db.AddInParameter(cmd, "@PK_QuotationID", DbType.Int32, OPID);
                //db.AddInParameter(cmd, "@BillNo", DbType.String, BillNo);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintPatient(int PatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PATIENT);
                db.AddInParameter(cmd, "@PK_PatientID", DbType.Int32, PatientID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintPatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesTrans(int PatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESTRANSREPORTS);
                db.AddInParameter(cmd, "@SalesID", DbType.Int32, PatientID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintPatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }


        public static DataSet PrintCustomerSales(int PatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_CUSTOMERSALESBILL);
                db.AddInParameter(cmd, "@Customer_ID", DbType.Int32, PatientID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintPatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSales(string iDatefrom = "", string iDateto = "", string iBranch = "", string iPaymentMode = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESREPORTS);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, iPaymentMode);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);

                //db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintUnpaid| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }


        public static DataSet PrintUnPaid(string iDatefrom = "", string iDateto = "", string iCustomer = "", string iScheme = "", string iBranch = "", string iEmployeeCode = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_UNPAIDREPORTS);
                //db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, ExchangeID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@PK_RegisterID", DbType.String, iScheme);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);
                db.AddInParameter(cmd, "@FK_UserID", DbType.String, iEmployeeCode);

                //db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintUnpaid| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintRegister(string iDatefrom = "", string iDateto = "", string iMobileNo = "", string iScheme = "", string iBranch = "", string iEmployeeCode = "", int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_REGISTERREPORTS);
                //db.AddInParameter(cmd, "@PK_RegisterID", DbType.Int32, RegisterID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                //db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, iMobileNo);
                db.AddInParameter(cmd, "@FK_ChitID", DbType.String, iScheme);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);
                db.AddInParameter(cmd, "@FK_UserID", DbType.String, iEmployeeCode);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                //db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintRegister| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintVisitCustomer(string iDatefrom = "", string iDateto = "", string iBranch = "", string iEmployeeCode = "", int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_VISITCUSTOMER);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);
                db.AddInParameter(cmd, "@FK_UserID", DbType.String, iEmployeeCode);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintRegister| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintRenewal(string iDatefrom = "", string iDateto = "", string iMobileNo = "", string iScheme = "", string iBranch = "", string iEmployeeCode = "", int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_RENEWALREPORTS);
                //db.AddInParameter(cmd, "@PK_RegisterID", DbType.Int32, RegisterID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                // db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, iMobileNo);
                db.AddInParameter(cmd, "@FK_ChitID", DbType.String, iScheme);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);
                db.AddInParameter(cmd, "@FK_UserID", DbType.String, iEmployeeCode);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                //db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintRenewal| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintProduct(int iCategory = 0, int iSubCategory = 0, int iSupplierID = 0, int iProductID = 0, int ProductType = 0, string orderby = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRODUCT);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@PK_ProductID", DbType.Int32, iProductID);
                db.AddInParameter(cmd, "@FK_ProductTypeID", DbType.Int32, ProductType);
                db.AddInParameter(cmd, "@Order", DbType.String, orderby);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintRenewal| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintProductOrderWise(int iCategory = 0, int iSubCategory = 0, int iSupplierID = 0, int iProductID = 0, string orderby = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRODUCTORDERWISE);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@PK_ProductID", DbType.Int32, iProductID);
                db.AddInParameter(cmd, "@Order", DbType.String, orderby);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintRenewal| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintReorderProduct(int iCategory = 0, int iSubCategory = 0, int iSupplierID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_REORDERPRODUCT);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintRenewal| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintReceipt(string iDatefrom = "", string iDateto = "", string iMobileNo = "", string iScheme = "", string iBranch = "", string iEmployeeCode = "", int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_RECEIPTREPORT);
                //db.AddInParameter(cmd, "@PK_RegisterID", DbType.Int32, RegisterID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                // db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, iMobileNo);
                db.AddInParameter(cmd, "@FK_ChitID", DbType.String, iScheme);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);
                db.AddInParameter(cmd, "@FK_UserID", DbType.String, iEmployeeCode);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                //db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintRenewal| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet Printclosed(string iDatefrom = "", string iDateto = "", string iMobileNo = "", string iScheme = "", string iBranch = "", string iEmployeeCode = "", int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_CLOSEDREPORT);
                //db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, ExchangeID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                // db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, iMobileNo);
                db.AddInParameter(cmd, "@FK_ChitID", DbType.String, iScheme);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);
                db.AddInParameter(cmd, "@FK_UserID", DbType.String, iEmployeeCode);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                //db.AddInParameter(cmd, "@Account", DbType.String, iAccountNo);
                //db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports Printclosed| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet Printclosed(string iCustomer = "", string iScheme = "", string iBranch = "", string iEmployeeCode = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_CLOSEDREPORT);
                //db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, ExchangeID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@PK_RegisterID", DbType.String, iScheme);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);
                db.AddInParameter(cmd, "@FK_UserID", DbType.String, iEmployeeCode);
                //db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports Printclosed| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintUnPaid(string iCustomer = "", string iScheme = "", string iBranch = "", string iEmployeeCode = "", int iRegionID = 0, int iZoneID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_UNPAIDREPORTS);
                //db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, ExchangeID);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@PK_RegisterID", DbType.String, iScheme);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);
                db.AddInParameter(cmd, "@FK_UserID", DbType.String, iEmployeeCode);
                db.AddInParameter(cmd, "@FK_RegionID", DbType.Int32, iRegionID);
                db.AddInParameter(cmd, "@FK_ZoneID", DbType.Int32, iZoneID);
                //db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintUnpaid| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintPatientDetails(int PatientID = 0, string iDatefrom = "", string iDateto = "", string iCategory = "", string iReferredby = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PATIENTDETAILS);
                db.AddInParameter(cmd, "@FK_PatientID", DbType.Int32, PatientID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@Category", DbType.String, iCategory);
                db.AddInParameter(cmd, "@ReferredBy", DbType.String, iReferredby);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintPatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintNewPatientDetails(int PatientID = 0, string iDatefrom = "", string iDateto = "", int iCategory = 0, string iReferredby = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_NEWPATIENTDETAILS);
                db.AddInParameter(cmd, "@FK_PatientID", DbType.Int32, PatientID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@PK_DescriptionID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@ReferredBy", DbType.String, iReferredby);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintPatient | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintStockDetails(string iProduct = "",string iSupplier ="")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_STOCK);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, iProduct);
                db.AddInParameter(cmd, "@PK_SupplierID", DbType.String, iSupplier);
                
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintStockReport(string iCategory = "", string iSubCategory = "", string iProduct = "", string iSupplier = "", string iProductType = "", string iType = "", string iProductCode = "", string UnitID = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_STOCKSUMMARYREPORT);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplier);
                db.AddInParameter(cmd, "@FK_ProductTypeID", DbType.Int32, iProductType);
                db.AddInParameter(cmd, "@FK_UnitID", DbType.Int32, UnitID);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);
                db.AddInParameter(cmd, "@ProductCode", DbType.String, iProductCode);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintBarcodeReport(string iCategory = "", string iSubCategory = "", string iProduct = "", string iSupplier = "", string iBarcode = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_BARCODE);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplier);
                db.AddInParameter(cmd, "@Barcode", DbType.String, iBarcode);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintStockDetails1(string iCategory = "", string iProduct = "", string iBranch = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_STOCKSUMMARY);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, iProduct);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintStock | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintSalesEntry(string iDatefrom = "", string iDateto = "", string iCustomer = "", string iCompany = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESENTRY);

                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@FK_CompanyID", DbType.String, iCompany);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintWorkEntry(int SalesID = 0, string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_WORKORDERREPORT);
                db.AddInParameter(cmd, "@PK_WorkOrderID", DbType.Int32, SalesID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintEstimateEntry(int SalesID = 0, string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_ESTIMATEREPORT);
                db.AddInParameter(cmd, "@PK_SalesOrderID", DbType.Int32, SalesID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintTDSSalesEntry(int SalesID = 0, string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_TDSPAYMENTENTRY);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet AdvanceReport(string iDatefrom = "", string iDateto = "", string iCustomer = "", string iPaymentMode = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.Rpt_Select_Advance);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, iCustomer);
                db.AddInParameter(cmd, "@AdvanceType", DbType.String, iPaymentMode);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet SalaryReport(string iDatefrom = "", string iDateto = "", string iCustomer = "", string iPaymentMode = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALARY);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, iCustomer);
                db.AddInParameter(cmd, "@Type", DbType.String, iPaymentMode);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintServiceSpare(string iDatefrom = "", string iDateto = "", string iCustomer = "", string iProduct = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SERVICESPARETRANS);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, iProduct);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }


        public static DataSet PrintRetailsSalesEntry(int SalesID = 0, string iDatefrom = "", string iDateto = "", string iCustomer = "", string iPaymentMode = "", string iProduct = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_RETAILSSALESENTRY);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, SalesID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@PaymentMode", DbType.String, iPaymentMode);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, iProduct);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintSalesReturnDetailedReport(int SalesID = 0, string iDatefrom = "", string iDateto = "", string iCustomer = "", string iPaymentMode = "", string iProduct = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESRETURNDETAILED);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, iProduct);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesPending(string iDatefrom = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESPENDING);
                db.AddInParameter(cmd, "@DueDays", DbType.Int32, iDatefrom);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPaymentReport(string iDatefrom = "", string iDateto = "", string iSupplierID = "", string Type = "", int BillType = 1)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PAYMENT);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                db.AddInParameter(cmd, "@Type", DbType.String, Type);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintStockCheck(string iDatefrom = "", string iDateto = "", int iSupplierID = 0, string iType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_STOCKCHECKTRANS);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@PK_StockCheckID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }


        public static DataSet PrintReceiptReport(string iDatefrom = "", string iDateto = "", string iCustomerID = "", string Type = "", int iIsRetail = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_RECEIPT);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@Type", DbType.String, Type);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                db.AddInParameter(cmd, "@IsRetail", DbType.Int32, iIsRetail);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintRetailSalesPending(string iDatefrom = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_RETAILPENDING);
                db.AddInParameter(cmd, "@DueDays", DbType.Int32, iDatefrom);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPurchasePending(string iDatefrom = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASEPENDING);
                db.AddInParameter(cmd, "@DueDays", DbType.Int32, iDatefrom);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPurchaseDetailed(string iDatefrom = "", string iDateto = "", string iSupplierID = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_PURCHASESTATEMENT_GETDYNAMIC);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet GetSalesInvoice(string FieldName, string TableName, string WhereCondition)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbAttendanceCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_ALLTABLE);
                db.AddInParameter(cmd, "@FieldName", DbType.String, FieldName);
                db.AddInParameter(cmd, "@TableName", DbType.String, TableName);
                db.AddInParameter(cmd, "@WhereCondition", DbType.String, WhereCondition);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintAttenndance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet GetInAttendanceLog(string FieldName, string TableName, string WhereCondition)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbAttendanceCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_EMPLOYEELOG_GETDYNAMIC);
                db.AddInParameter(cmd, "@FieldName", DbType.String, FieldName);
                db.AddInParameter(cmd, "@TableName", DbType.String, TableName);
                db.AddInParameter(cmd, "@WhereCondition", DbType.String, WhereCondition);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintAttenndance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet GetCustomer(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMER);
                db.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, ID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintAttenndance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet GetEmployee(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_EMPLOYEE);
                db.AddInParameter(cmd, "@PK_EmployeeID", DbType.Int32, ID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintAttenndance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet GetCustomers(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMER);
                db.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, ID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintAttenndance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet GetArea(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_AREA);
                db.AddInParameter(cmd, "@PK_AreaID", DbType.Int32, ID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintAttenndance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet GetCity(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CITY);
                db.AddInParameter(cmd, "@PK_CityID", DbType.Int32, ID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintAttenndance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet GetCompany(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_COMPANYLOAD);
                db.AddInParameter(cmd, "@PK_CompanyID", DbType.Int32, ID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintAttenndance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet GetState(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STATES);
                db.AddInParameter(cmd, "@PK_StateID", DbType.Int32, ID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintAttenndance | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet GetShift(int ID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SHIFT);
                db.AddInParameter(cmd, "@PK_ShiftID", DbType.Int32, ID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintShift | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintPurchase(string iDatefrom = "", string iDateto = "", int PurchaseID = 0, string iSupplierID = "", int BillType = 1, string iType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASE);
                db.AddInParameter(cmd, "@PK_PurchaseID", DbType.Int32, PurchaseID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@BillType", DbType.Int32, BillType);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet GetSalesInvoices(int iCustomerID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESINVOICES);
                db.AddInParameter(cmd, "@PK_SalesInvoiceID", DbType.Int32, iCustomerID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet GetSalesInvoiceReport(string iDatefrom = "", string iDateto = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESINVOICESREPORT);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintBillAmountClear(string iDatefrom = "", string iDateto = "", string iSupplierID = "", string iType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USB_SELECT_RETAILAMOUNTCLEAR);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@Status", DbType.String, iType);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintHSNReportSummary(string iDatefrom = "", string iDateto = "", int iProductID = 0, string TransactionType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_HSNREPORTSUMMARY);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProductID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@TransactionType", DbType.String, TransactionType);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintHSNReportDetailed(string iDatefrom = "", string iDateto = "", int iProductID = 0, string TransactionType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_HSNREPORTDETAILED);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProductID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@TransactionType", DbType.String, TransactionType);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintPurchaseReturn(string iDatefrom = "", string iDateto = "", int PurchaseID = 0, string iSupplierID = "", int Type = 1)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASERETURN);
                db.AddInParameter(cmd, "@PK_PurchaseReturnID", DbType.Int32, PurchaseID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintProductReplacement(string iDatefrom = "", string iDateto = "", int PurchaseID = 0, int iSupplierID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRODUCTREPLACEMENT);
                db.AddInParameter(cmd, "@FK_ReplacedProductID", DbType.Int32, PurchaseID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iSupplierID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintServiceEntry(string iDatefrom = "", string iDateto = "", int PurchaseID = 0, int iSupplierID = 0, string iStatus = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SERVICE);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, PurchaseID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPurchaseReturnDetailed(string iDatefrom = "", string iDateto = "", string iSupplierID = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASERETURNDETAILED);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintInwardReport(string iDatefrom = "", string iDateto = "", string iProduct = "", string iCategory = "", string iSubCategory = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_INWARD);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPurchaseGST(string iDatefrom = "", string iDateto = "", string iType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASEGST);
                db.AddInParameter(cmd, "@Date1", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@Date2", DbType.String, iDateto);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesGST(string iDatefrom = "", string iDateto = "", string iType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESGST);
                db.AddInParameter(cmd, "@Date1", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@Date2", DbType.String, iDateto);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintSalesEntry(int SalesID = 0, string iDatefrom = "", string iDateto = "", string iCustomer = "", string iCompany = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESENTRY);
                db.AddInParameter(cmd, "@PK_SalesEntryID", DbType.Int32, SalesID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@FK_CompanyID", DbType.String, iCompany);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesReturnGST(string iDatefrom = "", string iDateto = "", string iType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESRETURNGST);
                db.AddInParameter(cmd, "@Date1", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@Date2", DbType.String, iDateto);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPurchaseReturnGST(string iDatefrom = "", string iDateto = "", string iType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASERETURNGST);
                db.AddInParameter(cmd, "@Date1", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@Date2", DbType.String, iDateto);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintProductWiseSalesReport(string iDatefrom = "", string iDateto = "", string iProduct = "", string iCategory = "", string iSubCategory = "", string iSupplier = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRODUCTWISESALES);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintProductWisePurchaseReport(string iDatefrom = "", string iDateto = "", string iProduct = "", string iCategory = "", string iSubCategory = "", string iSupplier = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRODUCTWISEPURCHASE);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplier);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintProductWisePurchaseSummaryReport(string iDatefrom = "", string iDateto = "", string iProduct = "", string iCategory = "", string iSubCategory = "", string iSupplier = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRODUCTWISEPURCHASESUMMARY);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplier);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }


        public static DataSet PrintProductWiseSalesSummaryReport(string iDatefrom = "", string iDateto = "", string iProduct = "", string iCategory = "", string iSubCategory = "", string iSupplier = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRODUCTWISESALESSUMMARY);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplier);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintProcessingPendingReport(string iProduct = "", string iCategory = "", string iSubCategory = "", string iSupplier = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PROCESSINGPENDING);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iSupplier);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintProcessingInwardReport(string iDatefrom = "", string iDateto = "", string iProduct = "", string iCategory = "", string iSubCategory = "", string iSupplier = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PROCESSINGINWARD);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iSupplier);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintProcessingOutwardReport(string iDatefrom = "", string iDateto = "", string iProduct = "", string iCategory = "", string iSubCategory = "", string iSupplier = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PROCESSINGOUTWARD);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, iProduct);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, iCategory);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, iSubCategory);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.Int32, iSupplier);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintExchange(int ExchangeID = 0, string iDatefrom = "", string iDateto = "", string iCustomer = "", string iBranch = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_Exchange);
                db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, ExchangeID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.String, iBranch);
                //db.AddInParameter(cmd, "@Status", DbType.String, iStatus);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSales | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintReturn(string iDatefrom = "", string iDateto = "", string iCustomer = "",int CompanyID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESRETURN);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@FK_CompanyID", DbType.Int32, CompanyID);
                

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSalesReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPurchaseOrder(string iDatefrom = "", string iDateto = "", string iSupplier = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PURCHASEORDER);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplier);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSalesReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintCustomer(int CustomerID = 0, string iCustomerType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_CUSTOMER);
                db.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, CustomerID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, iCustomerType);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSupplier()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SUPPLIER);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintVendor()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_VENDOR);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet GetLoginDetails(string UserName, string Password)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_USERLOGIN);
                db.AddInParameter(cmd, "@username", DbType.String, UserName);
                db.AddInParameter(cmd, "@password", DbType.String, CommonMethods.Security.Encrypt(Password, true));
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSchemeLedgerReport(string iMoblieNo = "", string iRegisterNo = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SCHEMELEDGERREPORT);
                // db.AddInParameter(cmd, "@PK_ExchangeID", DbType.Int32, ExchangeID);
                // db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                // db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@RegisterNo", DbType.String, iRegisterNo);
                db.AddInParameter(cmd, "@MoblieNo", DbType.String, iMoblieNo);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesLedgerReport(string iDatefrom = "", string iDateto = "", string iCustomer = "",int CompanyID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SALESSTATEMENT_GETBYDATE);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@FK_CompanyID", DbType.Int32, CompanyID);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintStockLedgerReport(string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_STOCKREPORT_GETBYDATE);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintBankStatement(string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_BANKSTATEMENT_GETBYDATE);
                db.AddInParameter(cmd, "@Date1", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@Date2", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_BankID", DbType.String, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintDayBook(string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_DAYBOOK_GETBYDATE);
                db.AddInParameter(cmd, "@Date1", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@Date2", DbType.String, iDateto);
                db.AddInParameter(cmd, "@VoucherType", DbType.String, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintPurchaseLedgerReport(string iDatefrom = "", string iDateto = "", string iCustomer = "", int iType = 1,int CompanyID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_PURCHASESTATEMENT_GETBYDATE);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.String, iCustomer);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, iType);
                db.AddInParameter(cmd, "@FK_CompanyID", DbType.Int32, CompanyID);
                

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintBillLedgerReport(string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_PURCHASEBILLSTATEMENT_GETBYDATE);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintSalesBillLedgerReport(string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SALESBILLSTATEMENT_GETBYDATE);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintVendorLedgerReport(string iDatefrom = "", string iDateto = "", string iCustomer = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_VENDORSTATEMENT_GETBYDATE);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@FK_VendorID", DbType.String, iCustomer);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
        public static DataSet PrintPricingLogReport(string iDatefrom = "", string iDateto = "", int iCustomer = 0, int iSupplier = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRICELOG);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@PK_ProductID", DbType.Int32, iCustomer);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplier);

                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintSchemeLedgerReport | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet SalesTaxReturnReport(int FromMonth = 0, int FromYear = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_SALESTAXRETURN);
                db.AddInParameter(cmd, "@FromYear", DbType.Int32, FromYear);
                db.AddInParameter(cmd, "@FromMonth", DbType.Int32, FromMonth);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintAttendance(int EmployeeID = 0, string WhereCondition = "", string Year = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_ATTENDANCELOGREPORT);
                db.AddInParameter(cmd, "@Month", DbType.String, WhereCondition);
                db.AddInParameter(cmd, "@Year", DbType.String, Year);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, EmployeeID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintPaysilp(int EmployeeID = 0, string WhereCondition = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PAYSLIP);
                db.AddInParameter(cmd, "@MonthYear", DbType.String, WhereCondition);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, EmployeeID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet PrintAttendanceBYID(int EmployeeID = 0, string WhereCondition = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_ATTENDANCELOGREPORT_BYDYNAMIC);
                db.AddInParameter(cmd, "@Month", DbType.String, WhereCondition);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, EmployeeID);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }


        public static DataSet MarginReport(int SupplierID = 0, int CategoryID = 0, int SubcategoryID = 0, int ProductID = 0, int CustomerID = 0, string iDatefrom = "", string iDateto = "", string iType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_MARGINREPORT);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, SupplierID);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, CategoryID);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, SubcategoryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, ProductID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, CustomerID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }

        public static DataSet MarginReport1(int SupplierID = 0, int CategoryID = 0, int SubcategoryID = 0, int ProductID = 0, int CustomerID = 0, string iDatefrom = "", string iDateto = "", string iType = "")
        {
            string sException = string.Empty;
            Database db;
            DataSet dsReportData = null;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_MARGINREPORT_GETDYNAMIC);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, SupplierID);
                db.AddInParameter(cmd, "@FK_CategoryID", DbType.Int32, CategoryID);
                db.AddInParameter(cmd, "@FK_SubCategoryID", DbType.Int32, SubcategoryID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, ProductID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, CustomerID);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, iDatefrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, iDateto);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);
                dsReportData = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.VHMSReports PrintOPBill | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsReportData;
        }
    }
}
