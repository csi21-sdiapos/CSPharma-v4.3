using CSPharma_v4._1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CSPharma_v4._1.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
