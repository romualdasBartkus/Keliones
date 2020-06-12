using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keliones
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Trip> UserTrips { get; set; }
        public string UserCountry { get; set; }

        public User()
        {
            UserTrips = new List<Trip>();
        }
    }
}
