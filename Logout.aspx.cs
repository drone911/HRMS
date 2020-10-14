using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.RemoveAll();
        string[] cookies= Request.Cookies.AllKeys;
        foreach (string CookieName in cookies)
        {
            Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(-1);
            
        }
        Response.Redirect("~/Home.aspx");
    }
}