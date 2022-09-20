using Az204.Model.EntitiesLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Az204.Api.Controllers
{
    //  La ruta por defecto será nombre/Login
    [Route("[controller]")]
    [ApiController]
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
            return Ok(logins);
        }

        [Route("Save")]
        [HttpPost]
        public async Task<IActionResult> SaveLoginAsync([FromBody] Login login)
        {
            logins.Add(login);

            return Ok(login);
        }
    }
}
