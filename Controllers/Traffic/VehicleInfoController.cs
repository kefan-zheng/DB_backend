using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class VehicleInfoController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_VEHICLE_INFO>>> GetVehicleInfo()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_VEHICLE_INFO>().ToListAsync();
        }

        // GET: api/VehicleInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_VEHICLE_INFO>>> GetVehicleInfo(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_VEHICLE_INFO>().Where(it => it.VEHICLE_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/VehicleInfo
        [HttpPost]
        public async Task<ActionResult<LD_VEHICLE_INFO>> PostVehicleInfo(LD_VEHICLE_INFO vehicle)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(vehicle).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_VEHICLE_INFO>().Where(it => it.VEHICLE_ID == vehicle.VEHICLE_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetVehicleInfo), new { id = vehicle.VEHICLE_ID }, vehicle);
        }


        // PUT: api/VehicleInfo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleInfo(string id, LD_VEHICLE_INFO vehicle)
        {
            if (id != vehicle.VEHICLE_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(vehicle).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_VEHICLE_INFO>().Where(it => it.VEHICLE_ID == id).Any())
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

        // DELETE: api/VehicleInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleInfo(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_VEHICLE_INFO>().Where(it => it.VEHICLE_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_VEHICLE_INFO>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
