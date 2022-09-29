using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.ServiceLayer.Managers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Az204.Api.CustomFilters
{
    //  Se resuelve que controlador se hace cargo de la ruta y ejecuta el filtro antes de llamarse a la ruta
    public class AzureLogFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var serviceManager = new ServiceManager();
            //  Tareas pre-ejecución de la petición
            HttpRequestAudit audit = new HttpRequestAudit();
            audit.Action = context.ActionDescriptor.DisplayName;
            audit.DateCreated = DateTimeOffset.Now;
            audit.HttpHeaders = context.HttpContext.Request.Headers;
            audit.ClientIp = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            audit.Entity = context.ActionArguments;
            //audit.Browser = context.HttpContext.Request.b;
            audit.UrlRequest = context.HttpContext.Request.Host + context.HttpContext.Request.Path;
            
            await serviceManager.GetAuditService().SaveHttpAuditRequest(audit);

            //  Esperamos a la petición a ser ejecutada
            ActionExecutedContext resultContext = await next();

            //  Tareas post-ejecución de la petición

        }
    }
}
