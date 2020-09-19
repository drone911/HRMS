using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Add_Employee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string _connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\WebSite5\\App_Data\\Database.mdf;Integrated Security=True";
        string _query = "INSERT INTO [Employee] (eid,efname,elname,eaddress,ecity,ebirthdate,ebranch,egender,ebloodgroup,edateofjoining,ebodymark) values (@eid,@efname,@elname,@eaddress,@ecity,@ebirthdate,@ebranch,@egender,@ebloodgroup,@edateofjoining,@ebodymark)";
        using (SqlConnection conn = new SqlConnection(_connStr))
        {
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = _query;
                comm.Parameters.AddWithValue("@eid", TextBox1.Text);
                comm.Parameters.AddWithValue("@efname", TextBox2.Text);
                comm.Parameters.AddWithValue("@elname", TextBox3.Text);
                comm.Parameters.AddWithValue("@eaddress", TextBox4.Text);
                comm.Parameters.AddWithValue("@ecity", TextBox5.Text);
                comm.Parameters.AddWithValue("@ebirthdate", TextBox6.Text);
                comm.Parameters.AddWithValue("@ebranch", TextBox7.Text);
                comm.Parameters.AddWithValue("@egender", TextBox8.Text);
                comm.Parameters.AddWithValue("@ebloodgroup", TextBox9.Text);
                comm.Parameters.AddWithValue("@edateofjoining", TextBox10.Text);
                comm.Parameters.AddWithValue("@ebodymark", TextBox11.Text);

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