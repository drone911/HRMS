using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using hrms;

public class PostOffice
{
    public string Name { get; set; }
    public string District { get; set; }
    public string State { get; set; }
}
public class PostalResponse
{
    public string Message { get; set; }
    public string Status { get; set; }
    public List<PostOffice> PostOffice;
}
public partial class HRRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EmployementVerificationProofUpload.Attributes["onchange"] = "UploadEmpProof(this)";
        GSTRegistrationCertificateUpload.Attributes["onchange"] = "UploadGSTProof(this)";
    }
    protected void RegisterHRButton_Click(object sender, EventArgs e)
    {
        bool flag = false;
        if (ViewState["employeeVerificationFile"].ToString() == null)
        {
            EmployementVerificationProofLabel.Text = "*Upload the required file";
            EmployementVerificationProofLabel.CssClass = "invalid-input";
            flag = true;
        }
        if (ViewState["gstCertificateFile"].ToString() == null)
        {
            GSTRegistrationLabel.Text = "*Upload the required file";
            GSTRegistrationLabel.CssClass = "invalid-input";
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
            SqlCommand insertHr = new SqlCommand("insert into HR values(@email, @mobile, @address, 0, @qualification, @employementVerificationCertificate, @GSTRegistrationCertificate, @bloodGroup, @pincode,@city, @state)", sqlConnection);
            insertHr.Parameters.AddWithValue("email", Request.Cookies["email"].Value);
            insertHr.Parameters.AddWithValue("mobile", MobileNumberInput.Text.Trim());
            insertHr.Parameters.AddWithValue("address", AddressLine1.Text.Trim() + "," + AddressLine2.Text.Trim());
            insertHr.Parameters.AddWithValue("qualification", QualificationInput.Text.Trim());
            insertHr.Parameters.AddWithValue("employementVerificationCertificate", ViewState["employeeVerificationFile"].ToString());
            insertHr.Parameters.AddWithValue("GSTRegistrationCertificate", ViewState["gstCertificateFile"].ToString());
            insertHr.Parameters.AddWithValue("bloodGroup", BloodGroupInput.SelectedItem.Value);
            insertHr.Parameters.AddWithValue("pincode", ViewState["pincode"].ToString());
            insertHr.Parameters.AddWithValue("city", CityInput.Text);
            insertHr.Parameters.AddWithValue("state", StateInput.Text);
            insertHr.ExecuteNonQuery();
            Util.CallJavascriptFunction(Page, "popout", new string[] { "Information Updated Successfully", "3" });
            Util.TimeoutAndRedirect(Page, "~/HRHome.aspx", 3);
        }
    }
    
    protected void PincodeInput_TextChanged(object sender, EventArgs e)
    {

        string Url = "https://api.postalpincode.in/pincode/" + PincodeInput.Text.Trim().ToString();

        using (HttpClient client = new HttpClient())
        {
            var responseTask = client.GetAsync(Url);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<PostalResponse[]>();
                readTask.Wait();
                var res = readTask.Result;

                if (res[0].Status == "Success")
                {
                    CityInput.Text = res[0].PostOffice[0].District;
                    StateInput.Text = res[0].PostOffice[0].State;
                    ViewState["pincode"] = PincodeInput.Text;
                }
                else
                {
                    ViewState["pincode"]= null;
                }
            }
        }
    }

    protected void EmployementProofButton_Click(object sender, EventArgs e)
    {
        if (EmployementVerificationProofUpload.PostedFile.ContentLength > 0 && EmployementVerificationProofUpload.PostedFile.ContentLength <= 2000000)
        {
            if (EmployementVerificationProofUpload.PostedFile.ContentType == "image/jpeg" || EmployementVerificationProofUpload.PostedFile.ContentType == "application/pdf")
            {
                string extension;
                if (EmployementVerificationProofUpload.PostedFile.ContentType == "image/jpeg")
                {
                    extension = ".jpeg";
                }
                else
                {
                    extension = ".pdf";
                }
                string fileName = Request.Cookies["email"].Value.Replace('.', '_') + "_EmployementVerificationCertificate";
                string file = Server.MapPath("~/Uploads/" + fileName);
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
                EmployementVerificationProofUpload.SaveAs(file);

                EmployementVerificationProofLabel.Text = EmployementVerificationProofUpload.PostedFile.FileName + " uploaded";
                EmployementVerificationProofLabel.CssClass = "valid-input";

                ViewState["employeeVerificationFile"] = fileName;
            }
            else
            {
                EmployementVerificationProofLabel.Text = "Uploaded file should be of type jpg, jpeg or pdf";
                EmployementVerificationProofLabel.CssClass = "invalid-input";
            }
        }
        else
        {
            EmployementVerificationProofLabel.Text = "Uploaded file should have maximum size of 2MB";
            EmployementVerificationProofLabel.CssClass = "invalid-input";
        }
    }
    protected void GSTProofButton_Click(object sender, EventArgs e)
    {
        if (GSTRegistrationCertificateUpload.PostedFile.ContentLength > 0 && GSTRegistrationCertificateUpload.PostedFile.ContentLength <= 2000000)
        {
            if (GSTRegistrationCertificateUpload.PostedFile.ContentType == "image/jpeg" || GSTRegistrationCertificateUpload.PostedFile.ContentType == "application/pdf")
            {
                string extension;
                if (GSTRegistrationCertificateUpload.PostedFile.ContentType == "image/jpeg")
                {
                    extension = ".jpeg";
                }
                else
                {
                    extension = ".pdf";
                }
                string fileName = Request.Cookies.Get("email").Value.Replace('.', '_') + "_GSTRegistrationCertificate";
                string file = Server.MapPath("~/Uploads/" + fileName);
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
                GSTRegistrationCertificateUpload.SaveAs(file);

                GSTRegistrationLabel.Text = GSTRegistrationCertificateUpload.PostedFile.FileName + " uploaded";
                GSTRegistrationLabel.CssClass = "valid-input";

                ViewState["gstCertificateFile"] = fileName;
            }
            else
            {
                GSTRegistrationLabel.Text = "Uploaded file should be of type jpg, jpeg or pdf";
                GSTRegistrationLabel.CssClass = "invalid-input";
            }
        }
        else
        {
            GSTRegistrationLabel.Text = "Uploaded file should have maximum size of 2MB";
            GSTRegistrationLabel.CssClass = "invalid-input";
        }
    }
}