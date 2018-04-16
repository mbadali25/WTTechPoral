using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using WTTechPortal.Models.Jira;
using Microsoft.AspNetCore.Identity;
using WTTechPortal.Services;
using WTTechPortal.Models;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.IO;

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
        public async Task<IActionResult> Index(int? month, int? year,  int? projectid, bool? usercheck)
        {
            


            var yearlist = new SelectList(new[]
{
                new {Id="2016",Value="2016" },
                new {Id="2017",Value="2017" },
                new {Id="2018",Value="2018" },
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

            if (month == null)
            {
                month = DateTime.Today.Month;
            }
            if (year == null)
            {
                year = DateTime.Today.Year;
            }

            
            var results = _context.jiraissue.Include(cv => cv.customfieldvalues).Include(co => co.customfieldvalues.customfieldoptions).Include(x => x.projects).Include(i => i.issusestatusname).Include(r => r.resolutions).Include(w => w.worklogs).Where(r => r.RESOLUTIONDATE.HasValue).Where(b => b.TIMESPENT.HasValue).Where(m => m.DUEDATE.Value.Month.Equals(month)).Where(y => y.DUEDATE.Value.Year.Equals(year));
            if (projectid != null)
            {
                results = results.Where(a => a.PROJECT.Equals(projectid));
            }
            

            


            if (isusercheck == true)


            {
                var user = _userManager.GetUserName(User);

                //handle wrong username
                if (user == "mbadali25")
                {
                    user = "mbadali";
                }
 
                    results = results.Where(a => a.ASSIGNEE.Equals(user));
                
                

            }


            var list = (from a in results                
                        select a);


            list = list.Include(cv => cv.customfieldvalues).Include(co => co.customfieldvalues.customfieldoptions);

            var group = (list.GroupBy(a => a.customfieldvalues.customfieldoptions).Select(a => new { Hours = a.Sum(b => b.TIMESPENThr), Name = a.Key.customvalue, WorkName = a.Select(x => x.customfieldvalues.customfieldoptions.customvalue).First() }).Select(r => new SelectListItem
            {
                Text = r.WorkName,
                Value = r.Hours.ToString()
            })).ToList();

            var companies = _context.project;
            var companylist = (from c in companies
                               orderby c.pname
                               select c).ToList();
            companylist.Insert(0 ,new project { ID = 0, pname = "Select All" });
            
            ViewBag.groups = group;
            ViewBag.companylistitems = companylist;
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

        // GET: jiratime/Export
      /*  public IActionResult Export(int? month, int? year)
        {
            if (month == null)
            {
                month = DateTime.Today.Month;
            }
            if (year == null)
            {
                year = DateTime.Today.Year;
            }


            var results = _context.jiraissue.ToList();
            // Step 1
            var document = new Document
            {
                PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 42) }
            };

            // Step 2
            var pdfPage = document.Pages.Add();

            // Step 3
      

            // Initializes a new instance of the TextFragment for report's title 
            var textFragment = new TextFragment("Test Title");
            // Set text properties
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            textFragment.TextState.FontStyle = FontStyles.Bold;

            // Initializes a new instance of the Table
            Table table = new Table
            {
                // Set column auto widths of the table
                ColumnWidths = "10 10 10 10 10 10",
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Set cell padding
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                // Set the table border color as Black
                Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                // Set the border for table cells as Black
                DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black),
            };

            table.DefaultCellTextState = new TextState("TimesNewRoman", 10);

            //table.SetColumnTextState(5, paymentFormat);
            table.ImportEntityList(results);

            //Repeat Header
            table.RepeatingRowsCount = 1;

            // Step 4
            pdfPage.Paragraphs.Add(textFragment);


            // Add table object to first page of input document
            pdfPage.Paragraphs.Add(table);

            // Step 5
            using (var streamOut = new MemoryStream())
            {
                document.Save(streamOut);
                return new FileContentResult(streamOut.ToArray(), "application/pdf")
                {
                    FileDownloadName = "tenants.pdf"
                };
            }
        
    }*/

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
