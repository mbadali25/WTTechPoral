using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using WTTechPortal.Data;
using WTTechPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Localization.SqlLocalizer.DbStringLocalizer;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WTTechPortal.Controllers
{
    [Authorize]
    [Route("api/ImportExport")]
    public class ImportExportController : Controller
    {
        
        private readonly WttechportalDbContext _context;

        public ImportExportController(WttechportalDbContext context)
        {
            
            _context = context;
        }

        // http://localhost:6062/api/ImportExport/localizedData.csv
        [HttpGet]
        [Route("localizedData.csv")]
        [Produces("text/csv")]
        public IActionResult GetDataAsCsv()
        {
            
            return Ok((_context.tasklist).ToList());
        }

        
        private List<LocalizationRecord> readStream(Stream stream)
        {
            bool skipFirstLine = true;
            string csvDelimiter = ";";

            List<LocalizationRecord> list = new List<LocalizationRecord>();
            var reader = new StreamReader(stream);


            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(csvDelimiter.ToCharArray());
                if (skipFirstLine)
                {
                    skipFirstLine = false;
                }
                else
                {
                    var itemTypeInGeneric = list.GetType().GetTypeInfo().GenericTypeArguments[0];
                    var item = new LocalizationRecord();
                    var properties = item.GetType().GetProperties();
                    for (int i = 0; i < values.Length; i++)
                    {
                        properties[i].SetValue(item, Convert.ChangeType(values[i], properties[i].PropertyType), null);
                    }

                    list.Add(item);
                }

            }

            return list;
        }
        }
}
