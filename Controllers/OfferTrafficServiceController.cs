using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class OfferTrafficServiceController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_OFFER_TRAFFIC_SERVICE>>> GetOfferTrafficService()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_OFFER_TRAFFIC_SERVICE>().ToListAsync();
        }

        // GET: api/OfferTrafficService/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_OFFER_TRAFFIC_SERVICE>>> GetOfferTrafficService(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_OFFER_TRAFFIC_SERVICE>().Where(it => it.VEHICLE_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/OfferTrafficService
        [HttpPost]
        public async Task<ActionResult<LD_OFFER_TRAFFIC_SERVICE>> PostOfferTrafficService(LD_OFFER_TRAFFIC_SERVICE vehicle_service)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(vehicle_service).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_OFFER_TRAFFIC_SERVICE>().Where(it => it.VEHICLE_ID == vehicle_service.VEHICLE_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetOfferTrafficService), new { id = vehicle_service.VEHICLE_ID }, vehicle_service);
        }


        // PUT: api/OfferTrafficService/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfferTrafficService(string id, LD_OFFER_TRAFFIC_SERVICE vehicle_service)
        {
            if (id != vehicle_service.VEHICLE_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(vehicle_service).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_OFFER_TRAFFIC_SERVICE>().Where(it => it.VEHICLE_ID == id).Any())
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/OfferTrafficService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfferTrafficService(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_OFFER_TRAFFIC_SERVICE>().Where(it => it.VEHICLE_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_OFFER_TRAFFIC_SERVICE>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
