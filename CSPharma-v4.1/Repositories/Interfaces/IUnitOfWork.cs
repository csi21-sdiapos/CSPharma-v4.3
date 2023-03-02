namespace CSPharma_v4._1.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }

        IRoleRepository Role { get; }
    }
}
