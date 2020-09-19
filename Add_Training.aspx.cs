using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Add_Training : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string _connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\WebSite5\\App_Data\\Database.mdf;Integrated Security=True";
        string _query = "INSERT INTO [Training] (eid,dept,loc,start_date,end_date) values (@eid,@dept,@loc,@start_date,@end_date)";
        using (SqlConnection conn = new SqlConnection(_connStr))
        {
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = _query;
                comm.Parameters.AddWithValue("@eid", TextBox1.Text);
                comm.Parameters.AddWithValue("@dept", TextBox2.Text);
                comm.Parameters.AddWithValue("@loc", TextBox3.Text);
                comm.Parameters.AddWithValue("@start_date", TextBox4.Text);
                comm.Parameters.AddWithValue("@end_date", TextBox5.Text);
                

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
    }
}