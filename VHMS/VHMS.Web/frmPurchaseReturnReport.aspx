﻿<%@ Page Title="Purchase Return Report" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmPurchaseReturnReport.aspx.cs" Inherits="frmPurchaseReturnReport" %>


<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <link rel="stylesheet" href="../lib/jBreadcrumbs/css/BreadCrumb.css" />
    <script src="../js/xpspopup.js" type="text/javascript"></script>
    <%--<style>
        div.dt-buttons {
            float: right !important;
            margin-left: 10px !important;
        }
    </style>

    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.1.0/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.10/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.1.0/css/select.dataTables.min.css" type="text/css" />--%>
    <style>
        .stylink {
            color: black !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hiSalesID" runat="server" />
    <div class="container-wrapper">
        <section class="content-header">
            <h1>Purchase Return Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Purchase Return Report</li>
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
                                <div class="form-group col-md-2 col-sm-4" id="divDOB">
                                    <label>
                                        From</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOB"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDOB" AutoComplete="off" runat="server" Width="150" Height="30"></asp:TextBox>
                                        <%--  <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />--%>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divDOR">
                                    <label>
                                        To</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOR"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDOR" AutoComplete="off" runat="server" Width="150" Height="30"></asp:TextBox>
                                        <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOR"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />--%>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="Branch">
                                    <label>
                                        Supplier</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlSupplier" CssClass="select2" Width="200" Height="30" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" style="display:none">
                                    <label>
                                        Report Type</label>
                                    <asp:DropDownList ID="ddluser" runat="server" data-placeholder="Select User"
                                        TabIndex="3" CssClass="select2">
                                        <asp:ListItem Value="Summary" Text="Summary"></asp:ListItem>
                                        <asp:ListItem Value="Detailed" Text="Detailed"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClientClick="doPrint(); return false;" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnExcel_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnPDF_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divRecords">
                        <asp:GridView ID="gvPurchaseReturn" runat="server" Caption="Purchase Return Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_PurchaseReturnID"
                            EmptyDataText="No Records Found" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="gvManageSales_RowCommand" ShowFooter="true" AllowSorting="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Return No" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbStart" Text='<%# ""+Eval("ReturnNo").ToString()%>' CssClass="stylink" CommandArgument='<%# Eval("PK_PurchaseReturnID") %>' runat="server" CommandName="Delete">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField HeaderText="ReturnNo" ReadOnly="true" DataField="ReturnNo" />
                                <asp:BoundField HeaderText="ReturnDate" ReadOnly="true" DataField="ReturnDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Bill No" ReadOnly="true" DataField="BillNo" />
                                <asp:BoundField HeaderText="Bill Date" ReadOnly="true" DataField="BillDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="SupplierName" ReadOnly="true" DataField="SupplierName" />
                                <asp:BoundField HeaderText="Phone No" ReadOnly="true" DataField="PhoneNo1" />
                                <asp:BoundField HeaderText="Total Quantity " ReadOnly="true" DataField="TotalQuantity" />
                                <asp:BoundField HeaderText="Total Amount" ReadOnly="true" DataField="TotalAmount" />
                                <asp:BoundField HeaderText="Discount Amount" ReadOnly="true" DataField="DiscountAmount" />
                                <asp:BoundField HeaderText="Tax Amount" ReadOnly="true" DataField="TaxAmount" />
                                <asp:TemplateField HeaderText="InvoiceAmount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Style="width: 10px;" Text='<%# ""+Eval("NetAmount").ToString()%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="cssPager" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                    <div class="row" id="divRecordsDetailed">
                        <asp:GridView ID="gvDetailed" runat="server" Caption="Purchase Detailed Return Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_PurchaseReturnTransID"
                            EmptyDataText="No Records Found" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="gvManageSales_RowCommand" ShowFooter="true" AllowSorting="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Return No" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbStart" Text='<%# ""+Eval("ReturnNo").ToString()%>' CssClass="stylink" CommandArgument='<%# Eval("PK_PurchaseReturnID") %>' runat="server" CommandName="Delete">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField HeaderText="Return No" ReadOnly="true" DataField="ReturnNo" />
                                <asp:BoundField HeaderText="Return Date" ReadOnly="true" DataField="ReturnDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Bill No" ReadOnly="true" DataField="BillNo" />
                                <asp:BoundField HeaderText="Bill Date" ReadOnly="true" DataField="BillDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Product Name" ReadOnly="true" DataField="ProductName" />
                                <asp:BoundField HeaderText="Code" ReadOnly="true" DataField="SMSCode" />
                                <asp:BoundField HeaderText="Batch No" ReadOnly="true" DataField="BatchNo" />
                                <asp:BoundField HeaderText="Quantity" ReadOnly="true" DataField="Quantity" />
                                <asp:BoundField HeaderText="Rate" ReadOnly="true" DataField="Rate" />
                                <asp:BoundField HeaderText="Disc. Amount" ReadOnly="true" DataField="DiscountAmount" />
                                <asp:BoundField HeaderText="Tax Percent " ReadOnly="true" DataField="TaxPercent" />
                                <asp:BoundField HeaderText="Tax Amount" ReadOnly="true" DataField="TaxAmount" />
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Style="width: 10px;" Text='<%# ""+Eval("SubTotal").ToString()%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="cssPager" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </table>
            </section>
        </section>
    </div>
    <script type="text/javascript">
        document.onkeydown = function () {
            if (event.keyCode == 113) {
                var myWindow = window.open("frmDPurchaseReturnReport.aspx", "_self");
            }

        };
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            $("input[id*=txtDOB]").attr("data-link-format", "dd/MM/yyyy");
            $('input[id*=txtDOB]').datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            $("input[id*=txtDOR]").attr("data-link-format", "dd/MM/yyyy");
            $('input[id*=txtDOR]').datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
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

            if (document.getElementById('<%= ddluser.ClientID %>').value == "Summary")
                var prtContent = document.getElementById('<%=   gvPurchaseReturn.ClientID   %>');
            else
                var prtContent = document.getElementById('<%=   gvDetailed.ClientID   %>');
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
    <style>
        .footer {
            position: relative;
            height: 5em;
            margin-bottom: -5em;
            /* bottom:3px;
    z-index:05px;
    margin-top:-5em; */
            background-image: url('bg_footer111.gif');
            background-repeat: no-repeat;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: bold;
            color: #FFFFFF;
            left: 5px;
            right: -5px;
            width: 982px;
        }
    </style>
</asp:Content>


