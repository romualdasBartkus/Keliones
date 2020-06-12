using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keliones
{
    public class Trip
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Country { get; set; }
        public List<string> Cities { get; set; }
        public decimal Rating { get; set; }
        public decimal Price { get; set; }

    }
}
