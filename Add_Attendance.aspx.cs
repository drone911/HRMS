using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Add_Attendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string _connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\WebSite5\\App_Data\\Database.mdf;Integrated Security=True";
        string _query = "INSERT INTO [Attendance] (aid,eid,Name,Date,Mark) values (@aid,@eid,@Name,@Date,@Mark)";
        using (SqlConnection conn = new SqlConnection(_connStr))
        {
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = _query;
                comm.Parameters.AddWithValue("@aid", TextBox1.Text);
                comm.Parameters.AddWithValue("@eid", TextBox2.Text);
                comm.Parameters.AddWithValue("@Name", TextBox3.Text);
                comm.Parameters.AddWithValue("@Date", TextBox4.Text);
                comm.Parameters.AddWithValue("@Mark", TextBox5.Text);
               


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
        Response.Redirect("~/View_Attendance.aspx");
    }
}