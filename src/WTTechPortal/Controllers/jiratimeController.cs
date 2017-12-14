using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using WTTechPortal.Models.Jira;
using Microsoft.AspNetCore.Identity;
using WTTechPortal.Models;

namespace WTTechPortal.Controllers
{
    public class jiratimeController : Controller
    {
        private readonly JiraDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public jiratimeController(JiraDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: jiratime
        public async Task<IActionResult> Index(int? month, int? year, bool? usercheck)
        {
            var yearlist = new SelectList(new[]
{
                new {Id="2016",Value="2016" },
                new {Id="2017",Value="2017" },
            },
                "Id", "Value");

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

 

            //set variable for checkbox
            bool isusercheck =false;

            if (usercheck != null)
            {
                isusercheck = Convert.ToBoolean(usercheck);
            }
            else
            {
                isusercheck = false;
            }
            ViewBag.isusercheck = isusercheck;
            
            var results = _context.jiraissue.Where(a => a.PROJECT.Equals(10500)).Where(b => b.TIMESPENT.HasValue).Include(x => x.projects).Include(i => i.issusestatusname).Include(r => r.resolutions).Include(w => w.worklogs).Where(m => m.DUEDATE.Value.Month.Equals(month)).Where(y => y.DUEDATE.Value.Year.Equals(year));

            if (isusercheck == true)


            {
                var user = _userManager.GetUserName(User);

                //handle wrong username
                if (user == "mbadali25")
                {
                    user = "mbadali";
                }
                results = _context.jiraissue.Where(a => a.PROJECT.Equals(10500)).Where(b => b.TIMESPENT.HasValue).Include(x => x.projects).Include(i => i.issusestatusname).Include(r => r.resolutions).Where(m => m.DUEDATE.Value.Month.Equals(month)).Where(y => y.DUEDATE.Value.Year.Equals(year)).Where(a => a.ASSIGNEE.Equals(user));
                
            }


            var list = (from a in results                
                        select a);


            list = list.Include(cv => cv.customfieldvalues).Include(co => co.customfieldvalues.customfieldoptions);

            var group = (list.GroupBy(a => a.customfieldvalues.customfieldoptions).Select(a => new { Hours = a.Sum(b => b.TIMESPENThr), Name = a.Key.customvalue, WorkName = a.Select(x => x.customfieldvalues.customfieldoptions.customvalue).First() }).Select(r => new SelectListItem
            {
                Text = r.WorkName,
                Value = r.Hours.ToString()
            })).ToList();

            ViewBag.groups = group;
            
            return View(await results.ToListAsync());
        }

        // GET: jiratime/Details/5
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

        // GET: jiratime/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: jiratime/Create
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

        // GET: jiratime/Edit/5
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

        // POST: jiratime/Edit/5
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

        // GET: jiratime/Delete/5
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

        // POST: jiratime/Delete/5
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
