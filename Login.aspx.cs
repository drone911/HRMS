using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using hrms;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\WebSite2\\App_Data\\Database.mdf;Integrated Security=True";
        cn.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select * from login where id=@id";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cn;


        SqlParameter p = new SqlParameter("id", TextBox1.Text);

        cmd.Parameters.Add(p);


        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataSet ds = new DataSet();
        da.Fill(ds, "T1");


        if (ds.Tables["T1"].Rows.Count > 0)
        {
            Session["id"] = ds.Tables["T1"].Rows[0]["id"].ToString().Trim();
            Session["role"] = ds.Tables["T1"].Rows[0]["role"].ToString().Trim();
           
            //Response.Write(c.Equals(emp));
            if (Session["role"].Equals("Employee"))
            {
                Response.Redirect("~/Employee_Home.aspx");
            }
            else if (Session["role"].Equals("HR"))
            {
                Response.Redirect("~/HR_Home.aspx");
            }
            
        }
        
        else 
        {
            Label1.Text = "Invalid Login Credentials";
        }
       
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Label1.Text = "Login Cancelled";
        //Response.Redirect("~/Login.aspx");
    }
}





