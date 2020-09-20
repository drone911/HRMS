using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace hrms
{
    public static class Util
    {
        static public void sendEmail(string to, string subject, string body)
        {
            // change gmail
            MailMessage mail = new MailMessage("something@gmail.com", to,subject, body);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
        }

    }
}