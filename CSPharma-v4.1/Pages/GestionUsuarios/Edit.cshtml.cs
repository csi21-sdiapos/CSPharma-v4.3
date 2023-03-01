using CSPharma_v4._1.Areas.Identity.Data;
using CSPharma_v4._1_DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CSPharma_v4._1.Pages.GestionUsuarios
{
    public class EditModel : PageModel
    {
        private readonly LoginRegisterContext _loginRegisterContext;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EditModel(LoginRegisterContext loginRegisterContext, SignInManager<ApplicationUser> signInManager)
        {
            _loginRegisterContext = loginRegisterContext;
            _signInManager = signInManager;
        }

        [BindProperty]
        public ApplicationUser User { get; set; } = default!;

        public IList<SelectListItem> Roles { get; set; }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _loginRegisterContext.ApplicationUserSet == null)
            {
                return NotFound();
            }

            var user = await _loginRegisterContext.ApplicationUserSet.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;

            var roles = GetRoles();
            if (roles == null)
            {
                return NotFound();
            }

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role => 
                new SelectListItem(
                    role.Name, 
                    role.Id, 
                    userRoles.Any(ur => ur.Contains(role.Name))
                )
            ).ToList();

            Roles = roleItems;

            return Page();
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _loginRegisterContext.Roles.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            return RedirectToPage("./Index");
        }

    }
}
