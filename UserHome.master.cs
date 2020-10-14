using hrms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserHome : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] splittedAbsoluteUrl = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
        string pageName = splittedAbsoluteUrl[splittedAbsoluteUrl.Length - 1];
        if (!Util.IsLoggedIn(Request.Cookies))
        {
            Response.Redirect("~/Login.aspx");
            Response.End();
        }
        if (!pageName.Equals("UserRegistration.aspx"))
        {
            if (Request.Cookies["registered"].Value != true.ToString())
            {
                Response.Redirect("~/UserRegistration.aspx");
                Response.End();
            }
        }
        else
        {
            DisableNavigationControls();
        }
        
    }
    protected void DisableNavigationControls()
    {
        ContentPlaceHolder ct = (ContentPlaceHolder)Page.Master.Master.FindControl("MainContent");
        ((WebControl)ct.FindControl("ProfileSelection")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("ProfileSelection")).NavigateUrl += "";
        ((WebControl)ct.FindControl("Attendance")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("Attendance")).NavigateUrl += "";
        ((WebControl)ct.FindControl("Training")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("Training")).NavigateUrl += "";
        ((WebControl)ct.FindControl("UserViewJobPosting")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("UserViewJobPosting")).NavigateUrl += "";
    }
}

