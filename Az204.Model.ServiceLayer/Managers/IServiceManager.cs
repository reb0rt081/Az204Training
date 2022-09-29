using Az204.Model.ServiceLayer.Api;

namespace Az204.Model.ServiceLayer.Managers
{
    //  Este servicio debe identificar el servicio que se encarga de gestionar la petición.
    //  Este objeto será la factoría de servicios que se ocupa de la petición, es decir, el experto.
    public interface IServiceManager
    {
        //  A medida que los servicios aumentan será necesario mejorar esta factoría
        ILoginService GetLoginService();

        IAuditService GetAuditService();
    }
}
