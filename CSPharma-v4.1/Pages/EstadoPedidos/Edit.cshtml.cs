using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSPharma_v4._1_DAL.DataContexts;
using CSPharma_v4._1_DAL.Models;

namespace CSPharma_v4._1.Pages.EstadoPedidos
{
    public class EditModel : PageModel
    {
        private readonly CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext _context;

        // Constructor that initializes the context
        public EditModel(CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext context)
        {
            _context = context;
        }

        // Bind property for the TdcTchEstadoPedidos object
        [BindProperty]
        public TdcTchEstadoPedidos TdcTchEstadoPedidos { get; set; } = default!;


        // Handler for the HTTP GET request
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // If the id is not provided or the TdcTchEstadoPedidos is null, return NotFound
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            // Get the TdcTchEstadoPedidos object with the given id
            var tdctchestadopedidos =  await _context.TdcTchEstadoPedidos.FirstOrDefaultAsync(m => m.Id == id);
            // If the object is not found, return NotFound
            if (tdctchestadopedidos == null)
            {
                return NotFound();
            }

            // Assign the object to the bind property
            TdcTchEstadoPedidos = tdctchestadopedidos;
            // Create select lists for the related entities and add them to the ViewData dictionary
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion");
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio");
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago");
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea");

            // Return the page
            return Page();
        }


        // Handler for the HTTP POST request
        public async Task<IActionResult> OnPostAsync()
        {
            // If the model state is not valid, return the page
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Attach the object to the context and set its state to Modified
            _context.Attach(TdcTchEstadoPedidos).State = EntityState.Modified;

            try
            {
                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the object does not exist, return NotFound
                if (!TdcTchEstadoPedidosExists(TdcTchEstadoPedidos.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Redirect to the Index page
            return RedirectToPage("./Index");
        }


        // Check if a TdcTchEstadoPedidos object with the given id exists in the database
        private bool TdcTchEstadoPedidosExists(int id)
        {
            return _context.TdcTchEstadoPedidos.Any(e => e.Id == id);
        }
    }
}
