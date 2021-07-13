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

namespace LvDao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class FavouriteContentsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_FAVOURITES_CONTENTS>>> GetFavouriteContents()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_FAVOURITES_CONTENTS>().ToListAsync();
        }

        // GET: api/FavouriteContents
        [HttpGet("{id_link}")]
        public async Task<ActionResult<IEnumerable<LD_FAVOURITES_CONTENTS>>> GetFavouriteContents(string id_link)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = id_link.Split(new char[] { '&' });
            string id = para[0];
            string link = para[1];
            var res = await db.Queryable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == id && it.MERCHANT_LINK == link).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/FavouriteContents
        [HttpPost]
        public async Task<ActionResult<LD_USER>> PostFavouriteContents(LD_FAVOURITES_CONTENTS favouritecontents)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(favouritecontents).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == favouritecontents.FAVOR_ID && it.MERCHANT_LINK == favouritecontents.MERCHANT_LINK).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetFavouriteContents), new { id = favouritecontents.FAVOR_ID , link = favouritecontents.MERCHANT_LINK }, favouritecontents);
        }


        // PUT: api/FavouriteContents
        [HttpPut("{id_link}")]
        public async Task<IActionResult> PutUser(string id_link, LD_FAVOURITES_CONTENTS favouritecontents)
        {
            string[] para = id_link.Split(new char[] { '&' });
            string id = para[0];
            string link = para[1];
            if (id != favouritecontents.FAVOR_ID || link !=favouritecontents.MERCHANT_LINK)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(favouritecontents).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == id && it.MERCHANT_LINK == link).Any())
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

        // DELETE: api/FavouriteContents
        [HttpDelete("{id_link}")]
        public async Task<IActionResult> DeleteFavouriteContents(string id_link)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = id_link.Split(new char[] { '&' });
            string id = para[0];
            string link = para[1];
            var res = await db.Queryable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == id && it.MERCHANT_LINK == link).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == id && it.MERCHANT_LINK == link).ExecuteCommand());
            return NoContent();
        }
    }
}
