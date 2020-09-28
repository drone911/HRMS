using hrms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRHome : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] splittedAbsoluteUrl = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
        string pageName = splittedAbsoluteUrl[splittedAbsoluteUrl.Length - 1];

        if (!pageName.Equals("HRRegistration.aspx"))
        {
            if (Request.Cookies["registered"].Value != true.ToString())
            {
                DisableNavigationControls();
                Response.Redirect("~/HRRegistration.aspx");
                Response.End();
            }
            else
            {
                if (Request.Cookies["verified"] != null)
                {
                    if (Request.Cookies["verified"].Value != true.ToString())
                    {
                        DisableNavigationControls();
                        string[] functionParams = { "Wait for 2-3 Business days for verification.", "5" };
                        Util.CallJavascriptFunction(Page, "popout", functionParams);
                    }
                }
                else
                {
                    DisableNavigationControls();
                    string[] functionParams = { "Wait for 2-3 Business days for verification.", "5" };
                    Util.CallJavascriptFunction(Page, "popout", functionParams);
                }
            }
        }

    }

    private void DisableNavigationControls()
    {
        ContentPlaceHolder ct = (ContentPlaceHolder)Page.Master.Master.FindControl("MainContent");
        ((WebControl)ct.FindControl("profileSelection")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("profileSelection")).NavigateUrl += "";
        ((WebControl)ct.FindControl("EmployeeDropdown")).CssClass += " disabled";
        ((WebControl)ct.FindControl("JobPostingDropdown")).CssClass += " disabled";
        ((WebControl)ct.FindControl("attendanceSelection")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("attendanceSelection")).NavigateUrl += "";
        ((WebControl)ct.FindControl("TrainingDropdown")).CssClass += " disabled";
    }
}


