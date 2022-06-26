using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TangyWeb_API.Helper
{
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// To send mails from personal emails accounts
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="htmlMessage"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("hello@dotnetmastery.com"));
                emailToSend.To.Add(MailboxAddress.Parse(email));
                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };
                //send email
                using var emailClient = new SmtpClient();
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                //Always use app accesing password
                emailClient.Authenticate("INSERTARMAILPROCEDENCIA","INSERTARAPPKEY");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// To send mails from an domain account email
        /// https://sendgrid.com/go/email-brand-signup-sales-1?utm_source=google&utm_medium=cpc&utm_term=sendgrid&utm_campaign=SendGrid_G_S_LATAM_Brand_(English)&cq_plac=&cq_net=g&cq_pos=&cq_med=&cq_plt=gp&gclid=CjwKCAjw5NqVBhAjEiwAeCa97SDhY5CzRPs5RJ4Buqov35jGOYYZf_e1dJKjotr9rcqbLLU0jcDDfxoCpmIQAvD_BwE
        /// </summary>
        /// <returns></returns>
        public async Task SendEmailAsyn_FromDomain(string email, string subject, string htmlMessage)
        {
            try
            {
                var client = new SendGridClient("SendGridSecret");
                var from = new EmailAddress("hello@dotnetmastery.com", "Tangy");
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
                await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
