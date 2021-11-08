using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ValidationUser
{
    public static class ValidationUser
    {
        [FunctionName(nameof(ValidationUser))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "{id}")] HttpRequest req,
            int id,
            ILogger log)
        {
            var str = Environment.GetEnvironmentVariable("ConnectionString");
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var text = $"UPDATE Person SET isActive = 1 WHERE Id = {id}; ";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    var rows = await cmd.ExecuteNonQueryAsync();
                    log.LogInformation($"{rows} rows were updated");

                    var result = rows > 0 ? "Conta confirmada com sucesso" : "Não é possivel atualizar este usuário.";
                    return new OkObjectResult(result);
                }
            }
        }
    }
}
