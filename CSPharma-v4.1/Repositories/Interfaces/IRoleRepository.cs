using CSPharma_v4._1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CSPharma_v4._1.Repositories.Interfaces
{
    // This is an interface for the Role Repository which defines the contract for
    // methods that will be implemented in classes that use this interface.
    public interface IRoleRepository
    {
        // GetRoles: returns a collection of IdentityRole objects.
        ICollection<IdentityRole> GetRoles();
    }
}
