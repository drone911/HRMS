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

public partial class HRAddEmployee : System.Web.UI.Page
{
    private DataTable dataTable;
    private CheckBox[] checkBoxes;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["email"] != null)
            {
                EmailTextBox.Text = Request.QueryString["email"].ToString();
            }
            if (Request.QueryString["position"] != null)
            {
                PositionInput.Text = Request.QueryString["position"].ToString();
            }
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand selectEmployees = new SqlCommand("Select firstName, lastName, organisationRole, [Employee].[email] from [Employee], [User] where [Employee].email=[User].email and [Employee].[to] IS NULL and [Employee].isVerified=1 and [Employee].[employedHREmail] = @hremail", connection);
        selectEmployees.Parameters.AddWithValue("hremail", Util.GetEmail(Request));
        
        SqlDataAdapter adapter = new SqlDataAdapter(selectEmployees);

        dataTable = new DataTable();
        adapter.Fill(dataTable);

        checkBoxes = new CheckBox[dataTable.Rows.Count];
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            checkBoxes[i] = new CheckBox();
            checkBoxes[i].Checked = false;
            checkBoxes[i].Enabled = true;
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

            UpperHeirarchyTable.Rows.Add(tableRow);
        }

    }
    protected void AddEmployee_Click(object sender, EventArgs e)
    {
        string email = EmailTextBox.Text.Trim().ToLower();
        string position = PositionInput.Text;
        int positionHeirarchy = Convert.ToInt32(PositionTypeDropdown.SelectedValue);

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand checkAvailability = new SqlCommand("Select isEmployed from [SimpleUser] where email = @email", connection);
        checkAvailability.Parameters.AddWithValue("email", email);

        SqlDataAdapter adapter = new SqlDataAdapter(checkAvailability);
        DataTable table = new DataTable();
        adapter.Fill(table);
        bool sendMail = true;
        if (table.Rows.Count > 0)
        {
            if ((Boolean)table.Rows[0]["isEmployed"])
            {
                EmailLabel.Text = "User is already employed.";
                EmailLabel.CssClass = "invalid-input";
                sendMail = false;
            }
        }
        else
        {
            EmailLabel.Text = "No Such User, Please Check Email ID.";
            EmailLabel.CssClass = "invalid-input";
            sendMail = false;
        }
        if (sendMail)
        {
            SqlCommand getOrganisationDetails = new SqlCommand("Select OrganisationName from [HR] where email = @email", connection);
            getOrganisationDetails.Parameters.AddWithValue("email", Util.GetEmail(Request));
            adapter.SelectCommand = getOrganisationDetails;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                // change subject and body for better representation
                SqlCommand insertEmployee = new SqlCommand("Insert into [Employee](eID, employedHREmail, organisationRole, heirarchy, [from], email, isVerified, verificationToken) values(@eID, @employedHREmail, @organisationRole, @heirarchy, @from, @email, @isVerified, @verificationToken)", connection);
                String Token = Crypto.GenerateSalt(16);
                insertEmployee.Parameters.AddWithValue("eID", Crypto.GenerateSalt(8));
                insertEmployee.Parameters.AddWithValue("employedHREmail", Util.GetEmail(Request));
                insertEmployee.Parameters.AddWithValue("organisationRole", position);
                insertEmployee.Parameters.AddWithValue("heirarchy", positionHeirarchy);
                insertEmployee.Parameters.AddWithValue("from", DateTime.Today.ToString("yyyy-MM-dd"));
                insertEmployee.Parameters.AddWithValue("email", email);
                insertEmployee.Parameters.AddWithValue("isVerified", 0);
                insertEmployee.Parameters.AddWithValue("verificationToken", Token);
                insertEmployee.ExecuteNonQuery();
                string url = ConfigurationManager.AppSettings["domain"].ToString() + "verifyAddEmployee.aspx?email=" + email + "&token=" + Token;
                
                string subject = "Verification for position at HRMS";
                string body = "You have been added as " + position + "at " + table.Rows[0]["organisationName"].ToString() + ". Please Click on this link to verify position <a href = \"" + url + "\">verify your email</a>. If their is any descepancy contact corresponding HR at " + Util.GetEmail(Request);
                Util.SendEmail(email, subject, body);

                int addedCount = 0;
                string insertString = "Insert into [Heirarchy] Values ";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (checkBoxes[i].Enabled)
                    {
                        if (checkBoxes[i].Checked)
                        {
                            insertString += "('" + dataTable.Rows[i]["email"] + "','" + EmailTextBox.Text.ToString().ToLower() + "'),";
                            addedCount++;
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

                string[] param = new string[] { "Mail Send Succesfully", "2" };

                Util.CallJavascriptFunction(Page, "popout", param);
                Util.TimeoutAndRedirect(Page, ConfigurationManager.AppSettings["domain"] + "HRAddEmployee.aspx", 3);
            }
            else
            {
                Response.Redirect("~/error.aspx");
            }
        }
    }
}