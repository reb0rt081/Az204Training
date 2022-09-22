using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.Daos;
using Az204.Model.PersistenceLayer.PersistenceManagers;

namespace Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.Managers
{
    public class AzureCosmosDbPersistenceManager : PersistenceManager
    {
        public override ILoginDao GetLoginDao()
        {
            return loginDao ?? (loginDao = new LoginDao());
        }
    }
}
