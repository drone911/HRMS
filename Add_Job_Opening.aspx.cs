using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Add_Job_Opening : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string _connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\WebSite5\\App_Data\\Database.mdf;Integrated Security=True";
        string _query = "INSERT INTO [Job] (jid,Candidates_Required,Job_Opening_Dept,Location,Created_On,Status) values (@jid,@Candidates_Required,@Job_Opening_Dept,@Location,@Created_On,@Status)";
        using (SqlConnection conn = new SqlConnection(_connStr))
        {
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = _query;
                comm.Parameters.AddWithValue("@jid", TextBox1.Text);
                comm.Parameters.AddWithValue("@Candidates_Required", TextBox2.Text);
                comm.Parameters.AddWithValue("@Job_Opening_Dept", TextBox3.Text);
                comm.Parameters.AddWithValue("@Location", TextBox4.Text);
                comm.Parameters.AddWithValue("@Created_On", TextBox5.Text);
                comm.Parameters.AddWithValue("@Status", TextBox6.Text);
                

                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // other codes here
                    // do something with the exception
                    // don't swallow it.
                }
            }
        }
        Response.Redirect("~/Job_Opening.aspx");
    }
}