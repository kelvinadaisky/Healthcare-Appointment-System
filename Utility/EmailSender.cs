using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;


namespace Web_prog_Project.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient("f6b82db6b4047392d8f3eef35aea750c", "42c01c4df284fe593bb3248cf5f6c588")
            {

            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
           .Property(Send.FromEmail, "kelvinadaiskyirutingabo2002@gmail.com")
           .Property(Send.FromName, "Stacy Kelvin Irutingabo")
           .Property(Send.Subject, subject)
           .Property(Send.HtmlPart, htmlMessage)
           .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", email}
                 }
               });
            MailjetResponse response = await client.PostAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                // Log the error or throw an exception
                throw new Exception($"Failed to send email: {response.GetErrorMessage}");
            }

        }
    }
}