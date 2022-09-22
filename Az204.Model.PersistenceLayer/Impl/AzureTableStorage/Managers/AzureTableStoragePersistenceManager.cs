using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureTableStorage.Daos;
using Az204.Model.PersistenceLayer.PersistenceManagers;

namespace Az204.Model.PersistenceLayer.Impl.AzureTableStorage.Managers
{
    public class AzureTableStoragePersistenceManager : PersistenceManager
    {
        public override ILoginDao GetLoginDao()
        {
            return loginDao ?? (loginDao = new LoginDao());
        }
    }
}
