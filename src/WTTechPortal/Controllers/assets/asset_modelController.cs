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
    public class asset_modelController : Controller
    {
        private readonly WttechportalDbContext _context;

        public asset_modelController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: asset_model
        public async Task<IActionResult> Index()
        {
            return View(await _context.asset_model.ToListAsync());
        }

        // GET: asset_model/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_model = await _context.asset_model.SingleOrDefaultAsync(m => m.id == id);
            if (asset_model == null)
            {
                return NotFound();
            }

            return View(asset_model);
        }

        // GET: asset_model/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: asset_model/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,model")] asset_model asset_model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset_model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(asset_model);
        }

        // GET: asset_model/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_model = await _context.asset_model.SingleOrDefaultAsync(m => m.id == id);
            if (asset_model == null)
            {
                return NotFound();
            }
            return View(asset_model);
        }

        // POST: asset_model/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,model")] asset_model asset_model)
        {
            if (id != asset_model.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset_model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!asset_modelExists(asset_model.id))
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
            return View(asset_model);
        }

        // GET: asset_model/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_model = await _context.asset_model.SingleOrDefaultAsync(m => m.id == id);
            if (asset_model == null)
            {
                return NotFound();
            }

            return View(asset_model);
        }

        // POST: asset_model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset_model = await _context.asset_model.SingleOrDefaultAsync(m => m.id == id);
            _context.asset_model.Remove(asset_model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool asset_modelExists(int id)
        {
            return _context.asset_model.Any(e => e.id == id);
        }
    }
}
