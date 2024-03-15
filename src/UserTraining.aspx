<%@ Page Title="" Language="C#" MasterPageFile="~/UserHome.master" AutoEventWireup="true" CodeFile="UserTraining.aspx.cs" Inherits="UserTraining" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <div class="row" style="padding: 1rem 2rem; text-align: center; justify-content: space-between;">
        <div class="col-12">
            <div class="cards" style="padding: 1rem 2rem;">
                <h2 style="padding-top: 1.2rem;">Current Training</h2>
                <hr />
                <asp:MultiView ActiveViewIndex="0" ID="CurrentTrainingMultiView" runat="server">
                    <asp:View ID="TrainingView" runat="server">
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
                        <div class="row" style="flex-direction: column; align-items: flex-start; max-width: 45%;">
                            <p style="font-size: 1.1rem; font-weight: 100; margin-bottom: 0px; text-align: left; margin-left: 7.5rem;">Description</p>
                            <h4 style="margin-left: 7.5rem;">
                                <asp:Label ID="TrainingDescription" CssClass="textbox" runat="server" Style="text-align: left;"></asp:Label></h4>
                        </div>
                    </asp:View>
                    <asp:View ID="NoTrainingView" runat="server">
                        <h3 class="h5">Hooray!! No Ongoing Training</h3>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
    <div class="row" style="padding: 1rem 2rem; text-align: center">
        <div class="col-12">
            <div class="cards" style="padding: 1rem 2rem;">
                <h2 style="padding-top: 1.2rem; padding-bottom: 1rem">Previous Training</h2>
                <hr />
                <asp:MultiView ActiveViewIndex="0" ID="PreviousTrainingMultiView" runat="server">
                    <asp:View ID="PreviousTrainings" runat="server">
                        <div class="row">
                                <asp:Table ID="PrevTrainingTable" runat="server" CssClass="table" CellSpacing="0" Width="100%">
                                </asp:Table>
                        </div>
                    </asp:View>
                    <asp:View ID="NoPreviousTrainings" runat="server">
                        <h3 class="h5">No Records of Previous Trainings</h3>
                    </asp:View>
                </asp:MultiView>

            </div>
        </div>
    </div>
    <style>
        .textbox {
            border: none;
            background-color: transparent;
            display: inline-block;
            background-image: none;
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
    </style>
</asp:Content>

