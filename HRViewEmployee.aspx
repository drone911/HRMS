<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRViewEmployee.aspx.cs" Inherits="HRViewEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:MultiView runat="server" ID="ViewMultiView">
        <asp:View ID="NotAuth" runat="server">
            <h2>Not Authorized to View</h2>
        </asp:View>
        <asp:View ID="Auth" runat="server">
            <div class="row">
                <div class="col-4">
                    <div class="card">

                        <div id="ContentPicWrapper" style="padding-top: 0.7rem">
                            <img class="profilePic" id="EmployeeProfilePic" src="Uploads/ProfilePictures/profilePic.png" runat="server" />
                        </div>
                        <h2 style="letter-spacing: 2.5px; padding-top: 0.7rem"><asp:Label ID="NameLabel" runat="server"></asp:Label></h2>
                        <h4 style="font-weight: 300"><asp:Label ID="PositionLabel" runat="server"></asp:Label></h4>
                    </div>
                </div>
                <div class="col-4">
                    <div class="card">
                        <asp:Label ID="AddressLabel" runat="server"></asp:Label>
                        <asp:Label ID="ContactLabel" Text="Mobile Number: " runat="server"></asp:Label>
                        <asp:Label ID="EmailLabel" Text="Email: " runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-4">
                    <div class="card">
                        <asp:Label ID="BloodGroupLabel" Text="Blood Group: " runat="server"></asp:Label>
                        <asp:Label ID="QualificationLabel" Text="Qualification: " runat="server"></asp:Label>
                    </div>
                </div>

            </div>
            <style>
                
                #ContentPicWrapper {
                    object-fit: contain;
                }
                .profilePic{
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
                    align-items: center;
                    border-radius: 25px;
                    box-shadow:1px 2px 7px 0px #bcbcbc;
                }
            </style>

        </asp:View>
    </asp:MultiView>
</asp:Content>

