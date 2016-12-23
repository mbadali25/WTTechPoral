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
    public class priority_selectController : Controller
    {
        private readonly WttechportalDbContext _context;

        public priority_selectController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: priority_select
        public async Task<IActionResult> Index()
        {
            return View(await _context.priority_select.ToListAsync());
        }

        // GET: priority_select/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority_select = await _context.priority_select.SingleOrDefaultAsync(m => m.priorityid == id);
            if (priority_select == null)
            {
                return NotFound();
            }

            return View(priority_select);
        }

        // GET: priority_select/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: priority_select/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("priorityid,priorityname")] priority_select priority_select)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priority_select);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(priority_select);
        }

        // GET: priority_select/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority_select = await _context.priority_select.SingleOrDefaultAsync(m => m.priorityid == id);
            if (priority_select == null)
            {
                return NotFound();
            }
            return View(priority_select);
        }

        // POST: priority_select/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("priorityid,priorityname")] priority_select priority_select)
        {
            if (id != priority_select.priorityid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priority_select);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!priority_selectExists(priority_select.priorityid))
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
            return View(priority_select);
        }

        // GET: priority_select/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority_select = await _context.priority_select.SingleOrDefaultAsync(m => m.priorityid == id);
            if (priority_select == null)
            {
                return NotFound();
            }

            return View(priority_select);
        }

        // POST: priority_select/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priority_select = await _context.priority_select.SingleOrDefaultAsync(m => m.priorityid == id);
            _context.priority_select.Remove(priority_select);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool priority_selectExists(int id)
        {
            return _context.priority_select.Any(e => e.priorityid == id);
        }
    }
}
