using hrms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       ResumeUpload.Attributes["onchange"] = "UploadResume(this)";
    }
    protected void RegisterUserButton_Click(object sender, EventArgs e)
    {
        bool flag = false;
        if (ViewState["resume"].ToString() == null)
        {
            ResumeLabel.Text = "*Upload the required file";
            ResumeLabel.CssClass = "invalid-input";
            flag = true;
        }
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
        else
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            sqlConnection.Open();
            
            SqlCommand insertSimpleUser = new SqlCommand("insert into [SimpleUser](email, mobileNumber, address, qualification, resume, bloodGroup, pincode, city, state) values(@email, @mobile, @address, @qualification, @resume, @bloodGroup, @pincode,@city, @state)", sqlConnection);
            insertSimpleUser.Parameters.AddWithValue("email", Request.Cookies["email"].Value);
            insertSimpleUser.Parameters.AddWithValue("mobile", MobileNumberInput.Text.Trim());
            insertSimpleUser.Parameters.AddWithValue("address", AddressLine1.Text.Trim() + "," + AddressLine2.Text.Trim());
            insertSimpleUser.Parameters.AddWithValue("qualification", QualificationInput.Text.Trim());
            insertSimpleUser.Parameters.AddWithValue("resume", ViewState["resume"].ToString());
            insertSimpleUser.Parameters.AddWithValue("bloodGroup", BloodGroupInput.SelectedItem.Value);
            insertSimpleUser.Parameters.AddWithValue("pincode", ViewState["pincode"].ToString());
            insertSimpleUser.Parameters.AddWithValue("city", CityInput.Text);
            insertSimpleUser.Parameters.AddWithValue("state", StateInput.Text);
            insertSimpleUser.ExecuteNonQuery();
            
            SqlCommand updateVerification = new SqlCommand("update [User] set isFullyRegistered=1 where email=@email", sqlConnection);
            updateVerification.Parameters.AddWithValue("email", Util.GetEmail(Request));
            updateVerification.ExecuteNonQuery();
            Response.Cookies["registered"].Value = "True";
            Response.Cookies["registered"].Expires.AddDays(30);
            
            Util.CallJavascriptFunction(this, "popout", new string[] { "Information Updated Successfully", "3" });
            Util.TimeoutAndRedirect(this, "UserProfile.aspx", 3);
        }
    }
    protected void PincodeInput_TextChanged(object sender, EventArgs e)
    {
        string[] result = Util.getCityAndState(PincodeInput.Text.Trim().ToString());
        if (result[0] == "Success")
        {
            CityInput.Text = result[1];
            StateInput.Text = result[2];
            ViewState["pincode"] = PincodeInput.Text;
        }
        else
        {
            ViewState["pincode"] = null;
        }
    }
    protected void ResumeButton_Click(object sender, EventArgs e)
    {
        if (ResumeUpload.PostedFile.ContentLength > 0 && ResumeUpload.PostedFile.ContentLength <= 2000000)
        {
            if (ResumeUpload.PostedFile.ContentType == "image/jpeg" || ResumeUpload.PostedFile.ContentType == "application/pdf")
            {
                string extension;
                if (ResumeUpload.PostedFile.ContentType == "image/jpeg")
                {
                    extension = ".jpeg";
                }
                else
                {
                    extension = ".pdf";
                }
                string fileName = Request.Cookies["email"].Value.Replace('.', '_') + "_Resume";
                string file = Server.MapPath("~/Uploads/simpleuser/" + fileName);
                if (File.Exists(file + ".pdf"))
                {
                    File.Delete(file + ".pdf");
                }
                if (File.Exists(file + ".jpeg"))
                {
                    File.Delete(file + ".jpeg");
                }
                fileName += extension;
                file += extension;
                ResumeUpload.SaveAs(file);

                ResumeLabel.Text = ResumeUpload.PostedFile.FileName + " uploaded";
                ResumeLabel.CssClass = "valid-input";

                ViewState["resume"] = fileName;
            }
            else
            {
                ResumeLabel.Text = "Uploaded file should be of type jpg, jpeg or pdf";
                ResumeLabel.CssClass = "invalid-input";
            }
        }
        else
        {
            ResumeLabel.Text = "Uploaded file should have maximum size of 2MB";
            ResumeLabel.CssClass = "invalid-input";
        }
    }
}