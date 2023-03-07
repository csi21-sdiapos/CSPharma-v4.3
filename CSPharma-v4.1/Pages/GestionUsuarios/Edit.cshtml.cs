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
        // Dependency injection for the Unit of Work and SignInManager.
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Constructor that receives the dependencies through dependency injection.
        public EditModel(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        // Property that will receive the data sent from the Razor Page.
        [BindProperty]
        public EditUserViewModel UserModel { get; set; } = default!;


        // This method is executed when the Razor Page is loaded with a GET request.
        public async Task<IActionResult> OnGetAsync(string id)
        {
            // Get the user with the specified id.
            var user = _unitOfWork.User.GetUser(id);
            // Get all the available roles.
            var roles = _unitOfWork.Role.GetRoles();

            // Get all the roles the user belongs to.
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            /*
                var roleItems = new List<SelectListItem>();

                foreach (var role in roles) 
                {
                    var hasRole = userRoles.Any(ur => ur.Contains(role.Name));

                    roleItems.Add(new SelectListItem(role.Name, role.Id, hasRole));
                }
            */

            // Create a list of SelectListItem to be used to render the available roles in a select element in the Razor Page.
            // (misma función que el var roleItems anterior, pero refactorizado para más legibilidad)
            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name))
                )
            ).ToList();

            // Create an instance of the ViewModel to be used in the Razor Page.
            var userModel = new EditUserViewModel
            {
                User = user,
                Roles = roleItems
            };

            // Assign the ViewModel instance to the property that will receive the data sent from the Razor Page.
            UserModel = userModel;

            // Return the Razor Page.
            return Page();
        }


        // This method is executed when the Razor Page is submitted with a POST request.
        public async Task<IActionResult> OnPostAsync(EditUserViewModel UserModel)
        {
            // Get the user with the specified id.
            var user = _unitOfWork.User.GetUser(UserModel.User.Id);

            // If the user is not found, return a 404 Not Found response.
            if (user == null)
            {
                return NotFound();
            }

            // Get all the roles the user belongs to.
            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            // Loop through the roles in ViewModel
            // Check if the Role is Assigned In DB
            // If Assigned -> Do Nothing
            // If Not Assigned -> Add Role
            // note if we are assigning and de-assigning probably tens or hundreds rows then it will have a significant database delay and for this reason we are going to use lists and we will assign and de-assign all rows at once at the end

            // Create a list of roles to be added to the user.
            var rolesToAdd = new List<string>();
            // Create a list of roles to be removed from the user.
            var rolesToRemove = new List<string>();

            // Iterate over all the roles in the ViewModel to determine which ones need to be added or removed from the user.
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

            // Add the roles that need to be added to the user.
            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }
            // Remove the roles that need to be removed from the user.
            if (rolesToRemove.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            // Update the user's properties with the values submitted from the Razor Page.
            user.UserName = UserModel.User.UserName;
            user.Email= UserModel.User.Email;
            user.PhoneNumber = UserModel.User.PhoneNumber;

            // Update the user
            _unitOfWork.User.UpdateUser(user);

            // Return the Razor Page Index.
            return RedirectToPage("./Index");
        }
    }
}