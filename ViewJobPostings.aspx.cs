using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRViewJobs : System.Web.UI.Page
{
    SqlConnection connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        if (Cache["Cities"] != null)
        {
            foreach (DataRow row in ((DataTable)Cache["Cities"]).Rows)
            {
                FilterCities.Items.Add(new ListItem(row["city"].ToString()));
            }
        }
        else
        {
            SqlCommand getCities = new SqlCommand("Select DISTINCT city from [Job],[HR] where [Job].employerHREmail=[HR].email", connection);
            DataTable CitiesTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(getCities);
            adapter.Fill(CitiesTable);
            foreach(DataRow row in CitiesTable.Rows)
            {
                FilterCities.Items.Add(new ListItem(row["city"].ToString()));
            }
            Cache.Insert("Cities", CitiesTable, null, DateTime.Now.AddSeconds(10), TimeSpan.FromSeconds(5));
        }

    }
}