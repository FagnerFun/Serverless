using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SaveUser.Model;
using SaveUser.Validator;
using System.Net.Http;
using System.Threading.Tasks;

namespace SaveUser.Function
{
    public static class SaveUserStarter
    {
        [FunctionName(nameof(SaveUserStarter))]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var user = await req.Content.ReadAsAsync<User>();
            var validation = new UserValidator().Validate(user);
            if (!validation.IsValid)
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new StringContent(string.Join(" ", validation.Errors)),
                };

            string instanceId = await starter.StartNewAsync(nameof(SaveUserOrchestrator), user);
            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return await starter.WaitForCompletionOrCreateCheckStatusResponseAsync(req, instanceId);
        }
    }
}