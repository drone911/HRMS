using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Helpers;
using hrms;
using System.Web.UI.HtmlControls;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        EmailLabel.Text = "";
        BirthdayLabel.Text = "";
        AlertLabel.Visible = false;

    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        EmailLabel.Text = "";
        BirthdayLabel.Text = "";
        AlertLabel.Visible = false;

        String email = EmailInput.Text.Trim().ToLower();
        String dbcstring = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        SqlConnection sqlConnection = new SqlConnection(dbcstring);
        sqlConnection.Open();
        if (sqlConnection.State.ToString() == "Open")
        {
            SqlCommand sqlCommand = new SqlCommand("select email from [User] where email=@email", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("email", email));
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable ds = new DataTable();
            dataAdapter.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                EmailLabel.Text = "**Email already register, please register a from different email address.";
            }
            else
            {
                String[] date = FlatpickrCalender.Text.Split('-');
                var birthdate = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]));
                if (DateTime.Now.Year - birthdate.Year < 18)
                {
                    BirthdayLabel.Text = "**age should be greater than 18";
                }
                else
                {
                    string verificationToken = Crypto.GenerateSalt(8);
                    string salt = Crypto.GenerateSalt(16);

                    string tokenisedUrl = ConfigurationManager.AppSettings["domain"] + "VerifyEmail.aspx?token=" + verificationToken + "&email=" + email;
                    String emailBody = "Please follow the link to <a href = \"" + tokenisedUrl + "\">verify your email</a>";

                    // change email body in future
                    Util.SendEmail(email, "Verification mail from HR Management Site", emailBody);
                    SqlCommand insertCommand = new SqlCommand("Insert into [User](email, firstName, lastName, isEmailVerified, verificationToken, birthdate, role, hashedPassword, salt) values(@email, @firstName, @lastName, 0, @verificationToken, @birthdate, @role, @hashedPassword, @salt)", sqlConnection);
                    insertCommand.Parameters.AddWithValue("email", email);
                    insertCommand.Parameters.AddWithValue("firstName", FirstNameInput.Text.Trim().ToLower());
                    insertCommand.Parameters.AddWithValue("lastName", LastNameInput.Text.Trim().ToLower());
                    insertCommand.Parameters.AddWithValue("verificationToken", verificationToken);
                    insertCommand.Parameters.AddWithValue("birthdate", birthdate.ToShortDateString());
                    insertCommand.Parameters.AddWithValue("role", RoleInput.SelectedValue.Trim().ToLower());
                    insertCommand.Parameters.AddWithValue("hashedPassword", Crypto.HashPassword(salt + PasswordInput.Text));
                    insertCommand.Parameters.AddWithValue("salt", salt);
                    
                    insertCommand.ExecuteNonQuery();
                    AlertLabel.Visible = true;
                    AlertLabel.Text = "Succesfully Registered, Check you email for verification link, redirecting to login...";
                    string timeOutUrl = ConfigurationManager.AppSettings["domain"] + "Login.aspx";
                    Util.TimeoutAndRedirect(Page, timeOutUrl, 4);

                }

            }
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }
    }
}