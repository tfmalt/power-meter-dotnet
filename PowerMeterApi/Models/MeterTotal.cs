using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerMeterApi.Models
{
    public class MeterTotal
    {
        public Double Value { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
