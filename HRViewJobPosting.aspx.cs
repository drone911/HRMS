using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Net;
using hrms;

public partial class HRViewJobPosting : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ApplierTable.Rows.Clear();

        string id = Request.QueryString["id"];
        if (id == null)
        {
            Response.Redirect("~/HRViewJobPostings.aspx");
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getDetails = new SqlCommand("Select * from [Job] where jId=@id", connection);
        getDetails.Parameters.AddWithValue("id", id);
        SqlDataAdapter adapter = new SqlDataAdapter(getDetails);
        DataTable detailsTable = new DataTable();
        adapter.Fill(detailsTable);

        if (detailsTable.Rows.Count > 0)
        {
            PositionLabel.Text = detailsTable.Rows[0]["position"].ToString();
            OpeningLabel.Text = detailsTable.Rows[0]["openings"].ToString();
            DescriptionLabel.Text = detailsTable.Rows[0]["description"].ToString();
            if (detailsTable.Rows[0]["experience"].ToString() == "entry")
            {
                ExperienceLabel.Text = "Entry Level/Intern";
            }
            else
            {
                ExperienceLabel.Text = "Experienced Professional";
            }
            FromLabel.Text = detailsTable.Rows[0]["createdOn"].ToString().Substring(0, 10);
            if (detailsTable.Rows[0]["status"].ToString() == "closed")
            {
                StatusLabel.Text = "Closed";
                StatusLabel.CssClass = "closed";
            }
            else
            {
                StatusLabel.Text = "Open";
                StatusLabel.CssClass = "open";
            }

            if (detailsTable.Rows[0]["status"].ToString() == "closed")
            {
                RemoveJob.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/HRViewJobPostings.aspx");
        }

        SqlCommand getAppliers = new SqlCommand("Select [SimpleUser].email, firstName, LastName, mobileNumber, resume from [SimpleUser],[User] where [SimpleUser].email=[User].email and  [SimpleUser].email IN (Select [JobApply].email from [JobApply] where jId=@id) and isEmployed=0", connection);
        getAppliers.Parameters.AddWithValue("id", id);
        adapter.SelectCommand = getAppliers;
        DataTable appliersTable = new DataTable();
        adapter.Fill(appliersTable);

        if (detailsTable.Rows[0]["status"].ToString() == "open")
        {
            TableHeaderRow headerRow = new TableHeaderRow();

            TableHeaderCell celll0 = new TableHeaderCell();
            celll0.Text = "Name";
            celll0.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll0);

            TableHeaderCell celll1 = new TableHeaderCell();
            celll1.Text = "Email";
            celll1.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll1);

            TableHeaderCell celll2 = new TableHeaderCell();
            celll2.Text = "Mobile Number";
            celll2.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll2);

            TableHeaderCell celll3 = new TableHeaderCell();
            celll3.Text = "View Resume";
            celll3.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll3);

            TableHeaderCell celll4 = new TableHeaderCell();
            celll4.Text = "Add as Employee";
            celll4.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll4);

            TableHeaderCell celll5 = new TableHeaderCell();
            celll5.Text = "Remove Candidate";
            celll5.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll5);


            ApplierTable.Rows.AddAt(0, headerRow);

            for(int i = 0; i<appliersTable.Rows.Count; i++)
            {
                DataRow row = appliersTable.Rows[i];
                TableRow tablerow = new TableRow();

                TableCell cell0 = new TableCell();
                cell0.Text = row["firstName"].ToString() + " "+  row["lastName"].ToString();
                tablerow.Cells.Add(cell0);

                TableCell cell1 = new TableCell();
                cell1.Text = row["email"].ToString();
                tablerow.Cells.Add(cell1);

                TableCell cell2 = new TableCell();
                cell2.Text = row["mobileNumber"].ToString();
                tablerow.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                Button viewResume = new Button();
                viewResume.Text = "View";
                viewResume.CommandArgument = row["resume"].ToString();
                viewResume.Command += new CommandEventHandler(this.ViewResumeButton_Clicked);
                viewResume.CssClass = "btn btn-primary";
                cell3.Controls.Add(viewResume);
                tablerow.Cells.Add(cell3);

                TableCell cell4 = new TableCell();
                HyperLink addlink = new HyperLink();
                addlink.NavigateUrl = ConfigurationManager.AppSettings["domain"] + "HRAddEmployee.aspx?email=" + row["email"].ToString() + "&position=" + detailsTable.Rows[0]["position"].ToString();
                addlink.CssClass = "btn btn-success";
                addlink.Text = "Add";
                cell4.Controls.Add(addlink);
                tablerow.Cells.Add(cell4);

                TableCell cell5 = new TableCell();
                Button removeCandidate = new Button();
                removeCandidate.Text = "Remove";
                removeCandidate.CommandArgument = row["email"].ToString();
                removeCandidate.Command += new CommandEventHandler(this.RemoveCandidateButton_Clicked);
                removeCandidate.CssClass = "btn btn-outline-danger";
                cell5.Controls.Add(removeCandidate);
                tablerow.Cells.Add(cell5);

                ApplierTable.Rows.Add(tablerow);
            }
        }
        else
        {
            TableHeaderRow headerRow = new TableHeaderRow();

            TableHeaderCell celll0 = new TableHeaderCell();
            celll0.Text = "Name";
            celll0.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll0);

            TableHeaderCell celll1 = new TableHeaderCell();
            celll1.Text = "Email";
            celll1.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll1);

            TableHeaderCell celll2 = new TableHeaderCell();
            celll2.Text = "Mobile Number";
            celll2.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll2);

            TableHeaderCell celll3 = new TableHeaderCell();
            celll3.Text = "View Resume";
            celll3.Scope = TableHeaderScope.Column;
            headerRow.Cells.Add(celll3);

            ApplierTable.Rows.AddAt(0, headerRow);

            for (int i = 0; i < appliersTable.Rows.Count; i++)
            {
                DataRow row = appliersTable.Rows[i];
                TableRow tablerow = new TableRow();

                TableCell cell0 = new TableCell();
                cell0.Text = row["firstName"].ToString() + " " + row["lastName"].ToString();
                tablerow.Cells.Add(cell0);

                TableCell cell1 = new TableCell();
                cell1.Text = row["email"].ToString();
                tablerow.Cells.Add(cell1);

                TableCell cell2 = new TableCell();
                cell2.Text = row["mobileNumber"].ToString();
                tablerow.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                Button viewResume = new Button();
                viewResume.Text = "View";
                viewResume.CommandArgument = row["resume"].ToString();
                viewResume.Command += new CommandEventHandler(this.ViewResumeButton_Clicked);
                viewResume.CssClass = "btn btn-primary";
                cell3.Controls.Add(viewResume);
                tablerow.Cells.Add(cell3);

                ApplierTable.Rows.Add(tablerow);
            }
        }
    }

    private void RemoveCandidateButton_Clicked(object sender, CommandEventArgs e)
    {
        string email = e.CommandArgument.ToString();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand remove = new SqlCommand("Delete From [JobApply] where jID=@id and email=@email", connection);
        remove.Parameters.AddWithValue("id", Request.QueryString["id"]);
        remove.Parameters.AddWithValue("email", email);
        remove.ExecuteNonQuery();
        Util.SendEmail(email, "Application Rejected at HRMS", "Your application has been rejected.");

        string[] param = new string[] { "Candidate Removed", "2" };
        Util.CallJavascriptFunction(this, "popout", param);
        this.Page_Load(sender, e);
    }

    protected void ViewResumeButton_Clicked(object sender, CommandEventArgs e)
    {
       
        string fileName = e.CommandArgument.ToString();
        if (fileName == null)
        {
            string[] param = new string[] { "No Resume Found", "3" };
            Util.CallJavascriptFunction(this, "popout", param);
        }
        else
        {
            string path = Server.MapPath("Uploads/simpleuser/" + fileName);
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(path);
            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
            else
            {
                string[] param = new string[] { "Cannot Open Resume", "3" };
                Util.CallJavascriptFunction(this, "popout", param);
            }
        }
    }
}