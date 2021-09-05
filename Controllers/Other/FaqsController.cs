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
    public class FaqsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_FAQS>>> GetFaqs()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_FAQS>().ToListAsync();
        }

        // GET: api/Faqs
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_FAQS>>> GetFaqs(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_FAQS>().Where(it => it.QUESTION_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Faqs
        [HttpPost]
        public async Task<ActionResult<LD_FAQS>> PostUser(LD_FAQS faqs)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(faqs).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_FAQS>().Where(it => it.QUESTION_ID == faqs.QUESTION_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetFaqs), new { id = faqs.QUESTION_ID }, faqs);
        }


        // PUT: api/Faqs
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaqs(string id, LD_FAQS faqs)
        {
            if (id != faqs.QUESTION_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(faqs).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_FAQS>().Where(it => it.QUESTION_ID == id).Any())
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

        // DELETE: api/Faqs
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaqs(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_FAQS>().Where(it => it.QUESTION_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_FAQS>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
