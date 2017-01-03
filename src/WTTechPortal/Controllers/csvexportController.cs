using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WTTechPortal.Models;
using Microsoft.AspNetCore.Mvc;
using WTTechPortal.Data;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net;
using Microsoft.Net.Http.Headers;

namespace WTTechPortal.Controllers
{

    [Route("api/[controller]")]
    public class csvexportController : Controller
    {
        private readonly WttechportalDbContext _context;

        public csvexportController(WttechportalDbContext context)
        {
            _context = context;
            
        }

        // GET api/csvtest
        [HttpGet]
        public IActionResult Get()
        {
            var datat = (_context.tasklist.Select(x => x)).ToList();
            return Ok(datat);
        }

        [HttpGet]
        [Route("data.csv")]
        [Produces("text/csv")]
        public IActionResult GetDataAsCsv()
        {
            var datat = (_context.tasklist.Select(x => x)).ToList();
            
            return Ok(datat);
            
        }





    }
}

