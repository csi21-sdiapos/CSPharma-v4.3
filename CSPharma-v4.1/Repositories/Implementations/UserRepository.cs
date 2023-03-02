using CSPharma_v4._1.Areas.Identity.Data;
using CSPharma_v4._1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CSPharma_v4._1.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly LoginRegisterContext _loginRegisterContext;

        public UserRepository(LoginRegisterContext loginRegisterContext)
        {
            _loginRegisterContext = loginRegisterContext;
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            return _loginRegisterContext.Users.ToList();
        }

        public ApplicationUser GetUser(string id)
        {
            return _loginRegisterContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
