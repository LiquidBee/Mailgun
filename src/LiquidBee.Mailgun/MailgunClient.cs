using System;
using System.Net.Http;
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
            return await new HttpClient()
                .PostAsync(new Uri($"https://api:{_options.Key}@api.mailgun.net/v3/{_options.Domain}/{endpoint}"), content);
        }
    }
}