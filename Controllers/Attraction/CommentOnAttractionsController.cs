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
    public class CommentOnAttractionsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_COMMENT_ON_ATTRACTIONS>>> GetCommentOnAttractions()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_COMMENT_ON_ATTRACTIONS>().ToListAsync();
        }

        // GET: api/CommentOnAttractions
        [HttpGet("{userid_attrid}")]
        public async Task<ActionResult<IEnumerable<LD_COMMENT_ON_ATTRACTIONS>>> GetCommentOnAttractions(string userid_attrid)
        {
            //处理字符串
            string[] para = userid_attrid.Split(new char[] { '&' });
            string userid = para[0];
            string attrid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_COMMENT_ON_ATTRACTIONS>().Where(it => it.USER_ID == userid&&it.ATTRACTION_ID == attrid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/CommentOnAttractions
        [HttpPost]
        public async Task<ActionResult<LD_COMMENT_ON_ATTRACTIONS>> PostCommentOnAttractions(LD_COMMENT_ON_ATTRACTIONS commentonattractions)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(commentonattractions).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_COMMENT_ON_ATTRACTIONS>().Where(it => it.USER_ID == commentonattractions.USER_ID&&it.ATTRACTION_ID==commentonattractions.ATTRACTION_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetCommentOnAttractions), new { userid = commentonattractions.USER_ID,attrid=commentonattractions.ATTRACTION_ID }, commentonattractions);
        }


        // PUT: api/CommentOnAttractions
        [HttpPut("{userid_attrid}")]
        public async Task<IActionResult> PutCommentOnAttractions(string userid_attrid, LD_COMMENT_ON_ATTRACTIONS commentonattractions)
        {
            //处理字符串
            string[] para = userid_attrid.Split(new char[] { '&' });
            string userid = para[0];
            string attrid = para[1];
            if (userid != commentonattractions.USER_ID || attrid !=commentonattractions.ATTRACTION_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(commentonattractions).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_COMMENT_ON_ATTRACTIONS>().Where(it => it.USER_ID == userid&&it.ATTRACTION_ID == attrid).Any())
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

        // DELETE: api/CommentOnAttractions
        [HttpDelete("{userid_attrid}")]
        public async Task<IActionResult> DeleteCommentOnAttractions(string userid_attrid)
        {
            //处理字符串
            string[] para = userid_attrid.Split(new char[] { '&' });
            string userid = para[0];
            string attrid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_COMMENT_ON_ATTRACTIONS>().Where(it => it.USER_ID == userid &&it.ATTRACTION_ID == attrid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_COMMENT_ON_ATTRACTIONS>().Where(it => it.USER_ID == userid &&it.ATTRACTION_ID == attrid).ExecuteCommand());
            return NoContent();
        }
    }
}
