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
    public class MailController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_MAIL>>> GetMail()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_MAIL>().ToListAsync();
        }

        // GET: api/Mail
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_MAIL>>> GetMail(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_MAIL>().Where(it => it.MAIL_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Mail
        [HttpPost]
        public async Task<ActionResult<LD_MAIL>> PostMail(LD_MAIL mail)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(mail).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_MAIL>().Where(it => it.MAIL_ID == mail.MAIL_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetMail), new { id = mail.MAIL_ID }, mail);
        }


        // PUT: api/Mail
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMail(string id, LD_MAIL mail)
        {
            if (id != mail.MAIL_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(mail).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_MAIL>().Where(it => it.MAIL_ID == id).Any())
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

        // DELETE: api/Mail
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMail(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_MAIL>().Where(it => it.MAIL_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_MAIL>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
