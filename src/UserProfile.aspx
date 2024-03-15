<%@ Page Title="" Language="C#" MasterPageFile="~/UserHome.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">

    <div class="row" style="padding: 1rem 2rem; text-align: center; justify-content: space-between;">
        <div class="col-6">
            <div class="cards">
                <!--
                <div id="ContentPicWrapper" style="padding-top: 0.7rem">
                    <img class="profilePic" id="EmployeeProfilePic" src="Uploads/ProfilePictures/profilePic.png" runat="server" />
                </div>
                -->
                <div class="row" style="align-items: center;">
                    <div class="col-5" style="padding: 2rem;">
                        <div class="row" style="align-items: center; justify-content: center">
                            <div id="profile-container">
                                <img id="profileImage" runat="server" class="imgWidthGreater" src="Uploads/ProfilePictures/profilePic.png" />
                            </div>
                            <asp:FileUpload ViewStateMode="Enabled" ID="imageUpload" CssClass="imageUpload" accept="image/jpeg, image/png" AllowMultiple="false" name="profile_photo" runat="server" />
                            <label for="profileImage" class="small mb-0">Click on the picture to change avatar</label>
                            <asp:Label ID="imageLabel" Visible="false" CssClass="invalid-input" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="vertical-line"></div>
                    <div class="col-6">
                        <h2 style="letter-spacing: 2.5px; padding-top: 0.7rem; color: #5b5b5b">
                            <asp:Label ID="NameLabel" runat="server" ></asp:Label></h2>
                        <h4 style="font-weight: 300; font-size: 1.2rem">
                            <asp:Label ID="PositionLabel" runat="server" ></asp:Label></h4>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-6">
            <div class="cards" style="padding: 1rem 2rem;">
                <h2 style="padding-top: 1.2rem;">Personal Details</h2>
                <hr />
                <div class="row">
                    <div class="col-6">
                        <div class="row" style="flex-direction: column;">
                            <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Birth Date</p>
                            <h4>
                                <asp:Label ID="BirthDateLabel" runat="server"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="row" style="flex-direction: column;">
                            <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Blood Group</p>
                            <h4 style="margin-bottom: 1rem">
                                <asp:Label ID="BloodGroupLabel" runat="server"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="row" style="flex-direction: column;">
                            <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Qualification</p>
                            <h4>
                                <asp:Label ID="QualificationLabel" runat="server"></asp:Label></h4>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="row" style="padding: 1rem 1rem;">
        <div class="col-12">
            <div class="cards" style="padding: 1rem 2rem; margin: 0 auto;">
                <h2 style="padding-top: 1.2rem; padding-bottom: 1rem">Contact Details</h2>
                <hr />
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <label class="col-form-label" for="MobileNumberInput">Mobile Number</label>
                            <asp:TextBox runat="server" TextMode="Phone" CssClass="form-control" ID="MobileNumberInput" ValidationGroup="vg" required />
                            <asp:RegularExpressionValidator runat="server" Display="Dynamic" CssClass="invalid-input" ErrorMessage="*Enter a valid number" ControlToValidate="MobileNumberInput" ValidationExpression="^[0-9]{10}$" ValidationGroup="vg"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label class="col-form-label" for="Address">Address</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="Address" Rows="5" TextMode="MultiLine" ValidationGroup="vg" required />
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label class="col-form-label" for="PincodeInput">Pincode</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="PincodeInput" OnTextChanged="PincodeInput_TextChanged" ValidationGroup="vg1" CausesValidation="true" required AutoPostBack="true" />
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
                    </div>
                </div>
                <div class="row" style="align-items:center;">
                    <div class="col-6">
                        <asp:Button CssClass="btn btn-primary" ID="ViewResumeButton" runat="server" Text="View Current Resume" OnCommand="ViewResumeButton_Command" />
                        <div class="form-group" style="margin-top:1rem;">
                            <div class="custom-file">
                                <asp:FileUpload CssClass="custom-file-input" ViewStateMode="Enabled" runat="server" AllowMultiple="false" ID="ResumeUpload" />
                                <asp:Label runat="server" CssClass="custom-file-label" ID="customResumeFileLabel" for="ResumeUpload">Upload New Resume</asp:Label>
                                
                            </div>
                            <asp:Label ID="ResumeLabel" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-6">
                        <asp:Button CssClass="btn btn-success" ID="SaveChangesButton" OnClick="SaveChangesButton_Click" Text="Save Changes" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        .btn{
            font-size:1.2rem;
        }
        
        .imgWidthGreater {
            height: 100%;
            width: auto;
        }

        .imgHeightGreater {
            width: 100%;
            height: auto;
        }

        .imageUpload {
            display: none;
        }

        #profileImage {
            cursor: pointer;
        }

        #profile-container {
            width: 100px;
            height: 100px;
            overflow: hidden;
            border-radius: 50%;
        }


        label {
            display: block;
        }

        .col-4 {
            margin: 0 auto;
        }

        .cards {
            width: 95%;
            height: 100%;
            margin: 0 auto;
            justify-content: center;
            border-radius: 25px;
            box-shadow: 1px 2px 7px 0px #bcbcbc;
            transition: all ease-in 0.1s;
        }

        .vertical-line {
            height: 135px;
            width: 1px;
            background-color: rgba(0, 0, 0, 0.1);
            margin: auto 0;
        }

        .form-group {
            max-width: 75%;
        }
    </style>
    <script>
        $(document).ready(function () {
            if ($('#<%=profileImage.ClientID%>').width() > $('#<%=profileImage.ClientID%>').height()) {

                $('#<%=profileImage.ClientID%>').addClass('imgWidthGreater').removeClass('imgHeightGreater');
            } else {
                $('#<%=profileImage.ClientID%>').addClass('imgHeightGreater').removeClass('imgWidthGreater');
            }
        });

        $("#<%=profileImage.ClientID%>").click(function (e) {
            $("#<%=imageUpload.ClientID%>").click();
        });

        function fasterPreview(uploader) {
            if (uploader.files && uploader.files[0]) {
                $('#<%=profileImage.ClientID%>').attr('src',
                    window.URL.createObjectURL(uploader.files[0])).on('load', function () {
                        if (this.width > this.height) {
                            $(this).removeClass('imgHeightGreater').addClass('imgWidthGreater');
                        } else {
                            $(this).removeClass('imgWidthGreater').addClass('imgHeightGreater');
                        }
                    });

            }
        }

        $("#<%=imageUpload.ClientID%>").change(function () {
            fasterPreview(this);
        });
        
    </script>
</asp:Content>

