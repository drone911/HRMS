using hrms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Util.IsLoggedIn(Request.Cookies))
        {
            string username = Request.Cookies["email"].Value.Split('@')[0];
            string role = Request.Cookies["role"].Value.ToString();
            // add other navigate URLS
            if(role == "hr")
            {
                HomeHyperLink.NavigateUrl = "~/HRHome.aspx";
            }

            LoginLogoutHyperLink.NavigateUrl = "~/Logout.aspx";
            LoginLogoutHyperLink.Text = "Log Out";
            HiLabel.Text = "Hi " + username;
            HiLabel.Visible = true;
            Register.Visible = false;
        }
        else
        {
            HomeHyperLink.NavigateUrl = "~/Home.aspx";
            LoginLogoutHyperLink.NavigateUrl = "~/Login.aspx";
            LoginLogoutHyperLink.Text = "Log In";
            HiLabel.Visible = false;
            
        }
    }
}
