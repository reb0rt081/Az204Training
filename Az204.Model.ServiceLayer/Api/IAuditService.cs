using Az204.Model.EntitiesLayer.Entities;

namespace Az204.Model.ServiceLayer.Api
{
    public interface IAuditService
    {
        Task<HttpRequestAudit> SaveHttpAuditRequest(HttpRequestAudit httpRequestAudit);
    }
}
