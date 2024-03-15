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
public partial class UserTraining : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();

        SqlCommand getCurrent = new SqlCommand("Select name, description, startDate, endDate from [Training] INNER JOIN [TrainingEmp] on [Training].tID = [TrainingEmp].tID where endDate>=GETDATE() and [TrainingEmp].[email] = @email", connection);
        getCurrent.Parameters.AddWithValue("email", Util.GetEmail(Request));

        SqlDataAdapter adapter = new SqlDataAdapter(getCurrent);
        DataTable currTrainings = new DataTable();

        adapter.Fill(currTrainings);

        if (currTrainings.Rows.Count > 0)
        {
            CurrentTrainingMultiView.ActiveViewIndex = 0;
            DataRow row = currTrainings.Rows[0];
            TrainingNameLabel.Text = row["name"].ToString();
            TrainingDescription.Text = row["description"].ToString();
            TrainingStartDate.Text = Util.ConvertToReadableDate((DateTime)row["startDate"]);
            TrainingEndDate.Text = Util.ConvertToReadableDate((DateTime)row["endDate"]);
        }
        else
        {
            CurrentTrainingMultiView.ActiveViewIndex = 1;
        }

        SqlCommand getPrev = new SqlCommand("Select name as 'Training Name', startDate as 'Start Date', endDate as 'End Date',  employerHREmail as 'HR Email' from [Training] INNER JOIN [TrainingEmp] ON [Training].tID = [TrainingEmp].tID where email=@email and endDate<GETDATE()", connection);
        getPrev.Parameters.AddWithValue("email", Util.GetEmail(Request));
        
        adapter.SelectCommand = getPrev;
        DataTable prevTrainings = new DataTable();
        adapter.Fill(prevTrainings);
        PrevTrainingTable.Rows.Clear();

        if (prevTrainings.Rows.Count > 0)
        {
            PreviousTrainingMultiView.ActiveViewIndex = 0;
            Util.AddTableHeaders(PrevTrainingTable, prevTrainings);

            foreach (DataRow row in prevTrainings.Rows)
            {
                TableRow tableRow = new TableRow();
                tableRow.CssClass = "r";
                foreach (DataColumn col in prevTrainings.Columns)
                {
                    if (col.ColumnName.Contains("Date"))
                    {
                        TableCell tempcell = new TableCell();
                        tempcell.Text = Util.ConvertToReadableDate((DateTime)row[col]);
                        tableRow.Cells.Add(tempcell);

                    }
                    else
                    {
                        TableCell tempcell = new TableCell();
                        tempcell.Text = row[col].ToString();
                        tableRow.Cells.Add(tempcell);

                    }
                }
                PrevTrainingTable.Rows.Add(tableRow);
            }

        }
        else
        {
            PreviousTrainingMultiView.ActiveViewIndex = 1;
        }
    }
}