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
    public class site_configController : Controller
    {
        private readonly WttechportalDbContext _context;

        public site_configController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: site_config
        public async Task<IActionResult> Index()
        {
            return View(await _context.site_config.ToListAsync());
        }

        // GET: site_config/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site_config = await _context.site_config.SingleOrDefaultAsync(m => m.id == id);
            if (site_config == null)
            {
                return NotFound();
            }

            return View(site_config);
        }

        // GET: site_config/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: site_config/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,adminemail,smtppassword,smtpport,smtpserver,sslenabled,smtpserver,sendasemail,smtpuser")] site_config site_config)
        {
            if (ModelState.IsValid)
            {
                _context.Add(site_config);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(site_config);
        }

        // GET: site_config/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site_config = await _context.site_config.SingleOrDefaultAsync(m => m.id == id);
            if (site_config == null)
            {
                return NotFound();
            }
            return View(site_config);
        }

        // POST: site_config/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,adminemail,smtppassword,smtpport,smtpserver,sslenabled,smtpserver,sendasemail,smtpuser")] site_config site_config)
        {
            if (id != site_config.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(site_config);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!site_configExists(site_config.id))
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
            return View(site_config);
        }

        // GET: site_config/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site_config = await _context.site_config.SingleOrDefaultAsync(m => m.id == id);
            if (site_config == null)
            {
                return NotFound();
            }

            return View(site_config);
        }

        // POST: site_config/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var site_config = await _context.site_config.SingleOrDefaultAsync(m => m.id == id);
            _context.site_config.Remove(site_config);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool site_configExists(int id)
        {
            return _context.site_config.Any(e => e.id == id);
        }
    }
}
