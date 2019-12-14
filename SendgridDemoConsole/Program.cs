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
        private const string Apikey = "<<<<Insert API key here>>>>"; //Requires attention
        private static readonly SendGridClient Client = new SendGridClient(Apikey);

        static async Task Main(string[] args)
        {
            var response = await SendCustomEmail();

            //var response = await SendTemplateEmail();

            Console.WriteLine(response.StatusCode);
        }

        private static async Task<Response> SendCustomEmail()
        {
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("<<<<Insert Sender email here>>>>", "<<<<Insert Sender name here>>>>")); //Requires attention

            var recipients = new List<EmailAddress>
            {
                new EmailAddress("<<<<Insert Addressee email here>>>>", "<<<<Insert Addressee name here>>>>"),//Requires attention
            };

            msg.AddTos(recipients);

            msg.SetSubject("Mail opgesteld vanuit code");

            msg.AddContent(MimeType.Html, "<p>Hello World!</p>");

            return await Client.SendEmailAsync(msg);
        }

        private static async Task<Response> SendTemplateEmail()
        {
            var requestmessage = new HttpRequestMessage(HttpMethod.Post, "https://api.sendgrid.com/v3/mail/send");

            requestmessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Apikey);
            requestmessage.Content = new StringContent(
                "{\n  " +
                "\"personalizations\": [\n    " +
                "{\n      " +
                "\"to\": [\n        " +
                "{\n          " +
                "\"email\": \"<<<<Insert Addressee email here>>>>\",\n          " + //Requires attention
                "\"name\": \"<<<<Insert Addressee name here>>>>\"\n        " +      //Requires attention
                "}\n      " +
                "],\n      " +
                "}\n  " +
                "],\n  " +
                "\"from\": {\n    " +
                "\"email\": \"<<<<Insert Sender email here>>>>\",\n    " +          //Requires attention
                "\"name\": \"<<<<Insert Sender name here>>>>\"\n  " +               //Requires attention
                "},\n  " +
                "\"template_id\": \"<<<<Insert Template ID here>>>>\"\n" +          //Requires attention
                "}",
                Encoding.UTF8,
                "application/json"
            );

            return await Client.MakeRequest(requestmessage);
        }
    }
}
