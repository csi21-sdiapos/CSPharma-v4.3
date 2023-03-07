using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CSPharma_v4._1_DAL.DataContexts;
using CSPharma_v4._1_DAL.Models;

namespace CSPharma_v4._1.Pages.EstadoPedidos
{
    public class CreateModel : PageModel
    {
        private readonly CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext _context;

        public CreateModel(CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext context)
        {
            _context = context;
        }


        // Returns the page for creating a new TdcTchEstadoPedidos entity
        public IActionResult OnGet()
        {
            // Populates the select lists for related entities
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion");
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio");
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago");
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea");
            
            return Page();
        }

        [BindProperty]
        public TdcTchEstadoPedidos TdcTchEstadoPedidos { get; set; }


        // Saves the new entity to the database
        public async Task<IActionResult> OnPostAsync()
        {
            // Checks if the model state is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Adds the new entity to the data context and saves changes
            _context.TdcTchEstadoPedidos.Add(TdcTchEstadoPedidos);
            await _context.SaveChangesAsync();

            // Redirects to the index page for the entity
            return RedirectToPage("./Index");
        }
    }
}
