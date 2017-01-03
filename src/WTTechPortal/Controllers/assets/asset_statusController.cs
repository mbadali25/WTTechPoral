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
    public class asset_statusController : Controller
    {
        private readonly WttechportalDbContext _context;

        public asset_statusController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: asset_status
        public async Task<IActionResult> Index()
        {
            return View(await _context.asset_status.ToListAsync());
        }

        // GET: asset_status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_status = await _context.asset_status.SingleOrDefaultAsync(m => m.id == id);
            if (asset_status == null)
            {
                return NotFound();
            }

            return View(asset_status);
        }

        // GET: asset_status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: asset_status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,assetstatus")] asset_status asset_status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset_status);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(asset_status);
        }

        // GET: asset_status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_status = await _context.asset_status.SingleOrDefaultAsync(m => m.id == id);
            if (asset_status == null)
            {
                return NotFound();
            }
            return View(asset_status);
        }

        // POST: asset_status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,assetstatus")] asset_status asset_status)
        {
            if (id != asset_status.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset_status);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!asset_statusExists(asset_status.id))
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
            return View(asset_status);
        }

        // GET: asset_status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_status = await _context.asset_status.SingleOrDefaultAsync(m => m.id == id);
            if (asset_status == null)
            {
                return NotFound();
            }

            return View(asset_status);
        }

        // POST: asset_status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset_status = await _context.asset_status.SingleOrDefaultAsync(m => m.id == id);
            _context.asset_status.Remove(asset_status);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool asset_statusExists(int id)
        {
            return _context.asset_status.Any(e => e.id == id);
        }
    }
}
