using CSPharma_v4._1.Areas.Identity.Data;
using CSPharma_v4._1.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CSPharma_v4._1.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LoginRegisterContext _loginRegisterContext;

        // Constructor to inject LoginRegisterContext
        public RoleRepository(LoginRegisterContext loginRegisterContext)
        {
            _loginRegisterContext = loginRegisterContext;
        }

        // Returns a collection of all roles in the database
        public ICollection<IdentityRole> GetRoles()
        {
            return _loginRegisterContext.Roles.ToList();
        }
    }
}
