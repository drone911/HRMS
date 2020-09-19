<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 40px;
        }

        .auto-style2 {
            margin-left: 80px;
        }
    </style>
    <div>
        <br />
    </div>
    <p class="MsoNormal">
        <b style="mso-bidi-font-weight: normal"><span style="font-family: &quot; comic sans ms&quot;">Enter Login Details<o:p>:</o:p></span></b>
    </p>



    <p>
        &nbsp;
    </p>
    <p style="margin-left: 40px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <p style="margin-left: 40px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b style="mso-bidi-font-weight: normal"><span style="font-family: &quot; comic sans ms&quot;">&nbsp;UserName<o:p></o:p></span></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Height="19px" Width="135px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please Enter Username"></asp:RequiredFieldValidator>
    </p>
    <div style="margin-left: 40px">
        <p class="MsoNormal">
            <b class="auto-style1" style="mso-bidi-font-weight: normal"><span style="font-family: &quot; comic sans ms&quot;">Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></b>
            <asp:TextBox ID="TextBox2" runat="server" Height="19px" Width="135px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Please Enter Password"></asp:RequiredFieldValidator>
        </p>
        <p class="MsoNormal">
            &nbsp;
        </p>
        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Login" BackColor="Aqua" BorderColor="#003366" Font-Bold="True" Font-Names="Comic Sans MS" Width="57px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Cancel" BackColor="Aqua" BorderColor="#003366" Font-Bold="True" Font-Names="Comic Sans MS" Width="74px" />
    </div>
    <p class="auto-style2">
        &nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <p style="margin-left: 40px">
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    </p>
    <p style="margin-left: 40px">
        &nbsp;
    </p>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <p style="margin-left: 40px">
        &nbsp;
    </p>
</asp:Content>
