<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRAddTraining.aspx.cs" Inherits="HRAddTraining" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>

    <div class="w-100">
        <div class="jumbotron col-10 mx-auto" style="padding-left: 5rem; padding-right: 5rem; margin: auto;">
            <h1 class="display-4">Add Training</h1>
            <hr class="my-4" />
            <asp:Label ID="alertlabel" runat="server" Visible="false"></asp:Label>
            <br />
            <div class="form-group">
                <asp:Label runat="server" CssClass="pr-2">Training Name</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="NameTextbox" ValidationGroup="vg" required></asp:TextBox>
                <asp:Label runat="server" ID="Name"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label runat="server" CssClass="pr-2">Training Description</asp:Label>
                <asp:TextBox TextMode="MultiLine" CssClass="form-control" runat="server" ID="DescriptionTextbox" ValidationGroup="vg"></asp:TextBox>
            </div>
            <div class="row" style="justify-content: space-between">
                <div class="form-group col-5">
                    <label for="StartDateCalender" style="padding-bottom: 5px; width: 100vw">Start Date</label>
                    <asp:TextBox runat="server" ValidationGroup="vg" ID="StartDateCalender" required Text="Select Date" class="form-control" type="text" placeholder="Select Date.." />
                    <asp:Label ID="StartDateLabel" runat="server" CssClass="invalid-input"></asp:Label>
                    <asp:CompareValidator runat="server" ControlToValidate="StartDateCalender" CssClass="invalid-input" ErrorMessage="*Select a date" Font-Strikeout="False" Display="Dynamic" ValidationGroup="vg" ValueToCompare="Select Date" Operator="NotEqual"></asp:CompareValidator>
                </div>

                <div class="form-group col-5">
                    <label for="EndDateCalender" style="padding-bottom: 5px; width: 100vw">End Date</label>
                    <asp:TextBox runat="server" ValidationGroup="vg" ID="EndDateCalender" required Text="Select Date" class="form-control" type="text" placeholder="Select Date.." />
                    <asp:Label ID="EndDateLabel" runat="server" CssClass="invalid-input"></asp:Label>
                    <asp:CompareValidator runat="server" ControlToValidate="EndDateCalender" CssClass="invalid-input" ErrorMessage="*Select a date" Font-Strikeout="False" Display="Dynamic" ValidationGroup="vg" ValueToCompare="Select Date" Operator="NotEqual"></asp:CompareValidator>
                </div>
            </div>
        </div>
        <div class="jumbotron col-10 mx-auto" style="padding-left: 5rem; padding-right: 5rem; margin-top:50px">

            <h1 class="display-4">Add Available Employees To Training</h1>
            <hr class="my-4" />
            <asp:Table ID="SelectionTable" runat="server" class="table">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Position</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Add or Remove</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <hr class="my-4" />
            
            <asp:Button runat="server" ID="AddTraining" CssClass="btn btn-primary btn-lg mb-3" ValidationGroup="vg" Text="Add Training" OnClick="AddTraining_Click" />
            
        </div>
        <br />
        <br />
    </div>
    <style>
        textarea.form-control {
            height: 15rem;
        }

        span.alert {
            padding: 0.7rem 1rem;
            display: block
        }
    </style>
    <script>
        flatpickr("#<%= StartDateCalender.ClientID %>", {
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "Y-m-d",
        });
        flatpickr("#<%= EndDateCalender.ClientID %>", {
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "Y-m-d",
        });

    </script>
    </div>
</asp:Content>

