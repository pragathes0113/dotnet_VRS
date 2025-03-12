﻿<%@ Page Title="Print Product Payment" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="PrintProductPaymentInvoice.aspx.cs" Inherits="PrintProductPaymentInvoice" %>

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
            width: 15px;
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnBillNo" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>Print Product Payment
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Transaction</a></li>
                <li><a href="#">Sales</a></li>
                <li class="active">Print Product Payment</li>
            </ol>
        </div>
        <div style="margin-left: 5%">
           <%-- <asp:CheckBox ID="chkOriginal" runat="server" Text="Original" Checked="true" CssClass="BigCheckBox"
                Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="chkDuplicate" runat="server" Text="Duplicate" Checked="true" CssClass="BigCheckBox"
                Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="chkTransport" runat="server" Text="Transport" Checked="true" CssClass="BigCheckBox"
                Visible="false" />
            <asp:Button ID="btnExportReport" runat="server" Text="Print Report" TabIndex="4"
                CssClass="btn btn-danger margin btn-sm" OnClick="btnPrint_Click" />
            <asp:Button ID="btnPrintDuplicate" runat="server" Text="Print Duplicate" TabIndex="4"
                CssClass="btn btn-danger margin btn-sm" OnClick="btnPrintDuplicate_Click" />
            <asp:Button ID="btnPrintOffice" runat="server" Text="Print Office Copy" TabIndex="4"
                CssClass="btn btn-danger margin btn-sm" OnClick="btnPrintOffice_Click" />
            <asp:Button ID="btnPrintTransport" runat="server" Text="Print Transport" TabIndex="4"
                CssClass="btn btn-danger margin btn-sm" OnClick="btnPrintTransport_Click" />
            <asp:LinkButton ID="btnCustomerMail" CssClass="btn btn-success margin btn-sm"  runat="server" TabIndex="4" OnClick="btnCustomerMail_Click">
                    <span class='fa fa-envelope'/> Send Mail to Customer 
            </asp:LinkButton>
            <asp:LinkButton ID="btnAgentMail" CssClass="btn btn-success margin btn-sm"  runat="server" TabIndex="4" OnClick="btnAgentMail_Click">
                    <span class='fa fa-envelope'/> Send Mail to Agent 
            </asp:LinkButton>--%>
            <%--<asp:Button ID="btnCustomerMail" runat="server" Text="Send Mail to Customer" TabIndex="4"
                CssClass="btn btn-success margin btn-sm" OnClick="btnPrint_Click" />--%>
           <%-- <asp:Button ID="btnAgentMail" runat="server" Text="Send Mail to Agent" TabIndex="4"
                CssClass="btn btn-success margin btn-sm" OnClick="btnPrint_Click" />--%>
        </div>
        <CR:CrystalReportViewer ID="CRDischargeSummaryReport" ToolPanelView="None" runat="server"
            Width="100%" HasCrystalLogo="False" HasDrillUpButton="False" HasDrilldownTabs="False"
            HasToggleGroupTreeButton="False" />
      <%--  <CR:CrystalReportViewer ID="CRDuplicate" ToolPanelView="None" runat="server" Width="100%"
            HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
            HasPrintButton="False" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
        <CR:CrystalReportViewer ID="CROffice" ToolPanelView="None" runat="server" Width="100%"
            HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
            HasPrintButton="False" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
        <CR:CrystalReportViewer ID="CRTransport" ToolPanelView="None" runat="server" Width="100%"
            HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
            HasPrintButton="False" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />--%>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);

            pLoadingSetup(true);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.select2').select2({ theme: 'bootstrap' });
            });
        });


    </script>
    <table>
    </table>
</asp:Content>
