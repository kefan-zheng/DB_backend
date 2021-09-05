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
    public class MomentController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_MOMENT>>> GetMoment()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_MOMENT>().ToListAsync();
        }

        // GET: api/Moment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_MOMENT>>> GetMoment(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_MOMENT>().Where(it => it.MOMENT_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Moment
        [HttpPost]
        public async Task<ActionResult<LD_MOMENT>> PostMoment(LD_MOMENT moment)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(moment).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_MOMENT>().Where(it => it.MOMENT_ID == moment.MOMENT_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetMoment), new { id = moment.MOMENT_ID }, moment);
        }


        // PUT: api/Moment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoment(string id, LD_MOMENT moment)
        {
            if (id != moment.MOMENT_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(moment).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_MOMENT>().Where(it => it.MOMENT_ID == id).Any())
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

        // DELETE: api/Moment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoment(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_MOMENT>().Where(it => it.MOMENT_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_MOMENT>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
