﻿<%@ Page Title="Print Payment" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="PrintPayment.aspx.cs" Inherits="PrintPayment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <link href="css/print.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnBillNo" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>Print Payment
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Transaction</a></li>
                <li><a href="#">Payment</a></li>
                <li class="active">Print Payment</li>
            </ol>
            <div class="pull-right">
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-danger pull-left" Text="Print" OnClick="btnPrint_Click" />
            </div>
            <br />
            <br />
        </div>
        <div class="content">
            <div id="divPdf" runat="server" style="text-align: left;">
                <div style="background-color: white; margin-left: 115px;" id="divOPInvoice" runat="server"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);

            pLoadingSetup(true);

        });
    </script>
    <table>

    </table>
</asp:Content>



