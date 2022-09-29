using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.ServiceLayer.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Az204.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private static List<Login> logins;

        static BlobController()
        {
            logins = new List<Login>{new Login { Name = "l1", Password = "123456" }};
        }

        [Route("GenerateSas/{containerName}/{blobName}")]
        [HttpGet]
        // ContainerName es el nombre del contenedor blob y BlobName el nombre del archivo o recurso dentro del container
        public async Task<IActionResult> Get(string containerName, string blobName)

        {
            var serviceManager = new ServiceManager();
            
            string urlSas = await serviceManager.GetAuditService().GetSasUrl(containerName, blobName);

            return Ok(urlSas);

        }

    }
}
