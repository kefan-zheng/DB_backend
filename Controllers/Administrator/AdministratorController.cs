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
    public class AdministratorController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_ADMINISTRATOR>>> GetAdministrator()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_ADMINISTRATOR>().ToListAsync();
        }

        // GET: api/Administrator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_ADMINISTRATOR>>> GetAdministrator(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_ADMINISTRATOR>().Where(it => it.ADMINISTRATOR_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Administrator
        [HttpPost]
        public async Task<ActionResult<LD_ADMINISTRATOR>> PostAdministrator(LD_ADMINISTRATOR administrator)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(administrator).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_ADMINISTRATOR>().Where(it => it.ADMINISTRATOR_ID == administrator.ADMINISTRATOR_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetAdministrator), new { id = administrator.ADMINISTRATOR_ID }, administrator);
        }


        // PUT: api/Administrator/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, LD_ADMINISTRATOR administrator)
        {
            if (id != administrator.ADMINISTRATOR_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(administrator).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_ADMINISTRATOR>().Where(it => it.ADMINISTRATOR_ID == id).Any())
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

        // DELETE: api/Administrator/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrator(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_ADMINISTRATOR>().Where(it => it.ADMINISTRATOR_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_ADMINISTRATOR>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
