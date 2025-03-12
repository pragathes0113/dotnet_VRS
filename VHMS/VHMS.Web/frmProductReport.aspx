﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VHMSMasterPage.master" CodeFile="frmProductReport.aspx.cs" Inherits="frmProductReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        @media (max-width:600px) {
            .fixed .main-header, .content-header > h1, .content-header > .breadcrumb, .content-header br {
                display: none;
                visibility: hidden;
            }

            .fixed .content-wrapper {
                padding-top: 0px;
            }


            .box.box-warning.box-solid {
                height: auto !important;
            }

            .box-body .btn-group {
                margin: 0px !important;
                padding: 0px !important;
            }

            .box-body .btn-info {
                margin-left: 15px !important;
                float: left;
                padding: 5px 0px;
                width: 58px;
            }
        }

        .lblTotal {
            text-align: right;
            font-weight: bold;
            display: block;
            font-size: 16px;
        }

        .table-responsive {
            overflow: hidden;
            overflow-x: scroll;
            -webkit-overflow-scrolling: touch;
        }

        .MyCalendar {
            z-index: 100000;
        }
    </style>
    <%--<style>
        div.dt-buttons {
            float: right !important;
            margin-left: 10px !important;
        }
    </style>

    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.1.0/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.10/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.1.0/css/select.dataTables.min.css" type="text/css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">

    <div class="container-wrapper">
        <section class="content-header">
            <h1>Product Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Product Report</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <section class="content">
                <div class="box box-warning box-solid" style="width: 100%;" id="divFilter">
                    <div class="box-header with-border">
                        <div class="box-title">
                            Filter Options
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">

                            <div class="form-group col-md-2" style="margin-left: -5px;" id="divCategory">
                                <label>
                                    Category</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlCategory" AutoPostBack="false" Style="width: 200px; height: 30px;" runat="server" CssClass="select2">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-2" style="margin-left: -5px; display:none;" id="divSubCategory">
                                <label>
                                    SubCategory</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlSubCategory" AutoPostBack="false" Style="width: 200px; height: 30px;" runat="server" CssClass="select2">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-3" style="margin-left: -5px; display:none;" id="divSupplier">
                                <label>
                                    Supplier</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlSupplier" AutoPostBack="false" Style="width: 200px; height: 30px;" runat="server" CssClass="select2">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-2" id="divCode" style="display:none;">
                                <label>
                                    SMSCode/PartyCode</label><span class="text-danger">*</span>
                                <asp:TextBox ID="txtCode" runat="server" placeholder="Code" class="form-control TRSearch"
                                    OnTextChanged="tbAccount_TextChanged" AutoPostBack="false"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3" id="divProduct">
                                    <label>Product</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlProduct" CssClass="select2" Width="200" Height="30" runat="server">
                                    </asp:DropDownList>
                                </div>
                             <div class="form-group col-md-2" id="divPaymentMode">
                                    <label>
                                        Type</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlPaymentMode" AutoPostBack="false" Width="200" Height="30" runat="server" CssClass="select2">
                                        <asp:ListItem Text="Product Wise" Value="Product Wise"></asp:ListItem>
                                        <asp:ListItem Text="HSN Wise" Value="HSN Wise"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            <div class="form-group col-md-1 btn-group" style="margin-top: 20px;">
                                <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" OnClick="btnView_Click" runat="server" />
                            </div>
                            <div class="form-group col-md-1 btn-group" style="margin-top: 20px;">
                                <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" OnClientClick="doPrint();return false;" runat="server" />
                            </div>
                            <div class="form-group col-md-1 btn-group" style="margin-top: 20px;">
                                <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false" CssClass="btn btn-info" OnClick="btnExcel_Click" runat="server" />
                            </div>
                            <div class="form-group col-md-1 btn-group" style="margin-top: 20px;">
                                <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false" CssClass="btn btn-info" OnClick="btnPDF_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row table-responsive" id="divRecords">
                    <asp:Label ID="lblTotal" CssClass="lblTotal" runat="server" />
                    <asp:GridView ID="gvReceipt" runat="server" Caption="Product Report" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                        AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_ProductID"
                        EmptyDataText="No Records Found" ShowFooter="false" >
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Category" ReadOnly="true" DataField="CategoryName" />
                            <asp:BoundField HeaderText="Code" ReadOnly="true" DataField="SMSCode" />
                            <asp:BoundField HeaderText="Product" ReadOnly="true" DataField="ProductName" />
                            <asp:BoundField HeaderText="HSNCode" ReadOnly="true" DataField="HSNCode" />
                            <asp:BoundField HeaderText="Brand Name" ReadOnly="true" DataField="BrandName" />
                            <asp:BoundField HeaderText="PurchasePrice" ReadOnly="true" DataField="PurchasePrice" />
                            <asp:BoundField HeaderText="SalesPrice" ReadOnly="true" DataField="SalesPrice" />
                            <asp:BoundField HeaderText="C_Disc %" ReadOnly="true" DataField="C_DiscountPercent" />
                            <asp:BoundField HeaderText="D_Disc %" ReadOnly="true" DataField="D_DiscountPercent" />
                            <asp:BoundField HeaderText="CI_Disc %" ReadOnly="true" DataField="CI_DiscountPercent" />
                            <asp:BoundField HeaderText="DI_Disc %" ReadOnly="true" DataField="DI_DiscountPercent" />
                        </Columns>
                        <PagerStyle CssClass="cssPager" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </section>
        </section>
    </div>

    <script type="text/javascript">
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$("#txtDOB,#txtDOR").attr("data-link-format", "dd/MM/yyyy");
            //$("#txtDOB,#txtDOR").datetimepicker({
            //    pickTime: false,
            //    useCurrent: true,
            //    format: 'DD/MM/YYYY'
            //});
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });
            $("[id$=txtCode]").change(function () {
                $("[id$=txtCode]").val(($("[id$=txtCode]").val().split('|')[0]).trim());
            });
            $("[id$=txtCode]").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetSMSCode") %>',
                        data: "{ 'prefix': '" + request.term + "','SupplierID':0,'IsAll':'A'}",
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
    </script>
    <style>
        .MyCalendar {
            background-color: white !important;
        }
    </style>
    <script>
        function doPrint() {
            var prtContent = document.getElementById('<%=   gvReceipt.ClientID   %>');
            prtContent.border = 1; //set no border here
            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,border=1,status=0,resizable=1');
            WinPrint.document.write(prtContent.outerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>
    <style>
        .table-bordered > thead > tr > th,
        .table-bordered > tbody > tr > th,
        .table-bordered > tfoot > tr > th,
        .table-bordered > thead > tr > td,
        .table-bordered > tbody > tr > td,
        .table-bordered > tfoot > tr > td {
            border: 1px solid #000 !important;
        }
    </style>
</asp:Content>

