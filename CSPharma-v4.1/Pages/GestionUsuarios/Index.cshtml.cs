using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CSPharma_v4._1.Areas.Identity.Data;
using CSPharma_v4._1_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using CSPharma_v4._1.Repositories.Interfaces;

namespace CSPharma_v4._1.Pages.GestionUsuarios
{
    [Authorize(Roles = "Administrators")]
    public class IndexModel : PageModel
    {
        private readonly LoginRegisterContext _loginRegisterContext;

        // The constructor takes a LoginRegisterContext instance and assigns it to the _loginRegisterContext field
        public IndexModel(LoginRegisterContext loginRegisterContext)
        {
            _loginRegisterContext = loginRegisterContext;
        }

        // A property that represents the list of ApplicationUser objects to display
        // The default! value ensures that the property is not null
        public IList<ApplicationUser> ApplicationUserList { get; set; } = default!;


        // The OnGetAsync method is executed when the page is requested with a GET HTTP verb
        public async Task OnGetAsync()
        {
            // If the ApplicationUserSet property of the LoginRegisterContext is not null,
            // retrieve a list of ApplicationUser objects from the database and assign it to the ApplicationUserList property
            if (_loginRegisterContext.ApplicationUserSet != null)
            {
                ApplicationUserList = await _loginRegisterContext.ApplicationUserSet.ToListAsync();
            }
        }
    }
}
