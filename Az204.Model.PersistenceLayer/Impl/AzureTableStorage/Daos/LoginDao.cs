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
            //  La connection string se puede obtener en el portal de Azure
            string connectionString = "connectionStringFromAzurePortal";

            TableClient tableClient = new TableClient(connectionString, "logins");

            Pageable<LoginTableEntity>? query = tableClient.Query<LoginTableEntity>();
            List<Login> logins = new List<Login>();

            foreach (LoginTableEntity? loginTableEntity in query)
            {
                logins.Add(new Login
                {
                    Id = Guid.Parse(loginTableEntity.Id),
                    Name = loginTableEntity.PartitionKey,
                    Password = loginTableEntity.RowKey
                });
            }

            return Task.FromResult(logins);
        }

        public Task<List<Login>> GetLoginByLoginNameAndPassword(string loginName, string password)
        {
            //  La connection string se puede obtener en el portal de Azure
            string connectionString = "connectionStringFromAzurePortal";

            TableClient tableClient = new TableClient(connectionString, "logins");

            Pageable<LoginTableEntity>? query = tableClient.Query<LoginTableEntity>(lte => lte.PartitionKey == loginName && lte.RowKey == password);
            List<Login> logins = new List<Login>();

            foreach (LoginTableEntity? loginTableEntity in query)
            {
                logins.Add(new Login
                {
                    Id = Guid.Parse(loginTableEntity.Id),
                    Name = loginTableEntity.PartitionKey,
                    Password = loginTableEntity.RowKey
                });
            }

            return Task.FromResult(logins);
        }

        public async Task<Login> Save(Login login)
        {
            //  La connection string se puede obtener en el portal de Azure
            string connectionString = "connectionStringFromAzurePortal";
            
            //  El table name tiene que coincidir con la tabla que hemos creado en el portal
            //  Es mejor que el tableclient sea un singleton que se comparte en el DAO para evitar el port exhaust
            TableClient tableClient = new TableClient(connectionString, "logins");

            await tableClient.CreateIfNotExistsAsync();

            //  Tambien se pueden guardar entitades en batch y no una a una
            Response? response = await tableClient.UpdateEntityAsync(new LoginTableEntity(login), ETag.All);

            return login;
        }
    }
}
