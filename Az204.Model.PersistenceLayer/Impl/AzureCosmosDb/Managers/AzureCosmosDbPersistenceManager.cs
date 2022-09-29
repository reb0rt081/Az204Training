using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.Daos;
using Az204.Model.PersistenceLayer.PersistenceManagers;
using Az204.Model.UtilitiesLayer;

namespace Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.Managers
{
    public class AzureCosmosDbPersistenceManager : PersistenceManager
    {
        public override IAuditDao GetAuditDao()
        {
            //  
            throw new NotSupportedException($"{AppUtilities.PersistenceTechnologies.AZURE_COSMOS_DB.ToString()} is not a valid Persistence Storage for Audit");
        }

        public override ILoginDao GetLoginDao()
        {
            return loginDao ?? (loginDao = new LoginDao());
        }
    }
}
