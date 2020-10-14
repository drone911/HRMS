<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRViewEmployees.aspx.cs" Inherits="HRViewEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <div>
        <div class="form-group">
            <label class="col-form-label" for="FilterEmployees">Filter Employees</label>
            <asp:DropDownList CssClass="form-control col-4" runat="server" ValidationGroup="vg" ID="FilterEmployees" AutoPostBack="true">
                <asp:ListItem Text="Active" Value="active" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inactive" Value="inactive"></asp:ListItem>
                <asp:ListItem Text="Not Verified" Value="notverified"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <dx:ASPxGridView ID="EmployeeGrid" runat="server" OnRowDeleting="EmployeeGrid_RowDeleting"></dx:ASPxGridView>
    </div>
</asp:Content>


