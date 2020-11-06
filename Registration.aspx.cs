using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Helpers;
using System.IO;
using hrms;
using System.Web.UI.HtmlControls;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        EmailLabel.Text = "";
        BirthdayLabel.Text = "";
        AlertLabel.Visible = false;
        imageLabel.Visible = false;
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        EmailLabel.Text = "";
        BirthdayLabel.Text = "";
        AlertLabel.Visible = false;

        String email = EmailInput.Text.Trim().ToLower();
        String dbcstring = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        SqlConnection sqlConnection = new SqlConnection(dbcstring);
        sqlConnection.Open();
        if (sqlConnection.State.ToString() == "Open")
        {
            SqlCommand sqlCommand = new SqlCommand("select email from [User] where email=@email", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("email", email));
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable ds = new DataTable();
            dataAdapter.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                EmailLabel.Text = "**Email already register, please register a from different email address.";
            }
            else
            {
                String[] date = FlatpickrCalender.Text.Split('-');
                var birthdate = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]));
                if (DateTime.Now.Year - birthdate.Year < 18)
                {
                    BirthdayLabel.Text = "**age should be greater than 18";
                }
                else
                {
                    string profilePicture = "profilePic.png";
                    if (imageUpload.PostedFile.ContentLength > 0)
                    {
                        string extension;
                        if (imageUpload.PostedFile.ContentType == "image/jpeg")
                        {
                            extension = ".jpeg";
                        }
                        else if (imageUpload.PostedFile.ContentType == "image/png")
                        {
                            extension = ".png";
                        }
                        else
                        {
                            imageLabel.Text = "*Image should be of type jpg, jpeg or png only.";
                            imageLabel.Visible = true;
                            return;
                        }

                        profilePicture = email.Replace('.', '_') + "_Profile";
                        string file = Server.MapPath("~/Uploads/ProfilePictures/" + profilePicture);
                        if (File.Exists(file + ".png"))
                        {
                            File.Delete(file + ".png");
                        }
                        if (File.Exists(file + ".jpeg"))
                        {
                            File.Delete(file + ".jpeg");
                        }
                        profilePicture += extension;
                        file += extension;
                        imageUpload.SaveAs(file);
                    }

                    string verificationToken = Crypto.GenerateSalt(8);
                    string salt = Crypto.GenerateSalt(16);

                    string tokenisedUrl = ConfigurationManager.AppSettings["domain"] + "VerifyEmail.aspx?token=" + verificationToken + "&email=" + email;
                    String emailBody = "Please follow the link to <a href = \"" + tokenisedUrl + "\">verify your email</a>";

                    // change email body in future
                    Util.SendEmail(email, "Verification mail from HR Management Site", emailBody);
                    SqlCommand insertCommand = new SqlCommand("Insert into [User](email, firstName, lastName, isEmailVerified, verificationToken, birthdate, role, hashedPassword, salt, profilepicture) values(@email, @firstName, @lastName, 0, @verificationToken, @birthdate, @role, @hashedPassword, @salt, @profile)", sqlConnection);
                    insertCommand.Parameters.AddWithValue("email", email);
                    insertCommand.Parameters.AddWithValue("firstName", FirstNameInput.Text.Trim().ToLower());
                    insertCommand.Parameters.AddWithValue("lastName", LastNameInput.Text.Trim().ToLower());
                    insertCommand.Parameters.AddWithValue("verificationToken", verificationToken);
                    insertCommand.Parameters.AddWithValue("birthdate", birthdate.ToString("yyyy-MM-dd"));
                    insertCommand.Parameters.AddWithValue("role", RoleInput.SelectedValue.Trim().ToLower());
                    insertCommand.Parameters.AddWithValue("hashedPassword", Crypto.HashPassword(salt + PasswordInput.Text));
                    insertCommand.Parameters.AddWithValue("salt", salt);
                    insertCommand.Parameters.AddWithValue("profile", profilePicture);

                    
                    insertCommand.ExecuteNonQuery();
                    if (RoleInput.SelectedValue.Trim().ToLower() == "moderator")
                    {
                        SqlCommand insertMod = new SqlCommand("Insert into [Moderator](email) values(@email)", sqlConnection);
                        insertMod.Parameters.AddWithValue("email", email);
                        insertMod.ExecuteNonQuery();
                    }
                    AlertLabel.Visible = true;
                    AlertLabel.Text = "Succesfully Registered, Check you email for verification link, redirecting to login...";
                    string timeOutUrl = ConfigurationManager.AppSettings["domain"] + "Login.aspx";
                    Util.TimeoutAndRedirect(Page, timeOutUrl, 4);

                }

            }
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }
    }
}