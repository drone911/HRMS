using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hrms;

public partial class UserViewJobPosting : System.Web.UI.Page
{
    private SqlConnection connection;
    private string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (id == null)
        {
            Response.Redirect("~/UserViewJobPostings.aspx");
            Response.End();
        }
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();

        SqlCommand getStatusOfUser = new SqlCommand("Select isEmployed, resume from [SimpleUser] where email=@email", connection);
        getStatusOfUser.Parameters.AddWithValue("email", Util.GetEmail(Request));
        DataTable userTable = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter(getStatusOfUser);
        adapter.Fill(userTable);

        if (userTable.Rows[0]["isEmployed"].ToString() == true.ToString())
        {
            DisableApplyNowButton("employed");
        }
        else
        {
            SqlCommand getAppliedStatus = new SqlCommand("Select jID from [JobApply] Where jID=@id and email=@email", connection);
            getAppliedStatus.Parameters.AddWithValue("email", Util.GetEmail(Request));
            getAppliedStatus.Parameters.AddWithValue("id", id);
            DataTable appliedTable = new DataTable();
            adapter.SelectCommand = getAppliedStatus;
            adapter.Fill(appliedTable);
            if (appliedTable.Rows.Count > 0)
            {
                DisableApplyNowButton("applied");
            }

        }

        SqlCommand getDetails = new SqlCommand("Select * from [Job] Where jID=@id and status='Open'", connection);
        getDetails.Parameters.AddWithValue("id", id);
        adapter.SelectCommand = getDetails;
        DataTable detailsTable = new DataTable();

        adapter.Fill(detailsTable);

        SqlCommand getOrgDetails = new SqlCommand("Select firstName, lastName, organisationName, organisationAddress,organisationCity,organisationState from [HR],[User] where [User].email=[HR].email and [HR].email=@hremail", connection);

        if (detailsTable.Rows.Count > 0)
        {
            DataRow row = detailsTable.Rows[0];

            getOrgDetails.Parameters.AddWithValue("hremail", row["employerHREmail"].ToString());

            PositionLabel.Text = row["position"].ToString();
            OpeningsLabel.Text = row["openings"].ToString();
            HREmailLabel.Text = row["employerHREmail"].ToString();
            CreatedOnLabel.Text = row["createdOn"].ToString().Substring(0, 10);
            DesctiptionLabel.InnerText = row["description"].ToString();

            DataTable orgTable = new DataTable();
            adapter.SelectCommand = getOrgDetails;

            adapter.Fill(orgTable);

            DataRow orgRow = orgTable.Rows[0];

            OrganisationLabel.Text = orgRow["organisationName"].ToString();
            OrganisationAddress.Text = orgRow["organisationAddress"].ToString() + "<br/>" + orgRow["organisationCity"].ToString() + ", " + orgRow["organisationState"].ToString();
            HRNameLabel.Text = Util.CapFirstLetter(orgRow["firstName"].ToString()) + " " + Util.CapFirstLetter(orgRow["lastName"].ToString());

            if (ViewState["showpopup"]!=null)
            {
                ViewState.Remove("showpopup");
                Util.CallJavascriptFunction(Page, "popout", new string[] { "Sucessfully Applied","3" });

            }
        }
        else
        {
            Response.Redirect("~/UserViewJobPostings.aspx");
        }
    }

    private void DisableApplyNowButton(string condition)
    {
        ApplyNowButton.Click -= ApplyNowButton_Click;
        if (condition == "employed")
        {
            ApplyNowButton.Visible = false;
        }
        else if (condition == "applied")
        {
            ApplyNowButton.Enabled = false;
            ApplyNowButton.Text = "Applied";
            ApplyNowButton.CssClass = "btn btn-success .disabled";
        }
    }

    protected void ApplyNowButton_Click(object sender, EventArgs e)
    {

        if (connection.State.ToString() != "Open")
        {
            connection.Open();
        }
        SqlCommand apply = new SqlCommand("Insert INTO [JobApply](jID, email) Values(@id, @email)", connection);
        apply.Parameters.AddWithValue("id", id);
        apply.Parameters.AddWithValue("email", Util.GetEmail(Request));
        apply.ExecuteNonQuery();
        ViewState["showpop"] = true;
        Page_Load(sender, e);
    }
}