namespace Footballize.Services.Messaging
{
    using System;
    using System.Text;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Microsoft.AspNetCore.Identity.UI.Services;

    using Newtonsoft.Json;

    using SendGrid;
    using Common;
    
    // Documentation: https://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/index.html
    public class SendGridEmailSender : IEmailSender
    {
        private const string AuthenticationScheme = "Bearer";
        private const string BaseUrl = "https://api.sendgrid.com/v3/";
        private const string SendEmailUrlPath = "mail/send";

        private readonly string fromAddress;
        private readonly string fromName;
        private readonly HttpClient httpClient;
        private readonly ILogger<SendGridEmailSender> logger;

        public SendGridEmailSender(ILoggerFactory loggerFactory, string apiKey, string fromAddress, string fromName)
        {
            DataValidator.ThrowIfNull(loggerFactory, nameof(ILoggerFactory));
            DataValidator.ThrowIfNullOrEmpty(apiKey, nameof(apiKey));
            DataValidator.ThrowIfNullOrEmpty(fromAddress, nameof(fromAddress));
            DataValidator.ThrowIfNullOrEmpty(fromName, nameof(fromName));

            this.logger = loggerFactory.CreateLogger<SendGridEmailSender>();
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationScheme, apiKey);
            this.httpClient.BaseAddress = new Uri(BaseUrl);
            this.fromAddress = fromAddress;
            this.fromName = fromName;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {

            DataValidator.ThrowIfNullOrEmpty(this.fromAddress, nameof(this.fromAddress));
            DataValidator.ThrowIfNullOrEmpty(email, nameof(email));
            DataValidator.ThrowIfSubjectOrMessageIsEmpty(subject, message);


            var msg = new SendGridMessage(
                new SendGridEmail(email),
                subject,
                new SendGridEmail(this.fromAddress, this.fromName),
                message);

            try
            {
                var json = JsonConvert.SerializeObject(msg);
                var response = await this.httpClient.PostAsync(
                    SendEmailUrlPath,
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    // See if we can read the response for more information, then log the error
                    var errorJson = await response.Content.ReadAsStringAsync();
                    throw new Exception(
                        $"SendGrid indicated failure! Code: {response.StatusCode}, reason: {errorJson}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception during sending email: {ex}");
            }
        }
    }
}