
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.Daos
{
    public abstract class BaseDao
    {
        //  Crea un Blob storage (container) en caso de que no exista todavía
        protected async Task<BlobContainerClient> CreateContainerIfNotExistWithCors(string containerName, string storageConnectionString)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
            //  Cuando se va a generar un contenedor, el cliente debe consumir un recurso web hospedado en tu mismo dominio.
            BlobContainerClient blobContainerClient;
            //  Vamos a acceder a las propiedades Blob del cliente directamente modificando por código configuraciones remotas que en JavaScript generarían problemas con el Cors para visualizar y ejecutar contenido
            BlobServiceProperties blobServiceProperties = blobServiceClient.GetProperties().Value;
            if (blobServiceProperties.Cors.Count == 0)
            {
                //  Habilitamos el Cors para ese container
                BlobCorsRule blobCorsRule = new BlobCorsRule()
                {
                    //  Aceptamos que desde cualquier dominio/subdominio podamos acceder al Blob
                    AllowedOrigins = "*",
                    //  Todos los verbos HTTP para ser 
                    AllowedMethods = "GET,POST,DELETE,OPTIONS,MERGE,PUT",
                    AllowedHeaders = "*",
                    ExposedHeaders = "*",
                    MaxAgeInSeconds = 0
                };
                blobServiceProperties.Cors.Add(blobCorsRule);
                blobServiceClient.SetProperties(blobServiceProperties);
            }

            blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync();

            return blobContainerClient;
        }
    }
}
