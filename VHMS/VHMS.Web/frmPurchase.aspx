﻿<%@ Page Title="Purchase" Language="C#" MasterPageFile="~/VHMSMasterPage.master"
    AutoEventWireup="true" CodeFile="frmPurchase.aspx.cs" Inherits="frmPurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Purchase
                </h3>
                <div class="form-group  col-md-4" style="margin-left: 255px; margin-top: -66px;">
                    <label>
                        Supplier Name</label>
                    <select id="ddlCategoryName" class="form-control select2" data-placeholder="Select Category Name" tabindex="-1"></select>
                </div>
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
                                                <th>Purchase No</th>
                                                <th>Entry Date</th>
                                                <th>Bill No</th>
                                                <th>Bill Date</th>
                                                <th>Supplier</th>
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
                                        Search Purchase records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive" style="min-height: 10px !important">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Purchase No</th>
                                                <th>Entry Date</th>
                                                <th>Bill No</th>
                                                <th>Bill Date</th>
                                                <th>Supplier</th>
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
                    <div class="box-title">Purchase Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-2" id="divBillNo">
                            <label>Purchase No</label>
                            <input type="text" class="form-control" id="txtBillNo" placeholder="Purchase No"
                                maxlength="15" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divBillDate">
                            <label>Entry Dt</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="1" id="txtBillDate" readonly="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-2" id="divNo">
                            <label>
                                Bill No</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtNo" placeholder="Bill No"
                                maxlength="30" tabindex="2" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" id="divDate">
                            <label>Bill Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="3" id="txtDate" readonly="true" />
                            </div>
                        </div>
                        <div class="form-group col-md-4" id="divSupplier">
                            <label>Supplier</label><span class="text-danger">*</span>
                            <select id="ddlSupplierName" class="form-control select2" data-placeholder="Select Supplier Name" tabindex="4"></select>
                        </div>
                    </div>
                    <div class="row" style="display: none;">
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Tax</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divTaxName" style="display: none;">

                            <select id="ddlTaxName" class="form-control select2" data-placeholder="Select Tax Name" tabindex="5"></select>
                        </div>
                        <div class="form-group col-md-3" id="divProductType" style="display: none;">
                            <input type="radio" name="SupplierProduct" id="rdoSupplier" value="S" tabindex="-1" />Supplier Products 
                        <span style="padding-left: 30px" />
                            <input type="radio" name="SupplierProduct" id="rdoAll" checked="checked" value="A" tabindex="-1" />All Products
                        </div>

                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                <input type="checkbox" id="chkDC" tabindex="6" />&nbsp;&nbsp;&nbsp; DC
                            </label>
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Purchase OrderNo</label>
                        </div>
                        <div class="form-group col-md-2" id="divOrderNo">
                            <select id="ddlOrderNo" class="form-control select2" data-placeholder="Select Purchase OrderNo" tabindex="4"></select>
                        </div>
                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-1" id="divSno">
                                    <label>
                                        S.No</label>
                                    <input type="text" class="form-control TRSearch" id="txtSNo" placeholder="Sno"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-2" id="divCode" style="margin-left: -27px; display: none;">
                                    <label>
                                        Search Code</label>
                                    <input type="text" class="form-control TRSearch" id="txtCode" placeholder="Code" maxlength="12" tabindex="7" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divProductName">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <select id="ddlProductName" class="form-control select2" data-placeholder="Select Product Name" tabindex="8"></select>
                                </div>
                                <div class="form-group col-md-1" id="divSMSCode" style="display: none">
                                    <label>
                                        SMS Code</label>
                                    <input type="text" class="form-control TRSearch" id="txtSMSCode" placeholder="Code"
                                        maxlength="12" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divPartyCode" style="margin-left: -27px; display: none;">
                                    <label>
                                        Party Code</label>
                                    <input type="text" class="form-control TRSearch" id="txtPartyCode" placeholder="Code"
                                        maxlength="12" tabindex="-1" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divPerviousRate">
                                    <label>
                                        Pervious Rate</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtPerviousRate" placeholder="Pervious Rate"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-1" id="divQuantity">
                                    <label>
                                        Quantity</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Quantity"
                                        maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divRate">
                                    <label>
                                        Rate</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRate" placeholder="Rate"
                                        maxlength="12" tabindex="10" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divSalesMargin" style="display: none">
                                    <label>
                                        Sales Margin</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtSalesMargin" placeholder="Margin"
                                        maxlength="12" tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" readonly />
                                </div>
                                <div class="form-group col-md-1" id="divSalesRate">
                                    <label>
                                        Sales Rate</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtSalesRate" placeholder="Rate"
                                        maxlength="12" tabindex="11" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-1" id="divBatchNo" style="display: none;">
                                    <label>
                                        Batch No</label>
                                    <input type="text" class="form-control TRSearch" id="txtBatchNo" placeholder="Batch No"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divDisPer" style="display: none">
                                    <label>
                                        Disc %</label>
                                    <input type="text" class="form-control TRSearch" id="txtDisPer" placeholder="Discount %"
                                        maxlength="12" tabindex="12" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>

                                <div class="form-group col-md-1" id="divDisAmt" style="display: none">
                                    <label>
                                        Disc. Amt</label>
                                    <input type="text" class="form-control TRSearch" id="txtDisAmt" placeholder="Disc. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>

                                <div class="form-group col-md-1" id="divTaxTrans" style="display: none">
                                    <label>
                                        Tax</label><span class="text-danger">*</span>
                                    <select id="ddlTax" class="form-control select2" data-placeholder="Select Tax " disabled="disabled" tabindex="14"></select>
                                </div>
                                <div class="form-group col-md-1" id="divTaxAmt" style="display: none">
                                    <label>
                                        Tax Amt</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtTaxAmt" placeholder="Disc. Amt"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>

                                <div class="form-group col-md-1" id="divFrequency">
                                    <label>
                                        Subtotal</label>
                                    <input type="text" class="form-control TRSearch" id="txtSubTotal" placeholder="Subtotal"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>
                                <div class="form-group col-md-1" id="divSerialNo" style="display: none">
                                    <label>
                                        Serial No</label>
                                    <input type="text" class="form-control TRSearch" id="txtSerialNo" placeholder="Serial No"
                                        maxlength="12" tabindex="13" />
                                </div>
                                <div class="form-group col-md-2" id="divBarcode" style="display: none">
                                    <input type="text" class="form-control TRSearch" id="txtBarcode" placeholder="Subtotal"
                                        maxlength="12" tabindex="-1" onkeypress="return IsNumeric(event)" readonly="true" />
                                </div>

                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="15">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="16">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive" style="min-height: 200px !Important;">
                            <div id="divOPBillingList">
                            </div>
                        </div>
                    </div>
                    <div class="row"></div>
                    <div class="row" style="display: none">
                        <div class="form-group col-md-1" style="display: none;">
                            <label>
                                Upload</label>
                        </div>
                        <div class="form-group col-md-2" style="display: none;">
                            <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                runat="server" ID="imgupload1" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="214px"
                                OnUploadedComplete="UploadedComplete" OnClientUploadComplete="DocumentuploadComplete" />
                            <input type="hidden" id="hdnImgupload1" />

                        </div>
                        <div class="form-group col-md-1" style="display: none">
                            <img src="" id="imgUpload1" alt="" style="visibility: hidden;" />
                        </div>
                        <div class="form-group col-md-2" style="display: none;">
                            <input type="text" class="form-control" style="height: 15px; width: 15px; background-color: #f3c8c8;" />
                            Newly added Products
                        </div>
                        <div class="form-group col-md-2" style="display: none;">
                            <input type="text" class="form-control" style="height: 15px; width: 15px; background-color: #b0ecb8;" />
                            Rate Increased Products
                        </div>
                        <div class="form-group col-md-2" style="display: none;">
                            <input type="text" class="form-control" style="height: 15px; width: 15px; background-color: #cc76dc;" />
                            Rate Decreased Products
                        </div>
                        <div class="form-group col-md-7"></div>
                        <div class="form-group col-md-1">
                            <label>
                                Discount %</label>
                        </div>
                        <div class="form-group col-md-1" id="divDiscountPercent">

                            <input type="text" class="form-control" id="txtDiscountPercent" placeholder="Discount Percent"
                                maxlength="15" tabindex="17" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
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
                    <div class="row" style="display: none">
                        <div class="form-group col-md-7"></div>
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
                                Disc. Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divDiscountAmount">

                            <input type="text" class="form-control" id="txtDiscountAmount" placeholder="Discount Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="18" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <div class="form-group col-md-1"></div>
                        <div class="form-group col-md-2" style="display: none">
                            <input type="checkbox" id="chk" value="value" />
                            <label for="chk" style="font-size: 15px;">Calculate TCS </label>
                        </div>
                        <div class="form-group col-md-1" style="display: none">
                            <label>
                                TCS %</label>
                        </div>
                        <div class="form-group col-md-1" id="divTCSPercent" style="display: none">

                            <input type="text" class="form-control" id="txtTCSPercent" placeholder="TCS Percent"
                                maxlength="15" tabindex="19" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-1"></div>
                        <div class="form-group col-md-1" style="display: none">
                            <label>
                                TCS Amt</label>
                        </div>
                        <div class="form-group col-md-1" id="divTCSAmount" style="display: none">

                            <input type="text" class="form-control" id="txtTCSAmount" placeholder="TCS Amount"
                                maxlength="15" tabindex="-1" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                        </div>
                        <div class="form-group col-md-5"></div>
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
                                Taxable Amt</label>
                        </div>
                        <div class="form-group col-md-2" id="divTotalAmount">
                            <input type="text" class="form-control" id="txtTotalAmount" placeholder="Taxable Amount" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <div class="form-group col-md-7"></div>
                        <div class="form-group col-md-1">
                            <label>
                                IGST</label>
                        </div>
                        <div class="form-group col-md-1" id="divIGST">

                            <input type="text" class="form-control" id="txtIGST" placeholder="SGST"
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
                    <div class="row">
                        <div class="form-group col-md-7">
                        </div>
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
                                Roundoff</label>
                        </div>
                        <div class="form-group col-md-2" id="divRoundoff">

                            <input type="text" class="form-control" id="txtRoundoff" placeholder="Roundoff" style="font-size: 104%; font-weight: bold;"
                                maxlength="15" tabindex="22" value="0" autocomplete="off" readonly />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-8" id="divAddress">
                            <label>
                                Comments</label>
                            <textarea id="txtComments" class="form-control" maxlength="255" tabindex="23" rows="3" aria-autocomplete="none"></textarea>
                        </div>
                        <div class="form-group col-md-2" style="text-align: right">
                            <label style="font-size: 20px;">Net Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divNetAmount">
                            <input type="text" class="form-control" id="txtNetAmount" style="font-weight: bold; font-size: 20px;" placeholder="Net Amount"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>

                        <div class="form-group col-md-2" id="divOtherPasswordlbl" style="text-align: right">
                            <label>
                                Password</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divOtherPassword">
                            <input type="password" class="form-control" id="txtOtherPassword" placeholder="Special Password" autocomplete="off" maxlength="512"
                                tabindex="24" />
                        </div>
                    </div>
                    <div class="row" style="display: none">
                        <div class="form-group col-md-2" id="divOtherCharges">
                            <label>Other Charges</label>
                            <input type="text" class="form-control" id="txtOtherCharges" placeholder="Other Charges"
                                maxlength="15" tabindex="25" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" id="divCourierCharges">
                            <label>Courier Charges</label>
                            <input type="text" class="form-control" id="txtCourierCharges" placeholder="Courier Charges"
                                maxlength="15" tabindex="26" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" id="divPaymentDiscountPercent">
                            <label>
                                Payment Disc.%</label>
                            <input type="text" class="form-control" id="txtPaymentDiscountPercent" placeholder="Payment Discount %"
                                maxlength="15" tabindex="27" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                        <div class="form-group col-md-2" id="divDis_amt_Type">
                            <label>Calculate for</label>
                            <select id="ddlDis_amt_Typ" class="form-control select2" data-placeholder="Select Type" tabindex="28">
                                <option value="NetAmount">Net Amount</option>
                                <option value="TaxableAmount">Taxable Amount</option>
                            </select>
                        </div>
                        <div class="form-group col-md-2" id="divPaymentDiscount">
                            <label>
                                Payment Disc. Amt</label>
                            <input type="text" class="form-control" id="txtPaymentDiscount" placeholder="Payment Discount"
                                maxlength="15" tabindex="29" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row" style="display: none;">
                        <div class="form-group col-md-4">
                            <label>
                                Image 1</label>
                            <button id="btnClearImage1" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagePurchasefile" type="file" id="imagePurchasefile" data-image-src="imgUploadPurchase1_view" accept="image/*" onchange="ResizeImage('imagePurchasefile');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUploadPurchase1_view" class="preview_img" alt="" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-4">
                            <label>
                                Image 2</label>
                            <button id="btnClearImage2" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagePurchasefile" type="file" id="imagePurchasefile2" data-image-src="imgUploadPurchase2_view" accept="image/*" onchange="ResizeImage('imagePurchasefile2');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUploadPurchase2_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>
                        <div class="form-group col-md-4">
                            <label>
                                Image 3</label>
                            <button id="btnClearImage3" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagePurchasefile" type="file" id="imagePurchasefile3" data-image-src="imgUploadPurchase3_view" accept="image/*" onchange="ResizeImage('imagePurchasefile3');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUploadPurchase3_view" alt="" class="preview_img" style="width: 280px;" />
                            </a>
                        </div>

                    </div>
                    <div class="modal-footer clearfix">
                        <button id="btnSave" type="button" class="btn btn-info pull-right" tabindex="30">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                        <button id="btnUpdate" type="button" class="btn btn-info pull-right" tabindex="31">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Update Changes</button>
                        <button id="btnPurchaseBarcode" type="button" class="btn btn-info pull-left" tabindex="32">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Barcode</button>
                        <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="33">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                    </div>
                </div>
            </div>
            <%-- <div class="fullscreen-container">
                <div id="popdiv">
                    <h1>Rate has been changed
                    </h1>
                    <button id="btnCloseDialog">Close</button>
                </div>
            </div>--%>
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
                                <img src="" id="imgUpload5" alt="" style="visibility: hidden; display: block; margin-right: auto; width: 280px; height: 280px" />
                            </div>
                            <div>
                                <img src="" id="imgUpload6" alt="" style="visibility: hidden; display: block; margin-top: -280px; margin-left: 297px; width: 280px; height: 280px" />
                            </div>
                            <div>
                                <img src="" id="imgUpload7" alt="" style="visibility: hidden; display: block; margin-right: auto; margin-left: 593px; margin-top: -280px; width: 280px; height: 280px" />
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
            <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group" id="divReason" style="display: none;">
                                <label>
                                    Reason</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtReason" placeholder="Please enter Reason"
                                    maxlength="150" tabindex="28" />
                            </div>
                            <div class="form-group" id="divPassword">
                                <label>
                                    Password</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtPassword" placeholder="Please enter Password"
                                    maxlength="150" tabindex="29" autocomplete="off" />
                            </div>
                            <div class="form-group" id="divID">
                                <label>
                                    ID</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtID"
                                    maxlength="150" tabindex="30" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnOK" tabindex="31">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancel" tabindex="32">
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
            <div class="modal fade" id="composedialog" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title" style="text-align: center"></h4>
                        </div>
                        <div class="modal-footer clearfix" style="text-align: center">
                            <button type="button" class="btn btn-warning" id="btnCloseDialog" tabindex="33">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                OK</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnPurchaseID" />
    <input type="hidden" id="hdnPurchaseTransID" />
    <input type="hidden" id="hdnTaxPercent" />
    <input type="hidden" id="hdnCGSTPercent" />
    <input type="hidden" id="hdnSGSTPercent" />
    <input type="hidden" id="hdnIGSTPercent" />
    <input type="hidden" id="hdnCGSTAmount" />
    <input type="hidden" id="hdnSGSTAmount" />
    <input type="hidden" id="hdnIGSTAmount" />
    <input type="hidden" id="hdRS" />
    <input type="hidden" id="hdnStateCode" />
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
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnDays" />
    <input type="hidden" id="hdnOPOrderID" />
    <input type="hidden" id="hdnSMSCode" />
    <input type="hidden" id="hdnIsAllProduct" />
    <script src="UserDefined_Js/jPurchase.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jPurchase.js") %>"
        type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmPurchase.aspx") %>';

        $(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
            $(this).closest(".select2-container").siblings('select:enabled').select2('open');
        });

        // steal focus during close - only capture once and stop propogation
        $('select.select2').on('select2:closing', function (e) {
            $(e.target).data("select2").$selection.one('focus focusin', function (e) {
                e.stopPropagation();
            });
        });

        $(document).ready(function () {


            $("[id$=txtCode]").change(function () {
                $("[id$=txtCode]").val(($("[id$=txtCode]").val().split('|')[0]).trim());
            });
            $("[id$=txtCode]").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetSMSCode") %>',
                        data: "{ 'prefix': '" + request.term + "','SupplierID':" + $("#ddlSupplierName").val() + ",'IsAll':'" + $('input[name="SupplierProduct"]:checked').val() + "'}",
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
                        "width": ("600px"), "backgroundColor": ("#d7dde2")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
            });

        });

        function DocumentuploadComplete(sender, args) {
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetProofPath",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $("#hdnImgupload1").val(r.d);
                    $get("imgUpload1").src = "./images/Documents/Purchase/" + r.d;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }



    </script>

    <script type="text/javascript" src="JS/fancybox/jquery.fancybox.js?v=2.1.4"></script>
    <link rel="stylesheet" type="text/css" href="JS/fancybox/jquery.fancybox.css?v=2.1.4" media="screen" />

    <script type="text/javascript">
        document.onkeydown = function () {
            if (event.keyCode == 113) {
                var myWindow = window.open("frmDPurchase.aspx", "_self");
            }

            //if (event.keyCode == 67 && event.altKey) {
            //    $('#hdnIsAllProduct').val(0);
            //    if ($("#ddlProductName").val() > 0) {
            //        if ($("#txtCode").val() != "")
            //            GetSalesProductDetails($("#ddlProductName").val(), $("#txtCode").val(), 0, $("#ddlSupplierName").val());
            //        else
            //            GetSalesProductDetails($("#ddlProductName").val(), $("#hdnSMSCode").val(), 0, $("#ddlSupplierName").val());
            //    }
            //}

            //if (event.keyCode == 86 && event.altKey) {
            //    $('#hdnIsAllProduct').val(1);
            //    if ($("#ddlProductName").val() > 0) {
            //        if ($("#txtCode").val() != "")
            //            GetAllProductDetails($("#ddlProductName").val(), $("#txtCode").val(), 1, $("#ddlSupplierName").val());
            //        else
            //            GetAllProductDetails($("#ddlProductName").val(), $("#hdnSMSCode").val(), 1, $("#ddlSupplierName").val());
            //    }
            //}

        };

    </script>
    <%--  <style>
        .fullscreen-container {
            display: none;
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(90, 90, 90, 0.5);
            z-index: 9999;
        }

        #popdiv {
            height: 300px;
            width: 420px;
            background-color: #97ceaa;
            position: center;
            top: 50px;
            left: 50px;
        }

        
    </style>--%>
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
