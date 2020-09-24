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

public partial class SendVerification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void SendVerification_Click(object sender, EventArgs e)
    {
        EmailLabel.Text = "";
        RegisterButton.Visible = false;
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        sqlConnection.Open();

        string email = EmailTextBox.Text.Trim().ToLower();
        if (sqlConnection.State.ToString() == "Open")
        {
            SqlCommand sqlCommand = new SqlCommand("select email, isEmailVerified from [User] where email=@email", sqlConnection);

            sqlCommand.Parameters.AddWithValue("email", email);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if ((Boolean)dt.Rows[0]["isEmailVerified"])
                {
                    EmailLabel.Text = "Email already verified, Redirecting to login page";
                    EmailLabel.CssClass = "text-success";
                    Util.TimeoutAndRedirect(Page, ConfigurationManager.AppSettings["domain"] + "Login.aspx");
                }
                else
                {
                    string verificationToken = Crypto.GenerateSalt(8);
                    SqlCommand updateVerificationToken = new SqlCommand("update [User] set verificationToken=@token where email = @email", sqlConnection);
                    updateVerificationToken.Parameters.AddWithValue("token", verificationToken);
                    updateVerificationToken.Parameters.AddWithValue("email", email);
                    string tokenisedUrl = ConfigurationManager.AppSettings["domain"] + "VerifyEmail.aspx?token=" + verificationToken + "&email=" + email;
                    String emailBody = "Please follow the link to <a href = \"" + tokenisedUrl + "\">verify your email</a>";

                    // change email body in future
                    Util.SendEmail(email, "Verification mail from HR Management Site", emailBody);
                    updateVerificationToken.ExecuteNonQuery();
                    EmailLabel.CssClass = "text-success";
                    EmailLabel.Text = "Successfully send verification email, go check your mail";
                    Util.TimeoutAndRedirect(Page, ConfigurationManager.AppSettings["domain"] + "Login.aspx", 5);

                }
                }
            else
            {
                EmailLabel.Text = "*Not a registered Email";
                EmailLabel.CssClass = "text-danger";
                RegisterButton.Visible = true;
            }
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }
    }
}