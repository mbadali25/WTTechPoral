using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using WTTechPortal.Models;
using Sakura.AspNetCore;
using WTTechPortal.Models.IPAM;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace WTTechPortal.Controllers
{
    [Authorize]
    public class ipamController : Controller
    {
        private readonly WttechportalDbContext _context;

        public ipamController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: ipam
        public async Task<IActionResult> Index(string state, string searchstring)
        {
            var wttechportalDbContext = from i in _context.ipam select i;
            wttechportalDbContext = wttechportalDbContext.Include(i => i.orginzation);
            ViewBag.availableresults = wttechportalDbContext.Where(x => x.state.Equals("AVAILABLE")).Count();
            ViewBag.usedresults = wttechportalDbContext.Where(x => x.state.Equals("USED")).Count();
            var statelist = _context.ipam.OrderBy(c => c.state).Select(x => new { Id = x.state, Value = x.state }).Distinct();
            ViewBag.statelist = new SelectList(statelist, "Id", "Value");

            if (state != null)
            {
                wttechportalDbContext = wttechportalDbContext.Where(x => x.state.Equals(state)); 
            }
            if (searchstring != null)
            {
                wttechportalDbContext = from i in wttechportalDbContext
                                        where (i.ip.Contains(searchstring) || i.hostname.Contains(searchstring))
                                        select i;
                                        
            }
            ViewBag.totalresults = wttechportalDbContext.Count();

            



            int pagenumb = 1;
            int pagesize = 100;
            return View(await wttechportalDbContext.ToPagedListAsync(pagesize,pagenumb) );
            
        }



        private bool ipamExists(int id)
        {
            return _context.ipam.Any(e => e.id == id);
        }
    }
}
