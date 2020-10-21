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

public partial class HRAddTraining : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        insert.ExecuteNonQuery();

        string[] param = new string[] { "Training Added", "3" };
        Util.CallJavascriptFunction(this, "popout", param);
        alertlabel.Text = "Redirecting to Add The Employees";
        alertlabel.Visible = true;
        alertlabel.CssClass += " alert alert-success";
        string url = ConfigurationManager.AppSettings["domain"] + "HRViewTraining.aspx?id=" + HttpUtility.UrlEncode(id);
        Util.TimeoutAndRedirect(Page, url, 4);
    }
}