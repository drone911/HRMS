using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using hrms;

public partial class HRViewTrainings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string status = TrainingStatus.SelectedValue;
        SqlCommand query, getCount;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);

        if (status == "none")
        {
            query = new SqlCommand("Select tID,name as Name, CAST(startDate as varchar(10)) as 'Start Date', CAST(endDate as varchar(10)) as 'End Date' from [Training] Where employerHREmail=@email", connection);
            query.Parameters.AddWithValue("email", Util.GetEmail(Request));

            getCount = new SqlCommand("Select COUNT(email) from [TrainingEmp] where tID IN (Select tID from [Training] Where employerHREmail=@email) group by tID", connection);
            getCount.Parameters.AddWithValue("email", Util.GetEmail(Request));
        }
        else
        {
            if (status == "ongoing")
            {
                query = new SqlCommand("Select tID, Name, CAST(startDate as varchar(10)) as 'Start Date', CAST(endDate as varchar(10)) as 'End Date' from [Training] Where employerHREmail=@email and endDate >= @date", connection);
                query.Parameters.AddWithValue("email", Util.GetEmail(Request));
                query.Parameters.AddWithValue("date", DateTime.Now.ToString("yyyy/MM/dd"));
                getCount = new SqlCommand("Select COUNT(email) from [TrainingEmp] where tID IN (Select tID from [Training] Where employerHREmail=@email and endDate >= @date) group by tID", connection);
                getCount.Parameters.AddWithValue("email", Util.GetEmail(Request));
                getCount.Parameters.AddWithValue("date", DateTime.Now.ToString("yyyy/MM/dd"));
            }
            else
            {
                query = new SqlCommand("Select tID, Name, CAST(startDate as varchar(10)) as 'Start Date', CAST(endDate as varchar(10)) as 'End Date' from [Training] Where employerHREmail=@email and endDate < @date", connection);
                query.Parameters.AddWithValue("email", Util.GetEmail(Request));
                query.Parameters.AddWithValue("date", DateTime.Now.ToString("yyyy/MM/dd"));
                getCount = new SqlCommand("Select COUNT(email) from [TrainingEmp] where tID IN (Select tID from [Training] Where employerHREmail=@email and endDate < @date) group by tID", connection);
                getCount.Parameters.AddWithValue("email", Util.GetEmail(Request));
                getCount.Parameters.AddWithValue("date", DateTime.Now.ToString("yyyy/MM/dd"));
            }
        }
        connection.Open();
        SqlDataAdapter adapter = new SqlDataAdapter(query);
        DataTable trainingDataTable = new DataTable();
        adapter.Fill(trainingDataTable);

        DataTable tempTable = new DataTable();
        tempTable = trainingDataTable.Copy();

        trainingDataTable.Columns.RemoveAt(0);

        TrainingTable.Rows.Clear();
        TableHeaderCell[] ExtraCells = new TableHeaderCell[2];
        TableHeaderCell viewCell = new TableHeaderCell();
        TableHeaderCell countCell = new TableHeaderCell();

        countCell.Text = "# of Trainees";
        countCell.Scope = TableHeaderScope.Column;

        viewCell.Text = "View";
        viewCell.Scope = TableHeaderScope.Column;

        ExtraCells[0] = countCell;
        ExtraCells[1] = viewCell;
        Util.AddTableHeaders(TrainingTable, trainingDataTable, ExtraCells);


        adapter.SelectCommand = getCount;
        DataTable countTable = new DataTable();

        adapter.Fill(countTable);

        for (int i = 0; i < trainingDataTable.Rows.Count; i++)
        {
            DataRow row = trainingDataTable.Rows[i];

            TableRow tableRow = new TableRow();
            tableRow.CssClass = "table-row";
            foreach (DataColumn col in trainingDataTable.Columns)
            {
                TableCell tempcell = new TableCell();
                tempcell.Text = row[col].ToString();
                tableRow.Cells.Add(tempcell);

            }

            TableCell cell1 = new TableCell();
            cell1.Text = countTable.Rows[i][0].ToString();
            tableRow.Cells.Add(cell1);

            HyperLink link = new HyperLink();
            link.Text = "View Details";
            string temp = ConfigurationManager.AppSettings["domain"] + "HRViewTraining.aspx?id=" + HttpUtility.UrlEncode(tempTable.Rows[i]["tID"].ToString());
            link.NavigateUrl = temp;

            link.CssClass = "btn btn-primary";
            TableCell cell = new TableCell();
            cell.Controls.Add(link);
            cell.CssClass = "no-search";
            tableRow.Cells.Add(cell);

            TrainingTable.Rows.Add(tableRow);
        }
    }
}