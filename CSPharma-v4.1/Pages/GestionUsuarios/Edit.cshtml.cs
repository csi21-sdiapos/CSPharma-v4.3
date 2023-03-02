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