<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRViewEmployees.aspx.cs" Inherits="HRViewEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <link href="Scripts/css/addons/datatables.min.css" rel="stylesheet">
    <script type="text/javascript" src="Scripts/js/addons/datatables.min.js"></script>

    <div>
        <div class="form-group">
            <label class="col-form-label" for="FilterEmployees">Filter Employees</label>
            <asp:DropDownList CssClass="form-control col-2" runat="server" ValidationGroup="vg" ID="FilterEmployees" AutoPostBack="true">
                <asp:ListItem Text="Active" Value="active" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inactive" Value="inactive"></asp:ListItem>
                <asp:ListItem Text="Not Verified" Value="notverified"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <hr />
         <input class="form-control col-3" autocomplete="off" id="search" type="text" placeholder="Search here,.." />
        <br />
        <asp:Table ID="EmployeeTable" runat="server" CssClass="table table-bordered table-sm" CellSpacing="0" Width="100%">
            
        </asp:Table>
    </div>
    <style>
        table.dataTable thead .sorting:after,
        table.dataTable thead .sorting:before,
        table.dataTable thead .sorting_asc:after,
        table.dataTable thead .sorting_asc:before,
        table.dataTable thead .sorting_asc_disabled:after,
        table.dataTable thead .sorting_asc_disabled:before,
        table.dataTable thead .sorting_desc:after,
        table.dataTable thead .sorting_desc:before,
        table.dataTable thead .sorting_desc_disabled:after,
        table.dataTable thead .sorting_desc_disabled:before {
            bottom: .5em;
        }
    </style>
    <script>
        $(document).ready(function () {
            $("#search").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("tr.r").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
</asp:Content>


