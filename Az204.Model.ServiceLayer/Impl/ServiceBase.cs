

using Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.Managers;
using Az204.Model.PersistenceLayer.Impl.AzureTableStorage.Managers;
using Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.Managers;
using Az204.Model.PersistenceLayer.PersistenceManagers;
using Az204.Model.UtilitiesLayer;

namespace Az204.Model.ServiceLayer.Impl
{
    public abstract class ServiceBase
    {
        protected List<PersistenceManager> persistenceManagers;
        
        public ServiceBase(List<PersistenceManager> persistenceManagers)
        {
            this.persistenceManagers = persistenceManagers;
        }

        protected PersistenceManager GetPersistenceManager(AppUtilities.PersistenceTechnologies persistenceTechnologies)
        {
            PersistenceManager persistenceManager = null;

            switch(persistenceTechnologies)
            {
                case AppUtilities.PersistenceTechnologies.AZURE_COSMOS_DB:
                    persistenceManager = persistenceManagers.FirstOrDefault(pm => pm is AzureCosmosDbPersistenceManager);
                    break;
                case AppUtilities.PersistenceTechnologies.AZURE_TABLE_STORAGE:
                    persistenceManager = persistenceManagers.FirstOrDefault(pm => pm is AzureTableStoragePersistenceManager);
                    break;
                case AppUtilities.PersistenceTechnologies.AZURE_BLOB_STORAGE:
                    persistenceManager = persistenceManagers.FirstOrDefault(pm => pm is AzureBlobStoragePersistenceManager);
                    break;
            }

            return persistenceManager;
        }
    }
}
