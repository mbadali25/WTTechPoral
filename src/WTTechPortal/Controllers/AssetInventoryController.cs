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
        public async Task<IActionResult> Index(int? assetstatus, int? brand, int? opertaingsystem)
        {
            var wttechportalDbContext = (from a in _context.assetinventory select a);

            var modelist = _context.asset_model.OrderBy(c => c.id).Select(x => new { Id = x.id, Value = x.model });
            var statuslist = _context.asset_status.OrderBy(c => c.id).Select(x => new { Id = x.id, Value = x.assetstatus });
            var operatingsystemlist = _context.asset_opertaingsystem.OrderBy(c => c.id).Select(x => new { Id = x.id, Value = x.operatingsystem });
            ViewBag.modelist = new SelectList(modelist, "Id", "Value");
            ViewBag.statuslist = new SelectList(statuslist, "Id", "Value");
            ViewBag.operatingsystemlist = new SelectList(operatingsystemlist, "Id", "Value");

            // Resutls Count
            ViewBag.totalassets = wttechportalDbContext.Count();
            ViewBag.assetsready = wttechportalDbContext.Where(x => x.ready.Equals(1)).Count();
            ViewBag.assetsreadyneedrepair = wttechportalDbContext.Where(x => x.ready.Equals(6)).Count();
            ViewBag.assestsneedrepair = wttechportalDbContext.Where(x => x.ready.Equals(2)).Count();
            ViewBag.assetsunchecked = wttechportalDbContext.Where(x => x.ready.Equals(5)).Count();
            ViewBag.functionalneedos = wttechportalDbContext.Where(x => x.ready.Equals(3)).Count();
            ViewBag.assetsnotusable = wttechportalDbContext.Where(x => x.ready.Equals(4)).Count();

            // var wttechportalDbContext = _context.assetinventory.Include(a => a.assettypes).Include(a => a.brands).Include(a => a.operatingsystems).Include(a => a.orginzation).Include(a => a.status).OrderBy(a => a.ready);


            if (assetstatus != null)
            {
                wttechportalDbContext = wttechportalDbContext.Where(a => a.ready.Equals(assetstatus));
            }

            if (brand != null)
            {
                wttechportalDbContext = wttechportalDbContext.Where(a => a.brand.Equals(brand));

            }
            if (opertaingsystem != null)
            {
                wttechportalDbContext = wttechportalDbContext.Where(a => a.operatingsystem.Equals(opertaingsystem));
            }

            wttechportalDbContext = wttechportalDbContext.Include(a => a.assettypes).Include(a => a.brands).Include(a => a.operatingsystems).Include(a => a.orginzation).Include(a => a.status).OrderBy(a => a.ready);

            ViewBag.results = wttechportalDbContext.Count();

            return View(await wttechportalDbContext.ToListAsync());
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
