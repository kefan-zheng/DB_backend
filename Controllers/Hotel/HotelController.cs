using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using LvDao;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_HOTEL>>> GetHotel()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_HOTEL>().ToListAsync();
        }

        // GET: api/Hotel
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_HOTEL>>> GetHotel(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_HOTEL>().Where(it => it.HOTEL_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Hotel
        [HttpPost]
        public async Task<ActionResult<LD_HOTEL>> PostHotel(LD_HOTEL hotel)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(hotel).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_HOTEL>().Where(it => it.HOTEL_ID == hotel.HOTEL_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.HOTEL_ID }, hotel);
        }


        // PUT: api/Hotel
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(string id, LD_HOTEL hotel)
        {
            if (id != hotel.HOTEL_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(hotel).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_HOTEL>().Where(it => it.HOTEL_ID == id).Any())
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

        // DELETE: api/Hotel
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_HOTEL>().Where(it => it.HOTEL_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_HOTEL>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
