<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRViewJobPostings.aspx.cs" Inherits="HRViewJobPostings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <link href="Scripts/css/addons/datatables.min.css" rel="stylesheet">
    <script type="text/javascript" src="Scripts/js/addons/datatables.min.js"></script>

    <div>
        <div class="row">
            <div class="form-group col-6">
                <label class="col-form-label" for="FilterJobs">Filter Job Postings</label>
                <asp:DropDownList CssClass="form-control col-8" runat="server" ValidationGroup="vg" ID="FilterJobs" AutoPostBack="true">
                    <asp:ListItem Text="No Filter" Value="none" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Intern/Entry Level" Value="entry"></asp:ListItem>
                    <asp:ListItem Text="Experienced Professional" Value="professional"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-6">
                <label class="col-form-label" for="FilterStatus">Filter Job Status</label>
                <asp:DropDownList CssClass="form-control col-8" runat="server" ValidationGroup="vg" ID="FilterStatus" AutoPostBack="true">
                    <asp:ListItem Text="No Filter" Value="none" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Open" Value="open"></asp:ListItem>
                    <asp:ListItem Text="Closed" Value="closed"></asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>

        <hr />
        <input class="form-control col-3" autocomplete="off" id="search" type="text" placeholder="Search here,.." />
        <br />
        <asp:Table ID="JobTable" runat="server" CssClass="table" CellSpacing="0" Width="100%">
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

