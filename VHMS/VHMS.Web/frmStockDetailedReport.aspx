﻿<%@ Page Title="Stock Detailed Report" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmStockDetailedReport.aspx.cs" Inherits="frmStockDetailedReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
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
            <h1>Stock Detailed Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">>Stock Detailed Report</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <section class="content">

                <table border="0" class="table table-striped table-bordered table-responsive dTableR">
                    <div class="box box-warning box-solid" id="divFilter">
                        <div class="box-header with-border">
                            <div class="box-title">
                                Filter Options
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-4" id="divProduct">
                                    <label>
                                        Prodtct</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlProduct" Width="200" Height="30" runat="server" CssClass="select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3" id="divSupplier">
                                    <label>
                                        Supplier</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlSupplier" Width="200" Height="30" runat="server" CssClass="select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="BtnView" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClientClick="doPrint();return false;" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnAddNew" Text="Excel" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnAddNew_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnPDF_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </table>

                <div class="row" id="divRecords">
                    <asp:GridView ID="gvProductMas" runat="server" Caption="StockDetailed Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                        AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_StockID"
                        EmptyDataText="No Records Found" OnRowDataBound="GridView2_RowDataBound" ShowFooter="false" AllowSorting="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="ID" ReadOnly="true" DataField="FK_ProductID" />
                            <asp:BoundField HeaderText="ProductName" ReadOnly="true" DataField="ProductName" />
                            <asp:BoundField HeaderText="Purchase Quantity" ReadOnly="true" DataField="InQuantity" DataFormatString="{0:N0}" HtmlEncode="false" />
                            <asp:BoundField HeaderText="Sales Quantity" ReadOnly="true" DataField="OutQuantity" DataFormatString="{0:N0}" HtmlEncode="false" />
                            <asp:BoundField HeaderText="Available Quantity" ReadOnly="true" DataField="Quantity" DataFormatString="{0:N0}" HtmlEncode="false" />
                           <%-- <asp:BoundField HeaderText="Selling Rate" ReadOnly="true" DataField="SellingRate" DataFormatString="{0:N0}" HtmlEncode="false" />
                            <asp:BoundField HeaderText="Stock Value" ReadOnly="true" DataField="productvalue" DataFormatString="{0:N0}" HtmlEncode="false" />--%>

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
        });
    </script>
    <style>
        .MyCalendar {
            background-color: white !important;
        }
    </style>
    <script>
        function doPrint() {
            var prtContent = document.getElementById('<%=    gvProductMas.ClientID   %>');
            prtContent.border = 1; //set no border here
            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
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


