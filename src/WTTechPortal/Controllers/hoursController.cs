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
    public class hoursController : Controller
    {
        private readonly WttechportalDbContext _context;

        public hoursController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: hours
        public async Task<IActionResult> Index(int? month, int? year)
        {
            var yearlist = new SelectList(new[]
            {
                new {Id="2016",Value="2016" },
                new {Id="2017",Value="2017" },
            },
                "Id","Value");

            var monthlist = new SelectList(new[]
            {
                new {Value="Janurary",Id="1" },
                new {Value="Feburary",Id="2" },
                new {Value="March",Id="3" },
                new {Value="April",Id="4" },
                new {Value="May",Id="5" },
                new {Value="June",Id="6" },
                new {Value="July",Id="7" },
                new {Value="August",Id="8" },
                new {Value="September",Id="9" },
                new {Value="October",Id="10" },
                new {Value="November",Id="11" },
                new {Value="December",Id="12" },
            },
            "Id", "Value");
            if (month == null)
            {
                month = DateTime.Today.Month;
            }
            if (year == null)
            {
                year = DateTime.Today.Year;
            }

            // Storing List in View bags
            ViewBag.yearlist = yearlist;
            ViewBag.monthlist = monthlist;
            var list = (from a in _context.tasklist
                        where a.completedate.HasValue
                        select a);

            list = list.Include(p => p.prioritites).Include(o => o.owners).Include(s => s.statuses).Include(s => s.workcodes).OrderBy(or => or.status).Where(aa => aa.completedate.Value.Month.Equals(month)).Where(aa => aa.completedate.Value.Year.Equals(year)).Where(x => x.status.Equals(10)).OrderBy(x => x.workcode);
            
            
            var group = (list.GroupBy(a => a.workcode).Select(a => new { Hours = a.Sum(b => b.hours.Value), Name = a.Key.Value, WorkName = a.Select(x => x.workcodes.workcode).First() }).Select(r => new SelectListItem
            {
                Text = r.WorkName,
                Value = r.Hours.ToString()
            
            })).ToList();
            
            

            ViewBag.groups = group;
            
                   
            return View(await list.ToListAsync());
        }

        // GET: hours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasklist = await _context.tasklist.SingleOrDefaultAsync(m => m.id == id);
            if (tasklist == null)
            {
                return NotFound();
            }

            return View(tasklist);
        }

        // GET: hours/Create
        public IActionResult Create()
        {
            ViewData["owner"] = new SelectList(_context.owner_select, "ownerid", "ownerid");
            ViewData["priority"] = new SelectList(_context.priority_select, "priorityid", "priorityid");
            ViewData["status"] = new SelectList(_context.status_select, "statusid", "statusid");
            ViewData["workcode"] = new SelectList(_context.workcodes, "id", "id");
            return View();
        }

        // POST: hours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,actionitem,comments,completedate,desiredcompdate,hours,org,owner,priority,status,task,updateddate,workcode")] tasklist tasklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tasklist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["owner"] = new SelectList(_context.owner_select, "ownerid", "ownerid", tasklist.owner);
            ViewData["priority"] = new SelectList(_context.priority_select, "priorityid", "priorityid", tasklist.priority);
            ViewData["status"] = new SelectList(_context.status_select, "statusid", "statusid", tasklist.status);
            ViewData["workcode"] = new SelectList(_context.workcodes, "id", "id", tasklist.workcode);
            return View(tasklist);
        }

        // GET: hours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasklist = await _context.tasklist.SingleOrDefaultAsync(m => m.id == id);
            if (tasklist == null)
            {
                return NotFound();
            }
            ViewData["owner"] = new SelectList(_context.owner_select, "ownerid", "ownerid", tasklist.owner);
            ViewData["priority"] = new SelectList(_context.priority_select, "priorityid", "priorityid", tasklist.priority);
            ViewData["status"] = new SelectList(_context.status_select, "statusid", "statusid", tasklist.status);
            ViewData["workcode"] = new SelectList(_context.workcodes, "id", "id", tasklist.workcode);
            return View(tasklist);
        }

        // POST: hours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,actionitem,comments,completedate,desiredcompdate,hours,org,owner,priority,status,task,updateddate,workcode")] tasklist tasklist)
        {
            if (id != tasklist.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasklist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tasklistExists(tasklist.id))
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
            ViewData["owner"] = new SelectList(_context.owner_select, "ownerid", "ownerid", tasklist.owner);
            ViewData["priority"] = new SelectList(_context.priority_select, "priorityid", "priorityid", tasklist.priority);
            ViewData["status"] = new SelectList(_context.status_select, "statusid", "statusid", tasklist.status);
            ViewData["workcode"] = new SelectList(_context.workcodes, "id", "id", tasklist.workcode);
            return View(tasklist);
        }

        // GET: hours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasklist = await _context.tasklist.SingleOrDefaultAsync(m => m.id == id);
            if (tasklist == null)
            {
                return NotFound();
            }

            return View(tasklist);
        }

        // POST: hours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasklist = await _context.tasklist.SingleOrDefaultAsync(m => m.id == id);
            _context.tasklist.Remove(tasklist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool tasklistExists(int id)
        {
            return _context.tasklist.Any(e => e.id == id);
        }
    }
}
