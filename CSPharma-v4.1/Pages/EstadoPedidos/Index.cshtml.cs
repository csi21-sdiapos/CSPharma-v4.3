using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CSPharma_v4._1_DAL.DataContexts;
using CSPharma_v4._1_DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CSPharma_v4._1.Pages.EstadoPedidos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        // A reference to the database context used to query the data.
        private readonly CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext _context;

        // Constructor that injects the database context.
        public IndexModel(CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext context)
        {
            _context = context;
        }

        // A list of TdcTchEstadoPedidos objects to display on the page.
        public IList<TdcTchEstadoPedidos> TdcTchEstadoPedidos { get;set; } = default!;

        // Method that is executed when the page is loaded.
        public async Task OnGetAsync()
        {
            // If the TdcTchEstadoPedidos table in the database is not null
            if (_context.TdcTchEstadoPedidos != null)
            {
                // Load the TdcTchEstadoPedidos objects from the database with related entities using eager loading
                TdcTchEstadoPedidos = await _context.TdcTchEstadoPedidos
                .Include(t => t.CodEstadoDevolucionNavigation)
                .Include(t => t.CodEstadoEnvioNavigation)
                .Include(t => t.CodEstadoPagoNavigation)
                .Include(t => t.CodLineaNavigation).ToListAsync();
            }
        }
    }
}
