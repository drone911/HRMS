<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRViewEmployee.aspx.cs" Inherits="HRViewEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>

    <asp:MultiView runat="server" ID="ViewMultiView">
        <asp:View ID="NotAuth" runat="server">
            <h2>Not Authorized to View</h2>
        </asp:View>
        <asp:View ID="Auth" runat="server">
            <div class="w-100 pb-4 pl-1">
                <asp:HyperLink CssClass="btn btn-success" NavigateUrl="~/HRViewEmployees.aspx" runat="server"><img class="p-1" style="transform: translateY(-1px);" src="Scripts/icons/arrow-left-circle.svg" /> Go Back</asp:HyperLink>
            </div>
            <div class="row" style="text-align: center">
                <div class="col-4">
                    <div class="card" style="align-items: center;">
                        <div id="profile-container" style="padding-top: 0.7rem">
                            <img class="profileImage" id="EmployeeProfilePic" src="Uploads/ProfilePictures/profilePic.png" runat="server" />
                        </div>
                        <h2 style="letter-spacing: 2.5px; padding-top: 0.7rem; color: #5b5b5b">
                            <asp:Label ID="NameLabel" runat="server"></asp:Label></h2>
                        <h4 style="font-weight: 300">
                            <asp:Label ID="PositionLabel" runat="server"></asp:Label></h4>
                    </div>
                </div>
                <div class="col-4">
                    <div class="card">
                        <h2 style="padding-top: 0px; padding-bottom: 1rem; margin-top: 0px">Personal Details</h2>
                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Blood Group</p>
                        <h4 style="margin-bottom: 15px;">
                            <asp:Label ID="BloodGroupLabel" runat="server"></asp:Label></h4>
                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Qualification</p>
                        <h4 style="margin-bottom: 15px;">
                            <asp:Label ID="QualificationLabel" runat="server"></asp:Label></h4>
                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Birth Date</p>
                        <h4 style="margin-bottom: 2rem;">
                            <asp:Label ID="BirthdayLabel" runat="server"></asp:Label></h4>

                    </div>
                </div>
                <div class="col-4">
                    <div class="card">
                        <h2 style="padding-top: 1.2rem; padding-bottom: 1rem">Contact Details</h2>
                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Address</p>
                        <h4 style="font-size: 1.2rem; padding-bottom: 0.5rem;">
                            <asp:Label ID="AddressLabel" runat="server" Style="margin-bottom: 1rem;"></asp:Label></h4>

                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Number</p>
                        <h4 style="font-size: 1.2rem; padding-bottom: 0.5rem">
                            <asp:Label ID="ContactLabel" runat="server"></asp:Label></h4>

                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Email</p>
                        <h4 style="font-size: 1.2rem; padding-bottom: 1rem">
                            <asp:Label ID="EmailLabel" runat="server"></asp:Label></h4>
                    </div>
                </div>
            </div>
            <div class="row" style="text-align: center; margin-top: 1.5rem; margin-bottom: 2rem; padding-bottom: 2rem;">
                <div class="col-6">
                    <div class="card">
                        <h2 style="padding-top: 1.2rem; padding-bottom: 1rem">Attendance</h2>
                        <div class="row" style="justify-content: space-evenly; margin: 0rem 0rem 0.3rem 0rem; align-items: center;">
                            <div class="form-group">
                                <label for="AttStartDate" style="margin-bottom:0rem;">Start Date</label>
                                <asp:TextBox runat="server" ID="AttStartDate" Style="text-align: center; max-width: 9rem; font-size: 1.1rem;" AutoPostBack="true" OnTextChanged="Att_TextChanged" class="form-control" type="text" />
                                <asp:Label ID="AttStartDateLabel" runat="server" Visible="false" CssClass="invalid-input"></asp:Label>
                            </div>
                            <div class="col-5">
                                <asp:MultiView ID="AttendanceLabelMultiView" ActiveViewIndex="0" runat="server">
                                    <asp:View ID="AttPresentView" runat="server">
                                        <p style="font-size: 1.1rem;margin-bottom:0rem;">Present
                                            <asp:Label ID="CurrAtt" CssClass="h3" runat="server"></asp:Label>
                                            of
                                            <asp:Label ID="TotAtt" runat="server" CssClass="h3"></asp:Label>
                                            days i.e.,
                                            <asp:Label ID="AttPerc" CssClass="h3" runat="server"></asp:Label>%</p>

                                    </asp:View>
                                    <asp:View ID="AttNotPresentView" runat="server">
                                        <asp:Label ID="AttendanceLabel" runat="server"></asp:Label>
                                    </asp:View>
                                </asp:MultiView>

                            </div>
                        </div>
                        <div class="row" style="justify-content: space-evenly; align-items: center; margin: 0rem 0rem 3rem 0rem;">

                            <div class="form-group">
                                <label for="AttEndDate" style="margin-bottom:0rem;">End Date</label>
                                <asp:TextBox runat="server" ID="AttEndDate" Style="text-align: center; max-width: 9rem; font-size: 1.1rem;" AutoPostBack="true" OnTextChanged="Att_TextChanged" class="form-control" type="text" />
                                <asp:Label ID="AttEndDateLabel" runat="server" Visible="false" CssClass="invalid-input"></asp:Label>
                            </div>
                            <div class="col-5">
                                <asp:Button Text="Export Attendance" ID="Export" OnClick="Export_Click" CssClass="btn-lg btn-primary" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-6">
                    <div class="card">
                        <h2 style="padding-top: 1.2rem; padding-bottom: 1rem">Current Training</h2>
                        <div class="row" style="justify-content: space-evenly; margin: 1rem 0rem;">
                            <asp:MultiView ActiveViewIndex="0" ID="CurrentTrainingMultiView" runat="server">
                                <asp:View ID="TrainingView" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-4">
                                                <div class="row" style="flex-direction: column;">
                                                    <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Training Name</p>
                                                    <h4>
                                                        <asp:Label ID="TrainingNameLabel" runat="server"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-4">
                                                <div class="row" style="flex-direction: column;">
                                                    <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Start Date</p>
                                                    <h4 style="margin-bottom: 1rem">
                                                        <asp:Label ID="TrainingStartDate" runat="server"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-4">
                                                <div class="row" style="flex-direction: column;">
                                                    <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">End Date</p>
                                                    <h4>
                                                        <asp:Label ID="TrainingEndDate" runat="server"></asp:Label></h4>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12" style="margin-top: 1.3rem; margin-bottom: 3rem;">
                                        <asp:HyperLink ID="ViewTrainingHyperLink" Text="View Training" CssClass="btn-lg btn-success" Style="color: white; text-decoration: none;" runat="server"></asp:HyperLink>
                                    </div>
                                </asp:View>
                                <asp:View ID="NoTrainingView" runat="server">
                                    <h3 class="h5">No Ongoing Training</h3>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </div>
            </div>
            <style>
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
                    font-size: 1.5rem;
                }

                .col-4 {
                    margin: 0 auto;
                }

                .card {
                    height: 100%;
                    width: 95%;
                    justify-content: center;
                    border-radius: 25px;
                    box-shadow: 1px 2px 7px 0px #bcbcbc;
                    transition: all ease-in 0.1s;
                }
                .form-group{
                    margin-bottom:0rem;
                }
                .filler {
                    padding: 3.5rem;
                }
            </style>
            <script>
                $(document).ready(function () {
                    if ($('#<%=EmployeeProfilePic.ClientID%>').width() > $('#<%=EmployeeProfilePic.ClientID%>').height()) {

                        $('#<%=EmployeeProfilePic.ClientID%>').addClass('imgWidthGreater').removeClass('imgHeightGreater');
                    } else {
                        $('#<%=EmployeeProfilePic.ClientID%>').addClass('imgHeightGreater').removeClass('imgWidthGreater');
                    }
                });
                flatpickr("#<%=AttStartDate.ClientID%>", {
                    dateFormat: "d/m/Y",
                });
                flatpickr("#<%=AttEndDate.ClientID%>", {
                    dateFormat: "d/m/Y",
                });
            </script>
        </asp:View>
    </asp:MultiView>
</asp:Content>

