using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string role = (String)Session["role"];
        string username = (String)Session["id"];
        if (role == null)
        {
            HomeHyperLink.NavigateUrl = "~/Home.aspx";

        }
        else if (role.Equals("HR"))
        {
            HomeHyperLink.NavigateUrl = "~/HR_Home.aspx";
        }
        else if (role.Equals("Employee"))
        {
            HomeHyperLink.NavigateUrl = "~/Employee_Home.aspx";
        }
        
        if (username == null)
        {
            LoginLogoutHyperLink.NavigateUrl = "~/Login.aspx";
            LoginLogoutHyperLink.Text = "Log In";
            HiLabel.Visible = false;
        }
        else
        {
            LoginLogoutHyperLink.NavigateUrl = "~/Logout.aspx";
            LoginLogoutHyperLink.Text = "Log Out";
            HiLabel.Text = "Hi " + username;
            HiLabel.Visible = true;
        }
    }
}
