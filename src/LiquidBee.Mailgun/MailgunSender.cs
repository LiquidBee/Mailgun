using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LiquidBee.Mailgun
{
    public class MailgunSender
    {
        private readonly MailgunClient _client;
        private readonly ILogger<MailgunSender> _logger;
        private readonly string _from;

        public MailgunSender(MailgunClient client, IOptions<MailgunOptions> options, ILogger<MailgunSender> logger)
        {
            _client = client;
            _logger = logger;
            _from = options.Value.From;
        }

        public async Task SendMessage(string to, string subject, string html, string from = null)
        {
            if (to == null || subject == null || html == null)
            {
                _logger.LogError($"Send message argument is null! {to} : {subject} = {html}");
                return;
            }

            var content = new MultipartFormDataContent
            {
                {new StringContent(from ?? _from), "from"},
                {new StringContent(to), "to"},
                {new StringContent(subject), "subject"},
                {new StringContent(html), "html"}
            };

            var response = await _client.Send("messages", content);
            
            _logger.LogInformation(response.ToString());
        }
    }
}