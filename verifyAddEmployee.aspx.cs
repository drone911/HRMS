using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class verifyAddEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string email = Request.QueryString["email"].Trim();
        string token = Request.QueryString["token"].Trim();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getToken = new SqlCommand("Select [eID], [to], [verificationToken] from [Employee] where email=@email and [isVerified]=0 and [to] IS NULL order by [from] desc", connection);
        getToken.Parameters.AddWithValue("email", email);
        SqlDataAdapter adapter = new SqlDataAdapter(getToken);

        DataTable table = new DataTable();
        adapter.Fill(table);

        if (table.Rows.Count > 0)
        {
            if(table.Rows[0]["verificationToken"].ToString() == token)
            {
                SqlCommand verified = new SqlCommand("Update [Employee] set isVerified=1, [from]=@from where eID=@eID;  Update [SimpleUser] set isEmployed=1 where email=@email;", connection);
                verified.Parameters.AddWithValue("eID", table.Rows[0]["eID"]);
                verified.Parameters.AddWithValue("from", DateTime.Now.ToString("yyyy-MM-dd"));
                verified.Parameters.AddWithValue("email", email);
                verified.ExecuteNonQuery();
                Response.Redirect("~/UserProfile.aspx");
            }
            else
            {
                Response.Redirect("~/error.aspx");
            }
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }
    }
}