using BLL.Dtos;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MailService
    {
        private MailSettingsDto _mailSettings;

        public MailService(MailSettingsDto mailSettings)
        {
            _mailSettings = mailSettings;

        }
        public void Send(MailRequestDto mailRequest)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_mailSettings.Mail);
                message.To.Add(new MailAddress(mailRequest.ToEmail));
                message.Subject = mailRequest.Subject;
                message.IsBodyHtml = mailRequest.HtmlAllowed;
                message.Body = mailRequest.Body;

                var smtp = new SmtpClient();
                smtp.Host = _mailSettings.Host;
                smtp.Port = _mailSettings.Port;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
