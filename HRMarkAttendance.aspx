<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRMarkAttendance.aspx.cs" Inherits="HRMarkAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <strong id="date" class="float-right"><%=DateTime.Now.ToShortDateString() %></strong>
    <asp:Table ID="AttendanceTable" runat="server" CssClass="table">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell Scope="Column">Name</asp:TableHeaderCell>
            <asp:TableHeaderCell Scope="Column">Position</asp:TableHeaderCell>
            <asp:TableHeaderCell Scope="Column">Mark Attendance</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <asp:Button Text="Update Attendance" ID="Update" CssClass="btn btn-primary ml-2" OnClick="Update_Click" runat="server" />
    <style>
        #date{
            padding-bottom:2rem;
        }
        #date::before{
            content:"Date: ";
            font-size:1rem;
            font-weight: 400;
        }
        #date::after{
            content: "";
            display:block;
            width:50%;
            height:3px;
            margin: auto auto;
            background-color:#0069d9;
            transition: all ease-in 0.3s;
        }
        #date:hover::after{
            width:95%;
        }
    </style>
</asp:Content>

