using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CSPharma_v4._1_DAL.DataContexts;
using CSPharma_v4._1_DAL.Models;

namespace CSPharma_v4._1.Pages.EstadoPedidos
{
    public class DeleteModel : PageModel
    {
        private readonly CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext _context;

        public DeleteModel(CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext context)
        {
            _context = context;
        }

        // The entity to be deleted. This property is automatically populated from the data submitted in the form.
        [BindProperty]
        public TdcTchEstadoPedidos TdcTchEstadoPedidos { get; set; }


        // HTTP GET method for the page. It retrieves the entity to be deleted and displays its details in the page.
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // If no ID was provided or the database is empty, return a "not found" response.
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            // Find the entity to be deleted in the database.
            var tdctchestadopedidos = await _context.TdcTchEstadoPedidos.FirstOrDefaultAsync(m => m.Id == id);

            // If the entity was not found, return a "not found" response.
            if (tdctchestadopedidos == null)
            {
                return NotFound();
            }
            else 
            {
                // If the entity was found, populate the TdcTchEstadoPedidos property with its data.
                TdcTchEstadoPedidos = tdctchestadopedidos;
            }

            // Render the page.
            return Page();
        }


        // HTTP POST method for the page. It deletes the entity from the database and redirects the user to the index page.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // If no ID was provided or the database is empty, return a "not found" response.
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            // Find the entity to be deleted in the database.
            var tdctchestadopedidos = await _context.TdcTchEstadoPedidos.FindAsync(id);

            // If the entity was found, delete it and save the changes to the database.
            if (tdctchestadopedidos != null)
            {
                TdcTchEstadoPedidos = tdctchestadopedidos;
                _context.TdcTchEstadoPedidos.Remove(TdcTchEstadoPedidos);
                await _context.SaveChangesAsync();
            }

            // Redirect the user to the index page.
            return RedirectToPage("./Index");
        }
    }
}
