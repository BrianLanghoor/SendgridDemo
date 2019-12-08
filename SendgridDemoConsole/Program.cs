using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SendgridDemo
{
    class Program
    {
        private const string Apikey = "<<API key hier>>";
        private static readonly SendGridClient Client = new SendGridClient(Apikey);

        static async Task Main(string[] args)
        {
            //var msg = new SendGridMessage();

            //msg.SetFrom(new EmailAddress("brian_langhoor@hotmail.com", "Kees"));

            //var recipients = new List<EmailAddress>
            //{
            //    new EmailAddress("brian.langhoor@teamrockstars.nl", "Brian Langhoor"),
            //};

            //msg.AddTos(recipients);

            //msg.SetSubject("Mail opgesteld vanuit code");

            ////msg.AddContent(MimeType.Text, "Hello World plain text!");
            //msg.AddContent(MimeType.Html, "<p>Hello World!</p>");

            //var response = await Client.SendEmailAsync(msg);

            var requestmessage = new HttpRequestMessage(HttpMethod.Post, "https://api.sendgrid.com/v3/mail/send");

            requestmessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Apikey);
            requestmessage.Content = new StringContent(
                "{\n  " +
                    "\"personalizations\": [\n    " +
                        "{\n      " +
                            "\"to\": [\n        " +
                                "{\n          " +
                                    "\"email\": \"brian.langhoor@teamrockstars.nl\",\n          " +
                                    "\"name\": \"Brian Langhoor\"\n        " +
                                "}\n      " +
                            "],\n      " +
                            "\"subject\": \"Hello, World!\"\n    " +
                        "}\n  " +
                    "],\n  " +
                    "\"from\": {\n    " +
                        "\"email\": \"brian_langhoor@hotmail.com\",\n    " +
                        "\"name\": \"Brian Langhoor\"\n  " +
                    "},\n  " +
                    "\"template_id\": \"<<TemplateId hier>>\"\n" +
                "}",
                Encoding.UTF8,
                "application/json"
                );

            var response = await Client.MakeRequest(requestmessage);

            Console.WriteLine(response.StatusCode);
        }
    }
}
