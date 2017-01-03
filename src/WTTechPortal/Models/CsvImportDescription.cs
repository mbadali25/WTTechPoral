
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WTTechPortal.Models
{
    public class CsvImportDescription
    {
        public string Information { get; set; }
        public ICollection<IFormFile> File { get; set; }
    }
}
