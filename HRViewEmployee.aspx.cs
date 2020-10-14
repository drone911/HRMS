using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using hrms;

public partial class HRViewEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string email = Request.QueryString["email"];
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getData = new SqlCommand("Select firstName, LastName, birthdate, profilepicture, employedHREmail, organisationRole, [from], mobileNumber, [to], Address, pincode, city, state, bloodGroup, qualification from [Employee], [SimpleUser], [User] where [Employee].[to] IS NULL and [Employee].[email]=[SimpleUser].[email] and [SimpleUser].[email]=[User].[email] and [User].[email]=@email", connection);
        getData.Parameters.AddWithValue("email", "temp1234@gmail.com");
        DataTable table = new DataTable();
        SqlDataAdapter dataAdapter = new SqlDataAdapter(getData);
        dataAdapter.Fill(table);
        if (table.Rows.Count > 0)
        {
            if (table.Rows[0]["employedHREmail"].ToString() != Util.GetEmail(Request))
            {
                ViewMultiView.SetActiveView(NotAuth);
            }
            else
            {
                ViewMultiView.SetActiveView(Auth);
                DataRow row = table.Rows[0];
                NameLabel.Text = row["firstName"] + " " + row["lastName"];
                PositionLabel.Text = row["organisationRole"] + "";
                EmployeeProfilePic.Src = "Uploads/ProfilePictures/" + row["profilepicture"];
                AddressLabel.Text = row["Address"] + "\n" + row["city"] + "(" + row["pincode"] + ")" + "\n" + row["state"];
                ContactLabel.Text += row["mobileNumber"];
                EmailLabel.Text += email;
                BloodGroupLabel.Text += row["bloodGroup"];
                QualificationLabel.Text += row["qualification"];
            }
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }

            }
}