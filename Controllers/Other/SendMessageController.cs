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
    public class SendMessageController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_SEND_MESSAGE>>> GetFaqs()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_SEND_MESSAGE>().ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_SEND_MESSAGE>>> GetSendMessage(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_SEND_MESSAGE>().Where(it => it.MAIL_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Users
        [HttpPost]
        public async Task<ActionResult<LD_SEND_MESSAGE>> PostUser(LD_SEND_MESSAGE sendmessage)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(sendmessage).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_SEND_MESSAGE>().Where(it => it.MAIL_ID == sendmessage.MAIL_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetSendMessage), new { id = sendmessage.MAIL_ID }, sendmessage);
        }


        // PUT: api/SendMessage
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSendMessage(string id, LD_SEND_MESSAGE sendmessage)
        {
            if (id != sendmessage.MAIL_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(sendmessage).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_SEND_MESSAGE>().Where(it => it.MAIL_ID == id).Any())
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

        // DELETE: api/SendMessage
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSendMessage(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_SEND_MESSAGE>().Where(it => it.MAIL_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_SEND_MESSAGE>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
