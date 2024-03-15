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

public partial class UserViewJobPostings : System.Web.UI.Page
{
    SqlConnection connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlDataAdapter adapter = new SqlDataAdapter();
        if (!IsPostBack)
        {
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
                adapter.SelectCommand = getCities;
                adapter.Fill(CitiesTable);
                foreach (DataRow row in CitiesTable.Rows)
                {
                    FilterCities.Items.Add(new ListItem(row["city"].ToString()));
                }
                Cache.Insert("Cities", CitiesTable, null, DateTime.Now.AddSeconds(30), TimeSpan.Zero);
            }
        }
        SqlCommand getDetails;

        if (FilterCities.SelectedValue != "none")
        {
            if (FilterExperience.SelectedValue != "none")
            {
                getDetails = new SqlCommand("Select jID, position, CAST(createdOn as varchar(10)) as 'createdOn', experience, city, state, organisationName from [Job] Inner Join [HR] on [Job].employerHREmail=[HR].email where city=@city and experience=@exp", connection);
                getDetails.Parameters.AddWithValue("exp", FilterExperience.SelectedValue);
                getDetails.Parameters.AddWithValue("city", FilterCities.SelectedValue);

            }
            else
            {
                getDetails = new SqlCommand("Select jID,position, CAST(createdOn as varchar(10)) as 'createdOn', experience, city, state, organisationName from [Job] Inner Join [HR] on [Job].employerHREmail=[HR].email where city=@city", connection);
                getDetails.Parameters.AddWithValue("city", FilterCities.SelectedValue);

            }
        }
        else
        {
            if (FilterExperience.SelectedValue != "none")
            {
                getDetails = new SqlCommand("Select jID,position, CAST(createdOn as varchar(10)) as 'createdOn', experience, city, state, organisationName from [Job] Inner Join [HR] on [Job].employerHREmail=[HR].email where experience=@exp", connection);
                getDetails.Parameters.AddWithValue("exp", FilterExperience.SelectedValue);

            }
            else
            {
                getDetails = new SqlCommand("Select jID,position, CAST(createdOn as varchar(10)) as 'createdOn', experience, city, state, organisationName from [Job] Inner Join [HR] on [Job].employerHREmail=[HR].email", connection);
            }
        }
        adapter.SelectCommand = getDetails;
        DataTable repeaterTable = new DataTable();
        adapter.Fill(repeaterTable);

        if (repeaterTable.Rows.Count > 0)
        {

            DetailsMultiView.SetActiveView(MainView);
            JobRepeater.DataSource = repeaterTable;
            JobRepeater.DataBind();
        }
        else
        {
            DetailsMultiView.SetActiveView(NoDetailsView);
        }
    }
}