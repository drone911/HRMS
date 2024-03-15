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

public partial class ModeratorViewModerators : System.Web.UI.Page
{
    SqlConnection connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        string filter = FilterModerators.SelectedValue;
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        if (filter == "notverified")
        {
            SqlCommand fetchQuery = new SqlCommand("Select firstName as 'First Name', lastName as 'Last Name', [User].email as EmailAddress from [User] INNER JOIN [Moderator] on [User].email = [Moderator].email where isVerified=0", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(fetchQuery);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);

            ModeratorTable.Rows.Clear();

            TableHeaderCell[] ExtraCells = new TableHeaderCell[1];
            TableHeaderCell Verify = new TableHeaderCell();
            
            ExtraCells[0] = Verify;
            
            Util.AddTableHeaders(ModeratorTable, datatable, ExtraCells);

            foreach (DataRow row in datatable.Rows)
            {
                TableRow tableRow = new TableRow();
                tableRow.CssClass = "r";
                foreach (DataColumn col in datatable.Columns)
                {
                    TableCell tempcell = new TableCell();
                    tempcell.Text = row[col].ToString();
                    tableRow.Cells.Add(tempcell);
                }
                Button verifyBtn = new Button();
                verifyBtn.Text = "Verify";
                verifyBtn.CssClass = "btn btn-success";
                verifyBtn.CommandArgument = row["EmailAddress"].ToString();
                verifyBtn.Command += VerifyBtn_Command;

                TableCell cell = new TableCell();
                cell.Controls.Add(verifyBtn);
                cell.CssClass = "no-search";

                
                tableRow.Cells.Add(cell);
                
                ModeratorTable.Rows.Add(tableRow);
            }

        }
        else
        {
            SqlCommand fetchQuery = new SqlCommand("Select firstName as 'First Name', lastName as 'Last Name', [User].email as 'Email Address' from [User] INNER JOIN [Moderator] on [User].email=[Moderator].email where [Moderator].isVerified=1", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(fetchQuery);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);

            ModeratorTable.Rows.Clear();

            Util.AddTableHeaders(ModeratorTable, datatable);

            foreach (DataRow row in datatable.Rows)
            {
                TableRow tableRow = new TableRow();
                foreach (DataColumn col in datatable.Columns)
                {
                    TableCell tempcell = new TableCell();
                    tempcell.Text = Util.CapFirstLetter(row[col].ToString());
                    tableRow.Cells.Add(tempcell);
                }
                ModeratorTable.Rows.Add(tableRow);
            }

        }
    }

    private void VerifyBtn_Command(object sender, CommandEventArgs e)
    {
        if (connection.State.ToString() != "Open")
        {
            connection.Open();
        }
        SqlCommand updateMod = new SqlCommand("Update [Moderator] set isVerified=1 where email=@email", connection);
        updateMod.Parameters.AddWithValue("email", e.CommandArgument.ToString());

        updateMod.ExecuteNonQuery();

        Util.CallJavascriptFunction(this, "popout", new string[] { "Verified Moderator", "3" });
        Page_Load(sender, (EventArgs)e);
    }
}
