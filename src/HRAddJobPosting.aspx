<%@ Page Title="" Language="C#" MasterPageFile="~/HRHome.master" AutoEventWireup="true" CodeFile="HRAddJobPosting.aspx.cs" Inherits="HRAddJobPosting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    
    <div class="w-100">
        <div class="jumbotron col-10 mx-auto" style="padding-left: 5rem; padding-right: 5rem;">
            <h1 class="display-4">Add Job Posting</h1>
            <hr class="my-4" />
            <div class="form-group">
                <asp:Label runat="server" CssClass="pr-2">Position</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="PositionTextbox" ValidationGroup="vg" required></asp:TextBox>
                <asp:Label runat="server" ID="Position"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label runat="server" CssClass="pr-2">Openings</asp:Label>
                <asp:TextBox TextMode="Number" CssClass="form-control" runat="server" ID="OpeningTextbox" ValidationGroup="vg" required></asp:TextBox>
                <asp:CompareValidator runat="server" ControlToValidate="OpeningTextbox" CssClass="invalid-input" Display="Dynamic" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
            </div>
            <div class="form-group">
                <asp:Label runat="server" CssClass="pr-2">Experience Level</asp:Label>
                <asp:DropDownList CssClass="form-control" runat="server" ValidationGroup="vg" ID="ExperienceDropdown">
                    <asp:ListItem Text="Select Experience Type.." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Entry Level/Intern" Value="entry"></asp:ListItem>
                    <asp:ListItem Text="Experienced Professional" Value="professional"></asp:ListItem>
                </asp:DropDownList>
                 <asp:CompareValidator runat="server" CssClass="invalid-input" ErrorMessage="*Select An Option" ControlToValidate="ExperienceDropdown" Display="Dynamic" ValidationGroup="vg" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
           
            </div>
            <div class="form-group">
                <asp:Label runat="server" CssClass="pr-2">Description</asp:Label>
                <asp:TextBox TextMode="MultiLine" CssClass="form-control" runat="server" ID="DescriptionTextbox" ValidationGroup="vg"></asp:TextBox>
            </div>
            
            <asp:Button runat="server" CssClass="btn btn-primary btn-lg mb-3" ValidationGroup="vg" Text="Add Job Posting" OnClick="AddJob_Click" />
            <br />
            <br />
        </div>
        <style>
            textarea.form-control{
                height: 15rem;
            }
        </style>
    </div>
</asp:Content>

