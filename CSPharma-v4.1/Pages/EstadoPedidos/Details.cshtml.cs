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
    public class DetailsModel : PageModel
    {
        private readonly CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext _context;

        public DetailsModel(CSPharma_v4._1_DAL.DataContexts.CspharmaInformacionalContext context)
        {
            _context = context;
        }

        // Property to hold the details of a TdcTchEstadoPedidos object
        public TdcTchEstadoPedidos TdcTchEstadoPedidos { get; set; }


        // Method to retrieve the details of a TdcTchEstadoPedidos object with the given id
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Check if id or _context.TdcTchEstadoPedidos is null, if yes return NotFound page
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            // Find the TdcTchEstadoPedidos object with the given id
            var tdctchestadopedidos = await _context.TdcTchEstadoPedidos.FirstOrDefaultAsync(m => m.Id == id);

            // If the object is not found, return NotFound page
            if (tdctchestadopedidos == null)
            {
                return NotFound();
            }
            else // Otherwise, set the TdcTchEstadoPedidos property to the found object 
            {
                TdcTchEstadoPedidos = tdctchestadopedidos;
            }

            // Return the details page with the TdcTchEstadoPedidos object
            return Page();
        }
    }
}
