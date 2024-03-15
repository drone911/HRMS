using hrms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ModeratorProfile : System.Web.UI.Page
{
    private SqlConnection connection;
    private bool refresh=false;
    protected void Page_Load(object sender, EventArgs e)
    {
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getUserDetails;

        getUserDetails = new SqlCommand("Select profilepicture, firstName, lastName, birthDate from [User] where [User].email = @email ", connection);
        getUserDetails.Parameters.AddWithValue("email", Util.GetEmail(Request));

        DataTable userDetails = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter(getUserDetails);
        adapter.Fill(userDetails);

        if (userDetails.Rows.Count > 0)
        {
            DataRow row = userDetails.Rows[0];

            NameLabel.Text = Util.CapFirstLetter(row["firstName"].ToString()) + " " + Util.CapFirstLetter(row["lastName"].ToString());
            if (!IsPostBack | refresh)
            {
                profileImage.Src = "~/Uploads/ProfilePictures/" + row["profilepicture"].ToString();
            }
            BirthDateLabel.Text = Util.ConvertToReadableDate((DateTime)row["birthDate"]);
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }
    }

    protected void SaveChangesButton_Click(object sender, EventArgs e)
    {
        if (connection.State.ToString() != "Open")
        {
            connection.Open();
        }
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

            profilePicture = Util.GetEmail(Request).Replace('.', '_') + "_Profile" + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
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
        SqlCommand updateValues = new SqlCommand("Update [User] set profilepicture=@profile where [User].email=@email;", connection);
        updateValues.Parameters.AddWithValue("profile", profilePicture);
        updateValues.Parameters.AddWithValue("email", Util.GetEmail(Request));
        updateValues.ExecuteNonQuery();
        string profileURL = "~/Uploads/ProfilePictures/" + profilePicture;

        Response.Cookies["profileURL"].Value = profileURL;
        refresh = true;
        this.Page_Load(sender, e);
        refresh = false;
        ContentPlaceHolder ct = (ContentPlaceHolder)Page.Master.Master.FindControl("MainContent");
        ((System.Web.UI.HtmlControls.HtmlImage)ct.FindControl("profilePic")).Src = profileURL;

        Util.CallJavascriptFunction(this, "popout", new string[] { "Saved Changes", "3" });
    }
}