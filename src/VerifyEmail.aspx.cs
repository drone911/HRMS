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

public partial class VerifyEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string verificationToken = Request.QueryString["token"];
        string email = Request.QueryString["email"];
        if (email == null) {
            email = "1";
        }
        if (verificationToken == null)
        {
            verificationToken = "1";
        }
        String dbcstring = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        SqlConnection sqlConnection = new SqlConnection(dbcstring);
        sqlConnection.Open();
        if (sqlConnection.State.ToString() == "Open")
        {
            SqlCommand sqlCommand = new SqlCommand("select email, verificationToken from [User] where email=@email", sqlConnection);

            sqlCommand.Parameters.AddWithValue("email", email);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if (verificationToken == (string)dt.Rows[0]["verificationToken"])
                {
                    SqlCommand updateVerified = new SqlCommand("update [User] set isEmailVerified=1 where email = @email", sqlConnection);
                    updateVerified.Parameters.AddWithValue("email", email);
                    updateVerified.ExecuteNonQuery();
                    DisplayLabel.Text = "Succesfully Verified, Redirecting to login page";
                    DisplayLabel.CssClass = "alert alert-success";
                    Util.TimeoutAndRedirect(Page, ConfigurationManager.AppSettings["domain"] + "Login.aspx");
                }
                else
                {
                    DisplayLabel.Text = "Not able to verify, Redirecting to Email Verification Page";
                    DisplayLabel.CssClass = "alert alert-danger";
                    Util.TimeoutAndRedirect(Page, ConfigurationManager.AppSettings["domain"] + "SendVerificationAgain.aspx");
                }
            }
            else
            {
                DisplayLabel.Text = "Not registered Email, Redirecting to Registration page";
                DisplayLabel.CssClass = "alert alert-danger";
                Util.TimeoutAndRedirect(Page, ConfigurationManager.AppSettings["domain"] + "UserRegistration.aspx");
            }
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }
    }
}