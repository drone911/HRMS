using hrms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRProfile : System.Web.UI.Page
{
    private string GSTCert;
    private string EmployeeCert;
    private string pincode;
    private bool refresh = false;
    private SqlConnection connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        pincode = PincodeInput.Text;
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getHRDetails;

        getHRDetails = new SqlCommand("Select profilepicture, employementVerificationCertificate, GSTRegistrationCertificate, mobileNumber, address, pincode, city, state, bloodGroup, qualification, organisationName, organisationAddress, organisationPincode, organisationCity,organisationState,firstName, lastName, birthDate from [User], [HR] where [User].email = [HR].email and [HR].email =@email", connection);
        getHRDetails.Parameters.AddWithValue("email", Util.GetEmail(Request));
        
        
        DataTable userDetails = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter(getHRDetails);
        adapter.Fill(userDetails);
        if (userDetails.Rows.Count > 0)
        {
            DataRow row = userDetails.Rows[0];
            
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
            EmployeeCert = row["employementVerificationCertificate"].ToString();
            GSTCert = row["GSTRegistrationCertificate"].ToString();
            OrganisationName.Text = Util.CapFirstLetters(row["organisationName"].ToString());
            OrganisationAddress.Text = row["organisationAddress"].ToString();
            OrganisationCity.Text = row["organisationCity"].ToString();
            OrganisationPincode.Text = row["organisationPincode"].ToString();
            OrganisationState.Text = row["organisationState"].ToString();

            QualificationLabel.Text = Util.CapFirstLetter(row["qualification"].ToString());
            BirthDateLabel.Text = Util.ConvertToReadableDate((DateTime)row["birthDate"]);
            ViewState["pincode"] = PincodeInput.Text;

            
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }
    }

    protected void ViewGSTCert_Command(object sender, CommandEventArgs e)
    {
        if (GSTCert == null)
        {
            string[] param = new string[] { "No GST Certificate Found", "3" };
            Util.CallJavascriptFunction(this, "popout", param);
        }
        else
        {
            string path = Server.MapPath("Uploads/hr/" + GSTCert);
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
                string[] param = new string[] { "Cannot Open GST Certificate", "3" };
                Util.CallJavascriptFunction(this, "popout", param);

            }
        }
    }

    protected void ViewEmployeeCert_Command(object sender, CommandEventArgs e)
    {
        if (EmployeeCert == null)
        {
            string[] param = new string[] { "No Employee Certificate Found", "3" };
            Util.CallJavascriptFunction(this, "popout", param);
        }
        else
        {
            string path = Server.MapPath("Uploads/hr/" + EmployeeCert);
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
                string[] param = new string[] { "Cannot Open Employee Certificate", "3" };
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
        SqlCommand updateValues = new SqlCommand("Begin Transaction; Update [HR] set address=@address, mobileNumber=@mobileNumber, pincode=@pincode, city=@city, state=@state where [HR].email=@email; Update [User] set profilepicture=@profile where [User].email=@email; COMMIT;", connection);
        updateValues.Parameters.AddWithValue("profile", profilePicture);
        updateValues.Parameters.AddWithValue("address", Address.Text.Trim());
        updateValues.Parameters.AddWithValue("mobileNumber", MobileNumberInput.Text.Trim());
        updateValues.Parameters.AddWithValue("pincode", PincodeInput.Text.Trim());
        updateValues.Parameters.AddWithValue("city", CityInput.Text.Trim());
        updateValues.Parameters.AddWithValue("state", StateInput.Text.Trim());
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