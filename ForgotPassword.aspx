<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" ErrorPage="~/error.aspx" Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <link href="Scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />
    
    <script src="Scripts/js/jquery-3.3.1.min.js"></script>
    <script src="Scripts/js/popper.min.js"></script>
    <script src="Scripts/js/bootstrap.min.js"></script>
</head>
<body>
    
    <form id="form1" runat="server">
        <script type="text/javascript">
            var popout = function (text, time) {
                time = Number(time);
                $(".popout").html(text);
                $(".popout").removeClass("hide").addClass("show");
                setTimeout(() => {
                    $(".popout").removeClass("show").addClass("hide");
                }, time * 1000);
            }
        </script>
        <div class="wrapper">
            <h2>Forgot Password?</h2>
            <div class="form-group">
                <asp:Label runat="server" for="EmailInput" CssClass="form-check-label">Enter Registered Email</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ValidationGroup="vg" ID="EmailInput"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" CssClass="invalid-input" ControlToValidate="EmailInput" ErrorMessage="*Enter Valid Email" Display="Dynamic" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"></asp:RegularExpressionValidator>
                <asp:Label ID="EmailLabel" runat="server"></asp:Label>
            </div>
            <asp:Button CssClass="btn btn-primary" runat="server" Text="Send Mail" OnClick="SendEmailButton_Click" ID="SendEmailButton" />
            <asp:HyperLink CssClass="btn btn-outline-success" runat="server" Text="Verify Email" NavigateUrl="SendVerification.aspx" Visible="false" ID="VerifyEmailHyperlink" />
        </div>
    </form>
    <span runat="server" id="pop" class="popout hide"></span>
    <style>
        #form1 h2 {
            font-size: 5rem;
            padding: 1.5rem;
        }

        .invalid-input {
            color: red;
            font-size: small;
            padding-left: 2px
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
            padding: 9rem 1.5rem;
            background-color: white;
            position: relative;
            border-radius: 10px;
            box-shadow: 0px 0px 3px 0px white;
        }

        .form-group {
            padding-top: 1.5rem;
            padding-left: 1.5rem;
            padding-bottom: 1rem;
        }

        #SendEmailButton {
            left: 2.9rem;
            position: absolute;
        }

        #VerifyEmailHyperlink {
            right: 2.9rem;
            position: absolute;
        }
    </style>

</body>
</html>
