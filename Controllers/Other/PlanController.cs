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
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PlanController:ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_PLAN>>> GetPlan()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_PLAN>().ToListAsync();
        }


        // GET: api/Plan
        [HttpGet("{userid_planid}")]
        public async Task<ActionResult<IEnumerable<LD_PLAN>>> GetPlan(string userid_planid)
        {
            string[] para = userid_planid.Split(new char[] { '&' });
            string userid = para[0];
            string planid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_PLAN>().Where(it => it.USER_ID == userid && it.PLAN_ID == int.Parse(planid)).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Plan
        [HttpPost]
        public async Task<ActionResult<LD_PLAN>> PostPlan(LD_PLAN plan)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(plan).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_PLAN>().Where(it => it.USER_ID == plan.USER_ID && it.PLAN_ID == plan.PLAN_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(PostPlan), new { userid = plan.USER_ID,planid = plan.PLAN_ID }, plan);
        }

        // PUT: api/Mail
        [HttpPut("{userid_planid}")]
        public async Task<IActionResult> PutPlan(string userid_planid, LD_PLAN plan)
        {
            string[] para = userid_planid.Split(new char[] { '&' });
            string userid = para[0];
            string planid = para[1];
            if (userid != plan.USER_ID || int.Parse(planid) != plan.PLAN_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(plan).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_PLAN>().Where(it => it.USER_ID == userid && it.PLAN_ID == int.Parse(planid)).Any())
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
    }
}
