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
    public class DetailsModel : PageModel
    {
        private readonly sqllite.Data.ChinookContext _context;

        public DetailsModel(sqllite.Data.ChinookContext context)
        {
            _context = context;
        }

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
    }
}
