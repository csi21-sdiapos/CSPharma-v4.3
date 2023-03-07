using CSPharma_v4._1.Areas.Identity.Data;
using CSPharma_v4._1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CSPharma_v4._1.Repositories.Implementations
{
    /**
     * The UserRepository class implements the IUserRepository interface and defines methods to interact with user data in the database.
    */
    public class UserRepository : IUserRepository
    {
        private readonly LoginRegisterContext _loginRegisterContext;

        /**
        * Constructor that takes an instance of LoginRegisterContext as a parameter.
        * @param loginRegisterContext The context to be used to access the user data in the database.
        */
        public UserRepository(LoginRegisterContext loginRegisterContext)
        {
            _loginRegisterContext = loginRegisterContext;
        }

        /**
        * Retrieves a collection of all users from the database.
        * @return A collection of all users.
        */
        public ICollection<ApplicationUser> GetUsers()
        {
            return _loginRegisterContext.Users.ToList();
        }

        /**
        * Retrieves a user by their ID from the database.
        * @param id The ID of the user to retrieve.
        * @return The ApplicationUser object of the user with the specified ID.
        */
        public ApplicationUser GetUser(string id)
        {
            return _loginRegisterContext.Users.FirstOrDefault(u => u.Id == id);
        }

        /**
        * Updates the information of a user in the database.
        * @param user The ApplicationUser object containing the updated information.
        * @return The ApplicationUser object of the updated user.
        */
        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            _loginRegisterContext.Update(user);
            _loginRegisterContext.SaveChanges();

            return user;
        }
    }
}
