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
    public class ReleaseMomentController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_RELEASE_MOMENT>>> GetReleaseMoment()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_RELEASE_MOMENT>().ToListAsync();
        }

        // GET: api/ReleaseMoment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_RELEASE_MOMENT>>> GetReleaseMoment(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_RELEASE_MOMENT>().Where(it => it.MOMENT_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/ReleaseMoment
        [HttpPost]
        public async Task<ActionResult<LD_RELEASE_MOMENT>> PostReleaseMoment(LD_RELEASE_MOMENT moment)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(moment).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_RELEASE_MOMENT>().Where(it => it.MOMENT_ID == moment.MOMENT_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetReleaseMoment), new { id = moment.MOMENT_ID }, moment);
        }


        // PUT: api/ReleaseMoment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReleaseMoment(string id, LD_RELEASE_MOMENT moment)
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
                if (!db.Queryable<LD_RELEASE_MOMENT>().Where(it => it.MOMENT_ID == id).Any())
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

        // DELETE: api/ReleaseMoment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReleaseMoment(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_RELEASE_MOMENT>().Where(it => it.MOMENT_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_RELEASE_MOMENT>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
