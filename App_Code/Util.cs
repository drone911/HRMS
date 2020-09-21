using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace hrms
{
    public static class Util
    {
        static public void sendEmail(string to, string subject, string body)
        {
            // change gmail
            MailMessage mail = new MailMessage("something@gmail.com", to, subject, body);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
        }
        static public void timeoutAndRedirect(Page currentPage, string timeOutUrl, int timeInSeconds = 2)
        {
            HtmlMeta oScript = new HtmlMeta();
            oScript.Attributes.Add("http-equiv", "REFRESH");
            oScript.Attributes.Add("content", timeInSeconds.ToString() + "; url='" + timeOutUrl + "'");
            currentPage.Header.Controls.Add(oScript);
        }
    }
}