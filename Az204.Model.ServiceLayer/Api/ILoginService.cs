using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.UtilitiesLayer;

namespace Az204.Model.ServiceLayer.Api
{
    //  Aquí habría unos pasos de validación de lo información y hasheo de la contraseña, comprobar si el login existente.
    //  Aquí iría toda la lógica de negocio que supone guardar un login, el controlador de la capa 4 tiene que ser estúpido.
    //  Normalmente aquí tenemos que usar un TaskScheduler o algun mecanismo que asegure secuencia o uso de hilos ante peticiones.
    public interface ILoginService
    {
        Task<Login> Save(Login login, AppUtilities.PersistenceTechnologies persistenceTechnology);

        Task<List<Login>> GetLogins(AppUtilities.PersistenceTechnologies persistenceTechnology);

        Task<List<Login>> GetLoginByLoginNameAndPassword(AppUtilities.PersistenceTechnologies persistenceTechnologY, string loginName, string password);
    }
}
