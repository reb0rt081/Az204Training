using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Api;

namespace Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.Daos
{
    public class AuditDao : IAuditDao
    {
        public Task<HttpRequestAudit> SaveHttpRequestAudit(HttpRequestAudit httpRequestAudit)
        {
            throw new NotImplementedException();
        }
    }
}
