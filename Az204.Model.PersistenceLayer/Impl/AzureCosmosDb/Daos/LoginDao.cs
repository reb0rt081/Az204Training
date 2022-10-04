using System.Net;
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

        public Task<List<Login>> GetLoginByLoginNameAndPassword(string loginName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Login> Save(Login login)
        {
            string endpointUri = "https://robertocosmosdb.documents.azure.com:443/";
            string primaryKey =
                "QBLROrRLoHmIkQhGeHElRzKECiUq06f0uj6EeengggOHRy2WWG9svQL6D7tgkARkbgENZwursjjpcJX8Ng0Jug==";
            string databaseId = "TestWebDatabse";
            CosmosClient cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { ApplicationName = "Az204TestWeb" });
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            string containerId = "Logins";
            //  partitionKey tiene que coincidir con el valor del objeto de dominio que queremos guardar
            Container container = await database.CreateContainerIfNotExistsAsync(containerId, "/partitionKey");

            LoginCosmosTableEntity item = new LoginCosmosTableEntity()
            {
                PartitionKey = login.Name,
                Id = login.Id.ToString(),
                Password = login.Password
            };

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
