<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRRegistration.aspx.cs" Inherits="HRRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <div style="padding-bottom:50px">
        <div class="col-12">
            <h2>Need Some More Details!</h2>
            <hr style="border-top: 3px solid rgba(0, 0, 0, 0.5)" />

        </div>

        <div class="row">
            <div class="mx-auto col-5">
                <div>
                    <asp:Label ID="AlertLabel" runat="Server" CssClass="row alert alert-primary" Visible="false" role="alert"></asp:Label>
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="MobileNumberInput">Mobile Number</label>
                    <asp:TextBox runat="server" TextMode="Phone" CssClass="form-control" ID="MobileNumberInput" ValidationGroup="vg" required />
                    <asp:RegularExpressionValidator runat="server" CssClass="invalid-input" ErrorMessage="*Enter a valid number" ControlToValidate="MobileNumberInput" ValidationExpression="^[0-9]{10}$" ValidationGroup="vg"></asp:RegularExpressionValidator>

                </div>
                <div class="form-group">
                    <label class="col-form-label" for="AddressLine1">Address Line 1</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="AddressLine1" ValidationGroup="vg" required />
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="AddressLine2">Address Line 2</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="AddressLine2" ValidationGroup="vg" required />
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="PincodeInput">Pincode</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="PincodeInput" ValidationGroup="vg1" CausesValidation="true" required AutoPostBack="true" OnTextChanged="PincodeInput_TextChanged" />
                    <asp:RegularExpressionValidator ControlToValidate="PincodeInput" ValidationGroup="vg1" ErrorMessage="*Not A Valid Pincode" CssClass="invalid-input" runat="server" ValidationExpression="[1-9]{1}[0-9]{5}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:Label ID="PincodeLabel" runat="server"></asp:Label>

                </div>

                <div class="form-group">
                    <label class="col-form-label" for="CityInput">City</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="CityInput" ReadOnly="true" ValidationGroup="vg" required />
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="StateInput">State</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="StateInput" ValidationGroup="vg" required ReadOnly="True" />
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="BloodGroupInput">Blood Group</label>
                    <asp:DropDownList CssClass="form-control" runat="server" ValidationGroup="vg" ID="BloodGroupInput">
                        <asp:ListItem Text="Select Blood Group.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                        <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                        <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                        <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                        <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                        <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                        <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                        <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator runat="server" CssClass="invalid-input" ErrorMessage="*Select An Option" ControlToValidate="BloodGroupInput" Display="Dynamic" ValidationGroup="vg" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                </div>
                 <!-- make it a dropdown list -->
               
                <div class="form-group">
                    <label class="col-form-label" for="QualificationInput">Qualification</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="QualificationInput" ValidationGroup="vg" required />
                </div>
            </div>
            <div class="mx-auto col-5">
                
                <label class="col-form-label" for="EmployementVerificationProofUpload">Upload Employee Verification Certificate</label>
                <div class="form-group">
                    <div class="custom-file">
                        <asp:FileUpload CssClass="custom-file-input" runat="server" AllowMultiple="false" ID="EmployementVerificationProofUpload" />
                        <asp:Label runat="server" CssClass="custom-file-label" ID="customEmpFileLabel" for="EmployementVerificationProofUpload">Choose file</asp:Label>
                        <asp:Button ID="EmployementProofButton" Text="Upload" runat="server" OnClick="EmployementProofButton_Click" UseSubmitBehavior="false" CausesValidation="false" Style="display: none" />
                    </div>
                    <asp:Label ID="EmployementVerificationProofLabel" runat="server"></asp:Label>
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="GSTRegistrationCertificateUpload">Upload GST Registration Certificate</label>
                    <div class="custom-file">
                        <asp:FileUpload runat="server" CssClass="custom-file-input" ID="GSTRegistrationCertificateUpload" AllowMultiple="false" />
                        <asp:Label runat="server" CssClass="custom-file-label" ID="customBusinessFileLabel" for="GSTRegistrationCertificateUpload">Choose file</asp:Label>
                        <asp:Button ID="GSTProofButton" Text="Upload" runat="server" OnClick="GSTProofButton_Click" UseSubmitBehavior="false" CausesValidation="false" Style="display: none" />
                    </div>
                    <asp:Label ID="GSTRegistrationLabel" runat="server"></asp:Label>
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="OrganisationNameInput">Organisation Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="OrganisationNameInput" ValidationGroup="vg" required />
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="OrganisationAddressLine1">Organisation Address Line 1</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="OrganisationAddressLine1" ValidationGroup="vg" required />
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="OrganisationAddressLine2">Organisation Address Line 2</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="OrganisationAddressLine2" ValidationGroup="vg" required />
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="OrganisationPincode">Organisation Pincode</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="OrganisationPincode" ValidationGroup="vg2" CausesValidation="true" required AutoPostBack="true" OnTextChanged="OrganisationPincode_TextChanged" />
                    <asp:RegularExpressionValidator ControlToValidate="OrganisationPincode" ValidationGroup="vg2" ErrorMessage="*Not A Valid Pincode" CssClass="invalid-input" runat="server" ValidationExpression="[1-9]{1}[0-9]{5}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                <div class="form-group">
                    <label class="col-form-label" for="OrganisationCity">Organisation City</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="OrganisationCity" ValidationGroup="vg" ReadOnly="true" required />
                </div>
                <div class="form-group">
                    <label class="col-form-label" for="OrganisationState">Organisation State</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="OrganisationState" ValidationGroup="vg" ReadOnly="true" required />
                </div>

                <asp:Button runat="server" CssClass="btn btn-success" ID="RegisterHRButton" CausesValidation="true" Text="Update Information" OnClick="RegisterHRButton_Click" ValidationGroup="vg" />
            </div>
        </div>
    </div>
    <style>
        .invalid-input {
            color: red;
            font-size: small;
            padding-left: 2px
        }

        .valid-input {
            color: green;
            font-size: small;
            padding-left: 2px
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
        function UploadEmpProof(fileUpload) {
            if (fileUpload.value != '') {
                var empFileName = fileUpload.value.split("\\").pop();
                document.getElementById("<%=EmployementProofButton.ClientID %>").click();
                $("#customEmpFileLabel").addClass("selected").html(empFileName);
            }

        }

        function UploadGSTProof(fileUpload) {
            if (fileUpload.value != '') {
                var gstFileName = fileUpload.value.split("\\").pop();
                document.getElementById("<%=GSTProofButton.ClientID %>").click();
                $("#customBusinessFileLabel").addClass("selected").html(gstFileName);
            }

        }
    </script>

</asp:Content>

