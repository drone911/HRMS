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
public partial class HRViewEmployees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string filter = FilterEmployees.SelectedValue;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        if (filter == "active")
        {
            SqlCommand fetchQuery = new SqlCommand("Select u.firstName as 'First Name', u.lastName as 'Last Name', e.organisationRole as Position, e.email as EmailAddress, CAST(e.[from] as VARCHAR(10)) as 'Start Date' from [Employee] as [e], [User] as [u] where [e].[to] IS NULL and [e].[isVerified]=1 and [e].[email]=[u].[email] and [u].role='simpleuser' and [e].[employedHREmail]=@hremail order by [e].[heirarchy] ASC", connection);
            fetchQuery.Parameters.AddWithValue("hremail", Util.GetEmail(Request));
            SqlDataAdapter adapter = new SqlDataAdapter(fetchQuery);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);

            //EmployeeGrid.DataSource = datatable;
            //EmployeeGrid.DataBind();

            EmployeeTable.Rows.Clear();

            TableHeaderCell[] ExtraCells = new TableHeaderCell[2];
            TableHeaderCell viewCell = new TableHeaderCell();
            
            viewCell.Text = "View";
            viewCell.Scope = TableHeaderScope.Column;
            TableHeaderCell removeCell = new TableHeaderCell();
            removeCell.Text = "Remove";
            removeCell.Scope = TableHeaderScope.Column;

            ExtraCells[0] = viewCell;
            ExtraCells[1] = removeCell;
            
            Util.AddTableHeaders(EmployeeTable, datatable, ExtraCells);

            foreach(DataRow row in datatable.Rows)
            {
                TableRow tableRow = new TableRow();
                tableRow.CssClass = "r";
                foreach (DataColumn col in datatable.Columns)
                {
                    TableCell tempcell = new TableCell();
                    tempcell.Text = row[col].ToString();
                    tableRow.Cells.Add(tempcell);
                }
                HyperLink link = new HyperLink();
                link.Text = "View Details";
                link.NavigateUrl = ConfigurationManager.AppSettings["domain"] + "HRViewEmployee.aspx?email=" + row["EmailAddress"];
                link.CssClass = "btn btn-primary";
                TableCell cell = new TableCell();
                cell.Controls.Add(link);
                cell.CssClass = "no-search";

                Button remove = new Button();
                remove.Text = "Remove Employee";
                remove.CssClass = "btn btn-outline-danger";
                
                TableCell cell1 = new TableCell();
                cell1.Controls.Add(remove);
                cell1.CssClass = "no-search";

                tableRow.Cells.Add(cell);
                tableRow.Cells.Add(cell1);

                EmployeeTable.Rows.Add(tableRow);
            }

        }
        else if (filter == "inactive")
        {
            SqlCommand fetchQuery = new SqlCommand("Select u.firstName as 'First Name', u.lastName as 'Last Name', e.organisationRole as Position, e.email as EmailAddress, CAST(e.[from] as VARCHAR(10)) as 'Start Date', CAST(e.[to] as VARCHAR(10)) as 'End Date' from [Employee] as [e], [User] as [u] where [e].[to] IS NOT NULL and [e].[isVerified]=1 and [e].[email]=[u].[email] and [u].role='simpleuser' and [e].[employedHREmail]=@hremail order by [e].[heirarchy] ASC", connection);
            fetchQuery.Parameters.AddWithValue("hremail", Util.GetEmail(Request));
            SqlDataAdapter adapter = new SqlDataAdapter(fetchQuery);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);

            EmployeeTable.Rows.Clear();

            Util.AddTableHeaders(EmployeeTable, datatable);

            foreach (DataRow row in datatable.Rows)
            {
                TableRow tableRow = new TableRow();
                foreach (DataColumn col in datatable.Columns)
                {
                    TableCell tempcell = new TableCell();
                    tempcell.Text = row[col].ToString();
                    tableRow.Cells.Add(tempcell);
                }
                EmployeeTable.Rows.Add(tableRow);
            }

        }
        else if(filter == "notverified")
        {
            SqlCommand fetchQuery = new SqlCommand("Select u.firstName as 'First Name', u.lastName as 'Last Name', e.organisationRole as Position, e.email as EmailAddress from [Employee] as [e], [User] as [u] where [e].[to] IS NULL and [e].[isVerified]=0 and [e].[email]=[u].[email] and [u].role='simpleuser' and [e].[employedHREmail]=@hremail order by [e].[heirarchy] ASC", connection);
            fetchQuery.Parameters.AddWithValue("hremail", Util.GetEmail(Request));
            SqlDataAdapter adapter = new SqlDataAdapter(fetchQuery);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);

            EmployeeTable.Rows.Clear();
            
            Util.AddTableHeaders(EmployeeTable, datatable);

            foreach (DataRow row in datatable.Rows)
            {
                TableRow tableRow = new TableRow();
                foreach (DataColumn col in datatable.Columns)
                {
                    TableCell tempcell = new TableCell();
                    tempcell.Text = row[col].ToString();
                    tableRow.Cells.Add(tempcell);
                }
                EmployeeTable.Rows.Add(tableRow);
            }

        }
    }
    
}