using CSPharma_v4._1.Repositories.Interfaces;

namespace CSPharma_v4._1.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; }

        public IRoleRepository Role { get; }

        public UnitOfWork(IUserRepository user, IRoleRepository role) 
        {
            User = user;
            Role = role;
        }
    }
}
