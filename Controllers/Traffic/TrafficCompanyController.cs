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
    [Authorize]
    public class TrafficCompanyController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_TRAFFIC_COMPANY>>> GetTrafficCompany()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_TRAFFIC_COMPANY>().ToListAsync();
        }

        // GET: api/TrafficCompany/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_TRAFFIC_COMPANY>>> GetTrafficCompany(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_TRAFFIC_COMPANY>().Where(it => it.COMPANY_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/TrafficCompany
        [HttpPost]
        public async Task<ActionResult<LD_TRAFFIC_COMPANY>> PostTrafficCompany(LD_TRAFFIC_COMPANY company)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(company).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_TRAFFIC_COMPANY>().Where(it => it.COMPANY_ID == company.COMPANY_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetTrafficCompany), new { id = company.COMPANY_ID }, company);
        }


        // PUT: api/TrafficCompany/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrafficCompany(string id, LD_TRAFFIC_COMPANY company)
        {
            if (id != company.COMPANY_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(company).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_TRAFFIC_COMPANY>().Where(it => it.COMPANY_ID == id).Any())
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

        // DELETE: api/TrafficCompany/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrafficCompany(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_TRAFFIC_COMPANY>().Where(it => it.COMPANY_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_TRAFFIC_COMPANY>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
