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
        public string PradziosData { get; set; }
        public string PabaigosData { get; set; }
        public string Salis { get; set; }
        public List<string> Miestai { get; set; }
        public decimal Rating { get; set; }

    }
}
