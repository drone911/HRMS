using System.Collections.Generic;
using System.Web;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Net.Http;
using System.Web.UI.WebControls;
using System.Data;
using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;

namespace hrms
{
    // helper class
    public class PostOffice
    {
        public string Name { get; set; }
        public string District { get; set; }
        public string State { get; set; }
    }
    // helper class
    public class PostalResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public List<PostOffice> PostOffice;
    }
    
    public static class Util
    {
        static public void SendEmail(string to, string subject, string body)
        {
            // change gmail
            MailMessage mail = new MailMessage("jigar1822@gmail.com", to, subject, body);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
        }
        static public void TimeoutAndRedirect(Page currentPage, string timeOutUrl, int timeInSeconds = 2)
        {
            HtmlMeta oScript = new HtmlMeta();
            oScript.Attributes.Add("http-equiv", "REFRESH");
            oScript.Attributes.Add("content", timeInSeconds.ToString() + "; url='" + timeOutUrl + "'");
            currentPage.Header.Controls.Add(oScript);
        }
        static public bool IsLoggedIn(HttpCookieCollection cookies)
        {
            // implement better login check using tokens
            if (cookies["email"] != null)
            {
                return true;
            }
            return false;
        }

        public static string ConvertToReadableDate(DateTime dateTime)
        {
            return dateTime.ToString("dd MMM yyyy");
        }

        static public bool IsOnCorrectPage(HttpCookieCollection cookies, string role)
        {
            if (cookies["role"].Value.ToString() == role)
            {
                return true;
            }
            return false;
        }
        static public void CallJavascriptFunction(Page page, string functionName, string[] parameters)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"" + functionName + "(");
            for (int i = 0; i < parameters.Length - 1; i++)
            {
                sb.Append(@"" + "\"" + parameters[i] + "\",");
            }
            sb.Append(@"" + "\"" + parameters[parameters.Length - 1] + "\");");
            sb.Append(@"</script>");
            System.Web.UI.ScriptManager.RegisterStartupScript(page, page.GetType(), "JCall1", sb.ToString(), false);
        }

        public static string GetEmail(HttpRequest request)
        {
            return request.Cookies.Get("email").Value.ToString();
        }
        public static string GetRole(HttpRequest request)
        {
            return request.Cookies.Get("role").Value.ToString();
        }
        static public void CallJavascriptFunction(Page page, string functionName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"" + functionName + "();");
            sb.Append(@"</script>");
            System.Web.UI.ScriptManager.RegisterStartupScript(page, page.GetType(), "JCall1", sb.ToString(), false);
        }
        // returns string[0] is status, string[1] is city, string[2] is state
        static public string[] getCityAndState(string pincode)
        {
            string[] toReturn = new string[3];
            string Url = "https://api.postalpincode.in/pincode/" + pincode;

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
                        toReturn[0] = "Success";
                        toReturn[1] = res[0].PostOffice[0].District;
                        toReturn[2] = res[0].PostOffice[0].State;
                    }
                    else
                    {
                        toReturn[0] = "Failure";
                    }
                }
            }
            return toReturn;
        }
        static public void AddTableHeaders(Table table, DataTable datatable)
        {
            TableHeaderRow headerRow = new TableHeaderRow();
            foreach (DataColumn column in datatable.Columns)
            {
                TableHeaderCell cell = new TableHeaderCell();
                cell.Text = column.ColumnName;
                cell.Scope = TableHeaderScope.Column;
                cell.CssClass = "th-sm";
                headerRow.Cells.Add(cell);
            }
            table.Controls.AddAt(0, headerRow);
        }
        static public void AddTableHeaders(Table table, DataTable datatable, TableHeaderCell[] ExtraCells)
        {
            TableHeaderRow headerRow = new TableHeaderRow();
            foreach (DataColumn column in datatable.Columns)
            {
                TableHeaderCell cell = new TableHeaderCell();
                cell.Text = column.ColumnName;
                cell.Scope = TableHeaderScope.Column;
                headerRow.Cells.Add(cell);
            }
            foreach (TableHeaderCell cell in ExtraCells)
            {
                headerRow.Cells.Add(cell);
            }
            table.Controls.AddAt(0, headerRow);
        }
        static public string CapFirstLetter(string s)
        {
            return s.Substring(0, 1).ToUpper() + s.ToLower().Substring(1, s.Length - 1);
        }
        static public string CapFirstLetters(string s, char splitOn=' ')
        {
            string[] split = s.Split(splitOn);
            string toReturn = "";
            for(int i = 0; i<split.Length-1;i++)
            {
                toReturn += CapFirstLetter(split[i]);
                toReturn += " ";
            }
            toReturn += CapFirstLetter(split[split.Length - 1]);
            return toReturn;
        }
        static public Tuple<int ,int> AttendanceBetween(DateTime start, DateTime end, string eID, string HREmail, SqlConnection connection = null)
        {
            if (connection == null || connection.State.ToString()!="Open")
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
                connection.Open();
            }
            SqlCommand getTot = new SqlCommand("Select Count(Distinct [date]) as count from [Attendance] where eID IN (Select eID from [Employee] Where employedHREmail=@hremail) and date>=@startDate and date<=@endDate", connection);
            getTot.Parameters.AddWithValue("hremail", HREmail);
            getTot.Parameters.AddWithValue("startDate", start.ToString("yyyy/MM/dd"));
            getTot.Parameters.AddWithValue("endDate", end.ToString("yyyy/MM/dd"));
            SqlDataAdapter adapter = new SqlDataAdapter(getTot);
            DataTable tot = new DataTable();
            adapter.Fill(tot);

            SqlCommand getUser = new SqlCommand("Select Count(date) as count from [Attendance] where eID=@id and date>=@startDate and date<=@endDate", connection);
            getUser.Parameters.AddWithValue("id", eID);
            getUser.Parameters.AddWithValue("startDate", start.ToString("yyyy/MM/dd"));
            getUser.Parameters.AddWithValue("endDate", end.ToString("yyyy/MM/dd"));
            adapter.SelectCommand = getUser;
            DataTable curr = new DataTable();
            adapter.Fill(curr);
            int val1=0, val2=0;
            if (tot.Rows.Count > 0)
            {
                val1 = (int)tot.Rows[0]["count"];
            }
            if (curr.Rows.Count > 0)
            {
                val2 = (int)curr.Rows[0]["count"];
            }
            return new Tuple<int, int>(val2, val1);
        }
    }
}