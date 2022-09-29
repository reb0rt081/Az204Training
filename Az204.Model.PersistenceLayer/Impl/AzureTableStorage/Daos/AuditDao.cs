using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.BlobDtos;
using Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.Daos;
using Azure.Storage.Blobs;

namespace Az204.Model.PersistenceLayer.Impl.AzureTableStorage.Daos
{
    public class AuditDao : BaseDao, IAuditDao
    {
        public async Task<HttpRequestAudit> SaveHttpRequestAudit(HttpRequestAudit httpRequestAudit)
        {
            var blobDto = new HttpRequestAuditBlobDto
            {
                HttpHeaders = httpRequestAudit.HttpHeaders,
                HttpBody = httpRequestAudit.Entity
            };

            string connectionString = "randomConnections";

            BlobContainerClient containerClient = await base.CreateContainerIfNotExistWithCors("audits", connectionString);
            
            //  Nombre del archivo o del repositorio donde guardaremos el blob
            BlobClient? blobClient = containerClient.GetBlobClient($"{httpRequestAudit.Id}.json");

            //  Serializando contenido del blob
            string json = System.Text.Json.JsonSerializer.Serialize(blobDto);

            await blobClient.UploadAsync(json);

            //  Guardamos el link o direccion URL del contenido Blob para almacenar más tarde en Table Storage
            httpRequestAudit.EntityBlobUrl = blobClient.Uri.AbsoluteUri;

            return httpRequestAudit;
        }
    }
}
