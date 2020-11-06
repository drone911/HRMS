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

public partial class ModeratorViewHRs : System.Web.UI.Page
{
    SqlConnection connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        string filter = FilterHRs.SelectedValue;
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand fetchQuery;
        if (filter == "notverified")
        {
            fetchQuery = new SqlCommand("Select firstName as 'First Name', lastName as 'Last Name', [User].email as EmailAddress, organisationName as 'Organisation' from [User] INNER JOIN [HR] on [User].email = [HR].email where isVerified=0", connection);

        }
        else
        {
            fetchQuery = new SqlCommand("Select firstName as 'First Name', lastName as 'Last Name', [User].email as EmailAddress, organisationName as 'Organisation' from [User] INNER JOIN [HR] on [User].email = [HR].email where isVerified=1", connection);
        }
        SqlDataAdapter adapter = new SqlDataAdapter(fetchQuery);
        DataTable datatable = new DataTable();
        adapter.Fill(datatable);

        HRTable.Rows.Clear();

        TableHeaderCell[] ExtraCells = new TableHeaderCell[1];
        TableHeaderCell ViewDetails = new TableHeaderCell();

        ExtraCells[0] = ViewDetails;

        Util.AddTableHeaders(HRTable, datatable, ExtraCells);

        foreach (DataRow row in datatable.Rows)
        {
            TableRow tableRow = new TableRow();
            tableRow.CssClass = "r";
            foreach (DataColumn col in datatable.Columns)
            {
                TableCell tempcell = new TableCell();
                tempcell.Text = Util.CapFirstLetter(row[col].ToString());
                tableRow.Cells.Add(tempcell);
            }
            HyperLink link = new HyperLink();
            link.Text = "View Details";
            link.NavigateUrl = ConfigurationManager.AppSettings["domain"] + "ModeratorViewHR.aspx?email=" + HttpUtility.UrlEncode(row["EmailAddress"].ToString());
            link.CssClass = "btn btn-primary";
            TableCell cell = new TableCell();
            cell.Controls.Add(link);

            tableRow.Cells.Add(cell);

            HRTable.Rows.Add(tableRow);
        }
    }
}