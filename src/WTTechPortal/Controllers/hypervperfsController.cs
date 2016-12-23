using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using WTTechPortal.Models;

namespace WTTechPortal.Controllers
{
    public class hypervperfsController : Controller
    {
        private readonly WttechportalDbContext _context;

        public hypervperfsController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: hypervperfs
        public async Task<IActionResult> Index()
        {
            return View(await _context.hypervperf.ToListAsync());
        }

        // GET: hypervperfs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hypervperf = await _context.hypervperf.SingleOrDefaultAsync(m => m.id == id);
            if (hypervperf == null)
            {
                return NotFound();
            }

            return View(hypervperf);
        }


        // POST: hypervperfs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        private bool hypervperfExists(int id)
        {
            return _context.hypervperf.Any(e => e.id == id);
        }
    }
}
