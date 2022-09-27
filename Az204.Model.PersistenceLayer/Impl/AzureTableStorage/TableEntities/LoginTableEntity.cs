using Az204.Model.EntitiesLayer.Entities;

using Azure;
using Azure.Data.Tables;

namespace Az204.Model.PersistenceLayer.Impl.AzureTableStorage.TableEntities
{
    //  Esta entidad solamente tiene sentido aquí si no queremos referencias Azure Table Storage en otros proyectos.
    public class LoginTableEntity : ITableEntity
    {
        //  Siempre hay que tener un constructor sin parámetros
        public LoginTableEntity()
        {
            ETag = ETag.All;
        }
        public LoginTableEntity(Login login) : this()
        {
            //  Equivalente a '*' de manera que no nos importa si el ETag ha cambiado, la sobreescribimos.
            PartitionKey = login.Name;
            RowKey = login.Password;
            Id = login.Id.ToString();
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

        //  Nos permite trabajar con la concurrencia de los objetos.
        //  El ETag es un GUID por instancia/operación.
        //  Si bajamos la entidad y la modificamos el ETag no va a coincidir con el nuevo y la inserción no se permite.
        public ETag ETag { get; set; }

        //  Podemos crear nuevas propiedades para persistir en Table Storage
        public string Id { get; set; }
    }
}
