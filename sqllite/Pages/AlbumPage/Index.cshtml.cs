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
    public class IndexModel : PageModel
    {
        private readonly sqllite.Data.ChinookContext _context;

        public IndexModel(sqllite.Data.ChinookContext context)
        {
            _context = context;
        }

        public IList<Album> Album { get;set; }

        public async Task OnGetAsync()
        {
            Album = await _context.Albums
                .Include(a => a.Artist).ToListAsync();
        }
    }
}
