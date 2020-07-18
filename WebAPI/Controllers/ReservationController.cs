using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.BSL.Interface;
using Web.DAL;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IGenericRepository<Reservation> genericRepository;
        CustomjsonResult customjsonResult = new CustomjsonResult();
        public ReservationController(IGenericRepository<Reservation> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
       
        // POST: api/Reservation
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(Reservation reservation)
        {
            try
            {
                await genericRepository.Add(reservation);
                customjsonResult.Result = "Sucessfully Register";
                return Ok(customjsonResult);
            }
            catch
            {
                customjsonResult.Result = "Error";
                return Ok(customjsonResult);
            }
 
        }

        // PUT: api/Reservation/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
