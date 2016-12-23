using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using WTTechPortal.Models;


namespace WTTechPortal.Controllers
{
    public class tasklistController : Controller
    {
        private readonly WttechportalDbContext _context;

 

        public tasklistController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: tasklist
       public async Task<IActionResult> Index(int? id, int? status, int? owner , int? prioity)
        {
            var priorities = _context.priority_select.OrderBy(c => c.priorityid).Select(x => new { Id = x.priorityid, Value = x.priorityname });
            var statuslist = _context.status_select.OrderBy(c => c.statusid).Select(x => new { Id = x.statusid, Value = x.statusname });
            var ownerlist = _context.owner_select.OrderBy(c => c.ownerid).Select(x => new { Id = x.ownerid, Value = x.ownername });
            ViewBag.priorities = new SelectList(priorities, "Id", "Value");
            ViewBag.statuslist = new SelectList(statuslist, "Id", "Value");
            ViewBag.ownerlist = new SelectList(ownerlist, "Id", "Value");


            var list = (from a in _context.tasklist
                        select a );

            list = list.Include(p => p.prioritites).Include(o => o.owners).Include(s => s.statuses).OrderBy( or => or.status);

            if (status != null)
            {
                list = list.Where(l => l.status.Equals(status));
            }

            if (owner != null)
            {
                list = list.Where(l => l.owner.Equals(owner));
            }


            if (prioity != null)
            {
                list = list.Where(l => l.priority.Equals(prioity));
            }

            return View(await list.ToListAsync());
        }





        // GET: tasklist/Details/5


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




        // GET: tasklist/Create

        public IActionResult Create()
        {

            var priorities = _context.priority_select.OrderBy(c => c.priorityid).Select(x => new { Id = x.priorityid, Value = x.priorityname });
            var statuslist = _context.status_select.OrderBy(c => c.statusid).Select(x => new { Id = x.statusid, Value = x.statusname });
            var ownerlist = _context.owner_select.OrderBy(c => c.ownerid).Select(x => new { Id = x.ownerid, Value = x.ownername });
            ViewBag.priorities = new SelectList(priorities, "Id", "Value");
            ViewBag.statuslist = new SelectList(statuslist, "Id", "Value");
            ViewBag.ownerlist = new SelectList(ownerlist, "Id", "Value");
            return View();
            
        }


 

        // POST: tasklist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,actionitem,comments,completedate,owner,priority,status,task")] tasklist tasklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tasklist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tasklist);
        }

        // GET: tasklist/Edit/5
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
            var priorities = _context.priority_select.OrderBy(c => c.priorityid).Select(x => new { Id = x.priorityid, Value = x.priorityname });
            var statuslist = _context.status_select.OrderBy(c => c.statusid).Select(x => new { Id = x.statusid, Value = x.statusname });
            var ownerlist = _context.owner_select.OrderBy(c => c.ownerid).Select(x => new { Id = x.ownerid, Value = x.ownername });
            ViewBag.priorities = new SelectList(priorities, "Id", "Value");
            ViewBag.statuslist = new SelectList(statuslist, "Id", "Value");
            ViewBag.ownerlist = new SelectList(ownerlist, "Id", "Value");
            return View(tasklist);
        }

        // POST: tasklist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,actionitem,comments,completedate,owner,priority,status,task")] tasklist tasklist)
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
            return View(tasklist);
        }

        // GET: tasklist/Delete/5
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

        // POST: tasklist/Delete/5
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

