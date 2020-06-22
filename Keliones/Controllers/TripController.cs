using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Keliones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        // GET: api/Trip
        [HttpGet]
        public List<Trip> Get()
        {
            return DbContext.GetTrips();
        }

        // GET api/Trip/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            return DbContext.GetTrip(id);
        }

        // POST api/Trip
        [HttpPost]
        public void Post([FromBody] Trip trip)
        {
            DbContext.InsertTrip(trip);
        }

        // PUT api/Trip/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Trip trip)
        {
          // 
        }

        // DELETE api/Trip/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DbContext.DeleteTrip(id);
        }
    }
}
