namespace CSPharma_v4._1.Repositories.Interfaces
{
    // This interface represents a Unit of Work, which is responsible for coordinating transactions
    // and database operations across multiple repositories.
    public interface IUnitOfWork
    {
        // Property to access the User repository.
        IUserRepository User { get; }

        // Property to access the Role repository.
        IRoleRepository Role { get; }
    }
}
