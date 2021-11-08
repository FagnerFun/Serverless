using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using SendMail.MailTemplate;
using SendMail.Model;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SendMail
{
    public class SendMail
    {
        [FunctionName(nameof(SendMail))]
        public static async Task Run(
        [ServiceBusTrigger("onboardinguserqueue", Connection = "ServiceBusConnection")] string message,
        [SendGrid(ApiKey = "SendgridAPIKey")] IAsyncCollector<SendGridMessage> messageCollector,
        ILogger log)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(message);

                var mailMessage = new SendGridMessage();
                mailMessage.AddTo(user.Mail);
                mailMessage.AddContent("text/html", string.Format(OnboardingTemplate.Mail, user.FirstName));
                mailMessage.SetFrom(new EmailAddress("fagner.santos@dextra-sw.com", "MVP Conf"));
                mailMessage.SetSubject($"Seja bem vindo ao MVP Conf 2021 {user.FirstName}!");

                await messageCollector.AddAsync(mailMessage);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Erro no envio de email.");
                throw;
            }
        }
    }
}
