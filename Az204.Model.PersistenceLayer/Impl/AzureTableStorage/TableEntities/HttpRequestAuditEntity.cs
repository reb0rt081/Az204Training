﻿using Az204.Model.EntitiesLayer.Entities;
using Azure;
using Azure.Data.Tables;

namespace Az204.Model.PersistenceLayer.Impl.AzureTableStorage.TableEntities
{
    public class HttpRequestAuditEntity : ITableEntity
    {
        //  Siempre hay que tener un constructor sin parámetros
        public HttpRequestAuditEntity()
        {
            //  Equivalente a '*' de manera que no nos importa si el ETag ha cambiado, la sobreescribimos.
            ETag = ETag.All;
        }
        public HttpRequestAuditEntity(HttpRequestAudit audit) : this()
        {
            PartitionKey = audit.UrlRequest;
            RowKey = audit.Id.ToString();
            EntityBlobUrl = audit.EntityBlobUrl;
            ClientIp = audit.ClientIp;
            Action = audit.Action;
            Browser = audit.Browser;

        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

        //  Nos permite trabajar con la concurrencia de los objetos.
        //  El ETag es un GUID por instancia/operación.
        //  Si bajamos la entidad y la modificamos el ETag no va a coincidir con el nuevo y la inserción no se permite.
        public ETag ETag { get; set; }

        public string EntityBlobUrl { get; set; }

        public string ClientIp { get; set; }

        public string Action { get; set; }

        public string Browser { get; set; }

    }
}
