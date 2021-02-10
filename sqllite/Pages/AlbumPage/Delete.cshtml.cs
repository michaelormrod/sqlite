using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sqllite.Data;
using sqllite.Models;

namespace sqllite.Pages.AlbumPage
{
    public class DeleteModel : PageModel
    {
        private readonly sqllite.Data.ChinookContext _context;

        public DeleteModel(sqllite.Data.ChinookContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Album Album { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album = await _context.Albums
                .Include(a => a.Artist).FirstOrDefaultAsync(m => m.AlbumId == id);

            if (Album == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album = await _context.Albums.FindAsync(id);

            if (Album != null)
            {
                _context.Albums.Remove(Album);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
