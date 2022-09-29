using Az204.Model.PersistenceLayer.PersistenceManagers;
using Az204.Model.ServiceLayer.Api;
using Az204.Model.ServiceLayer.Impl;

namespace Az204.Model.ServiceLayer.Managers
{
    //  Este servicio debe identificar el servicio que se encarga de gestionar la petición.
    //  Este objeto será la factoría de servicios que se ocupa de la petición, es decir, el experto.
    public class ServiceManager : IServiceManager
    {
        List<PersistenceManager> persistenceManagers;

        public ServiceManager()
        {
            persistenceManagers = new List<PersistenceManager>();
            persistenceManagers.Add(PersistenceManager.GetPersistenceManager(UtilitiesLayer.AppUtilities.PersistenceTechnologies.AZURE_COSMOS_DB));
            persistenceManagers.Add(PersistenceManager.GetPersistenceManager(UtilitiesLayer.AppUtilities.PersistenceTechnologies.AZURE_BLOB_STORAGE));
            persistenceManagers.Add(PersistenceManager.GetPersistenceManager(UtilitiesLayer.AppUtilities.PersistenceTechnologies.AZURE_TABLE_STORAGE));
        }

        private ILoginService loginService;
        private IAuditService auditService;

        public ILoginService GetLoginService()
        {
            //  Si generamos un nuevo ServiceManager por cada petición se devolverá la instancia existente de un servicio singleton.
            return loginService ?? (loginService = new LoginService(persistenceManagers));
        }

        public IAuditService GetAuditService()
        {
            return auditService ?? (auditService = new AuditService(persistenceManagers));
        }
    }
}
