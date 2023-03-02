using CSPharma_v4._1.Areas.Identity.Data;
using CSPharma_v4._1.Models;
using CSPharma_v4._1.Repositories.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EditModel(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        [BindProperty]
        public EditUserViewModel UserModel { get; set; } = default!;

        //public IActionResult OnGet(string id)
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
        /*
            var roleItems = new List<SelectListItem>();

            foreach (var role in roles)
            {
                var hasRole = userRoles.Any(ur => ur.Contains(role.Name));

                roleItems.Add(new SelectListItem(role.Name, role.Id, hasRole));
            }

            // todo este bloque de código se puede simplicar en la siguiente variable
        */
            var roleItems = roles.Select(role => 
                new SelectListItem(
                    role.Name, 
                    role.Id, 
                    userRoles.Any(ur => ur.Contains(role.Name))
                )
            ).ToList();

            var userModel = new EditUserViewModel 
            { 
                User = user,
                Roles = roleItems
            };
            
            UserModel = userModel;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            return RedirectToPage("./Index");
        }
    }
}
