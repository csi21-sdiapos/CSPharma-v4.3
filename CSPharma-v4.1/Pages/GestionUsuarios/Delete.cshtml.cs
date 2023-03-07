using CSPharma_v4._1.Areas.Identity.Data;
using CSPharma_v4._1_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CSPharma_v4._1.Pages.GestionUsuarios
{
    public class DeleteModel : PageModel
    {
        private readonly LoginRegisterContext _loginRegisterContext;

        // Constructor that initializes the _loginRegisterContext object
        public DeleteModel(LoginRegisterContext loginRegisterContext)
        {
            _loginRegisterContext = loginRegisterContext;
        }

        // Model binding property for ApplicationUser entity
        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }


        // HTTP GET method for retrieving ApplicationUser data and displaying the Delete page
        public async Task<IActionResult> OnGetAsync(string id)
        {
            // If id parameter is null or ApplicationUser set is empty, return NotFound response
            if (id == null || _loginRegisterContext.ApplicationUserSet == null)
            {
                return NotFound();
            }

            // Retrieve ApplicationUser object with matching id from ApplicationUser set
            var applicationUser = await _loginRegisterContext.ApplicationUserSet.FirstOrDefaultAsync(m => m.Id == id);

            // If no matching ApplicationUser object was found, return NotFound response
            if (applicationUser == null)
            {
                return NotFound();
            }
            else // Otherwise, set ApplicationUser property to retrieved ApplicationUser object and display the Delete page
            {
                ApplicationUser = applicationUser;
            }
            return Page();
        }


        // HTTP POST method for deleting ApplicationUser entity and redirecting to the Index page
        public async Task<IActionResult> OnPostAsync(string id)
        {
            // If id parameter is null or ApplicationUser set is empty, return NotFound response
            if (id == null || _loginRegisterContext.ApplicationUserSet == null)
            {
                return NotFound();
            }

            // Find ApplicationUser object with matching id from ApplicationUser set
            var applicationUser = await _loginRegisterContext.ApplicationUserSet.FindAsync(id);

            // If matching ApplicationUser object was found, remove it from the set and save changes to the database
            if (applicationUser != null)
            {
                ApplicationUser = applicationUser;
                _loginRegisterContext.ApplicationUserSet.Remove(ApplicationUser);
                await _loginRegisterContext.SaveChangesAsync();
            }

            // Redirect to the Index page
            return RedirectToPage("./Index");
        }
    }
}
