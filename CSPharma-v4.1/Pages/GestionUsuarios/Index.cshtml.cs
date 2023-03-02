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

        public IndexModel(LoginRegisterContext loginRegisterContext)
        {
            _loginRegisterContext = loginRegisterContext;
        }

        public IList<ApplicationUser> ApplicationUserList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_loginRegisterContext.ApplicationUserSet != null)
            {
                ApplicationUserList = await _loginRegisterContext.ApplicationUserSet.ToListAsync();
            }
        }
    }
}
