<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" ErrorPage="~/error.aspx" Inherits="ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <link href="Scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="Scripts/js/jquery-3.3.1.min.js"></script>
    <script src="Scripts/js/popper.min.js"></script>
    <script src="Scripts/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <h2>Change Password</h2>
            <h1>For <%=Request.QueryString["email"]==null?"":Request.QueryString["email"].Split('@')[0] %></h1>
            <div class="form-group">
                <label class="col-form-label" for="PasswordInput">New Password</label>
                <asp:TextBox runat="server" type="password" required CssClass="form-control" ID="PasswordInput" ValidationGroup="vg" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="PasswordInput" ValidationGroup="vg" ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$" Display="Dynamic" ErrorMessage="*Invalid, must include one uppercase, one lowercase, one number and minimum lenght of 8 character" CssClass="invalid-input"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label class="col-form-label" for="ConfirmPasswordInput">Confirm New Password</label>
                <asp:TextBox runat="server" type="password" required CssClass="form-control" ID="ConfirmPasswordInput" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="ConfirmPasswordInput" ValidationGroup="vg" ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$" Display="Dynamic" ErrorMessage="*Invalid, must include one uppercase, one lowercase, one number and minimum lenght of 8 character" CssClass="invalid-input" Width="100%"></asp:RegularExpressionValidator>
                <br />
                <asp:CompareValidator runat="server" ControlToCompare="PasswordInput" ControlToValidate="ConfirmPasswordInput" ValidationGroup="vg" Display="Dynamic" ErrorMessage="*Passwords do not compare" CssClass="invalid-input"></asp:CompareValidator>
            </div>
            <div class="btnWrapper">
                <asp:Button ID="ChangePasswordButton" CssClass="btn btn-primary" OnClick="ChangePasswordButton_Click" Text="Change Password" runat="server" />
            </div>
        </div>
        <span runat="server" id="pop" class="popout hide"></span>
    </form>
    <style>
        .invalid-input {
            color: red;
            font-size: small;
            padding-left: 2px;
        }

        #form1 h2 {
            font-size: 5rem;
            padding: 1.5rem 1.5rem 0.0rem 1.5rem;
        }

        #form1 h1 {
            padding: 1.5rem 1.5rem 0rem 2rem;
            font-weight: 400;
            font-size: 2rem;
        }

        .popout {
            position: fixed;
            right: 25px;
            bottom: 0px;
            background-color: rgba(0, 0, 0, 0.87);
            color: white;
            font-size: 1.2rem;
            padding: 0.6rem 2.5rem;
            border-radius: 5px;
            transition: all 0.4s cubic-bezier(0.53, 0.01, 0.38, 1.23);
        }

        .hide {
            transform: translateY(+20px);
            opacity: 0;
        }

        .show {
            transform: translateY(-17px);
            opacity: 1;
        }

        #form1 {
            width: 100vw;
            height: 100vh;
            overflow: hidden;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #f0f0f0;
        }

        .wrapper {
            padding: 4rem;
            background-color: white;
            position: relative;
            border-radius: 10px;
            box-shadow: 0px 0px 3px 0px white;
        }

        .form-group {
            padding-left: 2rem;
            padding-bottom: 0rem;
            width: 50vw
        }

        .btnWrapper{
            padding-left:2rem;
        }
    </style>
    <script>
        function popout(text, time) {
            time = Number(time);
            $(".popout").html(text);
            $(".popout").removeClass("hide").addClass("show");
            setTimeout(() => {
                $(".popout").removeClass("show").addClass("hide");
            }, time * 1000);
        }
    </script>
</body>
</html>
