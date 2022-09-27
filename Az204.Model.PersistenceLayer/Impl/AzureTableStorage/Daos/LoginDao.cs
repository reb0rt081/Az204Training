using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureTableStorage.TableEntities;

using Azure;
using Azure.Data.Tables;

namespace Az204.Model.PersistenceLayer.Impl.AzureTableStorage.Daos
{
    public class LoginDao : ILoginDao
    {
        public Task<List<Login>> GetLogins()
        {
            throw new NotImplementedException();
        }

        public async Task<Login> Save(Login login)
        {
            //  La connection string se puede obtener en el portal de Azure
            string connectionString = "connectionStringFromAzurePortal";
            
            //  El table name tiene que coindifir con la tabla que hemos creado en el portal
            TableClient tableClient = new TableClient(connectionString, "logins");

            await tableClient.CreateIfNotExistsAsync();

            Response? response = await tableClient.UpdateEntityAsync(new LoginTableEntity(login), ETag.All);

            return login;
        }
    }
}
