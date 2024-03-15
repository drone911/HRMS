using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using hrms;
using System.Configuration;
using System.Web.Helpers;

public partial class Login : System.Web.UI.Page
{
    SqlConnection cn;
    protected void Page_Load(object sender, EventArgs e)
    {
        cn = new SqlConnection();
        cn.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ToString();
        cn.Open();
        Response.Cookies["email"].Value = "";
        Response.Cookies["role"].Value = "";
        Response.Cookies["registered"].Value = "";
        Response.Cookies["verified"].Value = "";
        Response.Cookies.Remove("profileURL");

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        if (cn.State.ToString() == "Open")
        {
            string email = EmailInput.Text.Trim().ToLower();
            SqlCommand cmd = new SqlCommand("select hashedPassword, salt, isEmailVerified, role, isFullyRegistered,profilepicture from [User] where email=@email", cn);

            cmd.Parameters.AddWithValue("email", email);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                bool match = Crypto.VerifyHashedPassword(dt.Rows[0]["hashedPassword"].ToString(), dt.Rows[0]["salt"].ToString() + PasswordInput.Text);
                if (match)
                {
                    if ((bool)dt.Rows[0]["isEmailVerified"])
                    {
                        string role = dt.Rows[0]["role"].ToString();
                        string profileURL = "~/Uploads/ProfilePictures/" + dt.Rows[0]["profilepicture"].ToString();
                        Response.Cookies.Add(new HttpCookie("email", email));
                        Response.Cookies.Add(new HttpCookie("role", role));
                        Response.Cookies.Add(new HttpCookie("profileURL", profileURL));

                        if ((bool)dt.Rows[0]["isFullyRegistered"])
                        {
                            Response.Cookies.Add(new HttpCookie("registered", "True"));
                        }
                        else
                        {
                            Response.Cookies.Add(new HttpCookie("registered", "False"));
                        }
                        if (SavePasswordCheckbox.Checked)
                        {
                            Response.Cookies["email"].Expires.AddDays(30);
                            Response.Cookies["role"].Expires.AddDays(30);
                            Response.Cookies["registered"].Expires.AddDays(30);
                            Response.Cookies["profileURL"].Expires.AddDays(30);
                        }
                        

                        if (role == "hr")
                        {
                            if ((bool)dt.Rows[0]["isFullyRegistered"])
                            {
                                SqlCommand selectVerification = new SqlCommand("select isVerified from [HR] where email=@email", cn);
                                selectVerification.Parameters.AddWithValue("email", email);
                                SqlDataAdapter adapter = new SqlDataAdapter(selectVerification);
                                DataTable verificationDataTable = new DataTable();
                                adapter.Fill(verificationDataTable);
                                Response.Cookies.Add(new HttpCookie("verified", ((bool)verificationDataTable.Rows[0]["isVerified"]).ToString()));
                                if (SavePasswordCheckbox.Checked) {
                                    Response.Cookies["verified"].Expires.AddDays(30);
                                }
                                Response.Redirect("~/HRProfile.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/HRRegistration.aspx");
                            }
                        }
                        if(role == "simpleuser")
                        {
                            Response.Redirect("~/UserProfile.aspx");

                        }
                        if (role == "moderator")
                        {
                            SqlCommand selectVerification = new SqlCommand("select isVerified from [Moderator] where email=@email", cn);
                            selectVerification.Parameters.AddWithValue("email", email);
                            SqlDataAdapter adapter = new SqlDataAdapter(selectVerification);
                            DataTable verificationDataTable = new DataTable();
                            adapter.Fill(verificationDataTable);
                            Response.Cookies.Add(new HttpCookie("verified", ((bool)verificationDataTable.Rows[0]["isVerified"]).ToString()));
                            if (SavePasswordCheckbox.Checked)
                            {
                                Response.Cookies["verified"].Expires.AddDays(30);
                            }

                            Response.Redirect("~/ModeratorProfile.aspx");
                        }
                    }
                    else
                    {
                        LoginLabel.Text = "Mail not verified, redirecting to Send Verification Mail Page";
                        LoginLabel.CssClass = LoginLabel.CssClass + " text-danger";
                        string timeOutUrl = ConfigurationManager.AppSettings["domain"] + "SendVerification.aspx";
                        Util.TimeoutAndRedirect(Page, timeOutUrl, 3);
                    }
                }
                else
                {
                    LoginLabel.Text = "Invalid Login Credentials";
                    LoginLabel.CssClass = LoginLabel.CssClass + " text-danger";
                }

            }
            else
            {
                LoginLabel.Text = "Invalid Login Credentials";
                LoginLabel.CssClass = LoginLabel.CssClass +" text-danger";
            }

        }
        
    }
    protected void ForgotPasswordButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ForgotPassword.aspx");
    }
}





