using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sqllite.Data;
using sqllite.Models;

namespace sqllite.Pages.AlbumPage
{
    public class EditModel : PageModel
    {
        private readonly sqllite.Data.ChinookContext _context;

        public EditModel(sqllite.Data.ChinookContext context)
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
           ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(Album.AlbumId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AlbumExists(long id)
        {
            return _context.Albums.Any(e => e.AlbumId == id);
        }
    }
}
