
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using WcfService.PDF;
using WcfService.Persistence;

namespace WcfService.Email
{
    public class EmailSender
    {
        private string emailFrom;
        private string emailTo;
        private string password;
        public EmailSender(string emailTo, string emailFrom = "crossover.vinicius.arruda@gmail.com", string password = "crossover")
        {
            this.emailFrom = emailFrom;
            this.emailTo = emailTo;
            this.password = password;
        }


        public bool SendProfiles(List<MemoryStream> profiles)
        {
            try
            {
                while (profiles.Count > 0)
                {
                    int indexCount = 10;
                    if (profiles.Count < 10)
                        indexCount = profiles.Count;
                    List<MemoryStream> attachs = profiles.GetRange(0, indexCount);
                    profiles.RemoveRange(0, indexCount);

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = "Criminal Profile";
                    mail.Body = "Your search result is attached as PDF, thanks for using our services.";
                    indexCount = 0;
                    foreach (Stream s in attachs)
                    {
                        indexCount++;
                        mail.Attachments.Add(new Attachment(s, "attachment"+indexCount+".pdf"));
                    }

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, password);
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}