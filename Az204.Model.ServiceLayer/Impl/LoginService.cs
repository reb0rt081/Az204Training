using Az204.Model.EntitiesLayer.Entities;
using Az204.Model.PersistenceLayer.PersistenceManagers;
using Az204.Model.ServiceLayer.Api;
using Az204.Model.UtilitiesLayer;

namespace Az204.Model.ServiceLayer.Impl
{
    //  Aquí habría unos pasos de validación de lo información y hasheo de la contraseña, comprobar si el login existente.
    //  Aquí iría toda la lógica de negocio que supone guardar un login, el controlador de la capa 4 tiene que ser estúpido.
    //  Normalmente aquí tenemos que usar un TaskScheduler o algun mecanismo que asegure secuencia o uso de hilos ante peticiones.
    public class LoginService : ServiceBase, ILoginService
    {
        public LoginService(List<PersistenceManager> persistenceManagers) : base(persistenceManagers)
        {

        }

        public Task<List<Login>> GetLogins(AppUtilities.PersistenceTechnologies persistenceTechnology)
        {
            return GetPersistenceManager(persistenceTechnology).GetLoginDao().GetLogins();
        }

        public Task<List<Login>> GetLoginByLoginNameAndPassworf(AppUtilities.PersistenceTechnologies persistenceTechnologY, string loginName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Login> Save(Login login, AppUtilities.PersistenceTechnologies persistenceTechnology)
        {
            //  --> Lógica de negocio aquí <--

            return GetPersistenceManager(persistenceTechnology).GetLoginDao().Save(login);
        }
    }
}
