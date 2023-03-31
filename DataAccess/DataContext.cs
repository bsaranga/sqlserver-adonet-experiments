using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataAccess
{
    public class DataContext
    {
        private readonly string connectionString;
        private readonly ILogger<DataContext> logger;

        public DataContext(IConfiguration configuration, ILogger<DataContext> logger)
        {
            connectionString = configuration["ConnectionStrings:AdventureWorks"];
            this.logger = logger;
        }

        public void Connection()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Production.BillOfMaterials", connection);

                cmd.StatementCompleted += (s, e) => logger.LogInformation($"Rows: {e.RecordCount}");

                connection.Open();
                var reader = cmd.ExecuteReader();
            }
        }
    }
}