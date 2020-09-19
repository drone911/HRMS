<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Remove_Job.aspx.cs" Inherits="Remove_Job" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div>
        <br />
        <br />
        <br />
        &nbsp;Enter Job Id to Remove Job :&nbsp;
            <asp:TextBox ID="TextBox1" runat="server" Width="166px"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Remove Job Opening" />
        <br />
    </div>
</asp:Content>
