
using Microsoft.Extensions.Primitives;

namespace Az204.Model.PersistenceLayer.Impl.AzureBlobStorage.BlobDtos
{
    public class HttpRequestAuditBlobDto
    {
        public IDictionary<string, StringValues> HttpHeaders { get; set; }

        public object HttpBody { get; set; }
    }
}
