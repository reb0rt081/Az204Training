using System.Net;
using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.TableEntities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.Daos
{
    public class LoginDao : ILoginDao
    {
        public async Task<List<Login>> GetLogins()
        {
            //  Puede ser cualquier nombre de application
            string applicationName = "Az204TestWeb";
            string endpointUri = "https://robertocosmosdb.documents.azure.com:443/";
            string primaryKey = "QBLROrRLoHmIkQhGeHElRzKECiUq06f0uj6EeengggOHRy2WWG9svQL6D7tgkARkbgENZwursjjpcJX8Ng0Jug==";
            string databaseId = "TestWebDatabse";
            string containerId = "Logins";

            CosmosClient cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { ApplicationName = applicationName });
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            //  partitionKey tiene que coincidir con el nombre de la propiedad del objeto de dominio que queremos guardar (en este caso LoginCosmosTableEntity.partitionKey)
            Container container = await database.CreateContainerIfNotExistsAsync(containerId, "/partitionKey");

            var sqlQueryText = "SELECT * FROM c";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            List<Login> logins = new List<Login>();

            FeedIterator<LoginCosmosTableEntity> queryResultSetIterator = container.GetItemQueryIterator<LoginCosmosTableEntity>(queryDefinition);
            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<LoginCosmosTableEntity> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (LoginCosmosTableEntity loginCosmos in currentResultSet)
                {
                    logins.Add(new Login { Id = Guid.Parse(loginCosmos.Id), Password = loginCosmos.Password, Name = loginCosmos.PartitionKey });
                }
            }
            
            return logins;
        }

        public async Task<List<Login>> GetLoginByLoginNameAndPassword(string loginName, string password)
        {
            //  Puede ser cualquier nombre de application
            string applicationName = "Az204TestWeb";
            string endpointUri = "https://robertocosmosdb.documents.azure.com:443/";
            string primaryKey = "QBLROrRLoHmIkQhGeHElRzKECiUq06f0uj6EeengggOHRy2WWG9svQL6D7tgkARkbgENZwursjjpcJX8Ng0Jug==";
            string databaseId = "TestWebDatabse";
            string containerId = "Logins";

            CosmosClient cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { ApplicationName = applicationName });
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            //  partitionKey tiene que coincidir con el nombre de la propiedad del objeto de dominio que queremos guardar (en este caso LoginCosmosTableEntity.partitionKey)
            Container container = await database.CreateContainerIfNotExistsAsync(containerId, "/partitionKey");

            //  CONSEJO: Utiliza el editor manual de consultas en el portal de Azure Cosmos DB para probar que la consulta funciona
            //  Alternativa de consulta utilizando la instrucción SQL en lugar de LINQ
            var sqlQueryText = $"SELECT * FROM c WHERE c.partitionKey = '{loginName}' and c.Password = '{password}'";
            //  QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            //  FeedIterator<LoginCosmosTableEntity> queryResultSetIterator = container.GetItemQueryIterator<LoginCosmosTableEntity>(queryDefinition);

            List<Login> logins = new List<Login>();
            using (FeedIterator<LoginCosmosTableEntity> queryResultSetIterator = container.GetItemLinqQueryable<LoginCosmosTableEntity>().Where(lc => lc.PartitionKey == loginName && lc.Password == password).ToFeedIterator<LoginCosmosTableEntity>())
            {
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<LoginCosmosTableEntity> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (LoginCosmosTableEntity loginCosmos in currentResultSet)
                    {
                        logins.Add(new Login { Id = Guid.Parse(loginCosmos.Id), Password = loginCosmos.Password, Name = loginCosmos.PartitionKey });
                    }
                }
            }

            return logins;
        }

        public async Task<Login> Save(Login login)
        {
            //  Puede ser cualquier nombre de application
            string applicationName = "Az204TestWeb";
            string endpointUri = "https://robertocosmosdb.documents.azure.com:443/";
            string primaryKey = "QBLROrRLoHmIkQhGeHElRzKECiUq06f0uj6EeengggOHRy2WWG9svQL6D7tgkARkbgENZwursjjpcJX8Ng0Jug==";
            string databaseId = "TestWebDatabse";
            string containerId = "Logins";

            CosmosClient cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { ApplicationName = applicationName });
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            //  partitionKey tiene que coincidir con el nombre de la propiedad del objeto de dominio que queremos guardar (en este caso LoginCosmosTableEntity.partitionKey)
            Container container = await database.CreateContainerIfNotExistsAsync(containerId, "/partitionKey");

            LoginCosmosTableEntity item = new LoginCosmosTableEntity(login);

            try
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
                ItemResponse<LoginCosmosTableEntity> createLoginResponse = await container.CreateItemAsync<LoginCosmosTableEntity>(item, new PartitionKey(item.PartitionKey));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", createLoginResponse.Resource.Id, createLoginResponse.RequestCharge);

                // Read the item to see if it exists.  
                createLoginResponse = await container.ReadItemAsync<LoginCosmosTableEntity>(item.Id, new PartitionKey(item.PartitionKey));
                Console.WriteLine("Item in database with id: {0} finally exists\n", createLoginResponse.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Replace an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
                ItemResponse<LoginCosmosTableEntity> replaceLoginResponse = await container.ReplaceItemAsync(item, item.Id, new PartitionKey(item.PartitionKey));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Updated item in database with id: {0} Operation consumed {1} RUs.\n", replaceLoginResponse.Resource.Id, replaceLoginResponse.RequestCharge);
            }

            return login;
        }
    }
}
