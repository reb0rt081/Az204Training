using Az204.Model.EntitiesLayer.Entities;

namespace Az204.Model.ServiceLayer.Api
{
    public interface IAuditService
    {
        Task<HttpRequestAudit> SaveHttpAuditRequest(HttpRequestAudit httpRequestAudit);

        //  Dame una manera de acceder para un recurso Blob, por ejemplo, dada su información
        Task<string> GetSasUrl(string container, string blob);
    }
}
