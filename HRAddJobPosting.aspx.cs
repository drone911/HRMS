using hrms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRAddJobPosting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void AddJob_Click(object sender, EventArgs e)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand insertQuery = new SqlCommand("insert into [Job](jID, position, openings, description, experience, createdOn, status, employerHREmail) values(@jID, @position, @openings, @description, @experience, @createdOn, @status, @employerHREmail)", connection);
        insertQuery.Parameters.AddWithValue("jID", Crypto.GenerateSalt(8));
        insertQuery.Parameters.AddWithValue("position", PositionTextbox.Text.Trim());
        insertQuery.Parameters.AddWithValue("openings", OpeningTextbox.Text.Trim());
        insertQuery.Parameters.AddWithValue("description", DescriptionTextbox.Text);
        insertQuery.Parameters.AddWithValue("experience", ExperienceDropdown.SelectedValue);
        insertQuery.Parameters.AddWithValue("createdOn", DateTime.Now.ToString("yyyy/MM/dd"));
        insertQuery.Parameters.AddWithValue("status", "open");
        insertQuery.Parameters.AddWithValue("employerHREmail", Util.GetEmail(Request));

        insertQuery.ExecuteNonQuery();
        string[] param = new string[] { "Job Added", "3" };
        PositionTextbox.Text = "";
        OpeningTextbox.Text = "";
        DescriptionTextbox.Text = "";
        ExperienceDropdown.SelectedItem.Selected = false;
        ExperienceDropdown.Items[0].Selected= true;


        Util.CallJavascriptFunction(Page, "popout", param);

    }
}