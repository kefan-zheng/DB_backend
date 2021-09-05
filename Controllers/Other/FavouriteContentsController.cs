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
        [HttpGet("{fid_lid}")]
        public async Task<ActionResult<IEnumerable<LD_FAVOURITES_CONTENTS>>> GetFavouriteContents(string fid_lid)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = fid_lid.Split(new char[] { '&' });
            string fid = para[0];
            string lid = para[1];
            var res = await db.Queryable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == fid && it.LINK_ID == lid).ToListAsync();
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
                if (db.Queryable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == favouritecontents.FAVOR_ID && it.LINK_ID == favouritecontents.MERCHANT_LINK).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetFavouriteContents), new { fid = favouritecontents.FAVOR_ID , lid = favouritecontents.LINK_ID }, favouritecontents);
        }


        // PUT: api/FavouriteContents
        [HttpPut("{fid_lid}")]
        public async Task<IActionResult> PutUser(string fid_lid, LD_FAVOURITES_CONTENTS favouritecontents)
        {
            string[] para = fid_lid.Split(new char[] { '&' });
            string fid = para[0];
            string lid = para[1];
            if (fid != favouritecontents.FAVOR_ID || lid !=favouritecontents.LINK_ID)
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
                if (!db.Queryable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == fid && it.LINK_ID == lid).Any())
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
        [HttpDelete("{fid_lid}")]
        public async Task<IActionResult> DeleteFavouriteContents(string fid_lid)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = fid_lid.Split(new char[] { '&' });
            string fid = para[0];
            string lid = para[1];
            var res = await db.Queryable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == fid && it.LINK_ID == lid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_FAVOURITES_CONTENTS>().Where(it => it.FAVOR_ID == fid && it.LINK_ID == lid).ExecuteCommand());
            return NoContent();
        }
    }
}
