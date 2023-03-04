using CSPharma_v4._1.Areas.Identity.Data;
using CSPharma_v4._1.Models;
using CSPharma_v4._1.Pages.Roles;
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
        */

            // misma función que el var roleItems anterior, pero refactorizado para más legibilidad
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



        public async Task<IActionResult> OnPostAsync(EditUserViewModel UserModel)
        {
            var user = _unitOfWork.User.GetUser(UserModel.User.Id);

            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            // Loop through the roles in ViewModel
            // Check if the Role is Assigned In DB
            // If Assigned -> Do Nothing
            // If Not Assigned -> Add Role
            // note if we are assigning and de-assigning probably tens or hundreds rows then it will have a significant database delay and for this reason we are going to use lists and we will assign and de-assign all rows at once at the end

            var rolesToAdd = new List<string>();
            var rolesToRemove = new List<string>();

            foreach (var role in UserModel.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);

                if (role.Selected)
                {
                    if (assignedInDb == null) 
                    {
                        // await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        // await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Text);
                        rolesToRemove.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }
            if (rolesToRemove.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            user.UserName = UserModel.User.UserName;
            user.Email= UserModel.User.Email;
            user.PhoneNumber = UserModel.User.PhoneNumber;

            _unitOfWork.User.UpdateUser(user);

            return RedirectToPage("./Index");
        }
    }
}