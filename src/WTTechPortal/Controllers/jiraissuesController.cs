using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using WTTechPortal.Models.Jira;

namespace WTTechPortal.Controllers
{
    public class jiraissuesController : Controller
    {
        private readonly JiraDbContext _context;

        public jiraissuesController(JiraDbContext context)
        {
            _context = context;    
        }

        // GET: jiraissues
        public async Task<IActionResult> Index()
        {
            return View(await _context.jiraissue.ToListAsync());
        }

        // GET: jiraissues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jiraissue = await _context.jiraissue.SingleOrDefaultAsync(m => m.ID == id);
            if (jiraissue == null)
            {
                return NotFound();
            }

            return View(jiraissue);
        }

        // GET: jiraissues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: jiraissues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ASSIGNEE,DESCRIPTION,DUEDATE,PROJECT,RESOLUTION,SUMMARY,TIMEORIGINALESTIMATE,TIMESPENT,issuenum,issuestatus,issuetype")] jiraissue jiraissue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jiraissue);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(jiraissue);
        }

        // GET: jiraissues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jiraissue = await _context.jiraissue.SingleOrDefaultAsync(m => m.ID == id);
            if (jiraissue == null)
            {
                return NotFound();
            }
            return View(jiraissue);
        }

        // POST: jiraissues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ASSIGNEE,DESCRIPTION,DUEDATE,PROJECT,RESOLUTION,SUMMARY,TIMEORIGINALESTIMATE,TIMESPENT,issuenum,issuestatus,issuetype")] jiraissue jiraissue)
        {
            if (id != jiraissue.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jiraissue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!jiraissueExists(jiraissue.ID))
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
            return View(jiraissue);
        }

        // GET: jiraissues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jiraissue = await _context.jiraissue.SingleOrDefaultAsync(m => m.ID == id);
            if (jiraissue == null)
            {
                return NotFound();
            }

            return View(jiraissue);
        }

        // POST: jiraissues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jiraissue = await _context.jiraissue.SingleOrDefaultAsync(m => m.ID == id);
            _context.jiraissue.Remove(jiraissue);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool jiraissueExists(int id)
        {
            return _context.jiraissue.Any(e => e.ID == id);
        }
    }
}
