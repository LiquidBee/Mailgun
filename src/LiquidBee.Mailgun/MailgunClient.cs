using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace LiquidBee.Mailgun
{
    public class MailgunClient
    {
        private readonly MailgunOptions _options;

        public MailgunClient(IOptions<MailgunOptions> options)
        {
            _options = options.Value;
        }

        public async Task<HttpResponseMessage> Send(string endpoint, HttpContent content)
        {
            var client = new HttpClient();

            var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_options.Key}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

            return await client.PostAsync(new Uri($"https://api.mailgun.net/v3/{_options.Domain}/{endpoint}"), content);
        }
    }
}