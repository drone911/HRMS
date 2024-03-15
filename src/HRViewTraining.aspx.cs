using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using hrms;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRViewTraining : System.Web.UI.Page
{
    private string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        // implement auth of hr
        // right now database "where" condition provides auth control
        if (id == null)
        {
            Response.Redirect("~/HRViewTrainings.aspx");
            Response.End();
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        SqlCommand query = new SqlCommand("Select name, description, CAST(startDate as varchar(10)) as startdate, CAST(endDate as varchar(10)) as enddate from [Training] Where employerHREmail=@email and tID=@id", connection);
        query.Parameters.AddWithValue("id", id);
        query.Parameters.AddWithValue("email", Util.GetEmail(Request));

        SqlDataAdapter adapter = new SqlDataAdapter(query);

        DataTable detailsTable = new DataTable();
        adapter.Fill(detailsTable);

        if (detailsTable.Rows.Count <= 0)
        {
            Response.Redirect("~/HRViewTrainings.aspx");
            Response.End();
        }
        NameLabel.Text = detailsTable.Rows[0]["name"].ToString();
        StartDateLabel.Text = detailsTable.Rows[0]["startdate"].ToString();
        EndDateLabel.Text = detailsTable.Rows[0]["enddate"].ToString();
        DescriptionLabel.Text = detailsTable.Rows[0]["description"].ToString();
        DataTable empTable = new DataTable();
        query.CommandText = "Select [TrainingEmp].email as 'Email', CONCAT(firstName,' ' ,lastName) as 'Name', organisationRole as 'Position' from [TrainingEmp], [User], [Employee],[Training] where [Employee].email=[TrainingEmp].email and [User].email = [TrainingEmp].email and [Training].tID=[TrainingEmp].tID and [TrainingEmp].tID = @id and employerHREmail=@email";
        adapter.SelectCommand = query;
        adapter.Fill(empTable);

        TrainingTable.Rows.Clear();
        Util.AddTableHeaders(TrainingTable, empTable);

        for(int i =0; i<empTable.Rows.Count; i++)
        {
            DataRow row = empTable.Rows[i];
            TableRow tablerow = new TableRow();

            TableCell cell1 = new TableCell();
            cell1.Text = row["Email"].ToString();
            tablerow.Cells.Add(cell1);

            TableCell cell2 = new TableCell();
            cell2.Text = row["Name"].ToString();
            tablerow.Cells.Add(cell2);

            TableCell cell3 = new TableCell();
            cell3.Text = row["Position"].ToString();
            tablerow.Cells.Add(cell3);

            TrainingTable.Rows.Add(tablerow);
        }

    }
}
