using hrms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRAddTraining : System.Web.UI.Page
{
    private SqlConnection connection;
    private CheckBox[] checkBoxes;
    private DataTable dataTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand selectEmployees = new SqlCommand("Select [Employee].email, firstName, lastName, organisationRole from [Employee], [User] where [Employee].email=[User].email and [Employee].[to] IS NULL and [Employee].isVerified=1 and [Employee].[employedHREmail] = @hremail and [Employee].email NOT IN (Select email from [TrainingEmp], [Training] where [Training].endDate>@now and [TrainingEmp].tID=[Training].tID)", connection);
        selectEmployees.Parameters.AddWithValue("hremail", Util.GetEmail(Request));
        selectEmployees.Parameters.AddWithValue("now", DateTime.Now.ToString("yyyy-MM-dd"));
        SqlDataAdapter adapter = new SqlDataAdapter(selectEmployees);
        dataTable = new DataTable();
        adapter.Fill(dataTable);
        SelectionTable.Rows.Clear();
        checkBoxes = new CheckBox[dataTable.Rows.Count];
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            TableRow tableRow = new TableRow();
            Label nameLabel = new Label();
            TableCell nameCell = new TableCell();
            TableCell positionCell = new TableCell();
            TableCell checkboxCell = new TableCell();

            TableCell cell = new TableCell();

            nameLabel.Text = dataTable.Rows[i]["firstName"] + " " + dataTable.Rows[i]["lastName"];
            cell.Controls.Add(nameLabel);
            tableRow.Cells.Add(cell);

            Label positionLabel = new Label();
            positionLabel.Text = dataTable.Rows[i]["organisationRole"].ToString();
            positionCell.Controls.Add(positionLabel);
            tableRow.Cells.Add(positionCell);
            checkBoxes[i] = new CheckBox();
            checkBoxes[i].Checked = false;
            checkboxCell.Controls.Add(checkBoxes[i]);
            tableRow.Cells.Add(checkboxCell);

            SelectionTable.Rows.Add(tableRow);
        }

    }

    protected void AddTraining_Click(object sender, EventArgs e)
    {
        String[] date = StartDateCalender.Text.Split('-');

        var startdate = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]));

        date = EndDateCalender.Text.Split('-');

        var enddate = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]));

        if (startdate <= DateTime.Now)
        {
            StartDateLabel.Text = "*Cannot be in past.";
        }
        else
        {
            StartDateLabel.Text = "";
        }
        if (enddate <= DateTime.Now)
        {
            EndDateLabel.Text = "*Cannot be in past.";
            return;
        }
        else
        {
            EndDateLabel.Text = "";
        }
        if (enddate <= startdate)
        {
            EndDateLabel.Text = "*Should be greater than Start Date";
            return;
        }
        else
        {
            EndDateLabel.Text = "";
        }

        string name = NameTextbox.Text.Trim();
        string description = DescriptionTextbox.Text.Trim();

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand insert = new SqlCommand("insert into [Training](tID, startDate, endDate, employerHREmail, name, description) values(@id, @sd, @ed, @email, @name, @desc)", connection);
        string id = Crypto.GenerateSalt(8);
        insert.Parameters.AddWithValue("id", id);
        insert.Parameters.AddWithValue("sd", startdate.ToString("yyyy/MM/dd"));
        insert.Parameters.AddWithValue("ed", enddate.ToString("yyyy/MM/dd"));
        insert.Parameters.AddWithValue("email", Util.GetEmail(Request));
        insert.Parameters.AddWithValue("name", name);
        insert.Parameters.AddWithValue("desc", description);

        int addedCount = 0;
        string insertString = "Insert into [TrainingEmp](tID, email) Values ";
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            if (checkBoxes[i].Checked)
            {
                insertString += "('" + id + "','" + dataTable.Rows[i]["email"] + "'),";
                addedCount++;
            }

        }
        if (addedCount != 0)
        {
            insert.ExecuteNonQuery();
            insertString = insertString.Substring(0, insertString.Length - 1);
            insertString += ";";
            SqlCommand insertTrainingEmp = new SqlCommand(insertString, connection);
            insertTrainingEmp.ExecuteNonQuery();
            NameTextbox.Text = "";
            DescriptionTextbox.Text = "";
            StartDateCalender.Text = "Select Date";
            EndDateCalender.Text = "Select Date";

            string[] param = new string[] { "Training Added", "4" };
            Util.CallJavascriptFunction(this, "popout", param);

            this.Page_Load(sender, e);
        }
        else
        {
            string[] param1 = new string[] { "No Employee Selected", "4" };
            Util.CallJavascriptFunction(this, "popout", param1);
        }

    }
}