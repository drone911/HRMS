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
using System.Web.Helpers;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EmailLabel.CssClass = "";
        EmailLabel.Text = "";
        VerifyEmailHyperlink.Visible = false;
        if (Util.IsLoggedIn(Request.Cookies))
        {
            string[] javascriptFunctionParam = { "Logout First!", "4" };
            Util.CallJavascriptFunction(Page, "popout", javascriptFunctionParam);
            Util.TimeoutAndRedirect(Page, ConfigurationManager.AppSettings["domain"] + "Home.aspx");
        }
    }

    protected void SendEmailButton_Click(object sender, EventArgs e)
    {
        SendEmailButton.CssClass += " disabled";
        string email = EmailInput.Text.Trim().ToLower();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getEmailCommand = new SqlCommand("select isEmailVerified from [User] where email=@email", connection);
        getEmailCommand.Parameters.AddWithValue("email", email);
        SqlDataAdapter adaptor = new SqlDataAdapter(getEmailCommand);
        DataTable dt = new DataTable();
        adaptor.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if ((bool)dt.Rows[0]["isEmailVerified"])
            {
                string token = Crypto.GenerateSalt(16);
                string emailLink = ConfigurationManager.AppSettings["domain"] + "changePassword.aspx?email=" + email + "&token=" + token;
                string emailBody = "Follow the link to change password " + emailLink;
                SqlCommand insertToken = new SqlCommand("update [User] set forgotPasswordToken=@token where email=@email", connection);
                insertToken.Parameters.AddWithValue("email", email);
                insertToken.Parameters.AddWithValue("token", token);
                insertToken.ExecuteNonQuery();
                Util.SendEmail(email, "Change Password", emailBody);
                string[] javascriptFunctionParam = { "Mail Send", "4" };
                Util.CallJavascriptFunction(this, "popout", javascriptFunctionParam);
                Util.TimeoutAndRedirect(Page, ConfigurationManager.AppSettings["domain"] + "Login.aspx");
            }
            else
            {
                VerifyEmailHyperlink.Visible = true;
                EmailLabel.CssClass = "invalid-input";
                EmailLabel.Text = "*email not verified";
                SendEmailButton.CssClass = "btn btn-primary";
            }
        }
        else
        {
            EmailLabel.CssClass = "invalid-input";
            EmailLabel.Text = "*unknown email";
            SendEmailButton.CssClass = "btn btn-primary";
        }
    }
}