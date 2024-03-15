using hrms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class UserHome : System.Web.UI.MasterPage
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
        if (!pageName.Equals("UserRegistration.aspx"))
        {
            if (Request.Cookies["registered"].Value != true.ToString())
            {
                Response.Redirect("~/UserRegistration.aspx");
                Response.End();
            }
            else
            {
                if (Session["isEmployed"] != null)
                {
                    if ((bool)Session["isEmployed"] != true)
                    {
                        DisableNavigationControls();
                    }
                }
                else
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
                    connection.Open();
                    SqlCommand getEmployementStatus = new SqlCommand("Select isEmployed from [SimpleUser] where email=@email", connection);
                    getEmployementStatus.Parameters.AddWithValue("email", Util.GetEmail(Request));
                    DataTable employementStatus = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(getEmployementStatus);
                    adapter.Fill(employementStatus);

                    if (employementStatus.Rows.Count > 0)
                    {
                        if (employementStatus.Rows[0]["isEmployed"].ToString() == true.ToString())
                        {
                            Session.Add("isEmployed", true);
                        }
                        else
                        {

                            Session.Add("isEmployed", false);
                            DisableNavigationControls();
                        }
                    }
                    else
                    {
                        Response.Redirect("~/error.aspx");
                    }
                }
            }

        }
        if (Request.Cookies["profileURL"] == null)
        {
            profilePic.Src = "~/Uploads/ProfilePictures/profilePic.png";
        }
        else
        {
            profilePic.Src = Request.Cookies["profileURL"].Value;
            
        }
        
    }
    protected void DisableNavigationControls()
    {
        ContentPlaceHolder ct = (ContentPlaceHolder)Page.Master.Master.FindControl("MainContent");
        ((WebControl)ct.FindControl("Attendance")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("Attendance")).NavigateUrl = "";
        ((WebControl)ct.FindControl("Training")).CssClass += " disabled";
        ((HyperLink)ct.FindControl("Training")).NavigateUrl = "";
            }
}

