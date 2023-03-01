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

        public DeleteModel(LoginRegisterContext loginRegisterContext)
        {
            _loginRegisterContext = loginRegisterContext;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _loginRegisterContext.ApplicationUserSet == null)
            {
                return NotFound();
            }

            var applicationUser = await _loginRegisterContext.ApplicationUserSet.FirstOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }
            else
            {
                ApplicationUser = applicationUser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _loginRegisterContext.ApplicationUserSet == null)
            {
                return NotFound();
            }
            var applicationUser = await _loginRegisterContext.ApplicationUserSet.FindAsync(id);

            if (applicationUser != null)
            {
                ApplicationUser = applicationUser;
                _loginRegisterContext.ApplicationUserSet.Remove(ApplicationUser);
                await _loginRegisterContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
