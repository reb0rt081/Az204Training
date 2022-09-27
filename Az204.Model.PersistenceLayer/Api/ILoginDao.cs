using Az204.Model.EntitiesLayer.Entities;

namespace Az204.Model.PersistenceLayer.Api
{
    public interface ILoginDao
    {
        Task<Login> Save(Login login);

        Task<List<Login>> GetLogins();

        Task<List<Login>> GetLoginByLoginNameAndPassword(string loginName, string password);
    }
}
