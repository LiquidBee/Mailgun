using System.Linq;
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
        private readonly MailgunOptions _options;

        public MailgunSender(MailgunClient client, IOptions<MailgunOptions> options, ILogger<MailgunSender> logger)
        {
            _client = client;
            _logger = logger;
            _options = options.Value;
        }

        public async Task SendMessage(MailgunMessage message)
        {
            if (_options.Debug)
            {
                _logger.LogDebug($"Mailgun debug mode: {message.To} {message.Subject}");
                return;
            }

            var content = new MultipartFormDataContent
            {
                {new StringContent(message.From), "from"},
                {new StringContent(string.Join(",", message.To)), "to"},
                {new StringContent(message.Subject), "subject"},
                {new StringContent(message.Html), "html"}
            };

            if (message.Cc.Any()) content.Add(new StringContent(string.Join(",", message.Cc)), "cc");
            if (message.Bcc.Any()) content.Add(new StringContent(string.Join(",", message.Bcc)), "bcc");
            if (message.Attachments.Any()) message.Attachments.ForEach(stream => content.Add(stream, "attachment"));

            var response = await _client.Send("messages", content);

            _logger.LogInformation(response.ToString());
        }
    }
}