﻿var gMagazineData = [];
var gOPBillingList = [];
var RecordAvailable = 0;

$(function () {
    pLoadingSetup(false);

    ActionAdd = _CMActionAdd;
    ActionUpdate = _CMActionUpdate;
    ActionDelete = _CMActionDelete;
    ActionView = _CMActionView;
    $("#btnAddNew").show();
    $("#btnList").hide();
    $("#divID").hide();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#SearchResult").hide();
    $("#divTab").show();
    $("#divOPBilling").hide();
    GetProductList("ddlProductName");
    GetCustomerList("ddlCustomerName");
    GetTaxList("ddlTaxName");
    GetProductList("ddlProduct");
    GetTaxList("ddlTax");
    GetPassword();
    $("#ddlTaxName").change();
    $("#txtBillDate,#txtLRDate").attr("data-link-format", "dd/MM/yyyy");
    $("#txtBillDate,#txtLRDate").datetimepicker({
        pickTime: false,
        useCurrent: true,
        maxDate: moment(),
        format: 'DD/MM/YYYY'
    });

    var _Tfunctionality;
    if ($.cookie("OPBilling") != undefined) {
        _Tfunctionality = $.cookie("OPBilling");

        if (_Tfunctionality == "AddNewOPBilling") {
            pLoadingSetup(true);
            $("#btnAddNew").click();

            GetReceivedSalesEntry(parseInt($.cookie("SalesEntryID")));
            $("#hdnSalesEntryID").val(parseInt($.cookie("SalesEntryID")));
        }
        $.cookie("OPBilling", null);
        $.cookie("SalesEntryID", null);
    }
    pLoadingSetup(true);
    GetSettings();
    GetRecord();
});

function saveimage(id) {
    pLoadingSetup(false);
    var images = $("#" + id + "").attr('src');
    var ImageSave = images.replace("data:image/jpeg;base64,", "");
    var submitval = JSON.stringify({ data: ImageSave });
    $.ajax({
        type: "POST",
        url: pageUrl + "/saveimage",
        data: submitval,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $get(id).src = "./" + r.d;
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    pLoadingSetup(true);
}

$("#btnLink").click(function () {
    var myWindow = window.open("https://docs.ewaybillgst.gov.in/", "MsgWindow");
});

function GetPassword(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetUserPassword",
        data: JSON.stringify({ ID: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {

                            $("#hdRS").val(obj.ConfirmPassword);

                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function GetInvoiceNo() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetLastInvoiceNo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("#txtBillNo").val(obj.InvoiceNo);

                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

//function CalculateCustomerTotalValue() {
//    var iTaxPercent = parseFloat($("#txtCustomerToatlAmount").val()) + parseFloat($("#txtTotalTaxAmount").val());
//    if (isNaN(iTaxPercent)) iTaxPercent = 0;
//    $("#txtCustomerToatlAmountall").val(parseFloat(iTaxPercent).toFixed(2));
//    if (iTaxPercent >= "5000000")
//        $("#chk").prop("checked", true).change();
//    else
//        $("#chk").prop("checked", false).change();
//}


function GetSettings() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSettings",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("#hdnMaxsalesDiscount").val(obj.MaxSalesDiscount);
                            $("#hdnOpeningDate").val(obj.sOpeningDate);
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}
function Edit_OPBillingDetail(ID) {
    Bind_OPBillingByID(ID, gOPBillingList);
    return false;
}

$("#btnAddNew").click(function () {
    $("#secHeader").addClass('hidden');
    $("#btnAddNew").hide();
    $("#btnList").show();
    $("#hdnSalesEntryID").val("0");
    $("#hdnSalesID").val("0");
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#txtSNo").val("1");
    $("#divTab").hide();
    $("#divOPBilling").show();

    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#btnPrintbill").hide();
    $("#ddlTaxName").change();
    $("#ddlTax").change();
    $("#ddlTransport").val(null).change();
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#divOPBillingList").empty();
    $("#ddlTaxName").val(2).change();
    $("#ddlTax").val(2).change();
    $("#divOtherPasswordlbl").hide();
    $("#divOtherPassword").hide();
    GetInvoiceNo();
    $("#txtBillDate").focus();
    $("#txtBillNo").focus();
    $("#imagefile2").val("");
    $("#imagefile3").val("");
    $("#imagefile4").val("");
    return false;
});

$("#btnClearImage1").click(function () {
    $get("imgUpload2_view").src = "";
    $("#imagefile2").val("");
});

$("#btnClearImage2").click(function () {
    $get("imgUpload3_view").src = "";
    $("#imagefile3").val("");
});

$("#btnClearImage3").click(function () {
    $get("imgUpload4_view").src = "";
    $("#imagefile4").val("");
});
$("#btndetailsCancel").click(function () {
    $('#composedetails').modal('hide');
});

$("#chk").change(function () {
    if ($(this).is(":checked"))
        $("#txtTCSPercent").val(0.075);
    else
        $("#txtTCSPercent").val(0);

    var iTCS = parseFloat($("#txtTCSPercent").val());
    if (isNaN(iTCS)) iTCS = 0;
    $("#txtTCSAmount").val((parseFloat($("#txtTotalAmount").val()) * iTCS / 100).toFixed(2));
    CalculateAmount();
});

function GetProductDetails(id, SID, SalesID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetNewProductDetails",
        data: JSON.stringify({ ID: id, code: smscode, type: value, SupplierID: SID, SalesEntryID: SalesID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            var sTable = "";
                            var sCount = 1;
                            var sColorCode = "bg-info";

                            if (obj.length >= 5) { $("#divDetailsList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
                            else { $("#divDetailsList").css({ 'height': '', 'min-height': '' }); }

                            if (obj.length > 0) {
                                sTable = "<table id='tblDetailsList' class='table no-margin table-condensed table-hover'>";
                                sTable += "<thead><tr><th style='line-height:0.5;' class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Customer</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Rate</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Date</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Invoice No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Quantity</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Disc. Amt</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Subtotal</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Barcode</th>";
                                sTable += "</tr></thead><tbody id='tblDetailsList_body'>";
                                sTable += "</tbody></table>";
                                $("#divDetailsList").html(sTable);
                                for (var i = 0; i < obj.length; i++) {
                                    sTable = "<tr><td style='line-height:0.5;' id='" + obj[i].PK_SalesEntryTransID + "'>" + sCount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].CustomerName + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Rate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].sInvoiceDate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].InvoiceNo + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Quantity + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].DiscountAmount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].SubTotal + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Barcode + "</td>";
                                    sTable += "</tr>";
                                    sCount = sCount + 1;
                                    $("#tblDetailsList_body").append(sTable);
                                    //// if ($('#hdnIsAllProduct').val() == 1)
                                    //// $(".modal-title").html("&nbsp;&nbsp; All Customers - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                    //// else
                                    //if ($('#hdnIsAllProduct').val() == 0) {
                                    //    $(".modal-title").html("&nbsp;&nbsp; This Customers - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                    //    $("#Allcomposedetails").modal('hide');
                                    //    $('#composedetails').modal({ show: true, backdrop: true });

                                    //}
                                }
                                RecordAvailable = obj.length;

                            }
                            else { $("#divDetailsList").empty(); }

                            return false;
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {

                        RecordAvailable = 0;
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    dProgress(false);
    return false;

}

function GetSalesProductDetails(id, smscode, value, SID, SalesID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetNewProductDetails",
        data: JSON.stringify({ ID: id, code: smscode, type: value, SupplierID: SID, SalesEntryID: SalesID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            var sTable = "";
                            var sCount = 1;
                            var sColorCode = "bg-info";

                            if (obj.length >= 5) { $("#divDetailsList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
                            else { $("#divDetailsList").css({ 'height': '', 'min-height': '' }); }

                            if (obj.length > 0) {
                                sTable = "<table id='tblDetailsList' class='table no-margin table-condensed table-hover'>";
                                sTable += "<thead><tr><th style='line-height:0.5;' class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Customer</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Rate</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Date</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Invoice No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Quantity</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Disc. Amt</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Subtotal</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Barcode</th>";
                                sTable += "</tr></thead><tbody id='tblDetailsList_body'>";
                                sTable += "</tbody></table>";
                                $("#divDetailsList").html(sTable);
                                for (var i = 0; i < obj.length; i++) {
                                    sTable = "<tr><td style='line-height:0.5;' id='" + obj[i].PK_SalesEntryTransID + "'>" + sCount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].CustomerName + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Rate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].sInvoiceDate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].InvoiceNo + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Quantity + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].DiscountAmount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].SubTotal + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Barcode + "</td>";
                                    sTable += "</tr>";
                                    sCount = sCount + 1;
                                    $("#tblDetailsList_body").append(sTable);
                                    // if ($('#hdnIsAllProduct').val() == 1)
                                    // $(".modal-title").html("&nbsp;&nbsp; All Customers - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                    // else
                                    if ($('#hdnIsAllProduct').val() == 0) {
                                        $(".modal-title").html("&nbsp;&nbsp; This Customers - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                        $("#Allcomposedetails").modal('hide');
                                        $('#composedetails').modal({ show: true, backdrop: true });

                                    }
                                }
                                RecordAvailable = obj.length;

                            }
                            else { $("#divDetailsList").empty(); }

                            return false;
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {

                        RecordAvailable = 0;
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    dProgress(false);
    return false;

}


function GetAllProductDetails(id, smscode, value, SID, SalesID) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetNewProductDetails",
        data: JSON.stringify({ ID: id, code: smscode, type: value, SupplierID: SID, SalesEntryID: SalesID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {

                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            var sTable = "";
                            var sCount = 1;
                            var sColorCode = "bg-info";

                            if (obj.length >= 5) { $("#divAllDetailsList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
                            else { $("#divAllDetailsList").css({ 'height': '', 'min-height': '' }); }

                            if (obj.length > 0) {
                                sTable = "<table id='tblAllDetailsList' class='table no-margin table-condensed table-hover'>";
                                sTable += "<thead><tr><th style='line-height:0.5;' class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Customer</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Rate</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Date</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Invoice No</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Quantity</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Disc.Amt</th>";
                                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Subtotal</th>";
                                sTable += "</tr></thead><tbody id='tblAllDetailsList_body'>";
                                sTable += "</tbody></table>";
                                $("#divAllDetailsList").html(sTable);
                                for (var i = 0; i < obj.length; i++) {
                                    sTable = "<tr><td style='line-height:0.5;' id='" + obj[i].PK_SalesEntryTransID + "'>" + sCount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].CustomerName + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Rate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].sInvoiceDate + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].InvoiceNo + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].Quantity + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].DiscountAmount + "</td>";
                                    sTable += "<td style='line-height:0.5;'>" + obj[i].SubTotal + "</td>";
                                    sTable += "</tr>";
                                    sCount = sCount + 1;
                                    $("#tblAllDetailsList_body").append(sTable);
                                    if ($('#hdnIsAllProduct').val() == 1) {
                                        $(".Allmodal-title").html("&nbsp;&nbsp; All Customers - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                        // else
                                        //    $(".modal-title").html("&nbsp;&nbsp; This Customers - " + obj[i].Product.ProductName + " | " + obj[i].Product.SMSCode + " | " + obj[i].Product.ProductCode);
                                        $("#composedetails").modal('hide');
                                        $("#Allcomposedetails").modal({ show: true, backdrop: true });
                                    }
                                }
                                RecordAvailable = obj.length;

                            }
                            else { $("#divAllDetailsList").empty(); }

                            return false;
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {

                        RecordAvailable = 0;
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    dProgress(false);
    return false;

}

$("#btnList").click(function () {
    $("#btnAddNew").show();
    $("#btnList").hide();

    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();

    $("#divTab").show();
    $("#divOPBilling").hide();

    GetRecord();
    return false;
});

$("#btnClose").click(function () {
    $("#secHeader").removeClass('hidden');
    $("#hdnSalesEntryID").val("0");
    $("#hdnSalesID").val("0");
    gOPBillingList = [];
    ClearOPBillingTab();
    $("#btnList").click();
    return false;
});

function ClearOPBillingTab() {
    ClearOPBillingFields();
    $("#txtBillNo").val("");
    $("#txtBillDate").val("");
    $("#txtLRDate").val("");
    $("#txtVehicleNo").val("");
    $("#txtEWayNo").val("");
    $("#txtTransportGSTNo").val("");
    $("#txtNoofBages").val("");
    $("#txtComments").val("");
    $("#hdnRateChanged").val("0").change();
    $("#ddlProductName").val(null).change();
    $("#ddlTransport").val(0).change();
    $("#ddlCustomerName").val(null).change();
    $("#ddlPaymentMode").val("Credit").change();
    $("#ddlTax").val($("#ddlTax option:first").val());
    $("#txtTotalQuantity").val("0");
    $("#divBank").hide();
    $("#txtName").val("");
    $("#txtNarration").val("");
    $("#txtOPDNo").val("");
    gOPBillingList = [];
    $("#btnSave").show();
    $("#txtTCSPercent").val("0");
    $("#chk").prop("checked", false);
    $("#txtTCSAmount").val("0");
    $("#btnUpdate").hide();
    GetProductList("ddlProductName");
    $get("imgUpload2_view").src = "";
    $get("imgUpload3_view").src = "";
    $get("imgUpload4_view").src = "";
    $("[id*=imgUpload2_view]").css("visibility", "hidden");
    $("[id*=imgUpload3_view]").css("visibility", "hidden");
    $("[id*=imgUpload4_view]").css("visibility", "hidden");
    $("#ddlProductName").val(null).change();
    $("#txtBillNo").attr("disabled", false);
    return false;
}

$("#ddlPaymentMode").change(function () {
    if ($("#ddlPaymentMode").val() == "Card")
        $("#divBank").show();
    else
        $("#divBank").hide();
});

$("#txtCode").blur(function () {
    var ID = $("#txtCode").val();
    if ($("#txtCode").val().length > 2) {
        GetProductByCodeList("ddlProductName");
        if ($("#ddlProductName").val() > 0) {
            GetRate();
            // GetProductTax();
        }
    }
    else if ($("#txtCode").val().length == 0) {
        GetProductList("ddlProductName");
        ClearOPBillingFields();
        if ($("#ddlProductName").val() > 0) {
            GetRate();
            GetProductTax();
        }
    }
});

$("#txtSearchCode").change(function () {
    if ($("#txtSearchCode").val().length > 2) {
        GetProductByCode("ddlProduct");
        if ($("#ddlProduct").val() > 0) {
            GetRateByProduct();
            GetProductTax();
        }
    }
    else if ($("#txtSearchCode").val().length == 0) {
        GetProductList("ddlProduct");
        if ($("#ddlProduct").val() > 0) {
            GetRateByProduct();
            GetProductTax();
        }
    }
});

$("#ddlProduct").change(function () {
    if ($("#ddlProduct").val() > 0) {
        GetRateByProduct();
        GetProductTax();
    }
});

function GetTransPortGStNo() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTransportByID",
        data: JSON.stringify({ ID: $("#ddlTransport").val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("#txtTransportGSTNo").val(obj.GSTNo);
                            $("#txtVehicleNo").val(obj.VehicleNo);
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function GetCustomerByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetCustomerByID",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("#hdnStateCode").val(obj.State.StateCode);
                            $("#hdnstateID").val(obj.State.StateID);
                            $("#txtMobileNo").val(obj.MobileNo);
                            $("#txtAddress").val(obj.Address);
                            var GSTNo = obj.GSTNo.toUpperCase();
                            $("#txtGSTNo").val(GSTNo);
                            $("#hdnCustomerBalanceAmount").val(obj.Balance_amount);
                            for (var i = 0; i < gOPBillingList.length; i++) {
                                if (gOPBillingList[i].StatusFlag != "D") {
                                    iSubtotal = gOPBillingList[i].SubTotal;
                                    iTaxID = gOPBillingList[i].Tax.TaxID;
                                    GetTaxByID(iTaxID);

                                    gOPBillingList[i].Tax.TaxPercentage = parseFloat($("#hdnTaxPercent").val());
                                    if ($("#hdnStateCode").val() == 33) {
                                        gOPBillingList[i].Tax.CGSTPercent = parseFloat($("#hdnCGSTPercent").val());
                                        gOPBillingList[i].Tax.SGSTPercent = parseFloat($("#hdnSGSTPercent").val());
                                        gOPBillingList[i].Tax.IGSTPercent = 0;
                                        gOPBillingList[i].CGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
                                        gOPBillingList[i].SGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
                                        gOPBillingList[i].IGSTAmount = 0;
                                    }
                                    else {
                                        gOPBillingList[i].Tax.CGSTPercent = 0;
                                        gOPBillingList[i].Tax.SGSTPercent = 0;
                                        gOPBillingList[i].Tax.IGSTPercent = parseFloat($("#hdnIGSTPercent").val());
                                        gOPBillingList[i].CGSTAmount = 0;
                                        gOPBillingList[i].SGSTAmount = 0;
                                        gOPBillingList[i].IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
                                    }
                                    gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
                                }
                                CalculateAmount();
                            }

                            //var ObjProduct = obj.ShippingAddress;
                            //$("#ddlShippingAddress").empty();
                            //for (var index = 0; index < ObjProduct.length; index++) {

                            //    $("#ddlShippingAddress").append("<option value='" + ObjProduct[index].ShippingAddressID + "'>" + ObjProduct[index].BranchName + "</option>");
                            //}

                            //$("#ddlShippingAddress").val($("#ddlShippingAddress option:first").val()).change();
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}
function TaxCalculate() {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iSubtotal = gOPBillingList[i].SubTotal;
            iTaxID = gOPBillingList[i].Tax.TaxID;
            GetTaxByID(iTaxID);

            gOPBillingList[i].Tax.TaxPercentage = parseFloat($("#hdnTaxPercent").val());
            if ($("#hdnStateCode").val() == 33) {
                gOPBillingList[i].Tax.CGSTPercent = parseFloat($("#hdnCGSTPercent").val());
                gOPBillingList[i].Tax.SGSTPercent = parseFloat($("#hdnSGSTPercent").val());
                gOPBillingList[i].Tax.IGSTPercent = 0;
                gOPBillingList[i].CGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
                gOPBillingList[i].SGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
                gOPBillingList[i].IGSTAmount = 0;
            }
            else {
                gOPBillingList[i].Tax.CGSTPercent = 0;
                gOPBillingList[i].Tax.SGSTPercent = 0;
                gOPBillingList[i].Tax.IGSTPercent = parseFloat($("#hdnIGSTPercent").val());
                gOPBillingList[i].CGSTAmount = 0;
                gOPBillingList[i].SGSTAmount = 0;
                gOPBillingList[i].IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
            }
            gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
        }
        CalculateAmount();
    }
}
function GetRateByProduct() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetRateByProduct",
        data: JSON.stringify({ ID: $("#ddlProduct").val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("#txtWholeSalePrice").val(obj.Rate);
                            $("#txtRetailPrice").val(obj.RetailRate);
                            $("#txtPurchasePrice").val(obj.PurchaseRate);
                            $("#txtWholeSalePriceA").val(obj.WholeSalePriceA);
                            $("#txtWholeSalePriceB").val(obj.WholeSalePriceB);
                            $("#txtWholeSalePriceC").val(obj.WholeSalePriceC);
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function GetProductByCode(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductByCode",
        data: JSON.stringify({ ProductCode: $("#txtSearchCode").val().trim(), SMSOnly: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                            }
                            //console.log($("#ddlProductName").val());
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        dProgress(false);
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    dProgress(false);
                }
            }
            else {
                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

$("#ddlProductName").change(function () {
    if ($("#ddlProductName").val() > 0) {
       // GetProductTax();
        GetRate();
    }
});

function GetRate() {
    if ($("#ddlProductName").val() > 0 & $("#ddlCustomerName").val() > 0) {
        dProgress(true);
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetStockPreviousRateByID",
            data: JSON.stringify({ iProductID: $("#ddlProductName").val(), iCustomerID: $("#ddlCustomerName").val() }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d != "") {
                    var objResponse = jQuery.parseJSON(data.d);
                    if (objResponse.Status == "Success") {
                        if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                            var obj = jQuery.parseJSON(objResponse.Value);
                            if (obj != null) {
                                $("#txtAvailableQty").val(obj.Quantity);
                                $("#txtRate").val(obj.SellingPrice);
                                $("#txtPreviousPrice").val(obj.PreviousPrice);
                                $("#txtPurchasePrice").val(obj.PurchaseRate);
                                $("#txtSalesMargin").val(obj.SalesMargin);
                            }
                            dProgress(false);
                        }
                        else if (objResponse.Value == "NoRecord") {
                            dProgress(false);
                        }
                        else if (objResponse.Value == "Error") {
                            $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                        }
                    }
                    else if (objResponse.Status == "Error") {
                        if (objResponse.Value == "0") {
                            window.location("frmLogin.aspx");
                        }
                        else if (objResponse.Value == "Error") {
                            window.location = "frmErrorPage.aspx";
                        }
                        else if (objResponse.Value == "NoRecord") {
                        }
                    }
                }
                else {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            },
            error: function () {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        });
        return false;
    }
}
function GetBatchNo(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetStockByBatchNo",
        data: JSON.stringify({ ID: $("#ddlProductName").val()}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            $(sControlName).append('<option value="' + '0' + '">' + '--All--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                    $(sControlName).append("<option value='" + obj[index].StockID + "'>" + obj[index].BatchNo + "</option>");
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        dProgress(false);
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    dProgress(false);
                }
            }
            else {
                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}


$("#ddlCustomerName").change(function () {
    if ($("#ddlCustomerName").val() > 0) {
        GetCustomerByID($("#ddlCustomerName").val());
        GetRate();
       // GetProductTax();
        //  GetWholeSalesTotalAmount();
    }
});

$("#ddlTransport").change(function () {
    if ($("#ddlTransport").val() > 0) {
        GetTransPortGStNo();
    }
});




function GetTaxList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTax",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].TaxID + "'>" + obj[index].TaxName + "</option>");
                            }
                            $("#ddlTax").val($("#ddlTax option:first").val());
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        dProgress(false);
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    dProgress(false);
                }
            }
            else {
                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function GetBankList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetLedgerBank",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].LedgerID + "'>" + obj[index].LedgerName + "</option>");
                            }
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        dProgress(false);
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    dProgress(false);
                }
            }
            else {
                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}
function GetProductTax() {
    if ($("#ddlProductName").val() > 0) {
        dProgress(true);

        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetProductByID",
            data: JSON.stringify({ ID: $("#ddlProductName").val() }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d != "") {
                    var objResponse = jQuery.parseJSON(data.d);
                    if (objResponse.Status == "Success") {
                        if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                            var obj = jQuery.parseJSON(objResponse.Value);
                            if (obj != null) {

                                $("#ddlTax").val($("#ddlTaxName").val()).change();

                            }
                            dProgress(false);
                        }
                        else if (objResponse.Value == "NoRecord") {
                            $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                            dProgress(false);
                        }
                        else if (objResponse.Value == "Error") {
                            $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                        }
                    }
                    else if (objResponse.Status == "Error") {
                        if (objResponse.Value == "0") {
                            window.location("frmLogin.aspx");
                        }
                        else if (objResponse.Value == "Error") {
                            window.location = "frmErrorPage.aspx";
                        }
                        else if (objResponse.Value == "NoRecord") {
                            $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        }
                    }
                }
                else {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            },
            error: function () {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        });
        return false;
    }
}
function GetCustomerList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTopCustomer",
        data: JSON.stringify({ CustomerID:0}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                           $(sControlName).append('<option value="' + '0' + '">' + '--All--' + '</option>');
                            for (var index = 0; index < obj.length; index++) {
                                $(sControlName).append('<option value=' + obj[index].CustomerID + ' >' + obj[index].CustomerName + '</option>');
                            }
                        }
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                }
            }
            else {
                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}

function GetProductList(ddlname) {
    var sControlName = "#" + ddlname;
    dProgress(true);
    $(sControlName).empty();
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive)
                                    $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                            }
                            $("#ddlProductName").val($("#ddlProductName option:first").val()).change();
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        dProgress(false);
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    dProgress(false);
                }
            }
            else {
                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function GetProductByCodeList(ddlname) {
    if ($("#txtCode").val().length > 3) {
        var sControlName = "#" + ddlname;
        dProgress(true);
        $(sControlName).empty();
        $.ajax({
            type: "POST",
            url: "WebServices/VHMSService.svc/GetProductByCode",
            data: JSON.stringify({ ProductCode: $("#txtCode").val().trim(), SMSOnly: 0 }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d != "") {
                    var objResponse = jQuery.parseJSON(data.d);
                    if (objResponse.Status == "Success") {
                        if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                            var obj = $.parseJSON(objResponse.Value);
                            if (obj.length > 0) {
                                for (var index = 0; index < obj.length; index++) {
                                    if (obj[index].IsActive)
                                        $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                                }
                                $("#ddlProductName").val($("#ddlProductName option:first").val()).change();
                            }
                            dProgress(false);
                        }
                        else if (objResponse.Value == "NoRecord") {
                            $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                            dProgress(false);
                        }
                    }
                    else if (objResponse.Status == "Error") {
                        if (objResponse.Value == "0") {
                            $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                            window.location = "frmLogin.aspx";
                        }
                        else if (objResponse.Value == "Error") {
                            window.location = "frmErrorPage.aspx";
                        }
                        dProgress(false);
                    }
                }
                else {
                    $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    dProgress(false);
                }
            },
            error: function (e) {
                $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        });
        return false;
    }
}

Array.prototype.max = function () {
    var max = this.length > 0 ? this[0]["sNO"] : 0;
    var len = this.length;
    for (var i = 1; i < len; i++) if (this[i]["sNO"] > max) max = this[i]["sNO"];
    return max;
}

$("#txtRate,#txtQuantity, #txtDisAmt, #txtDisPer").change(function () {
    CalculateAmountTrans();
});
function CalculateTrans() {
    var iTax = parseFloat($("#hdnTransTaxPercent").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    var iCGST = parseFloat($("#hdnTransCGSTPercent").val());
    var iSGST = parseFloat($("#hdnTransSGSTPercent").val());
    var iIGST = parseFloat($("#hdnTransIGSTPercent").val());
    var iDisAmt = parseFloat($("#txtDisAmt").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDisAmt)) iDisAmt = 0;
    if (isNaN(iTax)) iTax = 0;
    var iSubTotal = (parseFloat(iRate) * parseFloat(iqty)) - iDisAmt;

    if ($("#hdnStateCode").val() == 33) {
        var iCGSTPercent = parseFloat(iSubTotal) * parseFloat(iCGST) / 100;
        var iSGSTPercent = parseFloat(iSubTotal) * parseFloat(iSGST) / 100;
        $("#hdnTransSGSTAmount").val(parseFloat(iSGSTPercent).toFixed(2));
        $("#hdnTransCGSTAmount").val(parseFloat(iCGSTPercent).toFixed(2));
        $("#hdnTransIGSTAmount").val(0)
    }
    else {
        var iIGSTPercent = parseFloat(iSubTotal) * parseFloat(iIGST) / 100;
        $("#hdnTransSGSTAmount").val(0);
        $("#hdnTransCGSTAmount").val(0);
        $("#hdnTransIGSTAmount").val(parseFloat(iIGSTPercent).toFixed(2))
    }
    var iTaxPercent = parseFloat($("#hdnTransCGSTAmount").val()) + parseFloat($("#hdnTransSGSTAmount").val()) + parseFloat($("#hdnTransIGSTAmount").val());
    $("#txtTaxAmt").val(parseFloat(iTaxPercent).toFixed(2));
    $("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
}

$("#txtDisAmt").change(function () {
    var iDisAmt = parseFloat($("#txtDisAmt").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDisAmt)) iDisAmt = 0;
    var iSubTotal = (parseFloat(iDisAmt) * 100) / (parseFloat(iRate) * parseFloat(iqty));
    $("#txtDisPer").val(parseFloat(iSubTotal).toFixed(2));
    CalculateSubtotal();
});
$("#txtTenderAmount").change(function () {
    CalculateBalance();
});
$("#txtNetAmount").change(function () {
    CalculateBalance();
});

function CalculateBalance() {
    var iNet = parseFloat($("#txtNetAmount").val());
    if (isNaN(iNet)) iNet = 0;
    var iTender = parseFloat($("#txtTenderAmount").val());
    if (isNaN(iTender)) iTender = 0;
    if (iTender > 0)
        $("#txtBalanceGiven").val((parseFloat(iNet) - parseFloat(iTender)).toFixed(2));
    else
        $("#txtBalanceGiven").val("0");
}

$("#txtDisPer,#txtRate,#txtQuantity").change(function () {
    var iDisAmt = parseFloat($("#txtDisPer").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDisAmt)) iDisAmt = 0;
    var iSubTotal = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iDisAmt) / 100;
    $("#txtDisAmt").val(parseFloat(iSubTotal).toFixed(2));
    CalculateSubtotal();
});

function CalculateSubtotal() {

    var iTax = parseFloat($("#hdnTransTaxPercent").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    var iCGST = parseFloat($("#hdnTransCGSTPercent").val());
    var iSGST = parseFloat($("#hdnTransSGSTPercent").val());
    var iIGST = parseFloat($("#hdnTransIGSTPercent").val());
    var iDisAmt = parseFloat($("#txtDisAmt").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iDisAmt)) iDisAmt = 0;
    if (isNaN(iTax)) iTax = 0;
    var iSubTotal = (parseFloat(iRate) * parseFloat(iqty)) - iDisAmt;

    if ($("#hdnStateCode").val() == 33) {
        var iCGSTPercent = parseFloat(iSubTotal) * parseFloat(iCGST) / 100;
        var iSGSTPercent = parseFloat(iSubTotal) * parseFloat(iSGST) / 100;
        $("#hdnTransSGSTAmount").val(parseFloat(iSGSTPercent).toFixed(2));
        $("#hdnTransCGSTAmount").val(parseFloat(iCGSTPercent).toFixed(2));
        $("#hdnTransIGSTAmount").val(0)
    }
    else {
        var iIGSTPercent = parseFloat(iSubTotal) * parseFloat(iIGST) / 100;
        $("#hdnTransSGSTAmount").val(0);
        $("#hdnTransCGSTAmount").val(0);
        $("#hdnTransIGSTAmount").val(parseFloat(iIGSTPercent).toFixed(2))
    }
    var iTaxPercent = parseFloat($("#hdnTransCGSTAmount").val()) + parseFloat($("#hdnTransSGSTAmount").val()) + parseFloat($("#hdnTransIGSTAmount").val());
    $("#txtTaxAmt").val(parseFloat(iTaxPercent).toFixed(2));
    $("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
}

//$("#hdnRateChanged").change(function () {
//    if ($("#hdnRateChanged").val() == "1") {
//        $("#divOtherPasswordlbl").show();
//        $("#divOtherPassword").show();
//    }
//    else {
//        $("#divOtherPasswordlbl").hide();
//        $("#divOtherPassword").hide();
//    }
//});
$("#btnAddMagazine,#btnUpdateMagazine").click(function () {
    TaxCalculate();

    if ($("#ddlCustomerName").val() == "0" || $("#ddlCustomerName").val() == undefined || $("#ddlCustomerName").val() == null) {
        $.jGrowl("Please select Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCustomer").addClass('has-error'); $("#ddlCustomerName").focus(); return false;
    }
    else { $("#divCustomer").removeClass('has-error'); }

    if ($("#ddlProductName").val() == "0" || $("#ddlProductName").val() == undefined || $("#ddlProductName").val() == null) {
        $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divSelectProductName").addClass('has-error'); $("#ddlProductName").focus(); return false;
    }
    else { $("#divSelectProductName").removeClass('has-error'); }

    if ($("#txtQuantity").val() == "" || $("#txtQuantity").val() == undefined || $("#txtQuantity").val() == null || $("#txtQuantity").val() <= 0) {
        $.jGrowl("Please enter Quantity", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
    } else { $("#divQuantity").removeClass('has-error'); }

    if ($("#txtRate").val() == "" || $("#txtRate").val() == undefined || $("#txtRate").val() == null) {
        $.jGrowl("Please enter Rate", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divRate").addClass('has-error'); $("#txtRate").focus(); return false;
    } else { $("#divRate").removeClass('has-error'); }

    //if ($("#ddlTax").val() == "0" || $("#ddlTax").val() == undefined || $("#ddlTax").val() == null) {
    //    $.jGrowl("Please select Tax", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divTaxTrans").addClass('has-error'); $("#ddlTax").focus(); return false;
    //}
    //else { $("#divTaxTrans").removeClass('has-error'); }

    if ($("#txtDisPer").val() == "" || $("#txtDisPer").val() == undefined || $("#txtDisPer").val() == null) {
        $.jGrowl("Please enter Disc. Percent", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDisPer").addClass('has-error'); $("#txtDisPer").focus(); return false;
    } else { $("#divDisPer").removeClass('has-error'); }

    if ($("#txtDisAmt").val() == "" || $("#txtDisAmt").val() == undefined || $("#txtDisAmt").val() == null) {
        $.jGrowl("Please enter Disc. Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divDisAmt").addClass('has-error'); $("#txtDisAmt").focus(); return false;
    } else { $("#divDisAmt").removeClass('has-error'); }

    if ($("#hdnMaxsalesDiscount").val() > 0) {
        if (parseFloat($("#hdnMaxsalesDiscount").val()) < parseFloat($("#txtDisPer").val())) {
            $.jGrowl(" Discount Percentage Exceeding Maximum discount", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#txtDisPer").focus(); return false;
        }
    }

    var iorate = $("#hdnOriginalRate").val();
    var irate = $("#txtRate").val();
    if (parseFloat($("#hdnOriginalRate").val()) != "0" && parseFloat($("#hdnOriginalRate").val()) != parseFloat($("#txtRate").val()))
        $("#hdnRateChanged").val("1").change();
    var iStockCount = 0; var StockValue = 0; var StockQty = 0; var WholesaleMinPrice = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].Product.ProductID == $("#ddlProductName").val()) {
            iStockCount = iStockCount + 1;
            StockQty = gOPBillingList[i].Quantity;
        }
    }
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetStockByID",
        data: JSON.stringify({ ID: $("#ddlProductName").val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            StockValue = obj.Quantity;
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });

    if (this.id == "btnAddMagazine" || $("#hdnSalesEntryID").val() == 0) {
        if (StockValue < $("#txtQuantity").val()) {
            $.jGrowl("Stock Not Available", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
        }
    }
    else {
        if ((parseFloat(StockValue) + parseFloat(StockQty)) < $("#txtQuantity").val()) {
            $.jGrowl("Stock Not Available", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#divQuantity").addClass('has-error'); $("#txtQuantity").focus(); return false;
        }
    }


    var ObjData = new Object();
    ObjData.SalesEntryID = 0;

    var oProduct = new Object();
    oProduct.ProductID = $("#ddlProductName").val();
    oProduct.ProductName = $("#ddlProductName option:selected").text();
    ObjData.Product = oProduct;


    var oStock = new Object();
    oStock.StockID = 0
    ObjData.Stock = oStock;

    var oTaxTrans = new Object();
    oTaxTrans.TaxID = 8;
    oTaxTrans.TaxPercentage =0;
    if ($("#hdnStateCode").val() == 33) {
        oTaxTrans.CGSTPercent = 0;
        oTaxTrans.SGSTPercent = 0;
        oTaxTrans.IGSTPercent = 0;
    }
    else {
        oTaxTrans.CGSTPercent = 0;
        oTaxTrans.SGSTPercent = 0;
        oTaxTrans.IGSTPercent = 0;
    }
    ObjData.Tax = oTaxTrans;
    ObjData.SGSTAmount =0;
    ObjData.CGSTAmount = 0;
    ObjData.IGSTAmount = 0;
    ObjData.TaxAmount =0;
    ObjData.Quantity = parseFloat($("#txtQuantity").val());
    ObjData.PurchasePrice = parseFloat($("#txtPurchasePrice").val());
    ObjData.PreviousPrice = parseFloat($("#txtPreviousPrice").val());
    ObjData.SalesMargin = parseFloat($("#txtSalesMargin").val());
    ObjData.Rate = parseFloat($("#txtRate").val());
    ObjData.DiscountPercentage = parseFloat($("#txtDisPer").val());
    ObjData.DiscountAmount = parseFloat($("#txtDisAmt").val());
    ObjData.SubTotal = parseFloat($("#txtSubTotal").val());
    ObjData.Barcode = $("#txtBarcode").val();
  //  GetProductDetails(ObjData.Product.ProductID, $("#ddlCustomerName").val(), $("#hdnSalesEntryID").val());
    if (RecordAvailable == 0)
        ObjData.NewProductFlag = 1;
    else
        ObjData.NewProductFlag = 0;

    if (this.id == "btnAddMagazine") {
        ObjData.sNO = gOPBillingList.max() + 1;
        ObjData.SNo = ObjData.sNO;
        ObjData.SalesEntryID = 0;
        ObjData.StatusFlag = "I";
        var Count = 0;
        for (var i = 0; i < gOPBillingList.length; i++) {
            if (gOPBillingList[i].StatusFlag != "D") {
                if ((gOPBillingList[i].Product.ProductID == $("#ddlProductName").val()) && (gOPBillingList[i].Rate == parseFloat($("#txtRate").val())) && (gOPBillingList[i].DiscountPercentage == parseFloat($("#txtDisPer").val()))) {
                    gOPBillingList[i].Quantity = gOPBillingList[i].Quantity + parseFloat($("#txtQuantity").val());
                    var iDisPercent = parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate) * parseFloat(gOPBillingList[i].DiscountPercentage) / 100;
                    gOPBillingList[i].DiscountAmount = parseFloat(iDisPercent);
                    gOPBillingList[i].SubTotal = gOPBillingList[i].SubTotal + parseFloat($("#txtSubTotal").val());
                    if ($("#hdnStateCode").val() == 33) {
                        gOPBillingList[i].Tax.CGSTPercent = 0;
                        gOPBillingList[i].Tax.SGSTPercent = 0;
                        gOPBillingList[i].Tax.IGSTPercent = 0;
                        gOPBillingList[i].CGSTAmount  = 0;
                        gOPBillingList[i].SGSTAmount = 0;
                        gOPBillingList[i].IGSTAmount = 0;
                    }
                    else {
                        gOPBillingList[i].Tax.CGSTPercent = 0;
                        gOPBillingList[i].Tax.SGSTPercent = 0;
                        gOPBillingList[i].Tax.IGSTPercent = 0;
                        gOPBillingList[i].CGSTAmount = 0;
                        gOPBillingList[i].SGSTAmount = 0;
                        gOPBillingList[i].IGSTAmount = 0;
                    }
                    gOPBillingList[i].TaxAmount = 0;
                    Count = 1;
                }
            }
        }
        if (Count == 0)
            AddOPBillingData(ObjData);
        else
            DisplayOPBillingList(gOPBillingList);
    }
    else if (this.id == "btnUpdateMagazine") {
        ObjData.sNO = $("#hdnOPSNo").val();
        if ($("#hdnSalesEntryID").val() > 0) {
            ObjData.StatusFlag = "U";
            ObjData.SalesEntryID = $("#hdnSalesEntryID").val();
        }
        else {
            ObjData.StatusFlag = "I";
            ObjData.SalesEntryID = 0;
        }
        Update_OPBilling(ObjData);
    }
    //if (RecordAvailable == 0) {
    //    $.jGrowl("No Previous Record Available", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    RecordAvailable = 0;
    //}
    CalculateAmount();
    //CalculateCustomerTotalValue();
    ClearOPBillingFields();
    var scrollBottom = Math.max($('#tblOPBillingList').height());
    $('#divOPBillingList').scrollTop(scrollBottom);

    $("#ddlProductName").focus();
    $("#btnAddMagazine").show();
    $("#btnUpdateMagazine").hide();
    $("#txtCode").focus();
});

$("#txtRoundoff").keypress(function (e) {
    if (e.which != 46 && e.which != 45 && e.which != 46 &&
        !(e.which >= 48 && e.which <= 57)) {
        return false;
    }
});

function ClearOPBillingFields() {
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    $("#ddlProductName").val("0").change();
    $("#ddlBatchNo").val("0").change();
    $("#txtCode").val("");
    $("#txtQuantity").val("0");
    $("#txtRate").val("0");
    $("#txtTaxAmt").val("0");
    $("#hdnOriginalRate").val("0");
    $("#txtDisPer").val("0");
    $("#txtDisAmt").val("0");
    $("#txtPurchasePrice").val("0");
    $("#txtPreviousPrice").val("0");
    $("#txtSalesMargin").val("0");
    $("#txtAvailableQty").val("0");
    $("#txtSubTotal").val("0.00");
    $("#txtBarcode").val("");
    $("#hdnOPSNo").val("");
    //$("#hdnSalesEntryID").val("");
    if (parseFloat($("#txtDiscountPercent").val()) > 0) {
        $("#txtDisPer").val($("#txtDiscountPercent").val());
    }
    $("#ddlProductName").val(null).change();
    $("#ddlBatchNo").val(null).change();
    $("#divSelectProductName").show();
    $("#divProductName").removeClass('has-error');
    $("#divQuantity").removeClass('has-error');
    $("#divRate").removeClass('has-error');
    return false;

    $("#ddlTax").val($("#ddlTaxName").val()).change();

}

function AddOPBillingData(oData) {
    gOPBillingList.push(oData);
    DisplayOPBillingList(gOPBillingList);
    return false;
}

//$("#txtDiscountAmount").change(function () {
//    calculateDiscount();
//});

function calculateDiscount() {
    var iOPBillingAmount = 0, iBillingDiscount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iOPBillingAmount = iOPBillingAmount + (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate));
        }
    }

    var iBillingDiscount = parseFloat($("#txtDiscountAmount").val());
    if (isNaN(iBillingDiscount)) iBillingDiscount = 0;
    $("#txtDiscountPercent").val(parseFloat(iBillingDiscount) * 100 / parseFloat(iOPBillingAmount).toFixed(2)).change();
}


function DisplayOPBillingList(gData) {
    var sTable = "";
    var sCount = 1;
    var sColorCode = "bg-info";

    if (gData.length >= 12) { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '400px', 'overflow': 'auto' }); }
    else { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }

    if (gData.length > 0) {
        sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
        sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
        sTable += "<th class='" + sColorCode + "'>Product Name</th>";
        sTable += "<th class='" + sColorCode + "'>PR</th>";
        sTable += "<th class='" + sColorCode + "'>SM</th>";
        sTable += "<th class='" + sColorCode + "'>PP</th>";
        sTable += "<th class='" + sColorCode + "'>Qty</th>";
        sTable += "<th class='" + sColorCode + "'>Rate</th>";
        sTable += "<th class='" + sColorCode + "'>DiscountPercentage</th>";
        sTable += "<th class='" + sColorCode + "'>DiscountAmount</th>";
        //sTable += "<th class='" + sColorCode + "'>Tax Percentage</th>";
        //sTable += "<th class='" + sColorCode + "'>Tax Amount</th>";
        sTable += "<th class='" + sColorCode + "'>Subtotal</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Edit</th>";
        sTable += "<th class='" + sColorCode + "' style='width:3px;text-align: center'>Delete</th>";
        sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
        sTable += "</tbody></table>";
        $("#divOPBillingList").html(sTable);
        for (var i = 0; i < gData.length; i++) {
            if (gData[i].StatusFlag != "D") {
                if (gData[i].NewProductFlag == 0)
                    sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                else
                    sTable = "<tr style='background-color:#f3c8c8;'><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                $("#txtSNo").val(sCount + 1);
                sTable += "<td>" + gData[i].Product.ProductName + "</td>";
                sTable += "<td>" + gData[i].PurchasePrice + "</td>";
                sTable += "<td>" + gData[i].SalesMargin + "</td>";
                sTable += "<td>" + gData[i].PreviousPrice + "</td>";
                sTable += "<td>" + gData[i].Quantity + "</td>";
                sTable += "<td>" + gData[i].Rate + "</td>";
                sTable += "<td>" + gData[i].DiscountPercentage.toFixed(2) + " %</td>";
                sTable += "<td>" + gData[i].DiscountAmount.toFixed(2) + "</td>";
                //sTable += "<td>" + gData[i].Tax.TaxPercentage + " %</td>";
                //sTable += "<td>" + gData[i].TaxAmount + "</td>";
                sTable += "<td>" + gData[i].SubTotal + "</td>";
             
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Edit_OPBillingDetail(this.id)'><i class='fa fa-lg fa-edit'/></a></td>";
                sTable += "<td><a href='#' id=" + gData[i].SNo + " onclick = 'Delete_OPBillingDetail(this.id)'><i class='fa fa-lg fa-trash-o text-red'/></a></td>";
                //sTable += "<td><a href='#' id=" + gData[i].Product.ProductID + " onclick = 'GetProductByID(this.id)'><i class='fa fa-lg fa-file-image-o text-green'/></a></td>";
                sTable += "</tr>";
                sCount = sCount + 1;
                $("#tblOPBillingList_body").append(sTable);
            }
        }
    }
    else { $("#divOPBillingList").empty(); }

    return false;
}

function Edit_Magazine(ID) {
    Bind_MagazineByID(ID, gOPBillingList);
    return false;
}

function Bind_OPBillingByID(ID, data) {
    $("#btnAddMagazine").hide();
    $("#btnUpdateMagazine").show();
    $("#ddlProductName").focus();
    var sCount = 0;
    for (var i = 0; i < data.length; i++) {
        if (data[i].sNO == ID) {
            if (data[i].StatusFlag != "D") {
                $("#txtSNo").val(ID);
                $("#hdnOPSNo").val(ID);
                $("#hdnSalesEntryID").val(data[i].SalesEntryID);
                $("#ddlProductName").val(data[i].Product.ProductID).change();
                $("#ddlBatchNo").val(data[i].Stock.StockID).change();
                $("#ddlTax").val(data[i].Tax.TaxID).change();
                $("#txtTaxAmt").val(data[i].TaxAmount);
                $("#txtQuantity").val(data[i].Quantity);
                $("#txtPreviousPrice").val(data[i].PreviousPrice);
                $("#txtPurchasePrice").val(data[i].PurchasePrice);
                $("#txtSalesMargin").val(data[i].SalesMargin);
                $("#txtRate").val(data[i].Rate);
                $("#txtDisPer").val(data[i].DiscountPercentage);
                $("#txtDisAmt").val(data[i].DiscountAmount);
                $("#txtSubTotal").val(data[i].SubTotal);
                $("#txtBarcode").val(data[i].Barcode);
            }
        }
    }
    return false;
}

function Update_OPBilling(oData) {
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].sNO == oData.sNO) {
            gOPBillingList[i].SalesEntryID = oData.SalesEntryID;
            var oProduct = new Object();
            oProduct.ProductID = oData.Product.ProductID;
            oProduct.ProductName = oData.Product.ProductName;
            gOPBillingList[i].Product = oProduct;

            var oStock = new Object();
            oStock.StockID = oData.Stock.StockID;
            oStock.BatchNo = oData.Stock.BatchNo;
            gOPBillingList[i].Stock = oStock;

            var oTransTax = new Object();
            oTransTax.TaxID = oData.Tax.TaxID;
            oTransTax.TaxPercentage = oData.Tax.TaxPercentage;
            oTransTax.IGSTPercent = oData.Tax.IGSTPercent;
            oTransTax.SGSTPercent = oData.Tax.SGSTPercent;
            oTransTax.CGSTPercent = oData.Tax.CGSTPercent;
            gOPBillingList[i].Tax = oTransTax;

            gOPBillingList[i].TaxAmount = oData.TaxAmount;
            gOPBillingList[i].CGSTAmount = oData.CGSTAmount;
            gOPBillingList[i].SGSTAmount = oData.SGSTAmount;
            gOPBillingList[i].IGSTAmount = oData.IGSTAmount;
            gOPBillingList[i].Quantity = oData.Quantity;
            gOPBillingList[i].Rate = oData.Rate;
            gOPBillingList[i].PurchasePrice = oData.PurchasePrice;
            gOPBillingList[i].SalesMargin = oData.SalesMargin;
            gOPBillingList[i].PreviousPrice = oData.PreviousPrice;
            gOPBillingList[i].DiscountPercentage = oData.DiscountPercentage;
            gOPBillingList[i].DiscountAmount = oData.DiscountAmount;
            gOPBillingList[i].SubTotal = oData.SubTotal;
            gOPBillingList[i].Barcode = oData.Barcode;
            gOPBillingList[i].StatusFlag = oData.StatusFlag;
            gOPBillingList[i].NewProductFlag = oData.NewProductFlag;
        }
    }
    DisplayOPBillingList(gOPBillingList);
    $("#btnAddOPBilling").show();
    $("#btnUpdateOPBilling").hide();
    ClearOPBillingFields();
    $("#ddlProductName").focus();
    return false;
}

function Delete_OPBillingDetail(ID) {
    if (ID == 0)
        return false;

    if (confirm('Are you sure to delete the selected record ?')) {
        for (var i = 0; i < gOPBillingList.length; i++) {
            if (gOPBillingList[i].SNo == ID) {
                var index = jQuery.inArray(gOPBillingList[i].valueOf("SNo"), gOPBillingList);
                if (gOPBillingList[i].SNo > 0) {
                    gOPBillingList[i].StatusFlag = "D";
                } else {
                    gOPBillingList.splice(index, 1);
                }
                $("#divOPBillingList").empty();
                DisplayOPBillingList(gOPBillingList);
                CalculateAmount();
            }
        }
    }
    return false;
}

function CalculateNetPrice() {
    var iAmountPrice = parseFloat($("#txtQuantity").val());


    if (isNaN(iAmountPrice)) iNoofCopies = 0;

    return false;
}

$("#txtSearchName").change(function () {

    if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
        var iDetails = $("#txtSearchName").val();
        GetSearchRecord(iDetails);
    }
    return false;
});

function GetRecord() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTopSalesEntry",
        data: JSON.stringify({ PublisherID: 0}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    var notetable = $("#tblRecord").dataTable();
                    notetable.fnDestroy();
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            $("#tblRecord_tbody").empty();
                            var TypeStatus = "";
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive == "1") {
                                    //{ TypeStatus = "<span class='label label-success'>Active</span>"; }
                                    //else
                                    //{ TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                    var table = "";
                                    if (obj[index].BalanceAmount > 0) {
                                        if (obj[index].DueDays == obj[index].Customer.MinDueDays)
                                            table += "<tr style='background-color:#d9f7927a;' id='" + obj[index].SalesEntryID + "'>";
                                        else
                                            table += "<tr id='" + obj[index].SalesEntryID + "'>";
                                    } else
                                        table += "<tr style='background-color:#f1c6ad;' id='" + obj[index].SalesEntryID + "'>";

                                    // var table = "<tr id='" + obj[index].SalesEntryID + "'>";
                                    table += "<td>" + (index + 1) + "</td>";
                                    table += "<td>" + obj[index].InvoiceNo + "</td>";
                                    table += "<td>" + obj[index].sWholeSalesInvoiceDate + "</td>";
                                    table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                    table += "<td>" + obj[index].TotalQty + "</td>";
                                    table += "<td>" + obj[index].NetAmount + "</td>";

                                    if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='PrintSales' title='Click here to Print Invoice'></i><i class='fa fa-print text-green'/></a></td>";

                                    //table += "<td style='text-align:center; color: darkviolet;'><a href='#' id=hid" + obj[index].Customer.CustomerID + " class='Address' style='color:black;' Accountno='" + obj[index].Customer.CustomerID + "' title='click here to Customer Ledger'><i class='fa fa-address-card' style='color: darkviolet;'></i></a></td>";

                                    //table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='LR' title='Click here for LR Entry'><i class='fa fa-truck text-black'></i></a></td>";

                                    table += "</tr>";
                                    $("#tblRecord_tbody").append(table);
                                }
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#btnSave").hide();
                                    $("#btnUpdate").hide();
                                    $("#btnPrintbill").show();
                                    $("#btnAddMagazine").hide();
                                    $("#btnUpdateMagazine").hide();
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Edit").click(function () {
                                if (ActionUpdate == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#divOtherPasswordlbl").show();
                                    $("#divOtherPassword").show();
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".PrintSales").click(function () {
                                SetSessionValue("SalesEntryID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintSalesEntryInvoice.aspx", "MsgWindow");
                            });
                            $(".Address").click(function () {
                                SetSessionValue("SalesID", $(this).attr('Accountno'));
                                SetSessionValue("Table", "Customer");
                                var myWindow = window.open("frmAddressLabel.aspx", "MsgWindow");
                            });
                            $(".LR").click(function () {
                                SetSessionValue("SalesEntryID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("frmLREntry.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?')) { ShowDeleteRecord($(this).parent().parent()[0].id); }
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#tblRecord_tbody").empty();
                        dProgress(false);
                    }
                    $("#tblRecord").dataTable({
                        "bPaginate": true,
                        "bFilter": true,
                        "bSort": true,
                        "iDisplayLength": 25,
                        aoColumns: [
                            { "sWidth": "5%" },
                            { "sWidth": "10%" },
                            { "sWidth": "13%" },
                            { "sWidth": "40%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "2%" },
                            { "sWidth": "2%" },
                            { "sWidth": "2%" },
                            //{ "sWidth": "2%" },
                            //{ "sWidth": "2%" },
                            { "sWidth": "2%" }
                        ]
                    });
                    $("#tblRecord_filter").addClass('pull-right');
                    $(".pagination").addClass('pull-right');
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                }
            }
            else {
                $("#tblRecord_tbody").empty();
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function GetSearchRecord(iDetails) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/SearchSalesEntry",
        data: JSON.stringify({ ID: iDetails, IsRetail: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    var notetable = $("#tblSearchResult").dataTable();
                    notetable.fnDestroy();
                    if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                        var obj = $.parseJSON(objResponse.Value);
                        if (obj.length > 0) {
                            $("#tblSearchResult_tbody").empty();
                            var TypeStatus = "";
                            for (var index = 0; index < obj.length; index++) {
                                if (obj[index].IsActive == "1") {

                                    //    { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                    //else
                                    //{ TypeStatus = "<span class='label label-danger'>Cancelled</span>"; }

                                    var table = "";
                                    if (obj[index].BalanceAmount > 0) {
                                        if (obj[index].DueDays == obj[index].Customer.MinDueDays)
                                            table += "<tr style='background-color:#d9f7927a;' id='" + obj[index].SalesEntryID + "'>";
                                        else
                                            table += "<tr id='" + obj[index].SalesEntryID + "'>";
                                    } else
                                        table += "<tr style='background-color:#f1c6ad;' id='" + obj[index].SalesEntryID + "'>";

                                    // var table = "<tr id='" + obj[index].SalesEntryID + "'>";
                                    table += "<td>" + (index + 1) + "</td>";
                                    table += "<td>" + obj[index].InvoiceNo + "</td>";
                                    table += "<td>" + obj[index].sWholeSalesInvoiceDate + "</td>";
                                    table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                    table += "<td>" + obj[index].TotalQty + "</td>";
                                    table += "<td>" + obj[index].NetAmount + "</td>";

                                    if (ActionView == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    if (ActionDelete == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Delete' title='Click here to Cancel'><i class='fa fa-lg fa-times-circle text-red'/></a></td>"; }
                                    else { table += "<td></td>"; }

                                    table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='PrintSales' title='Click here to Print Invoice'></i><i class='fa fa-print text-green'/></a></td>";

                                    //table += "<td style='text-align:center; color: darkviolet;'><a href='#' id=hid" + obj[index].Customer.CustomerID + " class='Address' style='color:black;' Accountno='" + obj[index].Customer.CustomerID + "' title='click here to Customer Ledger'><i class='fa fa-address-card' style='color: darkviolet;'></i></a></td>";

                                    //table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='LR' title='Click here for LR Entry'><i class='fa fa-truck text-black'></i></a></td>";

                                    table += "</tr>";
                                    $("#tblSearchResult_tbody").append(table);
                                }
                            }
                            $(".View").click(function () {
                                if (ActionView == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#btnSave").hide();
                                    $("#btnUpdate").hide();
                                    $("#btnPrintbill").show();
                                    $("#btnAddMagazine").hide();
                                    $("#btnUpdateMagazine").hide();
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Edit").click(function () {
                                if (ActionUpdate == "1") {
                                    EditRecord($(this).parent().parent()[0].id);
                                    $("#divOtherPasswordlbl").show();
                                    $("#divOtherPassword").show();
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                            $(".Address").click(function () {
                                SetSessionValue("SalesID", $(this).attr('Accountno'));
                                SetSessionValue("Table", "Customer");
                                var myWindow = window.open("frmAddressLabel.aspx", "MsgWindow");
                            });
                            $(".LR").click(function () {
                                SetSessionValue("SalesEntryID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("frmLREntry.aspx", "MsgWindow");
                            });
                            $(".PrintSales").click(function () {
                                SetSessionValue("SalesEntryID", parseInt($(this).parent().parent()[0].id));
                                var myWindow = window.open("PrintSalesEntryInvoice.aspx", "MsgWindow");
                            });
                            $(".Delete").click(function () {
                                if (ActionDelete == "1") {
                                    if (confirm('Are you sure to cancel the selected record ?')) { ShowDeleteRecord($(this).parent().parent()[0].id); }
                                }
                                else {
                                    $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    return false;
                                }
                            });
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $("#tblSearchResult_tbody").empty();
                        dProgress(false);
                    }
                    $("#tblSearchResult").dataTable({
                        "bPaginate": true,
                        "bFilter": true,
                        "bSort": true,
                        "iDisplayLength": 25,
                        aoColumns: [
                            { "sWidth": "5%" },
                            { "sWidth": "10%" },
                            { "sWidth": "13%" },
                            { "sWidth": "40%" },
                            { "sWidth": "10%" },
                            { "sWidth": "10%" },
                            { "sWidth": "2%" },
                            { "sWidth": "2%" },
                            //{ "sWidth": "2%" },
                            //{ "sWidth": "2%" },
                            { "sWidth": "2%" },
                            { "sWidth": "2%" }
                        ]
                    });
                    $("#tblSearchResult_filter").addClass('pull-right');
                    $(".pagination").addClass('pull-right');
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                }
            }
            else {
                $("#tblSearchResult_tbody").empty();
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

$("#ddlTaxName").change(function () {
    var iTaxID = $("#ddlTaxName").val();
    $("#ddlTax").val($("#ddlTaxName").val()).change();
    if (iTaxID != undefined && iTaxID > 0) {
        GetTaxByID(iTaxID);
        var iSubtotal = 0;
        for (var i = 0; i < gOPBillingList.length; i++) {
            if (gOPBillingList[i].StatusFlag != "D") {
                iSubtotal = gOPBillingList[i].SubTotal;
                gOPBillingList[i].Tax.TaxID = iTaxID;
                gOPBillingList[i].Tax.TaxPercentage = parseFloat($("#hdnTaxPercent").val());
                if ($("#hdnStateCode").val() == 33) {
                    gOPBillingList[i].Tax.CGSTPercent = parseFloat($("#hdnCGSTPercent").val());
                    gOPBillingList[i].Tax.SGSTPercent = parseFloat($("#hdnSGSTPercent").val());
                    gOPBillingList[i].Tax.IGSTPercent = 0;
                    gOPBillingList[i].CGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
                    gOPBillingList[i].SGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
                    gOPBillingList[i].IGSTAmount = 0;
                }
                else {
                    gOPBillingList[i].Tax.CGSTPercent = 0;
                    gOPBillingList[i].Tax.SGSTPercent = 0;
                    gOPBillingList[i].Tax.IGSTPercent = parseFloat($("#hdnIGSTPercent").val());
                    gOPBillingList[i].CGSTAmount = 0;
                    gOPBillingList[i].SGSTAmount = 0;

                    gOPBillingList[i].IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
                }
                gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
            }
        }
        DisplayOPBillingList(gOPBillingList);
        CalculateAmount();
    }
});

$("#ddlTax").change(function () {
    var iTax = $("#ddlTax").val();
    if (iTax != undefined && iTax > 0) {
        GetTaxTransByID(iTax);
        CalculateAmountTrans();
    }
});

function GetTaxTransByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTaxByID",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("#hdnTransTaxPercent").val(obj.TaxPercentage);
                            $("#hdnTransCGSTPercent").val(obj.CGSTPercent);
                            $("#hdnTransSGSTPercent").val(obj.SGSTPercent);
                            $("#hdnTransIGSTPercent").val(obj.IGSTPercent);
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function CalculateAmountTrans() {
    var iTax = parseFloat($("#hdnTransTaxPercent").val());
    var iRate = parseFloat($("#txtRate").val());
    var iqty = parseFloat($("#txtQuantity").val());
    var iCGST = parseFloat($("#hdnTransCGSTPercent").val());
    var iSGST = parseFloat($("#hdnTransSGSTPercent").val());
    var iIGST = parseFloat($("#hdnTransIGSTPercent").val());

    if (isNaN(iRate)) iRate = 0;
    if (isNaN(iqty)) iqty = 0;
    if (isNaN(iTax)) iTax = 0;
    var iTaxPercent = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iTax) / 100;
    $("#txtTaxAmt").val(parseFloat(iTaxPercent).toFixed(2));

    if ($("#hdnStateCode").val() == 33) {
        var iCGSTPercent = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iCGST) / 100;
        var iSGSTPercent = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iSGST) / 100;
        $("#hdnTransSGSTAmount").val(parseFloat(iSGSTPercent).toFixed(2));
        $("#hdnTransCGSTAmount").val(parseFloat(iCGSTPercent).toFixed(2));
        $("#hdnTransIGSTAmount").val(0)
    }
    else {
        var iIGSTPercent = parseFloat(iRate) * parseFloat(iqty) * parseFloat(iIGST) / 100;
        $("#hdnTransSGSTAmount").val(0);
        $("#hdnTransCGSTAmount").val(0);
        $("#hdnTransIGSTAmount").val(parseFloat(iIGSTPercent).toFixed(2))
    }
    var iSubTotal = (parseFloat(iRate) * parseFloat(iqty));
    $("#txtSubTotal").val(parseFloat(iSubTotal).toFixed(2));
    CalculateAmount();
}

$("#txtDiscountAmount").change(function () {
    CalculateAmount();
});

$("#txtRoundoff,#txtOtherCharges").change(function () {
    CalculateAmount();
});

//$("#txtDiscountPercent").change(function () {
//    var iqty = parseFloat($("#txtDiscountPercent").val());
//    if (isNaN(iqty)) iqty = 0;

//    $("#txtDiscountAmount").val((parseFloat($("#txtTotalAmount").val()) * iqty / 100).toFixed(2));
//    CalculateAmount();
//});



$("#txtTransportCharge").change(function () {
    var iqty = parseFloat($("#txtTransportCharge").val());
    if (isNaN(iqty)) iqty = 0;

    $("#txtOtherCharges").val(iqty).change();
    //CalculateAmount();
});

function GetTaxByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetTaxByID",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("#hdnTaxPercent").val(obj.TaxPercentage);
                            $("#hdnCGSTPercent").val(obj.CGSTPercent);
                            $("#hdnSGSTPercent").val(obj.SGSTPercent);
                            $("#hdnIGSTPercent").val(obj.IGSTPercent);
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function CalculateAmount() {
    var iOPBillingAmount = 0, TotalAmount = 0, iBillingQty = 0, RoundOff = 0, Amount = 0, iBillingCGST = 0, iBillingSGST = 0, iBillingIGST = 0, iBillingDiscount = 0, iBillingTaxAmt = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            iOPBillingAmount = iOPBillingAmount + parseFloat(gOPBillingList[i].SubTotal);
            iBillingTaxAmt = iBillingTaxAmt + parseFloat(gOPBillingList[i].TaxAmount);
            iBillingCGST = iBillingCGST + parseFloat(gOPBillingList[i].CGSTAmount);
            iBillingSGST = iBillingSGST + parseFloat(gOPBillingList[i].SGSTAmount);
            iBillingIGST = iBillingIGST + parseFloat(gOPBillingList[i].IGSTAmount);
            iBillingDiscount = iBillingDiscount + parseFloat(gOPBillingList[i].DiscountAmount);
            iBillingQty = iBillingQty + parseFloat(gOPBillingList[i].Quantity);
            TotalAmount = TotalAmount + (parseFloat(gOPBillingList[i].Rate) * parseFloat(gOPBillingList[i].Quantity));
        }
    }

    $("#txtAmount").val(parseFloat(TotalAmount).toFixed(2));
    $("#txtTotalAmount").val(parseFloat(iOPBillingAmount).toFixed(2));

    $("#txtTotalTaxAmount").val((parseFloat(iBillingTaxAmt) + parseFloat(iOPBillingAmount)).toFixed(2));

    $("#txtTaxAmount").val(parseFloat(iBillingTaxAmt).toFixed(2));
    $("#txtCGST").val(parseFloat(iBillingCGST).toFixed(2));
    $("#txtSGST").val(parseFloat(iBillingSGST).toFixed(2));
    $("#txtIGST").val(parseFloat(iBillingIGST).toFixed(2));
   // $("#txtDiscountAmount").val(parseFloat(iBillingDiscount).toFixed(2));

    //var iround = parseFloat($("#txtRoundoff").val());
    //if (isNaN(iround)) iround = 0;


    var iTCS_Amt = parseFloat($("#txtTCSAmount").val());
    if (isNaN(iTCS_Amt)) iTCS_Amt = 0;
    $("#txtTotalQty").val((parseFloat(iBillingQty)).toFixed(0));
    Amount = (parseFloat(iOPBillingAmount) + parseFloat($("#txtTaxAmount").val()) + parseFloat(iTCS_Amt)).toFixed(2);
    // $("#txtOtherCharges").val($("#txtTransportCharge").val());

    RoundOff = Math.round(Amount);
    $("#txtRoundoff").val((RoundOff - Amount).toFixed(2));
    var iround = parseFloat($("#txtRoundoff").val());
    if (isNaN(iround)) iround = 0;
    $("#txtNetAmount").val((parseFloat(iOPBillingAmount) + parseFloat($("#txtTaxAmount").val()) + parseFloat($("#txtOtherCharges").val()) + parseFloat(iround) + parseFloat(iTCS_Amt) - parseFloat($("#txtDiscountAmount").val())).toFixed(2));
    var iNet = parseFloat($("#txtNetAmount").val());
    if (isNaN(iNet)) iNet = 0;
    var iTender = parseFloat($("#txtTenderAmount").val());
    if (isNaN(iTender)) iTender = 0;
    if (iTender > 0)
        $("#txtBalanceGiven").val((parseFloat(iNet) - parseFloat(iTender)).toFixed(2));
    else
        $("#txtBalanceGiven").val("0");
}

$("#btnPrint").click(function () {
    SetSessionValue("SalesEntryID", $("#hdnSalesEntryID").val());
    var myWindow = window.open("PrintSalesEntryInvoice.aspx", "MsgWindow");
});


function GetLastSalesEntryByID() {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetLastSalesEntryByID",
        data: JSON.stringify({ ID: $("#hdnSalesID").val(), IsRetail: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("#hdnLastinvoiceDate").val(obj.sInvoiceDate);
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

$("#btnSave,#btnUpdate").click(function () {
    //CalculateAmountTrans();  
    TaxCalculate();
    $("#ddlTaxName").change();
    if (this.id == "btnSave") {
        if (ActionAdd != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
    }
    else if (this.id == "btnUpdate") {
        if (ActionUpdate != "1") { $.jGrowl("Access Denied", { sticky: false, theme: 'danger', life: jGrowlLife }); return false; }
        if (ActionUpdate == "1") {
            if ($("#txtOtherPassword").val().trim() == "" || $("#txtOtherPassword").val().trim() == undefined || $("#txtOtherPassword").val().trim() != $("#hdRS").val()) {
                $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divOtherPassword").addClass('has-error'); $("#txtOtherPassword").focus(); return false;
            } else { $("#divOtherPassword").removeClass('has-error'); }
        }
    }

    if ($("#txtBillDate").val().trim() == "" || $("#txtBillDate").val().trim() == undefined) {
        $.jGrowl("Please select Bill Date", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divBillDate").addClass('has-error'); $("#txtBillDate").focus(); return false;
    }
    else { $("#divBillDate").removeClass('has-error'); }

    //if ($("#txtLRDate").val().trim() == "" || $("#txtLRDate").val().trim() == undefined) {
    //    $.jGrowl("Please select LR Date", { sticky: false, theme: 'warning', life: jGrowlLife });
    //    $("#divLRDate").addClass('has-error'); $("#txtLRDate").focus(); return false;
    //}
    //else { $("#divLRDate").removeClass('has-error'); }

    if ($("#ddlCustomerName").val() == "0" || $("#ddlCustomerName").val() == undefined || $("#ddlCustomerName").val() == null) {
        $.jGrowl("Please select Customer", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divCustomerName").addClass('has-error'); $("#ddlCustomerName").focus(); return false;
    }
    else { $("#divCustomerName").removeClass('has-error'); }


    if ($("#hdnMaxsalesDiscount").val() > 0) {
        if (parseFloat($("#hdnMaxsalesDiscount").val()) < parseFloat($("#txtDiscountPercent").val())) {
            $.jGrowl(" Discount Percentage Exceeding Maximum discount", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#txtDiscountPercent").focus(); return false;
        }
    }

    if ($("#hdnSalesLimit").val() > 0) {
        if ($("#hdnSalesEntryID").val() > 0) {
            if ($("#ddlPaymentMode").val() == "Credit") {
                var TotalNetAmount = (parseFloat($("#hdnCustomerBalanceAmount").val()) + parseFloat($("#txtNetAmount").val())) - parseFloat($("#hdnNetAmt").val());
                // var Limit = parseFloat($("#hdnSalesLimit").val());
                if ($("#hdnSalesLimit").val() < TotalNetAmount) {
                    $.jGrowl("Credit Limit Exceed  Pending balance Amount  Rs . " + TotalNetAmount + "", { sticky: false, theme: 'warning', life: jGrowlLife });
                    return false;
                }
            }
        }
        else {
            if ($("#ddlPaymentMode").val() == "Credit") {
                var TotalNetAmount = parseFloat($("#hdnCustomerBalanceAmount").val()) + parseFloat($("#txtNetAmount").val());
                // var Limit = parseFloat($("#hdnSalesLimit").val());
                if ($("#hdnSalesLimit").val() < TotalNetAmount) {
                    $.jGrowl("Credit Limit Exceed  Pending balance Amount RS . " + TotalNetAmount + "", { sticky: false, theme: 'warning', life: jGrowlLife });
                    return false;
                }
            }

        }
    }
    if (gOPBillingList.length <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtCode").focus(); return false;
    }

    var iOPBillingAmount = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D")
            iOPBillingAmount = iOPBillingAmount + 1;
    }
    if (iOPBillingAmount <= 0) {
        $.jGrowl("No Product has been added", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#txtCode").focus(); return false;
    }
    var ObjOPBilling = new Object();

    ObjOPBilling.SalesEntryID = 0;
    ObjOPBilling.InvoiceNo = $("#txtBillNo").val().trim();
    ObjOPBilling.sInvoiceDate = $("#txtBillDate").val().trim();
    ObjOPBilling.sLRDate = $("#txtLRDate").val().trim();
    ObjOPBilling.SalesEntryTrans = gOPBillingList;

    var ObjCustomer = new Object();
    ObjCustomer.CustomerID = $("#ddlCustomerName").val();
    ObjCustomer.MobileNo = "";
    ObjCustomer.CustomerName = "";
    ObjCustomer.Address = "";
    ObjCustomer.Area = "";
    ObjOPBilling.Customer = ObjCustomer;

    var ObjTax = new Object();
    ObjTax.TaxID =8;
    ObjOPBilling.Tax = ObjTax;

    var ObjBank = new Object();
    ObjBank.LedgerID = 0;
    ObjOPBilling.Bank = ObjBank;

    var ObjTransport = new Object();
    ObjTransport.TransportID = 0;
    ObjOPBilling.Transport = ObjTransport;

    var ObjShippingAddress = new Object();
    ObjShippingAddress.ShippingAddressID = 0;
    ObjOPBilling.ShippingAddress = ObjShippingAddress;

    var ObjSalesOrder = new Object();
    ObjSalesOrder.SalesOrderID = 0;
    ObjOPBilling.SalesOrder = ObjSalesOrder;

    var ObjGift = new Object();
    ObjGift.GiftID = 0;
    ObjOPBilling.Gift = ObjGift;

    var oCustomerType = new Object();
    oCustomerType.CustomertypeID =0;
    ObjOPBilling.CustomerType = oCustomerType;
    var ostate = new Object();
    ostate.StateID = $("#hdnstateID").val();
    ObjOPBilling.State = ostate;
    ObjOPBilling.TaxPercent = $("#hdnTaxPercent").val().trim();
    ObjOPBilling.ImagePath1 = $("[id*=imgUpload2_view]").attr("src");
    ObjOPBilling.ImagePath2 = $("[id*=imgUpload3_view]").attr("src");
    ObjOPBilling.ImagePath3 = $("[id*=imgUpload4_view]").attr("src");
    ObjOPBilling.CGSTAmount = $("#txtCGST").val().trim();
    ObjOPBilling.SGSTAmount = $("#txtSGST").val().trim();
    ObjOPBilling.IGSTAmount = $("#txtIGST").val().trim();
    ObjOPBilling.TaxAmount = $("#txtTaxAmount").val().trim();
    ObjOPBilling.TotalAmount = $("#txtTotalAmount").val().trim();
    var Roundoff = parseFloat($("#txtRoundoff").val());
    if (isNaN(Roundoff))
        ObjOPBilling.Roundoff = 0;
    else
        ObjOPBilling.Roundoff = $("#txtRoundoff").val().trim();

    var DiscountAmount = parseFloat($("#txtRoundoff").val());
    if (isNaN(DiscountAmount))
        ObjOPBilling.DiscountAmount = 0;
    else
        ObjOPBilling.DiscountAmount = $("#txtDiscountAmount").val().trim();

    var DiscountPercent = parseFloat($("#txtDiscountPercent").val());
    if (isNaN(DiscountPercent))
        ObjOPBilling.DiscountPercent = 0;
    else
        ObjOPBilling.DiscountPercent = $("#txtDiscountPercent").val().trim();

    ObjOPBilling.AdditionalDiscount = 0;
    ObjOPBilling.NetAmount = $("#txtNetAmount").val().trim();
    ObjOPBilling.TransportCharges = $("#txtTransportCharge").val().trim();
    ObjOPBilling.OtherCharges = $("#txtOtherCharges").val().trim();
    ObjOPBilling.IsActive = 1;
    ObjOPBilling.CancelReason = "";
    ObjOPBilling.TCSPercent = $("#txtTCSPercent").val().trim();
    ObjOPBilling.TCSAmount = $("#txtTCSAmount").val().trim();
    ObjOPBilling.Narration = $("#txtNarration").val().trim();
    ObjOPBilling.LRNo = $("#txtLRNo").val().trim();
    ObjOPBilling.NoofBags = $("#txtNoofBages").val().trim();
    ObjOPBilling.Notes = $("#txtComments").val().trim();
    ObjOPBilling.EWayNo = $("#txtEWayNo").val().trim();
    ObjOPBilling.VehicleNo = $("#txtVehicleNo").val().trim();
    ObjOPBilling.ExchangeAmount = 0;
    ObjOPBilling.UsedPoints = 0;
    ObjOPBilling.SalesPoints = 0;
    ObjOPBilling.TransportName = $("#ddlTransport option:selected").text();
    ObjOPBilling.PaymentMode = $("#ddlPaymentMode").val();
    ObjOPBilling.IsRetailBill = 0;
    ObjOPBilling.BillStatus = "Completed Bill";
    ObjOPBilling.sDeliveryDate = "01-04-2021";
    ObjOPBilling.CashBill = 0;
    ObjOPBilling.RetailPaymentMode = [];

    if (ObjOPBilling.PaymentMode == "Card") {
        ObjOPBilling.CardNo = $("#txtCardNo").val().trim();
        ObjOPBilling.CardCharges = $("#txtCardCharges").val().trim();
    }
    else {
        ObjOPBilling.CardNo = "";
        ObjOPBilling.CardCharges = "0";
    }
    if ($("#hdnSalesID").val() > 0) {
        ObjOPBilling.SalesEntryID = $("#hdnSalesID").val();
        gOPBillingList.SalesEntryID = ObjOPBilling.SalesEntryID;

        if ($("#ddlPaymentMode").val() == "Credit") {
            ObjOPBilling.Status = "Pending";
            var Balance = parseFloat($("#txtNetAmount").val()) - parseFloat($("#hdnPaidAmt").val());
            ObjOPBilling.BalanceAmount = parseFloat(Balance).toFixed(2);
            ObjOPBilling.PaidAmount = parseFloat($("#hdnPaidAmt").val());
        }
        else {
            ObjOPBilling.Status = "Closed";
            ObjOPBilling.BalanceAmount = 0;
            ObjOPBilling.PaidAmount = parseFloat($("#txtNetAmount").val());

        }
        sMethodName = "UpdateSalesEntry";
    }
    else {
        sMethodName = "AddSalesEntry";
        ObjOPBilling.SalesEntryID = 0;

        if ($("#ddlPaymentMode").val() == "Credit") {
            ObjOPBilling.Status = "Pending";
            ObjOPBilling.BalanceAmount = parseFloat($("#txtNetAmount").val());
            ObjOPBilling.PaidAmount = 0;
        }
        else {
            ObjOPBilling.Status = "Closed";
            ObjOPBilling.PaidAmount = parseFloat($("#txtNetAmount").val());
            ObjOPBilling.BalanceAmount = 0;
        }
    }

    SaveandUpdateOPBilling(ObjOPBilling, sMethodName);

});

function SaveandUpdateOPBilling(ObjOPBilling, sMethodName) {
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/" + sMethodName,
        data: JSON.stringify({ Objdata: ObjOPBilling }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearOPBillingTab();
                        GetRecord();
                        $("#btnAddNew").show();
                        $("#btnList").hide();
                        $("#divTab").show();
                        $("#secHeader").removeClass('hidden');
                        $("#divOPBilling").hide();
                        if (sMethodName == "AddSalesEntry") {
                            $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            $("#hdnSalesEntryID").val(objResponse.Value);
                            EditRecord($("#hdnSalesEntryID").val());
                            $("#btnSave").hide();
                            $("#btnUpdate").hide();
                            $("#btnPrintbill").show();
                            // SetSessionValue("SalesEntryID", $("#hdnSalesEntryID").val());
                            // var myWindow = window.open("PrintSalesEntryInvoice.aspx", "MsgWindow");
                            $("#btnAddMagazine").hide();
                            $("#btnUpdateMagazine").hide();
                            //SetSessionValue("SalesEntryID", $("#hdnSalesEntryID").val());
                            //var myWindow = window.open("PrintSalesEntryInvoice.aspx", "MsgWindow");
                        }
                        else if (sMethodName == "UpdateSalesEntry") {
                            $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            EditRecord(ObjOPBilling.SalesEntryID);
                            $("#btnSave").hide();
                            $("#btnUpdate").hide();
                            $("#btnPrintbill").show();
                            //  SetSessionValue("SalesEntryID", $("#hdnSalesEntryID").val());
                            //var myWindow = window.open("PrintSalesEntryInvoice.aspx", "MsgWindow");
                            $("#btnAddMagazine").hide();
                            $("#btnUpdateMagazine").hide();

                        }

                        //$("#btnAddNew").show();
                        //$("#btnList").hide();
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}


$("#btnPrintbill").click(function () {
    SetSessionValue("SalesEntryID", $("#hdnSalesEntryID").val());
    var myWindow = window.open("PrintSalesEntryInvoice.aspx", "MsgWindow");
});

function EditRecord(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetSalesEntryByID",
        data: JSON.stringify({ ID: id, IsRetail: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("#btnAddNew").click();
                            $("#btnSave").hide();
                            $("#btnUpdate").show();
                            $("#btnPrintbill").show();
                            $("#txtOtherPassword").val("");
                            $("#txtBillNo").attr("disabled", true);

                            $("#hdnSalesID").val(obj.SalesEntryID)
                            $("#hdnSalesEntryID").val(obj.SalesEntryID)
                            $("#txtBillNo").val(obj.InvoiceNo);
                            $("#txtBillDate").val(obj.sInvoiceDate);
                            if (obj.sLRDate != "01-01-1900")
                                $("#txtLRDate").val(obj.sLRDate);
                            $("#txtRoundoff").val(obj.Roundoff);
                            $("#txtNetAmount").val(obj.NetAmount);
                            $("#ddlCustomerName").val(obj.Customer.CustomerID).change();
                            $("#ddlTaxName").val(obj.Tax.TaxID).change();
                            $("#hdnstateID").val(obj.State.StateID);
                            $("#txtTCSPercent").val(obj.TCSPercent);
                            $("#txtTCSAmount").val(obj.TCSAmount);
                            $("#txtCGST").val(obj.CGSTAmount);
                            $("#txtSGST").val(obj.SGSTAmount);
                            $("#txtIGST").val(obj.IGSTAmount);
                            $("#txtDiscountPercent").val(obj.DiscountPercent);
                            $("#txtDiscountAmount").val(obj.DiscountAmount);
                           
                            $("#hdnPaidAmt").val(obj.PaidAmount);
                            $("#hdnNetAmt").val(obj.NetAmount);
                            $("#hdnBalanceAmt").val(obj.BalanceAmount);
                            $("#hdnStatus").val(obj.Status);
                            $("#txtNarration").val(obj.Narration);
                            $("#txtLRNo").val(obj.LRNo);
                            $("#txtNoofBages").val(obj.NoofBags);
                            $("#txtComments").val(obj.Notes);
                            $("#txtEWayNo").val(obj.EWayNo);
                            $("#txtTransPortName").val(obj.TransportName);
                            $("#txtVehicleNo").val(obj.VehicleNo);
                            $("#ddlPaymentMode").val(obj.PaymentMode);
                            $("#txtTransportCharge").val(obj.TransportCharges);
                            $("#txtOtherCharges").val(obj.OtherCharges);

                            $("[id*=imgUpload2_view]").css("visibility", "visible");
                            $("[id*=imgUpload2_view]").attr("src", obj.ImagePath1);
                            $("[id*=imgUpload3_view]").css("visibility", "visible");
                            $("[id*=imgUpload3_view]").attr("src", obj.ImagePath2);
                            $("[id*=imgUpload4_view]").css("visibility", "visible");
                            $("[id*=imgUpload4_view]").attr("src", obj.ImagePath3);

                            if (obj.PaymentMode == "Card") {
                                $("#divBank").show();
                                $("#txtCardNo").val(obj.CardNo);
                                $("#txtCardCharges").val(obj.CardCharges);
                                $("#ddlBankName").val(obj.Bank.LedgerID);
                            }

                            gOPBillingList = [];
                            var ObjProduct = obj.SalesEntryTrans;
                            for (var index = 0; index < ObjProduct.length; index++) {
                                var objTemp = new Object();
                                objTemp.sNO = index + 1;
                                objTemp.SNo = objTemp.sNO;
                                objTemp.StatusFlag = "U";

                                var objMagazine = new Object();
                                objMagazine.ProductID = ObjProduct[index].Product.ProductID;
                                objMagazine.ProductName = ObjProduct[index].Product.ProductName;
                                objMagazine.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Product = objMagazine;


                                var objStock = new Object();
                                objStock.StockID = ObjProduct[index].Stock.StockID;
                                objStock.BatchNo = ObjProduct[index].Stock.BatchNo;
                                objTemp.Stock = objStock;

                                var objTransTax = new Object();
                                objTransTax.TaxID = ObjProduct[index].Tax.TaxID;
                                objTransTax.TaxPercentage = ObjProduct[index].Tax.TaxPercentage;
                                objTransTax.CGSTPercent = ObjProduct[index].Tax.CGSTPercent;
                                objTransTax.SGSTPercent = ObjProduct[index].Tax.SGSTPercent;
                                objTransTax.IGSTPercent = ObjProduct[index].Tax.IGSTPercent;
                                objTemp.Tax = objTransTax;

                                objTemp.SalesEntryTransID = ObjProduct[index].SalesEntryTransID;
                                objTemp.SalesEntryID = ObjProduct[index].SalesEntryID;
                                objTemp.ProductID = ObjProduct[index].Product.ProductID;
                                objTemp.ProductName = ObjProduct[index].Product.ProductName;
                                objTemp.SMSCode = ObjProduct[index].Product.SMSCode;
                                objTemp.Quantity = ObjProduct[index].Quantity;
                                objTemp.PreviousPrice = ObjProduct[index].PreviousPrice;
                                objTemp.PurchasePrice = ObjProduct[index].PurchasePrice;
                                objTemp.SalesMargin = ObjProduct[index].SalesMargin;
                                objTemp.TaxAmount = ObjProduct[index].TaxAmount;
                                objTemp.SGSTAmount = ObjProduct[index].SGSTAmount;
                                objTemp.IGSTAmount = ObjProduct[index].IGSTAmount;
                                objTemp.CGSTAmount = ObjProduct[index].CGSTAmount;
                                objTemp.Rate = ObjProduct[index].Rate;
                                objTemp.SubTotal = ObjProduct[index].SubTotal;
                                objTemp.DiscountPercentage = ObjProduct[index].DiscountPercentage;
                                objTemp.DiscountAmount = ObjProduct[index].DiscountAmount;
                                objTemp.Barcode = ObjProduct[index].Barcode;
                                objTemp.NewProductFlag = ObjProduct[index].NewProductFlag;

                                AddOPBillingData(objTemp);
                            }
                            CalculateAmount();
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

function ShowDeleteRecord(id) {
    // DeleteRecord(id, $("#txtReason").val());
    //$("#hdnID").val("");
    //$("#btnSave").show();
    //$("#btnUpdate").hide();
    $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp; Cancel Bill");
    $('#compose-modal').modal({ show: true, backdrop: true });
    $("#txtID").val(id);
    $("#txtReason").focus();
    return false;
}

$("#btnOK").click(function () {

    if ($("#txtReason").val() == undefined || $("#txtReason").val() == null || $("#txtReason").val().trim() == "") {
        $.jGrowl("Please enter reason for cancelling", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divReason").addClass('has-error'); $("#txtReason").focus(); return false;
    }
    else { $("#divReason").removeClass('has-error'); }

    if ($("#txtPassword").val().trim() == "" || $("#txtPassword").val().trim() == undefined || $("#txtPassword").val().trim() != $("#hdRS").val()) {
        $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
        $("#divPassword").addClass('has-error'); $("#txtPassword").focus(); return false;
    } else { $("#divPassword").removeClass('has-error'); }

    DeleteRecord($("#txtID").val(), $("#txtReason").val());

});

function ClearCancelData() {
    $("#txtID").val("");
    $("#txtReason").val("");
    $('#compose-modal').modal('hide');
}

$("#txtDiscountPercent").change(function () {
    if ($("#hdnMaxsalesDiscount").val() > 0) {
        if (parseFloat($("#hdnMaxsalesDiscount").val()) < parseFloat($("#txtDiscountPercent").val())) {
            $.jGrowl(" Discount Percentage Exceeding Maximum discount", { sticky: false, theme: 'warning', life: jGrowlLife });
            $("#txtDiscountPercent").focus(); return false;
        }
    }

    var iDisPercent = parseFloat($("#txtDiscountPercent").val());
    $("#txtDisPer").val($("#txtDiscountPercent").val()).change();
    if (isNaN(iDisPercent)) iDisPercent = 0;
    var iSubtotal = 0; var iDiscAmt = 0;
    for (var i = 0; i < gOPBillingList.length; i++) {
        if (gOPBillingList[i].StatusFlag != "D") {
            gOPBillingList[i].DiscountPercentage = iDisPercent;
            gOPBillingList[i].DiscountAmount = parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate) * iDisPercent / 100;
            iSubtotal = (parseFloat(gOPBillingList[i].Quantity) * parseFloat(gOPBillingList[i].Rate)) - parseFloat(gOPBillingList[i].DiscountAmount);
            gOPBillingList[i].SubTotal = parseFloat(iSubtotal).toFixed(2);
            if ($("#hdnStateCode").val() == 33) {
                gOPBillingList[i].CGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.CGSTPercent) / 100).toFixed(2);
                gOPBillingList[i].SGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.SGSTPercent) / 100).toFixed(2);
                gOPBillingList[i].IGSTAmount = 0;
            }
            else {
                gOPBillingList[i].CGSTAmount = 0;
                gOPBillingList[i].SGSTAmount = 0;
                gOPBillingList[i].IGSTAmount = (parseFloat(iSubtotal) * parseFloat(gOPBillingList[i].Tax.IGSTPercent) / 100).toFixed(2);
            }
            gOPBillingList[i].TaxAmount = (parseFloat(gOPBillingList[i].CGSTAmount) + parseFloat(gOPBillingList[i].SGSTAmount) + parseFloat(gOPBillingList[i].IGSTAmount)).toFixed(2);
        }
    }
    DisplayOPBillingList(gOPBillingList);
    CalculateAmount();
});


function DeleteRecord(id, Reason) {

    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/DeleteSalesEntry",
        data: JSON.stringify({ ID: id, Reason: Reason }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value > 0) {
                        ClearCancelData();
                        GetRecord();
                        $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location = "frmLogin.aspx";
                    }
                    else if (objResponse.Value == "OPBilling_R_01" || objResponse.Value == "OPBilling_D_01") {
                        $.jGrowl("Since this record is referred somewhere else you cannot delete it", { sticky: false, theme: 'danger', life: jGrowlLife });
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            }
        },
        error: function (e) {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
        }
    });
    return false;
}

$("#aGeneral").click(function () {
    $("#SearchResult").hide();
    GetRecord();
});

$("#aSearchResult").click(function () {
    $("#SearchResult").show();

});

$("#btnCancel").click(function () {
    $('#compose-modal').modal('hide');
    return false;
});

$("#btnRateCancel").click(function () {
    $('#composeRate').modal('hide');
    return false;
});

$("#btndetailsCancel").click(function () {
    $('#composedetails').modal('hide');
    return false;
});

$("#btnImageCancel").click(function () {
    $('#composeImage').modal('hide');
    return false;
});

function GetProductByID(id) {
    dProgress(true);
    $.ajax({
        type: "POST",
        url: "WebServices/VHMSService.svc/GetProductByID",
        data: JSON.stringify({ ID: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d != "") {
                var objResponse = jQuery.parseJSON(data.d);
                if (objResponse.Status == "Success") {
                    if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                        var obj = jQuery.parseJSON(objResponse.Value);
                        if (obj != null) {
                            $("[id*=imgUpload1]").css("visibility", "visible");
                            $("[id*=imgUpload1]").attr("src", obj.ProductImage1);
                            $("[id*=imgUpload5]").css("visibility", "visible");
                            $("[id*=imgUpload5]").attr("src", obj.ProductImage2);
                            $("[id*=imgUpload6]").css("visibility", "visible");
                            $("[id*=imgUpload6]").attr("src", obj.ProductImage3);

                            $(".modal-title").html("&nbsp;&nbsp; Product Image");
                            $('#composeImage').modal({ show: true, backdrop: true });
                        }
                        dProgress(false);
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                        dProgress(false);
                    }
                    else if (objResponse.Value == "Error") {
                        $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
                else if (objResponse.Status == "Error") {
                    if (objResponse.Value == "0") {
                        window.location("frmLogin.aspx");
                    }
                    else if (objResponse.Value == "Error") {
                        window.location = "frmErrorPage.aspx";
                    }
                    else if (objResponse.Value == "NoRecord") {
                        $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                    }
                }
            }
            else {
                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                dProgress(false);
            }
        },
        error: function () {
            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
            dProgress(false);
        }
    });
    return false;
}

$("#btnCustomerAdd").click(function () {

    return false;
});