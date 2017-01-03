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
    public class asset_typeController : Controller
    {
        private readonly WttechportalDbContext _context;

        public asset_typeController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: asset_type
        public async Task<IActionResult> Index()
        {
            return View(await _context.asset_type.ToListAsync());
        }

        // GET: asset_type/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_type = await _context.asset_type.SingleOrDefaultAsync(m => m.id == id);
            if (asset_type == null)
            {
                return NotFound();
            }

            return View(asset_type);
        }

        // GET: asset_type/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: asset_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,assettype")] asset_type asset_type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset_type);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(asset_type);
        }

        // GET: asset_type/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_type = await _context.asset_type.SingleOrDefaultAsync(m => m.id == id);
            if (asset_type == null)
            {
                return NotFound();
            }
            return View(asset_type);
        }

        // POST: asset_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,assettype")] asset_type asset_type)
        {
            if (id != asset_type.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset_type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!asset_typeExists(asset_type.id))
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
            return View(asset_type);
        }

        // GET: asset_type/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_type = await _context.asset_type.SingleOrDefaultAsync(m => m.id == id);
            if (asset_type == null)
            {
                return NotFound();
            }

            return View(asset_type);
        }

        // POST: asset_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset_type = await _context.asset_type.SingleOrDefaultAsync(m => m.id == id);
            _context.asset_type.Remove(asset_type);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool asset_typeExists(int id)
        {
            return _context.asset_type.Any(e => e.id == id);
        }
    }
}
