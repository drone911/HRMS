using hrms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web;
public partial class HRViewEmployees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string filter = FilterEmployees.SelectedValue;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        if (filter == "active")
        {
            SqlCommand fetchQuery = new SqlCommand("Select u.firstName as 'First Name', u.lastName as 'Last Name', e.organisationRole as Position, e.email as EmailAddress, e.[from] as 'Start Date' from [Employee] as [e], [User] as [u] where [e].[to] IS NULL and [e].[isVerified]=1 and [e].[email]=[u].[email] and [u].role='simpleuser' and [e].[employedHREmail]=@hremail order by [e].[heirarchy] ASC", connection);
            fetchQuery.Parameters.AddWithValue("hremail", Util.GetEmail(Request));
            SqlDataAdapter adapter = new SqlDataAdapter(fetchQuery);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);
            GridViewCommandColumn cmd = new GridViewCommandColumn();
            cmd.Caption = "View Employee";
            cmd.ShowDeleteButton = true;
            EmployeeGrid.DataSource = datatable;
            EmployeeGrid.DataBind();
            
            
            EmployeeGrid.Columns.Add(cmd);
        }
        else if (filter == "inactive")
        {
            SqlCommand fetchQuery = new SqlCommand("Select u.firstName as 'First Name', u.lastName as 'Last Name', e.organisationRole as Position, e.email as EmailAddress, e.[from] as 'Start Date', e.to as 'End Date' from [Employee] as [e], [User] as [u] where [e].[to] IS NOT NULL and [e].[isVerified]=1 and [e].[email]=[u].[email] and [u].role='simpleuser' and [e].[employedHREmail]=@hremail order by [e].[heirarchy] ASC", connection);
            fetchQuery.Parameters.AddWithValue("hremail", Util.GetEmail(Request));
            SqlDataAdapter adapter = new SqlDataAdapter(fetchQuery);
            DataTable table = new DataTable();
            adapter.Fill(table);
            EmployeeGrid.DataSource = table;
            EmployeeGrid.DataBind();
            EmployeeGrid.Columns.Add(new GridViewCommandColumn());
        }
        else if(filter == "notverified")
        {
            SqlCommand fetchQuery = new SqlCommand("Select u.firstName as 'First Name', u.lastName as 'Last Name', e.organisationRole as Position, e.email as EmailAddress, e.[from] as 'Start Date' from [Employee] as [e], [User] as [u] where [e].[to] IS NULL and [e].[isVerified]=0 and [e].[email]=[u].[email] and [u].role='simpleuser' and [e].[employedHREmail]=@hremail order by [e].[heirarchy] ASC", connection);
            fetchQuery.Parameters.AddWithValue("hremail", Util.GetEmail(Request));
            SqlDataAdapter adapter = new SqlDataAdapter(fetchQuery);
            DataTable table = new DataTable();
            adapter.Fill(table);
            EmployeeGrid.DataSource = table;
            EmployeeGrid.DataBind();
        }
    }



    protected void EmployeeGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        Response.Write(e.Keys[0]);
    }
}