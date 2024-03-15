<%@ Page Title="" Language="C#" MasterPageFile="~/UserHome.master" AutoEventWireup="true" CodeFile="UserViewJobPosting.aspx.cs" Inherits="UserViewJobPosting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <div class="container">
         <div class="w-100 pb-4 pl-1">
                <asp:HyperLink CssClass="btn btn-success" NavigateUrl="~/UserViewJobPostings.aspx" runat="server"><img class="p-1" style="transform: translateY(-1px);" src="Scripts/icons/arrow-left-circle.svg" /> Go Back</asp:HyperLink>
            </div>
        <div class="job-header my-card row">
            <div class="job-name row">
                <h1 style="margin: 0rem;">
                    <asp:Label ID="PositionLabel" runat="server" Text="temo"></asp:Label></h1>
                <p style="margin-bottom: 0;">
                    <asp:Label ID="OrganisationLabel" runat="server" CssClass="medium-text"></asp:Label>
                    <br />
                    <asp:Label ID="OrganisationAddress" runat="server" CssClass="light-text"></asp:Label>
                </p>
            </div>
            <asp:Button CssClass="btn btn-danger" Text="Apply Now" ID="ApplyNowButton" OnClick="ApplyNowButton_Click" runat="server" />
        </div>
        <div class="row job-body">
            <div class="col-7 job-description my-card">
                <h2>Description</h2>
                <hr />
                <p class="description" id="DesctiptionLabel" runat="server"></p>
            </div>
            <div class="col-4 job-details my-card">
                <h2>Job Details</h2>
                <hr />
                <ul style="text-align:left;">
                    <li>
                        <strong>Openings: </strong>
                        <asp:Label ID="OpeningsLabel" runat="server"></asp:Label>
                    </li>
                    <li>
                        <strong>Posted On: </strong>
                        <asp:Label ID="CreatedOnLabel" runat="server"></asp:Label>
                    </li>
                    <li>
                        <strong>Posted By: </strong>
                        <asp:Label ID="HRNameLabel" runat="server"></asp:Label>
                    </li>
                    <li>
                        <strong>Contact At: </strong>
                        <asp:Label ID="HREmailLabel" runat="server"></asp:Label>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <style>
        .container {
            width: 70vw;
        }
        
        .my-card {
            box-shadow: 0px 0px 2px 0px #c4c4c4;
            padding: 1rem 2rem;
            margin: 1rem 0rem;
        }

        .job-header {
            padding: 1.4rem 4rem;
            justify-content: space-between;
            align-items: center;
        }
        #page-content-wrapper h1, h2{
            color: #17609f
        }
        
        #page-content-wrapper h1{
            font-size: 3rem;
        }

        .medium-text{
            font-size: 1.4rem;
            font-weight:500;
        }
        .light-text{
            color:#a6a6a6;
            line-height:0px;
        }
        .job-name {
            flex-direction: column;
        }

        input.btn-danger, input.btn-success {
            font-size:1.5rem;
            padding-left:1.3rem;
            padding-right:1.3rem;
        }

        .job-body {
            margin: 0rem 0rem;
            justify-content: space-between;
        }

        .description{
            white-space:normal
        }
        .job-details {
            text-align: center;
            flex-direction: column;
        }
    </style>
</asp:Content>

