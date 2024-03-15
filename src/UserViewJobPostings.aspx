<%@ Page Title="" Language="C#" MasterPageFile="~/UserHome.master" AutoEventWireup="true" CodeFile="UserViewJobPostings.aspx.cs" Inherits="UserViewJobPostings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <div>
        <div style="margin-left: 1rem">
            <div class="form-group col-6">
                <label class="col-form-label" for="FilterCities">Filter Cities</label>
                <asp:DropDownList CssClass="form-control col-8" runat="server" ValidationGroup="vg" ID="FilterCities" AutoPostBack="true">
                    <asp:ListItem Text="No Filter" Value="none" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-6">
                <label class="col-form-label" for="FilterStatus">Filter Experience</label>
                <asp:DropDownList CssClass="form-control col-8" runat="server" ValidationGroup="vg" ID="FilterExperience" AutoPostBack="true">
                    <asp:ListItem Text="No Filter" Value="none" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Intern/Entry Level" Value="entry"></asp:ListItem>
                    <asp:ListItem Text="Experienced Professional" Value="professional"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <asp:MultiView ActiveViewIndex="0" ID="DetailsMultiView" runat="server">
            <asp:View ID="MainView" runat="server">
                <div class="row">
                    <asp:Repeater ID="JobRepeater" runat="server">
                        <ItemTemplate>
                            <asp:HyperLink class="detailsHyperLink col-5" NavigateUrl='<%# "~//UserViewJobPosting.aspx?id=" + HttpUtility.UrlEncode(Eval("jID").ToString()) %>' runat="server">
                        <div class="repeatItem">
                            <div class="item-header row" style="justify-content: space-between; align-items: center;">
                                <span>
                                    <h2><%# Eval("position") %></h2>
                                    <h4 style="font-size:1.2rem; display:inline; color:">@</h4>
                                <h4 style="font-size:1.2rem; display:inline;color:"><%# Eval("organisationName") %></h4>
                                
                                </span>
                                <span style="display:inline-block">
                                    <p style="display:inline; font-weight:200">Posted On</p><p style="font-weight: 500; color:#9a9a9a"><%# Eval("createdOn") %></p>
                                </span>
                                
                            </div>
                            <div class="item-details row" style="justify-content: space-between; align-items: center;">
                                <strong><%# Eval("city") %>, <%# Eval("state") %></strong>
                                <p><%# (Eval("experience").ToString()=="entry")? "Intern/Entry Level":"Experienced Professional" %></p>
                            </div>
                        </div>
                            </asp:HyperLink>

                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </asp:View>
            <asp:View ID="NoDetailsView" runat="server">
                <h2 class="alert alert-danger" style="margin-left:2rem; margin-top:2rem;">No Jobs Available, Try Other Filters</h2>
            </asp:View>
        </asp:MultiView>

    </div>
    <style>
        .repeatItem {
            box-shadow: 0px 0px 2px 0px #c4c4c4;
            padding: 1.5rem 2rem;
        }

            .repeatItem:hover, .repeatItem:focus {
                box-shadow: 0px 0px 8px 0px #9f9f9f;
            }

        div.item-header > span > h2 {
            color: #17609f;
            margin-bottom: 0px;
        }

        .detailsHyperLink {
            margin: 10px 30px;
            text-decoration: none;
        }

            .detailsHyperLink, .detailsHyperLink > * {
                text-decoration: none !important;
                color: black;
            }

        .item-details > p {
            color: #2699FB;
        }
    </style>
</asp:Content>

