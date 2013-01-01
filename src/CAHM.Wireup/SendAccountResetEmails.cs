using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace CAHM.Wireup
{
    public class SendAccountResetEmails : ISendAccountResetEmails
    {
        public void SendResetEmail(string email, string passwordResetUrl)
        {
            var from = ConfigurationManager.AppSettings["fromAddress"];
            var message = new MailMessage
                {
                    From = new MailAddress(from)
                };

            message.To.Add(new MailAddress(email));

            message.Subject = "Reset your password at Curds Against Huge Manatee";
            message.Body = GenerateBody(passwordResetUrl);
            message.IsBodyHtml = true;

            var client = new SmtpClient();
            client.Send(message);
        }

        private static string GenerateBody(string passwordResetUrl)
        {
            var bldr = new StringBuilder();

            bldr.AppendFormat("<a href=\"{0}\">Click here to reset your password.</a>", HttpUtility.HtmlAttributeEncode(passwordResetUrl));
            bldr.AppendLine();

            return bldr.ToString();
        }

    }
}
