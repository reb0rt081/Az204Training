using System.Globalization;
using Az204.Model.EntitiesLayer.Entities;
using Azure;
using Azure.Data.Tables;

namespace Az204.Model.PersistenceLayer.Impl.AzureTableStorage.TableEntities
{
    public class HttpRequestAuditTableEntity : ITableEntity
    {
        //  Siempre hay que tener un constructor sin parámetros
        public HttpRequestAuditTableEntity()
        {
            //  Equivalente a '*' de manera que no nos importa si el ETag ha cambiado, la sobreescribimos.
            ETag = ETag.All;
        }
        public HttpRequestAuditTableEntity(HttpRequestAudit audit) : this()
        {
            //  Para auditar vamos a generar particiones/tablas cada hora
            PartitionKey = audit.DateCreated.ToString("yyyy-MM-dd-HH", CultureInfo.InvariantCulture);
            RowKey = audit.Id.ToString();
            RequestDate = audit.DateCreated;
            UrlRequest = audit.UrlRequest;
            Action = audit.Action;
            ClientIp = audit.ClientIp;
            ComputeTime = audit.ComputeTime;
            EntityBlobUrl = audit.EntityBlobUrl;
            Browser = audit.Browser;
            
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

        public string Action { get; set; }

        public string Browser { get; set; }

        //  Nos permite trabajar con la concurrencia de los objetos.
        //  El ETag es un GUID por instancia/operación.
        //  Si bajamos la entidad y la modificamos el ETag no va a coincidir con el nuevo y la inserción no se permite.
        public ETag ETag { get; set; }

        public DateTimeOffset? RequestDate { get; set; }

        public string UrlRequest { get; set; }

        public string ClientIp { get; set; }

        public double? ComputeTime { get; set; }

        public string EntityBlobUrl { get; set; }


    }
}
