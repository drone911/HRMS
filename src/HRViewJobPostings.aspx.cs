using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using hrms;
public partial class HRViewJobPostings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string exp = FilterJobs.SelectedValue;
        string status = FilterStatus.SelectedValue;
        SqlCommand query;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

        if (exp == "none")
        {
            if (status == "none")
            {
                query = new SqlCommand("Select jID, position as Position, openings as Openings, status as Status, experience as 'Applier Type', CAST(createdOn as VARCHAR(10)) as 'Started From' from [Job] where employerHREmail=@email");
                query.Parameters.AddWithValue("email", Util.GetEmail(Request));
            }
            else
            {
                query = new SqlCommand("Select jID, position as Position, openings as Openings, status as Status, experience as 'Applier Type', CAST(createdOn as VARCHAR(10)) as 'Started From' from [Job] where status=@status and employerHREmail=@email");
                query.Parameters.AddWithValue("status", status);
                query.Parameters.AddWithValue("email", Util.GetEmail(Request));
            }
        }
        else
        {
            if (status == "none")
            {

                query = new SqlCommand("Select jID, position as Position, openings as Openings, status as Status, experience as 'Applier Type', CAST(createdOn as VARCHAR(10)) as 'Started From' from [Job] where experience=@exp and employerHREmail=@email");
                query.Parameters.AddWithValue("email", Util.GetEmail(Request));
                query.Parameters.AddWithValue("exp", exp);

            }
            else
            {

                query = new SqlCommand("Select jID, position as Position, openings as Openings, status as Status, experience as 'Applier Type', CAST(createdOn as VARCHAR(10)) as 'Started From' from [Job] where status=@status and experience=@exp and employerHREmail=@email");
                query.Parameters.AddWithValue("email", Util.GetEmail(Request));
                query.Parameters.AddWithValue("status", status);

                query.Parameters.AddWithValue("exp", exp);

            }
        }
        connection.Open();
        query.Connection = connection;

        SqlDataAdapter adapter = new SqlDataAdapter(query);
        DataTable datatable = new DataTable();
        adapter.Fill(datatable);
        DataTable tempTable = new DataTable();
        tempTable = datatable.Copy();

        datatable.Columns.RemoveAt(0);

        JobTable.Rows.Clear();
        TableHeaderCell[] ExtraCells = new TableHeaderCell[1];
        TableHeaderCell viewCell = new TableHeaderCell();

        viewCell.Text = "View";
        viewCell.Scope = TableHeaderScope.Column;
        
        ExtraCells[0] = viewCell;
        Util.AddTableHeaders(JobTable, datatable, ExtraCells);

        for (int i = 0; i < datatable.Rows.Count; i++)
        {
            DataRow row = datatable.Rows[i];

            TableRow tableRow = new TableRow();
            tableRow.CssClass = "table-row";
            foreach (DataColumn col in datatable.Columns)
            {
                if (col.ColumnName != "jID")
                {
                    TableCell tempcell = new TableCell();
                    tempcell.Text = row[col].ToString();
                    tableRow.Cells.Add(tempcell);

                }
            }
            HyperLink link = new HyperLink();
            link.Text = "View Details";
            string temp = ConfigurationManager.AppSettings["domain"] + "HRViewJobPosting.aspx?id=" + HttpUtility.UrlEncode(tempTable.Rows[i]["jID"].ToString());
            link.NavigateUrl = temp;

            link.CssClass = "btn btn-primary";
            TableCell cell = new TableCell();
            cell.Controls.Add(link);
            cell.CssClass = "no-search";

            tableRow.Cells.Add(cell);

            JobTable.Rows.Add(tableRow);
        }
    }
}