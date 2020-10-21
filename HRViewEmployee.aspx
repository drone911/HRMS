<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRViewEmployee.aspx.cs" Inherits="HRViewEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
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
                    <div class="card">

                        <div id="ContentPicWrapper" style="padding-top: 0.7rem">
                            <img class="profilePic" id="EmployeeProfilePic" src="Uploads/ProfilePictures/profilePic.png" runat="server" />
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
                        <h4 style="margin-bottom: 1rem">
                            <asp:Label ID="BloodGroupLabel" runat="server"></asp:Label></h4>
                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Qualification</p>
                        <h4>
                            <asp:Label ID="QualificationLabel" Text="" runat="server"></asp:Label></h4>
                        <div class="filler"></div>
                    </div>
                </div>
                <div class="col-4">
                    <div class="card">
                        <h2 style="padding-top: 1.2rem; padding-bottom: 1rem">Contact Details</h2>
                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Address</p>
                        <h4 style="font-size: 1.2rem; padding-bottom: 1rem">
                            <asp:Label ID="AddressLabel" runat="server"></asp:Label></h4>

                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Number</p>
                        <h4 style="font-size: 1.2rem; padding-bottom: 1rem">
                            <asp:Label ID="ContactLabel" runat="server"></asp:Label></h4>

                        <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px">Email</p>
                        <h4 style="font-size: 1.2rem; padding-bottom: 1rem">
                            <asp:Label ID="EmailLabel" runat="server"></asp:Label></h4>
                    </div>
                </div>
            </div>
            <style>
                #ContentPicWrapper {
                    object-fit: contain;
                }

                .profilePic {
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

                    .card:hover {
                        transform: scaleX(1.05) scaleY(1.05);
                    }

                .filler {
                    padding: 3.5rem;
                }
            </style>

        </asp:View>
    </asp:MultiView>
</asp:Content>

