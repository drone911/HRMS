using hrms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ModeratorViewHR : System.Web.UI.Page
{
    private SqlConnection connection;
    private string GSTCert;
    private string EmployeeCert;
    string email;
    protected void Page_Load(object sender, EventArgs e)
    {
        email = Request.QueryString["email"];
        if (email == "")
        {
            Response.Redirect("~/ModeratorViewHRs.aspx");
        }
        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getHRDetails;

        getHRDetails = new SqlCommand("Select profilepicture, employementVerificationCertificate, GSTRegistrationCertificate, mobileNumber, address, pincode, city, state, bloodGroup, qualification, organisationName, organisationAddress, organisationPincode, organisationCity,organisationState, isVerified, firstName, lastName, birthDate from [User], [HR] where [User].email = [HR].email and [HR].email =@email", connection);
        getHRDetails.Parameters.AddWithValue("email", email);


        DataTable userDetails = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter(getHRDetails);
        adapter.Fill(userDetails);
        if (userDetails.Rows.Count > 0)
        {
            DataRow row = userDetails.Rows[0];

            NameLabel.Text = Util.CapFirstLetter(row["firstName"].ToString()) + " " + Util.CapFirstLetter(row["lastName"].ToString());
            BloodGroupLabel.Text = row["bloodGroup"].ToString();
            profileImage.Src = "~/Uploads/ProfilePictures/" + row["profilepicture"].ToString();
            MobileNumberInput.Text = row["mobileNumber"].ToString();
            Address.Text = row["Address"].ToString();
            PincodeInput.Text = row["pincode"].ToString();
            CityInput.Text = row["city"].ToString();
            StateInput.Text = row["state"].ToString();
            EmployeeCert = row["employementVerificationCertificate"].ToString();
            GSTCert = row["GSTRegistrationCertificate"].ToString();
            OrganisationName.Text = Util.CapFirstLetters(row["organisationName"].ToString());
            OrganisationAddress.Text = row["organisationAddress"].ToString();
            OrganisationCity.Text = row["organisationCity"].ToString();
            OrganisationPincode.Text = row["organisationPincode"].ToString();
            OrganisationState.Text = row["organisationState"].ToString();
            QualificationLabel.Text = Util.CapFirstLetter(row["qualification"].ToString());
            BirthDateLabel.Text = Util.ConvertToReadableDate((DateTime)row["birthDate"]);
            if ((bool)row["isVerified"])
            {
                VerifyBtn.Text = "Verified";
                VerifyBtn.Enabled = false;
                RemoveBtn.Visible = false;   
            }
        }
        else
        {
            Response.Redirect("~/ModeratorViewHRs.aspx");
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

    protected void VerifyBtn_Click(object sender, EventArgs e)
    {
        if (connection.State.ToString() != "Open")
        {
            connection.Open();
        }
        SqlCommand updateHR = new SqlCommand("Update [HR] set isVerified=1 where email=@email", connection);
        updateHR.Parameters.AddWithValue("email", email);
        updateHR.ExecuteNonQuery();
        Page_Load(sender, e);
    }

    protected void RemoveBtn_Click(object sender, EventArgs e)
    {
        if (connection.State.ToString() != "Open")
        {
            connection.Open();
        }
        SqlCommand removeHR = new SqlCommand("Begin Transaction;Delete from [HR] where email=@email; Delete from [User] where email=@email;COMMIT", connection);
        removeHR.Parameters.AddWithValue("email", email);
        removeHR.ExecuteNonQuery();
        Response.Redirect("~/ModeratorViewHRs.aspx");
    }
}