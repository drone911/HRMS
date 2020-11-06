<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRViewTraining.aspx.cs" Inherits="HRViewTraining" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <div class="w-100 pb-4 pl-1">
        <asp:HyperLink CssClass="btn btn-success" NavigateUrl="~/HRViewTrainings.aspx" runat="server"><img class="p-1" style="transform: translateY(-1px);" src="Scripts/icons/arrow-left-circle.svg" /> Go Back</asp:HyperLink>
    </div>
    <div class="row w-100" style="justify-content: space-evenly">
        <div class="row col-4">
            <div class="col-12">
                <strong>Training Name: </strong>
                <asp:Label ID="NameLabel" runat="server"></asp:Label>
            </div>
            <div class="col-6">
                <strong>From: </strong>
                <asp:Label ID="StartDateLabel" runat="server"></asp:Label>
            </div>
            <div class="col-6 float-right">
                <strong>To: </strong>
                <asp:Label ID="EndDateLabel" runat="server"></asp:Label>
            </div>
            <div class="col-12">
                <strong>Description: </strong>
                <br />
                <asp:Label ID="DescriptionLabel" runat="server" CssClass="textbox"></asp:Label>
            </div>
            <div class="col-6 float-right">
                <asp:Button ID="RemoveTraining" runat="server" Text="Remove Training" CssClass="btn btn-outline-danger" />
            </div>

        </div>

        <div class="row col-7 float-right">
            <asp:Table CssClass="table" ID="TrainingTable" runat="server"></asp:Table>
        </div>

    </div>
    <style>
        .textbox {
            border: none;
            background-color: transparent;
            display: inline-block;
            background-image: none;
        }

        span.open {
            padding: 0.3em 0.7em;
            color: #28a745;
            background-color: transparent;
            background-image: none;
            border-color: #28a745;
            text-align: center;
            display: inline-block;
            border: 1px solid;
            border-radius: .25rem;
            vertical-align: middle;
        }

        span.closed {
            padding: 0.3em 0.7em;
            color: #dc3545;
            background-color: transparent;
            background-image: none;
            border-color: #dc3545;
            text-align: center;
            display: inline-block;
            border: 1px solid;
            border-radius: .25rem;
            vertical-align: middle;
        }

        div.row.col-4 {
            padding-top: 3rem;
            padding-bottom: 3rem;
            box-shadow: 0px 1px 4px 0px #949494;
            font-size: 1rem;
            margin-left: 5px;
            border: 2px solid;
            border-color: white;
            border-radius: 0.5rem;
        }

        div.row.col-7 {
            padding-top: 3rem;
            padding-bottom: 3rem;
            box-shadow: 0px 1px 4px 0px #949494;
            margin-left: 5px;
            border: 2px solid;
            border-color: white;
            border-radius: 0.5rem;
        }

        div.col-6, div.col-12 {
            margin-bottom: 1.2rem;
        }
    </style>
</asp:Content>

