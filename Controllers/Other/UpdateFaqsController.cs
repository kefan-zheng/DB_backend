using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvDao;
using LvDao.Models;
using SqlSugar;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UpdateFaqsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_UPDATE_FAQS>>> GetUpdateFaqs()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_UPDATE_FAQS>().ToListAsync();
        }

        // GET: api/UpdateFaqs
        [HttpGet("{Qid_Aid}")]
        public async Task<ActionResult<IEnumerable<LD_UPDATE_FAQS>>> GetUpdateFaqs(string Qid_Aid)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = Qid_Aid.Split(new char[] { '&' });
            string qid = para[0];
            string aid = para[1];
            var res = await db.Queryable<LD_UPDATE_FAQS>().Where(it => it.QUESTION_ID == qid &&it.ADMINISTRATOR_ID == aid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/UpdateFaqs
        [HttpPost]
        public async Task<ActionResult<LD_UPDATE_FAQS>> PostUpdateFaqs(LD_UPDATE_FAQS updatefaqs)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(updatefaqs).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_UPDATE_FAQS>().Where(it => it.QUESTION_ID == updatefaqs.QUESTION_ID ).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetUpdateFaqs), new { id = updatefaqs.QUESTION_ID  }, updatefaqs);
        }


        // PUT: api/UpdateFaqs
        [HttpPut("{Qid_Aid}")]
        public async Task<IActionResult> PutUpdateFaqs(string Qid_Aid, LD_UPDATE_FAQS updatefaqs)
        {

            string[] para = Qid_Aid.Split(new char[] { '&' });
            string qid = para[0];
            string aid = para[1];
            if (qid != updatefaqs.QUESTION_ID || aid!= updatefaqs.ADMINISTRATOR_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(updatefaqs).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_UPDATE_FAQS>().Where(it => it.QUESTION_ID == qid && it.ADMINISTRATOR_ID == aid).Any())
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

        // DELETE: api/UpdateFaqs
        [HttpDelete("{Qid_Aid}")]
        public async Task<IActionResult> DeleteUpdateFaqs(string Qid_Aid)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = Qid_Aid.Split(new char[] { '&' });
            string qid = para[0];
            string aid = para[1];
            var res = await db.Queryable<LD_UPDATE_FAQS>().Where(it => it.QUESTION_ID == qid&&it.ADMINISTRATOR_ID == aid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_UPDATE_FAQS>().Where(it => it.QUESTION_ID == qid && it.ADMINISTRATOR_ID == aid).ExecuteCommand());
            return NoContent();
        }
    }
}
