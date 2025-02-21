﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ModeratorHome.master" AutoEventWireup="true" CodeFile="ModeratorViewModerators.aspx.cs" Inherits="ModeratorViewModerators" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" Runat="Server">
    <div>
        <div class="form-group">
            <label class="col-form-label" for="FilterModerators">Filter Moderators</label>
            <asp:DropDownList CssClass="form-control col-2" runat="server" ValidationGroup="vg" ID="FilterModerators" AutoPostBack="true">
                <asp:ListItem Text="Not Verified" Value="notverified" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Verified" Value="verified"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <hr />
         <input class="form-control col-3" autocomplete="off" id="search" type="text" placeholder="Search here,.." />
        <br />
        <asp:Table ID="ModeratorTable" runat="server" CssClass="table" CellSpacing="0" Width="75%">
            
        </asp:Table>
    </div>
</asp:Content>

