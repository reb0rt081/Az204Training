
using Microsoft.Extensions.Primitives;

namespace Az204.Model.EntitiesLayer.Entities
{
    public class HttpRequestAudit : EntityBase
    {
        public DateTimeOffset DateCreated { get; set; }

        public string UrlRequest { get; set; }

        public string Action { get; set; }

        public string ClientIp { get; set; }

        public string Browser { get; set; }

        public IDictionary<string, StringValues> HttpHeaders { get; set; }

        public double? ComputeTime { get; set; }

        public object Entity { get; set; }

        public string EntityBlobUrl { get; set; }
    }
}
