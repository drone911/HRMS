using hrms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ModeratorHome : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string[] splittedAbsoluteUrl = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
        string pageName = splittedAbsoluteUrl[splittedAbsoluteUrl.Length - 1];
        if (!Util.IsLoggedIn(Request.Cookies))
        {
            Response.Redirect("~/Login.aspx");
            Response.End();
        }
        if (Request.Cookies["profileURL"] == null)
        {
            profilePic.Src = "~/Uploads/ProfilePictures/profilePic.png";
        }
        else
        {
            profilePic.Src = Request.Cookies["profileURL"].Value;

        }
        if (Request.Cookies["verified"] != null)
        {
            if (Request.Cookies["verified"].Value != true.ToString())
            {
                DisableNavigationControlsForNotVerified();
                string[] functionParams = { "Wait for verification.", "5" };
                Util.CallJavascriptFunction(Page, "popout", functionParams);
            }
        }
        else
        {
            DisableNavigationControlsForNotVerified();
            string[] functionParams = { "Wait for verification.", "5" };
            Util.CallJavascriptFunction(Page, "popout", functionParams);
        }
    }
    protected void DisableNavigationControlsForNotVerified()
    {
        ContentPlaceHolder ct = (ContentPlaceHolder)Page.Master.Master.FindControl("MainContent");
        ((WebControl)ct.FindControl("ManageHR")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("ManageHR")).NavigateUrl = "";
        ((WebControl)ct.FindControl("ManageMod")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("ManageMod")).NavigateUrl = "";
        
    }
}
