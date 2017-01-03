using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WTTechPortal.Models
{
    public class HyperVModels
    {
        public IEnumerable<WTTechPortal.Models.hypervperf> hypervperf { get; set; }
        public IEnumerable<WTTechPortal.Models.hypervvms> hypervvms { get; set; }

        internal string ToList()
        {
            throw new NotImplementedException();
        }
    }
}
