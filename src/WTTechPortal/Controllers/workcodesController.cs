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
    public class workcodesController : Controller
    {
        private readonly WttechportalDbContext _context;

        public workcodesController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: workcodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.workcodes.ToListAsync());
        }

        // GET: workcodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workcodes = await _context.workcodes.SingleOrDefaultAsync(m => m.id == id);
            if (workcodes == null)
            {
                return NotFound();
            }

            return View(workcodes);
        }

        // GET: workcodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: workcodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,workcode")] workcodes workcodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workcodes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workcodes);
        }

        // GET: workcodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workcodes = await _context.workcodes.SingleOrDefaultAsync(m => m.id == id);
            if (workcodes == null)
            {
                return NotFound();
            }
            return View(workcodes);
        }

        // POST: workcodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,workcode")] workcodes workcodes)
        {
            if (id != workcodes.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workcodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!workcodesExists(workcodes.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(workcodes);
        }

        // GET: workcodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workcodes = await _context.workcodes.SingleOrDefaultAsync(m => m.id == id);
            if (workcodes == null)
            {
                return NotFound();
            }

            return View(workcodes);
        }

        // POST: workcodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workcodes = await _context.workcodes.SingleOrDefaultAsync(m => m.id == id);
            _context.workcodes.Remove(workcodes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool workcodesExists(int id)
        {
            return _context.workcodes.Any(e => e.id == id);
        }
    }
}
