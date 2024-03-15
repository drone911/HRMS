<%@ Page Title="" Language="C#" MasterPageFile="~/ModeratorHome.master" AutoEventWireup="true" CodeFile="ModeratorProfile.aspx.cs" Inherits="ModeratorProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <div class="row" style="padding: 1rem 2rem; text-align: center; justify-content: space-between;">
        <div class="col-6">
            <div class="cards">
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
                            <asp:Label ID="NameLabel" runat="server"></asp:Label></h2>
                        <h4 style="font-weight: 300; font-size: 1.2rem">
                            <asp:Label ID="PositionLabel" Text="Moderator" runat="server"></asp:Label></h4>
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
                        <asp:Button CssClass="btn btn-success" ID="SaveChangesButton" OnClick="SaveChangesButton_Click" Text="Save Changes" runat="server" />
                    </div>

                </div>

            </div>
        </div>
    </div>
    <style>
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

