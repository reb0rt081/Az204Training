using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.BlobDtos;
using Az204.Model.PersistenceLayer.PersistenceManagers;
using Az204.Model.ServiceLayer.Api;
using Az204.Model.UtilitiesLayer;

namespace Az204.Model.ServiceLayer.Impl
{
    public class AuditService: ServiceBase, IAuditService
    {
        public AuditService(List<PersistenceManager> persistenceManagers) : base(persistenceManagers)
        {
        }

        public async Task<HttpRequestAudit> SaveHttpAuditRequest(HttpRequestAudit httpRequestAudit)
        {
            await GetPersistenceManager(AppUtilities.PersistenceTechnologies.AZURE_BLOB_STORAGE).GetAuditDao()
                .SaveHttpRequestAudit(httpRequestAudit);

            await GetPersistenceManager(AppUtilities.PersistenceTechnologies.AZURE_TABLE_STORAGE).GetAuditDao()
                .SaveHttpRequestAudit(httpRequestAudit);

            return httpRequestAudit;
        }

        //  Dame una manera de acceder para un recurso Blob, por ejemplo, dada su URL absoluta
        public async Task<string> GetSasUrl(string container, string blob)
        {
            string url = await GetPersistenceManager((AppUtilities.PersistenceTechnologies.AZURE_BLOB_STORAGE)).GetAuditDao()
                .GetSasUrl(container, blob);

            return url;
        }
    }
}
