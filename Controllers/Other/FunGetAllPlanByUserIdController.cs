using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using LvDao.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FunGetAllPlanByUserIdController:ControllerBase
    {
        // GET: api/Plan
        [HttpGet("{userid}")]
        public async Task<ActionResult<IEnumerable<LD_PLAN>>> GetAllPlanByUserId(string userid)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_PLAN>().Where(it => it.USER_ID == userid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }
    }
}
