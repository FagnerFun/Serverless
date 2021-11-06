using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SaveUser.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SaveUser
{
    public static class PersistUserActivity
    {
        [FunctionName(nameof(PersistUserActivity))]
        public static async Task<User> PersistUser([ActivityTrigger] User user, ILogger log)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                var rows = await conexao.QueryAsync<int>(@"Insert 
                                Person
                                    (FirstName, LastName, Mail, Created, IsActive)
                                values
                                    (@FirstName, @LastName, @Mail, @Created, @IsActive);
                                SELECT CAST(SCOPE_IDENTITY() as int)",
                            new { user.FirstName, user.LastName, user.Mail, Created = DateTime.Now, IsActive = false });

                log.LogInformation($"Id: {rows.Single()}");
                user.Id = rows.Single();
                return user;
            }
        }
    }
}
