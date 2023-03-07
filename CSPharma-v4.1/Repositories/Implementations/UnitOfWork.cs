using CSPharma_v4._1.Repositories.Interfaces;

namespace CSPharma_v4._1.Repositories.Implementations
{
    // This class represents the Unit of Work design pattern used to group related repository instances together and ensure they share the same database context instance
    public class UnitOfWork : IUnitOfWork
    {
        // The User repository instance
        public IUserRepository User { get; }

        // The Role repository instance
        public IRoleRepository Role { get; }

        // The constructor receives instances of IUserRepository and IRoleRepository and assigns them to their respective properties
        public UnitOfWork(IUserRepository user, IRoleRepository role) 
        {
            User = user;
            Role = role;
        }
    }
}
