<%@ Page Language="C#" AutoEventWireup="false" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <link href="Scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="Scripts/js/jquery-3.3.1.min.js"></script>
    <script src="Scripts/js/popper.min.js"></script>
    <script src="Scripts/js/bootstrap.min.js"></script>

</head>
<body style="padding: 10px">
    <style>
        .invalid-input {
            color: red;
            font-size: small;
            padding-left: 2px
        }
    </style>
    <div class="container col-8 align-content-center">
        <form id="form1" runat="server">
                <div><asp:Label ID="AlertLabel" runat="Server"  CssClass="row alert alert-primary" Visible="false" role="alert"></asp:Label></div>
            <div class="form-group form-row">
                
                <label class="col-form-label" for="RoleInput">What role do you want to register for?</label>
                <asp:DropDownList ID="RoleInput" CssClass="form-control" runat="server" ValidationGroup="vg">
                    <asp:ListItem Enabled="False" Value="-1">Select Role</asp:ListItem>
                    <asp:ListItem Value="hr">Human Resource Manager</asp:ListItem>
                    <asp:ListItem Value="simpleuser">User/Employee</asp:ListItem>
                    <asp:ListItem Value="moderator">Moderator</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group form-row">
                <label class="col-form-label" for="FirstNameInput">First Name</label>
                <asp:TextBox runat="server" type="text" CssClass="form-control" ID="FirstNameInput" ValidationGroup="vg" required />
                <asp:RegularExpressionValidator runat="server" CssClass="invalid-input" ErrorMessage="*Invalid Input" ControlToValidate="FirstNameInput" ValidationExpression="^[a-zA-Z]+$" ValidationGroup="vg"></asp:RegularExpressionValidator>
            </div>

            <div class="form-group form-row">
                <label class="col-form-label" for="LastNameInput">Last Name</label>
                <asp:TextBox runat="server" type="textt" CssClass="form-control" ID="LastNameInput" ValidationGroup="vg" required />
                <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" ErrorMessage="*Invalid Input" ControlToValidate="LastNameInput" ValidationExpression="^[a-zA-Z]+$" ValidationGroup="vg" Display="Dynamic"></asp:RegularExpressionValidator>

            </div>
            <div class="form-group form-row">
                <label class="col-form-label" for="EmailInput">Email</label>
                <asp:TextBox runat="server" type="email" CssClass="form-control" ID="EmailInput" required ValidationGroup="vg" />
                <asp:Label ID="EmailLabel" runat="server" CssClass="invalid-input"></asp:Label>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="*Enter a valid email" CssClass="invalid-input" ValidationGroup="vg" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" ControlToValidate="EmailInput" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group form-row">
                <label class="col-form-label" for="PasswordInput">Password</label>
                <asp:TextBox runat="server" type="password" required CssClass="form-control" ID="PasswordInput" ValidationGroup="vg" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="PasswordInput" ValidationGroup="vg" ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$" Display="Dynamic" ErrorMessage="*Invalid, must include one uppercase, one lowercase, one number and minimum lenght of 8 character" CssClass="invalid-input"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group form-row">
                <label class="col-form-label" for="ConfirmPasswordInput">Confirm Password</label>
                <asp:TextBox runat="server" type="password" required CssClass="form-control" ID="ConfirmPasswordInput" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="ConfirmPasswordInput" ValidationGroup="vg" ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$" Display="Dynamic" ErrorMessage="*Invalid, must include one uppercase, one lowercase, one number and minimum lenght of 8 character" CssClass="invalid-input" Width="100%"></asp:RegularExpressionValidator>

                <asp:CompareValidator runat="server" ControlToCompare="PasswordInput" ControlToValidate="ConfirmPasswordInput" ValidationGroup="vg" Display="Dynamic" ErrorMessage="*Passwords do not compare" CssClass="invalid-input"></asp:CompareValidator>
            </div>
            <div class="form-group">
                <label for="FlatpickrCalender" style="padding-bottom: 5px; width: 100vw">Birthday</label>
                <asp:TextBox runat="server" ValidationGroup="vg" ID="FlatpickrCalender" required Text="Select Date" class="form-control" type="text" placeholder="Select Date.."  />
                <asp:Label ID="BirthdayLabel" runat="server" CssClass="invalid-input"></asp:Label>
                <asp:CompareValidator runat="server" ControlToValidate="FlatpickrCalender" CssClass="invalid-input" ErrorMessage="*Select a date" Font-Strikeout="False" Display="Dynamic" ValidationGroup="vg" ValueToCompare="Select Date" Operator="NotEqual"></asp:CompareValidator>
            </div>
            <asp:Button runat="server" CssClass="btn btn-success" ID="RegisterButton" Text="Register" OnClick="RegisterButton_Click" ValidationGroup="vg" />
        </form>
    </div>
    <script>
        flatpickr("#FlatpickrCalender", {
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "Y-m-d",
        });
    </script>
</body>
</html>
