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
    public class AuditMomentInfoController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_AUDIT_MOMENT_INFO>>> GetAuditMomentInfo()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_AUDIT_MOMENT_INFO>().ToListAsync();
        }

        // GET: api/AuditMomentInfo/5
        [HttpGet("{mid_aid}")]
        public async Task<ActionResult<IEnumerable<LD_AUDIT_MOMENT_INFO>>> GetAuditMomentInfo(string mid_aid)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = mid_aid.Split(new char[] { '&' });
            string mid = para[0];
            string aid = para[1];
            var res = await db.Queryable<LD_AUDIT_MOMENT_INFO>().Where(it => it.MOMENT_ID == mid && it.ADMINISTRATOR_ID == aid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/AuditMomentInfo
        [HttpPost]
        public async Task<ActionResult<LD_USER>> PostAuditMomentInfo(LD_AUDIT_MOMENT_INFO auditmomentinfo)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(auditmomentinfo).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_AUDIT_MOMENT_INFO>().Where(it => it.MOMENT_ID == auditmomentinfo.MOMENT_ID && it.ADMINISTRATOR_ID == auditmomentinfo.ADMINISTRATOR_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetAuditMomentInfo), new { mid = auditmomentinfo.MOMENT_ID , aid=auditmomentinfo.ADMINISTRATOR_ID }, auditmomentinfo);
        }


        // PUT: api/AuditMomentInfo/5
        [HttpPut("{mid_aid}")]
        public async Task<IActionResult> PutAuditMomentInfo(string mid_aid, LD_AUDIT_MOMENT_INFO auditmomentinfo)
        {
            string[] para = mid_aid.Split(new char[] { '&' });
            string mid = para[0];
            string aid = para[1];
            if (mid != auditmomentinfo.MOMENT_ID || aid!= auditmomentinfo.ADMINISTRATOR_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(auditmomentinfo).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_AUDIT_MOMENT_INFO>().Where(it => it.MOMENT_ID == mid && it.ADMINISTRATOR_ID == aid).Any())
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

        // DELETE: api/AuditMomentInfo/5
        [HttpDelete("{mid_aid}")]
        public async Task<IActionResult> DeleteAuditMomentInfo(string mid_aid)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = mid_aid.Split(new char[] { '&' });
            string mid = para[0];
            string aid = para[1];
            var res = await db.Queryable<LD_AUDIT_MOMENT_INFO>().Where(it => it.MOMENT_ID == mid && it.ADMINISTRATOR_ID == aid ).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable< LD_AUDIT_MOMENT_INFO>().Where(it => it.MOMENT_ID == mid && it.ADMINISTRATOR_ID == aid).ExecuteCommand());
            return NoContent();
        }
    }
}
