using Newtonsoft.Json;

namespace Az204.Model.PersistenceLayer.Impl.AzureCosmosDb.TableEntities
{
    public class LoginCosmosTableEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }

        public string Password { get; set; }
    }
}
