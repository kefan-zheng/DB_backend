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
    public class CommentMomentController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_COMMENT_MOMENT>>> GetCommentMoment()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_COMMENT_MOMENT>().ToListAsync();
        }

        // GET: api/CommentMoment/5
        [HttpGet("{uid_mid}")]
        public async Task<ActionResult<IEnumerable<LD_COMMENT_MOMENT>>> GetCommentMoment(string uid_mid)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = uid_mid.Split(new char[] { '&' });
            string u_id = para[0];
            string m_id = para[1];
            var res = await db.Queryable<LD_COMMENT_MOMENT>().Where(it => it.MOMENT_ID == m_id && it.USER_ID==u_id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/CommentMoment
        [HttpPost]
        public async Task<ActionResult<LD_COMMENT_MOMENT>> PostCommentMoment(LD_COMMENT_MOMENT comment_moment)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(comment_moment).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_COMMENT_MOMENT>().Where(it => it.MOMENT_ID == comment_moment.MOMENT_ID && it.USER_ID==comment_moment.USER_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetCommentMoment), new { m_id = comment_moment.MOMENT_ID ,u_id=comment_moment.USER_ID}, comment_moment);
        }


        // PUT: api/CommentMoment/5
        [HttpPut("{uid_mid}")]
        public async Task<IActionResult> PutCommentMoment(string uid_mid, LD_COMMENT_MOMENT comment_moment)
        {
            string[] para = uid_mid.Split(new char[] { '&' });
            string u_id = para[0];
            string m_id = para[1];
            if (m_id != comment_moment.MOMENT_ID || u_id != comment_moment.USER_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(comment_moment).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_COMMENT_MOMENT>().Where(it => it.MOMENT_ID == m_id && it.USER_ID==u_id).Any())
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

        // DELETE: api/CommentMoment/5
        [HttpDelete("{uid_mid}")]
        public async Task<IActionResult> DeleteCommentMoment(string uid_mid)
        {
            string[] para = uid_mid.Split(new char[] { '&' });
            string u_id = para[0];
            string m_id = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_COMMENT_MOMENT>().Where(it => it.MOMENT_ID == m_id && it.USER_ID == u_id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_COMMENT_MOMENT>().Where(it => it.MOMENT_ID == m_id && it.USER_ID == u_id).ExecuteCommand());
            return NoContent();
        }
    }
}
