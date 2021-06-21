using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SendGridConsole
{
    class Program
    {
        private static string API_KEY;
        private static string FROM_ADDRESS;
        public static string[] TO_ADDRESS;

        static async Task  Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var configs = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false, true)
                .Build();
            API_KEY = configs["API_KEY"];
            FROM_ADDRESS = configs["FROM_ADDRESS"];
            TO_ADDRESS = configs["TO_ADDRESS"].Split(';');

            await ExecuteSendEmailAsync(args).ConfigureAwait(false);
        }

        static async Task ExecuteSendEmailAsync(params string[] args)
        {
            var client = new SendGridClient(new SendGridClientOptions
            {
                ApiKey = API_KEY
            });

            var from = new EmailAddress(FROM_ADDRESS, "Notification System");
            var tos = new List<EmailAddress>();
            foreach (var to in TO_ADDRESS)
            {
                tos.Add(new EmailAddress(to));
            }

            var subject = "Testing SendGrid Notification System";
            var body = "Testing SendGrid Notification System";
            var content = "<strong>Reply back to let me know that you got this</strong><br/><br/>Michael Seifi";

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(
                from, tos, subject, body, content);

            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
