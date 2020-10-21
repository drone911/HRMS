using System.Collections.Generic;
using System.Web;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Net.Http;
using System.Web.UI.WebControls;
using System.Data;

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
    }
}