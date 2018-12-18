using System.Collections.Generic;
using System.Net.Http;

namespace LiquidBee.Mailgun
{
    public class MailgunMessage
    {
        public string From { get; set; }

        public List<string> To { get; } = new List<string>();

        public List<string> Cc { get; } = new List<string>();

        public List<string> Bcc { get; } = new List<string>();

        public string Subject { get; set; }

        public string Html { get; set; }

        public List<StreamContent> Attachments { get; } = new List<StreamContent>();

        public MailgunMessage(string from, string subject = null, string html = null)
        {
            From = from;
            Subject = subject;
            Html = html;
        }
    }
}