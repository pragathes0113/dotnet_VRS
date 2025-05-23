﻿<%@ Page Title="Buyer Ledger Summary Report" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="frmCustomerLedgerSummary.aspx.cs" Inherits="frmCustomerLedgerSummary" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style type="text/css">
        #VHMSWebContent_CRDischargeSummaryReport__UI_mb, #VHMSWebContent_CRDischargeSummaryReport__UI_bc {
            height: inherit !important;
            top: 0px !important;
            left: 0px !important;
        }

        .BigCheckBox input {
            width: 25px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnAdmissionID" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>Buyer Ledger Summary Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Buyer Ledger Summary Report</li>
            </ol>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
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
                                     <%--   <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
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
                                      <%--  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOR"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />--%>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="Branch">
                                    <label>
                                        Buyer</label><span class="text-danger">*</span>
                                    <asp:DropDownList ID="ddlCustomer" Width="200" Height="30" runat="server" CssClass="select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" style="display: none;">
                                    <label>
                                        Report Type</label>
                                    <asp:DropDownList ID="ddluser" runat="server" data-placeholder="Select User"
                                        TabIndex="3" CssClass="select2">
                                        <asp:ListItem Value="Summary" Text="Summary"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnView" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1" style="display:none;">
                                    <asp:Button ID="Print" Text="print" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnPrint_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
                        Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="True"
                        HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);

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

            pLoadingSetup(true);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.select2').select2({ theme: 'bootstrap' });
            });
        });

    </script>
    <style>
        .MyCalendar {
            background-color: white !important;
        }
    </style>
</asp:Content>
