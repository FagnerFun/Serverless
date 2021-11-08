using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace ValidateUser
{
    public static class ValidateUser
    {
        [Function(nameof(ValidateUser))]
        public static async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route ="{id}")]
            HttpRequestData req,
            string id, 
            FunctionContext executionContext,
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

                    var response = req.CreateResponse(HttpStatusCode.OK);
                    response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                    response.WriteString(rows > 0 ? "Conta confirmada com sucesso" : "Não é possivel atualizar este usuário.");

                    return response;
                }
            }
        }
    }
}
