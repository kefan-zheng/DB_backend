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
    //[Authorize]
    public class AttractionController:ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_ATTRACTION>>> GetAttraction()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_ATTRACTION>().ToListAsync();
        }

        // GET: api/Attraction
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_ATTRACTION>>> GetAttraction(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_ATTRACTION>().Where(it => it.ATTRACTION_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Attraction
        [HttpPost]
        public async Task<ActionResult<LD_ATTRACTION>> PostAttraction(LD_ATTRACTION attraction)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(attraction).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_ATTRACTION>().Where(it => it.ATTRACTION_ID == attraction.ATTRACTION_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetAttraction), new { id = attraction.ATTRACTION_ID }, attraction);
        }


        // PUT: api/Attraction
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttraction(string id, LD_ATTRACTION attraction)
        {
            if (id != attraction.ATTRACTION_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(attraction).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_ATTRACTION>().Where(it => it.ATTRACTION_ID == id).Any())
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

        // DELETE: api/Attraction
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttraction(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_ATTRACTION>().Where(it => it.ATTRACTION_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_ATTRACTION>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
