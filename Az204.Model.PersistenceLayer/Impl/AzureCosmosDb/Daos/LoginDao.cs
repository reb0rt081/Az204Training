﻿using System.Net;
using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.TableEntities;
using Microsoft.Azure.Cosmos;

namespace Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.Daos
{
    public class LoginDao : ILoginDao
    {
        public Task<List<Login>> GetLogins()
        {
            throw new NotImplementedException();
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

            //  CONEJO: Utiliza el editor manual de consultas en el portal de Azure Cosmos DB para probar que la consulta funciona
            var sqlQueryText = $"SELECT * FROM c WHERE c.partitionKey = '{loginName}' and c.Password = '{password}'";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<LoginCosmosTableEntity> queryResultSetIterator = container.GetItemQueryIterator<LoginCosmosTableEntity>(queryDefinition);

            List<Login> logins = new List<Login>();
            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<LoginCosmosTableEntity> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (LoginCosmosTableEntity loginCosmos in currentResultSet)
                {
                    logins.Add(new Login{ Id = Guid.Parse(loginCosmos.Id), Password = loginCosmos.Password, Name = loginCosmos.PartitionKey});
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
                // Read the item to see if it exists.  
                ItemResponse<LoginCosmosTableEntity> andersenFamilyResponse = await container.ReadItemAsync<LoginCosmosTableEntity>(item.Id, new PartitionKey(item.PartitionKey));
                Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
                ItemResponse<LoginCosmosTableEntity> andersenFamilyResponse = await container.CreateItemAsync<LoginCosmosTableEntity>(item, new PartitionKey(item.PartitionKey));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", andersenFamilyResponse.Resource.Id, andersenFamilyResponse.RequestCharge);
            }

            return login;
        }
    }
}
