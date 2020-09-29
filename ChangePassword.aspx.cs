using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Helpers;
using hrms;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ChangePasswordButton_Click(object sender, EventArgs e)
    {
        string email = Request.QueryString["email"];
        string passwordToken = Request.QueryString["token"];
        if (email == null || passwordToken == null)
        {
            Response.Redirect("error.aspx");
        }
        else
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand getToken = new SqlCommand("select forgotPasswordToken from [User] where email=@email", connection);
            getToken.Parameters.AddWithValue("email", email);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(getToken);
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["forgotPasswordToken"].ToString()!=null)
                {
                    if (passwordToken == (string)dt.Rows[0]["forgotPasswordToken"])
                    {
                        string password = PasswordInput.Text;
                        string salt = Crypto.GenerateSalt(16);
                        SqlCommand insertNewPass = new SqlCommand("update [User] set forgotPasswordToken=NULL, hashedPassword=@hashedPassword, salt=@salt where email=@email", connection);
                        insertNewPass.Parameters.AddWithValue("email", email);
                        insertNewPass.Parameters.AddWithValue("hashedPassword", Crypto.HashPassword(salt + password));
                        insertNewPass.Parameters.AddWithValue("salt", salt);
                        insertNewPass.ExecuteNonQuery();
                        string[] javascriptFunctionParam = { "Password Changed", "4" };
                        Util.CallJavascriptFunction(this, "popout", javascriptFunctionParam);
                        Util.TimeoutAndRedirect(this, ConfigurationManager.AppSettings["domain"] + "Login.aspx", 4);
                    }
                    else
                    {
                        Response.Redirect("error.aspx");
                    }
                }
                else
                {
                    Response.Redirect("error.aspx");
                }

            }
            else
            {
                Response.Redirect("error.aspx");
            }
        }
    }
}