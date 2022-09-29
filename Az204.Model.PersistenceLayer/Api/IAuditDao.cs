using Az204.Model.EntitiesLayer.Entities;

namespace Az204.Model.PersistenceLayer.Api
{
    public interface IAuditDao
    {
        Task<HttpRequestAudit> SaveHttpRequestAudit(HttpRequestAudit httpRequestAudit);

        //  Dame una manera de acceder para un recurso Blob, por ejemplo, dada su información
        Task<string> GetSasUrl(string containerName, string blob);
    }
}
