using System;
using Microsoft.Extensions.DependencyInjection;

namespace LiquidBee.Mailgun
{
    public static class MailgunExtensions
    {
        public static IServiceCollection AddMailgun(this IServiceCollection services, Action<MailgunOptions> configurationOptions)
        {
            services.Configure(configurationOptions);
            services.AddSingleton<MailgunClient>();
            services.AddSingleton<MailgunSender>();
            return services;
        }
    }
}