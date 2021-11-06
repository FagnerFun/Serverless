using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using SaveUser.Model;
using System.Threading.Tasks;

namespace SaveUser.Function
{
    public class SaveUserOrchestrator
    {
        [FunctionName(nameof(SaveUserOrchestrator))]
        public static async Task<User> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var user = context.GetInput<User>();

            var saved = await context.CallActivityAsync<User>(nameof(PersistUserActivity), user);
            var sended = await context.CallActivityAsync<User>(nameof(StartOnboardingActivity), saved);

            return sended;
        }
    }
}
