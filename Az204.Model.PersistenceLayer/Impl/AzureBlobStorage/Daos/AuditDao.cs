using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.BlobDtos;
using Azure.Storage.Blobs;

namespace Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.Daos
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

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=ribesstorageaccount;AccountKey=8L3wQW9VMMr7cerg1w3Y7v5fnRo5rID0bYFuLK8WcjIucdDJbzltystBz85pyPsK87tXtvtFReop+AStyOe8Jg==;EndpointSuffix=core.windows.net";


            BlobContainerClient containerClient = await base.CreateContainerIfNotExistWithCors("audits", connectionString);

            //  Nombre del archivo o del repositorio donde guardaremos el blob
            BlobClient? blobClient = containerClient.GetBlobClient($"{httpRequestAudit.Id}.json");

            //  Serializando contenido del blob
            string json = System.Text.Json.JsonSerializer.Serialize(blobDto);

            using (Stream ms = new MemoryStream())
            {
                //  Cargamos el contenido JSON como string en el StreamWriter
                StreamWriter writer = new StreamWriter(ms);
                writer.Write(json);
                writer.Flush();
                ms.Position = 0;

                await blobClient.UploadAsync(ms, true);
            }

            //  Guardamos el link o direccion URL del contenido Blob para almacenar más tarde en Table Storage
            httpRequestAudit.EntityBlobUrl = blobClient.Uri.AbsoluteUri;

            return httpRequestAudit;
        }
    }
}
