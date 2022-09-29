using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.Managers;
using Az204.Model.PersistenceLayer.Impl.AzureTableStorage.Managers;
using Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.Managers;
using Az204.Model.UtilitiesLayer;
using System.Reflection.Metadata.Ecma335;

namespace Az204.Model.PersistenceLayer.PersistenceManagers
{
    public abstract class PersistenceManager
    {
        //  Singleton para asegurar que solamente utilizamos en alguno
        protected IAuditDao auditDao;
        protected ILoginDao loginDao;

        public abstract IAuditDao GetAuditDao();
        public abstract ILoginDao GetLoginDao();

        


        //  Los metodos estaticos se ejecutan a nivel de clase, no de objeto. Todos los objetos comparten esta propiedad.
        //  Hay que intentar evitar su uso.
        public static PersistenceManager GetPersistenceManager(AppUtilities.PersistenceTechnologies persistenceTechnology)
        {
            PersistenceManager persistenceManager = null;

            switch(persistenceTechnology)
            {
                case AppUtilities.PersistenceTechnologies.AZURE_COSMOS_DB:
                    persistenceManager = new AzureCosmosDbPersistenceManager();
                    break;
                case AppUtilities.PersistenceTechnologies.AZURE_TABLE_STORAGE:
                    persistenceManager = new AzureTableStoragePersistenceManager();
                    break;
                case AppUtilities.PersistenceTechnologies.AZURE_BLOB_STORAGE:
                    persistenceManager = new AzureBlobStoragePersistenceManager();
                    break;

            }

            return persistenceManager;
        }

        


    }
}
