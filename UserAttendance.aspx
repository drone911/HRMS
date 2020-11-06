<%@ Page Title="" Language="C#" MasterPageFile="~/UserHome.master" AutoEventWireup="true" CodeFile="UserAttendance.aspx.cs" Inherits="UserAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <strong id="date" class="float-right"><%=DateTime.Now.ToShortDateString() %></strong>
    <asp:MultiView ActiveViewIndex="0" ID="AttendanceMultiView" runat="server">
        <asp:View ID="UnderPresent" runat="server">
            <asp:Table ID="UnderAttendanceTable" runat="server" CssClass="table">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Scope="Column">Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell Scope="Column">Position</asp:TableHeaderCell>
                    <asp:TableHeaderCell Scope="Column">Mark Attendance</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <asp:Button Text="Update Attendance" ID="Update" CssClass="btn btn-primary ml-2" OnClick="Update_Click" runat="server" />
        </asp:View>
        <asp:View runat="server" ID="NotUnderPresent">
            <h2 style="text-align: center; margin-top: 3rem;" class="alert alert-danger">No Employees Under Your Supervision</h2>
        </asp:View>
    </asp:MultiView>
    <style>
        #date {
            padding-bottom: 2rem;
        }

            #date::before {
                content: "Date: ";
                font-size: 1rem;
                font-weight: 400;
            }

            #date::after {
                content: "";
                display: block;
                width: 50%;
                height: 3px;
                margin: auto auto;
                background-color: #0069d9;
                transition: all ease-in 0.3s;
            }

            #date:hover::after {
                width: 95%;
            }
    </style>
</asp:Content>

