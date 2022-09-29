using Az204.Model.EntitiesLayer.Entities;

namespace Az204.Model.PersistenceLayer.Api
{
    public interface IAuditDao
    {
        Task<HttpRequestAudit> SaveHttpRequestAudit(HttpRequestAudit httpRequestAudit);
    }
}
