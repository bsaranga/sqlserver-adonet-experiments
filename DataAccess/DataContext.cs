using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

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

        public void ForwardOnlyConnectedAccess()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Production.Product", connection);

                cmd.StatementCompleted += (s, e) => logger.LogInformation($"Rows: {e.RecordCount}");

                connection.Open();

                using(var reader = cmd.ExecuteReader())
                {
                    var columnSchema = reader.GetColumnSchema();
                    var dataTable = new DataTable();

                    foreach (var col in columnSchema)
                    {
                        dataTable.Columns.Add(col.ColumnName, col.DataType!);
                    }

                    while (reader.Read())
                    {
                        var dataRow = dataTable.NewRow();
                        
                        foreach (var col in columnSchema)
                        {
                            dataRow[col.ColumnName] = reader[col.ColumnName];
                        }

                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
        }

        public void DisconnectedAccess()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var entity = "Product";
                var dataAdapter = new SqlDataAdapter($"SELECT * FROM Production.{entity}", connection);
                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet, entity);
                
                var table = dataSet.Tables[entity];
                var rows = table.Rows;
                
                foreach (DataRow row in rows)
                {
                    
                }
            }
        }
    }
}