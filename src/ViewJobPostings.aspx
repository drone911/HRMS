<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="ViewJobPostings.aspx.cs" Inherits="HRViewJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <div>
        <div class="row">
            <div class="form-group col-6">
                <label class="col-form-label" for="FilterJobs">Filter Cities</label>
                <asp:DropDownList CssClass="form-control col-8" runat="server" ValidationGroup="vg" ID="FilterCities" AutoPostBack="true">
                    <asp:ListItem Text="No Filter" Value="none" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Intern/Entry Level" Value="entry"></asp:ListItem>
                    <asp:ListItem Text="Experienced Professional" Value="professional"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-6">
                <label class="col-form-label" for="FilterStatus">Filter Experience</label>
                <asp:DropDownList CssClass="form-control col-8" runat="server" ValidationGroup="vg" ID="FilterExperience" AutoPostBack="true">
                    <asp:ListItem Text="No Filter" Value="none" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
</asp:Content>

