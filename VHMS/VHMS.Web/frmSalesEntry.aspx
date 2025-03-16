﻿<%@ Page Title="Sales Entry" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmSalesEntry.aspx.cs" Inherits="frmSalesEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        .btnPrint, .btnPrintbill {
            background-color: #ef00bc !important;
            margin-top: 0px !important;
        }

        table.formatHTML5 tr.selected {
            background-color: #e92929 !important;
            color: #fff;
            vertical-align: middle;
            padding: 1.5em;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Sales Entry
                </h3>
            </div>
            <div class="breadcrumb">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
                <button id="btnList" class="btn btn-info">
                    <i class="fa fa-list"></i>&nbsp;&nbsp;List</button>
            </div>
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="divTab">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">General</a></li>
                    <li><a id="aSearchResult" href="#SearchResult" data-toggle="tab">Search Result</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="table-responsive">
                                    <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Invoice #</th>
                                                <th>Date</th>
                                                <th>Customer</th>
                                                <th>Total Qty</th>
                                                <th>Net Amount</th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tblRecord_tbody">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="SearchResult">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="form-group" id="divSearchaname">
                                    <label>
                                        Search Sales records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Invoice #</th>
                                                <th>Date</th>
                                                <th>Customer</th>
                                                <th>Total Qty</th>
                                                <th>Net Amount</th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tblSearchResult_tbody">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-primary" id="divOPBilling">
                <div class="box-header with-border">
                    <div class="box-title">Sales Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-1" id="divBillNo">
                            <label>
                                Invoice No</label>
                            <input type="text" class="form-control" id="txtBillNo" placeholder="Invoice No"
                                maxlength="15" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divBillDate">
                            <label>
                                Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-3" id="divCustomer">
                            <label>
                                Customer</label><span class="text-danger">*</span>
                            <select id="ddlCustomerName" class="form-control select2" data-placeholder="Select Customer Name" tabindex="3"></select>
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Add Customer</label>
                            <button id="btnCustomerAdd" class="btn btn-info">
                                <i class="fa fa-plus-square"></i>
                            </button>
                        </div>
                        <div class="form-group col-md-2" id="divMobileNo">
                            <label>
                                Mobile No</label>
                            <input type="text" class="form-control" id="txtMobileNo" placeholder="MobileNo"
                                maxlength="10" tabindex="-1" readonly />
                        </div>
                        <div class="form-group col-md-3" id="divAddress">
                            <label>
                                Address</label>
                            <input type="text" class="form-control" id="txtAddress" placeholder="Address"
                                maxlength="10" tabindex="-1" readonly />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-2" id="divCustomerType" style="display: none">
                            <label>
                                Cust.Type</label><span class="text-danger">*</span>
                            <select id="ddlCustomerType" class="form-control select2" data-placeholder="Select Customer Type" tabindex="4">
                            </select>
                        </div>

                        <div class="form-group col-md-1" style="display: none">
                            <label>
                                GST No</label>
                        </div>
                        <div class="form-group col-md-2" id="divGsTNo" style="display: none">
                            <input type="text" class="form-control" id="txtGSTNo" placeholder="GSTNo"
                                maxlength="10" tabindex="-1" />
                        </div>
                        <div class="form-group col-md-1" style="display: none">
                            <label>
                                Tax</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divTaxName" style="display: none">

                            <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax Name" tabindex="5"></select>
                        </div>
                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-1" id="divSno" style="width: 6%;">
                                    <label>
                                        S.No</label>
                                    <input type="text" class="form-control TRSearch" id="txtSNo" placeholder="Sno"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divProductName" style="margin-left: -21px;">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <select id="ddlProductName" class="form-control select2" data-placeholder="Select Product Name" tabindex="7"></select>
                                </div>
                                <div class="form-group col-md-1" id="divBatchNo" style="margin-left: -21px; display: none">
                                    <label>
                                        BatchNo</label><span class="text-danger">*</span>
                                    <select id="ddlBatchNo" class="form-control select2" data-placeholder="Select Batch No" tabindex="7"></select>
                                </div>
                                <div class="form-group col-md-1" id="divPurchasePrice" style="margin-left: -21px;">
                                    <label>
                                        PR</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtPurchasePrice" placeholder="Rate"
                                        maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-1" id="divPreviousPrice" style="margin-left: -21px;">
                                    <label>
                                        PSR</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtPreviousPrice" placeholder="Rate"
                                        maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-1" id="divSalesMargin" style="margin-left: -21px;display:none">
                                    <label>
                                        SM</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtSalesMargin" placeholder="Rate"
                                        maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-1" id="divAvailableQty" style="margin-left: -21px;">
                                    <label>
                                        Available Qty</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtAvailableQty" placeholder="Available"
                                        maxlength="12" tabindex="8" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-1" id="divQuantity" style="margin-left: -21px;">
                                    <label>
                                        Quantity</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Quantity"
                                        maxlength="12" tabindex="8" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divRate" style="margin-left: -21px;">
                                    <label>
                                        Rate</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRate" placeholder="Rate"
                                        maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divTaxTrans" style="margin-left: -21px; display: none">
                                    <label>
                                        Tax</label><span class="text-danger">*</span>
                                    <select id="ddlTax" class="form-control select2" data-placeholder="Select Tax " tabindex="10"></select>
                                </div>
                                <div class="form-group col-md-1" id="divTaxAmt" style="margin-left: -21px; display: none">
                                    <label>
                                        Tax Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtTaxAmt" placeholder="Tax. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divDisPer" style="margin-left: -21px;">
                                    <label>
                                        Disc %</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtDisPer" placeholder="Discount %"
                                        maxlength="12" tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divDisAmt" style="margin-left: -21px;">
                                    <label>
                                        Disc. Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtDisAmt" placeholder="Disc. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divFrequency" style="margin-left: -21px;">
                                    <label>
                                        Subtotal</label>
                                    <input type="text" class="form-control TRSearch" id="txtSubTotal" placeholder="Subtotal"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divBarcode" style="display: none">
                                    <input type="text" class="form-control TRSearch" id="txtBarcode" placeholder="Subtotal"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>

                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="12">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="13">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive" style="min-height: 150px !Important;">
                            <div id="divOPBillingList">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="display: none">
                            <label>
                                Discount %</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-1" id="divDiscountPercent" style="display: none">
                            <input type="text" class="form-control" id="txtDiscountPercent" placeholder="Discount Percent"
                                maxlength="15" tabindex="14" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Total Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divAmount">
                            <input type="text" class="form-control" id="txtAmount" placeholder="Total Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-7"></div>
                        <div class="form-group col-md-1">
                            <label style="font-size: 17px;">
                                Total Qty</label>
                        </div>
                        <div class="form-group col-md-1" id="divTotalQty">
                            <input type="text" class="form-control" id="txtTotalQty" style="font-weight: bold; font-size: 17px;" placeholder="Total Qty"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Discount Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divDiscountAmount">
                            <input type="text" class="form-control" id="txtDiscountAmount" placeholder="Discount Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="15" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <div class="form-group col-md-7"></div>
                        <div class="form-group col-md-2" style="display: none;">
                            <input type="checkbox" id="chk" value="value" />
                            <label for="chk">Calculate TCS </label>

                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                TCS %</label>
                        </div>
                        <div class="form-group col-md-1" id="divTCSPercent" style="display: none;">
                            <input type="text" class="form-control" id="txtTCSPercent" placeholder="TCS Percent"
                                maxlength="15" tabindex="16" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                TCS Amt</label>
                        </div>
                        <div class="form-group col-md-1" id="divTCSAmount" style="display: none;">
                            <input type="text" class="form-control" id="txtTCSAmount" placeholder="TCS Amount"
                                maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                CGST</label>
                        </div>
                        <div class="form-group col-md-1" id="divCGST">
                            <input type="text" class="form-control" id="txtCGST" placeholder="CGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>

                        <div class="form-group col-md-1">
                            <label>
                                Taxable Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divTotalAmount">
                            <input type="text" class="form-control" id="txtTotalAmount" placeholder="Total Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <div class="form-group col-md-9">
                        </div>

                        <div class="form-group col-md-1">
                            <label>
                                SGST</label>
                        </div>
                        <div class="form-group col-md-1" id="divSGST">
                            <input type="text" class="form-control" id="txtSGST" placeholder="SGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Tax Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTaxAmount">
                            <input type="text" class="form-control" id="txtTaxAmount" placeholder="Tax Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                After Tax Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTotalTaxAmount">

                            <input type="text" class="form-control" id="txtTotalTaxAmount" placeholder="Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="21" value="0" autocomplete="off" readonly />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9">
                        </div>
                        <div class="form-group col-md-1" style="display: none">
                            <label>
                                IGST</label>
                        </div>
                        <div class="form-group col-md-1" id="divIGST" style="display: none">
                            <input type="text" class="form-control" id="txtIGST" placeholder="SGST"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Other Charges</label>
                        </div>
                        <div class="form-group col-md-2" id="divOtherCharges">
                            <input type="text" class="form-control" id="txtOtherCharges" placeholder="Other Charges" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="28" value="0" autocomplete="off" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-2" id="divRCustomerToatlAmountall" style="display: none;">
                            <input type="text" class="form-control" id="txtCustomerToatlAmountall" placeholder="ToatlAmount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="28" value="0" autocomplete="off" readonly />
                        </div>
                        <div class="form-group col-md-2" id="divRCustomerToatlAmount" style="display: none;">
                            <input type="text" class="form-control" id="txtCustomerToatlAmount" placeholder="ToatlAmount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="28" value="0" autocomplete="off" readonly />
                        </div>
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1">
                            <label>
                                Roundoff</label>
                        </div>
                        <div class="form-group col-md-2" id="divRoundoff">
                            <input type="text" class="form-control" id="txtRoundoff" placeholder="Roundoff" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="28" value="0" autocomplete="off" readonly />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-7"></div>
                        <div class="form-group col-md-1">
                            <label>
                                Payment Mode</label>
                        </div>
                        <div class="form-group col-md-1">
                            <select id="ddlPaymentMode" class="form-control" tabindex="-1">
                                <option value="Credit" selected="selected">Credit</option>
                                <option value="Cash">Cash</option>
                                <option value="Card">Card</option>
                                <option value="NEFT/RTGS">NEFT/RTGS</option>
                            </select>
                        </div>
                        <div class="form-group col-md-1">
                            <label style="font-size: 20px;">
                                Net Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divNetAmount">
                            <input type="text" class="form-control" id="txtNetAmount" placeholder="Net Amount" style="font-weight: bold; font-size: 20px;"
                                maxlength="15" tabindex="-1" readonly="true" style="width: 192px;" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <div class="form-group col-md-1">
                            <label>
                                Transport Name</label>
                        </div>
                        <div class="form-group col-md-2" id="divTransportName">
                            <select id="ddlTransport" class="form-control select2" data-placeholder="Select Transport Name" tabindex="17"></select>
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Transport GSTNo</label>
                        </div>
                        <div class="form-group col-md-2" id="divTransportGSTNo">
                            <input type="text" class="form-control" id="txtTransportGSTNo" placeholder="Transport GSTNo"
                                maxlength="50" tabindex="-1" autocomplete="off" readonly="true" />
                        </div>

                        <div class="form-group col-md-2" style="display: none;">
                            <button id="btnLink" type="button" class="btn btn-link">
                                <i class="fa fa-link" aria-hidden="true"></i>&nbsp;&nbsp;
                              Eway link</button>
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                VehicleNo</label>
                        </div>
                        <div class="form-group col-md-2" id="divVehicleNo">

                            <input type="text" class="form-control" id="txtVehicleNo" placeholder=" Vehicle No"
                                maxlength="50" tabindex="18" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="text-align: right;">
                            <label>
                                Transport Charges</label>
                        </div>
                        <div class="form-group col-md-2" id="divTransportCharge">
                            <input type="text" class="form-control" id="txtTransportCharge" placeholder="Transport Charges" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="28" value="0" autocomplete="off" />
                        </div>

                        <div class="form-group col-md-2"></div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                TenderAmount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTenderAmount" style="display: none;">

                            <input type="text" class="form-control" id="txtTenderAmount" placeholder="TenderAmount"
                                maxlength="15" tabindex="-1" value="0" style="font-size: 20px;" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                BalanceGiven</label>
                        </div>
                        <div class="form-group col-md-2" id="divBalanceGiven" style="display: none;">

                            <input type="text" class="form-control" id="txtBalanceGiven" placeholder="BalanceGiven"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" style="font-size: 25px;" autocomplete="off" />
                        </div>

                    </div>
                    <div class="row">
                    </div>
                    <div class="row">
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                LR No</label>
                        </div>
                        <div class="form-group col-md-1" id="divLRNo" style="display: none;">
                            <input type="text" class="form-control" id="txtLRNo" placeholder=" LR No"
                                maxlength="50" tabindex="-1" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                LR Date</label>
                        </div>
                        <div class="form-group col-md-2" id="divLRDate" style="display: none;">
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="-1" id="txtLRDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-1"></div>

                    </div>
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" id="divOtherPasswordlbl">
                            <label>
                                Password</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divOtherPassword">
                            <input type="password" class="form-control" id="txtOtherPassword" placeholder="Special Password" autocomplete="off" maxlength="512"
                                tabindex="22" />
                        </div>
                    </div>
                    <div class="form-group col-md-12" id="divNote">
                        <label>
                            Notes</label>
                        <textarea id="txtComments" class="form-control" maxlength="255" tabindex="23" rows="3" aria-autocomplete="none"></textarea>
                    </div>
                </div>

                <div class="row" style="display: none">
                    <div class="form-group col-md-1">
                        <label>
                            EWayNo</label>
                    </div>
                    <div class="form-group col-md-2" id="divEWayNo">
                        <input type="text" class="form-control" id="txtEWayNo" placeholder="EWayNo"
                            maxlength="50" tabindex="19" autocomplete="off" />
                    </div>
                    <div class="form-group col-md-1">
                        <label>
                            Noof Bages</label>
                    </div>
                    <div class="form-group col-md-2" id="divNoofBages">
                        <input type="text" class="form-control" id="txtNoofBages" placeholder=" Noof Bages"
                            maxlength="50" tabindex="20" autocomplete="off" />
                    </div>
                    <div class="form-group col-md-1">
                        <label>
                            Narration</label>
                    </div>
                    <div class="form-group col-md-2" id="divNarration">
                        <input type="text" class="form-control" id="txtNarration" placeholder="Narration"
                            maxlength="500" tabindex="21" autocomplete="off" />
                    </div>

                    <div class="row" style="display: none">
                        <div class="form-group col-md-4">
                            <label>
                                Image 1</label>
                            <button id="btnClearImage1" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile2" data-image-src="imgUpload2_view" accept="image/*" onchange="ResizeImage('imagefile2');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload2_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-4">
                            <label>
                                Image 2</label>
                            <button id="btnClearImage2" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile3" data-image-src="imgUpload3_view" accept="image/*" onchange="ResizeImage('imagefile3');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload3_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-4">
                            <label>
                                Image 3</label>
                            <button id="btnClearImage3" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagefile" type="file" id="imagefile4" data-image-src="imgUpload4_view" accept="image/*" onchange="ResizeImage('imagefile4');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUpload4_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                    </div>
                </div>
                <div class="row" style="display: none" id="divBank">
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="margin-left: -11px;">
                            <label>
                                Card No</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divCardNo">

                            <input type="text" class="form-control" id="txtCardNo" placeholder="CardNo"
                                maxlength="16" tabindex="24" style="width: 192px;" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="margin-left: -11px;">
                            <label>
                                Card Charges</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divCardCharges">

                            <input type="text" class="form-control" id="txtCardCharges" placeholder="CardCharges"
                                maxlength="15" tabindex="25" value="0" style="width: 192px;" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                    </div>
                    <div class="form-group col-md-9"></div>
                    <div class="form-group col-md-1">
                        <label>
                            Bank</label>
                    </div>
                    <div class="form-group col-md-2">
                        <select id="ddlBankName" class="form-control select2" data-placeholder="Select Bank Name" tabindex="26"></select>
                    </div>

                </div>

                <div class="modal-footer clearfix">
                    <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="27">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                    <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="28">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Update</button>
                    <button type="button" class="btn btn-danger margin pull-right" id="btnClose" tabindex="29">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                    <button id="btnPrintbill" type="button" class="btn btn-info btnPrint pull-left" style="margin-top: 10px !important;" tabindex="30">
                        <i class="fa fa-print"></i>&nbsp;&nbsp; Print</button>
                </div>
            </div>

            <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group" id="divReason">
                                <label>
                                    Reason</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtReason" placeholder="Please enter Reason"
                                    maxlength="150" tabindex="31" />
                            </div>
                            <div class="form-group" id="divPassword">
                                <label>
                                    Password</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtPassword" placeholder="Please enter Password"
                                    maxlength="150" tabindex="32" autocomplete="off" />
                            </div>
                            <div class="form-group" id="divID">
                                <label>
                                    ID</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtID"
                                    maxlength="150" tabindex="33" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnOK" tabindex="34">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancel" tabindex="35">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="composedetails" tabindex="-1" role="dialog" aria-hidden="true" style="margin-top: -25px;">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header" style="height: 10px; padding: 0px;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <div id="divDetailsList">
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix" style="display: none;">
                            <button type="button" class="btn btn-danger pull-left" id="btndetailsCancel" tabindex="36">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="Allcomposedetails" tabindex="-1" role="dialog" aria-hidden="true" style="margin-top: -25px;">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="width: 105%;">
                        <div class="modal-header" style="height: 10px; padding: 0px;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="Allmodal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <div id="divAllDetailsList">
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix" style="display: none;">
                            <button type="button" class="btn btn-danger pull-left" id="btnAlldetailsCancel" tabindex="36">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="composeImage" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div>
                                <img src="" id="imgUpload1" alt="" style="visibility: hidden; display: block; margin-right: auto; width: 280px; height: 280px" />
                            </div>
                            <div>
                                <img src="" id="imgUpload5" alt="" style="visibility: hidden; display: block; margin-top: -280px; margin-left: 297px; width: 280px; height: 280px" />
                            </div>
                            <div>
                                <img src="" id="imgUpload6" alt="" style="visibility: hidden; display: block; margin-right: auto; margin-left: 593px; margin-top: -280px; width: 280px; height: 280px" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnImageCancel" tabindex="37">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="composeRate" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-md-6" id="divSearchCode">
                                    <label>
                                        SMSCode</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtSearchCode" placeholder="Code"
                                        maxlength="12" tabindex="38" />
                                </div>
                                <div class="form-group col-md-6" id="divProduct">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <div id="divSelectProduct">
                                        <select id="ddlProduct" class="form-control select2" data-placeholder="Select Product Name" tabindex="39"></select>
                                    </div>
                                </div>
                                <%--  <div class="form-group col-md-4" id="divPurchasePrice">
                                    <label>
                                        Purchase Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtPurchaseRate" placeholder="Purchase Price"
                                        maxlength="12" tabindex="40" readonly="true" />
                                </div>--%>
                                <div class="form-group col-md-4" id="divWholeSalePrice">
                                    <label>
                                        WholeSale Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePrice" placeholder="WholeSale Price"
                                        maxlength="12" tabindex="41" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divRetailPrice">
                                    <label>
                                        Retail Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRetailPrice" placeholder="Retail Price"
                                        maxlength="12" tabindex="42" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divWholeSalePriceA">
                                    <label>
                                        WholeSale Price A</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePriceA" placeholder="WholeSale Price A"
                                        maxlength="12" tabindex="43" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divWholeSalePriceB">
                                    <label>
                                        WholeSale Price B</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePriceB" placeholder="WholeSale Price B"
                                        maxlength="12" tabindex="44" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divWholeSalePriceC">
                                    <label>
                                        WholeSale Price C</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePriceC" placeholder="WholeSale Price C"
                                        maxlength="12" tabindex="48" readonly="true" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnRateCancel" tabindex="46">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>

            <div class="box box-solid box-primary" id="divCustomerinfo">
                <div class="box-header with-border">
                    <div class="box-title">
                        <i class="fa fa-user"></i>&nbsp;&nbsp;Customer
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-4" id="divCustomerName">
                            <label>
                                Customer</label><span class="text-danger">*</span>
                            <div class="input-group">
                                <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                <input type="text" class="form-control" id="txtName" style="text-transform: uppercase" placeholder="Please enter Customer Name"
                                    maxlength="150" tabindex="1" autocomplete="off" />
                            </div>
                        </div>
                        <div class="form-group col-md-4" id="divPhoneNo1">
                            <label>
                                Mobile No</label><span class="text-danger">*</span>
                            <div class="input-group">
                                <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                <input type="text" class="form-control" id="txtPhoneNo1" placeholder="Phone No" maxlength="10"
                                    tabindex="3" onkeypress="return IsNumeric(event)" autocomplete="off" />
                            </div>
                        </div>
                        <div class="form-group col-md-4" id="divAlternateNo">
                            <label>
                                Alternate No</label>
                            <div class="input-group">
                                <div class="input-group-addon"><i class="fa fa-whatsapp"></i></div>
                                <input type="text" class="form-control" id="txtAlternateNo" placeholder="Please enter Alternate No"
                                    maxlength="10" tabindex="5" onkeypress="return isNumberKey(event)" autocomplete="off" />
                            </div>
                        </div>
                        <div class="form-group col-md-6" id="divCustomerAddress">
                            <label>
                                Address</label>
                            <textarea id="txtCustomerAddress" class="form-control" maxlength="255" tabindex="4" rows="4" aria-autocomplete="none"></textarea>
                        </div>
                        <div class="form-group col-md-3" id="divState">
                            <label>
                                State</label><span class="text-danger">*</span>
                            <select id="ddlState" class="form-control select2" data-placeholder="Select State" tabindex="7">
                            </select>
                        </div>
                        <div class="form-group col-md-3" id="divPincode">
                            <label>
                                Pincode</label>
                            <input type="text" class="form-control" id="txtPincode" placeholder="Pincode"
                                maxlength="6" tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-3" id="divFax">
                            <label>
                                GST No</label>
                            <input type="text" class="form-control" id="txtFax" placeholder="GST No" maxlength="20"
                                tabindex="13" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-3" id="divEmail">
                            <label>
                                Email</label>
                            <div class="input-group">
                                <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
                                <input type="text" class="form-control" id="txtEmail" placeholder="Email" maxlength="50" tabindex="14" autocomplete="off" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer clearfix">
                    <button type="button" class="btn btn-danger pull-left" id="btnCustomerClose" tabindex="63">
                        <i class="fa fa-times"></i>&nbsp;&nbsp;Close</button>
                    <button type="button" class="btn btn-info pull-right" id="btnCustomerSave" tabindex="61">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                    <button type="button" class="btn btn-info pull-right" id="btnCustomerUpdate" tabindex="62">
                        <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnCustomerID" />
    <input type="hidden" id="hdnSalesEntryID" />
    <input type="hidden" id="hdnSalesID" />
    <input type="hidden" id="hdnLastinvoiceDate" />
    <input type="hidden" id="hdnSalesEntryTransID" />
    <input type="hidden" id="hdnTaxPercent" />
    <input type="hidden" id="hdnCGSTPercent" />
    <input type="hidden" id="hdnSGSTPercent" />
    <input type="hidden" id="hdnSMSCode" />
    <input type="hidden" id="hdnIGSTPercent" />
    <input type="hidden" id="hdRS" />
    <input type="hidden" id="hdnCGSTAmount" />
    <input type="hidden" id="hdnSGSTAmount" />
    <input type="hidden" id="hdnIGSTAmount" />
    <input type="hidden" id="hdnStateCode" />
    <input type="hidden" id="hdnstateID" />
    <input type="hidden" id="hdnSalesLimit" />
    <input type="hidden" id="hdnCustomerBalanceAmount" />
    <input type="hidden" id="hdnSalesDiscountPercent" />
    <input type="hidden" id="hdnMaxsalesDiscount" />
    <input type="hidden" id="hdnCustomerTypeID" />
    <input type="hidden" id="hdnTransTaxPercent" />
    <input type="hidden" id="hdnTransCGSTPercent" />
    <input type="hidden" id="hdnTransSGSTPercent" />
    <input type="hidden" id="hdnTransIGSTPercent" />
    <input type="hidden" id="hdnTransCGSTAmount" />
    <input type="hidden" id="hdnTransSGSTAmount" />
    <input type="hidden" id="hdnTransIGSTAmount" />
    <input type="hidden" id="hdnBalanceAmt" />
    <input type="hidden" id="hdnPaidAmt" />
    <input type="hidden" id="hdnNetAmt" />
    <input type="hidden" id="hdnStatus" />
    <input type="hidden" id="hdnIsAllProduct" />
    <input type="hidden" id="hdnOriginalRate" />
    <input type="hidden" id="hdnRateChanged" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/jSalesEntry.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jSalesEntry.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmSalesEntry.aspx") %>';
        $(document).ready(function () {

            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            $("[id$=txtCode]").change(function () {
                $("[id$=txtCode]").val(($("[id$=txtCode]").val().split('|')[0].trim()));
            });
            $("[id$=txtCode]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetBarcodeCode") %>',
                        data: "{ 'prefix': '" + request.term + "','CustomerID':0,'IsAll':'A'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item.split('|')[0]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                open: function () {
                    $("[id$=txtCode]").autocomplete("widget").css({
                        "width": ("800px"), "backgroundColor": ("#d7dde2")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
            });
        });

    </script>
    <script type="text/javascript">
        document.onkeydown = function () {
            if (event.keyCode == 67 && event.altKey) {
                $('#hdnIsAllProduct').val(0);
                if ($("#ddlProductName").val() > 0) {
                    if ($("#txtCode").val() != "")
                        GetSalesProductDetails($("#ddlProductName").val(), $("#txtCode").val(), 0, $("#ddlCustomerName").val());
                    else
                        GetSalesProductDetails($("#ddlProductName").val(), $("#hdnSMSCode").val(), 0, $("#ddlCustomerName").val());
                    //$(".modal-title").html("&nbsp;&nbsp; Last 10 sales details of this Product to this Customer");

                    //$('#composedetails').modal({ show: true, backdrop: true });
                }
            }

            if (event.keyCode == 86 && event.altKey) {
                $('#hdnIsAllProduct').val(1);
                if ($("#ddlProductName").val() > 0) {
                    if ($("#txtCode").val() != "")
                        GetAllProductDetails($("#ddlProductName").val(), $("#txtCode").val(), 1, $("#ddlCustomerName").val());
                    else
                        GetAllProductDetails($("#ddlProductName").val(), $("#hdnSMSCode").val(), 1, $("#ddlCustomerName").val());
                }
            }
            if (event.keyCode == 114 && event.altKey) {

                GetRateByProduct();
                $(".modal-title").html("&nbsp;&nbsp; Search Rate");
                $('#composeRate').modal({ show: true, backdrop: true });
                $("#ddlProduct").val($("#ddlProductName").val()).change();
            }
        };



    </script>
    <script type="text/javascript" src="JS/fancybox/jquery.fancybox.js?v=2.1.4"></script>
    <link rel="stylesheet" type="text/css" href="JS/fancybox/jquery.fancybox.css?v=2.1.4" media="screen" />
    <script type="text/javascript">

        $('img.preview_img').on('load', function () {
            //console.log($(this).attr('src'));
            $(this).parent("a").attr("href", $(this).attr("src"));
        });
        function ResizeImage(img_id) {

            var filesToUpload = document.getElementById(img_id).files;
            var file = filesToUpload[0];

            // Create an image
            var img = document.createElement("img");
            // Create a file reader
            var reader = new FileReader();
            // Set the image once loaded into file reader
            reader.onload = function (e) {
                //img.src = e.target.result;
                var img = new Image();

                img.src = this.result;

                setTimeout(function () {
                    var canvas = document.createElement("canvas");

                    var MAX_WIDTH = 1500;
                    var MAX_HEIGHT = 1000;
                    var width = img.width;
                    var height = img.height;

                    if (width > height) {
                        if (width > MAX_WIDTH) {
                            height *= MAX_WIDTH / width;
                            width = MAX_WIDTH;
                        }
                    } else {
                        if (height > MAX_HEIGHT) {
                            width *= MAX_HEIGHT / height;
                            height = MAX_HEIGHT;
                        }
                    }
                    canvas.width = width;
                    canvas.height = height;
                    var ctx = canvas.getContext("2d");
                    ctx.drawImage(img, 0, 0, width, height);
                    var dataurl = canvas.toDataURL("image/jpeg");
                    var image_view = $("#" + img_id).attr("data-image-src");
                    document.getElementById(image_view).src = dataurl;
                    $("#" + image_view).css({ "visibility": "visible", "display": "block" });
                    saveimage(image_view);

                }, 100);
            }
            // Load files into file reader
            reader.readAsDataURL(file);
        }
    </script>
</asp:Content>

