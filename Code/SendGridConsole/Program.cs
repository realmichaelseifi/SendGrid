using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SendGridConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var configs = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false, true)
                .Build();
            var apiKey = configs["API_KEY"];
        }

        static async Task ExecuteSendEmailAsync(params object[] args)
        {

        }
    }
}
