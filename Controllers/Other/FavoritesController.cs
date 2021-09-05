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
    public class FavoritesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_FAVORITES>>> GetFavorites()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_FAVORITES>().ToListAsync();
        }

        // GET: api/Favorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_FAVORITES>>> GetFavorites(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_FAVORITES>().Where(it => it.FAVOR_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Users
        [HttpPost]
        public async Task<ActionResult<LD_FAVORITES>> PostFavorites(LD_FAVORITES favorites)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(favorites).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_FAVORITES>().Where(it => it.FAVOR_ID == favorites.FAVOR_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetFavorites), new { id = favorites.FAVOR_ID }, favorites);
        }


        // PUT: api/Favorites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorites(string id, LD_FAVORITES favorites)
        {
            if (id != favorites.FAVOR_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(favorites).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_FAVORITES>().Where(it => it.FAVOR_ID == id).Any())
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

        // DELETE: api/Favorites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorites(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_FAVORITES>().Where(it => it.FAVOR_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_FAVORITES>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
