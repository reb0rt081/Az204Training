using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureTableStorage.TableEntities;
using Azure;
using Azure.Data.Tables;

namespace Az204.Model.PersistenceLayer.Impl.AzureTableStorage.Daos
{
    public class AuditDao : IAuditDao
    {
        public async Task<HttpRequestAudit> SaveHttpRequestAudit(HttpRequestAudit httpRequestAudit)
        {
            //  La connection string se puede obtener en el portal de Azure
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=ribesstorageaccount;AccountKey=8L3wQW9VMMr7cerg1w3Y7v5fnRo5rID0bYFuLK8WcjIucdDJbzltystBz85pyPsK87tXtvtFReop+AStyOe8Jg==;EndpointSuffix=core.windows.net";

            //  El table name tiene que coincidir con la tabla que hemos creado en el portal
            //  Es mejor que el tableclient sea un singleton que se comparte en el DAO para evitar el port exhaust
            TableClient tableClient = new TableClient(connectionString, "audits");

            await tableClient.CreateIfNotExistsAsync();
            
            //  Tambien se pueden guardar entitades en batch y no una a una
            // Tambien prueba UpdateEntityAsync
            //  Habría que analizar la respuesta con try-catch y mirar el response para ver si ha ido bien
            Response? response = await tableClient.UpsertEntityAsync(new HttpRequestAuditTableEntity(httpRequestAudit));

            return httpRequestAudit;
        }

        public Task<string> GetSasUrl(string containerName, string blob)
        {
            throw new NotImplementedException();
        }
    }
}
