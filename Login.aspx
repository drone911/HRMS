<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="Scripts/js/jquery-3.3.1.min.js"></script>
    <script src="Scripts/js/popper.min.js"></script>
    <script src="Scripts/js/bootstrap.min.js"></script>

</head>
<body>
    <style>
        .input-invalid {
            color: red;
            font-size: small;
            padding-left: 2px
        }

        #form1 {
            height: 95vh;
            padding-top: 13vh;
        }
        #cardDiv{
                margin: auto;
        }
        
    </style>
    <form class="" runat="server" id="form1">

        <div id="cardDiv" class="card col-3 ">
            <article class="card-body">
                <asp:HyperLink runat="server" NavigateUrl="~/UserRegistration.aspx" CssClass="float-right btn btn-outline-primary">Sign up</asp:HyperLink>
                <h4 class="card-title mb-4 mt-1">Sign in</h4>
                <hr />
                <div class="row w-100">
                    <asp:Label ID="LoginLabel" CssClass="text-center w-100" runat="server"></asp:Label>
                </div>

                <div class="form-group">
                    <label>Your email</label>
                    <asp:TextBox ID="EmailInput" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailValidator" CssClass="input-invalid" runat="server" ControlToValidate="EmailInput" ErrorMessage="*Email Required"></asp:RequiredFieldValidator>
                </div>
               <div class="form-group">
                    <asp:HyperLink runat="server" CssClass="float-right" NavigateUrl="~/ForgotPassword.aspx">Forgot?</asp:HyperLink>
                    <label>Your password</label>
                    <asp:TextBox ID="PasswordInput" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordValidator" CssClass="input-invalid" runat="server" ControlToValidate="PasswordInput" ErrorMessage="*Password Required"></asp:RequiredFieldValidator>

                </div>
                <div class="form-group">
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox runat="server" ID="SavePasswordCheckbox" />
                            Save password
                        </label>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button CssClass="btn btn-primary btn-block" ValidationGroup="vg" Text="Login" runat="server" OnClick="LoginButton_Click" />
                </div>
            </article>
        </div>
    </form>
</body>
</html>
