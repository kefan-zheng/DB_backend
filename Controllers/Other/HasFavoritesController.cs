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
    public class HasFavoritesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_HAS_FAVORITES>>> GetHasFavorites()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_HAS_FAVORITES>().ToListAsync();
        }

        // GET: api/HasFavorites
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_HAS_FAVORITES>>> GetHasFavorites(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_HAS_FAVORITES>().Where(it => it.FAVOR_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/HasFavorites
        [HttpPost]
        public async Task<ActionResult<LD_HAS_FAVORITES>> PostUser(LD_HAS_FAVORITES hasfavorites)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(hasfavorites).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_HAS_FAVORITES>().Where(it => it.FAVOR_ID == hasfavorites.FAVOR_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetHasFavorites), new { id = hasfavorites.FAVOR_ID }, hasfavorites);
        }


        // PUT: api/HasFavorites
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHasFavorites(string id, LD_HAS_FAVORITES hasfavorites)
        {
            if (id != hasfavorites.FAVOR_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(hasfavorites).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_HAS_FAVORITES>().Where(it => it.FAVOR_ID == id).Any())
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

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHasFavorites(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_HAS_FAVORITES>().Where(it => it.FAVOR_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_HAS_FAVORITES>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
