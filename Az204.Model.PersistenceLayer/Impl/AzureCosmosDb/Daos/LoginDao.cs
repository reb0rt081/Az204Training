using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Api;

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

        public Task<Login> Save(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
