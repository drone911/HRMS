using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Remove_Job : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\WebSite5\App_Data\Database.mdf;Integrated Security=True");
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        using (var cmd1 = new SqlCommand("DELETE FROM Job WHERE jid = @jid", con))
        {
            cmd1.Parameters.Add("@jid", SqlDbType.VarChar).Value = TextBox1.Text;
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();

            Response.Redirect("~/Job_Opening.aspx");
        }
    }
}