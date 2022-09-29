using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.Api;
using Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.BlobDtos;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

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

        public async Task<string> GetSasUrl(string containerName, string blob)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=ribesstorageaccount;AccountKey=8L3wQW9VMMr7cerg1w3Y7v5fnRo5rID0bYFuLK8WcjIucdDJbzltystBz85pyPsK87tXtvtFReop+AStyOe8Jg==;EndpointSuffix=core.windows.net";

            var containerClient = await base.CreateContainerIfNotExistWithCors(containerName, connectionString);
            var blobClient = containerClient.GetBlobClient(blob);

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = blobClient.BlobContainerName,
                BlobName = blobClient.Name,
                Resource = "b", // "b" para el blob y "c" para el container entero
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(10) //    Duración del permiso
            };

            //  Añadir todos los permisos que se requieran separado por barras:
            sasBuilder.SetPermissions(BlobAccountSasPermissions.Read | BlobAccountSasPermissions.List);

            string urlSas = null;
            if (blobClient.CanGenerateSasUri)
            {
                urlSas = blobClient.GenerateSasUri(sasBuilder).AbsoluteUri;
            }

            return urlSas;
        }
    }
}
