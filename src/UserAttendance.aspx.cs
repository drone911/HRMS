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

public partial class UserAttendance : System.Web.UI.Page
{
    private SqlConnection connection;
    private CheckBox[] checkBoxes;
    private DataTable dataTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand selectEmployees = new SqlCommand("Select eID, firstName, lastName, organisationRole from [Employee], [User] where [Employee].email=[User].email and [Employee].email IN (Select lowerEmail as email from [Heirarchy] where upperEmail=@supervisorEmail) and [Employee].[to] IS NULL and [Employee].isVerified=1 and [Employee].[employedHREmail] IN (Select employedHREmail from [Employee] where [Employee].[to] IS NULL and [Employee].isVerified=1 and [Employee].email = @supervisorEmail)", connection);
        selectEmployees.Parameters.AddWithValue("supervisorEmail", Util.GetEmail(Request));
        
        SqlDataAdapter adapter = new SqlDataAdapter(selectEmployees);
        dataTable = new DataTable();
        adapter.Fill(dataTable);

        SqlCommand attendance = new SqlCommand("Select eID, [date] from Attendance where [date]=CAST(GETDATE() as date) and eID IN (Select eID from [Employee], [User] where [Employee].email=[User].email and [Employee].email IN (Select lowerEmail as email from [Heirarchy] where upperEmail=@supervisorEmail) and [Employee].[to] IS NULL and [Employee].isVerified=1 and [Employee].[employedHREmail] IN (Select employedHREmail from [Employee] where [Employee].[to] IS NULL and [Employee].isVerified=1 and [Employee].email = @supervisorEmail))", connection);
        attendance.Parameters.AddWithValue("supervisorEmail", Util.GetEmail(Request));

        DataTable attendanceTable = new DataTable();
        adapter.SelectCommand = attendance;
        adapter.Fill(attendanceTable);
        if (dataTable.Rows.Count > 0)
        {
            AttendanceMultiView.ActiveViewIndex = 0;
            checkBoxes = new CheckBox[dataTable.Rows.Count];
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                bool flag = false;
                checkBoxes[i] = new CheckBox();

                for (int j = 0; j < attendanceTable.Rows.Count; j++)
                {
                    if (dataTable.Rows[i]["eID"].ToString() == attendanceTable.Rows[j]["eID"].ToString())
                    {
                        flag = true;
                        break;
                    }

                }
                if (flag)
                {
                    checkBoxes[i].Checked = true;
                    checkBoxes[i].Enabled = false;
                }
                else
                {
                    checkBoxes[i].Checked = false;
                    checkBoxes[i].Enabled = true;

                }
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TableRow tableRow = new TableRow();
                Label nameLabel = new Label();
                TableCell nameCell = new TableCell();
                TableCell positionCell = new TableCell();
                TableCell checkboxCell = new TableCell();

                TableCell cell = new TableCell();

                nameLabel.Text = Util.CapFirstLetter(dataTable.Rows[i]["firstName"].ToString()) + " " + Util.CapFirstLetter(dataTable.Rows[i]["lastName"].ToString());
                cell.Controls.Add(nameLabel);
                tableRow.Cells.Add(cell);

                Label positionLabel = new Label();
                positionLabel.Text = Util.CapFirstLetter(dataTable.Rows[i]["organisationRole"].ToString());
                positionCell.Controls.Add(positionLabel);
                tableRow.Cells.Add(positionCell);

                checkboxCell.Controls.Add(checkBoxes[i]);
                tableRow.Cells.Add(checkboxCell);

                UnderAttendanceTable.Rows.Add(tableRow);
            }

        }
        else
        {

            AttendanceMultiView.ActiveViewIndex = 1;
        }

    }

    protected void Update_Click(object sender, EventArgs e)
    {
        int addedCount = 0;
        string insertString = "Insert into [Attendance](aID, date, eID) Values ";
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            if (checkBoxes[i].Enabled)
            {
                if (checkBoxes[i].Checked)
                {
                    insertString += "('" + System.Web.Helpers.Crypto.GenerateSalt(8) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + dataTable.Rows[i]["eID"] + "'),";
                    addedCount++;
                    checkBoxes[i].Enabled = false;
                }
            }
        }
        if (addedCount != 0)
        {
            insertString = insertString.Substring(0, insertString.Length - 1);
            insertString += ";";
            SqlCommand insert = new SqlCommand(insertString, connection);
            insert.ExecuteNonQuery();
        }
        if (IsPostBack)
        {
            string[] param = new string[] { addedCount + " Attendance Added", "2" };
            Util.CallJavascriptFunction(this, "popout", param);

        }

    }
}