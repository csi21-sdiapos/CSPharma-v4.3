using CSPharma_v4._1.Areas.Identity.Data;

namespace CSPharma_v4._1.Repositories.Interfaces
{
    /**
     * IUserRepository Interface
     * This interface defines the methods for a user repository.
    */
    public interface IUserRepository
    {
        /**
        * GetUsers method
        *
        * This method retrieves all users from the repository.
        *
        * @return An ICollection of ApplicationUser objects.
        */
        ICollection<ApplicationUser> GetUsers();

        /**
         * GetUser method
         * 
         * This method retrieves a single user from the repository.
         * 
         * @param id The ID of the user to retrieve.
         * 
         * @return An ApplicationUser object.
         */
        ApplicationUser GetUser(string id);

        /**
         * UpdateUser method
         * 
         * This method updates a user in the repository.
         * 
         * @param user The ApplicationUser object to update.
         * 
         * @return The updated ApplicationUser object.
         */
        ApplicationUser UpdateUser(ApplicationUser user);
    }
}
