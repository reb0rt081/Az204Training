using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.Daos;
using Az204.Model.PersistenceLayer.PersistenceManagers;

namespace Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.Managers
{
    public class AzureBlobStoragePersistenceManager : PersistenceManager
    {
        public override ILoginDao GetLoginDao()
        {
            return loginDao ?? (loginDao = new LoginDao());
        }
    }
}
