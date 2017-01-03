using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using WTTechPortal.Models.Assests;
using Microsoft.AspNetCore.Authorization;

namespace WTTechPortal.Controllers.assets
{
    [Authorize]
    public class asset_opertaingsystemController : Controller
    {
        private readonly WttechportalDbContext _context;

        public asset_opertaingsystemController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: asset_opertaingsystem
        public async Task<IActionResult> Index()
        {
            return View(await _context.asset_opertaingsystem.ToListAsync());
        }

        // GET: asset_opertaingsystem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_opertaingsystem = await _context.asset_opertaingsystem.SingleOrDefaultAsync(m => m.id == id);
            if (asset_opertaingsystem == null)
            {
                return NotFound();
            }

            return View(asset_opertaingsystem);
        }

        // GET: asset_opertaingsystem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: asset_opertaingsystem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,operatingsystem")] asset_opertaingsystem asset_opertaingsystem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset_opertaingsystem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(asset_opertaingsystem);
        }

        // GET: asset_opertaingsystem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_opertaingsystem = await _context.asset_opertaingsystem.SingleOrDefaultAsync(m => m.id == id);
            if (asset_opertaingsystem == null)
            {
                return NotFound();
            }
            return View(asset_opertaingsystem);
        }

        // POST: asset_opertaingsystem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,operatingsystem")] asset_opertaingsystem asset_opertaingsystem)
        {
            if (id != asset_opertaingsystem.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset_opertaingsystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!asset_opertaingsystemExists(asset_opertaingsystem.id))
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
            return View(asset_opertaingsystem);
        }

        // GET: asset_opertaingsystem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_opertaingsystem = await _context.asset_opertaingsystem.SingleOrDefaultAsync(m => m.id == id);
            if (asset_opertaingsystem == null)
            {
                return NotFound();
            }

            return View(asset_opertaingsystem);
        }

        // POST: asset_opertaingsystem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset_opertaingsystem = await _context.asset_opertaingsystem.SingleOrDefaultAsync(m => m.id == id);
            _context.asset_opertaingsystem.Remove(asset_opertaingsystem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool asset_opertaingsystemExists(int id)
        {
            return _context.asset_opertaingsystem.Any(e => e.id == id);
        }
    }
}
