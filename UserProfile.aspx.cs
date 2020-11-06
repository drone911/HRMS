using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using hrms;
using System.Net;

public partial class UserProfile : System.Web.UI.Page
{
    private string resume;
    private SqlConnection connection;
    private string pincode;
    private bool refresh = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        pincode = PincodeInput.Text;
        ResumeUpload.Attributes["onchange"] = "UploadResume(this)";
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getUserDetails;

        if (Session["isEmployed"].ToString() == true.ToString())
        {
            getUserDetails = new SqlCommand("Select profilepicture, resume, mobileNumber, address, pincode, city, state, bloodGroup, qualification, organisationName, firstName, lastName, CAST(birthDate as varchar(10)) as birthDate, organisationRole from [User], [SimpleUser], [Employee], [HR] where [User].email = [SimpleUser].email and [SimpleUser].email = [Employee].email and [Employee].email=@email and [Employee].[from] IS NULL and [Employee].employedHREmail=[HR].email", connection);
            getUserDetails.Parameters.AddWithValue("email", Util.GetEmail(Request));
        }
        else
        {
            getUserDetails = new SqlCommand("Select profilepicture, resume, mobileNumber, address, pincode, city, state, bloodGroup, qualification, firstName, lastName, CAST(birthDate as varchar(10)) as birthDate from [User], [SimpleUser] where [User].email = [SimpleUser].email and [SimpleUser].email = @email", connection);
            getUserDetails.Parameters.AddWithValue("email", Util.GetEmail(Request));
        }
        DataTable userDetails = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter(getUserDetails);
        adapter.Fill(userDetails);
        if (userDetails.Rows.Count > 0)
        {
            DataRow row = userDetails.Rows[0];
            if (Session["isEmployed"].ToString() == true.ToString())
            {
                PositionLabel.Text = Util.CapFirstLetters(row["organisationRole"].ToString());
            }
            else
            {
                PositionLabel.Visible = false;
            }
            
            NameLabel.Text = Util.CapFirstLetter(row["firstName"].ToString()) + " " + Util.CapFirstLetter(row["lastName"].ToString());
            BloodGroupLabel.Text = row["bloodGroup"].ToString();
            if (!IsPostBack | refresh)
            {
                profileImage.Src = "~/Uploads/ProfilePictures/" + row["profilepicture"].ToString();
                MobileNumberInput.Text = row["mobileNumber"].ToString();
                Address.Text = row["Address"].ToString();
                PincodeInput.Text = row["pincode"].ToString();
                CityInput.Text = row["city"].ToString();
                StateInput.Text = row["state"].ToString();
            }
                resume = row["resume"].ToString();
            QualificationLabel.Text = Util.CapFirstLetter(row["qualification"].ToString());
            BirthDateLabel.Text = row["birthDate"].ToString();
            ViewState["pincode"] = PincodeInput.Text;

            if (resume == null)
            {
                ViewResumeButton.Enabled = false;
                ViewResumeButton.CssClass += " disabled";
            }
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }
    }

    protected void ViewResumeButton_Command(object sender, CommandEventArgs e)
    {
        if (resume == null)
        {
            string[] param = new string[] { "No Resume Found", "3" };
            Util.CallJavascriptFunction(this, "popout", param);
        }
        else
        {
            string path = Server.MapPath("Uploads/simpleuser/" + resume);
            WebClient client = new WebClient();
            try
            {
                Byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    
                }
            }
            catch
            {
                string[] param = new string[] { "Cannot Open Resume", "3" };
                Util.CallJavascriptFunction(this, "popout", param);

            }
        }
    }
   
    protected void PincodeInput_TextChanged(object sender, EventArgs e)
    {
        string[] result = Util.getCityAndState(pincode);
        if (result[0] == "Success")
        {
            CityInput.Text = result[1];
            StateInput.Text = result[2];
            PincodeInput.Text = pincode;
            ViewState["pincode"] = pincode;
            PincodeLabel.Text = null;
            PincodeLabel.CssClass = null;

        }
        else
        {
            PincodeLabel.Text = "*Valid Pincode was not supplied";
            PincodeLabel.CssClass = "invalid-input";
            ViewState["pincode"] = null;
        }
    }
    protected void SaveChangesButton_Click(object sender, EventArgs e)
    {
        if (connection.State.ToString() != "Open")
        {
            connection.Open();
        }
        bool flag = false;
        if (ViewState["pincode"].ToString() == null)
        {
            PincodeLabel.Text = "Not a valid pincode";
            PincodeLabel.CssClass = "invalid-input";
            flag = true;
        }
        if (flag)
        {
            return;
        }
        string fileName = "";
        string profilePicture = "profilePic.png";
        if (ResumeUpload.PostedFile.ContentLength > 0 && ResumeUpload.PostedFile.ContentLength <= 5000000)
        {
            if (ResumeUpload.PostedFile.ContentType == "application/pdf")
            {
                string extension;
                extension = ".pdf";
                fileName = Request.Cookies["email"].Value.Replace('.', '_') + "_Resume" + DateTime.Now.ToString("hh")+ DateTime.Now.ToString("mm")+ DateTime.Now.ToString("ss");
                string file = Server.MapPath("~/Uploads/simpleuser/" + fileName);
                if (File.Exists(file + ".pdf"))
                {
                    File.Delete(file + ".pdf");
                }
                fileName += extension;
                file += extension;
                ResumeUpload.PostedFile.SaveAs(file);

            }
        }
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

            profilePicture = Util.GetEmail(Request).Replace('.', '_') + "_Profile"+ DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
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
        if (fileName == null)
        {
            SqlCommand updateValues = new SqlCommand("Begin Transaction; Update [SimpleUser] set address=@address, mobileNumber=@mobileNumber, pincode=@pincode, city=@city, state=@state where [SimpleUser].email=@email; Update [User] set profilepicture=@profile where [User].email=@email; COMMIT;", connection);
            updateValues.Parameters.AddWithValue("profile", profilePicture);
            updateValues.Parameters.AddWithValue("address", Address.Text.Trim());
            updateValues.Parameters.AddWithValue("mobileNumber", MobileNumberInput.Text.Trim());
            updateValues.Parameters.AddWithValue("pincode", PincodeInput.Text.Trim());
            updateValues.Parameters.AddWithValue("city", CityInput.Text.Trim());
            updateValues.Parameters.AddWithValue("state", StateInput.Text.Trim());
            updateValues.Parameters.AddWithValue("email", Util.GetEmail(Request));
            updateValues.ExecuteNonQuery();
        }
        else
        {

            SqlCommand updateValues = new SqlCommand("Begin Transaction; Update [SimpleUser] set resume=@fileName, address=@address, mobileNumber=@mobileNumber, pincode=@pincode, city=@city, state=@state where [SimpleUser].email=@email; Update [User] set profilepicture=@profile where [User].email=@email; COMMIT;", connection);
            updateValues.Parameters.AddWithValue("email", Util.GetEmail(Request));
            updateValues.Parameters.AddWithValue("fileName", fileName);
            updateValues.Parameters.AddWithValue("profile", profilePicture);
            updateValues.Parameters.AddWithValue("address", Address.Text.Trim());
            updateValues.Parameters.AddWithValue("mobileNumber", MobileNumberInput.Text.Trim());
            updateValues.Parameters.AddWithValue("pincode", PincodeInput.Text.Trim());
            updateValues.Parameters.AddWithValue("city", CityInput.Text.Trim());
            updateValues.Parameters.AddWithValue("state", StateInput.Text.Trim());
            updateValues.ExecuteNonQuery();
        }

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