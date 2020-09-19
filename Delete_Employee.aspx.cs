using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Delete_Employee : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\WebSite5\App_Data\Database.mdf;Integrated Security=True");
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        using (var cmd1 = new SqlCommand("DELETE FROM Employee WHERE eid = @eid", con))
        {
            cmd1.Parameters.Add("@eid", SqlDbType.VarChar).Value = TextBox1.Text;
            con.Open();
            cmd1.ExecuteNonQuery();
        }
       

        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "Select * from Employee";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();

        con.Close();

    }
}