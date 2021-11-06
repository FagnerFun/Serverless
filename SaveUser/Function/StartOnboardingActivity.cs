using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SaveUser.Model;
using System;

namespace SaveUser
{
    public static class StartOnboardingActivity
    {
        [FunctionName(nameof(StartOnboardingActivity))]
        public static User StartOnboarding([ActivityTrigger] User user, ILogger log)
        {
            string connectionString = Environment.GetEnvironmentVariable("ServiceBusConnection");
            string queueName = Environment.GetEnvironmentVariable("QueueName");

            ServiceBusClient client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(queueName);

            var json = JsonConvert.SerializeObject(user);

            ServiceBusMessage message = new ServiceBusMessage(json);
            sender.SendMessageAsync(message);

            log.LogInformation($"Start Onboarding - {user.FirstName}.");
            return user;
        }
    }
}
