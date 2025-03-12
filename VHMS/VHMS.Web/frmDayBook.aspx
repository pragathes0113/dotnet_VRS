﻿<%@ Page Title="Day Book" Language="C#" AutoEventWireup="true" MasterPageFile="~/VHMSMasterPage.master" CodeFile="frmDayBook.aspx.cs" Inherits="frmDayBook" %>


<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
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
    <link href="css/print.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">

    <div class="container-wrapper">
        <section class="content-header">
            <h1>Day Book
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Day Book</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <section class="content">
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
                                       <asp:HiddenField ID="hdnDaybook" runat="server" />
                                    From</label><span class="text-danger">*</span>
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOB"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDOB" AutoComplete="off" runat="server" Width="150" Height="30"></asp:TextBox>
                                   <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
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
                            <div class="form-group col-md-2" id="divCustomer">
                                <label>
                                    Type</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlCustomer" Width="200" Height="30" runat="server"  CssClass="select2">
                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="Purchase"></asp:ListItem>
                                    <asp:ListItem Text="Purchase Return" Value="Purchase Return"></asp:ListItem>
                                    <asp:ListItem Text="Sales" Value="Sales"></asp:ListItem>
                                    <asp:ListItem Text="Sales Return" Value="Sales Return"></asp:ListItem>
                                    <asp:ListItem Text="Payment" Value="Payment"></asp:ListItem>
                                    <asp:ListItem Text="Vendor Payment" Value="Vendor Payment"></asp:ListItem>
                                    <asp:ListItem Text="Receipt" Value="Receipt"></asp:ListItem>
                                    <asp:ListItem Text="Expense" Value="Expense"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-1">
                                <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView_Click" runat="server" />
                            </div>
                            <div class="form-group col-md-1">
                                <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClientClick="doPrint()" runat="server" />
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
                <div class="content">
                    <div id="divPdf" runat="server" style="text-align: left;">
                        <div style="background-color: white;" id="divOPInvoice1" runat="server">
                        </div>
                    </div>
                </div>

            </section>
        </section>
    </div>

    <script type="text/javascript">
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
   <%-- <script>
        function doPrint() {
            var prtContent = document.getElementById('<%=   divPdf.ClientID   %>');
            prtContent.border = 1; //set no border here
            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
            WinPrint.document.write('<link rel="stylesheet" href="css/print.css" type="text/css" />');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }

    </script>--%>
    <script>        function doPrint() {            var panel = document.getElementById("<%=divPdf.ClientID %>");            var printWindow = window.open('', '', '');            printWindow.border = 1;            printWindow.document.write('<html><head><title>Print Invoice</title>');            // Make sure the relative URL to the stylesheet works:            printWindow.document.write('<base href="' + location.origin + location.pathname + '">');            // Add the stylesheet link and inline styles to the new document:            printWindow.document.write('<link rel="stylesheet" href="css/print.css">');            printWindow.document.write('<style type="text/css">print.css{width: 100%;}</style>');            printWindow.document.write('</head><body >');            printWindow.document.write(panel.innerHTML);            printWindow.document.write('</body></html>');            setTimeout(function () {                printWindow.print();            }, 10);            return false;        }        printWindow.document.close();        </script>
</asp:Content>
