using Az204.Api.CustomFilters;
using Az204.Api.DTOs;
using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.ServiceLayer.Managers;
using Az204.Model.UtilitiesLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Az204.Api.Controllers
{
    //  Este atrivbuto hace que nuestro código se ejecute antes de resolver la ruta y el metodo
    [AzureLogFilter]
    //  La ruta por defecto será nombre/Login
    [Route("[controller]")]
    [ApiController]
    //  Se genera una instancia de controller por cada petición que se recibe
    //  Si estamos esperando una llamada a BBDD  y .NET decide eliminar el controller entonces tenemos un problema si la sesión se mantiene abierta en la BBDD.
    //  Es necesario establecer IDisposable para ciertos servicios y controladores para así poder cerrar conexiones.
    public class LoginController : ControllerBase
    {
        //  El ciclo de vida de un controlador es crear/destruir por petición. Por cada petición se va a crear un objeto LoginController nuevo. Esto es completamente stateless.
        //  La hacemos estatica para que todos los controladores compartan esta informacion
        private static IList<Login> logins;

        //  La primera llamada se inicializa la lista únicamente, posteriores peticiones no van a cambiar la lista
        static LoginController()
        {
            logins = new List<Login>
            {
                new Login { Name = "L1", Password = "12345" }
            };
        }

        //  Podemos generar rutas para acceso a los métodos.
        [Route("All")]
        //  Solamente escuchamos peticiones HTTP.
        [HttpGet]
        //  Hacer controladores asíncronos siempre para gestionar las peticiones que nos llegan. Así gestionamos mejor los hilos de las tareas síncronas a las que están esperando: acceso a bases de datos o a otras capas.
        public async Task<IActionResult> GetLoginsAsync()
        {
            ServiceManager serviceManager = new ServiceManager();
            Task<List<Login>> logins = serviceManager.GetLoginService().GetLogins(AppUtilities.PersistenceTechnologies.AZURE_TABLE_STORAGE);

            return Ok(logins);
        }

        [Route("Save")]
        [HttpPost]
        public async Task<IActionResult> SaveLoginAsync([FromBody] Login login)
        {
            ServiceManager serviceManager = new ServiceManager();
            Task<Login> savedLogin = serviceManager.GetLoginService().Save(login, AppUtilities.PersistenceTechnologies.AZURE_TABLE_STORAGE);

            return Ok(savedLogin);
        }

        //  La ruta incluye los parámetros que necesitamos para el método.
        //  Esto no es seguro para nada ya que se expone la password. La mejor manera es utilizar un HttpPost para hacerlo seguro. 
        [Route("{loginName}/{password}")]
        [HttpGet]
        
        public async Task<IActionResult> GetLoginByNameAndPassword(string loginName, string password)
        {
            ServiceManager serviceManager = new ServiceManager();
            Task<List<Login>> logins = serviceManager.GetLoginService().GetLoginByLoginNameAndPassword(AppUtilities.PersistenceTechnologies.AZURE_TABLE_STORAGE, loginName, password);

            return Ok(logins);
        }

        //  Safer way to do it
        [Route("GetLoginByNameAndPassword")]
        [HttpPost]
        public async Task<IActionResult> GetLoginByNameAndPassword([FromBody] LoginControllerGetLoginByNameAndPasswordDto loginDto)
        {
            ServiceManager serviceManager = new ServiceManager();
            Task<List<Login>> logins = serviceManager.GetLoginService().GetLoginByLoginNameAndPassword(AppUtilities.PersistenceTechnologies.AZURE_TABLE_STORAGE, loginDto.LoginName, loginDto.Password);

            return Ok(logins);
        }
    }
}
