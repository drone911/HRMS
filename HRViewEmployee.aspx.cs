using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using hrms;

public partial class HRViewEmployee : System.Web.UI.Page
{
    private string eID;
    protected void Page_Load(object sender, EventArgs e)
    {
        string email = Request.QueryString["email"];
        if (email == null)
        {
            Response.Redirect("~/HRViewEmployees.aspx");
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getData = new SqlCommand("Select eID, firstName, LastName, birthdate, profilepicture, employedHREmail, organisationRole, [from], mobileNumber, [to], Address, pincode, city, state, bloodGroup, qualification from [Employee], [SimpleUser], [User] where [Employee].[to] IS NULL and [Employee].[email]=[SimpleUser].[email] and [SimpleUser].[email]=[User].[email] and [User].[email]=@email", connection);
        getData.Parameters.AddWithValue("email", email);
        DataTable table = new DataTable();
        SqlDataAdapter dataAdapter = new SqlDataAdapter(getData);
        dataAdapter.Fill(table);
        if (table.Rows.Count > 0)
        {
            if (table.Rows[0]["employedHREmail"].ToString() != Util.GetEmail(Request))
            {
                ViewMultiView.SetActiveView(NotAuth);
            }
            else
            {
                ViewMultiView.SetActiveView(Auth);
                DataRow row = table.Rows[0];
                eID = row["eID"].ToString();

                if (!IsPostBack)
                {
                    Tuple<int, int> att = Util.AttendanceBetween(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1), row["eID"].ToString(), row["employedHREmail"].ToString(), connection);
                    if (att.Item2 == 0)
                    {
                        AttendanceLabelMultiView.ActiveViewIndex = 1;
                        AttendanceLabel.Text = "No Working Days in this interval";
                    }
                    else
                    {
                        AttendanceLabelMultiView.ActiveViewIndex = 0;
                        AttPerc.Text= Math.Round((att.Item1 / (double)att.Item2) * 100, 2) + "%";
                        CurrAtt.Text = att.Item1.ToString();
                        TotAtt.Text = att.Item2.ToString();
                    }
                    AttStartDate.Text = DateTime.Now.AddYears(-1).ToString("dd/MM/yyyy");
                    AttEndDate.Text = DateTime.Now.AddYears(+1).ToString("dd/MM/yyyy");

                }

                NameLabel.Text = row["firstName"] + " " + row["lastName"];
                PositionLabel.Text = row["organisationRole"] + "";
                EmployeeProfilePic.Src = "Uploads/ProfilePictures/" + row["profilepicture"];
                AddressLabel.Text = row["Address"] + "<br/>" + row["city"] + "(" + row["pincode"] + ")" + "<br/>" + row["state"];
                ContactLabel.Text = row["mobileNumber"].ToString();
                EmailLabel.Text = email;
                BloodGroupLabel.Text = row["bloodGroup"].ToString();
                QualificationLabel.Text = row["qualification"].ToString();
                BirthdayLabel.Text = Util.ConvertToReadableDate((DateTime)row["birthdate"]);

                SqlCommand getCurrTraining = new SqlCommand("Select [Training].tID as tID, name, description, startDate, endDate from [Training], [TrainingEmp] where [Training].tID=[TrainingEmp].tID and employerHREmail=@hremail and email=@email and endDate>=GETDATE()", connection);
                getCurrTraining.Parameters.AddWithValue("email", email);
                getCurrTraining.Parameters.AddWithValue("hremail", Util.GetEmail(Request));

                dataAdapter.SelectCommand = getCurrTraining;
                DataTable currTraining = new DataTable();

                dataAdapter.Fill(currTraining);

                if (currTraining.Rows.Count > 0)
                {
                    CurrentTrainingMultiView.ActiveViewIndex = 0;
                    DataRow trow = currTraining.Rows[0];
                    TrainingNameLabel.Text = trow["name"].ToString();
                    TrainingStartDate.Text = Util.ConvertToReadableDate((DateTime)trow["startDate"]);
                    TrainingEndDate.Text = Util.ConvertToReadableDate((DateTime)trow["endDate"]);
                    ViewTrainingHyperLink.NavigateUrl = "~/HRViewTraining.aspx?id=" +HttpUtility.UrlEncode( trow["tID"].ToString());
                }
                else
                {
                    CurrentTrainingMultiView.ActiveViewIndex = 1;
                }
            }
        }
        else
        {
            Response.Redirect("~/error.aspx");
        }

    }

    protected void Att_TextChanged(object sender, EventArgs e)
    {
        string[] startString = AttStartDate.Text.Split('/');
        DateTime start = new DateTime(Convert.ToInt32(startString[2]), Convert.ToInt32(startString[1]), Convert.ToInt32(startString[0]));
        string[] endString = AttEndDate.Text.Split('/');
        DateTime end = new DateTime(Convert.ToInt32(endString[2]), Convert.ToInt32(endString[1]), Convert.ToInt32(endString[0]));

        if (start > end)
        {
            AttStartDateLabel.Text = "*Enter Valid Dates";
            AttStartDateLabel.Visible = true;
            AttEndDateLabel.Text = "*Enter Valid Dates";
            AttEndDateLabel.Visible = true;
        }
        else
        {
            AttStartDateLabel.Visible = false;
            AttEndDateLabel.Visible = false;

            Tuple<int, int> att = Util.AttendanceBetween(start, end, eID, Util.GetEmail(Request));
            if (att.Item2 == 0)
            {
                AttendanceLabelMultiView.ActiveViewIndex = 1;
                AttendanceLabel.Text = "No Working Days in this interval";
            }
            else
            {
                AttendanceLabelMultiView.ActiveViewIndex = 0;
                AttPerc.Text = Math.Round((att.Item1 / (double)att.Item2) * 100, 2) + "%";
                CurrAtt.Text = att.Item1.ToString();
                TotAtt.Text = att.Item2.ToString();
            }
        }
    }

    protected void Export_Click(object sender, EventArgs e)
    {
        string[] startString = AttStartDate.Text.Split('/');
        DateTime start = new DateTime(Convert.ToInt32(startString[2]), Convert.ToInt32(startString[1]), Convert.ToInt32(startString[0]));
        string[] endString = AttEndDate.Text.Split('/');
        DateTime end = new DateTime(Convert.ToInt32(endString[2]), Convert.ToInt32(endString[1]), Convert.ToInt32(endString[0]));

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand getTot = new SqlCommand("Select CAST([tot].[date] as varchar(10)) as 'Working Date', [curr].[eID] as 'Present' from  (Select Distinct [date] from [Attendance] where eID IN (Select eID from [Employee] Where employedHREmail=@hremail) and date>=@startDate and date<=@endDate) as tot Left Join (Select date,eID from [Attendance] where eID=@id and date>=@startDate and date<=@endDate) as curr ON tot.date=curr.date", connection);
        getTot.Parameters.AddWithValue("hremail", Util.GetEmail(Request));
        getTot.Parameters.AddWithValue("startDate", start.ToString("yyyy/MM/dd"));
        getTot.Parameters.AddWithValue("endDate", end.ToString("yyyy/MM/dd"));
        getTot.Parameters.AddWithValue("id", eID);

        SqlDataAdapter adapter = new SqlDataAdapter(getTot);
        DataTable att = new DataTable();
        adapter.Fill(att);
        if (att.Rows.Count > 0)
        {
            int i;
            for (i = 0; i < att.Rows.Count; i++)
            {
                if (att.Rows[i]["Present"].ToString() == "")
                {
                    att.Rows[i]["Present"] = "No";
                }
                else
                {
                    att.Rows[i]["Present"] = "Yes";
                }
            }
            string attachment = "attachment; filename=" + NameLabel.Text.Replace(' ', '_') + "_from_" + start.ToString("dd/MM/yyyy") + "_to_" + end.ToString("dd/MM/yyyy") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in att.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in att.Rows)
            {
                tab = "";
                for (i = 0; i < att.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();

        }
        else
        {

        }
    }
}