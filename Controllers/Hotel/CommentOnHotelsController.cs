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
    public class CommentOnHotelsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_COMMENT_ON_HOTELS>>> GetCommentOnHotels()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_COMMENT_ON_HOTELS>().ToListAsync();
        }

        // GET: api/Users
        [HttpGet("{userid_hoteid}")]
        public async Task<ActionResult<IEnumerable<LD_COMMENT_ON_HOTELS>>> GetCommentOnHotels(string userid_hoteid)
        {
            //处理函数
            string[] para = userid_hoteid.Split(new char[] { '&' });
            string userid = para[0];
            string hoteid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_COMMENT_ON_HOTELS>().Where(it => it.USER_ID == userid&&it.HOTEL_ID == hoteid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Users
        [HttpPost]
        public async Task<ActionResult<LD_COMMENT_ON_HOTELS>> PostCommentOnHotels(LD_COMMENT_ON_HOTELS commentonhotels)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(commentonhotels).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_COMMENT_ON_HOTELS>().Where(it => it.USER_ID == commentonhotels.USER_ID&&it.HOTEL_ID == commentonhotels.HOTEL_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetCommentOnHotels), new { userid = commentonhotels.USER_ID,hoteid=commentonhotels.HOTEL_ID }, commentonhotels);
        }


        // PUT: api/Users
        [HttpPut("{userid_hoteid}")]
        public async Task<IActionResult> PutCommentOnHotels(string userid_hoteid, LD_COMMENT_ON_HOTELS commentonhotels)
        {
            //处理字符串
            string[] para = userid_hoteid.Split(new char[] { '&' });
            string userid = para[0];
            string hoteid = para[1];
            if (para[0] != commentonhotels.USER_ID || para[1]!=commentonhotels.HOTEL_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(commentonhotels).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_COMMENT_ON_HOTELS>().Where(it => it.USER_ID == userid&&it.HOTEL_ID == hoteid).Any())
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

        // DELETE: api/Users
        [HttpDelete("{userid_hoteid}")]
        public async Task<IActionResult> DeleteCommentOnHotels(string userid_hoteid)
        {
            //处理函数
            string[] para = userid_hoteid.Split(new char[] { '&' });
            string userid = para[0];
            string hoteid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_COMMENT_ON_HOTELS>().Where(it => it.USER_ID == userid&&it.HOTEL_ID == hoteid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_COMMENT_ON_HOTELS>().Where(it => it.USER_ID == userid&&it.HOTEL_ID == hoteid).ExecuteCommand());
            return NoContent();
        }
    }
}
