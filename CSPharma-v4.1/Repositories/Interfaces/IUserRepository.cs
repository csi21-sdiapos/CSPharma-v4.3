using CSPharma_v4._1.Areas.Identity.Data;

namespace CSPharma_v4._1.Repositories.Interfaces
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUser(string id);
    }
}
