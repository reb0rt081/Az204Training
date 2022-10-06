using Az204.Model.EntitiesLayer.Entities;

using Newtonsoft.Json;

namespace Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.TableEntities
{
    public class LoginCosmosTableEntity
    {
        public LoginCosmosTableEntity()
        {
            //  Es recomendable tener un constructor sin parámetros por defecto para evitar problemas
        }

        //  Si se genera un constructor con parámetros, entonces es necesario para que Cosmos funcione un constructor sin parámetros
        public LoginCosmosTableEntity(Login login)
        {
            Id = login.Id.ToString();
            PartitionKey = login.Name;
            Password = login.Password;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        //  Json puede modificar el nombre de la propiedad cuando se serialice por lo que no hace falta que la propiedad se llame igual
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }

        public string Password { get; set; }
    }
}
