using Microsoft.Azure.WebJobs;
using SendGrid.Helpers.Mail;
using SendMail.Model;
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
        [SendGrid(ApiKey = "CustomSendGridKeyAppSettingName")] IAsyncCollector<SendGridMessage> messageCollector)
        {
            var emailObject = JsonSerializer.Deserialize<Mail>(message);

            var mailMessage = new SendGridMessage();
            mailMessage.AddTo(emailObject.To);
            mailMessage.AddContent("text/html", emailObject.Body);
            mailMessage.SetFrom(new EmailAddress(emailObject.From));
            mailMessage.SetSubject(emailObject.Subject);

            await messageCollector.AddAsync(mailMessage);
        }
    }
}
