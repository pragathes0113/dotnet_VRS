﻿<%@ Page Title="Customer Report" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmCustomerReport.aspx.cs" Inherits="frmCustomerReport" %>


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
            <h1>Customer Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Customer Report</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <div class="box-body">
                <div class="table-responsive">
                    <table border="0" class="table table-striped table-bordered table-responsive dTableR">
                        <div class="box box-warning box-solid" id="divFilter">
                            <div class="box-header with-border">
                                <div class="box-title">
                                    Filter Options
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="form-group col-md-2" id="divCustomer">
                                        <label>
                                            Customer Type</label><span class="text-danger">*</span>
                                        <asp:DropDownList ID="ddlCustomer" OnSelectedIndexChanged="ddlProduct1_SelectedIndexChanged"
                                            AutoPostBack="false" Width="200" Height="30" runat="server"  CssClass="select2">
                                            <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                            <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                            <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                            <asp:ListItem Text="CI" Value="CI"></asp:ListItem>
                                            <asp:ListItem Text="DI" Value="DI"></asp:ListItem>
                                            <%--<asp:ListItem Text="VIP" Value="VIP"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-1">
                                        <asp:Button ID="BtnView" Text="View" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnView_Click" runat="server" />
                                    </div>
                                    <div class="form-group col-md-1">
                                        <asp:Button ID="btnprint" Text="print" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClientClick="doPrint()" runat="server" />
                                    </div>
                                    <div class="form-group col-md-1">
                                        <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnExcel_Click" runat="server" />
                                    </div>
                                    <div class="form-group col-md-1">
                                        <asp:Button ID="Pdf" Text="Pdf" CausesValidation="false" CssClass="btn btn-info" Style="margin-top: 25PX; height: 30px; width: 60px; padding-top: 4PX;" OnClick="btnPDf_Click" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="divRecords">
                            <asp:GridView ID="gvProductMas" runat="server" Caption="Customer Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                                AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_CustomerID"
                                EmptyDataText="No Records Found" AllowSorting="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="CustomerName" ReadOnly="true" DataField="CustomerName" />
                                    <asp:BoundField HeaderText="CustomerType" ReadOnly="true" DataField="CustomerType" />
                                    <asp:BoundField HeaderText="Address" ReadOnly="true" DataField="Address" />
                                    <asp:BoundField HeaderText="MobileNo" ReadOnly="true" DataField="MobileNo" />
                                    <asp:BoundField HeaderText="AlternateNo" ReadOnly="true" DataField="AlternateNo" />
                                    <asp:BoundField HeaderText="GST No" ReadOnly="true" DataField="GSTNo" />
                                </Columns>
                                <PagerStyle CssClass="cssPager" HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </table>
                </div>
            </div>
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
