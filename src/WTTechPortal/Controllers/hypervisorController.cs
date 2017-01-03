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
using System.Dynamic;

namespace WTTechPortal.Controllers
{
    [Authorize]
    public class hypervisorController : Controller
    {
        private readonly WttechportalDbContext _context;

        public hypervisorController(WttechportalDbContext context)
        {
            _context = context;    
        }

        // GET: hypervisor
        public IActionResult Index()
        {
            var model = _context.hypervperf;
            ViewBag.hypervvms = _context.hypervvms;

            return View(model);
        }

        
        private bool hypervperfExists(int id)
        {
            return _context.hypervperf.Any(e => e.id == id);
        }
    }
}
