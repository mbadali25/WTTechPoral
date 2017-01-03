using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using WTTechPortal.Models.Inventory;
using WTTechPortal.Models;
using Microsoft.AspNetCore.Authorization;

namespace WTTechPortal.Controllers
{
    [Authorize]
    public class AssetInventoryController : Controller
    {
        private readonly WttechportalDbContext _context;

        public AssetInventoryController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: AssetInventory
        public async Task<IActionResult> Index()
        {
            var wttechportalDbContext = _context.assetinventory.Include(a => a.assettypes).Include(a => a.brands).Include(a => a.operatingsystems).Include(a => a.orginzation).Include(a => a.status);
            return View(await wttechportalDbContext.ToListAsync());
        }

        // GET: AssetInventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetinventory = await _context.assetinventory.SingleOrDefaultAsync(m => m.id == id);
            if (assetinventory == null)
            {
                return NotFound();
            }

            return View(assetinventory);
        }

        // GET: AssetInventory/Create
        public IActionResult Create()
        {

            ViewData["assettype"] = new SelectList(_context.asset_type, "id", "assettype");
            ViewData["brand"] = new SelectList(_context.asset_model, "id", "model");
            ViewData["operatingsystem"] = new SelectList(_context.asset_opertaingsystem, "id", "operatingsystem");
            ViewData["org"] = new SelectList(_context.org_list, "id", "orgname");
            ViewData["ready"] = new SelectList(_context.asset_status, "id", "assetstatus");
            return View();
        }

        // POST: AssetInventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,assetid,assettype,brand,cpucores,cpumodel,cpuspd,hddsize,memory,model,notes,operatingsystem,org,ready")] assetinventory assetinventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetinventory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["assettype"] = new SelectList(_context.asset_type, "id", "assettype", assetinventory.assettype);
            ViewData["brand"] = new SelectList(_context.asset_model, "id", "model", assetinventory.brand);
            ViewData["operatingsystem"] = new SelectList(_context.asset_opertaingsystem, "id", "operatingsystem", assetinventory.operatingsystem);
            ViewData["org"] = new SelectList(_context.org_list, "id", "orgname", assetinventory.org);
            ViewData["ready"] = new SelectList(_context.asset_status, "id", "assetstatus", assetinventory.ready);
            return View(assetinventory);
        }

        // GET: AssetInventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetinventory = await _context.assetinventory.SingleOrDefaultAsync(m => m.id == id);
            if (assetinventory == null)
            {
                return NotFound();
            }
            ViewData["assettype"] = new SelectList(_context.asset_type, "id", "assettype", assetinventory.assettype);
            ViewData["brand"] = new SelectList(_context.asset_model, "id", "model", assetinventory.brand);
            ViewData["operatingsystem"] = new SelectList(_context.asset_opertaingsystem, "id", "operatingsystem", assetinventory.operatingsystem);
            ViewData["org"] = new SelectList(_context.org_list, "id", "orgname", assetinventory.org);
            ViewData["ready"] = new SelectList(_context.asset_status, "id", "assetstatus", assetinventory.ready);
            return View(assetinventory);
        }

        // POST: AssetInventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,assetid,assettype,brand,cpucores,cpumodel,cpuspd,hddsize,memory,model,notes,operatingsystem,org,ready")] assetinventory assetinventory)
        {
            if (id != assetinventory.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetinventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!assetinventoryExists(assetinventory.id))
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
            ViewData["assettype"] = new SelectList(_context.asset_type, "id", "assettype", assetinventory.assettype);
            ViewData["brand"] = new SelectList(_context.asset_model, "id", "model", assetinventory.brand);
            ViewData["operatingsystem"] = new SelectList(_context.asset_opertaingsystem, "id", "operatingsystem", assetinventory.operatingsystem);
            ViewData["org"] = new SelectList(_context.org_list, "id", "orgname", assetinventory.org);
            ViewData["ready"] = new SelectList(_context.asset_status, "id", "assetstatus", assetinventory.ready);
            return View(assetinventory);
        }

        // GET: AssetInventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetinventory = await _context.assetinventory.SingleOrDefaultAsync(m => m.id == id);
            if (assetinventory == null)
            {
                return NotFound();
            }

            return View(assetinventory);
        }

        // POST: AssetInventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetinventory = await _context.assetinventory.SingleOrDefaultAsync(m => m.id == id);
            _context.assetinventory.Remove(assetinventory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool assetinventoryExists(int id)
        {
            return _context.assetinventory.Any(e => e.id == id);
        }
    }
}
