using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ResetOnboarding
{
    public static class ResetOnboarding
    {
        [FunctionName(nameof(ResetOnboarding))]
        public static async Task Run([TimerTrigger("0 /3 * * *")]TimerInfo myTimer, ILogger log)
        {
            var str = Environment.GetEnvironmentVariable("ConnectionString");
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var text = "Delete Person WHERE isActive = 0 and Created < DATEADD(day, -1, GETDATE()); ";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    var rows = await cmd.ExecuteNonQueryAsync();
                    log.LogInformation($"{rows} rows were updated");
                }
            }
        }
    }
}
