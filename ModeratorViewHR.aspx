<%@ Page Title="" Language="C#" MasterPageFile="~/ModeratorHome.master" AutoEventWireup="true" CodeFile="ModeratorViewHR.aspx.cs" Inherits="ModeratorViewHR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <div class="w-100 pb-4 pl-1">
                <asp:HyperLink CssClass="btn btn-success" NavigateUrl="~/ModeratorViewHRs.aspx" runat="server"><img class="p-1" style="transform: translateY(-1px);" src="Scripts/icons/arrow-left-circle.svg" /> Go Back</asp:HyperLink>
            </div>
    <div class="row" style="padding: 1rem 2rem; text-align: center; justify-content: space-between;">
        <div class="col-6">
            <div class="cards">
                <div class="row" style="align-items: center; height: 100%">
                    <div class="col-5" style="padding: 2rem;">
                        <div class="row" style="align-items: center; justify-content: center">
                            <div id="profile-container">
                                <img id="profileImage" runat="server" class="imgWidthGreater" src="Uploads/ProfilePictures/profilePic.png" />
                            </div>
                        </div>
                    </div>
                    <div class="vertical-line"></div>
                    <div class="col-6">
                        <h2 style="letter-spacing: 2.5px; padding-top: 0.7rem; color: #5b5b5b">
                            <asp:Label ID="NameLabel" runat="server"></asp:Label></h2>
                        <h4 style="font-weight: 300; font-size: 1.2rem;">
                            <asp:Label ID="PositionLabel" runat="server" Text="Human Resources"></asp:Label></h4>
                        <div class="row" style="justify-content:space-evenly;">
                            <asp:Button ID="VerifyBtn" runat="server" Text="Verify" OnClick="VerifyBtn_Click" CssClass="btn btn-success" />
                            <asp:Button ID="RemoveBtn" runat="server" Text="Remove" OnClick="RemoveBtn_Click" CssClass="btn btn-outline-danger" />
                        </div>
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
    <div class="row" style="padding: 1rem 1rem 2rem 1rem;">
        <div class="col-12">
            <div class="cards" style="padding: 1rem 2rem; margin: 0 auto;">
                <h2 style="padding-top: 1.2rem; padding-bottom: 1rem">Contact Details</h2>
                <hr />
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <label class="col-form-label" for="MobileNumberInput">Mobile Number</label>
                            <asp:Label runat="server" ReadOnly="true" TextMode="Phone" CssClass="form-control" ID="MobileNumberInput"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="col-form-label" for="Address">Address</label>
                            <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="Address" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label class="col-form-label" for="PincodeInput">Pincode</label>
                            <asp:Label runat="server" CssClass="form-control" ID="PincodeInput" ReadOnly="true"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="col-form-label" for="CityInput">City</label>
                            <asp:Label runat="server" CssClass="form-control" ID="CityInput" ReadOnly="true"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="col-form-label" for="StateInput">State</label>
                            <asp:Label runat="server" CssClass="form-control" ID="StateInput" ReadOnly="True"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row" style="align-items: center;">
                    <div class="col-6">
                        <asp:Button CssClass="btn btn-primary" ID="ViewEmployeeCert" runat="server" Text="View Employement Certificate" OnCommand="ViewEmployeeCert_Command" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="padding: 1rem 1rem; margin-bottom: 25px;">
        <div class="col-12">
            <div class="cards" style="padding: 1rem 2rem; margin: 0 auto;">
                <h2 style="padding-top: 1.2rem; padding-bottom: 1rem">Organisation Details</h2>
                <hr />
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <label class="col-form-label" for="OrganisationName">Organisation Name</label>
                            <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" Text="Sudharchan Industries" ID="OrganisationName" />
                        </div>
                        <div class="form-group">
                            <label class="col-form-label" for="OrganisationAddress">Organisation Address</label>
                            <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" Rows="5" TextMode="MultiLine" ID="OrganisationAddress" />
                        </div>
                        <div class="form-group">
                            <asp:Button CssClass="btn btn-primary" ID="ViewGSTCert" runat="server" Text="View GST Certificate" OnCommand="ViewGSTCert_Command" />
                        </div>

                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label class="col-form-label" for="OrganisationPincode">Organisation Pincode</label>
                            <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="OrganisationPincode" />
                        </div>
                        <div class="form-group">
                            <label class="col-form-label" for="OrganisationCity">Organisation City</label>
                            <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="OrganisationCity" />
                        </div>
                        <div class="form-group">
                            <label class="col-form-label" for="OrganisationState">Organisation State</label>
                            <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" ID="OrganisationState" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .btn {
            font-size: 1.2rem;
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
    </script>
</asp:Content>

