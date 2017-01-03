using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using WTTechPortal.Models;
using Microsoft.AspNetCore.Authorization;

namespace WTTechPortal.Controllers
{
    [Authorize]
    public class org_listController : Controller
    {
        private readonly WttechportalDbContext _context;

        public org_listController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: org_list
        public async Task<IActionResult> Index()
        {
            return View(await _context.org_list.ToListAsync());
        }

        // GET: org_list/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var org_list = await _context.org_list.SingleOrDefaultAsync(m => m.id == id);
            if (org_list == null)
            {
                return NotFound();
            }

            return View(org_list);
        }

        // GET: org_list/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: org_list/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,orgname,emailcontact")] org_list org_list)
        {
            if (ModelState.IsValid)
            {
                _context.Add(org_list);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(org_list);
        }

        // GET: org_list/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var org_list = await _context.org_list.SingleOrDefaultAsync(m => m.id == id);
            if (org_list == null)
            {
                return NotFound();
            }
            return View(org_list);
        }

        // POST: org_list/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,orgname,emailcontact")] org_list org_list)
        {
            if (id != org_list.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(org_list);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!org_listExists(org_list.id))
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
            return View(org_list);
        }

        // GET: org_list/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var org_list = await _context.org_list.SingleOrDefaultAsync(m => m.id == id);
            if (org_list == null)
            {
                return NotFound();
            }

            return View(org_list);
        }

        // POST: org_list/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var org_list = await _context.org_list.SingleOrDefaultAsync(m => m.id == id);
            _context.org_list.Remove(org_list);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool org_listExists(int id)
        {
            return _context.org_list.Any(e => e.id == id);
        }
    }
}
